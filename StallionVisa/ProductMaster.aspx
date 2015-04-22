<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductMaster.aspx.cs" Inherits="StallionVisa.ProductMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
        #table-3
        {
            border: 1px solid #5D7B9D;
            background-color: #F9F9F9;
            width: 100%;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            -border-radius: 3px;
            font-family: Arial, "Bitstream Vera Sans" ,Helvetica,Verdana,sans-serif;
            color: #333;
        }
        #table-3 td, #table-3 th
        {
            border-top-color: white;
            border-bottom: 1px solid #DFDFDF;
            color: #555;
            font-weight: bold;
        }
        #table-3 th
        {
                      
            font-family: Arial, "Bitstream Vera Sans" ,Helvetica,Verdana,sans-serif;
            font-weight: normal;
            padding: 7px 7px 8px;
            text-align: left;
            line-height: 1.3em;
            font-size: 14px;
            font-weight: bold;
            color: #5D7B9D;
            
        }
        #table-3 td
        {
            font-size: 12px;
            padding: 4px 7px 2px;
            vertical-align: top;
        }
        input.textbox
        { height:12px; width:50px;}
        .griditemtext{ text-align:left;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnlCompany" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save"
                ShowMessageBox="true" ShowSummary="false" />
                <div>
                <table id="table-3">
                    <tbody>
                    <tbody>
                        <tr>
                                <td>
                                    <asp:Label ID="lblItem" runat="server" Text="Item"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtItem" runat="server"  
                                        Width="180px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvItem" runat="server" ControlToValidate="txtItem"
                                         ErrorMessage="Item is Required" ForeColor="Red" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                </td>
                                
                                <caption>
                              
                                </caption>
                           </tr>

                         <tr>
                                <td>
                                    <asp:Label ID="lblUnit" runat="server" Text="Unit Of Measurement"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUnit" runat="server" 
                                        Width="180px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ControlToValidate="txtUnit"
                                         ErrorMessage="Unit is Required" ForeColor="Red" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                </td>
                                
                                <caption>
                              
                                </caption>
                           </tr>

                        <tr>
                                <td>
                                    <asp:Label ID="lblRate" runat="server" Text="Rate"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRate" runat="server" 
                                        Width="180px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRate" runat="server" ControlToValidate="txtRate"
                                         ErrorMessage="Unit Rate is Required" ForeColor="Red" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                </td>
                                
                                <caption>
                              
                                </caption>
                           </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblDescription" runat="server" Text="Item Description"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtitemdescription" runat="server"  TextMode="MultiLine" 
                                        Width="180px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtitemdescription"
                                         ErrorMessage="Description is Required" ForeColor="Red" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                </td>
                                
                                <caption>
                              
                                </caption>
                           </tr>

                        <tr>
                                <td>
                                    <asp:Label ID="lblsertax" runat="server" Text="Service Tax"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsertax" runat="server" Width="180px"></asp:TextBox>  
                                </td>
                                
                                <caption>
                              
                                </caption>
                             <td>
                                    <asp:Label ID="lblvat" runat="server" Text="VAT"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtvat" runat="server" Width="180px"></asp:TextBox>  
                                </td>
                                
                                <caption>
                              
                                </caption>
                           </tr>
            
                            <tr>
                                <td align="center" colspan="2">
                                
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="save"
                                    Width="60"  onclick="btnSave_Click" />
                                <asp:Button ID="btnUpdate" runat="server" 
                                    Text="Update"  Visible="false" onclick="btnUpdate_Click" />
                                <asp:Button ID="btnCancel" runat="server" 
                                    Text="Cancel"  onclick="btnCancel_Click" />
                            </td>
                            </tr>
                        
                    </tbody>
                </table>
                <br />
                <div align="center">
                    <asp:GridView ID="grdproduct" runat="server"  AutoGenerateColumns="False"
                      CellPadding="3"  Width="90%"
                        GridLines="Vertical" onrowcommand="grdproduct_RowCommand" 
                        BackColor="White" BorderColor="#999999" BorderStyle="None" 
                        BorderWidth="1px" AllowPaging="True" 
                        onpageindexchanging="grdproduct_PageIndexChanging" PageSize="20" 
                        >                         
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>

        <asp:TemplateField HeaderText="Item"  ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:Label ID="lblCompany_Name" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
               (<asp:Label ID="lblUnitMeasurement" runat="server" Text='<%#Eval("UnitMeasurement") %>'></asp:Label>)
        </ItemTemplate>
        </asp:TemplateField> 
         <asp:TemplateField HeaderText="Item Description" ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:Label ID="lblItem" runat="server" Text='<%#Eval("ItemDescription") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>      


        <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:LinkButton ID="lnkbtnEdit" CommandName="Eddit" CommandArgument='<%#Eval("ItemId") %>' runat="server" Text="Edit"></asp:LinkButton>&nbsp;|
                <asp:LinkButton ID="lnkbtnDelete" OnClientClick='return confirm("Are You Sure To Delete")' CommandName="Delet" CommandArgument='<%#Eval("ItemId") %>' runat="server" Text="Delete"></asp:LinkButton>
        </ItemTemplate>
        </asp:TemplateField>


</Columns>

    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F1F1F1" />
    <SortedAscendingHeaderStyle BackColor="#0000A9" />
    <SortedDescendingCellStyle BackColor="#CAC9C9" />
    <SortedDescendingHeaderStyle BackColor="#000065" />
</asp:GridView>
                </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
