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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            Load += Home_Load;
            LanguageManagerHelper.Instance.SelectLanChangeEven += Language_SelectLanChangeEven;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            LanguageManagerHelper.Instance.Initialize(this);//后面的窗体都可以直接使用这个方法来初始化语言
        }

        private void Language_SelectLanChangeEven()
        {
            button1.Text = LanguageManagerHelper.Instance.GetLanguageString("登录");
        }

    }
}
