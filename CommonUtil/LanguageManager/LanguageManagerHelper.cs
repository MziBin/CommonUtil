using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonUtil.LanguageManager
{
    public delegate void ChangeDelegate();

    /// <summary>
    /// 语言管理器
    /// 创建人：LGB
    /// 最后修改人：LGB
    /// 创建时间：2025-04-30
    /// 更新时间：2026-01-06
    /// 
    /// 更新内容：
    /// 2025-07-22
    /// 1. 添加了 LocalizableCategoryAttribute 和 LocalizableDescriptionAttribute 类，用于属性的本地化。
    /// 2. 通过这些属性，可以在属性网格中显示本地化的类别和描述。可以多次切换语言，能够实时更新。
    /// 
    /// 使用说明：
    /// 程序初始化时设置下面参数
    /// LanguageManagerHelper.Instance.CurrentLanguage = @"en-US";//获取当前语言,程序加载时读取
    /// LanguageManagerHelper.LanguageFilePath = @"E:\00_WorkCodeSpace\CommonUtil\CommonUtil\LanguageManager\LanguageFile\";//设置json语言文件夹路径
    /// LanguageManagerHelper.Instance.Initialize(this);//后面的窗体都可以直接使用这个方法来初始化语言，将当前窗体的对象传递进来，绑定关闭事件。窗体关闭后自动清理控件引用，防止内存泄漏
    /// 
    /// 切换语言方法
    /// SwitchLanguage("en-US");
    /// 
    /// 后面每个页面的窗体都要在load中加载这个方法。LanguageManagerClass.Instance.Initialize();
    /// 后面改变时，设置当前语言，在调用LanguageManagerClass.Instance.Initialize(this);生效。每个后面打开的窗体都要在load中加载这个方法。
    /// 因为load是在窗体打开时调用的，在load前调用是没有效果的。
    /// 
    /// LanguageManagerHelper.Instance.GetLanguageString("点击");//通过这个可以给出对应的翻译
    /// 
    /// 事件，可以用于在窗体中添加切换语言触发的事件，比如重新更新通过GetLanguageString获得字符串
    /// SelectLanChangeEven
    /// 
    /// 翻译实现逻辑
    /// 1.根据不同国家的JSON文件加载到dictionary中，然后循环将不同的控件的文本内容给翻译。
    /// 2.不同控件的control对象全部都放入到一个专门存储dictionary中。
    /// 3.可以根据不同字符串获取对应的翻译字符串。
    /// 
    /// Todo:
    /// 1. 整理代码规范
    /// 
    /// 
    /// </summary>

    public class LanguageManagerHelper : IDisposable
    {
        // 私有静态只读实例，使用Lazy<T>确保线程安全
        private static readonly Lazy<LanguageManagerHelper> lazyInstance = 
            new Lazy<LanguageManagerHelper>(() => new LanguageManagerHelper());

        // 当前语言
        public string CurrentLanguage { get; set; } = "";
        // 存储语言条目的字典
        public readonly Dictionary<string, Dictionary<string, string>> languageDictionary = new Dictionary<string, Dictionary<string, string>>();
        // 语言文件路径
        public static string LanguageFilePath { get; set; }
        // 用于存储控件和其原始Text的映射关系（使用object作为键，支持多种类型）
        private Dictionary<object, string> _controlOriginalTexts = new Dictionary<object, string>();
        // 用于跟踪已订阅关闭事件的窗体
        private HashSet<Form> _subscribedForms = new HashSet<Form>();

        //语言改变事件
        public event ChangeDelegate SelectLanChangeEven;

        // 私有构造函数，防止外部直接实例化
        private LanguageManagerHelper() { }

        // 公共静态属性，用于获取唯一实例（线程安全）
        public static LanguageManagerHelper Instance => lazyInstance.Value;

        /// <summary>
        /// 初始化语言管理器
        /// </summary>
        /// <param name="languageCode"></param>
        public void Initialize(Form form = null, string languageCode = null)
        {
            if(form != null)
            {
                InitializeFormControlOriginalTexts(form);
            }

            if (string.IsNullOrEmpty(LanguageFilePath))
            {
                return;
            }

            if (languageCode != null)
            {
                CurrentLanguage = languageCode;
            }

            // 加载所有语言文件
            LoadAllLanguages();

            // 初始化控件-原始键映射
            InitializeControlOriginalTexts();
            
            // 应用当前语言到所有窗体
            ApplyCurrentLanguage();
        }

        private void LoadAllLanguages()
        {
            var jsonFiles = GetJsonFiles(LanguageFilePath);
            foreach (string file in jsonFiles)
            {
                LoadLanguage(file);
            }
        }

        /// <summary>
        /// 获取文件夹下面所有 json 文件名称
        /// </summary>
        /// <param name="folderPath">目录路径</param>
        /// <returns></returns>
        private string[] GetJsonFiles(string folderPath)
        {
            try
            {
                return Directory.GetFiles(folderPath, "*.json");
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error getting JSON files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine($"Error getting JSON files: {ex.Message}");
                //return Array.Empty<string>();

                throw new Exception($"Error getting JSON files: {ex.Message}");
            }
        }

        /// <summary>
        /// 根据路劲加载语言 Json 文件
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadLanguage(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    var languageData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);
                    foreach (var lang in languageData)
                    {
                        languageDictionary[lang.Key] = lang.Value;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"Error loading language file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new Exception($"Error loading language file: {ex.Message}");
                }
            }
            else
            {
                //MessageBox.Show($"Language file {filePath} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception($"Language file {filePath} not found.");
            }
        }
        
        /// <summary>
        /// 初始化所有控件的原始文本映射
        /// </summary>
        private void InitializeControlOriginalTexts()
        {
            // 遍历当前应用程序的所有打开的窗体
            foreach (Form form in Application.OpenForms)
            {
                // 为当前窗体的所有控件初始化原始文本映射
                InitializeControlOriginalTextsRecursive(form);
            }
        }
        
        /// <summary>
        /// 为单个窗体的所有控件初始化原始文本映射
        /// </summary>
        /// <param name="form">要初始化的窗体</param>
        public void InitializeFormControlOriginalTexts(Form form)
        {
            // 为当前窗体的所有控件初始化原始文本映射
            InitializeControlOriginalTextsRecursive(form);
            
            // 订阅窗体关闭事件，防止内存泄漏
            SubscribeFormClosedEvent(form);
        }
        
        /// <summary>
        /// 订阅窗体关闭事件，用于清理控件引用
        /// </summary>
        /// <param name="form">要订阅的窗体</param>
        private void SubscribeFormClosedEvent(Form form)
        {
            lock (_subscribedForms)
            {
                if (!_subscribedForms.Contains(form))
                {
                    form.FormClosed += OnFormClosed;
                    _subscribedForms.Add(form);
                }
            }
        }
        
        /// <summary>
        /// 窗体关闭事件处理，用于清理控件引用
        /// </summary>
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is Form form)
            {
                lock (_subscribedForms)
                {
                    _subscribedForms.Remove(form);
                }
                RemoveFormControls(form);
            }
        }
        
        /// <summary>
        /// 移除窗体及其控件的引用，防止内存泄漏
        /// </summary>
        /// <param name="form">要移除的窗体</param>
        private void RemoveFormControls(Form form)
        {
            RemoveControlRecursive(form);
        }
        
        /// <summary>
        /// 递归移除控件引用
        /// </summary>
        /// <param name="control">要移除的控件</param>
        private void RemoveControlRecursive(Control control)
        {
            _controlOriginalTexts.Remove(control);
            
            // 使用ToArray避免在遍历时修改集合
            foreach (Control childControl in control.Controls.OfType<Control>().ToList())
            {
                RemoveControlRecursive(childControl);
            }
        }
        
        /// <summary>
        /// 递归初始化控件的原始文本映射
        /// </summary>
        /// <param name="control"></param>
        private void InitializeControlOriginalTextsRecursive(Control control)
        {
            // 对于每个控件，只在第一次遇到时存储其原始文本
            if (!_controlOriginalTexts.ContainsKey(control))
            {
                _controlOriginalTexts[control] = control.Text;
            }
            
            // 特殊处理MenuStrip，初始化其菜单项的原始文本
            if (control is MenuStrip menuStrip)
            {
                foreach (ToolStripItem item in menuStrip.Items)
                {
                    InitializeMenuItemTextRecursive(item);
                }
            }
            // 特殊处理ContextMenuStrip，初始化其菜单项的原始文本
            else if (control is ContextMenuStrip contextMenuStrip)
            {
                foreach (ToolStripItem item in contextMenuStrip.Items)
                {
                    InitializeMenuItemTextRecursive(item);
                }
            }
            // 特殊处理StatusStrip，初始化其项的原始文本
            else if (control is StatusStrip statusStrip)
            {
                foreach (ToolStripItem item in statusStrip.Items)
                {
                    InitializeSingleToolStripItemText(item);
                }
            }
            // 特殊处理ToolStrip，初始化其项的原始文本
            else if (control is ToolStrip toolStrip)
            {
                foreach (ToolStripItem item in toolStrip.Items)
                {
                    InitializeMenuItemTextRecursive(item);
                }
            }
            // 特殊处理TreeView，初始化其节点的原始文本
            else if (control is TreeView treeView)
            {
                InitializeTreeViewNodes(treeView);
            }
            // 特殊处理ListView，初始化其列标题和列表项的原始文本
            else if (control is ListView listView)
            {
                // 初始化列标题
                foreach (ColumnHeader column in listView.Columns)
                {
                    InitializeSingleColumnHeaderText(column);
                }
                // 初始化列表项
                InitializeListViewItems(listView);
            }
            
            // 递归遍历子控件
            foreach (Control childControl in control.Controls)
            {
                InitializeControlOriginalTextsRecursive(childControl);
            }
        }
        
        /// <summary>
        /// 初始化ListView所有列表项的原始文本
        /// </summary>
        private void InitializeListViewItems(ListView listView)
        {
            // 遍历所有列表项
            for (int i = 0; i < listView.Items.Count; i++)
            {
                ListViewItem item = listView.Items[i];
                // 为列表项本身存储原始文本
                string itemKey = $"{listView.Name}_Item_{i}";
                if (!_controlOriginalTexts.ContainsKey(itemKey))
                {
                    _controlOriginalTexts[itemKey] = item.Text;
                }
                
                // 遍历所有子项
                for (int j = 0; j < item.SubItems.Count; j++)
                {
                    ListViewItem.ListViewSubItem subItem = item.SubItems[j];
                    string subItemKey = $"{listView.Name}_Item_{i}_Sub_{j}";
                    if (!_controlOriginalTexts.ContainsKey(subItemKey))
                    {
                        _controlOriginalTexts[subItemKey] = subItem.Text;
                    }
                }
            }
        }
        
        /// <summary>
        /// 递归初始化菜单项的原始文本
        /// </summary>
        /// <param name="item">要初始化的菜单项</param>
        private void InitializeMenuItemTextRecursive(ToolStripItem item)
        {
            // 初始化当前菜单项的原始文本
            InitializeSingleToolStripItemText(item);
            
            // 如果是子菜单，递归处理其子项
            if (item is ToolStripMenuItem menuItem)
            {
                foreach (ToolStripItem subItem in menuItem.DropDownItems)
                {
                    InitializeMenuItemTextRecursive(subItem);
                }
            }
        }
        
        /// <summary>
        /// 初始化TreeView所有节点的原始文本
        /// </summary>
        private void InitializeTreeViewNodes(TreeView treeView)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                InitializeTreeNodeTextRecursive(node);
            }
        }
        
        /// <summary>
        /// 递归初始化树节点的原始文本
        /// </summary>
        /// <param name="node">要初始化的树节点</param>
        private void InitializeTreeNodeTextRecursive(TreeNode node)
        {
            // 初始化当前节点的原始文本
            InitializeSingleTreeNodeText(node);
            
            // 递归处理子节点
            foreach (TreeNode childNode in node.Nodes)
            {
                InitializeTreeNodeTextRecursive(childNode);
            }
        }
        
        /// <summary>
        /// 为单个控件初始化原始文本映射（仅当控件不在字典中时）
        /// </summary>
        /// <param name="control"></param>
        private void InitializeSingleControlOriginalText(Control control)
        {
            // 对于每个控件，只在第一次遇到时存储其原始文本
            if (!_controlOriginalTexts.ContainsKey(control))
            {
                _controlOriginalTexts[control] = control.Text;
            }
        }
        
        /// <summary>
        /// 更新控件文本
        /// </summary>
        /// <param name="control"></param>
        /// <param name="languageDict"></param>
        public void UpdateControlTexts(Control control, Dictionary<string, string> languageDict)
        {
            // 对于ToolStrip、MenuStrip和StatusStrip控件，跳过初始化控件本身的原始文本
            // 这些控件的原始文本已在专门的方法中处理
            bool isSpecialControl = control is ToolStrip || control is MenuStrip || control is StatusStrip;
            
            // 1. 处理特殊控件：ToolStrip、MenuStrip、StatusStrip
            if (isSpecialControl)
            {
                if (control is MenuStrip menuStrip)
                {
                    UpdateMenuStripTexts(menuStrip, languageDict);
                }
                else if (control is ContextMenuStrip contextMenuStrip)
                {
                    UpdateMenuStripTexts(contextMenuStrip, languageDict);
                }
                else if (control is StatusStrip statusStrip)
                {
                    UpdateStatusStripTexts(statusStrip, languageDict);
                }
                else if (control is ToolStrip toolStrip)
                {
                    UpdateToolStripTexts(toolStrip, languageDict);
                }
            }
            // 2. 处理复合控件：TreeView、ListView、TabControl
            else if (control is TreeView treeView)
            {
                // 确保当前控件的原始文本已存储
                InitializeSingleControlOriginalText(control);
                // 更新TreeView节点文本
                UpdateTreeViewTexts(treeView, languageDict);
            }
            else if (control is ListView listView)
            {
                // 确保当前控件的原始文本已存储
                InitializeSingleControlOriginalText(control);
                // 更新ListView列标题和列表项
                UpdateListViewTexts(listView, languageDict);
            }
            else if (control is TabControl tabControl)
            {
                // 确保当前控件的原始文本已存储
                InitializeSingleControlOriginalText(control);
                // 更新TabControl选项卡
                UpdateTabControlTexts(tabControl, languageDict);
            }
            // 3. 处理普通控件
            else
            {
                // 确保当前控件的原始文本已存储
                InitializeSingleControlOriginalText(control);
                
                // 使用控件的原始文本作为键，而不是当前Text属性
                string originalText = _controlOriginalTexts[control];
                
                if (languageDict != null && languageDict.TryGetValue(originalText, out string translatedText))
                {
                    if (control is TextBoxBase textBox)
                    {
                        textBox.Text = translatedText;
                    }
                    else if (control is Button button)
                    {
                        button.Text = translatedText;
                    }
                    else if (control is Label label)
                    {
                        label.Text = translatedText;
                    }
                    else if (control is GroupBox groupBox)
                    {
                        groupBox.Text = translatedText;
                    }
                    else if (control is RadioButton radioButton)
                    {
                        radioButton.Text = translatedText;
                    }
                    else if (control is CheckBox checkBox)
                    {
                        checkBox.Text = translatedText;
                    }
                    else
                    {
                        // 其他控件类型
                        control.Text = translatedText;
                    }
                }
            }
            
            // 递归遍历子控件
            foreach (Control childControl in control.Controls)
            {
                UpdateControlTexts(childControl, languageDict);
            }
        }
        
        /// <summary>
        /// 更新TabControl的选项卡标题
        /// </summary>
        private void UpdateTabControlTexts(TabControl tabControl, Dictionary<string, string> languageDict)
        {
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                // 确保当前控件的原始文本已存储
                InitializeSingleControlOriginalText(tabPage);
                
                // 使用原始文本作为键查找翻译
                string originalText = _controlOriginalTexts[tabPage];
                
                if (languageDict.TryGetValue(originalText, out string translatedText))
                {
                    tabPage.Text = translatedText;
                }
                
                // 递归处理TabPage内的控件
                UpdateControlTexts(tabPage, languageDict);
            }
        }
        
        /// <summary>
        /// 更新菜单项文本
        /// </summary>
        private void UpdateMenuStripTexts(ToolStrip menuStrip, Dictionary<string, string> languageDict)
        {
            foreach (ToolStripItem item in menuStrip.Items)
            {
                UpdateMenuItemText(item, languageDict);
            }
        }
        
        /// <summary>
        /// 递归更新菜单项文本
        /// </summary>
        private void UpdateMenuItemText(ToolStripItem item, Dictionary<string, string> languageDict)
        {
            // 确保当前控件的原始文本已存储
            InitializeSingleToolStripItemText(item);
            
            // 使用原始文本作为键查找翻译
            string originalText = _controlOriginalTexts[item];
            
            if (languageDict.TryGetValue(originalText, out string translatedText))
            {
                item.Text = translatedText;
            }
            
            // 如果是子菜单，递归处理
            if (item is ToolStripMenuItem menuItem)
            {
                foreach (ToolStripItem subItem in menuItem.DropDownItems)
                {
                    UpdateMenuItemText(subItem, languageDict);
                }
            }
        }
        
        /// <summary>
        /// 更新StatusStrip的标签文本
        /// </summary>
        private void UpdateStatusStripTexts(StatusStrip statusStrip, Dictionary<string, string> languageDict)
        {
            foreach (ToolStripItem item in statusStrip.Items)
            {
                UpdateMenuItemText(item, languageDict);
            }
        }
        
        /// <summary>
        /// 更新ToolStrip的项文本
        /// </summary>
        private void UpdateToolStripTexts(ToolStrip toolStrip, Dictionary<string, string> languageDict)
        {
            foreach (ToolStripItem item in toolStrip.Items)
            {
                UpdateMenuItemText(item, languageDict);
            }
        }
        
        /// <summary>
        /// 更新TreeView的节点文本
        /// </summary>
        private void UpdateTreeViewTexts(TreeView treeView, Dictionary<string, string> languageDict)
        {
            // 确保所有树节点的原始文本都已初始化
            InitializeTreeViewNodes(treeView);
            
            // 更新所有树节点的文本
            foreach (TreeNode node in treeView.Nodes)
            {
                UpdateTreeNodeText(node, languageDict);
            }
        }
        
        /// <summary>
        /// 递归更新TreeNode文本
        /// </summary>
        private void UpdateTreeNodeText(TreeNode node, Dictionary<string, string> languageDict)
        {
            // 确保当前控件的原始文本已存储
            InitializeSingleTreeNodeText(node);
            
            // 使用原始文本作为键查找翻译
            string originalText = _controlOriginalTexts[node];
            
            if (languageDict.TryGetValue(originalText, out string translatedText))
            {
                node.Text = translatedText;
            }
            
            // 递归处理子节点
            foreach (TreeNode childNode in node.Nodes)
            {
                UpdateTreeNodeText(childNode, languageDict);
            }
        }
        
        /// <summary>
        /// 更新ListView的列标题文本和列表项
        /// </summary>
        private void UpdateListViewTexts(ListView listView, Dictionary<string, string> languageDict)
        {
            // 更新列标题
            foreach (ColumnHeader column in listView.Columns)
            {
                // 确保当前控件的原始文本已存储
                InitializeSingleColumnHeaderText(column);
                
                // 使用原始文本作为键查找翻译
                string originalText = _controlOriginalTexts[column];
                
                if (languageDict.TryGetValue(originalText, out string translatedText))
                {
                    column.Text = translatedText;
                }
            }
            
            // 确保所有列表项的原始文本都已初始化
            InitializeListViewItems(listView);
            
            // 更新列表项
            for (int i = 0; i < listView.Items.Count; i++)
            {
                ListViewItem item = listView.Items[i];
                
                // 先获取列表项的原始文本
                string itemKey = $"{listView.Name}_Item_{i}";
                string originalItemText = item.Text;
                
                // 如果原始文本已存储，使用存储的原始文本
                if (_controlOriginalTexts.TryGetValue(itemKey, out string storedOriginalText))
                {
                    originalItemText = storedOriginalText;
                }
                
                // 更新列表项本身
                if (languageDict.TryGetValue(originalItemText, out string itemTranslatedText))
                {
                    item.Text = itemTranslatedText;
                }
                
                // 更新列表项的子项
                for (int j = 1; j < item.SubItems.Count; j++) // 从1开始，跳过第0项（与列表项本身相同）
                {
                    ListViewItem.ListViewSubItem subItem = item.SubItems[j];
                    string subItemKey = $"{listView.Name}_Item_{i}_Sub_{j}";
                    string originalSubItemText = subItem.Text;
                    
                    // 如果原始文本已存储，使用存储的原始文本
                    if (_controlOriginalTexts.TryGetValue(subItemKey, out string storedSubOriginalText))
                    {
                        originalSubItemText = storedSubOriginalText;
                    }
                    
                    // 更新子项
                    if (languageDict.TryGetValue(originalSubItemText, out string subItemTranslatedText))
                    {
                        subItem.Text = subItemTranslatedText;
                    }
                }
            }
        }
        
        /// <summary>
        /// 为ToolStripItem初始化原始文本映射
        /// </summary>
        private void InitializeSingleToolStripItemText(ToolStripItem item)
        {
            if (!_controlOriginalTexts.ContainsKey(item))
            {
                _controlOriginalTexts[item] = item.Text;
            }
        }
        
        /// <summary>
        /// 为TreeNode初始化原始文本映射
        /// </summary>
        private void InitializeSingleTreeNodeText(TreeNode node)
        {
            if (!_controlOriginalTexts.ContainsKey(node))
            {
                _controlOriginalTexts[node] = node.Text;
            }
        }
        
        /// <summary>
        /// 为ColumnHeader初始化原始文本映射
        /// </summary>
        private void InitializeSingleColumnHeaderText(ColumnHeader column)
        {
            if (!_controlOriginalTexts.ContainsKey(column))
            {
                _controlOriginalTexts[column] = column.Text;
            }
        }
        
        /// <summary>
        /// 应用当前语言到所有窗体
        /// </summary>
        private void ApplyCurrentLanguage()
        {
            if (languageDictionary.TryGetValue(CurrentLanguage, out var currentLanguageDict))
            {
                // 遍历当前应用程序的所有打开的窗体
                foreach (Form form in Application.OpenForms)
                {
                    // 更新当前窗体的控件文本
                    UpdateControlTexts(form, currentLanguageDict);
                }
            }
        }

        /// <summary>
        /// 获取当前语言的字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetLanguageString(string key)
        {
            if (languageDictionary.TryGetValue(CurrentLanguage, out var currentLanguageDict) && currentLanguageDict.TryGetValue(key, out string value))
            {
                return value;
            }
            return key; // 如果没有找到对应的语言字符串，返回原始键
        }

        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="languageCode"></param>
        public void SwitchLanguage(string languageCode)
        {
            CurrentLanguage = languageCode;

            //事件订阅不为空触发订阅事件
            SelectLanChangeEven?.Invoke();

            ApplyCurrentLanguage();
            RefreshPropertyGrids(); // 新增：刷新所有属性网格
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // 清理所有已订阅窗体的事件监听
                    lock (_subscribedForms)
                    {
                        foreach (var form in _subscribedForms.ToList())
                        {
                            form.FormClosed -= OnFormClosed;
                        }
                        _subscribedForms.Clear();
                    }
                    
                    // 清理控件字典
                    _controlOriginalTexts.Clear();
                    
                    // 取消事件订阅
                    SelectLanChangeEven = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region 属性

        /// <summary>
        /// 通过资源键获取本地化字符串
        /// </summary>
        /// <param name="resourceKey">资源键</param>
        /// <returns>本地化字符串，若未找到则返回键本身</returns>
        public string GetLocalizedString(string resourceKey)
        {
            if (languageDictionary.TryGetValue(CurrentLanguage, out var currentLanguageDict) &&
                currentLanguageDict.TryGetValue(resourceKey, out string value))
            {
                return value;
            }
            return resourceKey; // 未找到时返回键本身
        }

        /// <summary>
        /// 刷新所有 PropertyGrid 控件，使属性标签的语言更新
        /// </summary>
        public void RefreshPropertyGrids()
        {
            foreach (Form form in Application.OpenForms)
            {
                RefreshPropertyGridsRecursive(form);
            }
        }

        private void RefreshPropertyGridsRecursive(Control control)
        {
            if (control is PropertyGrid propertyGrid)
            {
                object selectedObj = propertyGrid.SelectedObject;
                propertyGrid.SelectedObject = null;
                propertyGrid.SelectedObject = selectedObj; // 强制刷新属性网格
            }

            // 递归处理子控件
            foreach (Control child in control.Controls)
            {
                RefreshPropertyGridsRecursive(child);
            }
        }
        #endregion


    }

    #region 属性的多语言化
    /*
     * 在属性上使用示例：
     *  [Browsable(false)]
     *  [LocalizableCategory("匹配属性")]
     *  [LocalizableDisplayName("00流水线瓶子面积使能")]
     *  [LocalizableDescription("00是否开启流水线瓶子面积大小")]
     */
    // 可本地化的 Category 属性
    public class LocalizableCategoryAttribute : CategoryAttribute
    {
        private readonly string _resourceKey;

        public LocalizableCategoryAttribute(string resourceKey)
            : base(resourceKey)
        {
            _resourceKey = resourceKey;
        }

        protected override string GetLocalizedString(string value)
        {
            // 从语言管理器获取本地化字符串
            return LanguageManagerHelper.Instance.GetLocalizedString(_resourceKey);
        }
    }

    // 可本地化的 Description 属性
    public class LocalizableDescriptionAttribute : DescriptionAttribute
    {
        private readonly string _resourceKey;
        private bool _localized;

        public LocalizableDescriptionAttribute(string resourceKey)
            : base(resourceKey)
        {
            _resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                if (!_localized)
                {
                    // 从语言管理器获取本地化字符串
                    string description = LanguageManagerHelper.Instance.GetLocalizedString(_resourceKey);
                    if (!string.IsNullOrEmpty(description))
                    {
                        DescriptionValue = description;
                    }
                    _localized = true;
                }
                return base.Description;
            }
        }
    }

    //可本地化的 DisplayName 属性
    public class LocalizableDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string _resourceKey;
        private bool _localized;

        public LocalizableDisplayNameAttribute(string resourceKey)
            : base(resourceKey)
        {
            _resourceKey = resourceKey;
        }

        public override string DisplayName
        {
            get
            {
                if (!_localized)
                {
                    // 从语言管理器获取本地化字符串
                    string displayName = LanguageManagerHelper.Instance.GetLocalizedString(_resourceKey);
                    if (!string.IsNullOrEmpty(displayName))
                    {
                        DisplayNameValue = displayName;
                    }
                    _localized = true;
                }
                return base.DisplayName;
            }
        }
    }
    #endregion

}
