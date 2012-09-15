using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class PopForm : Form
    {
        private int screenWidth = 0;
        private int screenHeight = 0;
        private bool isActivation = false;

        public PopForm()
        {
            InitializeComponent();
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        public override bool Focused
        {
            get
            {
                return false;
            }
        }

         


        private void PopForm_Load(object sender, EventArgs e)
        {
            screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.SetBounds(screenWidth - this.Width, screenHeight - this.Height, this.Width, this.Height);
        }

    }
}
