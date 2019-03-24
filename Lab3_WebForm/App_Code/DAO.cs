using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lab3_WebForm
{
    public class DAO
    {
        public static int pageSize = Int32.Parse(ConfigurationManager.AppSettings["pageSize"].ToString());
        public static SqlConnection getConnection() //tao ket noi toi csdl
        {
            string conString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ToString();
            SqlConnection myconnection = new SqlConnection(conString);
            return myconnection;
        }

        public static DataTable getDataUsingSql(string sql)
        {
            SqlCommand mycommand = new SqlCommand(sql, getConnection());
            SqlDataAdapter adapt = new SqlDataAdapter();
            adapt.SelectCommand = mycommand;
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            return ds.Tables[0];
        }

        public static int GetTotalRows(int CategoryID)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM products where CategoryID = " + CategoryID, getConnection());
            cmd.Connection.Open();
            int count = (int)cmd.ExecuteScalar();
            cmd.Connection.Close();
            return (int)Math.Ceiling(count / (double)pageSize);
        }


        public static DataTable getProductPaging(int CategoryID, int page)
        {

            if (page == 0)
            {
                page = 1;
            }
            if (pageSize == 0)
            {
                pageSize = 3;
            }
            int from = (page - 1) * pageSize + 1;
            int to = page * pageSize;
            String query = @"select * from (select *, ROW_NUMBER() over (order by ProductID) as RowNumber from 
                            (select * from Products where CategoryID = " + CategoryID + ") c) p where (RowNumber between " + from + " and " + to + ")";
            return getDataUsingSql(query);
        }

        public static DataTable getAllEmployees()
        {
            string sql = "select * from Employees";
            return getDataUsingSql(sql);
        }
        public static DataTable GetProductById(int productId)
        {
            string sql = "select * from Products where ProductID = " + productId;
            return getDataUsingSql(sql);
        }
        public static DataTable GetAllCustomers()
        {
            string sql = "select * from Customers";
            return getDataUsingSql(sql);
        }
        public static int executeSql(string sql)
        {
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Connection.Open();
            int count = command.ExecuteNonQuery();
            command.Connection.Close();
            return count;
        }
        public static int deleteEmployee(int empId)
        {
            return executeSql("delete  from employees where employeeID = " + empId.ToString());
        }

        public static int deleteProduct(int proID)
        {
            executeSql("delete  from [order details] where productID = " + proID.ToString());
            return executeSql("delete  from products where productID = " + proID.ToString());
        }

        public static DataTable getAllCategories()
        {
            return getDataUsingSql("select * from Categories");
        }

        public static DataTable getAllProductsByCatID(int catid)
        {
            return getDataUsingSql("select * from Products where categoryid = " + catid.ToString());
        }

        public static DataTable getAllTitleOfEmp()
        {
            string sql = "select distinct Title from Employees";
            return getDataUsingSql(sql);
        }

        public static int editEmployee(int empId, string name, DateTime birthdate, string title)
        {
            string sql = "update Employees set Lastname = @name, BirthDate = @date, Title = @title where employeeID = @id";
            SqlCommand command = new SqlCommand();
            command.Connection = getConnection();
            command.CommandText = sql;
            command.Parameters.Add("@name", SqlDbType.NVarChar);
            command.Parameters["@name"].Value = name;
            command.Parameters.Add("@date", SqlDbType.DateTime);
            command.Parameters["@date"].Value = birthdate;
            command.Parameters.Add("@title", SqlDbType.NVarChar);
            command.Parameters["@title"].Value = title;
            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = empId;
            command.Connection.Open();
            int count = command.ExecuteNonQuery();
            command.Connection.Close();
            return count;
        }
        public static int AddOrder(string customerId, string orderDate, string RequiredDate, string address)
        {
            //string s = "set identity_insert Products on";
            //string s1 = "set identity_insert Products off";
            string sql = @"INSERT Orders(CustomerID,OrderDate,RequiredDate,ShippedDate,ShipAddress)
                          VALUES(@customerId, @orderDate, @RequiredDate, null, @address);SELECT CAST(scope_identity() AS int)";
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Connection.Open();
            //Console.WriteLine(String.Format("sdadsad{0}",customerId));
            //Console.WriteLine($"sdadsad{customerId}");
            SqlParameter param2 = new SqlParameter("@customerId", SqlDbType.NVarChar);
            param2.Value = customerId;
            command.Parameters.Add(param2);

            SqlParameter param3 = new SqlParameter("@orderDate", SqlDbType.DateTime);
            param3.Value = orderDate;
            command.Parameters.Add(param3);

            SqlParameter param4 = new SqlParameter("@RequiredDate", SqlDbType.DateTime);
            param4.Value = RequiredDate;
            command.Parameters.Add(param4);

            SqlParameter param5 = new SqlParameter("@address", SqlDbType.NVarChar);
            param5.Value = address;
            command.Parameters.Add(param5);

            int k = (int)command.ExecuteScalar();
            if (command.Connection.State == System.Data.ConnectionState.Open) command.Connection.Close();
            return k;

        }
        public static int AddOrderDetail(int OrderID, int ProductID, double Price, int Quantity)
        {
            //string s = "set identity_insert Products on";
            //string s1 = "set identity_insert Products off";
            string sql = @"INSERT into [Order Details] (OrderID,ProductID, UnitPrice, Quantity, Discount)
                            VALUES(@OrderID,@ProductID,@Price,@Quantity,0);";
            SqlCommand command = new SqlCommand(sql, getConnection());
            //command.Parameters.AddWithValue("@na", Mem_NA);
            //command.Parameters.AddWithValue("@occ", Mem_Occ);

            command.Connection.Open();

            SqlParameter param2 = new SqlParameter("@OrderID", SqlDbType.Int);
            param2.Value = OrderID;
            command.Parameters.Add(param2);

            SqlParameter param3 = new SqlParameter("@ProductID", SqlDbType.Int);
            param3.Value = ProductID;
            command.Parameters.Add(param3);

            SqlParameter param4 = new SqlParameter("@Price", SqlDbType.Money);
            param4.Value = Price;
            command.Parameters.Add(param4);

            SqlParameter param5 = new SqlParameter("@Quantity", SqlDbType.Int);
            param5.Value = Quantity;
            command.Parameters.Add(param5);

            int k = command.ExecuteNonQuery();
            command.Connection.Close();
            return k;
        }

    }
}