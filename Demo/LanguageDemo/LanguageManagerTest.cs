using System;
using System.Windows.Forms;
using CommonUtil.LanguageManager;

namespace LanguageDemo
{
    public partial class LanguageManagerTest : Form
    {
        public LanguageManagerTest()
        {
            InitializeComponent();
            InitializeTestData();
            Load += LanguageManagerTest_Load;
            //InitializeLanguageManager();
        }

        private void LanguageManagerTest_Load(object sender, EventArgs e)
        {
            InitializeLanguageManager();
        }

        private void InitializeLanguageManager()
        {
            // 设置语言文件路径
            //string languageFilePath = "D:\\code\\lgb\\CommonUtil-20260106-b\\CommonUtil\\LanguageManager\\LanguageFile";
            string languageFilePath = "E:\\00_WorkCodeSpace\\CommonUtil-20260109-a\\CommonUtil\\LanguageManager\\LanguageFile\\";
            LanguageManagerHelper.LanguageFilePath = languageFilePath;
            
            // 设置当前语言
            LanguageManagerHelper.Instance.CurrentLanguage = "zh-CN";
            
            // 初始化语言（传入当前窗体，以便订阅关闭事件，防止内存泄漏）
            LanguageManagerHelper.Instance.Initialize(this);
            
            // 订阅语言切换事件
            LanguageManagerHelper.Instance.SelectLanChangeEven += OnLanguageChanged;
        }

        private void InitializeTestData()
        {
            // 初始化TreeView测试数据
            InitializeTreeViewData();
            
            // 初始化ListView测试数据
            InitializeListViewData();
        }

        private void InitializeTreeViewData()
        {
            // 添加根节点
            TreeNode rootNode1 = treeView1.Nodes.Add("根节点1");
            TreeNode rootNode2 = treeView1.Nodes.Add("根节点2");
            
            // 添加子节点
            rootNode1.Nodes.Add("子节点1-1");
            rootNode1.Nodes.Add("子节点1-2");
            rootNode2.Nodes.Add("子节点2-1");
            rootNode2.Nodes.Add("子节点2-2");
            
            // 展开所有节点
            treeView1.ExpandAll();
        }

        private void InitializeListViewData()
        {
            // 添加测试数据
            listView1.Items.Add(new ListViewItem(new string[] { "行1列1", "行1列2" }));
            listView1.Items.Add(new ListViewItem(new string[] { "行2列1", "行2列2" }));
            listView1.Items.Add(new ListViewItem(new string[] { "行3列1", "行3列2" }));
        }

        private void OnLanguageChanged()
        {
            // 语言切换时的处理
            MessageBox.Show($"语言已切换：{LanguageManagerHelper.Instance.CurrentLanguage}\n当前打开的窗体数：{Application.OpenForms.Count}");
        }

        private void btnSwitchToEnglish_Click(object sender, EventArgs e)
        {
            // 测试：切换到英文
            LanguageManagerHelper.Instance.SwitchLanguage("en-US");
        }

        private void btnSwitchToChinese_Click(object sender, EventArgs e)
        {
            // 测试：切换到中文
            LanguageManagerHelper.Instance.SwitchLanguage("zh-CN");
        }

        private void btnTestGetLanguageString_Click(object sender, EventArgs e)
        {
            // 测试GetLanguageString方法
            string hello = LanguageManagerHelper.Instance.GetLanguageString("Hello");
            string world = LanguageManagerHelper.Instance.GetLanguageString("World");
            string btnText = LanguageManagerHelper.Instance.GetLanguageString("切换到英文");
            MessageBox.Show($"翻译结果：\nHello → {hello}\nWorld → {world}\n切换到英文 → {btnText}");
        }
    }
}