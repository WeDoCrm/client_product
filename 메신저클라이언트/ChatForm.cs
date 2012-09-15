using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using System.Runtime.InteropServices;

namespace Client
{
    public partial class ChatForm : Form
    {
        [DllImport("user32")]
        private static extern int FlashWindow(System.IntPtr hdl, int bInvert);
        private Timer timer = new Timer();

        public ChatForm()
        {
            try
            {
                InitializeComponent();
                timer.Interval = 800;
                timer.Tick += new EventHandler(timer_Tick);
                this.Shown += new EventHandler(ChatForm_Shown);
                this.Activated += new EventHandler(Form_Activated);
                this.chatBox.TextChanged += new EventHandler(chatBox_TextChanged);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected override bool ShowWithoutActivation
        {

            get
            {
                return true;
            }
        }

        private void ChatForm_Shown(object sender, EventArgs e)
        {
            this.ReBox.Focus();
        }

        private void chatBox_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (this.Focused == false && this.ReBox.Focused == false && this.btnSend.Focused == false && chatBox.Focused == false)
                {
                    timer.Start();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            try
            {
                timer.Start();
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        private void label_font_Click(object sender, EventArgs e)
        {
            try
            {
                FontDialog dialog = new FontDialog();
                dialog.ShowColor = true;
                DialogResult result = dialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    txtbox_exam.Font = dialog.Font;
                    txtbox_exam.ForeColor = dialog.Color;
                    txtbox_exam.Refresh();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void label_font_MouseEnter(object sender, EventArgs e)
        {
            label_font.ForeColor = Color.DarkViolet;
        }

        private void label_font_MouseLeave(object sender, EventArgs e)
        {
            label_font.ForeColor = Color.Black;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                flashWindow();
            }
            catch (Exception)
            {
                
            }
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            try
            {
                timer.Stop();
                this.ReBox.Focus();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int flashWindow()
        {
            try
            {
                return FlashWindow(this.Handle, 1);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chatBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Mmenu_copy.DefaultDropDownDirection = ToolStripDropDownDirection.BelowRight;
                Mmenu_copy.Show(e.Location);
            }
        }

        private void chatBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.S)
            {
                if (e.Modifiers == Keys.ControlKey)
                {
                    if (chatBox.SelectedText.Length > 1)
                    {
                        Clipboard.SetText(chatBox.SelectedText);
                    }
                    else
                    {
                        Clipboard.SetText(chatBox.Text);
                    }
                }
            }
        }

        private void MmenuItem_copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(chatBox.Text);
        }

        private void ChatForm_MouseEnter(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void ChatForm_MouseClick(object sender, MouseEventArgs e)
        {
            timer.Stop();
        }

        private void ReBox_KeyDown(object sender, KeyEventArgs e)
        {
            timer.Stop();
        }

        private void ReBox_MouseClick(object sender, MouseEventArgs e)
        {
            timer.Stop();
        }

        private void ReBox_MouseEnter(object sender, EventArgs e)
        {
            timer.Stop();
        }
        
    }
}