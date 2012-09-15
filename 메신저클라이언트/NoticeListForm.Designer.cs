namespace Client
{
    partial class NoticeListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoticeListForm));
            this.listView = new System.Windows.Forms.ListView();
            this.mode = new System.Windows.Forms.ColumnHeader();
            this.title = new System.Windows.Forms.ColumnHeader();
            this.content = new System.Windows.Forms.ColumnHeader();
            this.sender = new System.Windows.Forms.ColumnHeader();
            this.time = new System.Windows.Forms.ColumnHeader();
            this.btn_del = new System.Windows.Forms.Button();
            this.btn_all = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView.AllowColumnReorder = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.mode,
            this.title,
            this.content,
            this.sender,
            this.time});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.listView.Location = new System.Drawing.Point(1, 2);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(650, 286);
            this.listView.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            // 
            // mode
            // 
            this.mode.Text = "종 류";
            this.mode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mode.Width = 51;
            // 
            // title
            // 
            this.title.Text = "제   목";
            this.title.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.title.Width = 120;
            // 
            // content
            // 
            this.content.Text = "       내  용";
            this.content.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.content.Width = 271;
            // 
            // sender
            // 
            this.sender.Text = "보낸 사람";
            this.sender.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sender.Width = 82;
            // 
            // time
            // 
            this.time.Text = "보낸 시각";
            this.time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.time.Width = 119;
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(565, 296);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(75, 23);
            this.btn_del.TabIndex = 9;
            this.btn_del.Text = "삭제";
            this.btn_del.UseVisualStyleBackColor = true;
            // 
            // btn_all
            // 
            this.btn_all.Location = new System.Drawing.Point(6, 296);
            this.btn_all.Name = "btn_all";
            this.btn_all.Size = new System.Drawing.Size(75, 23);
            this.btn_all.TabIndex = 10;
            this.btn_all.Text = "전체선택";
            this.btn_all.UseVisualStyleBackColor = true;
            this.btn_all.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_all_MouseClick);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(87, 296);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 11;
            this.cancel.Text = "선택취소";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cancel_MouseClick);
            // 
            // NoticeListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 325);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.btn_all);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.listView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(556, 355);
            this.Name = "NoticeListForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "공지사항 목록 ( 검색 : Ctrl+F )";
            this.TopMost = true;
            this.SizeChanged += new System.EventHandler(this.NoticeListForm_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView listView;
        public System.Windows.Forms.ColumnHeader sender;
        public System.Windows.Forms.ColumnHeader time;
        public System.Windows.Forms.ColumnHeader content;
        public System.Windows.Forms.ColumnHeader mode;
        public System.Windows.Forms.ColumnHeader title;
        public System.Windows.Forms.Button btn_del;
        public System.Windows.Forms.Button btn_all;
        public System.Windows.Forms.Button cancel;

    }
}