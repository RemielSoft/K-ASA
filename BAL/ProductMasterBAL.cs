using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DOM;

namespace BAL
{
    public class ProductMasterBAL
    {
        ProductMasterDAL productDAL =new ProductMasterDAL();
        public int Createproductmaster(ProductMasterDOM productDOM)
        {
            int id = 0;

            return id = productDAL.CreateProductMaster(productDOM);


        }
        public List<ProductMasterDOM> ReadProductDetails(int? id)
        {
            List<ProductMasterDOM> lstProductMasterDom = new List<ProductMasterDOM>();

            lstProductMasterDom = productDAL.ReadProductDetails(id);

            return lstProductMasterDom;
        }
        public void DeleteProductDetails(int id, string modifiedBy)
        {


            productDAL.DeleteProductDetails(id, modifiedBy);


        }
        public void UpdateProductMaster(ProductMasterDOM prodom)
        {
            productDAL.UpdateProductMaster(prodom);

        }
        public List<ProductMasterDOM> BindItemMaster()
        {
            List<ProductMasterDOM> lstProductMasterDom = new List<ProductMasterDOM>();
            lstProductMasterDom = productDAL.BindItemMaster();
            return lstProductMasterDom;
        }

        public List<ProductMasterDOM> UnitMesurement(int ProductId)
        {
            List<ProductMasterDOM> lstProductMasterDom = new List<ProductMasterDOM>();
            lstProductMasterDom = productDAL.UnitMesurement(ProductId);
            return lstProductMasterDom;
        }

        public int CreateBill(Bill bill)
        {
           int billId= productDAL.CreateBill(bill);
           return billId;
        }
        public List<ProductMasterDOM> ReadItems(int? itemId)
        {
            List<ProductMasterDOM> lstProductMasterDom = new List<ProductMasterDOM>();

            lstProductMasterDom = productDAL.ReadItems(itemId);

            return lstProductMasterDom;
        }
    }
}
