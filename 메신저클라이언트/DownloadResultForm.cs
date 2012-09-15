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
    public partial class DownloadResultForm : Form
    {
        [DllImport("user32")]
        private static extern int FlashWindow(System.IntPtr hdl, int bInvert);
        public Timer timer = new Timer();


        public DownloadResultForm()
        {
            InitializeComponent();
            timer.Interval = 800;
            timer.Tick += new EventHandler(timer_Tick);
            this.Activated += new EventHandler(Form_Activated);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            flashWindow();
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private int flashWindow()
        {
            return FlashWindow(this.Handle, 1);
        }
    }
}
