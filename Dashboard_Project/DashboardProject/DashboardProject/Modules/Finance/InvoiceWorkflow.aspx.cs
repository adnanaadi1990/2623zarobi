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
using System.Drawing;
using ITLDashboard.Classes;
using DashboardProject;
namespace DashboardProject.Modules.Finance
{
    public partial class InvoiceWorkflow : System.Web.UI.Page
    {
        public string PdfPath;
        public string FormID = "203";
        public string FilePath ="";
        public string filename = "";
        public string pathImage = "";
        public string User_ID = "";
        public string UserName = "";
        public string FormName = "";
        public string Form_ID = "";
        public string value = "";
        public string Remarks = "";
        public string TransactionID = "";
        public string FormCode = "";
        public string UserEmail = "";
        public string EmailSubject = "";
        public string EmailBody = "";
        public string SessionUser = "";
        public string DateTimeNow = "";
        public string url = "";
        public string urlMobile = "";

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsEmail = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        ComponentClass obj = new ComponentClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnUpload.Visible = true;
                //ddlEmailApproval.BackColor = System.Drawing.Color.AliceBlue;
                //ddlEmailApproval2nd.BackColor = System.Drawing.Color.AliceBlue;

                //ddlEmailMDA.BackColor = System.Drawing.Color.AliceBlue;
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
                try
                {
                    if (Request.QueryString["TransactionNo"] != null)
                    {
                        txtSAPDNo.Enabled = false;
                        btnDownload.Visible = true;
                        btnSave.Visible = false;
                        btnApproved.Visible = false;
                        btnReject.Visible = false;
                        btnReviewed.Visible = false;
                        btnMDA.Visible = false;
                        dvBrows.Visible = false;
                        btnUpload.Visible = false;
                        divEmail.Visible = false;
                        btnDelete.Visible = false;

                        string a = Request.QueryString["TransactionNo"].ToString();
                        cmd.CommandText = @"select * from tbl_FI_InvoiceWorkflow where TransactionMain = @TNo";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@TNo", a.ToString());
                        adp.SelectCommand = cmd;
                        dt.Clear();
                        adp.Fill(dt);
                        DataTableReader reader = dt.CreateDataReader();
                        while (reader.Read())
                        {
                            lblMaxTransactionNo.Text = reader["TransactionMain"].ToString();
                            lblMaxTransactionID.Text = reader["TransactionID"].ToString();
                            lblFileName.Text = reader["FileName"].ToString();
                            txtSAPDNo.Text = reader["SAPDocNo"].ToString();
                        }
                         GetHarcheyID();
                        GetStatusHierachyCategoryControls();
                        BindsysApplicationStatus();
                        GetDCWDetail();
                        getUserDetail();
                        if (((string)ViewState["HID"]) == "1")
                        {
                            btnApproved.Visible = false;
                            btnReject.Visible = false;
                            btnSave.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnMDA.Visible = false;
                            btnReviewed.Visible = false;
                            btnDelete.Visible = false;
                            dvDetail.Visible = true;
                            btnShowFile.Visible = true;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            btnDownload.Visible = true;
                        }
                        if (((string)ViewState["HID"]) == "2")
                        {
                            btnApproved.Visible = true;
                            btnReject.Visible = true;
                            btnSave.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnMDA.Visible = false;
                            btnReviewed.Visible = false;
                            btnDelete.Visible = false;
                            dvDetail.Visible = true;
                            btnShowFile.Visible = true;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            btnDownload.Visible = true;
                        }
                        if (((string)ViewState["HID"]) == "4")
                        {
                            btnApproved.Visible = false;
                            btnReject.Visible = false;
                            btnSave.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnMDA.Visible = true;
                            btnReviewed.Visible = false;
                            btnDelete.Visible = false;
                            dvDetail.Visible = true;
                            btnShowFile.Visible = true;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            btnDownload.Visible = true;
                            
                        }
                        if (((string)ViewState["HID"]) == "3")
                        {
                            btnApproved.Visible = false;
                            btnSave.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnMDA.Visible = true;
                            btnReviewed.Visible = false;
                            btnDelete.Visible = false;
                            dvDetail.Visible = true;
                            btnPrint.Visible = true;
                            btnShowFile.Visible = true;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            txtSAPDNo.Enabled = true;
                            btnDownload.Visible = true;
                        }
                    }
                    else
                    {
                        GetTransactionID();
                        getUserDetail();
                        BindUser();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void getUserDetail()
        {
            ds = obj.getUserDetail(Session["User_Name"].ToString());
            if (ds.Tables["tbluser_DisplayName"].Rows.Count > 0)
            {
                ViewState["SessionUser"] = ds.Tables["tbluser_DisplayName"].Rows[0]["DisplayName"].ToString();
                //ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            btnDelete.Visible = true;
            lblmessage.Text = "";
            error.Visible = false;
            sucess.Visible = false;
            lblError.Text = "";
            lblUpError.Text = "";
            bool hasfile = fleUpload.HasFile;
            filename = Path.GetFileName(fleUpload.PostedFile.FileName);
            if (fleUpload.FileName == "")
            {
                lblError.Text = "Please select file.";
            }
            else
            {
                string fileExt = Path.GetExtension(fleUpload.PostedFile.FileName);
                if (fileExt != ".pdf")
                {
                    lblError.Text = "Please select proper 'pdf' file.";
                }
                else
                {
                    string character = Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-"));
                    fleUpload.PostedFile.SaveAs(Server.MapPath("~/DashboardDocument/InvoiceWorkflow/" + character.ToString() + "_" + filename));
                    lblFileName.Text = character.ToString() + "_" + filename.ToString();
                    lblmessage.Text = "File uploaded successfully!";

                    lblmessage.Focus();
                    error.Visible = false;
                    lblmessage.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    sucess.Visible = true;
                    lblError.Text = "";
                    btnShowFile.Visible = true;
                    btnUpload.Visible = false;
                }
            }

            //}
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ClearInputss(Page.Controls);
            try
            {

                if (lblFileName.Text == "")
                {
                    lblUpError.Text = "Please Upload any file.";
                    error.Visible = true;
                    sucess.Visible = false;
                    return;
                }
                if (ddlEmailApproval.SelectedValue == "0" && ddlEmailApproval2nd.SelectedValue == "0" && DropDownList1.SelectedValue == "0" && DropDownList2.SelectedValue == "0" && DropDownList3.SelectedValue == "0" && DropDownList4.SelectedValue == "0" && DropDownList5.SelectedValue == "0" && DropDownList6.SelectedValue == "0" && DropDownList7.SelectedValue == "0" && DropDownList8.SelectedValue == "0" && DropDownList9.SelectedValue == "0" && DropDownList10.SelectedValue == "0" && DropDownList11.SelectedValue == "0" && DropDownList12.SelectedValue == "0")
                {
                    lblUpError.Text = "Please select any one Approver.";
                    error.Visible = true;
                    sucess.Visible = false;
                    return;
                }

                if (ddlEmailMDA.SelectedValue == "0")
                {
                    ddlEmailMDA.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Please select Reviwer.";
                    error.Visible = true;
                    sucess.Visible = false;
                    return;
                }

                FilePath = "~/DashboardDocument/InvoiceWorkFlow/" + lblFileName.Text.ToString();
                string Approval = ddlEmailApproval.SelectedValue.Trim() + "," + ddlEmailApproval2nd.SelectedValue.Trim() + "," +
                    DropDownList1.SelectedValue.Trim() + "," + DropDownList2.SelectedValue.Trim() + "," +
                    DropDownList3.SelectedValue.Trim() + "," + DropDownList4.SelectedValue.Trim() + "," +
                    DropDownList5.SelectedValue.Trim() + "," + DropDownList6.SelectedValue.Trim() + "," +
                    DropDownList7.SelectedValue.Trim() + "," + DropDownList8.SelectedValue.Trim() + "," +
                     DropDownList9.SelectedValue.Trim() + "," + DropDownList10.SelectedValue.Trim() + "," +
                      DropDownList11.SelectedValue.Trim() + "," + DropDownList12.SelectedValue.Trim();

                cmd.CommandText = "Exec SP_SYS_Createtbl_FI_InvoiceWorkflow" + " @TransactionMain='" + lblMaxTransactionNo.Text + "', " +
                       " @FileName='" + lblFileName.Text + "', " +
                        " @FilePath='" + FilePath.ToString() + "', " +
                         "@APPROVAL='" + Approval.ToString() + "', " +
                          "@REVIEWER='" + ddlEmailMDA.SelectedValue + "', " +
                          "@Remarks='" + txtRemarksReview.Text + "', " +
                           "@CreatedBy='" + Session["User_Name"].ToString() + "'";

                
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                adp.SelectCommand = cmd;
                adp.Fill(ds, "Message");
                sucess.Visible = true;

                string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
                lblmessage.Text = message + " # " + lblMaxTransactionID.Text;
                EmailWorkSendFirstApproval();

                lblmessage.Focus();
                error.Visible = false;
                lblmessage.Focus();
                Page.MaintainScrollPositionOnPostBack = false;
                ClearCont();
                GetTransactionID();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();

            }

            finally
            {
            }

        }

        private void BindUser()
        {

            cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserApproval where FormName = 'IWF'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlEmailApproval.DataSource = cmd.ExecuteReader();
            ddlEmailApproval.DataTextField = "DisplayName";
            ddlEmailApproval.DataValueField = "user_name";
            ddlEmailApproval.DataBind();

            conn.Close();
            conn.Open();
            ddlEmailApproval2nd.DataSource = cmd.ExecuteReader();
            ddlEmailApproval2nd.DataTextField = "DisplayName";
            ddlEmailApproval2nd.DataValueField = "user_name";
            ddlEmailApproval2nd.DataBind();
            conn.Close();
            conn.Open();
            DropDownList1.DataSource = cmd.ExecuteReader();
            DropDownList1.DataTextField = "DisplayName";
            DropDownList1.DataValueField = "user_name";
            DropDownList1.DataBind();
            conn.Close();
            conn.Open();
            DropDownList2.DataSource = cmd.ExecuteReader();
            DropDownList2.DataTextField = "DisplayName";
            DropDownList2.DataValueField = "user_name";
            DropDownList2.DataBind();
            conn.Close();
            conn.Open();
            DropDownList3.DataSource = cmd.ExecuteReader();
            DropDownList3.DataTextField = "DisplayName";
            DropDownList3.DataValueField = "user_name";
            DropDownList3.DataBind();
            conn.Close();
            conn.Open();
            DropDownList4.DataSource = cmd.ExecuteReader();
            DropDownList4.DataTextField = "DisplayName";
            DropDownList4.DataValueField = "user_name";
            DropDownList4.DataBind();
            conn.Close();
            conn.Open();
            DropDownList5.DataSource = cmd.ExecuteReader();
            DropDownList5.DataTextField = "DisplayName";
            DropDownList5.DataValueField = "user_name";
            DropDownList5.DataBind();
            conn.Close();
            conn.Open();
            DropDownList6.DataSource = cmd.ExecuteReader();
            DropDownList6.DataTextField = "DisplayName";
            DropDownList6.DataValueField = "user_name";
            DropDownList6.DataBind();
            conn.Close();
            conn.Open();
            DropDownList7.DataSource = cmd.ExecuteReader();
            DropDownList7.DataTextField = "DisplayName";
            DropDownList7.DataValueField = "user_name";
            DropDownList7.DataBind();
            conn.Close();
            conn.Open();
            DropDownList8.DataSource = cmd.ExecuteReader();
            DropDownList8.DataTextField = "DisplayName";
            DropDownList8.DataValueField = "user_name";
            DropDownList8.DataBind();
            conn.Close();
            conn.Open();
            DropDownList9.DataSource = cmd.ExecuteReader();
            DropDownList9.DataTextField = "DisplayName";
            DropDownList9.DataValueField = "user_name";
            DropDownList9.DataBind();
            conn.Close();
            conn.Open();
            DropDownList10.DataSource = cmd.ExecuteReader();
            DropDownList10.DataTextField = "DisplayName";
            DropDownList10.DataValueField = "user_name";
            DropDownList10.DataBind();
            conn.Close();
            conn.Open();
            DropDownList11.DataSource = cmd.ExecuteReader();
            DropDownList11.DataTextField = "DisplayName";
            DropDownList11.DataValueField = "user_name";
            DropDownList11.DataBind();
            conn.Close();
            conn.Open();
            DropDownList12.DataSource = cmd.ExecuteReader();
            DropDownList12.DataTextField = "DisplayName";
            DropDownList12.DataValueField = "user_name";
            DropDownList12.DataBind();
            conn.Close();

            cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserReviwer where FormName = 'IWF'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlEmailMDA.DataSource = cmd.ExecuteReader();
            ddlEmailMDA.DataTextField = "DisplayName";
            ddlEmailMDA.DataValueField = "user_name";
            ddlEmailMDA.DataBind();
            conn.Close();

            ddlEmailApproval.Items.Insert(0, new ListItem("------------Select------------", "0"));
            ddlEmailApproval2nd.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList1.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList2.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList3.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList4.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList5.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList6.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList7.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList8.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList9.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList10.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList11.Items.Insert(0, new ListItem("------------Select------------", "0"));
            DropDownList12.Items.Insert(0, new ListItem("------------Select------------", "0"));
            ddlEmailMDA.Items.Insert(0, new ListItem("------------Select------------", "0"));

        }

        private void GetTransactionID()
        {
            ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
            lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
            ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();

        }

        private void ClearCont()
        {
            ddlEmailApproval.SelectedIndex = -1;
            ddlEmailApproval2nd.SelectedIndex = -1;
            ddlEmailMDA.SelectedIndex = -1;
            lblFileName.Text = "";
            FilePath = "";
        }

        private void GetHarcheyID()
        {
            try
            {
                ds = obj.GetHarachyCustomerMaster(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
                dt = ds.Tables["HID"];
                ViewState["HIDDataSet"] = dt;

                if (ds.Tables["HID"].Rows.Count > 0)
                {
                    lblMaxTransactionID.Text = ds.Tables["HID"].Rows[0]["TransactionID"].ToString();
                    ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
                    ViewState["RoughtingUserID"] = ds.Tables["HID"].Rows[0]["RoughtingUserID"].ToString();
                    ViewState["Sequance"] = ds.Tables["HID"].Rows[0]["Sequance"].ToString();
                    ViewState["FormCreatedBy"] = ds.Tables["HID"].Rows[0]["CreatedBy"].ToString();
                    ViewState["SerialNo"] = ds.Tables["HID"].Rows[0]["SerialNo"].ToString();

                    ViewState["Status"] = ds.Tables["HID"].Rows[0]["Status"].ToString();

                }
                else
                {
                    ViewState["HID"] = "1";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "GetHarcheyID" + ex.ToString();
            }
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                EmailWorkApproval();
                ApplicationStatus();
                BindsysApplicationStatus();
                GetStatusHierachyCategoryControls();
            }
            catch (Exception ex)
            {
            }

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRemarksReview.Text == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Remarks should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    txtRemarksReview.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else
                {
                    EmailWorkReject();
                    ClosedFormAfterReject();
                    //   ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                }
            }

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "btnReject_Click" + ex.ToString();
            }
        }
        protected void ClosedFormAfterReject()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdClosedFormAfterReject = new SqlCommand())
                {
                    cmdClosedFormAfterReject.Connection = connection;
                    cmdClosedFormAfterReject.CommandType = CommandType.StoredProcedure;
                    cmdClosedFormAfterReject.CommandText = @"SP_ClosedFormAfterReject";

                    try
                    {
                        connection.Open();
                        cmdClosedFormAfterReject.Parameters.AddWithValue("@TransactionID", lblMaxTransactionID.Text.ToString());
                        cmdClosedFormAfterReject.Parameters.AddWithValue("@FormID", FormID.ToString());
                        cmdClosedFormAfterReject.Parameters.AddWithValue("@SerialNo", ViewState["SerialNo"].ToString());
                        cmdClosedFormAfterReject.Parameters.AddWithValue("@Remarks", txtRemarksReview.Text);
                        cmdClosedFormAfterReject.ExecuteNonQuery();

                    }
                    catch (SqlException e)
                    {
                        lblError.Text = e.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }
        private void GetDCWDetail()
        {
            ds = obj.getIWFDetail(lblMaxTransactionID.Text.ToString());
            grdDetail.DataSource = ds.Tables["tbl_FI_InvoiceWorkflow"];
            grdDetail.DataBind();
            grdDetail.Visible = true;

        }

        private void EmailWorkSendFirstApproval()
        {
            try
            {
                ds = obj.MailForwardUserToApprover(lblMaxTransactionID.Text, FormID.ToString());

                if (ds.Tables["MailForwardUserToApprover"].Rows.Count > 0)
                {
                    DataTableReader reader = ds.Tables["MailForwardUserToApprover"].CreateDataReader();
                    while (reader.Read())
                    {

                        url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                        urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                        TransactionID = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Invoice Workflow Request – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has sent you a Invoice Workflow Request against Form ID #  " + lblMaxTransactionID.Text.ToString() + " for approval. <br><br> Your kind approval is required" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Invoice WorkFlow Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();

                    }

                    ViewState["HID"] = "01";
                }

            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "Email User" + ex.ToString();
            }


        }

        private void EmailWorkApproval()
        {
            string HierachyCategoryStatus = "02";
            ds = obj.MailForwardFormApprover(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
            string Value = ds.Tables["MailForwardFormApprover"].Rows[0]["HierachyCategory"].ToString();
            DataTableReader reader = ds.Tables["MailForwardFormApprover"].CreateDataReader();
            if (ds.Tables["MailForwardFormApprover"].Rows.Count > 0 && Value == "2")
            {
                while (reader.Read())
                {

                    url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                    urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "Invoice Workflow Request – Form ID #  " + lblMaxTransactionID.Text + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " Invoice Workflow Request against Form ID#   " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + "<br><br> You are requested to Approve the Delivery Challan Workflow" +
                    "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                    "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                    "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                    "<br>Invoice Workflow Application <br> Information Systems Dashboard";

                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();

                    lblEmail.Text = "*Delivery Challan Workflow Request against Form ID # " + lblMaxTransactionID.Text + " has been approved by you";
                    ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                    lblEmail.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    Page.MaintainScrollPositionOnPostBack = true;
                    lblEmail.Focus();
                }


            }
            else
            {
                if (ds.Tables["MailForwardFormApprover"].Rows.Count > 0)
                {
                    while (reader.Read())
                    {

                        url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                        urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                        TransactionID = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Invoice Workflow Request – Form ID #  " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " Invoice Workflow Request against Form ID#   " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + "<br><br> You are requested to review the Delivery Challan Workflow information " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Invoice Workflow Approval Application <br> Information Systems Dashboard";

                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                        lblEmail.Text = "*Delivery Challan Workflow Request against Form ID # " + lblMaxTransactionID.Text + " has been approved by you";
                        lblEmail.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        Page.MaintainScrollPositionOnPostBack = true;
                        lblEmail.Focus();
                    }
                }
            }
        }

        private void EmailWorkReject()
        {
            ds = obj.MailForwardToUserOnRejection(lblMaxTransactionID.Text, FormID.ToString());

            if (ds.Tables["MailForwardToUserOnRejection"].Rows.Count > 0)
            {
                DataTableReader reader = ds.Tables["MailForwardToUserOnRejection"].CreateDataReader();
                while (reader.Read())
                {
                    url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                    urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "Invoice Workflow Request – Form ID #  " + lblMaxTransactionID.Text + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " Invoice Workflow Request against Form ID#   " + lblMaxTransactionID.Text.ToString() + " has been rejected by " + ViewState["SessionUser"].ToString() + "<br><br> You are requested to review the Delivery Challan Workflow information " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Invoice Workflow Application <br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();
                    ViewState["Status"] = "00"; // For Status Reject
                    lblEmail.Text = "*Delivery Challan Workflow Request against Form ID # " + lblMaxTransactionID.Text.ToString() + " has been rejected by you";
                    lblEmail.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    Page.MaintainScrollPositionOnPostBack = true;
                }
            }

        }
        private void EmailWorkSendMDA()
        {
            string HierachyCategory = "4";
            string HierachyCategoryStatus = "04"; // Allow based on reqierment if there is No MDA if other wise allow "4"//
            ds = obj.MailForwardToAllFromMDA(lblMaxTransactionID.Text, FormID.ToString(), HierachyCategory.ToString());

            if (ds.Tables["MailForwardToAllFromMDA"].Rows.Count > 0)
            {
                DataTableReader reader = ds.Tables["MailForwardToAllFromMDA"].CreateDataReader();
                while (reader.Read())
                {
                    url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                    urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "SAP Documnet No Created Invoice Workflow Request – Form ID # " + lblMaxTransactionID.Text + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " Invoice Workflow Request against Form ID#   " + lblMaxTransactionID.Text.ToString() + " has been reviewed by " + ViewState["SessionUser"].ToString() +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Invoice Workflow Application <br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();
                    InsertEmailHOD();
                    lblmessage.Text = "Delivery Challan Workflow Request Form ID #  " + lblMaxTransactionID.Text + " has been reviewed by you";

                    lblmessage.ForeColor = System.Drawing.Color.Green;
                    conn.Close();
                    sucess.Visible = true;
                    error.Visible = false;
                    lblmessage.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                    ViewState["Status"] = HierachyCategoryStatus.ToString();
                }

            }
            else
            {

            }
        }

        private void ApplicationStatus()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsert = new SqlCommand())
                {


                    string TransatcionID = "";
                    string HierachyCategory = "";
                    string Status = "";
                    string Remarks = "";
                    if (Request.QueryString["TransactionNo"].ToString() == null)
                    {
                        TransatcionID = ViewState["MaterialMaxID"].ToString();
                        HierachyCategory = "1";
                    }
                    else
                    {
                        TransatcionID = lblMaxTransactionID.Text;
                        HierachyCategory = ViewState["HID"].ToString();
                        Status = ViewState["Status"].ToString();
                        ds.Clear();
                        cmdInsert.CommandText = "";
                        cmdInsert.CommandText = @"SP_SYS_UpdateApplicationStatus";
                        cmdInsert.CommandType = CommandType.StoredProcedure;
                        cmdInsert.Connection = connection;
                        cmdInsert.Parameters.AddWithValue("@FormID", FormID.ToString());
                        cmdInsert.Parameters.AddWithValue("@TransactionID", lblMaxTransactionID.Text.ToString());
                        cmdInsert.Parameters.AddWithValue("@HierachyCategory", HierachyCategory.ToString());
                        cmdInsert.Parameters.AddWithValue("@RoughtingUserID", Session["User_Name"].ToString());
                        cmdInsert.Parameters.AddWithValue("@Status", Status.ToString());
                        cmdInsert.Parameters.AddWithValue("@TransferredTo", "");
                        cmdInsert.Parameters.AddWithValue("@SerialNo", ViewState["SerialNo"]);
                        cmdInsert.Parameters.AddWithValue("@Sequence", ViewState["Sequance"]);
                        cmdInsert.Parameters.AddWithValue("@Remarks", txtRemarksReview.Text);
                        try
                        {
                            connection.Open();
                            cmdInsert.ExecuteNonQuery();

                        }
                        catch (SqlException e)
                        {
                            lblError.Text = e.ToString();
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        protected void btnMDA_Click(object sender, EventArgs e)
        {
            try
            {
                UpddateWorking();
                EmailWorkSendMDA();
                ApplicationStatus();
                BindsysApplicationStatus();
                GetStatusHierachyCategoryControls();
            }
            catch (Exception ex)
            {
            }
        }

        private void GetStatusHierachyCategoryControls()
        {
            try
            {
                ds = obj.GetStatusHierachyCategoryControl(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString(), ViewState["HID"].ToString());
                if (ds.Tables["tbl_SysHierarchyControl"].Rows.Count > 0)
                {
                    ViewState["StatusHierachyCategory"] = ds.Tables["tbl_SysHierarchyControl"].Rows[0]["Status"].ToString();
                }
                if (((string)ViewState["StatusHierachyCategory"]) == "01" || ((string)ViewState["StatusHierachyCategory"]) == "02" || ((string)ViewState["StatusHierachyCategory"]) == "03" || ((string)ViewState["StatusHierachyCategory"]) == "04" || ((string)ViewState["StatusHierachyCategory"]) == "00" || ((string)ViewState["StatusHierachyCategory"]) == "06")
                {
                    btnSave.Enabled = false;
                    btnReject.Attributes.Add("disabled", "true");
                    btnApproved.Enabled = false;
                    btnMDA.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSaveSubmit.Enabled = false;
                    txtRemarksReview.Enabled = false;


                }
            }
            catch (Exception ex)
            {
                lblError.Text = "GetStatusHierachyCategoryControls" + ex.ToString();
            }
        }


        private void BindsysApplicationStatus()
        {
            ds = obj.BindsysApplicationStatus(lblMaxTransactionID.Text, FormID.ToString());
            grdWStatus.DataSource = ds.Tables["BindsysApplicationStatus"];
            grdWStatus.DataBind();
            grdWStatus.Visible = true;
        }

        protected void btnSaveSubmit_Click(object sender, EventArgs e)
        {

        }

        void ClearInputss(ControlCollection ctrlss)
        {
            foreach (Control ctrlsss in ctrlss)
            {
                if (ctrlsss is DropDownList)
                    ((DropDownList)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");

                ClearInputss(ctrlsss.Controls);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            FilePath = "~/DashboardDocument/DeliveryChallanWorkflow/" + lblFileName.Text.ToString();
            string pathDelete = Server.MapPath(FilePath.ToString());
            FileInfo file = new FileInfo(pathDelete);
            if (file.Exists)
            {
                file.Delete();
                lblmessage.Text = lblFileName.Text + "  has deleted successfully!";
                sucess.Visible = true;
                lblFileName.Text = "";
                btnDelete.Visible = false;
            }
            else
            {
                lblError.Text = "File does not exists";
            }
        }

        protected void btnReviewed_Click(object sender, EventArgs e)
        {

        }

        protected void btnShowFile_Click(object sender, EventArgs e)
        {

            btnPrint.Visible = false;
            string pdfFileToDisplay = "../../DashboardDocument/InvoiceWorkflow/" + lblFileName.Text;
            string pdfFileToDisplay1 = "DashboardDocument/InvoiceWorkflow/" + lblFileName.Text;
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
        protected void InsertEmail()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsertEmail = new SqlCommand())
                {
                    cmdInsertEmail.Connection = connection;
                    cmdInsertEmail.CommandType = CommandType.StoredProcedure;
                    cmdInsertEmail.CommandText = @"SP_InsertEmail";

                    try
                    {
                        connection.Open();
                        cmdInsertEmail.Parameters.AddWithValue("@TransactionID", TransactionID.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@FormCode", FormID.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@UserName", UserName.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@UserEmail", UserEmail.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@EmailSubject", EmailSubject.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@EmailBody", EmailBody.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@SessionUser", SessionUser.ToString());
                        cmdInsertEmail.ExecuteNonQuery();

                    }
                    catch (SqlException e)
                    {
                        lblError.Text = e.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }

        protected void InsertEmailHOD()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsertEmail = new SqlCommand())
                {
                    cmdInsertEmail.Connection = connection;
                    cmdInsertEmail.CommandType = CommandType.StoredProcedure;
                    cmdInsertEmail.CommandText = @"SP_InsertEmailHOD";

                    try
                    {
                        //string SplitString = "";
                        //string input = EmailBody.ToString(); ;
                        //SplitString = input.Substring(input.IndexOf(',') + 1);
                        connection.Open();
                        cmdInsertEmail.Parameters.AddWithValue("@TransactionID", TransactionID.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@FormCode", FormID.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@UserName", UserName.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@UserEmail", UserEmail.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@EmailSubject", EmailSubject.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@EmailBody", EmailBody.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@SessionUser", SessionUser.ToString());
                        cmdInsertEmail.Parameters.AddWithValue("@HID", ViewState["HID"].ToString());
                        cmdInsertEmail.ExecuteNonQuery();

                    }
                    catch (SqlException e)
                    {
                        lblError.Text = e.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            FilePath = "~/DashboardDocument/InvoiceWorkFlow/" + lblFileName.Text.ToString();
            string fileName = this.Server.MapPath(FilePath.ToString());
            if (System.IO.File.Exists(fileName))
            {
                string pathDelete = Server.MapPath(FilePath.ToString());
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename= " + lblFileName.Text.ToString() + "");
                Response.TransmitFile(pathDelete.ToString());
                Response.End();
            }
            else
            {
                lblError.Text = "File does not exist";
            }
        }
        protected void UpddateWorking()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand Update_DocNo_tbl_FI_InvoiceWorkflow = new SqlCommand())
                {
                    Update_DocNo_tbl_FI_InvoiceWorkflow.Connection = connection;
                    Update_DocNo_tbl_FI_InvoiceWorkflow.CommandType = CommandType.StoredProcedure;
                    Update_DocNo_tbl_FI_InvoiceWorkflow.CommandText = @"Update_DocNo_tbl_FI_InvoiceWorkflow";

                    try
                    {
                        //string SplitString = "";
                        //string input = EmailBody.ToString(); ;
                        //SplitString = input.Substring(input.IndexOf(',') + 1);
                        connection.Open();
                        Update_DocNo_tbl_FI_InvoiceWorkflow.Parameters.AddWithValue("@TransactionID", lblMaxTransactionID.Text.ToString());
                        Update_DocNo_tbl_FI_InvoiceWorkflow.Parameters.AddWithValue("@SAPDocNo", txtSAPDNo.Text.ToString());
                        Update_DocNo_tbl_FI_InvoiceWorkflow.ExecuteNonQuery();

                    }
                    catch (SqlException e)
                    {
                        lblError.Text = e.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }


    }
}