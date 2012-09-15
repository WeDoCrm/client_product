namespace Client
{
    partial class MemoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemoForm));
            this.MemoCont = new System.Windows.Forms.TextBox();
            this.MemoRe = new System.Windows.Forms.TextBox();
            this.senderid = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.복사CtrlCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Memobtn = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MemoCont
            // 
            this.MemoCont.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.MemoCont.Location = new System.Drawing.Point(0, 0);
            this.MemoCont.Margin = new System.Windows.Forms.Padding(10);
            this.MemoCont.Multiline = true;
            this.MemoCont.Name = "MemoCont";
            this.MemoCont.ReadOnly = true;
            this.MemoCont.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.MemoCont.Size = new System.Drawing.Size(333, 107);
            this.MemoCont.TabIndex = 1;
            this.MemoCont.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MemoCont_KeyDown);
            this.MemoCont.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MemoCont_MouseClick);
            // 
            // MemoRe
            // 
            this.MemoRe.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.MemoRe.Location = new System.Drawing.Point(2, 108);
            this.MemoRe.Multiline = true;
            this.MemoRe.Name = "MemoRe";
            this.MemoRe.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MemoRe.Size = new System.Drawing.Size(265, 64);
            this.MemoRe.TabIndex = 0;
            // 
            // senderid
            // 
            this.senderid.AutoSize = true;
            this.senderid.Enabled = false;
            this.senderid.Location = new System.Drawing.Point(300, 3);
            this.senderid.Name = "senderid";
            this.senderid.Size = new System.Drawing.Size(0, 12);
            this.senderid.TabIndex = 4;
            this.senderid.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.복사CtrlCToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 26);
            // 
            // 복사CtrlCToolStripMenuItem
            // 
            this.복사CtrlCToolStripMenuItem.Name = "복사CtrlCToolStripMenuItem";
            this.복사CtrlCToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.복사CtrlCToolStripMenuItem.Text = "복사 (Ctrl + C)";
            this.복사CtrlCToolStripMenuItem.Click += new System.EventHandler(this.복사CtrlCToolStripMenuItem_Click);
            // 
            // Memobtn
            // 
            this.Memobtn.Location = new System.Drawing.Point(271, 112);
            this.Memobtn.Name = "Memobtn";
            this.Memobtn.Size = new System.Drawing.Size(56, 54);
            this.Memobtn.TabIndex = 6;
            this.Memobtn.Text = "보내기";
            this.Memobtn.UseVisualStyleBackColor = true;
            // 
            // MemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 174);
            this.Controls.Add(this.Memobtn);
            this.Controls.Add(this.senderid);
            this.Controls.Add(this.MemoRe);
            this.Controls.Add(this.MemoCont);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(350, 193);
            this.Name = "MemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.SizeChanged += new System.EventHandler(this.MemoForm_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox MemoCont;
        public System.Windows.Forms.TextBox MemoRe;
        public System.Windows.Forms.Label senderid;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 복사CtrlCToolStripMenuItem;
        public System.Windows.Forms.Button Memobtn;
    }
}