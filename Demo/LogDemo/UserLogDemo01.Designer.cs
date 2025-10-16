namespace LogDemo
{
    partial class UserLogDemo01
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbShow = new System.Windows.Forms.RichTextBox();
            this.lbShow = new System.Windows.Forms.ListBox();
            this.btnError = new System.Windows.Forms.Button();
            this.txtMessageLog = new System.Windows.Forms.TextBox();
            this.btnFatal = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnWarn = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.btnTrace = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvShow = new System.Windows.Forms.DataGridView();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbShow
            // 
            this.rtbShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbShow.Location = new System.Drawing.Point(3, 3);
            this.rtbShow.Name = "rtbShow";
            this.rtbShow.ReadOnly = true;
            this.rtbShow.Size = new System.Drawing.Size(634, 288);
            this.rtbShow.TabIndex = 0;
            this.rtbShow.Text = "";
            // 
            // lbShow
            // 
            this.lbShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbShow.FormattingEnabled = true;
            this.lbShow.ItemHeight = 12;
            this.lbShow.Location = new System.Drawing.Point(3, 3);
            this.lbShow.Name = "lbShow";
            this.lbShow.Size = new System.Drawing.Size(634, 288);
            this.lbShow.TabIndex = 2;
            // 
            // btnError
            // 
            this.btnError.Location = new System.Drawing.Point(502, 41);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(75, 23);
            this.btnError.TabIndex = 4;
            this.btnError.Text = "error";
            this.btnError.UseVisualStyleBackColor = true;
            // 
            // txtMessageLog
            // 
            this.txtMessageLog.Location = new System.Drawing.Point(12, 12);
            this.txtMessageLog.Name = "txtMessageLog";
            this.txtMessageLog.Size = new System.Drawing.Size(403, 21);
            this.txtMessageLog.TabIndex = 5;
            // 
            // btnFatal
            // 
            this.btnFatal.Location = new System.Drawing.Point(504, 12);
            this.btnFatal.Name = "btnFatal";
            this.btnFatal.Size = new System.Drawing.Size(75, 23);
            this.btnFatal.TabIndex = 4;
            this.btnFatal.Text = "fatal";
            this.btnFatal.UseVisualStyleBackColor = true;
            this.btnFatal.Click += new System.EventHandler(this.btnFatal_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(585, 12);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(75, 23);
            this.btnInfo.TabIndex = 4;
            this.btnInfo.Text = "info";
            this.btnInfo.UseVisualStyleBackColor = true;
            // 
            // btnWarn
            // 
            this.btnWarn.Location = new System.Drawing.Point(502, 70);
            this.btnWarn.Name = "btnWarn";
            this.btnWarn.Size = new System.Drawing.Size(75, 23);
            this.btnWarn.TabIndex = 4;
            this.btnWarn.Text = "warn";
            this.btnWarn.UseVisualStyleBackColor = true;
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(585, 41);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 4;
            this.btnDebug.Text = "debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            // 
            // btnTrace
            // 
            this.btnTrace.Location = new System.Drawing.Point(585, 70);
            this.btnTrace.Name = "btnTrace";
            this.btnTrace.Size = new System.Drawing.Size(75, 23);
            this.btnTrace.TabIndex = 4;
            this.btnTrace.Text = "trace";
            this.btnTrace.UseVisualStyleBackColor = true;
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(423, 12);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 4;
            this.btnAll.Text = "all";
            this.btnAll.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 99);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(648, 320);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtbShow);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(640, 294);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "RichTextBox";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbShow);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(640, 294);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "listBox";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnSearch);
            this.tabPage3.Controls.Add(this.dtpEnd);
            this.tabPage3.Controls.Add(this.dtpStart);
            this.tabPage3.Controls.Add(this.dgvShow);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(640, 294);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "DataGridView";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvShow
            // 
            this.dgvShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShow.Location = new System.Drawing.Point(3, 40);
            this.dgvShow.Name = "dgvShow";
            this.dgvShow.RowTemplate.Height = 23;
            this.dgvShow.Size = new System.Drawing.Size(634, 251);
            this.dgvShow.TabIndex = 0;
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(3, 13);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(137, 21);
            this.dtpStart.TabIndex = 1;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(146, 13);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(132, 21);
            this.dtpEnd.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(562, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // UserLogDemo01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 444);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtMessageLog);
            this.Controls.Add(this.btnWarn);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnTrace);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnFatal);
            this.Controls.Add(this.btnError);
            this.Name = "UserLogDemo01";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbShow;
        private System.Windows.Forms.ListBox lbShow;
        private System.Windows.Forms.Button btnError;
        private System.Windows.Forms.TextBox txtMessageLog;
        private System.Windows.Forms.Button btnFatal;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnWarn;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Button btnTrace;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DataGridView dgvShow;
    }
}

