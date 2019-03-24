<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Lab3_WebForm.Home" %>

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
        .Content {
            overflow: auto;
            margin: 10px;
        }

        .menu {
            float: left;
            width: 20%;
        }

        .TableList {
            float: left;
        }

        .numberPage {
            color: darkred;
            text-decoration: none;
            border: 0.5px lightgrey solid;
            border-radius: 5px;
            padding: 8px 8px 8px 8px;
        }
        .error {
            color : red;
            font-weight:600;
        }
    </style>
</head>
<body>
    <div class="container">
         <form id="form1" runat="server" method="post">
        <div class="Content">
            <div class="menu">
                <asp:DataList ID="DataList1" runat="server" OnItemCommand="DataList1_ItemCommand" CellPadding="4" ForeColor="#333333">
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <ItemTemplate>
                        <asp:LinkButton Text='<%# DataBinder.Eval(Container.DataItem, "CategoryName")%>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CategoryID")%>' runat="server"></asp:LinkButton>
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:DataList>
            </div>
            <div class="TableList">

                <asp:GridView ID="Gridview" AutoGenerateColumns="False" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="Gridview_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ProductID" HeaderText="ID" />
                        <asp:BoundField DataField="ProductName" HeaderText="Name of Product" />
                        <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" />
                        <%--<asp:ButtonField HeaderText="Command" Text="Add To Cart" ButtonType="Button" />--%>
                        <%-- <asp:HyperLinkField Text="Edit" DataNavigateUrlFormatString="edit.aspx?pid={0}&name={1}" DataNavigateUrlFields="ProductID,ProductName" />
            <asp:HyperLinkField Text="Delete" DataNavigateUrlFormatString="delete.aspx?pid={0}&cid={1}" DataNavigateUrlFields="ProductID,CategoryID" />--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button Text="Add to Cart" runat="server" CommandName="Add" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" BorderStyle="None" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>

                <div class="Paging">
                    <asp:DataList ID="DataList3" RepeatDirection="Horizontal" runat="server" OnItemCommand="DataList3_ItemCommand" OnItemDataBound="DataList3_ItemDataBound">
                        <ItemTemplate>
                            <div class="numberPage">
                                <asp:LinkButton ID="PageNumberID"  runat="server" CommandName="PageNumber" CommandArgument="<%# Container.DataItem.ToString() %>" Text="<%# Container.DataItem.ToString() %>"></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div class="error">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </div>
            </div>

        </div>

        <div class="Cart">
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
            <div>
                <asp:HyperLink Text="View Cart" ID="HyperLink1" CssClass="btn btn-success" runat="server" NavigateUrl="Cart.aspx"></asp:HyperLink>
                <br />
                <asp:HyperLink Text="Order" ID="HyperLink2" CssClass="btn btn-info" runat="server" NavigateUrl="Order.aspx"></asp:HyperLink>
            </div>
        </div>
    </form>
    <//div>
   
</body>
</html>
