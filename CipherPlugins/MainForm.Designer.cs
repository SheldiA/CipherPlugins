namespace CipherPlugins
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_do = new System.Windows.Forms.Button();
            this.tb_fromFile = new System.Windows.Forms.TextBox();
            this.tb_toFile = new System.Windows.Forms.TextBox();
            this.bt_openFile = new System.Windows.Forms.Button();
            this.bt_saveFile = new System.Windows.Forms.Button();
            this.cb_chooseAlgorithm = new System.Windows.Forms.ComboBox();
            this.gb_main = new System.Windows.Forms.GroupBox();
            this.rb_decrypt = new System.Windows.Forms.RadioButton();
            this.rb_encrypt = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tb_key = new System.Windows.Forms.TextBox();
            this.pn_Main = new System.Windows.Forms.Panel();
            this.gb_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_do
            // 
            this.bt_do.Location = new System.Drawing.Point(152, 98);
            this.bt_do.Name = "bt_do";
            this.bt_do.Size = new System.Drawing.Size(182, 45);
            this.bt_do.TabIndex = 0;
            this.bt_do.Text = "DO";
            this.bt_do.UseVisualStyleBackColor = true;
            this.bt_do.Click += new System.EventHandler(this.bt_do_Click);
            // 
            // tb_fromFile
            // 
            this.tb_fromFile.Location = new System.Drawing.Point(12, 15);
            this.tb_fromFile.Name = "tb_fromFile";
            this.tb_fromFile.Size = new System.Drawing.Size(144, 20);
            this.tb_fromFile.TabIndex = 1;
            // 
            // tb_toFile
            // 
            this.tb_toFile.Location = new System.Drawing.Point(12, 55);
            this.tb_toFile.Name = "tb_toFile";
            this.tb_toFile.Size = new System.Drawing.Size(144, 20);
            this.tb_toFile.TabIndex = 2;
            // 
            // bt_openFile
            // 
            this.bt_openFile.Location = new System.Drawing.Point(171, 17);
            this.bt_openFile.Name = "bt_openFile";
            this.bt_openFile.Size = new System.Drawing.Size(41, 18);
            this.bt_openFile.TabIndex = 3;
            this.bt_openFile.Text = "...";
            this.bt_openFile.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bt_openFile.UseVisualStyleBackColor = true;
            this.bt_openFile.Click += new System.EventHandler(this.bt_openFile_Click);
            // 
            // bt_saveFile
            // 
            this.bt_saveFile.Location = new System.Drawing.Point(171, 58);
            this.bt_saveFile.Name = "bt_saveFile";
            this.bt_saveFile.Size = new System.Drawing.Size(41, 17);
            this.bt_saveFile.TabIndex = 4;
            this.bt_saveFile.Text = "...";
            this.bt_saveFile.UseVisualStyleBackColor = true;
            this.bt_saveFile.Click += new System.EventHandler(this.bt_saveFile_Click);
            // 
            // cb_chooseAlgorithm
            // 
            this.cb_chooseAlgorithm.FormattingEnabled = true;
            this.cb_chooseAlgorithm.Location = new System.Drawing.Point(240, 15);
            this.cb_chooseAlgorithm.Name = "cb_chooseAlgorithm";
            this.cb_chooseAlgorithm.Size = new System.Drawing.Size(94, 21);
            this.cb_chooseAlgorithm.TabIndex = 5;
            // 
            // gb_main
            // 
            this.gb_main.Controls.Add(this.rb_decrypt);
            this.gb_main.Controls.Add(this.rb_encrypt);
            this.gb_main.Location = new System.Drawing.Point(360, 13);
            this.gb_main.Name = "gb_main";
            this.gb_main.Size = new System.Drawing.Size(111, 62);
            this.gb_main.TabIndex = 6;
            this.gb_main.TabStop = false;
            // 
            // rb_decrypt
            // 
            this.rb_decrypt.AutoSize = true;
            this.rb_decrypt.Location = new System.Drawing.Point(14, 38);
            this.rb_decrypt.Name = "rb_decrypt";
            this.rb_decrypt.Size = new System.Drawing.Size(62, 17);
            this.rb_decrypt.TabIndex = 1;
            this.rb_decrypt.TabStop = true;
            this.rb_decrypt.Text = "Decrypt";
            this.rb_decrypt.UseVisualStyleBackColor = true;
            // 
            // rb_encrypt
            // 
            this.rb_encrypt.AutoSize = true;
            this.rb_encrypt.Checked = true;
            this.rb_encrypt.Location = new System.Drawing.Point(14, 16);
            this.rb_encrypt.Name = "rb_encrypt";
            this.rb_encrypt.Size = new System.Drawing.Size(61, 17);
            this.rb_encrypt.TabIndex = 0;
            this.rb_encrypt.TabStop = true;
            this.rb_encrypt.Text = "Encrypt";
            this.rb_encrypt.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tb_key
            // 
            this.tb_key.Location = new System.Drawing.Point(240, 56);
            this.tb_key.Name = "tb_key";
            this.tb_key.Size = new System.Drawing.Size(111, 20);
            this.tb_key.TabIndex = 8;
            // 
            // pn_Main
            // 
            this.pn_Main.AutoScroll = true;
            this.pn_Main.Location = new System.Drawing.Point(5, 160);
            this.pn_Main.Name = "pn_Main";
            this.pn_Main.Size = new System.Drawing.Size(466, 143);
            this.pn_Main.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 315);
            this.Controls.Add(this.pn_Main);
            this.Controls.Add(this.tb_key);
            this.Controls.Add(this.gb_main);
            this.Controls.Add(this.cb_chooseAlgorithm);
            this.Controls.Add(this.bt_saveFile);
            this.Controls.Add(this.bt_openFile);
            this.Controls.Add(this.tb_toFile);
            this.Controls.Add(this.tb_fromFile);
            this.Controls.Add(this.bt_do);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gb_main.ResumeLayout(false);
            this.gb_main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_do;
        private System.Windows.Forms.TextBox tb_fromFile;
        private System.Windows.Forms.TextBox tb_toFile;
        private System.Windows.Forms.Button bt_openFile;
        private System.Windows.Forms.Button bt_saveFile;
        private System.Windows.Forms.ComboBox cb_chooseAlgorithm;
        private System.Windows.Forms.GroupBox gb_main;
        private System.Windows.Forms.RadioButton rb_decrypt;
        private System.Windows.Forms.RadioButton rb_encrypt;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox tb_key;
        private System.Windows.Forms.Panel pn_Main;
    }
}

