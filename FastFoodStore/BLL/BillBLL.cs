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
    public class BillBLL
    {
        private static BillBLL instance;

        public static BillBLL Instance
        {
            get { if (instance == null) instance = new BillBLL(); return BillBLL.instance; }
            private set { BillBLL.instance = value; }
        }
        private BillBLL()
        {

        }
        public int GetId_UncheckBill_ByTableId(int id)
        {
            
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM Bill WHERE idTable ="+ id + "AND status = 0");
            if(data.Rows.Count>0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1;
        }
        public void InsertBill(int id, int idBillingPerson)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBill1 @idTable , @idBillingPerson", new object[] { id, idBillingPerson});
        }
        public int GetMaxBillId()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM Bill");
            }
            catch
            {
                return 1; 
            }
        }
        public void CheckOut(int id) 
        {
            string query = "UPDATE Bill SET status = 1, dateOut = GETDATE() WHERE id = "+id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        public DataTable GetListBillByDate(DateTime dateIn, DateTime dateOut)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_GetListBillByDate @dateIn , @dateOut", new object[] { dateIn, dateOut });
        }
        public int GetTotalBill(DateTime dateIn, DateTime dateOut)
        {
            return(int)DataProvider.Instance.ExecuteScalar("EXEC USP_CountTotalBill @dateIn , @dateOut", new object[] { dateIn, dateOut });
        }
        public double GetTotalRevenue(DateTime dateIn, DateTime dateOut)
        {
            return (double)DataProvider.Instance.ExecuteScalar("EXEC USP_TotalRevenue @dateIn , @dateOut", new object[] { dateIn, dateOut });
        }
    }
}
