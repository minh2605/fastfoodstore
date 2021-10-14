using FastFoodStore.BLL;
using FastFoodStore.DAO;
using FastFoodStore.DTO;
using FastFoodStore.MyLinkedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodStore
{
    public partial class fAdmin : Form
    {
        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            LoadAccount();
        }

        
        #region Function
        public void LoadAccount()
        {
            dtgvAccount.DataSource = AccountBLL.Instance.LoadAccountInfo();
            cbType.DataSource = AccountBLL.Instance.GetListAccount2();
            cbType.DisplayMember = "Type";
        }
        public int CheckRepeatUser(string tmp)
        {
                MyList<Account> listCate = AccountBLL.Instance.GetListAccount();
                int i = 0;
                for (Node k = listCate.Head; k != null; k = k.Next)
                {
                    Account items = (Account)k.Data;
                    if (tmp == items.UserName)
                    {
                        i = i + 1;
                    }
                }
                return i;
        }
        public void AddAccount()
        {
            try
            {
                if (txbUser == null || string.IsNullOrWhiteSpace(txbUser.Text) || txbName == null || string.IsNullOrWhiteSpace(txbName.Text) || txbPhone == null || string.IsNullOrWhiteSpace(txbPhone.Text) || txbAddress == null || string.IsNullOrWhiteSpace(txbAddress.Text))
                {
                    MessageBox.Show("Please Fill Full the textbox !");
                }
                else if (CheckRepeatUser(txbUser.Text) > 0)
                {
                    MessageBox.Show("This Username is existed !");
                }
                else
                {
                    UserInfoBLL.Instance.AddUser(txbUser.Text, txbName.Text, Convert.ToInt32(txbPhone.Text), txbAddress.Text);
                    AccountBLL.Instance.AddAccount(txbUser.Text, txbPass.Text, Convert.ToInt32(cbType.Text));
                    MessageBox.Show("Add Successfully!");
                    LoadAccount();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Can not Add !");
            }
        }
        public void ClickToShowInfo()
        {
            int index = dtgvAccount.CurrentCell.RowIndex;
            if (index >= 0)
            {
                txbID.Text = dtgvAccount.Rows[index].Cells[0].Value.ToString();
                txbUser.Text = dtgvAccount.Rows[index].Cells[1].Value.ToString();
                txbName.Text = dtgvAccount.Rows[index].Cells[2].Value.ToString();
                txbPhone.Text = dtgvAccount.Rows[index].Cells[3].Value.ToString();
                txbAddress.Text = dtgvAccount.Rows[index].Cells[4].Value.ToString();
                cbType.Text = dtgvAccount.Rows[index].Cells[5].Value.ToString();
            }
            else return;
        }
        public void ResetPass()
        {
            try
            {
                if(AccountBLL.Instance.UpdateAccount(txbUser.Text,txbPass.Text))
                {
                    MessageBox.Show("Update Successfully!");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Update Fail !");
            }
        }
        public void UpdateAccount()
        {
            try
            {
                if (txbUser == null || string.IsNullOrWhiteSpace(txbUser.Text) || txbName == null || string.IsNullOrWhiteSpace(txbName.Text) || txbPhone == null || string.IsNullOrWhiteSpace(txbPhone.Text) || txbAddress == null || string.IsNullOrWhiteSpace(txbAddress.Text))
                {
                    MessageBox.Show("Please Fill Full the textbox !");
                }
                else
                {
                    if(UserInfoBLL.Instance.UpdateUser(Convert.ToInt32(txbID.Text), txbName.Text, Convert.ToInt32(txbPhone.Text), txbAddress.Text))
                    {
                        MessageBox.Show("Update Successfully!");
                        LoadAccount();
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Can not Update !");
            }
        }
        public void DeleteAccount()
        {
            try
            {
                string username = txbUser.Text;
                if(loginAccount.UserName.Equals(username))
                {
                    MessageBox.Show("Can not Delete your own Account !");
                    return;
                }
                else
                {
                    AccountBLL.Instance.DeleteAccount(username);
                    MessageBox.Show("Delete Successfully !");
                    LoadAccount();
                }
            }
            catch
            {
                MessageBox.Show("Can not Delete !");
            }
        }
        public void SearchAccount()
        {
            dtgvAccount.DataSource = AccountBLL.Instance.SearchAccount(txbSearch.Text);
        }
        #endregion
        #region Events
        private void dtgvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ClickToShowInfo();
        }
        private void btnShowF_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnAddF_Click(object sender, EventArgs e)
        {
            AddAccount();
        }

        private void btnUpdateF_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }

        private void btnDeleteF_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want to Delete?", "Delete!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                DeleteAccount();
            }
            else return;
        }

        private void btnSearchF_Click(object sender, EventArgs e)
        {
            SearchAccount();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Password will be reset into 1 ?", "Notice!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                ResetPass();
            }
        }
        #endregion


    }
}
