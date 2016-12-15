using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITLDashboard.Modules.Reports
{
    public partial class PendingListByUser : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["User_Name"] == null)
                {
                    HttpContext con = HttpContext.Current;
                    con.Request.Url.ToString();

                    Session["Test"] = con.Request.Url.ToString();

                    //  Response.Redirect("~/SingleLogin.aspx");
                }
                if (Session["User_Name"] == null)
                {
                    Response.Redirect("~/SingleLogin.aspx");
                }
                getFormName();

            }
        }

        protected void getFormName()
        {
            cmd.CommandText = @"select FormIDCode,FormName from tblFormsDetail";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlApplication.DataSource = cmd.ExecuteReader();
            ddlApplication.DataTextField = "FormName";
            ddlApplication.DataValueField = "FormIDCode";
            ddlApplication.DataBind();
            conn.Close();
            ddlApplication.Items.Insert(0, new ListItem("------------Select------------", ""));
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                if (txtToID.Text == "")
                {
                    txtToID.Text = txtfromID.Text;
                }
                DataSet ds = new DataSet();

                ds.Clear();
                //DataTable DT = GetSPResult();
                string query = "";
                query = @"SP_UserPendingListbyUser";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@FormID", ddlApplication.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@TransactionIDFrom", txtfromID.Text);
                cmd.Parameters.AddWithValue("@TransactionIDTO", txtToID.Text);
                cmd.Parameters.AddWithValue("@UserName", Session["User_Name"]);
                ds.Clear();
                Adapter.Fill(ds, "PendingListbyUser");
                if (ds.Tables["PendingListbyUser"].Rows.Count > 0)
                {
                    ddlApplication.SelectedIndex = -1;
                    txtfromID.Text = "";
                    txtToID.Text = "";
                    //rbRptType.SelectedValue = "NormalDays";
                    lblError.Text = "";
                }

                else
                {
                    lblError.Text = "No Data Found!..";
                }

                //   }
            }

            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            Response.Redirect(url.ToString());
        }

        protected void grdData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            DataTable dtpaging = (DataTable)ViewState["paging"];
            grdData.DataSource = dtpaging;   // 6 feb 2014
            grdData.DataBind();
        }
    }
}