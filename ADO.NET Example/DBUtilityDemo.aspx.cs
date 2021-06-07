using ADO.NET_Class_Library_Ex;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADO.NET_Example
{
    public partial class DBUtilityDemo : System.Web.UI.Page
    {
        Dictionary<string, SqlParameter> cmdParameters = new Dictionary<string, SqlParameter>();
        SqlDatabaseUtility dbUtility = new SqlDatabaseUtility();
        TransactionEx transactionEx = new TransactionEx();
        AsyncADO asyncADO = new AsyncADO();
        CancellationTokenSource cancellationToken = new CancellationTokenSource();
        DataSet ds;

        protected /*async*/ void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load();
                //StronglyTypedDataSetLoad();
                //await PageLoadLINQ();
                //await PageLoadReg(); 
            }
        }

        private void Load()
        {
            ds = dbUtility.MergeEx();
            Session["DATASET"] = ds;

            DataView dv = ds.Tables[0].DefaultView;
            dv.Sort = "colorId, name, hex";

            if (ds != null)
                gvColor.DataSource = dv;
            else
                ltReport.Text = "Error: Gridview Datasource null.";

            gvColor.DataBind();
        }

        private void DataBind(DataSet ds)
        {
            if (ds != null)
                gvColor.DataSource = ds;
            else
                ltReport.Text = "Error: Gridview Datasource null.";

            gvColor.DataBind();
        }

        private void StronglyTypedDataSetLoad()
        {
            ColorDataSetTableAdapters.ColorTableAdapter colorTableAdapter = new ColorDataSetTableAdapters.ColorTableAdapter();
            ColorDataSet.ColorDataTable colorDataTable = new ColorDataSet.ColorDataTable();

            colorTableAdapter.Fill(colorDataTable);
            Session["DATATABLE"] = colorDataTable;

            gvColor.DataSource = from color in colorDataTable
                                 select new
                                 {
                                     color.name,
                                     color.hex
                                 };
            gvColor.DataBind();
        }

        private async Task PageLoadLINQ()
        {
            ds = await asyncADO.GetDataAsync("TestDBConnectionString", cmdParameters); //transactionEx.DistributedGetDataAsync("TestDBConnectionString", "TestDBConnectionString", cmdParameters);  //transactionEx.GetData("TestDBConnectionString", cmdParameters);
            ds.Tables[0].TableName = "Color";
            Session["DATASET"] = ds;

            if (ds != null)
            {
                gvColor.DataSource = from row in ds.Tables["Color"].AsEnumerable()
                                     select new Color
                                     {
                                         nm = row["name"].ToString(),
                                         hex = row["hex"].ToString()
                                     };

                gvColor.DataBind();
            }
            else
                ltReport.Text = "Error: GridView DataSource null";
        }

        private async Task PageLoadReg()
        {
            ds = await asyncADO.GetDataAsync("TestDBConnectionString", cmdParameters); //transactionEx.DistributedGetDataAsync("TestDBConnectionString", "TestDBConnectionString", cmdParameters);  //transactionEx.GetData("TestDBConnectionString", cmdParameters);
            Session["DATASET"] = ds;

            if (ds != null)
                gvColor.DataSource = ds;
            else
                ltReport.Text = "Error: Gridview Datasource null.";

            gvColor.DataBind();
        }

        protected async void btnColor_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                cmdParameters["name"] = new SqlParameter("name", txtColor.Text);
                cmdParameters["name"].Size = 50;
                cmdParameters["hex"] = new SqlParameter("hex", txtHex.Text);
                cmdParameters["hex"].SqlDbType = SqlDbType.NChar;
                cmdParameters["hex"].Size = 6;
                ltReport.Text = $"Rows Modified: {dbUtility.ExecuteCommandWithIdentity("TestDBConnectionString", cmdParameters)}"; //asyncADO.InsertColorUsingProviderModel("TestDBConnectionString", cmdParameters); 
                //dbUtility.ExecuteCommand("TestDBConnectionString", "dbo.sp_Color_Add", cmdParameters);

                txtColor.Text = String.Empty;
                txtHex.Text = String.Empty;
                cmdParameters.Clear();

                ds = await asyncADO.GetDataAsync("TestDBConnectionString", cmdParameters);
                Session["DATASET"] = ds;

                if (ds != null)
                    gvColor.DataSource = ds;
                else
                    ltReport.Text = "Error: Gridview Datasource null.";

                gvColor.DataBind();
            }

            
        }

        protected async void btnTestStuff_Click(object sender, EventArgs e)
        {
            //DataAnnotationValidator dAV = new DataAnnotationValidator();
            //dAV.Diagnosis();
            //await transactionEx.UpdateViaAdapter("TestDBConnectionString");
            //transactionEx.UpdateUsingSqlBulkCopyNoXML("TestDBConnectionString", (DataTable)Session["DATATABLE"]);
            transactionEx.CopyOver2DataBase("TestDBConnectionString", "TestDBConnectionString2");
        }

        protected void btnCancelLoad_Click(object sender, EventArgs e)
        {
            cancellationToken.Cancel();
        }

        protected async void gvColor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvColor.Rows[e.RowIndex];
            string id = row.Cells[3].Text;

            await transactionEx.DeleteDBAsync("TestDBConnectionString", id);

            ds = await asyncADO.GetDataAsync("TestDBConnectionString", cmdParameters);
            Session["DATASET"] = ds;

            if (ds != null)
                gvColor.DataSource = ds;
            else
                ltReport.Text = "Error: Gridview Datasource null.";

            gvColor.DataBind();
        }

        protected void btnRefresher_Click(object sender, EventArgs e)
        {
            
        }

        private void GridViewFiltering()
        {
            ds = (DataSet)Session["DATASET"];
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                if (ds != null)
                {
                    gvColor.DataSource = from row in ds.Tables["Color"].AsEnumerable()
                                         select new Color
                                         {
                                             nm = row["name"].ToString(),
                                             hex = row["hex"].ToString()
                                         };

                    gvColor.DataBind();
                }
                else
                    ltReport.Text = "Error: GridView DataSource null";
            }
            else
            {
                if (ds != null)
                {
                    gvColor.DataSource = from row in ds.Tables["Color"].AsEnumerable()
                                         where row["name"].ToString().ToUpper().StartsWith(txtSearch.Text.ToUpper())
                                         select new Color
                                         {
                                             nm = row["name"].ToString(),
                                             hex = row["hex"].ToString()
                                         };

                    gvColor.DataBind();
                }
                else
                    ltReport.Text = "Error: GridView DataSource null";
            }
        }

        private void GridViewFilteringWithStronglyTypedDataSets()
        {
            ColorDataSet.ColorDataTable colorDataTable = (ColorDataSet.ColorDataTable)Session["DATATABLE"];

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                gvColor.DataSource = from color in colorDataTable
                                     select new
                                     {
                                         color.name,
                                         color.hex
                                     };
                gvColor.DataBind();
            }
            else
            {
                gvColor.DataSource = from color in colorDataTable
                                     where color.name.ToUpper().StartsWith(txtSearch.Text.ToUpper())
                                     select new
                                     {
                                         color.name,
                                         color.hex
                                     };
                gvColor.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //GridViewFiltering();
            GridViewFilteringWithStronglyTypedDataSets();
        }

        protected void btnUpdateDB_Click(object sender, EventArgs e)
        {
            ds = (DataSet)Session["DATASET"];
            transactionEx.ProperUpdate(ds);
            ds = transactionEx.GetData("TestDBConnectionString", cmdParameters);
            DataBind(ds);
        }
    }
}