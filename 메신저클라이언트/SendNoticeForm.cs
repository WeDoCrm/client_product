using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class SendNoticeForm : Form
    {
        public SendNoticeForm()
        {
            InitializeComponent();
        }

        private void SendNoticeForm_SizeChanged(object sender, EventArgs e)
        {

            int rightgap = this.Width - 425;
            textBox1.Width = rightgap + 385;

            int heightgap = this.Height - 425;
            textBox1.Height = heightgap + 224;

            tbx_notice_title.Width = rightgap + 283;

            BtnSend.SetBounds(BtnSend.Left, 354 + heightgap, BtnSend.Width, BtnSend.Height);
            BtnSend.SetBounds(173 + (rightgap/2), BtnSend.Top, BtnSend.Width, BtnSend.Height);
        }

        private void rbt_nomal_Click(object sender, EventArgs e)
        {
            pbx_notice_e.Visible = false;
            pbx_notice_n.Visible = true;
        }

        private void rbt_emer_Click(object sender, EventArgs e)
        {
            pbx_notice_e.Visible = true;
            pbx_notice_n.Visible = false;
        }
    }
}