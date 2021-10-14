using FastFoodStore.BLL;
using FastFoodStore.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodStore
{
    public partial class fProfile : Form
    {
        private Account logined;
        private UserInfo info;
        public Account Logined
        {
            get { return logined; }
            set { logined = value; checkAccount(logined,info); }
        }
        public UserInfo Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;

            }
        }


        public fProfile(Account acc, UserInfo user)
        {
            InitializeComponent();
            this.info = user;
            this.Logined = acc;
            
        }
        #region Function
        void checkAccount(Account acc,UserInfo user)
        {
            txbName.Text = user.Name;
            txbUsername.Text = acc.UserName;
            txbPhone.Text = user.Phone.ToString();
            txbAddress.Text = user.Address;
        }
        void UpdateAccount()
        {
            try
            {
                string name = txbName.Text;
                string username = txbUsername.Text;
                int phone = Convert.ToInt32(txbPhone.Text);
                string address = txbAddress.Text;
                string pass = txbPassword.Text;
                string newPass = txbNewPass.Text;
                string verifyPass = txbVerify.Text;
                if (!newPass.Equals(verifyPass))
                {
                    MessageBox.Show("New Password do not match !");
                }
                else
                {
                    if (AccountBLL.Instance.UpdateAccount(username, name, phone, address, pass, newPass))
                    {
                        MessageBox.Show("Update Succesfully !");
                    }
                    else
                    {
                        MessageBox.Show("Please check password !");
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Please Full Fill the textbox!");
            }
            
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }
    }
}
