using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class NoticeDetailResultForm : Form
    {
        public NoticeDetailResultForm()
        {
            InitializeComponent();
        }

        private void NoticeDetailResultForm_SizeChanged(object sender, EventArgs e)
        {
            int widthgap = this.Width - listView1.Width;
            listView1.Width += widthgap;
            int heightgap = this.Height - (listView1.Height + 30);
            listView1.Height += heightgap;
        }
        private bool isAs = false;

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (isAs == false)
            {
                this.listView1.ListViewItemSorter = new ListViewItemComparerAs(e.Column);
                isAs = true;
            }
            else
            {
                this.listView1.ListViewItemSorter = new ListViewItemComparerDe(e.Column);
                isAs = false;
            }
        }
    }

 
}
