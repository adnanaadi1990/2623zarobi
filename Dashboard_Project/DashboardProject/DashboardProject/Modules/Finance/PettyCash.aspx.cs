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
using Telerik.Web.UI;
using ITLDashboard.Classes;
namespace ITLDashboard.Modules.Finance
{
    public partial class PettyCash : System.Web.UI.Page
    {
        public string PdfPath;
        public string FormID = "201";
        public string FilePath = "";
        public string filename = "";
        public string pathImage = "";

        public string TransatcionID = "";
        public string HierachyCategory = "";
        public string Status = "";
        public string Remarks = "";
        public string SessionID = "";

        public string TransactionIDEmail = "";
        public string FormCode = "";
        public string UserName = "";
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

                dvCheque.Visible = false;
                ddlEmailApproval.BackColor = System.Drawing.Color.AliceBlue;
                ddlEmailApproval2nd.BackColor = System.Drawing.Color.AliceBlue;
                ddlEmailApproval3rd.BackColor = System.Drawing.Color.AliceBlue;
                ddlEmailMDA.BackColor = System.Drawing.Color.AliceBlue;
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
                    txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                    if (Request.QueryString["TransactionNo"] != null)
                    {
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                        btnShowFile.Visible = true;
                        btnApproved.Visible = false;
                        btnReject.Visible = false;
                        btnPrint.Visible = false;
                        btnMDA.Visible = false;
                        dvBrows.Visible = false;
                        btnUpload.Visible = false;
                        divEmail.Visible = false;
                        btnDelete.Visible = false;
                        DVERROR.Visible = false;
                        txtDescription.Enabled = false;
                        txtRemarksReview.Visible = false;
                        txtRemarksReview.Enabled = false;

                        string a = Request.QueryString["TransactionNo"].ToString();

                        // cmd.CommandText = @"select * from tbl_FI_PettyCash where TransactionMain = @TNo";
                        cmd.CommandText = @" SELECT TransactionMain, TransactionID ,ChequeNo as [ChequeNo],convert(varchar,cast(Amount as money),1) as Amount,Description,FileName as [FileName] ,Replace(CreatedBy, '.' ,' ')  as [CreatedBy]
                                        ,CONVERT(VARCHAR(10),CreatedDateTime,103) as Date FROM tbl_FI_PettyCash where TransactionMain = @TNo";
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
                            txtChequeNo.Text = reader["ChequeNo"].ToString();
                            lblFileName.Text = reader["FileName"].ToString();
                            txtAmount.Text = reader["Amount"].ToString();
                            txtDescription.Text = reader["Description"].ToString();
                        }


