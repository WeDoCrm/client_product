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
    public partial class Notice : Form
    {
        [DllImport("user32")]
        private static extern int FlashWindow(System.IntPtr hdl, int bInvert);
        public Timer timer = new Timer();

        public Notice()
        {
            InitializeComponent();
        }

        private void Notice_SizeChanged(object sender, EventArgs e)
        {
            int rightgap = this.Width - 433;

            textBox.Width = rightgap + 414;
            

            int heightgap = this.Height - 273;
            textBox.Height = heightgap + 153;

            label_noticetitle.SetBounds(97 + (rightgap / 2), label_noticetitle.Top, label_noticetitle.Width, label_noticetitle.Height);
            btn_confirm.SetBounds(168 + (rightgap / 2), btn_confirm.Top, btn_confirm.Width, btn_confirm.Height);
        }

        public void flash()
        {
            flashWindow();
        }

        private int flashWindow()
        {
            return FlashWindow(this.Handle, 1);
        }
        
    }
}