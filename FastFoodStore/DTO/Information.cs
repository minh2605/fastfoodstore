using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodStore.DTO
{
    public class Information
    {
        private int id;
        private string foodName;
        private int count;
        private float price;
        private float totalPrice;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
        public Information(int id, string foodName, int count, float price, float totalPrice)
        {
            this.Id = id;
            this.FoodName = foodName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public Information(DataRow row)
        {
            this.Id = (int)row["idTable"];
            this.FoodName = row["name"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble((row["price"].ToString()));
            this.TotalPrice = (float)Convert.ToDouble((row["Total Price"].ToString()));
        }

    }   
}
