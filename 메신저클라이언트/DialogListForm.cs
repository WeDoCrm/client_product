using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class DialogListForm : Form
    {
        public DialogListForm()
        {
            InitializeComponent();
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


        private void listView_ItemCheck(object sender, ItemCheckEventArgs e)
        {

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
            ListView.CheckedListViewItemCollection col = listView.CheckedItems;
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
