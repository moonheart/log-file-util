namespace log_file_util
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnConvertEncoding = new System.Windows.Forms.Button();
            this.cbTargetEncoding = new System.Windows.Forms.ComboBox();
            this.tbRegex = new System.Windows.Forms.TextBox();
            this.tbReplacement = new System.Windows.Forms.TextBox();
            this.btnRegexReplace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(546, 190);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件名";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "编码";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "大小(字节)";
            this.columnHeader3.Width = 110;
            // 
            // btnConvertEncoding
            // 
            this.btnConvertEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConvertEncoding.Location = new System.Drawing.Point(139, 235);
            this.btnConvertEncoding.Name = "btnConvertEncoding";
            this.btnConvertEncoding.Size = new System.Drawing.Size(75, 23);
            this.btnConvertEncoding.TabIndex = 2;
            this.btnConvertEncoding.Text = "转换编码";
            this.btnConvertEncoding.UseVisualStyleBackColor = true;
            this.btnConvertEncoding.Click += new System.EventHandler(this.btnConvertEncoding_Click);
            // 
            // cbTargetEncoding
            // 
            this.cbTargetEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbTargetEncoding.FormattingEnabled = true;
            this.cbTargetEncoding.Location = new System.Drawing.Point(12, 237);
            this.cbTargetEncoding.Name = "cbTargetEncoding";
            this.cbTargetEncoding.Size = new System.Drawing.Size(121, 20);
            this.cbTargetEncoding.TabIndex = 3;
            // 
            // tbRegex
            // 
            this.tbRegex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbRegex.Location = new System.Drawing.Point(47, 261);
            this.tbRegex.Name = "tbRegex";
            this.tbRegex.Size = new System.Drawing.Size(167, 21);
            this.tbRegex.TabIndex = 4;
            // 
            // tbReplacement
            // 
            this.tbReplacement.Location = new System.Drawing.Point(255, 261);
            this.tbReplacement.Name = "tbReplacement";
            this.tbReplacement.Size = new System.Drawing.Size(100, 21);
            this.tbReplacement.TabIndex = 4;
            // 
            // btnRegexReplace
            // 
            this.btnRegexReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRegexReplace.Location = new System.Drawing.Point(361, 261);
            this.btnRegexReplace.Name = "btnRegexReplace";
            this.btnRegexReplace.Size = new System.Drawing.Size(75, 23);
            this.btnRegexReplace.TabIndex = 5;
            this.btnRegexReplace.Text = "正则替换";
            this.btnRegexReplace.UseVisualStyleBackColor = true;
            this.btnRegexReplace.Click += new System.EventHandler(this.btnRegexReplace_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "正则";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "替换";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 208);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(546, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 296);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegexReplace);
            this.Controls.Add(this.tbReplacement);
            this.Controls.Add(this.tbRegex);
            this.Controls.Add(this.cbTargetEncoding);
            this.Controls.Add(this.btnConvertEncoding);
            this.Controls.Add(this.listView1);
            this.Name = "MainForm";
            this.Text = "日志批处理工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnConvertEncoding;
        private System.Windows.Forms.ComboBox cbTargetEncoding;
        private System.Windows.Forms.TextBox tbRegex;
        private System.Windows.Forms.TextBox tbReplacement;
        private System.Windows.Forms.Button btnRegexReplace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}