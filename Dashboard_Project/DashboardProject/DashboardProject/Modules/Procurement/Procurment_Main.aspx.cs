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


namespace DashboardProject.Modules.Procurement
{
    public partial class Procurment_Main : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
       
        ComponentClass_AD obj_AD = new ComponentClass_AD();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User_Name"] == null)
                {
                    Response.Redirect("~/ITLLogin.aspx");

                }
                else
                {
                    ds = obj.getUserDetail(Session["User_Name"].ToString());
                    if (ds.Tables["tbluser_DisplayName"].Rows.Count > 0)
                    {
                        lblUSerName.Text = ds.Tables["tbluser_DisplayName"].Rows[0]["DisplayName"].ToString();
                        //ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
                    }

                }

            }
        }

      
        protected void btnQAF_Click(object sender, ImageClickEventArgs e)
        {
            Session["Application"] = "QAF";
            // Response.Redirect("~/Default.aspx");
            getFormsName();
            if (((string)ViewState["FNAME"]) == "QAF")
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");

            }
        }
        protected void getFormsName()
        {
            ds.Clear();
            ds = obj_AD.AllowForms(Session["User_Name"].ToString(), Session["Application"].ToString());
            string ColumnName = ds.Tables["AllowForm"].Columns[0].ColumnName;
            if (ds.Tables["AllowForm"].Rows.Count > 0)
            {
                if (ColumnName.ToString() != "Restricted")
                {
                    ViewState["FNAME"] = ds.Tables["AllowForm"].Rows[0]["Form_ID"].ToString();
                }
                else
                {
                    if (ds.Tables["AllowForm"].Rows[0]["Restricted"].ToString() == "0")
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/AccessDenied.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }
    }
}