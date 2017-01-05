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
using DashboardProject;

namespace DashboardProject.Modules.Finance
{
    public partial class SearchInvoiceWorkflow : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        public static string FormID = "203";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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
                getFileName();
            }
            catch (Exception ex)
            {
                lblError.Text = "btnSearch_Click" + ex.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //string url = HttpContext.Current.Request.Url.ToString();
                //Response.Redirect(url.ToString());
                string url = Request.Url.ToString();
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
                string Link = "";
                cmd.CommandText = @"select top(1) TransactionMain,TransactionID from tbl_FI_InvoiceWorkflow where TransactionID = @TNo";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@TNo", txtFormID.Text.ToString());
                adp.SelectCommand = cmd;
                ds.Clear();
                adp.Fill(ds, "Data");
                if (ds.Tables["Data"].Rows.Count > 0)
                {
                    dt = ds.Tables["Data"];
                    DataTableReader reader = dt.CreateDataReader();
                    while (reader.Read())
                    {
                        txtFormID.Text = reader["TransactionID"].ToString();
                        Link = reader["TransactionMain"].ToString();

                    }
                    Response.Redirect("~/Modules/Finance/InvoiceWorkflow.aspx?TransactionNo=" + Link.ToString());
                }
                else
                {
                    lblError.Text = "No Data Found!";
                }

            }
            catch (Exception ex)
            {
                lblError.Text = "getFileName" + ex.ToString();
            }
        }
    }
}