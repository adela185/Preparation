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
            MakeChildTable();
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
            
            Console.ReadLine();
        }
    }
}
