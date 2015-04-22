using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DOM;

namespace DAL
{
    public class BillDetailDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
        SqlCommand cmd = null;

        public List<int> AddItems(List<BillDetail> lstBillDetail, int billId)
        {
            List<int> id = new List<int>();
            foreach (BillDetail billDetail in lstBillDetail)
            {
                int billid = 0;
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "procAddBillDetail";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@in_billId", billId));
                    cmd.Parameters.Add(new SqlParameter("@in_itemId", billDetail.ItemId));
                    cmd.Parameters.Add(new SqlParameter("@in_quantity", billDetail.Quantity));
                    cmd.Parameters.Add(new SqlParameter("@in_rate", billDetail.Rate));
                    cmd.Parameters.Add(new SqlParameter("@in_servicetax", billDetail.serviceTax));
                    cmd.Parameters.Add(new SqlParameter("@in_Vat", billDetail.VAT));
                    cmd.Parameters.Add(new SqlParameter("@in_amount", billDetail.Amount));
                    cmd.Parameters.Add(new SqlParameter("@in_created_by", billDetail.CreatedBy));
                    cmd.Parameters.Add(new SqlParameter("@in_created_date",DateTime.Now));
                    billid = cmd.ExecuteNonQuery();
                    id.Add(billid);
            cmd.Dispose();
            con.Close();
            }
            return id;
        }

        public int SaveBill(Bill bill)
        {
               int id = 0;
               cmd = new SqlCommand();
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandText = "procSaveBillDate";
               cmd.Connection = con;
               con.Open();
               cmd.Parameters.Add(new SqlParameter("@in_item_Id", bill.ItemId));
               cmd.Parameters.Add(new SqlParameter("@in_bill_date", DateTime.Now));
               cmd.Parameters.Add(new SqlParameter("@in_customer_name", bill.CustomerName));
               cmd.Parameters.Add(new SqlParameter("@in_customer_address", bill.CustomerAddress));
               cmd.Parameters.Add(new SqlParameter("@in_created_by", bill.CreatedBy));
               cmd.Parameters.Add(new SqlParameter("@in_created_date", DateTime.Now));
               cmd.Parameters.Add(new SqlParameter("@out_bill_id", DbType.Int32));
               cmd.Parameters["@out_bill_id"].Direction = ParameterDirection.Output;
               cmd.ExecuteNonQuery();
               id = Convert.ToInt32(cmd.Parameters["@out_bill_id"].Value);
               cmd.Dispose();
               con.Close();
               return id;
        }

