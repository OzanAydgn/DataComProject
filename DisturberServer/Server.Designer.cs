namespace DisturberServer
{
    partial class Server
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbBitFlip = new System.Windows.Forms.RadioButton();
            this.rbCharReplace = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.lstServerLog = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCharReplace);
            this.groupBox1.Controls.Add(this.rbBitFlip);
            this.groupBox1.Controls.Add(this.rbNone);
            this.groupBox1.Location = new System.Drawing.Point(40, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hata Enjeksiyonu";
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Checked = true;
            this.rbNone.Location = new System.Drawing.Point(7, 22);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(138, 20);
            this.rbNone.TabIndex = 0;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "Hata Yok / Normal";
            this.rbNone.UseVisualStyleBackColor = true;
            // 
            // rbBitFlip
            // 
            this.rbBitFlip.AutoSize = true;
            this.rbBitFlip.Location = new System.Drawing.Point(7, 49);
            this.rbBitFlip.Name = "rbBitFlip";
            this.rbBitFlip.Size = new System.Drawing.Size(146, 20);
            this.rbBitFlip.TabIndex = 1;
            this.rbBitFlip.Text = "Bit Flip - Bit Çevirme";
            this.rbBitFlip.UseVisualStyleBackColor = true;
            // 
            // rbCharReplace
            // 
            this.rbCharReplace.AutoSize = true;
            this.rbCharReplace.Location = new System.Drawing.Point(7, 74);
            this.rbCharReplace.Name = "rbCharReplace";
            this.rbCharReplace.Size = new System.Drawing.Size(239, 20);
            this.rbCharReplace.TabIndex = 2;
            this.rbCharReplace.Text = "Char Replace - Karakter Değiştirme";
            this.rbCharReplace.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(161, 210);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(130, 48);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Server\'ı Başlat";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lstServerLog
            // 
            this.lstServerLog.FormattingEnabled = true;
            this.lstServerLog.ItemHeight = 16;
            this.lstServerLog.Location = new System.Drawing.Point(325, 8);
            this.lstServerLog.Name = "lstServerLog";
            this.lstServerLog.Size = new System.Drawing.Size(463, 404);
            this.lstServerLog.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstServerLog);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCharReplace;
        private System.Windows.Forms.RadioButton rbBitFlip;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lstServerLog;
    }
}

