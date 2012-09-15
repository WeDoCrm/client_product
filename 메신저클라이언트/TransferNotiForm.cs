using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class TransferNotiForm : Form
    {
        Color label_customer_color;
        Color label_from_color;
        Color backcolor;
        public TransferNotiForm()
        {
            InitializeComponent();
            label_customer_color = label_Customer.ForeColor;
            label_from_color = label_from.ForeColor;
            backcolor = this.BackColor;
        }

        private void TransferNotiForm_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Orange;
            label_Customer.ForeColor = Color.Black;
            label_from.ForeColor = Color.Black;
        }

        private void TransferNotiForm_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = backcolor;
            label_Customer.ForeColor = label_customer_color;
            label_from.ForeColor = label_from_color;
        }

        private void pbx_close_MouseEnter(object sender, EventArgs e)
        {
            pbx_close.Image = global::Client.Properties.Resources.btn_closesize_over;
        }

        private void pbx_close_MouseLeave(object sender, EventArgs e)
        {
            pbx_close.Image = global::Client.Properties.Resources.btn_closesize;
        }
    }
}
