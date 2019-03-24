using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebFormDemo.Models
{
    public class DAO
    {
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

        public static DataTable getAllEmployees()
        {
            string sql = "select * from Employees";
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
    
    }
}