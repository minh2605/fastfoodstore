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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUsername.Text;
            string passWord = txbPassword.Text;
            if (Login(userName, passWord))
            {
                Account logined = AccountBLL.Instance.GetAccByUsername(userName);
                UserInfo user = UserInfoBLL.Instance.GetUserInfoByUserName(userName);
                fHome f = new fHome(logined,user);
                //fHome f = new fHome();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else MessageBox.Show("Username or Password is not correct !!");
        }
        bool Login(string userName, string passWord)
        {
            return AccountBLL.Instance.Login(userName, passWord);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Do you want to Exit?","NOTICE!!", MessageBoxButtons.OKCancel)!= System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
