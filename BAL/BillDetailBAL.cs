using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOM;
using DAL;

namespace BAL
{
    public class BillDetailBAL
    {
        BillDetailDAL obDal = new BillDetailDAL();
        public List<int> AddItems(List<BillDetail> lstBillDetail, int id)
        {
            List<int> billId = new List<int>();
           billId= obDal.AddItems(lstBillDetail,id);
           return billId;
        }

        public int SaveBill(Bill bill)
        {
           return obDal.SaveBill(bill);
        }
        public List<Bill> ReadSexvicestax(int?id)
        {
            return obDal.ReadServiceTax(id);
        }

        public List<BillDetail> ReadBillDetails(int billId)
        {
            var temp1 = new List<BillDetail>();
            temp1 = obDal.ReadBillDetails(billId);
            return temp1;
        }
    }
}
