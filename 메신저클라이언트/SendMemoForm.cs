using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class SendMemoForm: Form
    {
        ToolTip tip = new ToolTip();
        public SendMemoForm()
        {
            InitializeComponent();
            tip = new ToolTip();
            tip.ToolTipIcon = ToolTipIcon.Info;
            tip.ToolTipTitle = "< 받는사람 >";
        }

        private void SendMemoForm_SizeChanged(object sender, EventArgs e)
        {
            int rightgap = this.Width - 361;

            txtbox_receiver.Width = rightgap + 276;
            textBox1.Width = rightgap + 344;

            int heightgap = this.Height - 254;
            textBox1.Height = heightgap + 154;

            BtnSend.SetBounds(BtnSend.Left, 186 + heightgap, BtnSend.Width, BtnSend.Height);
            BtnSend.SetBounds(136 + (rightgap/2), BtnSend.Top, BtnSend.Width, BtnSend.Height);
        }

        private void txtbox_receiver_MouseEnter(object sender, EventArgs e)
        {
            tip.Show(txtbox_receiver.Text, txtbox_receiver, txtbox_receiver.Location);
        }

        private void txtbox_receiver_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(txtbox_receiver);
        }
    }
}