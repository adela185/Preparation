using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace PlayField
{
    public class PlayingWithDataTables
    {
        private static System.Data.DataSet ds;

        private static void MakeDataTables()
        {
            MakeParentTable();
            MakeSecondParentTable();
            MakeChildTable();
            MakeChildDetails();
            MakeDataRelation();
        } 

        private static void ShowTable(DataSet ds, int i)
        {
            DataTable table = ds.Tables[i];

            foreach (DataColumn col in table.Columns)
            {
                Console.Write("{0,-14}", col.ColumnName);
            }
            Console.WriteLine();

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    Console.Write("{0,-14}", row[col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void MakeDataRelation()
        {
            DataColumn parentColumn = ds.Tables["ParentTable"].Columns["id"];
            DataColumn childColumn = ds.Tables["ChildTable"].Columns["ParentID"];
            DataRelation relation = new DataRelation("parent2Child", parentColumn, childColumn);
            ds.Tables["ChildTable"].ParentRelations.Add(relation);

            DataRelation childDetailRelation = ds.Relations.Add("childDetail", 
                ds.Tables["ChildTable"].Columns["ChildID"], 
                ds.Tables["ChildDetails"].Columns["ChildID"]/*, false*/);

            DataRelation childSecondParentRelation = ds.Relations.Add("childSecondParent",
                ds.Tables["SecondParentTable"].Columns["SecondParentID"],
                ds.Tables["ChildDetails"].Columns["SecondParentID"]);

        }

        private static void Show()
        {
            foreach (DataRow row in ds.Tables["ParentTable"].Rows)
            {
                Console.WriteLine("Parent ID: " + row["id"]);
                foreach (DataRow childRow in row.GetChildRows("parent2Child"))
                {
                    Console.WriteLine("  Child ID: " + childRow["ChildID"].ToString());
                    foreach (DataRow detailRow in childRow.GetChildRows("childDetail"))
                    {
                        Console.WriteLine("\tSecondParentItem: " + detailRow.GetParentRow("childSecondParent")["SecondParentID"]);
                        Console.WriteLine("\tQuantity: " + detailRow["Quantity"]);
                    }
                }
                Console.WriteLine();
            }
        }

        private static void MakeSecondParentTable()
        {
            DataTable table = new DataTable("SecondParentTable");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "SecondParentID";
            column.ReadOnly = true;
            column.Unique = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "SecondParentItem";
            column.AutoIncrement = false;
            column.Caption = "Caption: SecondParentItem";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["SecondParentID"];
            table.PrimaryKey = PrimaryKeyColumns;

            ds.Tables.Add(table);

            for (int i = 0; i < 3; i++)
            {
                row = table.NewRow();
                row["SecondParentID"] = i;
                row["SecondParentItem"] = $"SecondParentItem {i}";
                table.Rows.Add(row);
            }
        }

        private static void MakeChildDetails()
        {
            DataTable table = new DataTable("ChildDetails");
            DataColumn column;
            DataRow row;

            column = new DataColumn("Quantity", Type.GetType("System.Int32"));
            column.ReadOnly = false;
            column.Unique = false;
            column.AutoIncrement = false;
            table.Columns.Add(column);

            column = new DataColumn("ChildID", Type.GetType("System.Int32"));
            column.ReadOnly = false;
            column.Unique = false;
            column.AutoIncrement = false;
            table.Columns.Add(column);

            column = new DataColumn("SecondParentID", Type.GetType("System.Int32"));
            column.ReadOnly = false;
            column.Unique = false;
            column.AutoIncrement = false;
            table.Columns.Add(column);

            ds.Tables.Add(table);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    row = table.NewRow();
                    row["ChildID"] = i;
                    row["SecondParentID"] = j;
                    row["Quantity"] = 1;
                    table.Rows.Add(row);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    row = table.NewRow();
                    row["ChildID"] = i + 5;
                    row["SecondParentID"] = j;
                    row["Quantity"] = j;
                    table.Rows.Add(row);
                }
            }

            for (int i = 0; i < 5; i++)
            {
               row = table.NewRow();
                row["ChildID"] = i + 10;
                row["SecondParentID"] = i < 3 ? i : 1;
                row["Quantity"] = 3;
                table.Rows.Add(row);
            }
        }

        private static void MakeChildTable()
        {
            DataTable table = new DataTable("ChildTable");
            DataColumn column;
            DataRow row;

            column = new DataColumn("ChildID", Type.GetType("System.Int32"));
            column.AutoIncrement = true;
            column.ReadOnly = true;
            column.Unique = true;
            column.Caption = "Caption: ID";
            table.Columns.Add(column);

            column = new DataColumn("ChildItem", Type.GetType("System.String"));
            column.AutoIncrement = false;
            column.Unique = false;
            column.ReadOnly = false;
            column.Caption = "Caption: ChildItem";
            table.Columns.Add(column);

            column = new DataColumn("ParentID", Type.GetType("System.Int32"));
            column.AutoIncrement = false;
            column.Unique = false;
            column.ReadOnly = false;
            column.Caption = "Caption: ParentID";
            table.Columns.Add(column);

            ds.Tables.Add(table);

            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["ChildID"] = i;
                row["ChildItem"] = $"Item: {i}";
                row["ParentID"] = 0;
                table.Rows.Add(row);
            }
            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["ChildID"] = i + 5;
                row["ChildItem"] = $"Item: {i}";
                row["ParentID"] = 1;
                table.Rows.Add(row);
            }
            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["ChildID"] = i + 10;
                row["ChildItem"] = $"Item: {i}";
                row["ParentID"] = 2;
                table.Rows.Add(row);
            }
        }

        private static void MakeParentTable()
        {
            DataTable table = new DataTable("ParentTable");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "id";
            column.ReadOnly = true;
            column.Unique = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "ParentItem";
            column.AutoIncrement = false;
            column.Caption = "Caption: ParentItem";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["id"];
            table.PrimaryKey = PrimaryKeyColumns;

            ds = new DataSet();
            ds.Tables.Add(table);

            for (int i = 0; i <= 2; i++)
            {
                row = table.NewRow();
                row["id"] = i;
                row["ParentItem"] = $"ParentItem {i}";
                table.Rows.Add(row);
            }
        }

        public static void MakeChanges()
        {
            DataRowCollection rows = ds.Tables["ParentTable"].Rows;
            DataRow row = rows[0];
            Console.WriteLine(row["ParentItem"] + " - " + row.RowState.ToString());
            row["ParentItem"] = "I've been changed, and so my row state is modified";
            Console.WriteLine(row["ParentItem", DataRowVersion.Original] + " - " + row.RowState.ToString() + $": {row["ParentItem"]}\n");
        }

        public static void DatasetCopyShowcase()
        {
            DataSet customerDataSet = new DataSet();
            customerDataSet.Tables.Add(new DataTable("Customers"));
            customerDataSet.Tables["Customers"].Columns.Add("Name", typeof(string));
            customerDataSet.Tables["Customers"].Columns.Add("CountryRegion", typeof(string));
            customerDataSet.Tables["Customers"].Rows.Add("Juan", "Spain");
            customerDataSet.Tables["Customers"].Rows.Add("Johann", "Germany");
            customerDataSet.Tables["Customers"].Rows.Add("John", "UK");

            DataSet germanyCustomers = customerDataSet.Clone();

            DataRow[] copyRows =
              customerDataSet.Tables["Customers"].Select("CountryRegion = 'Germany'");

            DataTable customerTable = germanyCustomers.Tables["Customers"];

            foreach (DataRow copyRow in copyRows)
                customerTable.ImportRow(copyRow);

            foreach (DataColumn column in customerTable.Columns)
            {
                Console.Write(column.ColumnName + " ");
            }
            Console.WriteLine();
            foreach (DataRow row in customerTable.Rows)
            {
                foreach (DataColumn column in customerTable.Columns)
                {
                    Console.Write(row[column] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void Main(string[] args)
        {
            MakeDataTables();
            ShowTable(ds, 0);
            ShowTable(ds, 1);
            ds.AcceptChanges();
            MakeChanges();
            ShowTable(ds, 0);
            ds.RejectChanges();
            Console.WriteLine("I'll now \"undo\" my changes, and revert back to normal: ");
            ShowTable(ds, 0);
            Show();
            DatasetCopyShowcase();
            
            Console.ReadLine();
        }
    }
}
