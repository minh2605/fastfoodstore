using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodStore.DTO
{
    public class UserInfo
    {
        private int id;
        private string userName;
        private string name;
        private int phone;
        private string address;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public int Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public UserInfo(int id,string username,string name,int phone,string address)
        {
            this.Id = id;
            this.UserName = username;
            this.Name = name;
            this.Phone = phone;
            this.Address = address;
        }
        public UserInfo(DataRow row)
        {
            this.Id = (int)row["id"];
            this.UserName = row["userName"].ToString();
            this.Name = row["name"].ToString();
            this.Phone = (int)row["phone"];
            this.Address = row["address"].ToString();
        }

    }
}
