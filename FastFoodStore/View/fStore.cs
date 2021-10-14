using FastFoodStore.BLL;
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

namespace FastFoodStore
{
    public partial class fStore : Form
    {
        public fStore()
        {
            InitializeComponent();
            LoadListBillByDate(dtp1.Value,dtp2.Value);
            LoadTotalBill(dtp1.Value, dtp2.Value);
            //LoadTotalRevenue(dtp1.Value, dtp2.Value);  nếu chưa có bill nào trong ngày hiện tại thì cái ni ko mở dc
        }
        #region Bill
        public void LoadListBillByDate(DateTime dateIn, DateTime dateOut)
        {
            dataGridView2.DataSource = BillBLL.Instance.GetListBillByDate(dateIn, dateOut);
        }
        public void LoadTotalBill(DateTime dateIn, DateTime dateOut)
        {
            txbCountBill.Text = BillBLL.Instance.GetTotalBill(dateIn, dateOut).ToString();
        }
        public void LoadTotalRevenue(DateTime dateIn, DateTime dateOut)
        {
            try
            {
                txbTotalRevenue.Text = BillBLL.Instance.GetTotalRevenue(dateIn, dateOut).ToString() + "$";
            }
            catch(Exception )
            {
                MessageBox.Show("No Bill available !");
            }
            
        }
        private void button1_Click(object sender, EventArgs e) // check
        {
            LoadListBillByDate(dtp1.Value, dtp2.Value);
            LoadTotalBill(dtp1.Value, dtp2.Value);
            LoadTotalRevenue(dtp1.Value, dtp2.Value);
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion

        #region Food
        public void LoadFood()
        {
            dtgvFood.DataSource = FoodBLL.Instance.LoadFoodInfo();
            cbCategory.DataSource = CategoryBLL.Instance.GetListCategory2();
            cbCategory.DisplayMember = "Id";
        }
        private void btnShowF_Click(object sender, EventArgs e)
        {
            LoadFood();
            ClearFood();
        }
        public void ClickToShowFood()
        {
            int index = dtgvFood.CurrentCell.RowIndex;
            if (index >= 0)
            {
                txbID.Text = dtgvFood.Rows[index].Cells[0].Value.ToString();
                txbFoodName.Text = dtgvFood.Rows[index].Cells[1].Value.ToString();
                cbCategory.Text = dtgvFood.Rows[index].Cells[2].Value.ToString();
                txbPrice.Text = dtgvFood.Rows[index].Cells[3].Value.ToString();
            }
            else return;
        }
        public void ClearFood()
        {
            txbID.Clear();
            txbFoodName.Clear();
            txbPrice.Clear();
        }
        public int CheckRepeatFood(string tmp)
        {
            MyList<Food> listCate = FoodBLL.Instance.GetListFood();
            int i = 0;
            for (Node k = listCate.Head; k != null; k = k.Next)
            {
                Food items = (Food)k.Data;
                if (tmp == items.Name)
                {
                    i = i + 1;
                }
            }
            return i;
        }
        public void AddFood()
        {
            try
            {
                if (txbFoodName == null || string.IsNullOrWhiteSpace(txbFoodName.Text) || string.IsNullOrWhiteSpace(txbPrice.Text) || txbPrice == null)
                {
                    MessageBox.Show("Please Fill Full textbox !");
                }
                else if (CheckRepeatFood(txbFoodName.Text) > 0)
                {
                    MessageBox.Show("This Food is existed !");
                }
                else
                {
                    FoodBLL.Instance.InsertFood(txbFoodName.Text,Convert.ToInt32(cbCategory.Text),Convert.ToInt32(txbPrice.Text));
                    MessageBox.Show("Add Successfully!");
                    LoadFood();
                }

            }
            catch(Exception)
            {
                MessageBox.Show("Can not Add !");
                MessageBox.Show(cbCategory.Text);
            }
        }
        public void UpdateFood()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txbFoodName.Text) || string.IsNullOrWhiteSpace(txbPrice.Text))
                {
                    MessageBox.Show("Please Fill Full the textbox !");
                }
                else
                {
                    FoodBLL.Instance.UpdateFood(Convert.ToInt32(txbID.Text), txbFoodName.Text, Convert.ToInt32(cbCategory.Text), Convert.ToInt32(txbPrice.Text));
                    MessageBox.Show("Update Successfully!");
                    LoadFood();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not Update!");
            }
        }
        public void DeleteFood()
        {
            try
            {
                FoodBLL.Instance.DeleteFood(Convert.ToInt32(txbID.Text)); 
                MessageBox.Show("Delete Successfully!");
                LoadFood();
            }
            catch (Exception)
            {
                MessageBox.Show("Can not Delete!");
            }
        }
        public void SearchFoodByName()
        {
            dtgvFood.DataSource = FoodBLL.Instance.SearchFood(txbSearch.Text);
        }
        private void dtgvFood_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ClickToShowFood();
        }
        private void btnAddF_Click(object sender, EventArgs e)
        {
            AddFood();
        }

        private void btnUpdateF_Click(object sender, EventArgs e)
        {
            UpdateFood();
        }

        private void btnDeleteF_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want to Delete?", "Delete!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                DeleteFood();
            }
            else return;
        }
        private void btnSearchF_Click(object sender, EventArgs e)
        {
            SearchFoodByName();
        }

        #endregion

        #region Category
        public void LoadCategory()
        {
            dtgvCategory.DataSource = CategoryBLL.Instance.GetListCategory2();
        }
        private void btnShowC_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }
        public void ClickToShowCategory()
        {
            int index = dtgvCategory.CurrentCell.RowIndex;
            if (index >= 0)
            {
                txbIdCate.Text = dtgvCategory.Rows[index].Cells[0].Value.ToString();
                txbCategory.Text = dtgvCategory.Rows[index].Cells[1].Value.ToString();
            }
            else return;
        }
        public int CheckRepeatCategory(string tmp)
        {
            MyList<FoodCategory> listCate = CategoryBLL.Instance.GetListCategory();
            int i = 0;
            for (Node k = listCate.Head; k != null; k = k.Next)
            {
                FoodCategory items = (FoodCategory)k.Data;
                if (tmp == items.Name)
                {
                    i = i + 1;
                }
            }
            return i;
        }
        public void AddCategory()
        {
            try
            {
                if(string.IsNullOrWhiteSpace(txbCategory.Text))
                {
                    MessageBox.Show("Please enter category name !");
                }
                else if(CheckRepeatCategory(txbCategory.Text) > 0)
                {
                    MessageBox.Show("This category is existed !");
                }
                else
                {
                    CategoryBLL.Instance.InsertCategory(txbCategory.Text);
                    MessageBox.Show("Add Successfully!");
                    LoadCategory();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not Add !");
            }
        }
        public void UpdateCategory()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txbCategory.Text))
                {
                    MessageBox.Show("Please enter category name !");
                }
                else
                {
                    CategoryBLL.Instance.UpdateCategory(Convert.ToInt32(txbIdCate.Text),txbCategory.Text);
                    MessageBox.Show("Update Successfully!");
                    LoadCategory();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not Update!");
            }
        }
        public void DeleteCategory()
        {
            try
            {
                FoodBLL.Instance.DeleteFoodByIdCategory(Convert.ToInt32(txbIdCate.Text)); //xóa food trc
                CategoryBLL.Instance.DeleteCategory(Convert.ToInt32(txbIdCate.Text));
                MessageBox.Show("Delete Successfully!");
                LoadCategory();
            }
            catch(Exception)
            {
                MessageBox.Show("Can not Delete!");
            }
        }
        public void SearchCategoryByName()
        {
            try
            {
                dtgvCategory.DataSource= CategoryBLL.Instance.SearchCategory(txbSearchC.Text);
            }
            catch(Exception )
            {
                MessageBox.Show("Can not find this category !");
            }
        }
        private void dtgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ClickToShowCategory();
        }

        private void btnAddC_Click(object sender, EventArgs e)
        {
            AddCategory();
        }

        private void btnUpdateC_Click(object sender, EventArgs e)
        {
            UpdateCategory();
        }

        private void btnDeleteC_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want to Delete?", "Delete!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                DeleteCategory();
            }
            else return;
        }
        private void btnSearchC_Click(object sender, EventArgs e)
        {
            SearchCategoryByName();
        }




        #endregion


    }
}
