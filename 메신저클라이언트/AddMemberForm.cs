using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class AddMemberForm : Form
    {
        public AddMemberForm()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (CurrInListBox.SelectedItems.Count != 0)
            {
                ArrayList list = new ArrayList();
                ListBox.SelectedObjectCollection collection=CurrInListBox.SelectedItems;
                foreach (string member in collection)
                {
                    if (AddListBox.Items.Contains(member))
                    {
                        MessageBox.Show("이미 선택된 사용자 입니다.", "중복선택", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        AddListBox.Items.Add(member);
                        list.Add(member);
                    }
                }
                foreach (object obj in list)
                {
                    string item = obj.ToString();
                    CurrInListBox.Items.Remove(item);
                }
            }
        }

        private void Btnback_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            if (AddListBox.SelectedItems.Count != 0)
            {
                for (int i = 0; i < AddListBox.SelectedItems.Count; i++)
                {
                    list.Add(AddListBox.SelectedItems[i]);
                }
                foreach (object obj in list)
                {
                    string item = obj.ToString();
                    CurrInListBox.Items.Add(item);
                    AddListBox.Items.Remove(item);
                }
            }
        }

        private void BtnCancel_MouseClick(object sender, MouseEventArgs e)
        {
            ArrayList list = new ArrayList();
            if (AddListBox.SelectedItems.Count != 0)
            {
                for (int i = 0; i < AddListBox.SelectedItems.Count; i++)
                {
                    list.Add(AddListBox.SelectedItems[i]);
                }
                foreach (object obj in list)
                {
                    string item = obj.ToString();
                    CurrInListBox.Items.Add(item);
                    AddListBox.Items.Remove(item);
                }
            }
        }
    }
}