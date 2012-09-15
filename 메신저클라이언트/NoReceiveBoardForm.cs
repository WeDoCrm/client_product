using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class NoReceiveBoardForm : Form
    {
        Color label_color;
        Color panel_color;
        public NoReceiveBoardForm()
        {
            InitializeComponent();
            label_color = label_notice.ForeColor;
            panel_color = panel_file.BackColor;
        }

        private void NoReceiveBoardForm_SizeChanged(object sender, EventArgs e)
        {

        }

        private void panel_notice_MouseEnter(object sender, EventArgs e)
        {
            panel_notice.BackColor = Color.Orange;
            label_notice.ForeColor = Color.Black;
        }

        private void panel_notice_MouseLeave(object sender, EventArgs e)
        {
            panel_notice.BackColor = panel_color;
            label_notice.ForeColor = label_color;
        }

        private void panel_memo_MouseEnter(object sender, EventArgs e)
        {
            panel_memo.BackColor = Color.Orange;
            label_memo.ForeColor = Color.Black;
        }

        private void panel_memo_MouseLeave(object sender, EventArgs e)
        {
            panel_memo.BackColor = panel_color;
            label_memo.ForeColor = label_color;
        }

        private void panel_file_MouseEnter(object sender, EventArgs e)
        {
            panel_file.BackColor = Color.Orange;
            label_file.ForeColor = Color.Black;
        }

        private void panel_file_MouseLeave(object sender, EventArgs e)
        {
            panel_file.BackColor = panel_color;
            label_file.ForeColor = label_color;
        }

        private void panel_trans_MouseEnter(object sender, EventArgs e)
        {
            panel_trans.BackColor = Color.Orange;
            label_trans.ForeColor = Color.Black;
        }

        private void panel_trans_MouseLeave(object sender, EventArgs e)
        {
            panel_trans.BackColor = panel_color;
            label_trans.ForeColor = label_color;
        }

        private void panel_notice_MouseClick(object sender, MouseEventArgs e)
        {
            dgv_file.Visible = false;
            dgv_memo.Visible = false;
            dgv_transfer.Visible = false;
            dgv_notice.Visible = true;
        }

        private void panel_memo_MouseClick(object sender, MouseEventArgs e)
        {
            dgv_file.Visible = false;
            dgv_memo.Visible = true;
            dgv_transfer.Visible = false;
            dgv_notice.Visible = false;
        }

        private void panel_file_MouseClick(object sender, MouseEventArgs e)
        {
            dgv_file.Visible = true;
            dgv_memo.Visible = false;
            dgv_transfer.Visible = false;
            dgv_notice.Visible = false;
        }

        private void panel_trans_MouseClick(object sender, MouseEventArgs e)
        {
            dgv_file.Visible = false;
            dgv_memo.Visible = false;
            dgv_transfer.Visible = true;
            dgv_notice.Visible = false;
        }

    }
}
