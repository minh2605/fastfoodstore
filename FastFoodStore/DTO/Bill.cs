using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodStore.DTO
{
    public class Bill
    {
        private int id;
        private DateTime? dateIn;
        private DateTime? dateOut;
        private int status;
        private int idBillingPerson;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public DateTime? DateIn
        {
            get { return dateIn; }
            set { dateIn = value; }
        }
        public DateTime? DateOut
        {
            get { return dateOut; }
            set { dateOut = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public int IdBillingPerson
        {
            get { return idBillingPerson; }
            set { idBillingPerson = value; }
        }
        public Bill(int id, DateTime? dateIn, DateTime? dateOut,int status,int idBillingPerson)
        {
            this.Id = id;
            this.DateIn = dateIn;
            this.DateOut = dateOut;
            this.Status = status;
            this.IdBillingPerson = idBillingPerson;
        }
        public Bill(DataRow row)
        {
            this.Id = (int)row["id"];
            this.DateIn = (DateTime?)row["dateIn"];
            var dateOutCheck = row["dateOut"];
            if(dateOutCheck.ToString() != "")
            {
                this.DateOut = (DateTime?)dateOutCheck;
            }
            this.Status = (int)row["status"];
            this.IdBillingPerson = (int)row["idBillingPerson"];
        }
    }
}