                        BindsysApplicationStatus();
                        GetDetailPettyCash();
                        GetHarcheyID();
                        getUserDetail();
                        GetStatusHierachyCategoryControls();
                        if (((string)ViewState["HID"]) == "1")
                        {
                            DVERROR.Visible = true;
                            btnPrint.Visible = true;
                            btnApproved.Visible = false;
                            btnReject.Visible = false;
                            btnSave.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnMDA.Visible = false;
                            //btnShow.Visible = false;
                            btnDelete.Visible = false;
                            btnShowFile.Visible = true;
                            grdDetail.Visible = true;
                            dvFormID.Visible = true;
                            txtRemarksReview.Visible = true;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            dvTransactionNo.Visible = false;
                            //txtRemarksReview.Enabled = false;
                        }
                        if (((string)ViewState["HID"]) == "2")
                        {

                            DVERROR.Visible = true;
                            btnApproved.Visible = true;
                            btnReject.Visible = true;
                            btnSave.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnMDA.Visible = false;
                            btnShowFile.Visible = true;
                            btnDelete.Visible = false;
                            //btnShow.Visible = false;
                            grdDetail.Visible = true;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.Enabled = true;
                            txtRemarksReview.Visible = true;

                        }
                        if (((string)ViewState["HID"]) == "4")
                        {
                            DVERROR.Visible = true;
                            btnApproved.Visible = false;
                            btnReject.Visible = false;
                            btnSave.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnMDA.Visible = true;
                            btnShowFile.Visible = true;
                            btnDelete.Visible = false;
                            dvCheque.Visible = true;
                            grdDetail.Visible = true;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.Enabled = true;
                            txtRemarksReview.Visible = true;
                        }
                        if (((string)ViewState["HID"]) == "3")
                        {
                            btnApproved.Visible = false;
                            btnSave.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnMDA.Visible = true;
                            btnShowFile.Visible = true;
                            btnDelete.Visible = false;
                            dvCheque.Visible = true;
                            grdDetail.Visible = true;
                            DVERROR.Visible = true;
                            btnPrint.Visible = true;
                            dvFormID.Visible = true;
                            btnReject.Visible = true;
                            dvTransactionNo.Visible = false;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.Enabled = true;
                            txtRemarksReview.Visible = true;
                            btnPrint.Visible = true;
                        }
                        if (((string)ViewState["HID"]) == "5")
                        {
                            btnApproved.Visible = false;
                            btnReject.Visible = false;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnMDA.Visible = false;
                            divEmail.Visible = false;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            dvTransactionNo.Visible = false;
                            txtRemarksReview.Visible = false;
                            btnShowFile.Visible = true;
                            grdDetail.Visible = true;
                            ViewState["Status"] = "05";
                            ApplicationStatusSpecific();
                            BindsysApplicationStatus();

                        }
                    }
                    else
                    {
                        DVERROR.Visible = true;
                        GetTransactionMain();
                        BindUser();
                        getUserDetail();
                        txtDescription.BackColor = System.Drawing.Color.AliceBlue;


                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void getUserDetail()
        {
            try
            {
                ds = obj.getUserDetail(Session["User_Name"].ToString());
                if (ds.Tables["tbluser_DisplayName"].Rows.Count > 0)
                {

                    ViewState["SessionUser"] = ds.Tables["tbluser_DisplayName"].Rows[0]["DisplayName"].ToString();
                    ViewState["Department"] = ds.Tables["tbluser_DisplayName"].Rows[0]["Department"].ToString();
                    //ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
                }
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "User Detail" + ex.ToString();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {


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
                    lblError.Text = "Please select only PDF file.";
                }
                else
                {
                    string character = Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-"));
                    fleUpload.PostedFile.SaveAs(Server.MapPath("~/DashboardDocument/PettyCash/" + character.ToString() + "_" + filename));
                    lblFileName.Text = character.ToString() + "_" + filename.ToString();
                    lblmessage.Text = "File uploaded successfully!";


                    lblmessage.Focus();
                    error.Visible = false;
                    lblmessage.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    sucess.Visible = true;
                    btnShowFile.Visible = true;
                    btnUpload.Visible = false;
                    btnDelete.Visible = true;

                }
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            Response.Redirect(url.ToString());
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            sucess.Visible = false;
            lblmessage.Text = "";
            ClearInputss(Page.Controls);
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
                if (lblFileName.Text == "")
                {
                    lblError.Text = "Please Upload any file.";
                    return;
                }
                if (ddlEmailApproval.SelectedValue == "0")
                {
                    ddlEmailApproval.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Please select any S.M.C.S";
                    error.Visible = true;
                    return;
                }

                if (ddlEmailApproval2nd.SelectedValue == "0")
                {
                    ddlEmailApproval2nd.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Please select any C.O.O";
                    error.Visible = true;
                    return;
                }
                if (ddlEmailApproval3rd.SelectedValue == "0")
                {
                    ddlEmailApproval3rd.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Please select any C.F.O";
                    error.Visible = true;
                    return;

                }
                if (ddlEmailMDA.SelectedValue == "0")
                {
                    ddlEmailMDA.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Please select any MDA";
                    error.Visible = true;
                    return;

                }
                if (ddlNotification.SelectedValue == "")
                {
                    ddlNotification.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Please select any Person for Notification";
                    error.Visible = true;
                    return;

                }
                if (txtDescription.Text == "")
                {
                    txtDescription.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Description Box should not be left blank";
                    error.Visible = true;
                    return;

                }
                if (ddlEmailApproval3rd.SelectedValue == "0")
                {
                    txtDescription.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Description Box should not be left blank";
                    error.Visible = true;
                    return;

                }
                string Notification = "";

                for (int i = 0; i <= ddlNotification.Items.Count - 1; i++)
                {
                    if (ddlNotification.Items[i].Selected)
                    {
                        if (Notification == "") { Notification = ddlNotification.Items[i].Value; }
                        else { Notification += "," + ddlNotification.Items[i].Value; }
                    }

                }

                FilePath = "~/DashboardDocument/PettyCash/" + lblFileName.Text.ToString();
                string Approval = ddlEmailApproval.SelectedValue.Trim() + "," + ddlEmailApproval2nd.SelectedValue.Trim() + "," + ddlEmailApproval3rd.SelectedValue.Trim();
                cmd.CommandText = "Exec SP_SYS_CreatePettyCash" + " @TransactionMain='" + lblMaxTransactionNo.Text + "', " +
                        " @FileName='" + lblFileName.Text + "', " +
                        " @Description='" + txtDescription.Text + "', " +
                        " @FilePath='" + FilePath.ToString() + "', " +
                        " @APPROVAL='" + Approval.ToString() + "', " +
                        " @Notification='" + Notification.ToString() + "', " +
                        " @REVIEWER='" + ddlEmailMDA.SelectedValue + "', " +
                        " @CreatedBy='" + Session["User_Name"].ToString() + "', " +
                        " @Remarks = '" + txtRemarksReview.Text.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                adp.SelectCommand = cmd;
                adp.Fill(ds, "Message");
                sucess.Visible = true;

                string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
                lblmessage.Text = message + " # " + lblMaxTransactionID.Text;

                lblmessage.Focus();
                error.Visible = false;
                lblmessage.Focus();
                Page.MaintainScrollPositionOnPostBack = false;
                EmailWorkSendFirstApproval();
                ClearCont();
                lblMaxTransactionID.Text = "";
                GetTransactionMain();
                btnDelete.Visible = false;
                btnShowFile.Visible = true;
                ClearInputs(Page.Controls);
                for (int i = 0; i < ddlNotification.Items.Count; i++)
                {
                    ddlNotification.Items[i].Selected = true;
                }
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
            cmd.CommandText = "SELECT * FROM tbluserApprovalHOD where Designation = 'SMCS' and FormID = 'PC'";
            //cmd.CommandText = "SELECT * FROM tbluser where user_name = 'adnan.yousufzai'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlEmailApproval.DataSource = cmd.ExecuteReader();
            ddlEmailApproval.DataTextField = "DisplayName";
            ddlEmailApproval.DataValueField = "user_name";
            ddlEmailApproval.DataBind();
            // ddlEmailApproval.Items.Insert(0, new ListItem("------Select------", "0"));



            conn.Close();
            cmd.CommandText = "SELECT * FROM tbluser where Designation = 'C.O.O'";
            //cmd.CommandText = "SELECT * FROM tbluser where user_name = 'farrukh.aslam'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlEmailApproval2nd.DataSource = cmd.ExecuteReader();
            ddlEmailApproval2nd.DataTextField = "DisplayName";
            ddlEmailApproval2nd.DataValueField = "user_name";
            ddlEmailApproval2nd.DataBind();
            // ddlEmailApproval2nd.Items.Insert(0, new ListItem("------Select------", "0"));


            conn.Close();

            cmd.CommandText = "SELECT * FROM tbluser where Designation = 'Cheif Accountant'";
         //   cmd.CommandText = "SELECT * FROM tbluser where user_name = 'test.two'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlEmailApproval3rd.DataSource = cmd.ExecuteReader();
            ddlEmailApproval3rd.DataTextField = "DisplayName";
            ddlEmailApproval3rd.DataValueField = "user_name";
            ddlEmailApproval3rd.DataBind();
            //ddlEmailApproval3rd.Items.Insert(0, new ListItem("------Select------", "0"));

            conn.Close();


            cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserReviwer where FormName = 'PC'";
            //cmd.CommandText = "SELECT * FROM tbluser where user_name = 'abdul.qadir'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlEmailMDA.DataSource = cmd.ExecuteReader();
            ddlEmailMDA.DataTextField = "DisplayName";
            ddlEmailMDA.DataValueField = "user_name";
            ddlEmailMDA.DataBind();
            conn.Close();
            //  ddlEmailMDA.Items.Insert(0, new ListItem("------Select------", "0"));

           cmd.CommandText = " SELECT user_name,DisplayName FROM tbl_EmailToSpecificPerson where FormID = '201'";
            //cmd.CommandText = "SELECT * FROM tbluser where user_name = 'abdul.qadir'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlNotification.DataSource = cmd.ExecuteReader();
            ddlNotification.DataTextField = "DisplayName";
            ddlNotification.DataValueField = "user_name";
            ddlNotification.DataBind();
            conn.Close();
            for (int i = 0; i < ddlNotification.Items.Count; i++)
            {
                ddlNotification.Items[i].Selected = true;
            }
        }

        private void GetTransactionMain()
        {
            ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
            lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
            ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
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
                btnMDA.Enabled = false;
                btnCancel.Enabled = false;
                btnSaveSubmit.Enabled = false;

            }
        }

        private void GetTransactionID()
        {
            try
            {
                ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
                lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
                ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();

                ds = obj.GetTransactionMax();
                lblMaxTransactionID.Text = ds.Tables["MaterialMasterTrID"].Rows[0]["TransactionID"].ToString();
            }
            catch (Exception ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "Transaction ID" + ex.ToString();
            }
        }

        private void GetDetailPettyCash()
        {
            ds = obj.getPettyCashDetail(lblMaxTransactionID.Text.ToString());
            grdDetail.DataSource = ds.Tables["tblPettyCash"];
            grdDetail.DataBind();

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
                lblError.Text = "Approver" + ex.ToString();
            }
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
                        TransactionIDEmail = reader["TransactionID"].ToString(); 
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Petty Cash Request – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has sent you a Petty Cash Request against  Form ID # " + lblMaxTransactionID.Text + " for approval. <br><br> Your kind approval is required on the following URL:" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Finance Petty Cash Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                    }
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
                DataTableReader reader = ds.Tables["MailForwardFormApprover"].CreateDataReader();
                if (ds.Tables["MailForwardFormApprover"].Rows.Count > 0 && Value == "2")
                {
                    while (reader.Read())
                    {
                        url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                        urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                        TransactionIDEmail = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Petty Cash Request – Form ID #  " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + "Petty Cash Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr. " + ViewState["SessionUser"].ToString() + " <br><br> You are requested to Approve the Petty Cash Request on the following URL: " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Finance Petty Cash Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblEmail.Text = "Petty Cash Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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

                            url = Request.Url.ToString();
                            TransactionIDEmail = reader["TransactionID"].ToString();
                            FormCode = reader["FormID"].ToString();
                            UserName = reader["user_name"].ToString();
                            UserEmail = reader["user_email"].ToString();
                            EmailSubject = "Petty Cash Request – Form ID #  " + lblMaxTransactionID.Text.ToString() + "";
                            EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> Petty Cash Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr. " + ViewState["SessionUser"].ToString().Replace(".", " ") + " <br><br> You are requested to create the Petty Cash information on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                                 "<br>Finance Petty Cash Application<br> Information Systems Dashboard";
                            SessionUser = Session["User_Name"].ToString();
                            DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            InsertEmail();
                            ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                            lblEmail.Text = "Petty Cash Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
                            lblEmail.Focus();
                            Page.MaintainScrollPositionOnPostBack = false;
                            Page.MaintainScrollPositionOnPostBack = true;
                            lblEmail.Focus();
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
            string HierachyCategoryStatus = "00";
            if (ds.Tables["MailForwardToUserOnRejection"].Rows.Count > 0)
            {
                DataTableReader reader = ds.Tables["MailForwardToUserOnRejection"].CreateDataReader();
                while (reader.Read())
                {
                    url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                    urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                    TransactionIDEmail = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "Petty Cash Request – Form ID #  " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + "Petty Cash Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been disapproved by  " + Session["User_Name"].ToString().Replace(".", " ") + " <br><br> The reason of rejection is given below you can review your form on following url:" +
                    "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                    "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                    "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                    "<br> <br> <br>Reject Remarks: " + txtRemarksReview.Text +
                    "<br>Finance Petty Cash Application<br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();
                    ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                    lblEmail.Text = "Petty Cash Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been rejected by you";
                    lblEmail.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    Page.MaintainScrollPositionOnPostBack = true;
                    lblEmail.Focus();
                }

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
                    DataTableReader reader = ds.Tables["MailForwardToAllFromMDA"].CreateDataReader();
                    while (reader.Read())
                    {

                        url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                        urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                        TransactionIDEmail = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Petty Cash Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + "Cheque has been  created by " + ViewState["SessionUser"].ToString().Replace(".", " ") + "  against  Petty Cash Request Form ID # " + lblMaxTransactionID.Text.ToString() + " <br><br> Cheque No : " + txtChequeNo.Text.ToString() + "<br> Amount : " + txtAmount.Text.ToString() +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Finance Petty Cash Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                    }
                }

            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "Email MDA" + ex.ToString();
            }
        }

        private void EmailWorkSendSpecificPerson()
        {
            try
            {
                string HierachyCategory = "5";
                string HierachyCategoryStatus = "05"; // Allow based on reqierment if there is No MDA if other wise allow "4"//
                ds.Clear();
                ds = obj.MailForwardToSpecificPerson(FormID.ToString());

                if (ds.Tables["tbl_EmailToSpecificPerson"].Rows.Count > 0)
                {
                    DataTableReader reader = ds.Tables["tbl_EmailToSpecificPerson"].CreateDataReader();
                    while (reader.Read())
                    {
                        url = Request.Url.ToString();
                        TransactionIDEmail = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Petty Cash Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName + ",<br> <br>  Cheque has been  created by " + ViewState["SessionUser"].ToString().Replace(".", " ") + "  against  Petty Cash Request Form ID # " + lblMaxTransactionID.Text.ToString() + " <br><br> Cheque No : " + txtChequeNo.Text.ToString() + "<br> Amount : " + txtAmount.Text.ToString() + " <br><br> The form can be reviewed at the following URL:<br> <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>  This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br>" +
                          "<br>Finance Petty Cash Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
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
        private void ApplicationStatusSpecific()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "";
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
                command.CommandText = "";
                command.CommandText = @"SP_SYS_UpdateApplicationStatusSpecific";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                command.Parameters.AddWithValue("@FormID", FormID.ToString());
                command.Parameters.AddWithValue("@TransactionID", lblMaxTransactionID.Text.ToString());
                command.Parameters.AddWithValue("@HierachyCategory", HierachyCategory.ToString());
                command.Parameters.AddWithValue("@RoughtingUserID", Session["User_Name"].ToString());
                command.Parameters.AddWithValue("@Status", Status.ToString());
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        protected void btnMDA_Click(object sender, EventArgs e)
        {
            try
            {
                txtChequeNo.BackColor = System.Drawing.Color.White;
                txtAmount.BackColor = System.Drawing.Color.White;
                txtChequeNo.ForeColor = System.Drawing.Color.Black;
                txtAmount.ForeColor = System.Drawing.Color.Black;
                if (txtChequeNo.Text == "")
                {

                    lblError.Text = "";
                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "Cheque No should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtChequeNo.BackColor = System.Drawing.Color.Red;
                    txtChequeNo.ForeColor = System.Drawing.Color.White;
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else if (txtAmount.Text == "")
                {

                    lblError.Text = "";
                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "Amount should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtAmount.BackColor = System.Drawing.Color.Red;
                    txtAmount.ForeColor = System.Drawing.Color.White;
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    UpdateWorking();
                    GetDetailPettyCash();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "";
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
                        cmdInsertEmail.Parameters.AddWithValue("@TransactionID", TransactionIDEmail.ToString());
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
        private void BindsysApplicationStatus()
        {
            ds = obj.BindsysApplicationStatus(lblMaxTransactionID.Text, FormID.ToString());
            grdWStatus.DataSource = ds.Tables["BindsysApplicationStatus"];
            grdWStatus.DataBind();
            grdWStatus.Visible = true;
        }

        void ClearInputss(ControlCollection ctrlss)
        {
            foreach (Control ctrlsss in ctrlss)
            {
                if (ctrlsss is DropDownList)
                    ((DropDownList)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                if (ctrlsss is TextBox)
                    ((TextBox)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                ClearInputss(ctrlsss.Controls);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            FilePath = "~/DashboardDocument/PettyCash/" + lblFileName.Text.ToString();
            string pathDelete = Server.MapPath(FilePath.ToString());
            FileInfo file = new FileInfo(pathDelete);
            if (file.Exists)
            {
                file.Delete();
                lblmessage.Text = lblFileName.Text + "  has deleted successfully!";
                sucess.Visible = true;
                lblFileName.Text = "";
                btnDelete.Visible = false;
                btnUpload.Visible = true;
                btnDelete.Visible = false;
            }
            else
            {
                lblError.Text = "File does not exists";
            }
        }

        protected void btnShowFile_Click(object sender, EventArgs e)
        {
            int x = (Request.Browser.ScreenPixelsWidth) * 2 - 100;
            
            lblError.Text = "";
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
            else
            {
                lblError.Text = "File Cannot display or may be deleted.";
            }

        }
        private void UpdateWorking()
        {
            cmd.CommandText = @"update tbl_FI_PettyCash set ChequeNo = @CNo,Amount = @Amount
                               where TransactionID = @TransID ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@CNo", txtChequeNo.Text);
            cmd.Parameters.AddWithValue("@TransID", lblMaxTransactionID.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text.ToString().Replace(",", ""));
            conn.Open();
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                EmailWorkSendMDA();
                ApplicationStatus();
                BindsysApplicationStatus();
                //  EmailWorkSendSpecificPerson();
                InsertEmailHOD();
                GetStatusHierachyCategoryControls();


                lblmessage.Text = "Cheque No " + txtChequeNo.Text + " has been issued against  New Petty Cash Request Form ID #  " + lblMaxTransactionID.Text + " ";
                lblmessage.ForeColor = System.Drawing.Color.Green;
                conn.Close();
                sucess.Visible = true;
                error.Visible = false;
                lblmessage.Focus();
                Page.MaintainScrollPositionOnPostBack = false;
                btnPrint.Visible = true;
                dvCheque.Visible = true;
                GetDetailPettyCash();
            }
        }

        private void insertSpecificPerson()
        {
            conn.Close();
            cmd.CommandText = "SP_InsertsysApplicationStatusSpecificPerson";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TransactionID", lblMaxTransactionID.Text);
            cmd.Parameters.AddWithValue("@HierachyCategory", ViewState["HierachyCategory"].ToString());
            cmd.Parameters.AddWithValue("@RoughtingUserID", ViewState["HID"].ToString());
            cmd.Parameters.AddWithValue("@Status", ViewState["Status"].ToString());
            cmd.Parameters.AddWithValue("@Remarks", "Notification");
            cmd.Parameters.AddWithValue("@FormID", FormID.ToString());

            conn.Open();
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {

            }
        }
        protected void btnReject_Click1(object sender, EventArgs e)
        {

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
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Reject" + ex.ToString();
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

        private void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();
                else if (ctrl is ListBox)
                    ((ListBox)ctrl).ClearSelection();


                ClearInputs(ctrl.Controls);
            }
            Page.MaintainScrollPositionOnPostBack = false;
        }

        protected void InsertEmail()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsertEmail = new SqlCommand())
                {
                    cmdInsertEmail.Connection = connection;
                    cmdInsertEmail.CommandType = CommandType.Text;
                    cmdInsertEmail.CommandText = @"INSERT INTO tblEmailContentSending
           (TransactionID,FormCode,UserName,UserEmail,EmailSubject,EmailBody,DateTime,SessionUser) VALUES ('" + TransactionIDEmail.ToString() + "','" + FormCode.ToString() + "','" + UserName.ToString() + "','" + UserEmail.ToString() + "','" + EmailSubject.ToString() + "','" + EmailBody.ToString() + "','" + DateTimeNow.ToString() + "','" + SessionUser.ToString() + "')";

                    try
                    {
                        connection.Open();
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

        protected void btnSaveSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}