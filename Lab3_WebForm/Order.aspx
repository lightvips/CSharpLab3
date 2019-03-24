<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="Lab3_WebForm.Order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <style>
        .btn {
            margin: 10px;
        }
    </style>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
            <div class="form-group">

                <asp:Label ID="Label1" runat="server" Text="Name: "></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="form-group">

                <asp:Label ID="Label2" runat="server" Text="Order Date: "></asp:Label><asp:Label ID="Label6" runat="server" Text=""></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Required Date: "></asp:Label><asp:Calendar ID="Calendar1" runat="server" SelectedDate="<%# DateTime.Today %>" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                    <DayStyle BackColor="#CCCCCC" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                    <TodayDayStyle BackColor="#999999" ForeColor="White" />
                </asp:Calendar>
            </div>
            <br />
            <div class="form-group">
                
                <asp:Label ID="Label5" runat="server" Text="Address : "></asp:Label>
                <asp:TextBox ID="AdressTxt" runat="server" ></asp:TextBox>
                <%--<input id="TextAddress"  type="text" name="address" />--%>
            </div>
            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Order" OnClick="Button1_Click" />
            <asp:Label ID="Label7" runat="server" ForeColor="#FF3300"></asp:Label>
            <br />
            <asp:HyperLink Text="Back Home" CssClass="btn btn-secondary" ID="HyperLink1" runat="server" NavigateUrl="Home.aspx"></asp:HyperLink>
        </form>
    </div>

</body>
</html>
