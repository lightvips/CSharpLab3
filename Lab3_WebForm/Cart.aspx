<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Lab3_WebForm.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <style>
        .btn {
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
            <div>
                <div class="error" style="color: red">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </div>
                <asp:GridView ID="GridViewCart" runat="server" AutoGenerateColumns="false" OnRowCommand="GridViewCart_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                        <asp:BoundField DataField="Price" HeaderText="Price" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Total" HeaderText="Total" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button Text="+" runat="server" CommandName="add" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>' />
                                <asp:Button Text="-" runat="server" CommandName="sub" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>' />
                                <asp:Button Text="x" OnClientClick="return confirm('Are you want to remove')" runat="server" CommandName="remove" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </div>
            <asp:HyperLink Text="Back Home" CssClass="btn btn-secondary" ID="HyperLink1" runat="server" NavigateUrl="Home.aspx"></asp:HyperLink>
        </form>
    </div>

</body>
</html>
