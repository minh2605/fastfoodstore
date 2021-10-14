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
    public class CategoryBLL
    {
        private static CategoryBLL instance;

        public static CategoryBLL Instance
        {
            get { if (instance == null) instance = new CategoryBLL(); return CategoryBLL.instance; }
            private set { CategoryBLL.instance = value; }
        }
        private CategoryBLL()
        {

        }
        public MyList<FoodCategory> GetListCategory() // dung` mylist
        {
            string query = "SELECT * FROM FoodCategory";
            MyList<FoodCategory> listCategory = new MyList<FoodCategory>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow items in data.Rows)
            {
                FoodCategory fCate = new FoodCategory(items);
                listCategory.Add(fCate);
            }
            return listCategory;
        }
        public List<FoodCategory> GetListCategory1() // dung` list
        {
            string query = "SELECT * FROM FoodCategory";
            List<FoodCategory> listCategory = new List<FoodCategory>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow items in data.Rows)
            {
                FoodCategory fCate = new FoodCategory(items);
                listCategory.Add(fCate);
            }
            return listCategory;
        }
        public int GetIdCategoryFromName(string name)
        {
            string query = "SELECT id FROM FoodCategory WHERE name ='"+name+"'";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public DataTable GetListCategory2() // dùng cho store
        {
            string query = "SELECT * FROM FoodCategory";
            
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public void InsertCategory(string nameCategory)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertCategory @name", new object[] { nameCategory });
        }
        public void UpdateCategory(int id,string nameCategory)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_UpdateCategory @id , @name", new object[] { id,nameCategory });
        }
        public void DeleteCategory(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_DeleteCategory @id", new object[] { id });
        }
        public DataTable SearchCategory(string name)
        {
            string query = "SELECT * FROM FoodCategory WHERE name LIKE '%"+name+"%'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
    }
}
