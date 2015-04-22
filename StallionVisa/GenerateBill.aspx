<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="GenerateBill.aspx.cs" Inherits="StallionVisa._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        table {
            max-width: 100%;
            background-color: transparent;
            border-collapse: collapse;
            border-spacing: 0;
        }

        .table {
            width: 100%;
            margin-bottom: 0px;
        }

            .table .bold {
                font-weight: bold;
            }

            .table th, .table td {
                padding: 8px;
                line-height: 18px;
                text-align: left;
                vertical-align: top;
                border-top: 1px solid #dddddd;
            }

            .table th {
                font-weight: bold;
            }

            .table thead th {
                vertical-align: bottom;
            }

            .table caption + thead tr:first-child th, .table caption + thead tr:first-child td, .table colgroup + thead tr:first-child th, .table colgroup + thead tr:first-child td, .table thead:first-child tr:first-child th, .table thead:first-child tr:first-child td {
                border-top: 0;
            }

            .table tbody + tbody {
                border-top: 2px solid #dddddd;
            }

        .table-condensed th, .table-condensed td {
            padding: 4px 5px;
        }

        .table-bordered {
            border: 1px solid #dddddd;
            border-collapse: separate;
            border-collapse: collapsed;
            border-left: 0;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
        }

            .table-bordered th, .table-bordered td {
                border-left: 1px solid #dddddd;
            }

            .table-bordered caption + thead tr:first-child th, .table-bordered caption + tbody tr:first-child th, .table-bordered caption + tbody tr:first-child td, .table-bordered colgroup + thead tr:first-child th, .table-bordered colgroup + tbody tr:first-child th, .table-bordered colgroup + tbody tr:first-child td, .table-bordered thead:first-child tr:first-child th, .table-bordered tbody:first-child tr:first-child th, .table-bordered tbody:first-child tr:first-child td {
                border-top: 0;
            }

                .table-bordered thead:first-child tr:first-child th:first-child, .table-bordered tbody:first-child tr:first-child td:first-child {
                    -webkit-border-top-left-radius: 4px;
                    border-top-left-radius: 4px;
                    -moz-border-radius-topleft: 4px;
                }

                .table-bordered thead:first-child tr:first-child th:last-child, .table-bordered tbody:first-child tr:first-child td:last-child {
                    -webkit-border-top-right-radius: 4px;
                    border-top-right-radius: 4px;
                    -moz-border-radius-topright: 4px;
                }

            .table-bordered thead:last-child tr:last-child th:first-child, .table-bordered tbody:last-child tr:last-child td:first-child {
                -webkit-border-radius: 0 0 0 4px;
                -moz-border-radius: 0 0 0 4px;
                border-radius: 0 0 0 4px;
                -webkit-border-bottom-left-radius: 4px;
                border-bottom-left-radius: 4px;
                -moz-border-radius-bottomleft: 4px;
            }

            .table-bordered thead:last-child tr:last-child th:last-child, .table-bordered tbody:last-child tr:last-child td:last-child {
                -webkit-border-bottom-right-radius: 4px;
                border-bottom-right-radius: 4px;
                -moz-border-radius-bottomright: 4px;
            }

        .table-striped tbody tr:nth-child(odd) td, .table-striped tbody tr:nth-child(odd) th {
            background-color: #f9f9f9;
        }




        #table-3 {
            border: 1px solid #5D7B9D;
            background-color: #F9F9F9;
            width: 100%;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            -border-radius: 3px;
            font-family: Arial, "Bitstream Vera Sans",Helvetica,Verdana,sans-serif;
            color: #333;
        }

            #table-3 td, #table-3 th {
                border-top-color: white;
                border-bottom: 1px solid #DFDFDF;
                color: #555;
                font-weight: bold;
            }

            #table-3 th {
                font-family: Arial, "Bitstream Vera Sans",Helvetica,Verdana,sans-serif;
                font-weight: normal;
                padding: 7px 7px 8px;
                text-align: left;
                line-height: 1.3em;
                font-size: 14px;
                font-weight: bold;
                color: #5D7B9D;
            }

            #table-3 td {
                font-size: 12px;
                padding: 4px 7px 2px;
                vertical-align: top;
            }

        input.textbox {
            width: 50px;
        }

        .style1 {
            width: 32%;
        }

        .style4 {
            width: 17%;
        }
    </style>
    <script type="text/javascript">
        function fnValidrate() {
            if (document.getElementById("<%=txtQuantity.ClientID%>").value != "") {
                var dtText = document.getElementById("<%=txtQuantity.ClientID%>").value;
                debugger
                if (isValidrte(dtText)) {
                    var totalamount = document.getElementById("<%=lbltotalAmount.ClientID%>");
                    var perUnitCost = document.getElementById("<%=lblperUnitCost.ClientID%>").innerHTML;
                    totalamount.innerHTML = parseFloat(dtText) * parseFloat(perUnitCost);
                } else {
                    alert("Only Numeric Value is Allowed");
                    document.getElementById("<%=txtQuantity.ClientID %>").value = "";
                    document.getElementById("<%=txtQuantity.ClientID%>").focus();
                    return false;
                }
            }
        }
        function isValidrte(sText) {
            var rerate = /(^\d{0,7}(\.\d{1,2})?$)/;
            return rerate.test(sText);
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="add"
                ShowMessageBox="true" ShowSummary="false" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="save"
                ShowMessageBox="true" ShowSummary="false" />
            <div>
                <table class="table table-bordered table-striped table-condensed">
                    <thead>
                        <tr>
                            <th colspan="2">BILL TO :
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"></asp:Label>
                                <span style="color: red;">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCustomername" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCustomername"
                                    ErrorMessage="Please Fill CustomerName" ForeColor="Red" ValidationGroup="add" Display="None"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress" runat="server" Height="73px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tbody>
                </table>
            </div>
            <div>
                <table class="table table-bordered table-striped table-condensed">
                    <thead>
                        <tr>
                            <th colspan="4">Item Entries :
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblProductName" runat="server" Text="ItemName"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlItem" runat="server" Width="173px" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblUnitMesurement" runat="server" Text="UnitOfMeasurement"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblUnitMeasure" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblCostPerUnit" runat="server" Text="PerUnit Cost(Rs)"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="lblperUnitCost" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label><span style="color: red;">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtquantity"></asp:TextBox></td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuantity"
                                ErrorMessage="Please Fill Quantity" ForeColor="Red" ValidationGroup="add" Display="None"></asp:RequiredFieldValidator>
                            <td>
                                <asp:Label ID="lblTotal" runat="server" Text="Total Amount"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbltotalAmount" runat="server"></asp:Label>
                            </td>

                            <td colspan="2">
                                <div style="float: right;">
                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" ValidationGroup="add"
                                        Width="60" />
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click1" />
                                </div>
                            </td>

                        </tr>
                    </tbody>
                </table>
            </div>
            <br />
            <div align="center">
                <asp:GridView ID="gvItemDesc" runat="server" AutoGenerateColumns="False" Width="80%"
                    CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" AllowPaging="True">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="SELECT">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkRemove" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="measrment" HeaderText="Measurement" />
                        <asp:BoundField DataField="PerunitCost" HeaderText="PerUnit Cost" />
                        <asp:BoundField DataField="Amount" HeaderText="AMOUNT(INR)" />
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
            <br />
            <td>
                <asp:Label ID="lblServicetax" runat="server" Text="ServiceTax"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtserviceTax" runat="server" CssClass="txtquantity" Enabled="false"></asp:TextBox>
            </td>

             <td>
                <asp:Label ID="lblVat" runat="server" Text="VAT"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtVat" runat="server" CssClass="txtquantity" Enabled="false"></asp:TextBox>
            </td>

            <td>
                <asp:Label ID="lblAmount" runat="server" Text="Total"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTotal" runat="server" CssClass="txtquantity" Enabled="false"></asp:TextBox>

            </td>
            <td>
                <asp:Label ID="lbltotalwithtax" runat="server" Text="Total With Tax :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txttotalwithtax" runat="server" CssClass="txtquantity" Enabled="false"></asp:TextBox>

            </td>
            <div>
                <td>
                    <div style="float: right;">
                        <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" ValidationGroup="save"
                            Width="60" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" Width="60" OnClick="btnReset_Click"
                            OnClientClick="btnReset_Click" />
                    </div>
                </td>
            </div>
            <div align="center">
                <table>
                    <tr>
                        <asp:GridView ID="gridViewBill" runat="server" AutoGenerateColumns="False"
                            CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999"
                            BorderStyle="None" BorderWidth="1px" Width="90%" AllowPaging="True"
                            OnRowDataBound="gridViewBill_RowDataBound" ShowFooter="true">
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                            <Columns>
                                <asp:TemplateField HeaderText="Bill Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBill_Id" runat="server" Text='<%#Eval("BillId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBill_Date" runat="server" Text='<%#Eval("billDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClient_Name" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amout">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubtotal" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cenvat(INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblService_Tax" runat="server" Text='<%#Eval("serviceTax") %>'></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="VAT(INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvat" runat="server" Text='<%#Eval("VAT") %>'></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalText" runat="server" Text="Total :"></asp:Label>
                                    </FooterTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount With Tax(INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotal" runat="server" Text='<%#Eval("AmountWithTax") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal1" runat="server"></asp:Label>
                                    </FooterTemplate>
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
                        <div>
                            <td>
                                <div style="float: right;">
                                    <asp:Button ID="btnPrint" runat="server" ValidationGroup="save" OnClick="btnPrint_Click"
                                        Text="Print" Width="60" />
                                </div>
                            </td>
                        </div>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
