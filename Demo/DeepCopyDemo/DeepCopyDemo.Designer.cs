namespace DeepCopyDemo
{
    partial class DeepCopyDemo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblObject = new System.Windows.Forms.Label();
            this.lblDeepCopyObject = new System.Windows.Forms.Label();
            this.btnBinDeepCopy = new System.Windows.Forms.Button();
            this.btnNewtonsoftJson = new System.Windows.Forms.Button();
            this.btnTextJson = new System.Windows.Forms.Button();
            this.btnXML = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "原对象地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "深度拷贝后地址：";
            // 
            // lblObject
            // 
            this.lblObject.AutoSize = true;
            this.lblObject.Location = new System.Drawing.Point(141, 23);
            this.lblObject.Name = "lblObject";
            this.lblObject.Size = new System.Drawing.Size(41, 12);
            this.lblObject.TabIndex = 2;
            this.lblObject.Text = "label3";
            // 
            // lblDeepCopyObject
            // 
            this.lblDeepCopyObject.AutoSize = true;
            this.lblDeepCopyObject.Location = new System.Drawing.Point(141, 64);
            this.lblDeepCopyObject.Name = "lblDeepCopyObject";
            this.lblDeepCopyObject.Size = new System.Drawing.Size(41, 12);
            this.lblDeepCopyObject.TabIndex = 2;
            this.lblDeepCopyObject.Text = "label3";
            // 
            // btnBinDeepCopy
            // 
            this.btnBinDeepCopy.Location = new System.Drawing.Point(53, 94);
            this.btnBinDeepCopy.Name = "btnBinDeepCopy";
            this.btnBinDeepCopy.Size = new System.Drawing.Size(106, 23);
            this.btnBinDeepCopy.TabIndex = 3;
            this.btnBinDeepCopy.Text = "二进制深度拷贝";
            this.btnBinDeepCopy.UseVisualStyleBackColor = true;
            this.btnBinDeepCopy.Click += new System.EventHandler(this.btnBinDeepCopy_Click);
            // 
            // btnNewtonsoftJson
            // 
            this.btnNewtonsoftJson.Location = new System.Drawing.Point(53, 134);
            this.btnNewtonsoftJson.Name = "btnNewtonsoftJson";
            this.btnNewtonsoftJson.Size = new System.Drawing.Size(106, 23);
            this.btnNewtonsoftJson.TabIndex = 4;
            this.btnNewtonsoftJson.Text = "NewtonsoftJson";
            this.btnNewtonsoftJson.UseVisualStyleBackColor = true;
            this.btnNewtonsoftJson.Click += new System.EventHandler(this.btnNewtonsoftJson_Click);
            // 
            // btnTextJson
            // 
            this.btnTextJson.Location = new System.Drawing.Point(53, 180);
            this.btnTextJson.Name = "btnTextJson";
            this.btnTextJson.Size = new System.Drawing.Size(106, 23);
            this.btnTextJson.TabIndex = 5;
            this.btnTextJson.Text = "TextJson";
            this.btnTextJson.UseVisualStyleBackColor = true;
            this.btnTextJson.Click += new System.EventHandler(this.btnTextJson_Click);
            // 
            // btnXML
            // 
            this.btnXML.Location = new System.Drawing.Point(53, 226);
            this.btnXML.Name = "btnXML";
            this.btnXML.Size = new System.Drawing.Size(106, 23);
            this.btnXML.TabIndex = 6;
            this.btnXML.Text = "XML深度拷贝";
            this.btnXML.UseVisualStyleBackColor = true;
            this.btnXML.Click += new System.EventHandler(this.btnXML_Click);
            // 
            // DeepCopyDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 305);
            this.Controls.Add(this.btnXML);
            this.Controls.Add(this.btnTextJson);
            this.Controls.Add(this.btnNewtonsoftJson);
            this.Controls.Add(this.btnBinDeepCopy);
            this.Controls.Add(this.lblDeepCopyObject);
            this.Controls.Add(this.lblObject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DeepCopyDemo";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblObject;
        private System.Windows.Forms.Label lblDeepCopyObject;
        private System.Windows.Forms.Button btnBinDeepCopy;
        private System.Windows.Forms.Button btnNewtonsoftJson;
        private System.Windows.Forms.Button btnTextJson;
        private System.Windows.Forms.Button btnXML;
    }
}

