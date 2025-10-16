using CommonUtil.Log.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogDemo
{
    public partial class UserLogDemo01 : Form
    {
        ILog iLog = CommonUtil.Log.Implement.UserLog.Instance;

        public UserLogDemo01()
        {
            InitializeComponent();
            Load += UserLogDemo01_Load;
        }

        private void UserLogDemo01_Load(object sender, EventArgs e)
        {
            iLog.LogWrite(LogLevel.INFO, "UserLogDemo01_Load");
        }

        private void btnFatal_Click(object sender, EventArgs e)
        {
            iLog.LogWrite(LogLevel.FATAL, "btnFatal_Click");
            rtbShow.Text += "FATAL 日志已写入" + Environment.NewLine;
            //滑到底部
            rtbShow.SelectionStart = rtbShow.Text.Length;
            rtbShow.ScrollToCaret();

           lbShow.Items.Add("FATAL 日志已写入");
            lbShow.SelectedIndex = lbShow.Items.Count - 1;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string dateStart = dtpStart.Value.ToString("yyyy-MM-dd");
            string dateEnd = dtpEnd.Value.ToString("yyyy-MM-dd");
            //查询展示到datagridview中
            //1.获取日志文件路径
            string logPath = iLog.ALLDir;
            List<string> logFiles = new List<string>();
            for (DateTime date = dtpStart.Value; date <= dtpEnd.Value; date = date.AddDays(1))
            {
                string filePath = logPath + date.ToString("yyyyMMdd") + ".log";
                if (System.IO.File.Exists(filePath))
                {
                    logFiles.Add(filePath);
                }
            }
            //2.读取日志文件内容
            DataTable dt = new DataTable();
            dt.Columns.Add("日期", typeof(string));
            dt.Columns.Add("时间", typeof(string));
            dt.Columns.Add("级别", typeof(string));
            dt.Columns.Add("线程", typeof(string));
            dt.Columns.Add("内容", typeof(string));

            //3.解析日志内容
            //3.1将日志文件内容读取到一个list集合中
            List<string> logLines = new List<string>();
            foreach (string line in logFiles)
            {
                File.ReadAllLines(line).ToList().ForEach(x => logLines.Add(x));
            }
            //3.2内容解析
            dt.Rows.Clear();
            for (int i = 0; i < logLines.Count; i++)
            {
                string line = logLines[i];
                
                dt.Rows.Add(line.Split(new string[] { "$&$" }, StringSplitOptions.None));
            }

            dgvShow.DataSource = dt;

        }
    }
}
