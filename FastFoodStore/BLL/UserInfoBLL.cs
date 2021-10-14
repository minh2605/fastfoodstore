using FastFoodStore.DAO;
using FastFoodStore.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodStore.BLL
{
    public class UserInfoBLL
    {
        private static UserInfoBLL instance;

        public static UserInfoBLL Instance
        {
            get { if (instance == null) instance = new UserInfoBLL(); return UserInfoBLL.instance; }
            private set { UserInfoBLL.instance = value; }
        }
        private UserInfoBLL()
        {

        }
        public UserInfo GetUserInfoByUserName(string userName)
        {
            string query = "SELECT * FROM UserInfo WHERE userName ='"+userName+"'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                return new UserInfo(item);
            }
            return null;
        }
        public void AddUser(string username, string name, int phone, string address)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_AddUser @username , @name , @phone , @address", new object[] { username,name,phone,address });
        }
        public bool UpdateUser(int id, string name,int phone, string address)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_UpdateUser @id , @name , @phone , @address", new object[] { id, name, phone, address });
            return result > 0;
        }
    }
}
