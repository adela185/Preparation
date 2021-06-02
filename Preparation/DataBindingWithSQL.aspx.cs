using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class DataBindingWithSQL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindDataToGridView();
        }

        protected void BindDataToGridView()
        {
            var connectionFromConfiguration = WebConfigurationManager.ConnectionStrings["DBConnection"];

            using(SqlConnection dbConnection = new SqlConnection(connectionFromConfiguration.ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    SqlCommand command = new SqlCommand("Select colorId, name, hex " +
                                                        "From Color " +
                                                        "Order By colorId", dbConnection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        gvColors.DataSource = dataSet;
                        gvColors.DataBind();
                        Session["gvColors"] = dataSet;
                    }
                }
                catch (SqlException ex)
                {
                    ltError.Text = "Error: " + ex.Message;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }

        protected void gvColors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ltError.Text = string.Empty;
            GridViewRow gvRow = (GridViewRow)gvColors.Rows[e.RowIndex];
            HiddenField hdnColorId = (HiddenField)gvRow.FindControl("hdnColorId");

            var connecitonFromConfiguration = WebConfigurationManager.ConnectionStrings["DBConnection"];

            using (SqlConnection dbConnection = new SqlConnection(connecitonFromConfiguration.ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    DataSet ds = Session["gvColors"] as DataSet;
                    int id = (int)ds.Tables[0].Rows[e.RowIndex].ItemArray[0];
                    SqlCommand sqlCommand = new SqlCommand("Delete From Color Where colorId = @colorID", dbConnection);
                    SqlParameter param = new SqlParameter("colorId", id);
                    sqlCommand.Parameters.Add(param);
                    sqlCommand.ExecuteNonQuery();

                    gvColors.EditIndex = -1;
                    BindDataToGridView();
                }
                catch (SqlException ex)
                {
                    ltError.Text = "Error: " + ex.Message;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }

        protected void gvColors_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ltError.Text = string.Empty;
            gvColors.EditIndex = e.NewEditIndex;
            BindDataToGridView();
        }

        protected void gvColors_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvColors.EditIndex = -1;
            BindDataToGridView();
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            var connecitonFromConfiguration = WebConfigurationManager.ConnectionStrings["DBConnection"];

            using (SqlConnection dbConnection = new SqlConnection(connecitonFromConfiguration.ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    SqlCommand command = new SqlCommand("Insert Into Color (name, hex)" +
                                                        "Values ('', '')", dbConnection);
                    command.ExecuteNonQuery();
                    BindDataToGridView();
                }
                catch (SqlException ex)
                {
                    ltError.Text = "Error: " + ex.Message;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }

        protected void gvColors_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ltError.Text = string.Empty;
            GridViewRow gvRow = (GridViewRow)gvColors.Rows[e.RowIndex];
            HiddenField hdnColorId = (HiddenField)gvRow.FindControl("hdnColorId");
            TextBox txtName = (TextBox)gvRow.Cells[1].Controls[0];
            TextBox txtHex = (TextBox)gvRow.Cells[2].Controls[0];

            var connecitonFromConfiguration = WebConfigurationManager.ConnectionStrings["DBConnection"];

            using(SqlConnection dbConnection = new SqlConnection(connecitonFromConfiguration.ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    string sql = string.Format("Update Color " +
                                               "Set name='{0}', hex='{1}' " +
                                               "Where colorId={2}", txtName.Text, txtHex.Text, hdnColorId.Value);
                    SqlCommand command = new SqlCommand(sql, dbConnection);
                    command.ExecuteNonQuery();
                    gvColors.EditIndex = -1;
                    BindDataToGridView();
                }
                catch(SqlException ex)
                {
                    ltError.Text = "Error: " + ex.Message;
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
    }
}