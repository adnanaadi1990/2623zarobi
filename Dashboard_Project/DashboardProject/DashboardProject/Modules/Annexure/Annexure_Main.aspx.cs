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
using ITLDashboard.Classes;

namespace ITLDashboard.Modules.Annexure
{
    public partial class Annexure_Main : System.Web.UI.Page
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



        protected void btnPettyCash_Click(object sender, ImageClickEventArgs e)
        {
            Session["Application"] = "PC";
            getFormsName();
            if (((string)ViewState["FNAME"]) == "PC")
            {
                //Response.Redirect("PettyCash.aspx");
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }
        protected void btnVM_Click(object sender, ImageClickEventArgs e)
        {
            Session["Application"] = "VM";
            getFormsName();
            getFormsName();
            if (((string)ViewState["FNAME"]) == "VM")
            {
                //  Response.Redirect("VendorMaster.aspx");
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");

            }
        }
        protected void btnCM_Click(object sender, ImageClickEventArgs e)
        {
            Session["Application"] = "CM";
            getFormsName();
            if (((string)ViewState["FNAME"]) == "DCW")
            {
                Response.Redirect("DeliveryChallanWorkflow.aspx");
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");

            }
        }


        protected void btnClickedDeliveryChallanWorkflow_Click(object sender, ImageClickEventArgs e)
        {
            Session["Application"] = "DCW";
            getFormsName();
            if (((string)ViewState["FNAME"]) == "DCW")
            {
                //Response.Redirect("DeliveryChallanWorkflow.aspx");
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");

            }
        }

        protected void btnAssetsDisposal_Click(object sender, ImageClickEventArgs e)
        {
            Session["Application"] = "ADF";
            getFormsName();
            if (((string)ViewState["FNAME"]) == "ADF")
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
            ds = FIELDV.AllowForms(Session["User_Name"].ToString(), Session["Application"].ToString());
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


        protected void btnAWOF_Click(object sender, ImageClickEventArgs e)
        {
            Session["Application"] = "AWOF";
            getFormsName();
            if (((string)ViewState["FNAME"]) == "AWOF")
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");

            }
        }
        protected void btnATF_Click(object sender, ImageClickEventArgs e)
        {
            Session["Application"] = "ATFA";
            getFormsName();
            if (((string)ViewState["FNAME"]) == "ATFA")
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");

            }
        }
        protected void btnCreation_Click(object sender, ImageClickEventArgs e)
        {
            Session["Application"] = "ACF";
            getFormsName();
            if (((string)ViewState["FNAME"]) == "ACF")
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");

            }
        }
    }
}