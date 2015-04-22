using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DOM;
using BAL;


namespace StallionVisa
{
    public partial class ProductMaster : System.Web.UI.Page
    {
        #region Global Variables


        ProductMasterBAL productBAL = new ProductMasterBAL();
        ProductMasterDOM productmasterDom = new ProductMasterDOM();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ShowproductDetails();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Saveproductmaster();
            ShowproductDetails();
            Clear();
        }
        private void ShowMessage(string message)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", message), true);
        }
        private void Saveproductmaster()
        {
            int id;
            productmasterDom.ItemDescription = txtitemdescription.Text.TrimStart();
            productmasterDom.ItemName = txtItem.Text.TrimStart();
            productmasterDom.UnitRate = Convert.ToDecimal(txtRate.Text);
            productmasterDom.UnitMeasurement = txtUnit.Text.TrimStart();
            if (!string.IsNullOrEmpty(txtsertax.Text))
            {
                productmasterDom.ServiceTax = Convert.ToDecimal(txtsertax.Text.TrimStart());
            }
            else 
            {
                txtsertax.Text = "0";
            }
            if (!string.IsNullOrEmpty(txtvat.Text))
            {
                productmasterDom.VAT = Convert.ToDecimal(txtvat.Text);
            }
            else
            {
                
                txtvat.Text = "0";
            }
           
            productmasterDom.Createdby = "Admin";
            id = productBAL.Createproductmaster(productmasterDom);
            if (id > 0)
            {
                ShowMessage("Record Saved Successfully");
            }
            else
            {
                ShowMessage("This Product Name Is Already Exist");
            }
        }
        protected void grdproduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            ViewState["CompanyId"] = id;
            if (e.CommandName == "Eddit")
            {
                List<ProductMasterDOM> lst = new List<ProductMasterDOM>();
                lst = productBAL.ReadProductDetails(id);
                txtitemdescription.Text = lst[0].ItemDescription;
                txtItem.Text = lst[0].ItemName;
                txtUnit.Text = lst[0].UnitMeasurement;
                txtRate.Text = Convert.ToString(lst[0].UnitRate);
                txtsertax.Text = Convert.ToString(lst[0].ServiceTax);
                txtvat.Text = Convert.ToString(lst[0].VAT);
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
            else if (e.CommandName == "Delet")
            {

                productBAL.DeleteProductDetails(id, "Admin");
                ShowproductDetails();
                Clear();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCompanyMaster();

            Clear();
            ShowproductDetails();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        private void UpdateCompanyMaster()
        {

            ProductMasterDOM product = new ProductMasterDOM();
            product.ItemId = Convert.ToInt32(ViewState["CompanyId"]);
            product.ItemDescription = txtitemdescription.Text.Trim();
            product.ItemName = txtItem.Text.Trim();
            product.UnitMeasurement = txtUnit.Text.Trim();
            product.UnitRate = Convert.ToDecimal(txtRate.Text);
            if (!string.IsNullOrEmpty(txtsertax.Text))
            {
                product.ServiceTax = Convert.ToDecimal(txtsertax.Text);
            }
            else
            {
                txtsertax.Text = "0";
            }
            if (!string.IsNullOrEmpty(txtvat.Text))
            {
                product.VAT = Convert.ToDecimal(txtvat.Text);
            }
            else
            {
                txtvat.Text = "0";
            }
            product.Modifiedby = "Admin";
            productBAL.UpdateProductMaster(product);
        }
        private void ShowproductDetails()
        {
            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            lstproductMasterDom = productBAL.ReadProductDetails(0);
            grdproduct.DataSource = lstproductMasterDom;
            grdproduct.DataBind();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Visible = false;
            btnSave.Visible = true;
        }
         private void Clear()
        {
            txtitemdescription.Text = string.Empty;
            txtItem.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtsertax.Text = string.Empty;
            txtvat.Text = string.Empty;
        }

         protected void grdproduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdproduct.PageIndex = e.NewPageIndex;
            ShowproductDetails();
            
        }

    }

}