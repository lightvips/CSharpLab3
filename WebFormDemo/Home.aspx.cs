using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormDemo.Models;

namespace WebFormDemo
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ddl1.DataTextField = "CategoryName";
                ddl1.DataValueField = "CategoryID";

                DataTable dt = DAO.getAllCategories();
                ddl1.DataSource = dt;
                ddl1.DataBind();
                int catID;
                if (Request.QueryString["cid"] != null)
                    catID = Convert.ToInt32(Request.QueryString["cid"]);
                else catID = Convert.ToInt32(dt.Rows[0]["CategoryID"]);
                ddl1.SelectedValue = catID.ToString();

                gv1.DataSource = DAO.getAllProductsByCatID(catID);
                gv1.DataBind();

            }
        }

        protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}