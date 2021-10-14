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
    public class FoodBLL
    {
        private static FoodBLL instance;

        public static FoodBLL Instance
        {
            get { if (instance == null) instance = new FoodBLL(); return FoodBLL.instance; }
            private set { FoodBLL.instance = value; }
        }
        private FoodBLL()
        {

        }
        public MyList<Food> GetListFood()
        {
            string query = "SELECT * FROM Food";
            MyList<Food> listFood = new MyList<Food>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow items in data.Rows)
            {
                Food food = new Food(items);
                listFood.Add(food);
            }
            return listFood;
        }
        public MyList<Food> GetFoodByCategoryId(int id)
        {
            string query = "SELECT * FROM Food WHERE idCategory = "+id;
            MyList<Food> listFood = new MyList<Food>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow items in data.Rows)
            {
                Food food = new Food(items);
                listFood.Add(food);
            }
            return listFood;
        }
        public DataTable GetFoodByCategoryName(int id)
        {
            //string query = "SELECT * FROM Food AS F, FoodCategory AS FC WHERE FC.id = F.idCategory AND F.name = '" + name +"'";
            string query = "SELECT * FROM Food WHERE idCategory = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;

        }
        public int GetFoodIdByName(string name)
        {
            string query = "SELECT id FROM Food WHERE name = '"+name+"'";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public DataTable LoadFoodInfo() //Store
        {
            string query = "SELECT * FROM Food";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public void InsertFood(string name, int idCate,int price)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_AddFood @name , @idCate , @price", new object[] { name, idCate, price });
        }
        public void UpdateFood(int id, string name,int idCate,int price)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_UpdateFood @id , @name , @idCate , @price", new object[] { id, name, idCate, price });
        }
        public DataTable SearchFood(string name)
        {
            string query = "SELECT * FROM Food WHERE name LIKE '%" + name + "%'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public void DeleteFood(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_DeleteFood @id", new object[] { id });
        }
        public void DeleteFoodByIdCategory(int id)
        {
            string query = "DELETE FROM Food WHERE idCategory = " + id;
            DataProvider.Instance.ExecuteQuery(query);
        }
        
    }
}
