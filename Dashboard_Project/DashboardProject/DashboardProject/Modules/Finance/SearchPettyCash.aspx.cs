using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System;
using System.Collections;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.DirectoryServices;
using System.Security.Principal;
using System.Web.UI.HtmlControls;
using ITLDashboard.Classes;


namespace ITLDashboard.Modules.Finance
{
    public partial class SearchPettyCash : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        public static string FormID = "201";
        protected void Page_Load(object sender, EventArgs e)
        {
            try{
            if (!IsPostBack)
            {
                txtFormID.Text = "";
                lblError.Text = "";
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
            }
            }
            catch (Exception ex)
            {
                lblError.Text = "Page_Load" + ex.ToString();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";

                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = "Exec SP_BindsysApplicationStatus" + " @TransactionID='" + txtFormID.Text + "', " +
            "@FormID ='" + FormID.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                if (dt.Rows.Count < 1)
                {
                    lblError.Text = "No Data Found!";
                    dvFileName.Visible = false;
                    dvDescription.Visible = false;
                    dvShowDoc.Visible = false;
                    grdWStatus.Visible = false;
                    return;
                }

                grdWStatus.DataSource = dt;
                grdWStatus.DataBind();
                dvFileName.Visible = true;
                dvDescription.Visible = true;
                dvShowDoc.Visible = true;
                grdWStatus.Visible = true;
                btnPrint.Visible = true;
                getFileName();
                string fileName = lblFileName.Text.ToString();


            }
            catch (Exception ex)
            {
                lblError.Text = "btnSearch_Click" + ex.ToString();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try{
            string url = HttpContext.Current.Request.Url.ToString();
            Response.Redirect(url.ToString());
            }
            catch (Exception ex)
            {
                lblError.Text = "btnCancel_Click" + ex.ToString();
            }
            }
        protected void getFileName()
        {
            try
            {
                string a = txtFormID.Text;
                cmd.CommandText = @"select * from tbl_FI_PettyCash where TransactionID = @TNo";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@TNo", a.ToString());
                adp.SelectCommand = cmd;
                dt.Clear();
                adp.Fill(dt);
                DataTableReader reader = dt.CreateDataReader();
                while (reader.Read())
                {
                    txtFormID.Text = reader["TransactionID"].ToString();
                    lblFileName.Text = reader["FileName"].ToString();
                    txtDescription.Text = reader["Description"].ToString();
                    dvShowDoc.Visible = true;
                    dvFileName.Visible = true;
                    dvDescription.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "getFileName" + ex.ToString();
            }
        }



        protected void btnShowFile_Click(object sender, EventArgs e)
        {
           try{
            string pdfFileToDisplay = "../../DashboardDocument/PettyCash/" + lblFileName.Text;
            string pdfFileToDisplay1 = "DashboardDocument/PettyCash/" + lblFileName.Text;
            // Create the fully qualified file path...
            string fileName = this.Server.MapPath(pdfFileToDisplay.ToString());

            if (System.IO.File.Exists(fileName))
            {
                // Convert the filename into a URL...
                fileName = this.Request.Url.GetLeftPart(UriPartial.Authority) +
                 this.Request.ApplicationPath + "/" + pdfFileToDisplay1;

                System.Text.StringBuilder javaScript = new System.Text.StringBuilder();

                javaScript.Append("<script language=JavaScript>\n");
                // winFeatures could = position, menubars, etc. Google for more info...
                javaScript.Append("var winFeatures = '';\n");
                javaScript.Append("pdfReportWindow = window.open('" + fileName + "', 'PDFReport', winFeatures);\n");
                javaScript.Append("pdfReportWindow.focus();\n");
                javaScript.Append("\n");
                javaScript.Append("</script>\n");

                this.RegisterStartupScript("PdfReportScript", javaScript.ToString());
            }
           }
           catch (Exception ex)
           {
               lblError.Text = "btnShowFile_Click" + ex.ToString();
           }
        }

            
    }

}