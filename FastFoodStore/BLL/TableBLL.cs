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
    public class TableBLL
    {
        private static TableBLL instance;

        public static TableBLL Instance
        {
            get { if (instance == null) instance = new TableBLL(); return TableBLL.instance; }
            private set { TableBLL.instance = value; }
        }
        private TableBLL()
        {

        }
        public MyList<Table> LoadTableList2()
        {
            MyList<Table> tableList = new MyList<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table); 
            }
            return tableList;

        }
    }
}
