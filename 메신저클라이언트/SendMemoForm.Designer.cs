
namespace Client
{
    partial class SendMemoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendMemoForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.receiverIDs = new System.Windows.Forms.TextBox();
            this.formkey = new System.Windows.Forms.Label();
            this.txtbox_receiver = new System.Windows.Forms.TextBox();
            this.BtnSend = new System.Windows.Forms.Button();
            this.BtnReceiver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.textBox1.Location = new System.Drawing.Point(1, 27);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(344, 154);
            this.textBox1.TabIndex = 0;
            // 
            // receiverIDs
            // 
            this.receiverIDs.Enabled = false;
            this.receiverIDs.Location = new System.Drawing.Point(243, 24);
            this.receiverIDs.Multiline = true;
            this.receiverIDs.Name = "receiverIDs";
            this.receiverIDs.Size = new System.Drawing.Size(99, 176);
            this.receiverIDs.TabIndex = 4;
            this.receiverIDs.Visible = false;
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
            // txtbox_receiver
            // 
            this.txtbox_receiver.BackColor = System.Drawing.SystemColors.Window;
            this.txtbox_receiver.Location = new System.Drawing.Point(84, 3);
            this.txtbox_receiver.Name = "txtbox_receiver";
            this.txtbox_receiver.ReadOnly = true;
            this.txtbox_receiver.Size = new System.Drawing.Size(260, 21);
            this.txtbox_receiver.TabIndex = 7;
            this.txtbox_receiver.MouseLeave += new System.EventHandler(this.txtbox_receiver_MouseLeave);
            this.txtbox_receiver.MouseEnter += new System.EventHandler(this.txtbox_receiver_MouseEnter);
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(134, 187);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(75, 23);
            this.BtnSend.TabIndex = 14;
            this.BtnSend.Text = "보내기";
            this.BtnSend.UseVisualStyleBackColor = true;
            // 
            // BtnReceiver
            // 
            this.BtnReceiver.Location = new System.Drawing.Point(3, 2);
            this.BtnReceiver.Name = "BtnReceiver";
            this.BtnReceiver.Size = new System.Drawing.Size(75, 23);
            this.BtnReceiver.TabIndex = 15;
            this.BtnReceiver.Text = "받는사람";
            this.BtnReceiver.UseVisualStyleBackColor = true;
            // 
            // SendMemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 218);
            this.Controls.Add(this.BtnReceiver);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.txtbox_receiver);
            this.Controls.Add(this.formkey);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.receiverIDs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(361, 254);
            this.Name = "SendMemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "쪽지 보내기";
            this.TopMost = true;
            this.SizeChanged += new System.EventHandler(this.SendMemoForm_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox receiverIDs;
        public System.Windows.Forms.Label formkey;
        public System.Windows.Forms.TextBox txtbox_receiver;
        public System.Windows.Forms.Button BtnSend;
        public System.Windows.Forms.Button BtnReceiver;
    }
}