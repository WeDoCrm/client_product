namespace Client
{
    partial class ChatForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chatSendFile = new System.Windows.Forms.Button();
            this.BtnAddChatter = new System.Windows.Forms.Button();
            this.chatStatBar = new System.Windows.Forms.StatusStrip();
            this.ReBox = new System.Windows.Forms.TextBox();
            this.ChattersTree = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.Formkey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbox_exam = new System.Windows.Forms.TextBox();
            this.label_font = new System.Windows.Forms.Label();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.PictureBox();
            this.Mmenu_copy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MmenuItem_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).BeginInit();
            this.Mmenu_copy.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.chatSendFile);
            this.panel1.Controls.Add(this.BtnAddChatter);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 36);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(93, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // chatSendFile
            // 
            this.chatSendFile.Location = new System.Drawing.Point(195, 6);
            this.chatSendFile.Name = "chatSendFile";
            this.chatSendFile.Size = new System.Drawing.Size(75, 25);
            this.chatSendFile.TabIndex = 5;
            this.chatSendFile.Text = "파일전송";
            this.chatSendFile.UseVisualStyleBackColor = true;
            // 
            // BtnAddChatter
            // 
            this.BtnAddChatter.Location = new System.Drawing.Point(114, 6);
            this.BtnAddChatter.Name = "BtnAddChatter";
            this.BtnAddChatter.Size = new System.Drawing.Size(75, 25);
            this.BtnAddChatter.TabIndex = 4;
            this.BtnAddChatter.Text = "초대하기";
            this.BtnAddChatter.UseVisualStyleBackColor = true;
            // 
            // chatStatBar
            // 
            this.chatStatBar.Location = new System.Drawing.Point(0, 475);
            this.chatStatBar.Name = "chatStatBar";
            this.chatStatBar.Size = new System.Drawing.Size(282, 22);
            this.chatStatBar.TabIndex = 3;
            this.chatStatBar.Text = "상태바";
            // 
            // ReBox
            // 
            this.ReBox.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.ReBox.Location = new System.Drawing.Point(2, 413);
            this.ReBox.Multiline = true;
            this.ReBox.Name = "ReBox";
            this.ReBox.Size = new System.Drawing.Size(214, 61);
            this.ReBox.TabIndex = 4;
            this.ReBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReBox_KeyDown);
            this.ReBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ReBox_MouseClick);
            this.ReBox.MouseEnter += new System.EventHandler(this.ReBox_MouseEnter);
            // 
            // ChattersTree
            // 
            this.ChattersTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(220)))), ((int)(((byte)(237)))));
            this.ChattersTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChattersTree.ImageIndex = 0;
            this.ChattersTree.ImageList = this.imageList;
            this.ChattersTree.Location = new System.Drawing.Point(1, 38);
            this.ChattersTree.Name = "ChattersTree";
            this.ChattersTree.SelectedImageIndex = 0;
            this.ChattersTree.ShowLines = false;
            this.ChattersTree.ShowPlusMinus = false;
            this.ChattersTree.ShowRootLines = false;
            this.ChattersTree.Size = new System.Drawing.Size(295, 44);
            this.ChattersTree.StateImageList = this.imageList;
            this.ChattersTree.TabIndex = 7;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "온라인.png");
            // 
            // Formkey
            // 
            this.Formkey.Enabled = false;
            this.Formkey.Location = new System.Drawing.Point(175, 1);
            this.Formkey.Name = "Formkey";
            this.Formkey.Size = new System.Drawing.Size(104, 21);
            this.Formkey.TabIndex = 8;
            this.Formkey.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(136, 388);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "미리보기";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtbox_exam
            // 
            this.txtbox_exam.BackColor = System.Drawing.SystemColors.Window;
            this.txtbox_exam.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtbox_exam.Location = new System.Drawing.Point(193, 384);
            this.txtbox_exam.Name = "txtbox_exam";
            this.txtbox_exam.ReadOnly = true;
            this.txtbox_exam.Size = new System.Drawing.Size(85, 23);
            this.txtbox_exam.TabIndex = 11;
            this.txtbox_exam.Text = "가나ABab";
            // 
            // label_font
            // 
            this.label_font.AutoSize = true;
            this.label_font.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_font.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label_font.Location = new System.Drawing.Point(12, 384);
            this.label_font.Name = "label_font";
            this.label_font.Size = new System.Drawing.Size(60, 17);
            this.label_font.TabIndex = 12;
            this.label_font.Text = "글꼴설정";
            this.label_font.MouseLeave += new System.EventHandler(this.label_font_MouseLeave);
            this.label_font.Click += new System.EventHandler(this.label_font_Click);
            this.label_font.MouseEnter += new System.EventHandler(this.label_font_MouseEnter);
            // 
            // chatBox
            // 
            this.chatBox.BackColor = System.Drawing.SystemColors.Window;
            this.chatBox.Location = new System.Drawing.Point(1, 83);
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            this.chatBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.chatBox.Size = new System.Drawing.Size(295, 289);
            this.chatBox.TabIndex = 9;
            this.chatBox.Text = "";
            this.chatBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatBox_KeyDown_1);
            this.chatBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chatBox_MouseClick);
            // 
            // btnSend
            // 
            this.btnSend.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.Image")));
            this.btnSend.Location = new System.Drawing.Point(221, 413);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(61, 60);
            this.btnSend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSend.TabIndex = 4;
            this.btnSend.TabStop = false;
            // 
            // Mmenu_copy
            // 
            this.Mmenu_copy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MmenuItem_copy});
            this.Mmenu_copy.Name = "Mmenu_copy";
            this.Mmenu_copy.Size = new System.Drawing.Size(174, 26);
            // 
            // MmenuItem_copy
            // 
            this.MmenuItem_copy.Name = "MmenuItem_copy";
            this.MmenuItem_copy.Size = new System.Drawing.Size(173, 22);
            this.MmenuItem_copy.Text = "복사하기(Crtl + C)";
            this.MmenuItem_copy.Click += new System.EventHandler(this.MmenuItem_copy_Click);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(282, 497);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.label_font);
            this.Controls.Add(this.txtbox_exam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.ChattersTree);
            this.Controls.Add(this.ReBox);
            this.Controls.Add(this.chatStatBar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Formkey);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(298, 535);
            this.MinimumSize = new System.Drawing.Size(298, 535);
            this.Name = "ChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChatForm_MouseClick);
            this.MouseEnter += new System.EventHandler(this.ChatForm_MouseEnter);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).EndInit();
            this.Mmenu_copy.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.StatusStrip chatStatBar;
        public System.Windows.Forms.TextBox ReBox;
        public System.Windows.Forms.TreeView ChattersTree;
        public System.Windows.Forms.TextBox Formkey;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtbox_exam;
        public System.Windows.Forms.Label label_font;
        public System.Windows.Forms.ImageList imageList;
        public System.Windows.Forms.RichTextBox chatBox;
        public System.Windows.Forms.PictureBox btnSend;
        public System.Windows.Forms.ContextMenuStrip Mmenu_copy;
        public System.Windows.Forms.ToolStripMenuItem MmenuItem_copy;
        public System.Windows.Forms.Button chatSendFile;
        public System.Windows.Forms.Button BtnAddChatter;
        public System.Windows.Forms.PictureBox pictureBox1;
    }
}