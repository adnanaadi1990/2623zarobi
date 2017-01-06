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

namespace DashboardProject.Modules.Annexure
{
    public partial class AssetWriteoffForm : System.Web.UI.Page
    {
        public string FormID = "AWOF501";
        private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
          //  ddlApproval1.BackColor = System.Drawing.Color.AliceBlue;

          //  ddlApproval3.BackColor = System.Drawing.Color.AliceBlue;
            //txtRemarksReview.Visible = false;
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

            if (Request.QueryString["TransactionNo"] != null)
            {
                //getDataWhenQueryStringPass();
                //GetHarcheyID();
                //GetStatusHierachyCategoryControls();
                //BindsysApplicationStatus();
                if (((string)ViewState["HID"]) == "1")
                {
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                    grdDetail.Visible = true;
                    divEmail.Visible = false;
                 //   LinkButton1.Visible = false;
                    btnReject.Visible = false;
                    GridView1.Visible = true;
                    dvFormID.Visible = true;
                    dvTransactionNo.Visible = false;
                    //DisableControls(Page, false);

                }
                if (((string)ViewState["HID"]) == "2")
                {
                    grdDetail.Visible = true;
                    btnApproved.Visible = true;
                    btnReject.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                    divEmail.Visible = false;
                  //  LinkButton1.Visible = true;
                    dvFormID.Visible = true;
                    dvTransactionNo.Visible = false;
                    //ClearInputscolor(Page.Controls);
                }
                if (((string)ViewState["HID"]) == "3")
                {
                    grdDetail.Visible = true;
                    btnReviewed.Visible = true;
                    btnApproved.Visible = false;
                    btnReject.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                    divEmail.Visible = false;
                    //LinkButton1.Visible = true;
                    dvFormID.Visible = true;
                    dvTransactionNo.Visible = false;
                    //ClearInputscolor(Page.Controls);
                }
            }
            else
            {
                //setinitialrow();
                //GetTransactionID();
                //getUser();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {

        }

        protected void btnReviewed_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}