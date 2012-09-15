using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class MemoListForm : Form
    {
        public MemoListForm()
        {
            InitializeComponent();
        }

        private void MemoListForm_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                int widthgap = this.Width - listView.Width;
                listView.Width += widthgap;
                int heightgap = this.Height - (listView.Height + 30);
                listView.Height += heightgap;
                btn_all.SetBounds(btn_all.Left, (btn_all.Top + heightgap), btn_all.Width, btn_all.Height);
                btn_cancel.SetBounds(btn_cancel.Left, (btn_cancel.Top + heightgap), btn_cancel.Width, btn_cancel.Height);
                btn_del.SetBounds((btn_del.Left + widthgap), (btn_del.Top + heightgap), btn_del.Width, btn_del.Height);
            }
            catch (Exception ex)
            {

            }

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

        private void btn_cancel_MouseClick(object sender, MouseEventArgs e)
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

    }
}
