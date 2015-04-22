using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using DOM;
//using System.Windows.Forms;

namespace StallionVisa
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Global Variables

        Decimal amount = 0;
        decimal Total = 0;
        Decimal quantity = 0;
        Decimal rate = 0;
        Decimal grandTotal = 0;
        Decimal serviceTax = 0;
        Decimal vat = 0;
        Decimal serviceCharge = 0;
        List<BillDetail> lstBillDetail = new List<BillDetail>();
        List<Bill> lstBil = new List<Bill>();
        BillDetailBAL obBal = new BillDetailBAL();
        ViewBillBAL obViewBillBL = new ViewBillBAL();
        #endregion

        #region Protected Method

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["tempdata"] = null;
                btnPrint.Visible = false;
                int billid = Convert.ToInt32(Request.QueryString["billId"]);
                txtQuantity.Attributes.Add("OnBlur", "return fnValidrate()");
                BindItemMaster();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BillDetail billDetail = new BillDetail();
            ProductMasterBAL productMasterBAL = new ProductMasterBAL();
            BillDetailBAL Billob = new BillDetailBAL();
            List<Bill> lstbill = new List<Bill>();
            Bill bill = new Bill();
            int id;
            id = Convert.ToInt32(ddlItem.SelectedItem.Value);
            billDetail.ItemId = Convert.ToInt32(ddlItem.SelectedItem.Value);
            lstbill = Billob.ReadSexvicestax(billDetail.ItemId);
            foreach (var item in lstbill)
            {
                billDetail.serviceTax = item.ServiceTax;
                billDetail.VAT = item.VatValue;
            }
            billDetail.ItemName = Convert.ToString(ddlItem.SelectedItem.Text);
            billDetail.CustomerName = txtCustomername.Text;
            billDetail.CustomerAddress = txtAddress.Text;
            billDetail.Quantity = Convert.ToDecimal(txtQuantity.Text);
            billDetail.CreatedBy = "Admin";
            billDetail.Amount = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(lblperUnitCost.Text);
            lbltotalAmount.Text = Convert.ToString(Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(lblperUnitCost.Text));
            billDetail.measrment = lblUnitMeasure.Text;
            billDetail.Rate = Convert.ToDecimal(lblperUnitCost.Text);
            billDetail.PerunitCost = Convert.ToDecimal(lblperUnitCost.Text);
            //billDetail.serviceTax = Convert.ToDecimal(txtserviceTax.Text);
            //billDetail.VAT = Convert.ToDecimal(txtVat.Text);
            List<BillDetail> lstData = (List<BillDetail>)Session["tempdata"];
            if (lstData != null)
            {
                lstData.Add(billDetail);
                Session["tempdata"] = lstData;
            }
            else
            {
                lstBillDetail.Add(billDetail);
                Session["tempdata"] = lstBillDetail;
            }
            txtserviceTax.Text = string.Empty;
            //txtserviceTax.Text =Convert.ToString(billDetail.serviceTax);
            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            if (id <= 0)
            {
                ViewState["itemId"] = 0;
            }
            else
            {
                lstproductMasterDom = productMasterBAL.ReadItems(id);
                ViewState["itemId"] = lstproductMasterDom[0].ItemId;
            }
            BindGrid();
            ClearItems();

            if (txtCustomername.Text != null)
            {
                txtCustomername.ReadOnly = true;
            }
            else
            {
                txtCustomername.ReadOnly = false;
            }
            if (txtAddress.Text != null)
            {
                txtAddress.ReadOnly = true;
            }
            else
            {
                txtAddress.ReadOnly = false;
            }
            Readservicetax();
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            SaveData();
            ClearAllData();
            btnPrint.Visible = true;
            // Response.Redirect("GenerateBill.aspx");
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Session["billId"] != null)
            {
                int billId = Convert.ToInt32(Session["billId"]);
                Response.Redirect("/ReportSSRS/ReportBill.aspx?billId=" + billId);
            }
            else
            {
                Response.Redirect("/ReportSSRS/ReportBill.aspx?billId=" + Convert.ToInt32(ViewState["billId"]));
            }
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            lbltotalAmount.Text = CalculateAmount().ToString();
        }
        public void Readservicetax()
        {
            int Itemid = 0;
            Itemid = Convert.ToInt32(ViewState["itemId"]);
            BillDetailBAL Billob = new BillDetailBAL();
            List<Bill> lstbill = new List<Bill>();
            lstbill = Billob.ReadSexvicestax(Itemid);
            if (lstbill.Count > 0)
            {
                if (lstbill[0].ServiceTax == Decimal.MinValue)
                {
                    txtserviceTax.Text = "0";
                }
                else
                {
                    txtserviceTax.Text = lstbill[0].ServiceTax.ToString();
                }
                if (lstbill[0].VatValue == Decimal.MinValue)
                {
                    txtVat.Text = "0";
                }
                else
                {
                    txtVat.Text = lstbill[0].VatValue.ToString();

                }
            }
        }
        #endregion

        #region private Method
        private decimal CalculateAmount()
        {
            if (!string.IsNullOrEmpty(txtQuantity.Text.Trim()) && !string.IsNullOrEmpty(lblCostPerUnit.Text.Trim()))
            {
                quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
                rate = Convert.ToDecimal(lblCostPerUnit.Text.Trim());
                amount = quantity * rate;
            }
            return amount;
        }

        private void Calculate_TotalAmt()
        {
            if (Session["tempdata"] != null)
            {
                List<BillDetail> lstData = (List<BillDetail>)Session["tempdata"];

              //  txtTotal.Text = (lstData.Sum(Items => Items.Amount)).ToString();

                foreach (var item in lstData)
                {
                    serviceCharge = (item.Amount * item.serviceTax / 100);
                    Total = (item.Amount * item.VAT / 100);
                    grandTotal = Decimal.Round(item.Amount + serviceCharge + Total,2);
                    txttotalwithtax.Text = grandTotal.ToString();
                    txtTotal.Text =Convert.ToString(item.Amount);
                }
                
            }
        }

        private decimal CalculateServiceTax()
        {
            try
            {
                Decimal totalAmount = 0;
                if (!String.IsNullOrEmpty(txtTotal.Text))
                {
                    totalAmount = Convert.ToDecimal(txtTotal.Text);
                }
                if (!string.IsNullOrEmpty(txtserviceTax.Text.Trim()))
                {
                    serviceTax = Convert.ToDecimal(txtserviceTax.Text.Trim());
                    serviceCharge = (totalAmount * serviceTax / 100);
                }
                if (!string.IsNullOrEmpty(txtVat.Text.Trim()))
                {
                    vat = Convert.ToDecimal(txtVat.Text.Trim());
                    Total = (totalAmount * vat / 100);
                }
                //grandTotal = ((totalAmount + (totalAmount * serviceTax) / 100));
                grandTotal = totalAmount + serviceCharge + Total;
                //txttotalwithtax.Text = grandTotal.ToString();
                return grandTotal;
            }
            catch (Exception exp)
            {
                return 0;
            }

        }
        private void BindItemMaster()
        {
            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            ProductMasterBAL productMasterBAL = new ProductMasterBAL();
            lstproductMasterDom = productMasterBAL.BindItemMaster();
            ddlItem.DataSource = lstproductMasterDom;
            ddlItem.DataValueField = "ItemId";
            ddlItem.DataTextField = "ItemName";
            ddlItem.DataBind();
            ddlItem.Items.Insert(0, new ListItem("...Select Item...", "0"));
        }
        private void BindGrid()
        {
            if (Session["tempdata"] != null)
            {
                List<BillDetail> lstData = (List<BillDetail>)Session["tempdata"];
                gvItemDesc.DataSource = lstData;
                gvItemDesc.DataBind();
                Readservicetax();
                Calculate_TotalAmt();
                CalculateServiceTax();
            }
        }

        private void ClearItems()
        {
            ddlItem.SelectedIndex = 0;
            txtserviceTax.Text = string.Empty;
            txtVat.Text = string.Empty;
            lbltotalAmount.Text = string.Empty;
            lblUnitMeasure.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            lblperUnitCost.Text = string.Empty;

        }

        private void ClearAllData()
        {
            ddlItem.SelectedIndex = 0;
            txtAddress.Text = string.Empty;
            txtserviceTax.Text = string.Empty;
            txtVat.Text = string.Empty;
            lblTotal.Text = string.Empty;
            lblUnitMeasure.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtCustomername.Text = string.Empty;
            lblperUnitCost.Text = string.Empty;
            txtTotal.Text = string.Empty;
            txttotalwithtax.Text = string.Empty;
            gvItemDesc.DataSource = null;
            gvItemDesc.DataBind();
            Session["tempdata"] = null;
            ViewState["itemname"] = null;
        }
        private int SaveData()
        {
            int id = 0;
            List<BillDetail> lstData = new List<BillDetail>();
            Bill bill = new Bill();
            if (!string.IsNullOrEmpty(txtserviceTax.Text))
            {
                bill.ServiceTax = Convert.ToDecimal(txtserviceTax.Text);
            }
            else
            {
                bill.ServiceTax = Decimal.MinValue;
            }
            if (!string.IsNullOrEmpty(txtVat.Text))
            {
                bill.VatValue = Convert.ToDecimal(txtVat.Text);
            }
            else
            {
                bill.VatValue = Decimal.MinValue;
            }
            lstData = (List<BillDetail>)Session["tempdata"];
            foreach (var item in lstData)
            {
                bill.ItemId = item.ItemId;
                bill.CustomerName = item.CustomerName;
                bill.CustomerAddress = item.CustomerAddress;
            }
            bill.CreatedBy = "Admin";
            bill.TotalAmount = Convert.ToDecimal(txtTotal.Text);
            bill.TotalWithTax = Convert.ToDecimal(txttotalwithtax.Text);
            if (lstData != null)
            {
                id = obBal.SaveBill(bill);
                Session["billId"] = id;
                obBal.AddItems(lstData, id);
                lstData = ReadBillDetails(id);
                gridViewBill.DataSource = lstData;
                gridViewBill.DataBind();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", "Your Generated Bill id is " + id.ToString()), true);
                ClearAllData();
            }
            return id;
        }
        private List<BillDetail> ReadBillDetails(int billId)
        {
            var temp = new List<BillDetail>();
            temp = obBal.ReadBillDetails(billId);
            return temp;
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtserviceTax.Text = string.Empty;
            txtVat.Text = string.Empty;
            ClearItems();
            ddlItem.SelectedIndex = 0;
            txtTotal.Text = string.Empty;
            Session["tempdata"] = null;
            gvItemDesc.DataSource = null;
            gvItemDesc.DataBind();
            ddlItem.ClearSelection();
            Response.Redirect("GenerateBill.aspx");

        }
        protected void btnRemove_Click1(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow item in gvItemDesc.Rows)
                {
                    List<BillDetail> lstData = (List<BillDetail>)Session["tempdata"];
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)item.FindControl("chkRemove");
                    if (chk.Checked == true)
                    {
                        int index = item.RowIndex;
                        lstData.RemoveAt(index);
                        Session["tempdata"] = lstData;
                        BindGrid();
                    }
                    else if (item.RowIndex == gvItemDesc.Rows.Count - 1 && chk.Checked != true)
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", "Select Any Record to Remove "), true);
                    }
                    if (gvItemDesc.Rows.Count == 0)
                    {
                        ClearAllData();
                    }
                    Calculate_TotalAmt();
                }
                txtQuantity.Text = string.Empty;
            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show("At a time only one item can be removed");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", "At a time only one item can be removed "), true);
            }
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemId;
            itemId = Convert.ToInt32(ddlItem.SelectedItem.Value);
            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            ProductMasterBAL productMasterBAL = new ProductMasterBAL();
            lstproductMasterDom = productMasterBAL.UnitMesurement(itemId);
            lblUnitMeasure.Text = lstproductMasterDom[0].UnitMeasurement;
            lblperUnitCost.Text = Convert.ToString((lstproductMasterDom[0].UnitRate));
            var NoOfItem = txtQuantity.Text;
            if (NoOfItem != "")
            {
                lblperUnitCost.Text = Convert.ToString((lstproductMasterDom[0].UnitRate) * Convert.ToDecimal(NoOfItem));
            }
            //  ViewState["itemname"] = lstproductMasterDom[0].ItemName;
        }
        #endregion

        protected void gridViewBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                Total = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmountWithTax"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal1 = (Label)e.Row.FindControl("lblTotal1");
                lblTotal1.Text = Total.ToString();
            }
        }
    }
}
