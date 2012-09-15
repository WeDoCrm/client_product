namespace Client
{
    partial class SendNoticeForm :System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendNoticeForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.formkey = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbt_nomal = new System.Windows.Forms.RadioButton();
            this.rbt_emer = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_notice_title = new System.Windows.Forms.TextBox();
            this.pbx_notice_e = new System.Windows.Forms.PictureBox();
            this.pbx_notice_n = new System.Windows.Forms.PictureBox();
            this.BtnSend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_notice_e)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_notice_n)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.textBox1.Location = new System.Drawing.Point(12, 120);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(385, 224);
            this.textBox1.TabIndex = 0;
            // 
            // formkey
            // 
            this.formkey.AutoSize = true;
            this.formkey.Location = new System.Drawing.Point(10, 244);
            this.formkey.Name = "formkey";
            this.formkey.Size = new System.Drawing.Size(0, 12);
            this.formkey.TabIndex = 5;
            this.formkey.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(15, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "내 용 :";
            // 
            // rbt_nomal
            // 
            this.rbt_nomal.AutoSize = true;
            this.rbt_nomal.Checked = true;
            this.rbt_nomal.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbt_nomal.ForeColor = System.Drawing.Color.Blue;
            this.rbt_nomal.Location = new System.Drawing.Point(85, 12);
            this.rbt_nomal.Name = "rbt_nomal";
            this.rbt_nomal.Size = new System.Drawing.Size(75, 16);
            this.rbt_nomal.TabIndex = 7;
            this.rbt_nomal.TabStop = true;
            this.rbt_nomal.Text = "일반공지";
            this.rbt_nomal.UseVisualStyleBackColor = true;
            this.rbt_nomal.Click += new System.EventHandler(this.rbt_nomal_Click);
            // 
            // rbt_emer
            // 
            this.rbt_emer.AutoSize = true;
            this.rbt_emer.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbt_emer.ForeColor = System.Drawing.Color.Red;
            this.rbt_emer.Location = new System.Drawing.Point(175, 12);
            this.rbt_emer.Name = "rbt_emer";
            this.rbt_emer.Size = new System.Drawing.Size(75, 16);
            this.rbt_emer.TabIndex = 8;
            this.rbt_emer.Text = "긴급공지";
            this.rbt_emer.UseVisualStyleBackColor = true;
            this.rbt_emer.Click += new System.EventHandler(this.rbt_emer_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(16, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "종 류";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(16, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "제 목 :";
            // 
            // tbx_notice_title
            // 
            this.tbx_notice_title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbx_notice_title.Location = new System.Drawing.Point(69, 71);
            this.tbx_notice_title.Name = "tbx_notice_title";
            this.tbx_notice_title.Size = new System.Drawing.Size(283, 21);
            this.tbx_notice_title.TabIndex = 11;
            // 
            // pbx_notice_e
            // 
            this.pbx_notice_e.Image = global::Client.Properties.Resources.img_notice_e;
            this.pbx_notice_e.Location = new System.Drawing.Point(150, 36);
            this.pbx_notice_e.Name = "pbx_notice_e";
            this.pbx_notice_e.Size = new System.Drawing.Size(110, 30);
            this.pbx_notice_e.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbx_notice_e.TabIndex = 13;
            this.pbx_notice_e.TabStop = false;
            // 
            // pbx_notice_n
            // 
            this.pbx_notice_n.BackColor = System.Drawing.Color.Transparent;
            this.pbx_notice_n.Image = global::Client.Properties.Resources.img_notice_n;
            this.pbx_notice_n.Location = new System.Drawing.Point(150, 36);
            this.pbx_notice_n.Name = "pbx_notice_n";
            this.pbx_notice_n.Size = new System.Drawing.Size(110, 30);
            this.pbx_notice_n.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbx_notice_n.TabIndex = 14;
            this.pbx_notice_n.TabStop = false;
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(166, 352);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(75, 23);
            this.BtnSend.TabIndex = 15;
            this.BtnSend.Text = "보내기";
            this.BtnSend.UseVisualStyleBackColor = true;
            // 
            // SendNoticeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 387);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.pbx_notice_n);
            this.Controls.Add(this.pbx_notice_e);
            this.Controls.Add(this.tbx_notice_title);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbt_emer);
            this.Controls.Add(this.rbt_nomal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.formkey);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(425, 373);
            this.Name = "SendNoticeForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "공지하기";
            this.TopMost = true;
            this.SizeChanged += new System.EventHandler(this.SendNoticeForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_notice_e)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_notice_n)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Label formkey;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.RadioButton rbt_nomal;
        public System.Windows.Forms.RadioButton rbt_emer;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox tbx_notice_title;
        public System.Windows.Forms.PictureBox pbx_notice_e;
        public System.Windows.Forms.PictureBox pbx_notice_n;
        public System.Windows.Forms.Button BtnSend;
    }
}