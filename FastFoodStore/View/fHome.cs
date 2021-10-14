using FastFoodStore.BLL;
using FastFoodStore.DAO;
using FastFoodStore.DTO;
using FastFoodStore.MyLinkedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace FastFoodStore
{
    public partial class fHome : Form
    {
        private Account logined;
        private UserInfo info;
        public Account Logined
        {
            get { return logined; }
            set { logined = value; checkAccount(logined.Type); }
        }   
        public UserInfo Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
            }
        }
            
        public fHome(Account acc,UserInfo user)
        {
            InitializeComponent();
            this.Logined = acc;
            this.Info = user;
            LoadTable();
            LoadFoodCategory();
            //LoadFoodByCategoryName(cbCategory.Text);
        }
        #region Methods
        void checkAccount(int type)
        {
            if(type == 1)
            {
                adminToolStripMenuItem1.Visible = false;
            }
        }
        void LoadTable()
        {
            flpTable.Controls.Clear();
            MyList<Table> tableList = TableBLL.Instance.LoadTableList2();
            for (Node k = tableList.Head; k != null; k = k.Next)
            {

                Table items = (Table)k.Data;
                Button btn = new Button() { Width = 80, Height = 70 };
                btn.Text = items.Name + Environment.NewLine + items.Status;
                btn.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btn.Click += btn_Click;
                btn.Tag = items; //tag co kieu object
                switch (items.Status)
                {
                    case "EMPTY":
                        btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
                        break;
                    default:
                        btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(183)))), ((int)(((byte)(88)))));
                        btn.ForeColor = Color.Black;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        void LoadFoodCategory()
        {
            //DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<FoodCategory> listCate = CategoryBLL.Instance.GetListCategory1();
            cbCategory.DataSource = listCate;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodByCategoryId(int id)
        {
            //MyList<Food> listFood = FoodBLL.Instance.GetFoodByCategoryId(id);
            cbFood.DataSource = FoodBLL.Instance.GetFoodByCategoryName(id);
            cbFood.DisplayMember = "Name";
        }
        void LoadFoodByCategoryName(string name)
        {
            //cbFood.DataSource = FoodBLL.Instance.GetFoodByCategoryName(name);     
        }
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            MyList<Information> listInfo = InformationBLL.Instance.GetInformation_ByTable(id); //idTable
            float totalBill = 0;
            for (Node k = listInfo.Head; k != null; k = k.Next)
            {
                Information items = (Information)k.Data;
                ListViewItem lvItem = new ListViewItem(items.Id.ToString());
                lvItem.SubItems.Add(items.FoodName.ToString());
                lvItem.SubItems.Add(items.Count.ToString());
                lvItem.SubItems.Add(items.Price.ToString());
                lvItem.SubItems.Add(items.TotalPrice.ToString());
                totalBill += items.TotalPrice; 
                lsvBill.Items.Add(lvItem);
            }
            txbTotalBill.Text = totalBill.ToString() + "$";
        }

        #endregion
        #region Events

        private void btn_Click(object sender, EventArgs e)
        {
            int idTable = ((sender as Button).Tag as Table).Id; // lay infoTable từ tag của btn ở trên xún rồi ép kiểu qua Table đê lấy id
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(idTable);
        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fProfile f = new fProfile(Logined,Info);
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fStore f = new fStore();
            f.ShowDialog();
        }

        private void adminToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount = Logined;
            f.ShowDialog();
        }
        #endregion

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox combo = sender as ComboBox;

            //for (int i = 0;i<=combo.Items.Count;i++)
            //{
            //    if(i == combo.SelectedIndex)
            //    {
            //        LoadFoodByCategoryId(combo.SelectedIndex + 1);
            //        break;
            //    }
            //    //chua xu li dc khi xoa 
            //}
            int id = 0;
            ComboBox combo = sender as ComboBox;
            if(combo.SelectedItem == null)
            {
                return;
            }
            else
            {
                FoodCategory cate = combo.SelectedItem as FoodCategory;
                id = cate.Id;
            }
            LoadFoodByCategoryId(id);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            try
            {
                int idFood = FoodBLL.Instance.GetFoodIdByName(cbFood.Text);
                int idBill = BillBLL.Instance.GetId_UncheckBill_ByTableId(table.Id);
                //int idFood = FoodBLL.Instance.GetFoodIdByName(cbFood.Text);
                int count = (int)nmFoodCount.Value;
                if (idBill == -1) // nếu chưa có bill nào
                {
                    BillBLL.Instance.InsertBill(table.Id, Info.Id);
                    BillInfoBLL.Instance.InsertBillInfo(BillBLL.Instance.GetMaxBillId(), idFood, count);
                }
                else //đã tồn tại bill
                {
                    BillInfoBLL.Instance.InsertBillInfo(idBill, idFood, count);
                }
                MessageBox.Show("Add Succesfully!");
                ShowBill(table.Id); //load lại listview
                LoadTable(); // load lại table
                //MessageBox.Show(((int)nmFoodCount.Value).ToString());

            }
            catch
            {
                MessageBox.Show("Please choose the table!");
                //MessageBox.Show("Bill Id Max: "+BillBLL.Instance.GetMaxBillId().ToString());
                //MessageBox.Show("Food Count: "+((int)nmFoodCount.Value).ToString());
                //MessageBox.Show("Food Id: "+FoodBLL.Instance.GetFoodIdByName(cbFood.Text).ToString());
            }
            
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            try
            {
                Table table = lsvBill.Tag as Table;
                int idBill = BillBLL.Instance.GetId_UncheckBill_ByTableId(table.Id);
                //double totalPrice = Convert.ToDouble(txbTotalBill.Text.Split('$')[0]);
                if (idBill != -1)
                {
                    if (MessageBox.Show("Do you want to pay Table " + table.Name, "Notice!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        if(txbTotalBill.Text.Contains("-") || txbTotalBill.Text.StartsWith("0"))
                        {
                            MessageBox.Show("Price is not valid !");
                        }
                        else
                        {
                            BillBLL.Instance.CheckOut(idBill);
                            ShowBill(table.Id);
                            LoadTable();
                        }
                    }
                }
            }
            catch (Exception )
            {
                MessageBox.Show("No Bills to Pay!");
            }
            
        }
    }
}
