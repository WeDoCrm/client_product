namespace Client
{
    partial class Notice
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notice));
            this.textBox = new System.Windows.Forms.TextBox();
            this.pbx_notice_n = new System.Windows.Forms.PictureBox();
            this.pbx_notice_e = new System.Windows.Forms.PictureBox();
            this.label_title = new System.Windows.Forms.Label();
            this.label_sender = new System.Windows.Forms.Label();
            this.label_noticetitle = new System.Windows.Forms.Label();
            this.label_notice_sender = new System.Windows.Forms.Label();
            this.btn_confirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_notice_n)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_notice_e)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox.ForeColor = System.Drawing.Color.Black;
            this.textBox.Location = new System.Drawing.Point(1, 84);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(414, 153);
            this.textBox.TabIndex = 0;
            // 
            // pbx_notice_n
            // 
            this.pbx_notice_n.Image = global::Client.Properties.Resources.img_notice_n;
            this.pbx_notice_n.Location = new System.Drawing.Point(150, 5);
            this.pbx_notice_n.Name = "pbx_notice_n";
            this.pbx_notice_n.Size = new System.Drawing.Size(110, 30);
            this.pbx_notice_n.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbx_notice_n.TabIndex = 12;
            this.pbx_notice_n.TabStop = false;
            this.pbx_notice_n.Visible = false;
            // 
            // pbx_notice_e
            // 
            this.pbx_notice_e.BackColor = System.Drawing.Color.Transparent;
            this.pbx_notice_e.Image = global::Client.Properties.Resources.img_notice_e;
            this.pbx_notice_e.Location = new System.Drawing.Point(150, 5);
            this.pbx_notice_e.Name = "pbx_notice_e";
            this.pbx_notice_e.Size = new System.Drawing.Size(110, 30);
            this.pbx_notice_e.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbx_notice_e.TabIndex = 13;
            this.pbx_notice_e.TabStop = false;
            this.pbx_notice_e.Visible = false;
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_title.Location = new System.Drawing.Point(12, 49);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(51, 12);
            this.label_title.TabIndex = 14;
            this.label_title.Text = "제 목 : ";
            // 
            // label_sender
            // 
            this.label_sender.AutoSize = true;
            this.label_sender.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_sender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_sender.Location = new System.Drawing.Point(267, 66);
            this.label_sender.Name = "label_sender";
            this.label_sender.Size = new System.Drawing.Size(54, 12);
            this.label_sender.TabIndex = 15;
            this.label_sender.Text = "게시자 :";
            // 
            // label_noticetitle
            // 
            this.label_noticetitle.AutoSize = true;
            this.label_noticetitle.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_noticetitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_noticetitle.Location = new System.Drawing.Point(69, 49);
            this.label_noticetitle.Name = "label_noticetitle";
            this.label_noticetitle.Size = new System.Drawing.Size(53, 12);
            this.label_noticetitle.TabIndex = 16;
            this.label_noticetitle.Text = "공지사항";
            // 
            // label_notice_sender
            // 
            this.label_notice_sender.AutoSize = true;
            this.label_notice_sender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_notice_sender.Location = new System.Drawing.Point(327, 66);
            this.label_notice_sender.Name = "label_notice_sender";
            this.label_notice_sender.Size = new System.Drawing.Size(41, 12);
            this.label_notice_sender.TabIndex = 17;
            this.label_notice_sender.Text = "관리자";
            // 
            // btn_confirm
            // 
            this.btn_confirm.Location = new System.Drawing.Point(172, 246);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(75, 23);
            this.btn_confirm.TabIndex = 18;
            this.btn_confirm.Text = "확인";
            this.btn_confirm.UseVisualStyleBackColor = true;
            // 
            // Notice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 275);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.label_notice_sender);
            this.Controls.Add(this.label_noticetitle);
            this.Controls.Add(this.label_sender);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.pbx_notice_e);
            this.Controls.Add(this.pbx_notice_n);
            this.Controls.Add(this.textBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(433, 273);
            this.Name = "Notice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "공지사항";
            this.TopMost = true;
            this.SizeChanged += new System.EventHandler(this.Notice_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_notice_n)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_notice_e)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox;
        public System.Windows.Forms.PictureBox pbx_notice_n;
        public System.Windows.Forms.PictureBox pbx_notice_e;
        public System.Windows.Forms.Label label_title;
        public System.Windows.Forms.Label label_sender;
        public System.Windows.Forms.Label label_noticetitle;
        public System.Windows.Forms.Label label_notice_sender;
        public System.Windows.Forms.Button btn_confirm;
    }
}