using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class DialogContent : Form
    {
        public DialogContent()
        {
            InitializeComponent();
        }

        private void textBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.DefaultDropDownDirection = ToolStripDropDownDirection.BelowRight;
                contextMenuStrip1.Show(e.Location);
            }
        }

        private void 복사CtrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox.Text);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.S)
            {
                if (e.Modifiers == Keys.ControlKey)
                {
                    if (textBox.SelectedText.Length > 1)
                    {
                        Clipboard.SetText(textBox.SelectedText);
                    }
                    else
                    {
                        Clipboard.SetText(textBox.Text);
                    }
                }
            }
        }
    }
}