        public List<BillDetail> ReadItems()
        {
            cmd = new SqlCommand("procReadItems", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr= cmd.ExecuteReader();
            List<BillDetail> lst = new List<BillDetail>();
            while (dr.Read())
            {
                BillDetail ob = new BillDetail();
                ob.Id =Convert.ToInt32(dr["Id"]);
                ob.ItemDescription = dr["item_description"].ToString();
                ob.Quantity = Convert.ToDecimal(dr["quantity"]);
                ob.Rate = Convert.ToDecimal(dr["rate"]);
                ob.Amount = Convert.ToDecimal(dr["amount"]);
                lst.Add(ob);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lst;
        }

        public List<BillDetail> ReadBillDetailByBillId(int billId)
        {
            cmd = new SqlCommand("procReadBillDetailByBillId",con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr;
            cmd.Parameters.Add(new SqlParameter("@in_bill_id", billId));
            dr= cmd.ExecuteReader();
            List<BillDetail> lstBillDetail = new List<BillDetail>();
            while (dr.Read())
            {
                BillDetail ob = new BillDetail();
                ob.BillId = Convert.ToInt32(dr["bill_id"]);
                ob.Box = Convert.ToInt32(dr["Box"]);
                ob.Pieces = Convert.ToInt32(dr["Pieces"]);
                ob.ItemDescription = dr["item_description"].ToString();
                ob.Quantity = Convert.ToDecimal(dr["quantity"]);
                ob.Rate = Convert.ToDecimal(dr["rate"]);
                ob.Amount = Convert.ToDecimal(dr["amount"]);
                ob.ItemName = dr["ItemName"].ToString();
                ob.measrment = dr["measurement"].ToString();
                ob.BoxMeasrument = dr["BoxMeasurement"].ToString();
                ob.PiecesMeasrument = dr["PiecesMeasurement"].ToString();
                ob.CreatedBy = dr["created_by"].ToString();
                
                ob.CreatedDate = Convert.ToDateTime(dr["created_date"]);
                ob.ModifiedBy = dr["modified_by"].ToString();
                if (!string.IsNullOrEmpty(dr["modified_date"].ToString()))
                {
                    ob.ModifiedDate = Convert.ToDateTime(dr["modified_date"]);
                }
                lstBillDetail.Add(ob);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lstBillDetail;

        }
        public void DeleteItemByBillId(int id)
        {
            cmd = new SqlCommand("Delete from Bill_Detail Where Id='" + id + "'",con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public List<Bill> ReadServiceTax(int? taxid)
        {
            cmd = new SqlCommand("procReadServiceTaxMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr;
            if (taxid != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@item_id", taxid));
            }
            dr = cmd.ExecuteReader();
            List<Bill> lstBill = new List<Bill>();
            while (dr.Read())
            {
                Bill Billob = new Bill();
                Billob.ItemId = Convert.ToInt32(dr["item_Id"]);
                if ((dr["Service_Tax"]) == DBNull.Value)
                {
                    Billob.ServiceTax = 0.0M;
                }
                else
                {
                    Billob.ServiceTax = Convert.ToDecimal(dr["Service_Tax"]);
                }
                if ((dr["VAT"]) == DBNull.Value)
                {
                    Billob.VatValue = 0.0M;
                }
                else
                {
                    Billob.VatValue = Convert.ToDecimal(dr["VAT"]);
                }

                lstBill.Add(Billob);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lstBill;
        }
        public List<BillDetail> ReadBillDetails(int billId)
        {
            cmd = new SqlCommand("procReadBillDetailByBillId_New", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@in_bill_Id", billId));
            SqlDataReader dr = cmd.ExecuteReader();
            List<BillDetail> lst = new List<BillDetail>();
            decimal sum = 0;
            while (dr.Read())
            {
                BillDetail billDetails = new BillDetail();
                billDetails.BillId = Convert.ToInt32(dr["bill_id"]);
                billDetails.billDate = Convert.ToDateTime(dr["bill_Date"]);
                billDetails.ItemName = Convert.ToString(dr["item_Name"]);
                billDetails.Amount = Convert.ToDecimal(dr["amount"]);
                if ((dr["service_tax"]) == DBNull.Value)
                {
                    billDetails.serviceTax = Decimal.Round(Decimal.MinValue, 2);
                }
                else
                {
                    billDetails.serviceTax = Decimal.Round((Convert.ToDecimal(dr["service_tax"])) * (Convert.ToDecimal(dr["amount"])) / 100,2);
                }

                if ((dr["VAT"]) == DBNull.Value)
                {
                    billDetails.VAT = Decimal.Round(Decimal.MinValue,2);
                }
                else
                {
                    billDetails.VAT = Decimal.Round((Convert.ToDecimal(dr["VAT"])) * (Convert.ToDecimal(dr["amount"])) / 100,2);
                }
                billDetails.AmountWithTax =Decimal.Round(billDetails.Amount + billDetails.serviceTax + billDetails.VAT,2);
                sum += billDetails.AmountWithTax;
                billDetails.TotalAmountWithTax = Decimal.Round(sum,2);
                billDetails.Amount = Convert.ToDecimal(dr["amount"]);
                lst.Add(billDetails);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lst;
        }
    }
}
