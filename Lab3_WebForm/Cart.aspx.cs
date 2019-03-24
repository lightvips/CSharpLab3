using Lab3_WebForm.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab3_WebForm
{
    public partial class Cart : System.Web.UI.Page
    {
        
        DataTable dt = new DataTable();
        Dictionary<int, int> Carts;
        List<int> listPage = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carts = (Dictionary<int, int>)Session["Cart"];
                LoadCart();
            }
        }

        private void LoadCart()
        {
            Carts = (Dictionary<int, int>)Session["Cart"];
            if (Carts.Count > 0)
            {
                Label1.Text = "Your Cart:";
            }
            else
            {
                Label1.Text = "Cart is Empty.!";
            }
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Price", typeof(double));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Total", typeof(int));


            //string countryName = "USA";
            //DataTable dt = new DataTable();
            //int id = (from DataRow dr in dt.Rows
            //          where (string)dr["CountryName"] == countryName
            //          select (int)dr["id"]).FirstOrDefault();
            for (int i = 0; i < Carts.Count; i++)
            {
                int pID = Carts.Keys.ElementAt(i);
                Product p = Product.GetProductByID(pID);
                DataTable dataTable = DAO.GetProductById(pID);
                dt.Rows.Add();
                //dt.Rows[dt.Rows.Count - 1]["ProductID"] = p.ProductID; 
                dt.Rows[dt.Rows.Count - 1]["ProductID"] = (from DataRow dr in dataTable.Rows
                                                            select (int)dr["ProductID"]).FirstOrDefault();
                dt.Rows[dt.Rows.Count - 1]["ProductName"] = (from DataRow dr in dataTable.Rows
                                                             select (string)dr["ProductName"]).FirstOrDefault();
                dt.Rows[dt.Rows.Count - 1]["Price"] = p.Price;
                dt.Rows[dt.Rows.Count - 1]["Quantity"] = Carts[pID];
                dt.Rows[dt.Rows.Count - 1]["Total"] = Carts[pID] * p.Price;
            }
            GridViewCart.DataSource = dt;
            GridViewCart.DataBind();
        }
        protected void GridViewCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Carts = (Dictionary<int, int>)Session["Cart"];
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "add")
            {
                int temp = Carts[id] + 1;
                Carts.Remove(id);
                Carts.Add(id, temp);
            }
            else if (e.CommandName == "sub")
            {
                int temp = Carts[id] - 1;
                Carts.Remove(id);
                if (temp != 0)
                {
                    Carts.Add(id, temp);
                }
            }
            else
            {
                Carts.Remove(id);
                //Session["Cart"]
            }
            Session["Cart"] = Carts;

            LoadCart();
        }
    }
}