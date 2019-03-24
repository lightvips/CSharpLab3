<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebFormDemo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DataList ID="dtl1" runat="server" OnSelectedIndexChanged="dtl1_SelectedIndexChanged"></asp:DataList>
    <asp:GridView ID="gv1" AutoGenerateColumns="false" runat="server" BackColor="#CCFFFF" BorderColor="#003399" BorderStyle="Solid" >
        <Columns>
            <asp:BoundField DataField="ProductID" HeaderText="ID"  />
            <asp:BoundField DataField="ProductName" HeaderText="Name of Product" />
            <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price"/>
            <asp:ButtonField HeaderText="Command" Text="Add To Cart" ButtonType="Button" />
           <%-- <asp:HyperLinkField Text="Edit" DataNavigateUrlFormatString="edit.aspx?pid={0}&name={1}" DataNavigateUrlFields="ProductID,ProductName" />
            <asp:HyperLinkField Text="Delete" DataNavigateUrlFormatString="delete.aspx?pid={0}&cid={1}" DataNavigateUrlFields="ProductID,CategoryID" />--%>
        </Columns>
        <RowStyle BackColor="#00FF99" BorderStyle="Dotted" />
    </asp:GridView>
</asp:Content>
