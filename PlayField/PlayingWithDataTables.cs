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
                row["ChildItem"] = $"Item: {i + 5}";
                row["ParentID"] = 1;
                table.Rows.Add(row);
            }
            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["ChildID"] = i + 10;
                row["ChildItem"] = $"Item: {i + 10}";
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

        public static void FKConstriants()
        {
            ForeignKeyConstraint parent2ChildFK = new ForeignKeyConstraint("parent2ChildFK", ds.Tables["ParentTable"].Columns["id"], ds.Tables["ChildTable"].Columns["ParentID"]);
            parent2ChildFK.DeleteRule = Rule.Cascade;
            ds.Tables["ChildTable"].Constraints.Add(parent2ChildFK);//Already added via relation with default Rule.None
        }

        public static void RowStatePlaying()
        {
            ds.AcceptChanges();
            //FKConstriants();
            ds.Tables["ParentTable"].ChildRelations["parent2Child"].ChildKeyConstraint.DeleteRule = Rule.Cascade; //Rule.None would strand child rows
            ds.Tables["ParentTable"].ChildRelations["parent2Child"].ChildKeyConstraint.UpdateRule = Rule.Cascade;
            //ds.Tables["ParentTable"].Rows.RemoveAt(2); //Row States for Parent and Child Row States will be marked as deleted
            ds.Tables["ParentTable"].Rows[2].Delete();
            ds.Tables["ParentTable"].ChildRelations["parent2Child"].ChildKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
            ds.Tables["ChildTable"].ChildRelations["childDetail"].ChildKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
            //ds.Tables["ParentTable"].RejectChanges();

            DataRow[] deletedRows = ds.Tables["ParentTable"].Select(null, null, DataViewRowState.Deleted);
            //ds.AcceptChanges();
            foreach (DataColumn column in ds.Tables["ParentTable"].Columns)
                Console.Write("\t{0}", column.ColumnName);

            Console.WriteLine("\tRowState");

            foreach (DataRow row in deletedRows)
            {
                foreach (DataColumn column in ds.Tables["ParentTable"].Columns)
                    Console.Write("\t{0}", row[column, DataRowVersion.Original]);

                Console.WriteLine("\t" + row.RowState);
            }
            Console.WriteLine();

            DataTable table = new DataTable();
            DataTableReader rdr = new DataTableReader(ds.Tables["SecondParentTable"]);
            table.Load(rdr, LoadOption.Upsert);
            //Show();
        }
        
        public static void Edit()
        {
            ds.AcceptChanges();
            ds.Tables["ParentTable"].Rows[2].BeginEdit();
            ds.Tables["ParentTable"].ColumnChanged += new DataColumnChangeEventHandler(OnColumnChanged);
            ds.Tables["ParentTable"].Rows[2]["ParentItem"] = "t";
            ds.Tables["ParentTable"].Rows[2].EndEdit();
        }

        private static void OnColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Column.ColumnName == "ParentItem")
                if (e.ProposedValue.ToString() == "t")
                {
                    Console.WriteLine($"Edit {e.Row["ParentItem", DataRowVersion.Proposed]} can't be blank");
                    e.Row.EndEdit();
                }
        }

        private static void ErrorRows()
        {
            ds.Tables["ParentTable"].RowChanged += new DataRowChangeEventHandler(OnRowChanged);

            for (int i = 5; i < 10; i++)
                ds.Tables["ParentTable"].Rows.Add(new Object[] { i, (i * 100).ToString() });

            if (ds.Tables["ParentTable"].HasErrors)
            {
                Console.WriteLine("Errors in table: " + ds.Tables["ParentTable"].TableName);
                foreach (DataRow row in ds.Tables["ParentTable"].GetErrors())
                {
                    Console.WriteLine($"ErrorID: {row[0]} and ErrorItem: {row[1]}\n");
                }
            }

            ds.Tables["ParentTable"].Columns[0].ReadOnly = false;
            int id = 3;
            if (ds.Tables["ParentTable"].HasErrors)
            {
                foreach (DataRow errRow in ds.Tables["ParentTable"].GetErrors())
                {
                    if (errRow.RowError == "Error")
                    {
                        errRow[0] = id;
                        errRow[1] = "ParentItem " + id++;
                        errRow.RowError = "";
                    }
                    else
                        errRow.RejectChanges();
                }
            }
            ds.AcceptChanges();

            Console.WriteLine();
            foreach (DataRow row in ds.Tables["ParentTable"].Rows)
            {
                Console.WriteLine($"ParentID: {row[0]} and ParentItem: {row[1]}\n");
            }
        }

        private static void OnRowChanged(object sender, DataRowChangeEventArgs e)
        {
            if ((int)e.Row[0] != 3)
                e.Row.RowError = "Error";
        }

        private static void UsingDataReader()
        {
            //DataTableReader rdr = new DataTableReader(ds.Tables["SecondParentTable"]);
            DataTableReader rdr = ds.CreateDataReader();

            do
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            Console.Write(rdr[i] + " ");
                        }
                        Console.WriteLine();
                    }
                } 
                else
                    Console.WriteLine("Empty");
            } while (rdr.NextResult());
        }

        private static void DataViewing()
        {
            DataView dv = new DataView(ds.Tables["ChildDetails"], "SecondParentID = 1", "Quantity", DataViewRowState.CurrentRows);
            WriteView(dv);
            Console.WriteLine("Default:");
            //dv = ds.Tables["ChildDetails"].DefaultView;
            dv.Table.Rows[0]["Quantity"] = 70;
            WriteView(dv);
            //dv.RowStateFilter = DataViewRowState.ModifiedOriginal;
            Console.WriteLine("Original");
            WriteView(dv);
            Console.WriteLine();

            int index = dv.Find(3);
            for (int i = 0; i < dv.Table.Columns.Count; i++)
                Console.Write($"{dv[index][i]}\t");
            Console.WriteLine();

            DataRowView[] foundRows = dv.FindRows(3);
            foreach (DataRowView row in foundRows) 
            { 
                for (int i = 0; i < dv.Table.Columns.Count; i++)
                    Console.Write($"{row[i]}\t");
                Console.WriteLine();
            }

            dv = new DataView(ds.Tables["ParentTable"], null, "id", DataViewRowState.CurrentRows);
            foreach (DataRowView row in dv)
            {
                Console.WriteLine(row["ParentItem"]);
                DataView prodView = row.CreateChildView("parent2Child");
                prodView.Sort = "ChildItem";
                foreach (DataRowView prodRow in prodView)
                {
                    Console.WriteLine($"\t{prodRow["ChildItem"]}");
                }
            }
            Console.WriteLine();

            DataRowView newRowView = dv.AddNew();
            newRowView["id"] = 3;
            newRowView["ParentItem"] = "ParentItem 3";
            newRowView.EndEdit();
            WriteView(dv);

            DataViewManager viewManager = new DataViewManager(ds);
            foreach (DataViewSetting viewSetting in viewManager.DataViewSettings)
            {
                viewSetting.ApplyDefaultSort = true;
            }
            viewManager.DataViewSettings["ParentTable"].Sort = "Parent Item";
        }

        private static void WriteView(DataView dv)
        {
            foreach (DataRowView rowView in dv)
            {
                for(int i = 0; i < dv.Table.Columns.Count; i++)
                    Console.Write($"{rowView[i]}\t");
                Console.WriteLine();
            }
        }

        private static void Querying()
        {
            DataTable table = ds.Tables["ParentTable"];

            IEnumerable<DataRow> query =
                from parent in table.AsEnumerable()
                where parent.Field<int>("id") == 2
                select parent;

            foreach (DataRow row in query)
            {
                Console.WriteLine($"Class: {row.Field<string>("ParentItem")}");
                foreach (DataRow childRow in row.GetChildRows("parent2Child"))
                {
                    Console.WriteLine($"\t{childRow.Field<string>("ChildItem")}");
                }
            }
        }

        public static void Main(string[] args)
        {
            MakeDataTables();
            //ShowTable(ds, 0);
            //ShowTable(ds, 1);
            ds.AcceptChanges();
            //MakeChanges();
            //ShowTable(ds, 0);
            //ds.RejectChanges();
            //Console.WriteLine("I'll now \"undo\" my changes, and revert back to normal: ");
            //ShowTable(ds, 0);
            //Show();
            //DatasetCopyShowcase();

            //RowStatePlaying();

            //Edit();
            //ErrorRows();

            //UsingDataReader();

            //DataViewing();

            Querying();
            
            Console.ReadLine();
        }
    }
}
