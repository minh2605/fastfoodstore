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
    public class BillInfoBLL
    {
        private static BillInfoBLL instance;

        public static BillInfoBLL Instance
        {
            get { if (instance == null) instance = new BillInfoBLL(); return BillInfoBLL.instance; }
            private set { BillInfoBLL.instance = value; }
        }
        private BillInfoBLL()
        {

        }
        public MyList<BillInfo> GetListBillInfo(int id)
        {
            MyList<BillInfo> listBillInfo = new MyList<BillInfo>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM BillInfo WHERE idBill = " + id);
            foreach(DataRow items in data.Rows)
            {
                BillInfo billInfo = new BillInfo(items);
                listBillInfo.Add(billInfo);
            }
            return listBillInfo;    
        }
        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBIll , @idFood , @count ",new object[] { idBill, idFood, count });
        }
    }
}
