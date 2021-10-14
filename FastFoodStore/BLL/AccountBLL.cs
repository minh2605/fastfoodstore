using FastFoodStore.DAO;
using FastFoodStore.DTO;
using FastFoodStore.MyLinkedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodStore.BLL
{
    public class AccountBLL
    {
        private static AccountBLL instance;

        public static AccountBLL Instance
        {
            get { if (instance == null) instance = new AccountBLL(); return instance; }
            private set { instance = value; }
        }
        private AccountBLL()
        {

        }
        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord";
            DataTable result = DataProvider.Instance.ExecuteQuery(query,new object []{ userName,passWord});
            return result.Rows.Count > 0;
        }
        public MyList<Account> GetListAccount()
        {
            string query = "SELECT * FROM Account";
            MyList<Account> listAccount = new MyList<Account>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow items in data.Rows)
            {
                Account acc = new Account(items);
                listAccount.Add(acc);
            }
            return listAccount;
        }
        public DataTable GetListAccount2()
        {
            string query = "SELECT * FROM Account";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public Account GetAccByUsername(string userName)
        {
            string query = "SELECT * FROM Account WHERE username = '"+userName+"'";
            DataTable data =DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        public bool UpdateAccount(string userName, string name, int phone , string address,string pass, string newPass)
        {
            int check = DataProvider.Instance.ExecuteNonQuery("EXEC USP_UpdateAccount @userName , @name , @address , @phone , @password , @newPass",new object[] { userName,name,address,phone,pass,newPass});
            return check > 0;
        }
        public DataTable LoadAccountInfo()
        {
            string query = "SELECT I.id,I.userName,I.name,I.phone,I.address,A.type FROM Account AS A , UserInfo AS I WHERE A.userName = I.userName";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public void AddAccount(string username, string pass,int type)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_AddAccount @username , @pass , @type", new object[] { username, pass, type });
        }
        public bool UpdateAccount(string username,string pass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_UpdateAccount3 @username , @pass", new object[] { username, pass });
            return result > 0;
        }
        public DataTable SearchAccount(string name)
        {
            string query = "SELECT I.id,I.userName,I.name,I.phone,I.address,A.type FROM Account AS A , UserInfo AS I WHERE A.userName = I.userName AND I.name LIKE '%"+name+"%'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public bool DeleteAccount(string username)
        {
            string query = "DELETE FROM Account WHERE userName = N'"+username+"'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
