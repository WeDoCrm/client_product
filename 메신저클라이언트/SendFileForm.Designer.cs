namespace Client
{
    partial class SendFileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendFileForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_filename = new System.Windows.Forms.Label();
            this.label_filesize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbox_FileReceiver = new System.Windows.Forms.TextBox();
            this.label_detail = new System.Windows.Forms.LinkLabel();
            this.label_result = new System.Windows.Forms.Label();
            this.formkey = new System.Windows.Forms.Label();
            this.btn_selectfile = new System.Windows.Forms.Button();
            this.btn_receivers = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "전송 파일 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "파일 크기 :";
            // 
            // label_filename
            // 
            this.label_filename.AutoSize = true;
            this.label_filename.Location = new System.Drawing.Point(80, 38);
            this.label_filename.Name = "label_filename";
            this.label_filename.Size = new System.Drawing.Size(0, 12);
            this.label_filename.TabIndex = 6;
            // 
            // label_filesize
            // 
            this.label_filesize.AutoSize = true;
            this.label_filesize.Location = new System.Drawing.Point(80, 60);
            this.label_filesize.Name = "label_filesize";
            this.label_filesize.Size = new System.Drawing.Size(0, 12);
            this.label_filesize.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "전송 결과 :";
            // 
            // txtbox_FileReceiver
            // 
            this.txtbox_FileReceiver.Location = new System.Drawing.Point(81, 5);
            this.txtbox_FileReceiver.Name = "txtbox_FileReceiver";
            this.txtbox_FileReceiver.ReadOnly = true;
            this.txtbox_FileReceiver.Size = new System.Drawing.Size(131, 21);
            this.txtbox_FileReceiver.TabIndex = 9;
            // 
            // label_detail
            // 
            this.label_detail.AutoSize = true;
            this.label_detail.Location = new System.Drawing.Point(292, 82);
            this.label_detail.Name = "label_detail";
            this.label_detail.Size = new System.Drawing.Size(41, 12);
            this.label_detail.TabIndex = 10;
            this.label_detail.TabStop = true;
            this.label_detail.Text = "자세히";
            this.label_detail.Visible = false;
            // 
            // label_result
            // 
            this.label_result.AutoSize = true;
            this.label_result.Location = new System.Drawing.Point(80, 82);
            this.label_result.Name = "label_result";
            this.label_result.Size = new System.Drawing.Size(0, 12);
            this.label_result.TabIndex = 11;
            // 
            // formkey
            // 
            this.formkey.AutoSize = true;
            this.formkey.Location = new System.Drawing.Point(182, 60);
            this.formkey.Name = "formkey";
            this.formkey.Size = new System.Drawing.Size(0, 12);
            this.formkey.TabIndex = 12;
            this.formkey.Visible = false;
            // 
            // btn_selectfile
            // 
            this.btn_selectfile.Location = new System.Drawing.Point(284, 33);
            this.btn_selectfile.Name = "btn_selectfile";
            this.btn_selectfile.Size = new System.Drawing.Size(51, 23);
            this.btn_selectfile.TabIndex = 13;
            this.btn_selectfile.Text = "찾기";
            this.btn_selectfile.UseVisualStyleBackColor = true;
            // 
            // btn_receivers
            // 
            this.btn_receivers.Location = new System.Drawing.Point(4, 3);
            this.btn_receivers.Name = "btn_receivers";
            this.btn_receivers.Size = new System.Drawing.Size(76, 23);
            this.btn_receivers.TabIndex = 17;
            this.btn_receivers.Text = "받는사람";
            this.btn_receivers.UseVisualStyleBackColor = true;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(177, 112);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(76, 23);
            this.btn_start.TabIndex = 18;
            this.btn_start.Text = "파일전송";
            this.btn_start.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(95, 112);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(74, 23);
            this.btn_cancel.TabIndex = 19;
            this.btn_cancel.Text = "취소";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // SendFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 144);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_receivers);
            this.Controls.Add(this.btn_selectfile);
            this.Controls.Add(this.formkey);
            this.Controls.Add(this.label_result);
            this.Controls.Add(this.label_detail);
            this.Controls.Add(this.txtbox_FileReceiver);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_filesize);
            this.Controls.Add(this.label_filename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(361, 182);
            this.MinimumSize = new System.Drawing.Size(361, 182);
            this.Name = "SendFileForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "파일 보내기";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label_filename;
        public System.Windows.Forms.Label label_filesize;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtbox_FileReceiver;
        public System.Windows.Forms.LinkLabel label_detail;
        public System.Windows.Forms.Label label_result;
        public System.Windows.Forms.Label formkey;
        public System.Windows.Forms.Button btn_selectfile;
        public System.Windows.Forms.Button btn_receivers;
        public System.Windows.Forms.Button btn_start;
        public System.Windows.Forms.Button btn_cancel;
    }
}