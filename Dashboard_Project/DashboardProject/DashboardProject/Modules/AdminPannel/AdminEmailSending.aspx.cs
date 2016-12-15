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

namespace ITLDashboard.Modules.AdminPannel
{
    public partial class AdminEmailSending : System.Web.UI.Page
    {
        public string FormID = "EmailSF";
        //private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        public int a;

        public string User_ID = "";
        public string UserName = "";
        public string FormName = "";
        public string Form_ID = "";
        public string value = "";



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                getFromName();
                getUser();
            }
        }
        protected void getFromName()
        {
            string strQuery = @"SELECT FormName,FormIDCode from tblFormsDetail";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(ds, "tblFormsDetail");

            ddlFormName.DataTextField = ds.Tables["tblFormsDetail"].Columns["FormName"].ToString(); // text field name of table dispalyed in dropdown
            ddlFormName.DataValueField = ds.Tables["tblFormsDetail"].Columns["FormIDCode"].ToString();             // to retrive specific  textfield name 
            ddlFormName.DataSource = ds.Tables["tblFormsDetail"];      //assigning datasource to the dropdownlist
            ddlFormName.DataBind();  //binding dropdownlist
            ddlFormName.Items.Insert(0, new ListItem("------Select------", ""));
            conn.Close();
        }
        protected void getUser()
        {
            string strQuery = @"Select user_name,user_email,DisplayName from tbluser";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(ds, "tblUser");

            ddlUserName.DataTextField = ds.Tables["tblUser"].Columns["DisplayName"].ToString(); // text field name of table dispalyed in dropdown
            ddlUserName.DataValueField = ds.Tables["tblUser"].Columns["DisplayName"].ToString();             // to retrive specific  textfield name 
            ddlUserName.DataSource = ds.Tables["tblUser"];      //assigning datasource to the dropdownlist
            ddlUserName.DataBind();  //binding dropdownlist
            ddlUserName.Items.Insert(0, new ListItem("------Select------", ""));
            conn.Close();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                getData();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }
        protected void getData()
        {

            cmd.CommandText = @"SELECT * from tblEmailContentSending WHERE (ISNULL(@TransactionID,'')='' OR TransactionID = @TransactionID)
                            AND (ISNULL(@FormName,'')='' OR FormCode = @FormName)
                            AND (ISNULL(@User,'')='' OR UserName = @User)
                            order by TransactionID asc";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TransactionID", txtFormID.Text);
            cmd.Parameters.AddWithValue("@FormName", ddlFormName.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@User", ddlUserName.SelectedValue.ToString());
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            adp.SelectCommand = cmd;
            adp.Fill(ds, "tblEmailContentSending");
            grdData.DataSource = ds.Tables["tblEmailContentSending"];
            grdData.DataBind();
            dt = ds.Tables["tblEmailContentSending"];
            ViewState["paging"] = dt;
            grdData.Visible = true;


        }
        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            DataTable dtpaging = (DataTable)ViewState["paging"];
            grdData.DataSource = dtpaging;   // 6 feb 2014
            grdData.DataBind();
        }
        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                string EmailAddress = "";
                string EmailSubject = "";
                string EmailContant = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = grdData.Rows[rowIndex];

                //Access Cell values.
                EmailAddress = row.Cells[3].Text;
                EmailSubject = row.Cells[4].Text;
                EmailContant = row.Cells[5].Text;
                using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", EmailAddress.ToString()))
                {

                    mm.Subject = EmailSubject.ToString();

                    //,<br> <br>   I have Following request against " + " TransactionNo " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                    mm.Body = EmailContant.ToString();
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.office365.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("dashboard@internationaltextile.com", "Itldash$$");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void grdData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {
                var EmailAddress = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lbluseremail")).Text;
                var EmailSubject = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblEmailSubject")).Text;
                var EmailContant = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblEmailBody")).Text;
                using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", EmailAddress.ToString()))
                {

                    mm.Subject = EmailSubject.ToString();

                    //,<br> <br>   I have Following request against " + " TransactionNo " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                    mm.Body = EmailContant.ToString();
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.office365.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("dashboard@internationaltextile.com", "Itldash$$");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);

                    sucess.Visible = true;
                    lblmessage.Text = "Email send sucessfully!";

                    //EmailWorkSendFirstApproval();
                    lblmessage.Focus();
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlFormName.SelectedIndex = -1;
                    ddlUserName.SelectedIndex = -1;
                    txtFormID.Text = "";

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}