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
    public class InformationBLL
    {
        private static InformationBLL instance;

        public static InformationBLL Instance
        {
            get { if (instance == null) instance = new InformationBLL(); return InformationBLL.instance; }
            private set { InformationBLL.instance = value; }
        }
        private InformationBLL()
        {

        }   
        public MyList<Information> GetInformation_ByTable(int id)
        {
            string query = "SELECT B.idTable,F.name,BI.count,F.price,F.price*BI.count AS [Total Price] FROM BillInfo AS BI, Bill AS B,Food AS F WHERE BI.idBill = B.id AND BI.idFood = F.id AND B.status =0 AND idTable =" +id;
            MyList<Information> listInfo = new MyList<Information>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow items in data.Rows)
            {
                Information info = new Information(items);
                listInfo.Add(info);
            }
            return listInfo;
        }
    }
}
