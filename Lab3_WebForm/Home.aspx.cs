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
   
    public partial class Home : System.Web.UI.Page
    {
        Dictionary<int, int> Cart;
        DataTable dt = new DataTable();
        List<int> listPage = new List<int>();
        public int CurrentPage = 1;
        public int CateID = 1;
        public void GetListPage (int size)
        {
            listPage.Clear();
            for (int i = 1; i <= size; i++)
            {
                listPage.Add(i);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                GetListPage(DAO.GetTotalRows(1));
                if (Session["Cart"] == null)
                {
                    Session["Cart"] = new Dictionary<int, int>();
                }
                if (Session["CateID"] == null)
                    Session["CateID"] = 1;
                Cart = (Dictionary<int, int>)Session["Cart"];
                DataList1.DataSource = DAO.getAllCategories();
                DataList1.DataBind();
                //Gridview.DataSource = DAO.getAllProductsByCatID(1);
                Gridview.DataSource = DAO.getProductPaging(1, 1);
                Gridview.DataBind();
                DataList3.DataSource = listPage;
                DataList3.DataBind();
                LoadCart();
            }
           
        }

        private void LoadCart()
        {
            Cart = (Dictionary<int, int>)Session["Cart"];

            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Price", typeof(double));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Total", typeof(int));

            for (int i = 0; i < Cart.Count; i++)
            {
                int pID = Cart.Keys.ElementAt(i);
                Product p = Product.GetProductByID(pID);
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1]["ProductID"] = p.ProductID;
                dt.Rows[dt.Rows.Count - 1]["ProductName"] = p.ProductName;
                dt.Rows[dt.Rows.Count - 1]["Price"] = p.Price;
                dt.Rows[dt.Rows.Count - 1]["Quantity"] = Cart[pID];
                dt.Rows[dt.Rows.Count - 1]["Total"] = Cart[pID] * p.Price;
            }
            GridViewCart.DataSource = dt;
            GridViewCart.DataBind();
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            int catID = Convert.ToInt32(e.CommandArgument);
            //Gridview.DataSource = DAO.getAllProductsByCatID(catID);
            if(DAO.getProductPaging(catID, 1).Rows.Count > 0)
            {
                Gridview.DataSource = DAO.getProductPaging(catID, 1);
                Session["CateID"] = "";
                Session["CateID"] = catID;
                GetListPage(DAO.GetTotalRows(catID));
                DataList3.DataSource = listPage;
                DataList3.DataBind();
                Gridview.DataBind();
                Label1.Text = "";
            }
            else
            {
                Label1.Text = "Not found Data of this Category!";
                Gridview.DataSource = null;
                Gridview.DataBind();
                Session["CateID"] = null;
                DataList3.DataSource = null;
                DataList3.DataBind();

            }
        }

        protected void DataList3_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "PageNumber")
            {
                CurrentPage = Convert.ToInt32(e.CommandArgument);
                //int id = Convert.ToInt32(DropDownList1.SelectedValue);
                int id = Convert.ToInt32(Session["CateID"]);
                GetListPage(DAO.GetTotalRows(id));
                DataList1.DataSource = DAO.getAllCategories();
                DataList1.DataBind();
                DataList3.DataSource = listPage;
                DataList3.DataBind();
                Gridview.DataSource = DAO.getProductPaging(id, CurrentPage);
                Gridview.DataBind();
            }
            //if (e.CommandName == "Category")
            //{
            //    Session["CateID"] = Convert.ToInt32(e.CommandArgument);
            //    GetListPage(DAO.GetTotalRows(Convert.ToInt32(Session["CateID"])));
            //    DataList1.DataSource = DAO.getAllCategories();
            //    DataList1.DataBind();
            //    DataList3.DataSource = listPage;
            //    DataList3.DataBind();
            //    Gridview.DataSource = DAO.getProductPaging(Convert.ToInt32(Session["CateID"]), CurrentPage);
            //    Gridview.DataBind();
            //}
        }

        protected void DataList3_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            LinkButton btn = (LinkButton)e.Item.FindControl("PageNumberID");
            if (btn.CommandArgument != CurrentPage.ToString()) return;
            btn.CssClass = "active";
            btn.Enabled = false;
        }

        protected void GridViewCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Cart = (Dictionary<int, int>)Session["Cart"];
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "add")
            {
                int temp = Cart[id] + 1;
                Cart.Remove(id);
                Cart.Add(id, temp);
            }
            else if (e.CommandName == "sub")
            {
                int temp = Cart[id] - 1;
                Cart.Remove(id);
                if (temp != 0)
                {
                    Cart.Add(id, temp);
                }
            }
            else
            {
                Cart.Remove(id);
            }
            Session["Cart"] = Cart;

            LoadCart();
        }

        protected void Gridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Cart = (Dictionary<int, int>)Session["Cart"];

            if (e.CommandName == "Add")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                bool exist = Cart.ContainsKey(id);

                if (!exist)
                {
                    Cart.Add(id, 1);
                }
                else
                {
                    int temp = Cart[id] + 1;
                    Cart.Remove(id);
                    Cart.Add(id, temp);
                }
                Session["Cart"] = Cart;

                LoadCart();
            }
        }
    }
}