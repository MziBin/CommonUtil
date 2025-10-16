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
            this.label1 = new System.Windows.Forms.Label();
            this.lbShow = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnError = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnFatal = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnWarn = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.btnTrace = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbShow
            // 
            this.rtbShow.Location = new System.Drawing.Point(12, 299);
            this.rtbShow.Name = "rtbShow";
            this.rtbShow.Size = new System.Drawing.Size(403, 139);
            this.rtbShow.TabIndex = 0;
            this.rtbShow.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "richTextBox展示";
            // 
            // lbShow
            // 
            this.lbShow.FormattingEnabled = true;
            this.lbShow.ItemHeight = 12;
            this.lbShow.Location = new System.Drawing.Point(421, 299);
            this.lbShow.Name = "lbShow";
            this.lbShow.Size = new System.Drawing.Size(367, 136);
            this.lbShow.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(421, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "listBox展示";
            // 
            // btnError
            // 
            this.btnError.Location = new System.Drawing.Point(421, 41);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(75, 23);
            this.btnError.TabIndex = 4;
            this.btnError.Text = "error";
            this.btnError.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(403, 21);
            this.textBox1.TabIndex = 5;
            // 
            // btnFatal
            // 
            this.btnFatal.Location = new System.Drawing.Point(423, 12);
            this.btnFatal.Name = "btnFatal";
            this.btnFatal.Size = new System.Drawing.Size(75, 23);
            this.btnFatal.TabIndex = 4;
            this.btnFatal.Text = "fatal";
            this.btnFatal.UseVisualStyleBackColor = true;
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(419, 99);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(75, 23);
            this.btnInfo.TabIndex = 4;
            this.btnInfo.Text = "info";
            this.btnInfo.UseVisualStyleBackColor = true;
            // 
            // btnWarn
            // 
            this.btnWarn.Location = new System.Drawing.Point(421, 70);
            this.btnWarn.Name = "btnWarn";
            this.btnWarn.Size = new System.Drawing.Size(75, 23);
            this.btnWarn.TabIndex = 4;
            this.btnWarn.Text = "warn";
            this.btnWarn.UseVisualStyleBackColor = true;
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(419, 128);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 4;
            this.btnDebug.Text = "debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            // 
            // btnTrace
            // 
            this.btnTrace.Location = new System.Drawing.Point(419, 157);
            this.btnTrace.Name = "btnTrace";
            this.btnTrace.Size = new System.Drawing.Size(75, 23);
            this.btnTrace.TabIndex = 4;
            this.btnTrace.Text = "trace";
            this.btnTrace.UseVisualStyleBackColor = true;
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(419, 186);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 4;
            this.btnAll.Text = "all";
            this.btnAll.UseVisualStyleBackColor = true;
            // 
            // UserLogDemo01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnWarn);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnTrace);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnFatal);
            this.Controls.Add(this.btnError);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbShow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbShow);
            this.Name = "UserLogDemo01";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbShow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnError;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnFatal;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnWarn;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Button btnTrace;
        private System.Windows.Forms.Button btnAll;
    }
}

