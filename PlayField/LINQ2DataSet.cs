using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayField
{
     public class LINQ2DataSet
    {
        static string constr = ConfigurationManager.ConnectionStrings["TestDBConnectionString"].ConnectionString;
        static string AdWorks = ConfigurationManager.ConnectionStrings["AdWorks"].ConnectionString;
        static DataSet ds = new DataSet();

        private static void GetTable()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter("Select * From Color; Select * From Employees; Select * From Departments", con))
                {
                    adapter.Fill(ds);
                }
            }
        }

        public static void Selection()
        {
            GetTable();
            DataTable colors = ds.Tables[0];
            EnumerableRowCollection<DataRow> set = colors.AsEnumerable();

            IEnumerable<DataRow> query =
                from color in set
                select color;

            PrintQuery(query);

            var q = from color in set
                    select color.Field<string>("name");
            foreach (var row in q)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine();

            DataTable emps = ds.Tables[1];
            DataTable deps = ds.Tables[2];
            var q1 = from emp in emps.AsEnumerable()
                     from dep in deps.AsEnumerable()
                     where emp.Field<int>("DepartmentId") == dep.Field<int>("ID")
                        && emp.Field<string>("Gender") == "Male"
                     select new
                     {
                         DepartmentID = emp.Field<int>("DepartmentId"),
                         Name = emp.Field<string>("Name"),
                         Location = dep.Field<string>("Location"),
                         Department = dep.Field<string>("Name")
                     };
            foreach (var smallOrder in q1)
            {
                Console.WriteLine("Department ID: {0} Name: {1} Location: {2} Department: {3} ",
                    smallOrder.DepartmentID, smallOrder.Name,
                    smallOrder.Location, smallOrder.Department);
            }
            Console.WriteLine();


        }

        private static void PrintQuery(IEnumerable<DataRow> query)
        {
            foreach (DataRow row in query)
            {
                for (int i = 0; i < query.FirstOrDefault().ItemArray.Length; i++)
                {
                    Console.Write(String.Format("{0, 15}", row.ItemArray[i])); 
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        //Invalid
        private static void MakeSet()
        {
            try
            {
                // Create a new adapter and give it a query to fetch sales order, contact,
                // address, and product information for sales in the year 2002. Point connection
                // information to the configuration setting "AdventureWorks".
                string connectionString = @"Data Source=LAPTOP-M6HVHICA\SQLEXPRESS;Initial Catalog=AdventureWorks2019;"
                    + "Integrated Security=true;";

                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT SalesOrderID, ContactID, OrderDate, OnlineOrderFlag, " +
                    "TotalDue, SalesOrderNumber, Status, ShipToAddressID, BillToAddressID " +
                    "FROM Sales.SalesOrderHeader " +
                    "WHERE DATEPART(YEAR, OrderDate) = @year; " +

                    "SELECT d.SalesOrderID, d.SalesOrderDetailID, d.OrderQty, " +
                    "d.ProductID, d.UnitPrice " +
                    "FROM Sales.SalesOrderDetail d " +
                    "INNER JOIN Sales.SalesOrderHeader h " +
                    "ON d.SalesOrderID = h.SalesOrderID  " +
                    "WHERE DATEPART(YEAR, OrderDate) = @year; " +

                    "SELECT p.ProductID, p.Name, p.ProductNumber, p.MakeFlag, " +
                    "p.Color, p.ListPrice, p.Size, p.Class, p.Style, p.Weight  " +
                    "FROM Production.Product p; " +

                    "SELECT DISTINCT a.AddressID, a.AddressLine1, a.AddressLine2, " +
                    "a.City, a.StateProvinceID, a.PostalCode " +
                    "FROM Person.Address a " +
                    "INNER JOIN Sales.SalesOrderHeader h " +
                    "ON  a.AddressID = h.ShipToAddressID OR a.AddressID = h.BillToAddressID " +
                    "WHERE DATEPART(YEAR, OrderDate) = @year; " +

                    "SELECT DISTINCT c.ContactID, c.Title, c.FirstName, " +
                    "c.LastName, c.EmailAddress, c.Phone " +
                    "FROM Person.Contact c " +
                    "INNER JOIN Sales.SalesOrderHeader h " +
                    "ON c.ContactID = h.ContactID " +
                    "WHERE DATEPART(YEAR, OrderDate) = @year;",
                connectionString);

                // Add table mappings.
                da.SelectCommand.Parameters.AddWithValue("@year", 2002);
                da.TableMappings.Add("Table", "SalesOrderHeader");
                da.TableMappings.Add("Table1", "SalesOrderDetail");
                da.TableMappings.Add("Table2", "Product");
                da.TableMappings.Add("Table3", "Address");
                da.TableMappings.Add("Table4", "Contact");

                // Fill the DataSet.
                da.Fill(ds);

                // Add data relations.
                DataTable orderHeader = ds.Tables["SalesOrderHeader"];
                DataTable orderDetail = ds.Tables["SalesOrderDetail"];
                DataRelation order = new DataRelation("SalesOrderHeaderDetail",
                                         orderHeader.Columns["SalesOrderID"],
                                         orderDetail.Columns["SalesOrderID"], true);
                ds.Relations.Add(order);

                DataTable contact = ds.Tables["Contact"];
                DataTable orderHeader2 = ds.Tables["SalesOrderHeader"];
                DataRelation orderContact = new DataRelation("SalesOrderContact",
                                                contact.Columns["ContactID"],
                                                orderHeader2.Columns["ContactID"], true);
                ds.Relations.Add(orderContact);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL exception occurred: " + ex.Message);
            }
        }
    }
}
