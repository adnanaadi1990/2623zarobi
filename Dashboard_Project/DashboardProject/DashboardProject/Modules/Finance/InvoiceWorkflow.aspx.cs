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
        public string FormID = "202";
        public string FilePath = "";
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
                        url = HttpContext.Current.Request.Url.AbsoluteUri + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                        TransactionID = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Invoice Workflow Request – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> " + ViewState["SessionUser"].ToString() + " has sent you a Invoice Workflow Request  against Form ID # " + lblMaxTransactionID.Text + " for approval. <br><br> Your kind approval is required on the following URL: <br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                    "<br>Invoice Workflow Request Application<br> Information Systems Dashboard";
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
            try
            {
                string HierachyCategoryStatus = "02"; // Allow based on reqierment if there is No MDA if other wise allow "4"//
                ds = obj.MailForwardFormApprover(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
                string Value = ds.Tables["MailForwardFormApprover"].Rows[0]["HierachyCategory"].ToString();
                DataTableReader reader = ds.CreateDataReader();
                if (ds.Tables["MailForwardFormApprover"].Rows.Count > 0 && Value == "2")
                {
                    while (reader.Read())
                    {
                        var to = new MailAddress(reader["user_email"].ToString(),
                                                   reader["user_name"].ToString());
                        ViewState["UserName"] = reader["user_name"].ToString();
                        string aa = Request.CurrentExecutionFilePath;
                        string ab = HttpContext.Current.Request.Url.Authority;
                        string aaa = ab + aa;

                        using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", reader["user_email"].ToString()))
                        {

                            mm.Subject = "Invoice Workflow Request – Form ID #  " + lblMaxTransactionID.Text + "";
                            //,<br> <br>   I have Following request againts " + " TransactionNo " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                            mm.Body = ViewState["UserName"].ToString(); //<a href= " + "" + url.ToString() + "?SMCode=" + txtSMC.Text + ">  " + url.ToString() + "?SMCode=" + txtSMC.Text + "</a>
                            string MeterialCode = lblMaxTransactionID.Text;
                            string url = Request.Url.ToString();
                            mm.Body = "Dear Mr " + "" + ViewState["UserName"] + ",<br> <br> Invoice Workflow Request against Form ID #  " + lblMaxTransactionID.Text + " has been approved by Mr. " + Session["User_Name"].ToString() + " <br><br> You are requested to Approve the Invoice Workflow Request on the following URL: <br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message." +
                            "<br><br>Finance Invoice Workflow Application<br> Information Systems Dashboard";

                            mm.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential NetworkCred = new NetworkCredential("dashboard@internationaltextile.com", "itldashboard$$");
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                            lblEmail.Text = "*Invoice Workflow Request against Form ID # " + lblMaxTransactionID.Text + " has been approved by you";
                            ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                        }

                    }

                }
                else
                {
                    if (ds.Tables["MailForwardFormApprover"].Rows.Count > 0)
                    {
                        while (reader.Read())
                        {

                            var to = new MailAddress(reader["user_email"].ToString(),
                                                       reader["user_name"].ToString());
                            ViewState["UserName"] = reader["user_name"].ToString();
                            string aa = Request.CurrentExecutionFilePath;
                            string ab = HttpContext.Current.Request.Url.Authority;
                            string aaa = ab + aa;

                            using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", reader["user_email"].ToString()))
                            {

                                mm.Subject = "Invoice Workflow Request – Form ID #  " + lblMaxTransactionID.Text + "";
                                //,<br> <br>   I have Following request againts " + " Meterial No " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                                mm.Body = ViewState["UserName"].ToString(); //<a href= " + "" + url.ToString() + "?SMCode=" + txtSMC.Text + ">  " + url.ToString() + "?SMCode=" + txtSMC.Text + "</a>
                                string MeterialCode = lblMaxTransactionID.Text;
                                string url = Request.Url.ToString();
                                mm.Body = "Dear Mr " + "" + ViewState["UserName"] + ",<br> <br> Invoice Workflow Request against Form ID #  " + lblMaxTransactionID.Text + " has been approved by Mr. " + Session["User_Name"].ToString() + " <br><br> You are requested to review the Invoice workflow information on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                                 "<br>Finance Invoice Workflow Application<br> Information Systems Dashboard";
                                //  mm.Body  = "<html><body><div style='border-style:solid;border-width:5px;border-radius: 10px; padding-left: 10px;margin: 20px; font-size: 18px;'> <p style='font-family: Vladimir Script;font-weight: bold; color: #f7d722;font-size: 15px;'>Dear Mr XYZ "+"</p><hr><div width=40%;> <p  style='font-size: 20px;'>Hello</div></body></html>";

                                mm.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential("dashboard@internationaltextile.com", "itldashboard$$");
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                smtp.Send(mm);

                                lblEmail.Text = "*Invoice Workflow Request against Form ID # " + lblMaxTransactionID.Text + " has been approved by you";
                                ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                            }

                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "Approval Email" + ex.ToString();
            }

        }

        private void EmailWorkReject()
        {
            ds = obj.MailForwardToUserOnRejection(lblMaxTransactionID.Text, FormID.ToString());

            if (ds.Tables["MailForwardToUserOnRejection"].Rows.Count > 0)
            {
                DataTableReader reader = ds.CreateDataReader();
                while (reader.Read())
                {
                    var to = new MailAddress(reader["user_email"].ToString(),
                                               reader["user_name"].ToString());
                    ViewState["UserName"] = reader["user_name"].ToString();
                    string aa = Request.CurrentExecutionFilePath;
                    string ab = HttpContext.Current.Request.Url.Authority;
                    string aaa = ab + aa;

                    using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", reader["user_email"].ToString()))
                    {

                        mm.Subject = "Invoice Workflow Request – Form ID #  " + lblMaxTransactionID.Text + "";
                        //,<br> <br>   I have Following request againts " + " TransactionNo " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                        mm.Body = ViewState["UserName"].ToString(); //<a href= " + "" + url.ToString() + "?SMCode=" + txtSMC.Text + ">  " + url.ToString() + "?SMCode=" + txtSMC.Text + "</a>
                        string MeterialCode = lblMaxTransactionID.Text;
                        string url = Request.Url.ToString();
                        mm.Body = "Dear Mr " + "" + ViewState["UserName"] + ",<br> <br> Invoice Workflow Request against Form ID #  " + lblMaxTransactionID.Text + " has been rejected by Mr. " + Session["User_Name"].ToString() + " <br><br> You are requested to Review the Invoice Workflow Request on the following URL: <br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message." +
                        "<br><br>Finance Invoice Workflow Application<br> Information Systems Dashboard";
                        //  mm.Body  = "<html><body><div style='border-style:solid;border-width:5px;border-radius: 10px; padding-left: 10px;margin: 20px; font-size: 18px;'> <p style='font-family: Vladimir Script;font-weight: bold; color: #f7d722;font-size: 15px;'>Dear Mr XYZ "+"</p><hr><div width=40%;> <p  style='font-size: 20px;'>Hello</div></body></html>";

                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("dashboard@internationaltextile.com", "itldashboard$$");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup2();", true);
                        lblEmail.Text = "Invoice Workflow Request against Form ID # " + lblMaxTransactionID.Text + " has been rejected by you";
                    }
                }
                ViewState["Status"] = "00"; // For Status Reject
            }
        }

        private void EmailWorkSendMDA()
        {
            try
            {
                string HierachyCategory = "3";
                string HierachyCategoryStatus = "03"; // Allow based on reqierment if there is No MDA if other wise allow "4"//
                ds = obj.MailForwardToAllFromMDA(lblMaxTransactionID.Text, FormID.ToString(), HierachyCategory.ToString());

                if (ds.Tables["MailForwardToAllFromMDA"].Rows.Count > 0)
                {
                    DataTableReader reader = ds.CreateDataReader();
                    while (reader.Read())
                    {
                        var to = new MailAddress(reader["user_email"].ToString(),
                                                   reader["user_name"].ToString());
                        ViewState["UserName"] = reader["user_name"].ToString();
                        string aa = Request.CurrentExecutionFilePath;
                        string ab = HttpContext.Current.Request.Url.Authority;
                        string aaa = ab + aa;

                        using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", reader["user_email"].ToString()))
                        {
                            mm.Subject = "Invoice Workflow Request – Form ID # " + lblMaxTransactionID.Text + "";
                            //,<br> <br>   I have Following request againts " + " TransactionNo " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                            mm.Body = ViewState["UserName"].ToString(); //<a href= " + "" + url.ToString() + "?SMCode=" + txtSMC.Text + ">  " + url.ToString() + "?SMCode=" + txtSMC.Text + "</a> 
                            string url = Request.Url.ToString();
                            mm.Body = "Dear Mr " + "" + ViewState["UserName"] + ",<br> <br>  Invoice Workflow Request Form ID # " + lblMaxTransactionID.Text + " has been reviewed by reviewer. <br><br> The form can be reviewed at the following URL:<br> <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>  This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br>" +
                            "<br>Finance Invoice Workflow Application<br> Information Systems Dashboard";
                            mm.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential NetworkCred = new NetworkCredential("dashboard@internationaltextile.com", "itldashboard$$");
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            lblEmail.Text = "Invoice Workflow Request Form ID #  " + lblMaxTransactionID.Text + " has been reviewed by you ";
                            lblmessage.Text = "";
                            lblUpError.Text = "";
                            sucess.Visible = false;
                            error.Visible = false;
                            lblEmail.Focus();
                            btnSaveSubmit.Style["visibility"] = "hidden";
                            lblEmail.Focus();
                            Page.MaintainScrollPositionOnPostBack = false;
                            ViewState["Status"] = HierachyCategoryStatus.ToString();
                        }
                    }
                }

            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "Email MDA" + ex.ToString();
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
            ds = obj.GetStatusHierachyCategoryControl(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString(), ViewState["HID"].ToString(), ViewState["SerialNo"].ToString(), ViewState["Status"].ToString());
            if (ds.Tables["tbl_SysHierarchyControl"].Rows.Count > 0)
            {
                ViewState["StatusHierachyCategory"] = ds.Tables["tbl_SysHierarchyControl"].Rows[0]["Status"].ToString();
            }
            if (((string)ViewState["StatusHierachyCategory"]) == "01" || ((string)ViewState["StatusHierachyCategory"]) == "02" || ((string)ViewState["StatusHierachyCategory"]) == "03" || ((string)ViewState["StatusHierachyCategory"]) == "04" || ((string)ViewState["StatusHierachyCategory"]) == "00" || ((string)ViewState["StatusHierachyCategory"]) == "06")
            {
                btnSave.Enabled = false;
                btnReject.Attributes.Add("disabled", "true");
                btnApproved.Enabled = false;
                btnReviewed.Enabled = false;
                btnCancel.Enabled = false;
                btnSaveSubmit.Enabled = false;

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
    }
}