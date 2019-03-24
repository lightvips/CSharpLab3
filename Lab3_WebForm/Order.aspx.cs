using Lab3_WebForm.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab3_WebForm
{
    public partial class Order : System.Web.UI.Page
    {
        public string OrderDate, RequiredDate, CustomerID, address;
        Dictionary<int, int> Carts;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carts = (Dictionary<int, int>)Session["Cart"];
                DropDownList1.DataSource = DAO.GetAllCustomers();
                DropDownList1.DataTextField = "ContactName";
                DropDownList1.DataValueField = "CustomerID";
                DropDownList1.DataBind();
                OrderDate = DateTime.Now.ToString("yyyy-MM-dd");
                Label6.Text = OrderDate;
                CustomerID = DropDownList1.SelectedItem.Value;
            }
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            RequiredDate = Calendar1.SelectedDate.ToShortDateString();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomerID = DropDownList1.SelectedValue;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Carts = (Dictionary<int, int>)Session["Cart"];
            OrderDate = DateTime.Now.ToString("yyyy-MM-dd");
            RequiredDate = Calendar1.SelectedDate.ToShortDateString();
            CustomerID = DropDownList1.SelectedValue;
            //address = Request.Form["address"];
            address = AdressTxt.Text;
            TimeSpan deltaDate = Calendar1.SelectedDate - DateTime.Now;
            if (Session["Cart"] != null && Carts.Count > 0)
                if (deltaDate.Days >= 0)
                    if (address.Trim() != "")
                    {
                        int id = DAO.AddOrder(CustomerID, OrderDate, RequiredDate, address);
                        foreach (var item in Carts)
                        {
                            Product product = Product.GetProductByID(item.Key);
                            DAO.AddOrderDetail(id, item.Key, product.Price, item.Value);

                        }
                        Label7.Text = "Order Successfull "; /*+id*/
                        Session["Cart"] = null;
                       
                    }
                    else
                    {
                        Label7.Text = "Address must not be empty.";
                    }
                else
                {
                    Label7.Text = "RequiredDate must be than Today.";
                    
                }
            else
            {
                Label7.Text = "Carts is Empty.";
            }
        }
    }
}