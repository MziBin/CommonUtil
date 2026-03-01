using CommonUtil.LanguageManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanguageDemo
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Load += Login_Load;
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            LanguageManagerHelper.Instance.CurrentLanguage = @"en-US";//获取当前语言,程序加载时读取
            LanguageManagerHelper.LanguageFilePath = @"D:\code\lgb\CommonUtil-20260105-a\CommonUtil\LanguageManager\LanguageFile\";//设置json语言文件夹路径
            LanguageManagerHelper.Instance.Initialize();//后面的窗体都可以直接使用这个方法来初始化语言
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string languageStr = comboBox1.Text;
            LanguageManagerHelper.Instance.SwitchLanguage(languageStr);
            //LanguageManagerHelper.Instance.CurrentLanguage = languageStr;//获取当前语言,程序加载时读取
            //LanguageManagerHelper.Instance.Initialize();//后面的窗体都可以直接使用这个方法来初始化语言
            //this.Refresh();
        }
    }
}
