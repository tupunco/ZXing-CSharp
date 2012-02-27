namespace TestZXingDemo
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGen = new System.Windows.Forms.Button();
            this.buttonGenM3 = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.buttonDecode = new System.Windows.Forms.Button();
            this.textBoxDecodeFilePath = new System.Windows.Forms.TextBox();
            this.buttonDecodeSelectFile = new System.Windows.Forms.Button();
            this.openFileDialogDecode = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // buttonGen
            // 
            this.buttonGen.Location = new System.Drawing.Point(13, 12);
            this.buttonGen.Name = "buttonGen";
            this.buttonGen.Size = new System.Drawing.Size(139, 30);
            this.buttonGen.TabIndex = 0;
            this.buttonGen.Text = "Gen QR Code";
            this.buttonGen.UseVisualStyleBackColor = true;
            this.buttonGen.Click += new System.EventHandler(this.buttonGen_Click);
            // 
            // buttonGenM3
            // 
            this.buttonGenM3.Location = new System.Drawing.Point(13, 48);
            this.buttonGenM3.Name = "buttonGenM3";
            this.buttonGenM3.Size = new System.Drawing.Size(169, 30);
            this.buttonGenM3.TabIndex = 1;
            this.buttonGenM3.Text = "Gen Micro MQ Code";
            this.buttonGenM3.UseVisualStyleBackColor = true;
            this.buttonGenM3.Click += new System.EventHandler(this.buttonGenM3_Click);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResult.Location = new System.Drawing.Point(13, 172);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(628, 235);
            this.textBoxResult.TabIndex = 2;
            // 
            // buttonDecode
            // 
            this.buttonDecode.Location = new System.Drawing.Point(170, 106);
            this.buttonDecode.Name = "buttonDecode";
            this.buttonDecode.Size = new System.Drawing.Size(86, 31);
            this.buttonDecode.TabIndex = 3;
            this.buttonDecode.Text = "Decode";
            this.buttonDecode.UseVisualStyleBackColor = true;
            this.buttonDecode.Click += new System.EventHandler(this.buttonDecode_Click);
            // 
            // textBoxDecodeFilePath
            // 
            this.textBoxDecodeFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDecodeFilePath.Location = new System.Drawing.Point(13, 143);
            this.textBoxDecodeFilePath.Name = "textBoxDecodeFilePath";
            this.textBoxDecodeFilePath.Size = new System.Drawing.Size(628, 21);
            this.textBoxDecodeFilePath.TabIndex = 4;
            // 
            // buttonDecodeSelectFile
            // 
            this.buttonDecodeSelectFile.Location = new System.Drawing.Point(13, 106);
            this.buttonDecodeSelectFile.Name = "buttonDecodeSelectFile";
            this.buttonDecodeSelectFile.Size = new System.Drawing.Size(139, 31);
            this.buttonDecodeSelectFile.TabIndex = 5;
            this.buttonDecodeSelectFile.Text = "Select Decode File";
            this.buttonDecodeSelectFile.UseVisualStyleBackColor = true;
            this.buttonDecodeSelectFile.Click += new System.EventHandler(this.buttonDecodeSelectFile_Click);
            // 
            // openFileDialogDecode
            // 
            this.openFileDialogDecode.Filter = ".PNG|*.png|.JPG|*.jpg";
            this.openFileDialogDecode.Title = "Select Decode File";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 419);
            this.Controls.Add(this.buttonDecodeSelectFile);
            this.Controls.Add(this.textBoxDecodeFilePath);
            this.Controls.Add(this.buttonDecode);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.buttonGenM3);
            this.Controls.Add(this.buttonGen);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGen;
        private System.Windows.Forms.Button buttonGenM3;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Button buttonDecode;
        private System.Windows.Forms.TextBox textBoxDecodeFilePath;
        private System.Windows.Forms.Button buttonDecodeSelectFile;
        private System.Windows.Forms.OpenFileDialog openFileDialogDecode;
    }
}

