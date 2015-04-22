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

    public class ProductMasterDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
        SqlCommand cmd = null;
        ProductMasterDOM productMasterDom = new ProductMasterDOM();
        public int CreateProductMaster(ProductMasterDOM productdom)
        {
            int id = 0;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procCreateProductMaster";
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@ItemDescription", productdom.ItemDescription));
            cmd.Parameters.Add(new SqlParameter("@Unit_Of_Measurement", productdom.UnitMeasurement));
            cmd.Parameters.Add(new SqlParameter("@Unit_Rate", productdom.UnitRate));
            cmd.Parameters.Add(new SqlParameter("@ItemName", productdom.ItemName));
            cmd.Parameters.Add(new SqlParameter("@Service_Tax", productdom.ServiceTax));
            cmd.Parameters.Add(new SqlParameter("@VAT", productdom.VAT));
            cmd.Parameters.Add(new SqlParameter("@createdby", productdom.Createdby));
            cmd.Parameters.Add(new SqlParameter("@createddate", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@out_itemId", DbType.Int32));
            cmd.Parameters["@out_itemId"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            id = Convert.ToInt32(cmd.Parameters["@out_itemId"].Value);
            cmd.Dispose();
            con.Close();
            return id;
        }
        public List<ProductMasterDOM> ReadProductDetails(int? id)
        {
            cmd = new SqlCommand("procReadProductMaster", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            if (id != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@in_Item_Id", id));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@in_Item_Id", null));
            }
            SqlDataReader dr = cmd.ExecuteReader();

            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            while (dr.Read())
            {
                ProductMasterDOM productMasterDom = new ProductMasterDOM();
                productMasterDom.ItemId = Convert.ToInt32(dr["item_Id"]);
                productMasterDom.ItemDescription = dr["Item_Description"].ToString();
                if (Convert.ToString(dr["Item_Name"]) != null)
                {
                    productMasterDom.ItemName = Convert.ToString(dr["Item_Name"]);
                }
                if ((dr["unit_Rate"]) == DBNull.Value)
                {
                    productMasterDom.UnitRate = Decimal.MinValue;
                }
                else
                {
                    productMasterDom.UnitRate = Convert.ToDecimal(dr["unit_Rate"]);
                }
                if (Convert.ToString(dr["unit_Of_Mesurement"]) == null)
                {
                    productMasterDom.UnitMeasurement = string.Empty;
                }
                else
                {
                    productMasterDom.UnitMeasurement = Convert.ToString(dr["unit_Of_Mesurement"]);
                }
                if ((dr["Service_Tax"]) == DBNull.Value)
                {
                    productMasterDom.ServiceTax = Decimal.MinValue;
                }
                else
                {
                    productMasterDom.ServiceTax = Convert.ToDecimal(dr["Service_Tax"]);
                }
                if ((dr["VAT"]) == DBNull.Value)
                {
                    productMasterDom.VAT = Decimal.MinValue;
                }
                else
                {
                    productMasterDom.VAT = Convert.ToDecimal(dr["VAT"]);
                }

                lstproductMasterDom.Add(productMasterDom);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lstproductMasterDom;
        }
        public void DeleteProductDetails(int id, string modifiedBy)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procDeletePrductMaster";
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Add(new SqlParameter("@in_Item_Id", id));
            cmd.Parameters.Add(new SqlParameter("@in_Modified_By", modifiedBy));


            cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();

        }
        public void UpdateProductMaster(ProductMasterDOM product)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procUpdateproductMaster";
            cmd.Connection = con;
            con.Open();


            cmd.Parameters.Add(new SqlParameter("@itemId", product.ItemId));
            cmd.Parameters.Add(new SqlParameter("@ItemDescripation", product.ItemDescription));
            cmd.Parameters.Add(new SqlParameter("@ItemName", product.ItemName));
            cmd.Parameters.Add(new SqlParameter("@unit_of_measurement", product.UnitMeasurement));
            cmd.Parameters.Add(new SqlParameter("@unit_Rate", product.UnitRate));
            cmd.Parameters.Add(new SqlParameter("@Service_Tax", product.ServiceTax));
            cmd.Parameters.Add(new SqlParameter("@VAT", product.VAT));
            cmd.Parameters.Add(new SqlParameter("@modifiedby", product.Modifiedby));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public List<ProductMasterDOM> BindItemMaster()
         {
            con.Open();
            cmd = new SqlCommand("procReadProductMasterItem", con);
            SqlDataReader dr = cmd.ExecuteReader();
            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            while (dr.Read())
            {
                ProductMasterDOM productMasterDom = new ProductMasterDOM();
                if (!string.IsNullOrEmpty(dr["item_Name"].ToString().Trim()))
                {
                    productMasterDom.ItemId = Convert.ToInt32(dr["item_Id"]);
                    productMasterDom.ItemName = dr["item_Name"].ToString();
                    lstproductMasterDom.Add(productMasterDom);
                }
            }
            return lstproductMasterDom;
        }
        public List<ProductMasterDOM> UnitMesurement(int ItemId)
        {
            cmd = new SqlCommand("procReadUnitOfMesurementByItemId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@in_Itemid", ItemId));
            SqlDataReader dr = cmd.ExecuteReader();
            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            while (dr.Read())
            {
                ProductMasterDOM productMasterDom = new ProductMasterDOM();
                productMasterDom.ProductId = Convert.ToInt32(dr["item_Id"]);
                productMasterDom.UnitMeasurement =Convert.ToString((dr["unit_Of_Mesurement"]));
                productMasterDom.UnitRate =Convert.ToDecimal((dr["unit_Rate"]));
                lstproductMasterDom.Add(productMasterDom);
            }
            return lstproductMasterDom;
        }
        public int CreateBill(Bill bill)
        {
            int billId = 0;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procCreateBill";
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@customerName", bill.CustomerName));
            cmd.Parameters.Add(new SqlParameter("@customerAddress", bill.CustomerAddress));
            cmd.Parameters.Add(new SqlParameter("@serviceTax", bill.ServiceTax));
            cmd.Parameters.Add(new SqlParameter("@billDate", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@createdby", bill.CreatedBy));
            cmd.Parameters.Add(new SqlParameter("@createddate", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@out_BillId", DbType.Int32));
            cmd.Parameters["@out_BillId"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            billId = Convert.ToInt32(cmd.Parameters["@out_BillId"].Value);
            cmd.Dispose();
            con.Close();
            return billId;
        }
        public List<ProductMasterDOM> ReadItems(int? itemId)
        {
            cmd = new SqlCommand("procReadItem", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            if (itemId != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@in_itemId", itemId));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@in_itemId", null));
            }
            SqlDataReader dr = cmd.ExecuteReader();
            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            while (dr.Read())
            {
                ProductMasterDOM productMasterDom = new ProductMasterDOM();
                productMasterDom.ItemId = Convert.ToInt32(dr["item_Id"]);
                productMasterDom.ItemName = dr["item_Name"].ToString();
                //productMasterDom.ItemId = Convert.ToInt32(dr["Item_Id"]);
                //productMasterDom.AllItem = productMasterDom.SubitemName + productMasterDom.ItemDescription;
                lstproductMasterDom.Add(productMasterDom);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lstproductMasterDom;
        }
    }
}
