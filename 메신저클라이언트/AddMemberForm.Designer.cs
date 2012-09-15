namespace Client
{
    partial class AddMemberForm
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
            this.CurrInListBox = new System.Windows.Forms.ListBox();
            this.AddListBox = new System.Windows.Forms.ListBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.Btnback = new System.Windows.Forms.Button();
            this.label_currInList = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radiobt_con = new System.Windows.Forms.RadioButton();
            this.radiobt_all = new System.Windows.Forms.RadioButton();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.radiobt_g = new System.Windows.Forms.RadioButton();
            this.combobox_team = new System.Windows.Forms.ComboBox();
            this.formkey = new System.Windows.Forms.Label();
            this.label_choice = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnConfirm = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CurrInListBox
            // 
            this.CurrInListBox.FormattingEnabled = true;
            this.CurrInListBox.ItemHeight = 12;
            this.CurrInListBox.Location = new System.Drawing.Point(12, 95);
            this.CurrInListBox.Name = "CurrInListBox";
            this.CurrInListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.CurrInListBox.Size = new System.Drawing.Size(104, 208);
            this.CurrInListBox.TabIndex = 0;
            // 
            // AddListBox
            // 
            this.AddListBox.FormattingEnabled = true;
            this.AddListBox.ItemHeight = 12;
            this.AddListBox.Location = new System.Drawing.Point(158, 95);
            this.AddListBox.Name = "AddListBox";
            this.AddListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.AddListBox.Size = new System.Drawing.Size(109, 208);
            this.AddListBox.TabIndex = 1;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnAdd.Location = new System.Drawing.Point(122, 154);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(29, 29);
            this.BtnAdd.TabIndex = 2;
            this.BtnAdd.Text = ">>";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // Btnback
            // 
            this.Btnback.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Btnback.Location = new System.Drawing.Point(122, 201);
            this.Btnback.Name = "Btnback";
            this.Btnback.Size = new System.Drawing.Size(29, 29);
            this.Btnback.TabIndex = 3;
            this.Btnback.Text = "<<";
            this.Btnback.UseVisualStyleBackColor = true;
            this.Btnback.Click += new System.EventHandler(this.Btnback_Click);
            // 
            // label_currInList
            // 
            this.label_currInList.AutoSize = true;
            this.label_currInList.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_currInList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label_currInList.Location = new System.Drawing.Point(28, 75);
            this.label_currInList.Name = "label_currInList";
            this.label_currInList.Size = new System.Drawing.Size(88, 12);
            this.label_currInList.TabIndex = 5;
            this.label_currInList.Text = "사용자 리스트";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(173, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "선택한 사용자";
            // 
            // radiobt_con
            // 
            this.radiobt_con.AutoSize = true;
            this.radiobt_con.Location = new System.Drawing.Point(163, 21);
            this.radiobt_con.Name = "radiobt_con";
            this.radiobt_con.Size = new System.Drawing.Size(87, 16);
            this.radiobt_con.TabIndex = 8;
            this.radiobt_con.Text = "현재 접속자";
            this.radiobt_con.UseVisualStyleBackColor = true;
            // 
            // radiobt_all
            // 
            this.radiobt_all.AutoSize = true;
            this.radiobt_all.Checked = true;
            this.radiobt_all.Location = new System.Drawing.Point(14, 21);
            this.radiobt_all.Name = "radiobt_all";
            this.radiobt_all.Size = new System.Drawing.Size(87, 16);
            this.radiobt_all.TabIndex = 9;
            this.radiobt_all.TabStop = true;
            this.radiobt_all.Text = "사용자 전체";
            this.radiobt_all.UseVisualStyleBackColor = true;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.radiobt_g);
            this.groupBox.Controls.Add(this.radiobt_all);
            this.groupBox.Controls.Add(this.radiobt_con);
            this.groupBox.Location = new System.Drawing.Point(7, 21);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(265, 46);
            this.groupBox.TabIndex = 10;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "리스트 보기";
            // 
            // radiobt_g
            // 
            this.radiobt_g.AutoSize = true;
            this.radiobt_g.Location = new System.Drawing.Point(107, 21);
            this.radiobt_g.Name = "radiobt_g";
            this.radiobt_g.Size = new System.Drawing.Size(47, 16);
            this.radiobt_g.TabIndex = 10;
            this.radiobt_g.Text = "팀별";
            this.radiobt_g.UseVisualStyleBackColor = true;
            // 
            // combobox_team
            // 
            this.combobox_team.FormattingEnabled = true;
            this.combobox_team.Location = new System.Drawing.Point(155, 4);
            this.combobox_team.Name = "combobox_team";
            this.combobox_team.Size = new System.Drawing.Size(109, 20);
            this.combobox_team.TabIndex = 11;
            this.combobox_team.Visible = false;
            // 
            // formkey
            // 
            this.formkey.AutoSize = true;
            this.formkey.Location = new System.Drawing.Point(15, 311);
            this.formkey.Name = "formkey";
            this.formkey.Size = new System.Drawing.Size(0, 12);
            this.formkey.TabIndex = 11;
            this.formkey.Visible = false;
            // 
            // label_choice
            // 
            this.label_choice.AutoSize = true;
            this.label_choice.Location = new System.Drawing.Point(103, 8);
            this.label_choice.Name = "label_choice";
            this.label_choice.Size = new System.Drawing.Size(45, 12);
            this.label_choice.TabIndex = 12;
            this.label_choice.Text = "팀 선택";
            this.label_choice.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(155, 311);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(55, 24);
            this.BtnCancel.TabIndex = 15;
            this.BtnCancel.Text = "취소";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BtnCancel_MouseClick);
            // 
            // BtnConfirm
            // 
            this.BtnConfirm.Location = new System.Drawing.Point(213, 311);
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.Size = new System.Drawing.Size(55, 24);
            this.BtnConfirm.TabIndex = 16;
            this.BtnConfirm.Text = "확인";
            this.BtnConfirm.UseVisualStyleBackColor = true;
            // 
            // AddMemberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 346);
            this.Controls.Add(this.BtnConfirm);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.label_choice);
            this.Controls.Add(this.combobox_team);
            this.Controls.Add(this.formkey);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_currInList);
            this.Controls.Add(this.Btnback);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.AddListBox);
            this.Controls.Add(this.CurrInListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(284, 374);
            this.MinimumSize = new System.Drawing.Size(284, 374);
            this.Name = "AddMemberForm";
            this.Text = "사용자 추가";
            this.TopMost = true;
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox CurrInListBox;
        public System.Windows.Forms.ListBox AddListBox;
        public System.Windows.Forms.Button BtnAdd;
        public System.Windows.Forms.Button Btnback;
        public System.Windows.Forms.Label label_currInList;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.RadioButton radiobt_con;
        public System.Windows.Forms.RadioButton radiobt_all;
        public System.Windows.Forms.GroupBox groupBox;
        public System.Windows.Forms.Label formkey;
        public System.Windows.Forms.RadioButton radiobt_g;
        public System.Windows.Forms.ComboBox combobox_team;
        public System.Windows.Forms.Label label_choice;
        public System.Windows.Forms.Button BtnCancel;
        public System.Windows.Forms.Button BtnConfirm;
    }
}