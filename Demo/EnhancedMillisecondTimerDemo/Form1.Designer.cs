namespace EnhancedMillisecondTimerDemo
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.btnBasicTest = new System.Windows.Forms.Button();
            this.btnNestedTest = new System.Windows.Forms.Button();
            this.btnMeasureTest = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBasicTest
            // 
            this.btnBasicTest.Location = new System.Drawing.Point(12, 12);
            this.btnBasicTest.Name = "btnBasicTest";
            this.btnBasicTest.Size = new System.Drawing.Size(150, 40);
            this.btnBasicTest.TabIndex = 0;
            this.btnBasicTest.Text = "基本计时测试";
            this.btnBasicTest.UseVisualStyleBackColor = true;
            this.btnBasicTest.Click += new System.EventHandler(this.btnBasicTest_Click);
            // 
            // btnNestedTest
            // 
            this.btnNestedTest.Location = new System.Drawing.Point(168, 12);
            this.btnNestedTest.Name = "btnNestedTest";
            this.btnNestedTest.Size = new System.Drawing.Size(150, 40);
            this.btnNestedTest.TabIndex = 1;
            this.btnNestedTest.Text = "嵌套计时测试";
            this.btnNestedTest.UseVisualStyleBackColor = true;
            this.btnNestedTest.Click += new System.EventHandler(this.btnNestedTest_Click);
            // 
            // btnMeasureTest
            // 
            this.btnMeasureTest.Location = new System.Drawing.Point(324, 12);
            this.btnMeasureTest.Name = "btnMeasureTest";
            this.btnMeasureTest.Size = new System.Drawing.Size(150, 40);
            this.btnMeasureTest.TabIndex = 2;
            this.btnMeasureTest.Text = "MeasureSegment测试";
            this.btnMeasureTest.UseVisualStyleBackColor = true;
            this.btnMeasureTest.Click += new System.EventHandler(this.btnMeasureTest_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 70);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(776, 340);
            this.txtResult.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "测试结果";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnMeasureTest);
            this.Controls.Add(this.btnNestedTest);
            this.Controls.Add(this.btnBasicTest);
            this.Name = "Form1";
            this.Text = "EnhancedMillisecondTimer测试";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnBasicTest;
        private System.Windows.Forms.Button btnNestedTest;
        private System.Windows.Forms.Button btnMeasureTest;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label1;
    }
}

