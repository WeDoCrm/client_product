using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class NoticeListForm : Form
    {
        
        public NoticeListForm()
        {
            InitializeComponent();
        }

        private void NoticeListForm_SizeChanged(object sender, EventArgs e)
        {
            int widthgap = this.Width - 668;
            listView.Width = (650 + widthgap);
            listView.Columns[2].Width = (271 + widthgap);
            int heightgap = this.Height - 363;
            listView.Height = (286 + heightgap);

            this.cancel.SetBounds(this.cancel.Left, 294 + heightgap, this.cancel.Width, this.cancel.Height);
            

            this.btn_all.SetBounds(this.btn_all.Left, 294 + heightgap, this.btn_all.Width, this.btn_all.Height);
            

            this.btn_del.SetBounds(this.btn_del.Left, 294 + heightgap, this.btn_del.Width, this.btn_del.Height);
            this.btn_del.SetBounds(585 + widthgap, this.btn_del.Top, this.btn_del.Width, this.btn_del.Height);
        }

        private bool isAs = false;

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (isAs == false)
            {
                this.listView.ListViewItemSorter = new ListViewItemComparerAs(e.Column);
                isAs = true;
            }
            else
            {
                this.listView.ListViewItemSorter = new ListViewItemComparerDe(e.Column);
                isAs = false;
            }

        }

        private void cancel_MouseClick(object sender, MouseEventArgs e)
        {
            ListView.CheckedListViewItemCollection col = this.listView.CheckedItems;
            if (col.Count != 0)
            {
                foreach (ListViewItem item in col)
                {
                    item.Checked = false;
                }
            }
        }

        private void btn_all_MouseClick(object sender, MouseEventArgs e)
        {
            ListView.ListViewItemCollection col = listView.Items;
            if (col.Count != 0)
            {
                foreach (ListViewItem item in col)
                {
                    item.Checked = true;
                }
            }
        }

    }
}
