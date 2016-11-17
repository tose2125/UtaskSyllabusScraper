namespace UtaskSyllabusScraper
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.fileDropPanel = new System.Windows.Forms.Panel();
            this.mainFormLabel = new System.Windows.Forms.Label();
            this.directorySelectDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fileDropPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileDropPanel
            // 
            this.fileDropPanel.AllowDrop = true;
            this.fileDropPanel.Controls.Add(this.mainFormLabel);
            this.fileDropPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileDropPanel.Location = new System.Drawing.Point(0, 0);
            this.fileDropPanel.Margin = new System.Windows.Forms.Padding(10);
            this.fileDropPanel.Name = "fileDropPanel";
            this.fileDropPanel.Padding = new System.Windows.Forms.Padding(10);
            this.fileDropPanel.Size = new System.Drawing.Size(284, 261);
            this.fileDropPanel.TabIndex = 0;
            this.fileDropPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.fileDropPanel_DragDrop);
            this.fileDropPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.fileDropPanel_DragEnter);
            // 
            // mainFormLabel
            // 
            this.mainFormLabel.AutoSize = true;
            this.mainFormLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainFormLabel.Location = new System.Drawing.Point(10, 10);
            this.mainFormLabel.Name = "mainFormLabel";
            this.mainFormLabel.Size = new System.Drawing.Size(107, 12);
            this.mainFormLabel.TabIndex = 0;
            this.mainFormLabel.Text = "ここにファイルをドロップ";
            // 
            // directorySelectDialog
            // 
            this.directorySelectDialog.Description = "HTMLファイルごとにJSONファイルを出力します。";
            this.directorySelectDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "csv";
            this.saveFileDialog.Filter = "CSV (コンマ区切り)|*.csv|すべてのファイル|*.*";
            this.saveFileDialog.Title = "全HTMLファイルからCSVファイルを出力します。";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.fileDropPanel);
            this.Name = "MainForm";
            this.Text = "UtaskSyllabusScraper";
            this.fileDropPanel.ResumeLayout(false);
            this.fileDropPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel fileDropPanel;
        private System.Windows.Forms.Label mainFormLabel;
        private System.Windows.Forms.FolderBrowserDialog directorySelectDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

