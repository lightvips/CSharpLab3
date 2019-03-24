using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Lab3_WebForm.App_Code
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }

        public double Price { get; set; }
        public bool Discontinued { get; set; }

        public Product(int id, string name, double price)
        {
            ProductID = id;
            ProductName = name;
            Price = price;

        }
        public Product(int id, string name, string catName, string SupName, double price, bool discontinued)
        {
            ProductID = id;
            ProductName = name;
            CategoryName = catName;
            SupplierName = SupName;
            Price = price;
            Discontinued = discontinued;

        }


        //public static List<Product> getProducts()
        //{
        //    DataTable dt = new DataTable();
        //    dt = DataAccess.GetAllProducts();
        //    List<Product> productList = new List<Product>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        Category category = Category.GetCategryByID(Convert.ToInt32(dr["CategoryID"]));
        //        Supplier supplier = Supplier.getSupplierById(Convert.ToInt32(dr["SupplierID"]));
        //        Product product = new Product(
        //             Convert.ToInt32(dr["ProductID"]),
        //             dr["ProductName"].ToString(),
        //             category.CategoryName.ToString(),
        //             supplier.CompanyName.ToString(),
        //             Convert.ToDouble(dr["UnitPrice"]),
        //             Convert.ToBoolean(dr["Discontinued"]));
        //        productList.Add(product);
        //    }
        //    return productList;
        //}

        //public static int deleteProduct(int productId)
        //{
        //    return DataAccess.DeleteProduct(productId);
        //}

        public static Product GetProductByID(int productID)
        {
            DataTable dt = new DataTable();
            dt = DAO.GetProductById(productID);
            DataRow dr = dt.Rows[0];

            Product product = new Product(
                     Convert.ToInt32(dr["ProductID"]),
                     dr["ProductName"].ToString(),
                     Convert.ToDouble(dr["UnitPrice"]));
            return product;
        }
        //public static List<Product> GetProductByCatID(int CatID)
        //{
        //    DataTable dt = new DataTable();
        //    dt = DataAccess.GetProductByCatId(CatID);

        //    List<Product> productList = new List<Product>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        Category category = Category.GetCategryByID(Convert.ToInt32(dr["CategoryID"]));
        //        Supplier supplier = Supplier.getSupplierById(Convert.ToInt32(dr["SupplierID"]));
        //        Product product = new Product(
        //                 Convert.ToInt32(dr["ProductID"]),
        //                 dr["ProductName"].ToString(),
        //                 category.CategoryName.ToString(),
        //                 supplier.CompanyName.ToString(),
        //                 Convert.ToDouble(dr["UnitPrice"]),
        //                 Convert.ToBoolean(dr["Discontinued"]));
        //        productList.Add(product);
        //    }
        //    return productList;
        //}

        //public static List<Product> GetProductByCatIDPaging(int CatID, int page)
        //{
        //    DataTable dt = new DataTable();
        //    dt = DataAccess.getProductPaging(CatID, page);

        //    List<Product> productList = new List<Product>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        Category category = Category.GetCategryByID(Convert.ToInt32(dr["CategoryID"]));
        //        Supplier supplier = Supplier.getSupplierById(Convert.ToInt32(dr["SupplierID"]));
        //        Product product = new Product(
        //                 Convert.ToInt32(dr["ProductID"]),
        //                 dr["ProductName"].ToString(),
        //                 category.CategoryName.ToString(),
        //                 supplier.CompanyName.ToString(),
        //                 Convert.ToDouble(dr["UnitPrice"]),
        //                 Convert.ToBoolean(dr["Discontinued"]));
        //        productList.Add(product);
        //    }
        //    return productList;
        //}

        //public static int EditProduct(int productID, string productName, int categoryName, int supplierName, double price, bool discontinued)
        //{
        //    return DataAccess.EditProduct(productID, productName, categoryName, supplierName, price, discontinued);
        //}
        //public static int AddProduct(int productID, string productName, int categoryName, int supplierName, double price, bool discontinued)
        //{
        //    return DataAccess.AddProduct(productID, productName, categoryName, supplierName, price, discontinued);
        //}

        //public static int pageNumber(int CategoryID)
        //{
        //    return DataAccess.CountRows(CategoryID);
        //}
    }
}