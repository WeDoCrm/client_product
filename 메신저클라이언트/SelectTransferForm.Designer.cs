namespace Client
{
    partial class SelectTransferForm
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
            this.cbx_receiver = new System.Windows.Forms.ComboBox();
            this.pbx_trans_confirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbx_receiver
            // 
            this.cbx_receiver.FormattingEnabled = true;
            this.cbx_receiver.Location = new System.Drawing.Point(12, 18);
            this.cbx_receiver.Name = "cbx_receiver";
            this.cbx_receiver.Size = new System.Drawing.Size(193, 20);
            this.cbx_receiver.TabIndex = 0;
            // 
            // pbx_trans_confirm
            // 
            this.pbx_trans_confirm.Location = new System.Drawing.Point(220, 16);
            this.pbx_trans_confirm.Name = "pbx_trans_confirm";
            this.pbx_trans_confirm.Size = new System.Drawing.Size(60, 23);
            this.pbx_trans_confirm.TabIndex = 2;
            this.pbx_trans_confirm.Text = "확인";
            this.pbx_trans_confirm.UseVisualStyleBackColor = true;
            // 
            // SelectTransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 58);
            this.Controls.Add(this.pbx_trans_confirm);
            this.Controls.Add(this.cbx_receiver);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectTransferForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "전달 대상자";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cbx_receiver;
        public System.Windows.Forms.Button pbx_trans_confirm;
    }
}