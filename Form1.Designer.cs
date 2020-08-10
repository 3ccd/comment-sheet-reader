namespace CommentSheetOMR
{
    partial class CommentSheetReader
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.startScanButton = new System.Windows.Forms.Button();
            this.StatusBox = new System.Windows.Forms.GroupBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.CalcStartButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBar3Value = new System.Windows.Forms.Label();
            this.trackBar2Value = new System.Windows.Forms.Label();
            this.trackBar1Value = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resultBox = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataSaveButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.folderButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.StatusBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // startScanButton
            // 
            this.startScanButton.Location = new System.Drawing.Point(827, 108);
            this.startScanButton.Name = "startScanButton";
            this.startScanButton.Size = new System.Drawing.Size(313, 64);
            this.startScanButton.TabIndex = 1;
            this.startScanButton.Text = "スキャン";
            this.startScanButton.UseVisualStyleBackColor = true;
            this.startScanButton.Click += new System.EventHandler(this.StartScanButton_Click);
            // 
            // StatusBox
            // 
            this.StatusBox.Controls.Add(this.statusLabel);
            this.StatusBox.Location = new System.Drawing.Point(827, 12);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(313, 90);
            this.StatusBox.TabIndex = 2;
            this.StatusBox.TabStop = false;
            this.StatusBox.Text = "Status";
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.statusLabel.Location = new System.Drawing.Point(6, 15);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(301, 72);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "スキャナが接続されていることを確認してからスキャンを開始してください。\r\nスキャンは「スキャン開始」ボタンをクリックします。";
            // 
            // CalcStartButton
            // 
            this.CalcStartButton.Enabled = false;
            this.CalcStartButton.Location = new System.Drawing.Point(827, 402);
            this.CalcStartButton.Name = "CalcStartButton";
            this.CalcStartButton.Size = new System.Drawing.Size(313, 63);
            this.CalcStartButton.TabIndex = 3;
            this.CalcStartButton.Text = "読取開始";
            this.CalcStartButton.UseVisualStyleBackColor = true;
            this.CalcStartButton.Click += new System.EventHandler(this.CalcStartButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBar3Value);
            this.groupBox1.Controls.Add(this.trackBar2Value);
            this.groupBox1.Controls.Add(this.trackBar1Value);
            this.groupBox1.Controls.Add(this.trackBar3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Location = new System.Drawing.Point(827, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 218);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "読取領域の設定";
            // 
            // trackBar3Value
            // 
            this.trackBar3Value.Location = new System.Drawing.Point(196, 145);
            this.trackBar3Value.Name = "trackBar3Value";
            this.trackBar3Value.Size = new System.Drawing.Size(109, 12);
            this.trackBar3Value.TabIndex = 8;
            this.trackBar3Value.Text = "value";
            this.trackBar3Value.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackBar2Value
            // 
            this.trackBar2Value.Location = new System.Drawing.Point(196, 82);
            this.trackBar2Value.Name = "trackBar2Value";
            this.trackBar2Value.Size = new System.Drawing.Size(109, 12);
            this.trackBar2Value.TabIndex = 7;
            this.trackBar2Value.Text = "value";
            this.trackBar2Value.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackBar1Value
            // 
            this.trackBar1Value.Location = new System.Drawing.Point(198, 19);
            this.trackBar1Value.Name = "trackBar1Value";
            this.trackBar1Value.Size = new System.Drawing.Size(109, 12);
            this.trackBar1Value.TabIndex = 6;
            this.trackBar1Value.Text = "value";
            this.trackBar1Value.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(6, 160);
            this.trackBar3.Maximum = 100;
            this.trackBar3.Minimum = 10;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(299, 45);
            this.trackBar3.TabIndex = 5;
            this.trackBar3.Value = 40;
            this.trackBar3.Scroll += new System.EventHandler(this.TrackBar3_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "マークと自由記述の区別閾値";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "マークの読み取り閾値";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(8, 97);
            this.trackBar2.Maximum = 20;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(299, 45);
            this.trackBar2.TabIndex = 2;
            this.trackBar2.Value = 5;
            this.trackBar2.Scroll += new System.EventHandler(this.TrackBar2_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "マークの読み取り範囲";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(8, 34);
            this.trackBar1.Maximum = 15;
            this.trackBar1.Minimum = 3;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(299, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Value = 6;
            this.trackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.resultBox);
            this.groupBox2.Location = new System.Drawing.Point(501, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 685);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "読取結果";
            // 
            // resultBox
            // 
            this.resultBox.BackColor = System.Drawing.SystemColors.Window;
            this.resultBox.FormattingEnabled = true;
            this.resultBox.HorizontalScrollbar = true;
            this.resultBox.ItemHeight = 12;
            this.resultBox.Location = new System.Drawing.Point(6, 20);
            this.resultBox.Name = "resultBox";
            this.resultBox.ScrollAlwaysVisible = true;
            this.resultBox.Size = new System.Drawing.Size(308, 652);
            this.resultBox.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(482, 684);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // dataSaveButton
            // 
            this.dataSaveButton.Enabled = false;
            this.dataSaveButton.Location = new System.Drawing.Point(827, 636);
            this.dataSaveButton.Name = "dataSaveButton";
            this.dataSaveButton.Size = new System.Drawing.Size(313, 61);
            this.dataSaveButton.TabIndex = 9;
            this.dataSaveButton.Text = "データ保存";
            this.dataSaveButton.UseVisualStyleBackColor = true;
            this.dataSaveButton.Click += new System.EventHandler(this.DataSaveButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.folderButton);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Location = new System.Drawing.Point(827, 471);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(313, 159);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "保存設定";
            // 
            // folderButton
            // 
            this.folderButton.Location = new System.Drawing.Point(265, 19);
            this.folderButton.Name = "folderButton";
            this.folderButton.Size = new System.Drawing.Size(40, 19);
            this.folderButton.TabIndex = 7;
            this.folderButton.Text = "参照";
            this.folderButton.UseVisualStyleBackColor = true;
            this.folderButton.Click += new System.EventHandler(this.FolderButton_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(299, 90);
            this.label8.TabIndex = 6;
            this.label8.Text = "例：2019_goudoten\r\n同じアンケート用紙を扱う場合、このファイル名は一致させてください。異なるCSVファイルへ出力されてしまう可能性があります。\r\n\r" +
    "\nこのファイル名での保存が初回でない場合、既に存在するデータへ追記されます。";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(78, 44);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(227, 19);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "result";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "ファイル名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "保存フォルダ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(78, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(181, 19);
            this.textBox1.TabIndex = 0;
            // 
            // CommentSheetReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 709);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dataSaveButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CalcStartButton);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.startScanButton);
            this.Name = "CommentSheetReader";
            this.Text = "CommentSheetReader";
            this.StatusBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startScanButton;
        private System.Windows.Forms.GroupBox StatusBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button CalcStartButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox resultBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.Button dataSaveButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button folderButton;
        private System.Windows.Forms.Label trackBar3Value;
        private System.Windows.Forms.Label trackBar2Value;
        private System.Windows.Forms.Label trackBar1Value;
    }
}

