﻿using System.Web;
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



namespace ITLDashboard.Modules.SBApp
{
    public partial class SAPBasis_Main : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        FieldValidationCode FIELDV = new FieldValidationCode();
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
                    lblUSerName.Text = Session["User_Name"].ToString();
                }

            }

        }

        protected void lnklogout_Click(object sender, EventArgs e)
        {

            Session.Abandon();
            Response.Redirect("~/ITLLogin.aspx");

        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Session["ProjectId"] = "1";
        }



        protected void getFormsName()
        {
            string ColumnName = "";
            ds = FIELDV.AllowForms(Session["User_Name"].ToString(), Session["Application"].ToString());
            if (ds.Tables["AllowForm"].Columns.Contains("Restricted"))
            {
                ColumnName = ds.Tables["AllowForm"].Columns["Restricted"].ColumnName;
            }
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

        protected void btnUAF_Click(object sender, ImageClickEventArgs e)
        {
            Session["Application"] = "UserAF";

            getFormsName();
            if (((string)ViewState["FNAME"]) == "UserAF")
            {
                //Response.Redirect("PettyCash.aspx");
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }

        protected void btnTRF_Click(object sender, ImageClickEventArgs e)
        {
            Session["Application"] = "TRF";

            getFormsName();
            if (((string)ViewState["FNAME"]) == "TRF")
            {
                //Response.Redirect("PettyCash.aspx");
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }
    }
}