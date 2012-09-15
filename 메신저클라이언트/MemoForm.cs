using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Client
{
    public partial class MemoForm : Form
    {
        [DllImport("user32")]
        private static extern int FlashWindow(System.IntPtr hdl, int bInvert);
        public Timer timer = new Timer();

        public MemoForm()
        {
            InitializeComponent();
            timer.Interval = 800;
            timer.Tick+=new EventHandler(timer_Tick);
            this.Enter += new EventHandler(MemoForm_Enter);
            this.MemoRe.Enter += new EventHandler(MemoForm_Enter);
        }

        void MemoForm_Enter(object sender, EventArgs e)
        {
            timer.Stop();
        }


        private void MemoForm_SizeChanged(object sender, EventArgs e)
        {
            int rightgap = this.Width - 357;
            MemoCont.Width = rightgap + 341;
            MemoRe.Width = rightgap + 265;

            int heightgap = this.Height - 193;
            MemoCont.Height = heightgap + 107;

            MemoRe.SetBounds(MemoRe.Left, 108 + heightgap, MemoRe.Width, MemoRe.Height);

            Memobtn.SetBounds(Memobtn.Left, 109 + heightgap, Memobtn.Width, Memobtn.Height);
            Memobtn.SetBounds(271+rightgap, Memobtn.Top, Memobtn.Width, Memobtn.Height);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            flashWindow();
        }

        private int flashWindow()
        {
            return FlashWindow(this.Handle, 1);
        }

        private void MemoCont_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.DefaultDropDownDirection = ToolStripDropDownDirection.BelowRight;
                contextMenuStrip1.Show(e.Location);
            }
        }

        private void 복사CtrlCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(MemoCont.Text);
        }

        private void MemoCont_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.S)
            {
                if (e.Modifiers == Keys.ControlKey)
                {
                    if (MemoCont.SelectedText.Length > 1)
                    {
                        Clipboard.SetText(MemoCont.SelectedText);
                    }
                    else
                    {
                        Clipboard.SetText(MemoCont.Text);
                    }
                }
            }
        }
    }
}