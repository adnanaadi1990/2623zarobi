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

namespace DashboardProject.Modules.HR
{
    public partial class ShortLeave : System.Web.UI.Page
    {
        public string FormID = "SL503";
        private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        public string TransatcionID = "";
        public string li = "";
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
                if (Request.QueryString["TransactionNo"] != null)
                {

                    getDataWhenQueryStringPass();
                    GetHarcheyID();
                    GetStatusHierachyCategoryControls();
                    BindsysApplicationStatus();
                    if (((string)ViewState["HID"]) == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtEmployeeName.Enabled = false;
                        txtEmployeeCardNo.Enabled = false;
                        txtDepartment.Enabled = false;
                        txtDivision.Enabled = false;
                        txtDate.Enabled = false;
                        RBList.Enabled = false;
                        divEmail.Visible = false;
                        ////LinkButton1.Visible = false;
                        btnApproved.Visible = false;
                        btnReject.Visible = false;
                        txtTimeOut.Enabled = false;
                        txtTimeIn.Enabled = false;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                    }
                    if (((string)ViewState["HID"]) == "2")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtEmployeeName.Enabled = false;
                        txtEmployeeCardNo.Enabled = false;
                        txtDepartment.Enabled = false;
                        txtDivision.Enabled = false;
                        txtDate.Enabled = false;
                        RBList.Enabled = false;
                        divEmail.Visible = false;
                        ////LinkButton1.Visible = true;
                        btnApproved.Visible = true;
                        btnReject.Visible = true;
                        txtTimeOut.Enabled = false;
                        txtTimeIn.Enabled = false;
                        dvGK.Visible = true;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                    }
                    if (((string)ViewState["HID"]) == "3")
                    {
                        ClearInputscolor(Page.Controls);
                        txtEmployeeName.Enabled = false;
                        txtEmployeeCardNo.Enabled = false;
                        txtDepartment.Enabled = false;
                        txtDivision.Enabled = false;
                        txtDate.Enabled = false;
                        RBList.Enabled = false;
                        btnApproved.Visible = false;
                        btnReject.Visible = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        btnMDA.Visible = false;
                        divEmail.Visible = false;
                        ////LinkButton1.Visible = false;
                     
                        txtTimeOut.Enabled = false;
                        txtTimeIn.Enabled = false;
                        dvGK.Visible = true;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        ViewState["Status"] = "03";
                        ApplicationStatus();
                        BindsysApplicationStatus();
                    }

                    if (((string)ViewState["HID"]) == "4")
                    {
                        ClearInputscolor(Page.Controls);
                        txtEmployeeName.Enabled = false;
                        txtEmployeeCardNo.Enabled = false;
                        txtDepartment.Enabled = false;
                        txtDivision.Enabled = false;
                        txtDate.Enabled = false;
                        RBList.Enabled = false;
                        btnApproved.Visible = false;
                        btnReject.Visible = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        btnMDA.Visible = false;
                        divEmail.Visible = false;
                        ////LinkButton1.Visible = false;
                   
                        txtTimeOut.Enabled = true;
                        txtTimeIn.Enabled = true;
                        dvGK.Visible = true;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                    }

                }
                else
                {
                    GetTransactionID();
                    getUserHOD();
                    getUser();
                    madatorycolor();
                }
            }

        }
        //---/////////// Buttons Events//////////////////////////
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtEmployeeName.Text == "")
            {
                lblUpError.Text = "Fill all required field!.";
                error.Visible = true;
                txtEmployeeName.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            if (txtEmployeeCardNo.Text == "")
            {
                lblUpError.Text = "Fill all required field!.";
                error.Visible = true;
                txtEmployeeCardNo.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            if (txtDepartment.Text == "")
            {
                lblUpError.Text = "Fill all required field!.";
                error.Visible = true;
                txtDepartment.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            if (txtDivision.Text == "")
            {
                lblUpError.Text = "Fill all required field!.";
                error.Visible = true;
                txtDivision.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            if (txtDate.Text == "")
            {
                lblUpError.Text = "Fill all required field!.";
                error.Visible = true;
                txtDate.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }

            if (RBList.SelectedValue == "")
            {
                lblUpError.Text = "Fill all required field!.";
                error.Visible = true;
                RBList.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }

          
            cmd.CommandText = "";
            cmd.CommandText = "SP_SYS_ShortLeave";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
            cmd.Parameters.AddWithValue("@EmployeeName", txtEmployeeName.Text);
            cmd.Parameters.AddWithValue("@EmployeeCardNo", txtEmployeeCardNo.Text);
            cmd.Parameters.AddWithValue("@Department", txtDepartment.Text);
            cmd.Parameters.AddWithValue("@Division", txtDivision.Text);
            cmd.Parameters.AddWithValue("@Date", txtDate.Text);
            //cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text));
            cmd.Parameters.AddWithValue("@ReasonofLeaving", RBList.Text);



            string final = ViewState["HOD"] + "," + ddlHr.SelectedValue.ToString();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            adp.SelectCommand = cmd;
            adp.Fill(ds, "Message");
            sucess.Visible = true;


            string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
            lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
            lblmessage.Text = message + " # " + lblMaxTransactionID.Text;


            lblmessage.Focus();
            error.Visible = false;
            Page.MaintainScrollPositionOnPostBack = false;
            EmailWorkSendFirstApproval();
            ClearInputs(Page.Controls);

            ClearInputscolor(Page.Controls);
            GetTransactionID();
            madatorycolor();
        }

        protected void btnMDA_Click(object sender, EventArgs e)
        {
            lblEmail.Text = "";
            lblmessage.Text = "";
            lblUpError.Text = "";
            sucess.Visible = true;
            error.Visible = false;
            ApplicationStatus();
            BindsysApplicationStatus();
            GetStatusHierachyCategoryControls();
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {

                EmailWorkSendApprovalFromApproval();
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
            EmailWorkReject();
            //LinkButton1.Attributes.Add("disabled", "true");
            btnReject.Attributes.Add("disabled", "true");
            btnApproved.Attributes.Add("disabled", "true");
            btnMDA.Enabled = false;
            ApplicationStatus();
            BindsysApplicationStatus();
            GetStatusHierachyCategoryControls();
        }

        //protected void btnReviewed_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtTimeOut.BackColor = System.Drawing.Color.White;
        //        txtTimeIn.BackColor = System.Drawing.Color.White;
        //        if (txtTimeOut.Text == "")
        //        {
        //            lblUpError.Text = "Fill all required field!.";
        //            error.Visible = true;
        //            txtTimeOut.BackColor = System.Drawing.Color.Red;
        //            Page.MaintainScrollPositionOnPostBack = false;
        //            return;
        //        }
        //        else if (txtTimeIn.Text == "")
        //        {
        //            lblUpError.Text = "Fill all required field!.";
        //            error.Visible = true;
        //            txtTimeIn.BackColor = System.Drawing.Color.Red;
        //            Page.MaintainScrollPositionOnPostBack = false;
        //            return;
        //        }
        //        else
        //        {
        //            updateFWorking();
        //            EmailWorkSendMDA();

        //            txtTimeOut.Enabled = false;
        //            txtTimeIn.Enabled = false;
        //            //btnReviewed.Attributes.Add("disabled", "true");
        //            //btnReviewed.Enabled = true;
        //            //btnReviewed.Style["visibility"] = "hidden";
        //            ApplicationStatus();
        //            BindsysApplicationStatus();
        //            GetStatusHierachyCategoryControls();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputs(Page.Controls);
        }

        ///////////////////////////////////////////////////Methods//////////////////////////////////////////////////////////
        private void GetStatusHierachyCategoryControls()
        {
            ds = obj.GetStatusHierachyCategory(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
            if (ds.Tables["StatusHierachyCategory"].Rows.Count > 0)
            {
                ViewState["StatusHierachyCategory"] = ds.Tables["StatusHierachyCategory"].Rows[0]["Status"].ToString();
            }
            if (((string)ViewState["StatusHierachyCategory"]) == "01" || ((string)ViewState["StatusHierachyCategory"]) == "02" || ((string)ViewState["StatusHierachyCategory"]) == "03" || ((string)ViewState["StatusHierachyCategory"]) == "04")
            {
                btnSave.Enabled = false;
                btnApproved.Enabled = false;
                //LinkButton1.Attributes.Add("disabled", "true");
                btnCancel.Enabled = false;
                btnMDA.Enabled = false;
            }
        }

        protected void getDataWhenQueryStringPass()
        {
            string TI = Request.QueryString["TransactionNo"].ToString();
            //methodCall();

            cmd.CommandText = "";
            cmd.CommandText = "SELECT * from tbl__HR_ShortLeave where TransactionMain = @TI";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TI", TI.ToString());
            adp.SelectCommand = cmd;
            adp.Fill(ds, "Data");
            lblMaxTransactionID.Text = ds.Tables["Data"].Rows[0]["TransactionID"].ToString();
            txtEmployeeName.Text = ds.Tables["Data"].Rows[0]["EmployeeName"].ToString();
            txtEmployeeCardNo.Text = ds.Tables["Data"].Rows[0]["EmployeeCardNo"].ToString();
            txtDepartment.Text = ds.Tables["Data"].Rows[0]["Department"].ToString();
            txtDivision.Text = ds.Tables["Data"].Rows[0]["Division"].ToString();
            txtDate.Text = ds.Tables["Data"].Rows[0]["Date"].ToString();

            RBList.SelectedValue = ds.Tables["Data"].Rows[0]["ReasonofLeaving"].ToString();
            txtTimeOut.Text = ds.Tables["Data"].Rows[0]["TimeOut"].ToString();
            txtTimeIn.Text = ds.Tables["Data"].Rows[0]["TimeIn"].ToString();


        }

        protected void DisableControls(Control parent, bool State)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is DropDownList)
                {
                    ((DropDownList)(c)).Enabled = State;
                }
                if (c is TextBox)
                {
                    ((TextBox)(c)).Enabled = State;
                }
                if (c is ListBox)
                {
                    ((ListBox)(c)).Enabled = State;
                }
                DisableControls(c, State);

                ClearInputscolor(Page.Controls);

            }
        }

        void ClearInputscolor(ControlCollection ctrlss)
        {
            foreach (Control ctrlsss in ctrlss)
            {
                if (ctrlsss is TextBox)
                    ((TextBox)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                if (ctrlsss is DropDownList)
                    ((DropDownList)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                if (ctrlsss is ListBox)
                    ((ListBox)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                if (ctrlsss is RadioButtonList)
                    ((RadioButtonList)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                ClearInputscolor(ctrlsss.Controls);
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

                ClearInputs(ctrl.Controls);
            }
        }

        protected void madatorycolor()
        {
            txtEmployeeName.BackColor = System.Drawing.Color.AliceBlue;
            txtEmployeeCardNo.BackColor = System.Drawing.Color.AliceBlue;
            txtDepartment.BackColor = System.Drawing.Color.AliceBlue;
            txtDivision.BackColor = System.Drawing.Color.AliceBlue;
            txtDate.BackColor = System.Drawing.Color.AliceBlue;
            ddlHr.BackColor = System.Drawing.Color.AliceBlue;
            ddlCSD.BackColor = System.Drawing.Color.AliceBlue;

        }

        private void GetTransactionID()
        {
            ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
            lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
            ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();

        }

        private void GetHarcheyID()
        {
            ds = obj.GetHarachyPettyCash(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
            if (ds.Tables["HID"].Rows.Count > 0)
            {
                lblMaxTransactionID.Text = ds.Tables["HID"].Rows[0]["TransactionID"].ToString();
                ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
            }
        }

        private void getUser()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdgetData = new SqlCommand())
                {

                    cmdgetData.CommandText = "SELECT user_name,DisplayName,Category FROM tblusermodulecategory where Category = 'Human Resources' or Category ='HR & Compliance' ";
                    cmdgetData.CommandType = CommandType.Text;
                    cmdgetData.Connection = connection;
                    connection.Open();

                    adp.SelectCommand = cmdgetData;
                    adp.Fill(dt);
                    ViewState["tblusermodulecategoryMerchandiser"] = dt;
                    ddlHr.Items.Insert(0, new ListItem("------Select------", "0"));
                    connection.Close();

                    cmdgetData.CommandText = "SELECT user_name,DisplayName FROM tbluserApprovalHOD where FormID = 'ATFA' and Designation = 'CFO'";
                    cmdgetData.CommandType = CommandType.Text;
                    cmdgetData.Connection = connection;
                    connection.Open();

                    ddlCSD.DataSource = cmdgetData.ExecuteReader();
                    ddlCSD.DataTextField = "DisplayName";
                    ddlCSD.DataValueField = "user_name";
                    ddlCSD.DataBind();
                    ddlCSD.Items.Insert(0, new ListItem("------Select------", "0"));
                    connection.Close();
                    ddlCSD.SelectedIndex = 1;
                    ddlHr.SelectedIndex = 1;
                }
            }
        }
        //Getting User Hod Automatically 
        protected void getUserHOD()
        {
            try
            {
                dvemaillbl.Visible = false;
                ds = obj.getUserHOD(Session["User_Name"].ToString());
                ViewState["HOD"] = "";
                if (ds.Tables["getUserHOD"].Rows.Count > 0)
                {
                    ViewState["HOD"] = ds.Tables["getUserHOD"].Rows[0]["HOD"].ToString().Trim();
                    lblHOD.Text = ds.Tables["getUserHOD"].Rows[0]["HODName"].ToString().Trim();
                }

            }
            catch (Exception ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "User HOD" + ex.ToString();
            }
        }

        private void updateFWorking()
        {

            cmd.CommandText = "";
            cmd.CommandText = @"UPdate tbl__HR_ShortLeave
		                     set  TimeOut = @TimeOut,
		                          TimeIn = @TimeIn
                             where TransactionID = @TransactionID";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TimeOut", txtTimeOut.Text);
            cmd.Parameters.AddWithValue("@TimeIn", txtTimeIn.Text);
            cmd.Parameters.AddWithValue("@TransactionID", lblMaxTransactionID.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        ///////////////////////////////////////////////////Email WOrk//////////////////////////////////////////////////////////
        #region Email_Working

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
                        EmailSubject = "Asset WriteOff Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " have sent you a Asset WriteOff Form Request against  Form ID # " + lblMaxTransactionID.Text + " for approval. <br><br> Your kind approval is required on the following URL: " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Assets Application <br> Information Systems Dashboard";
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

        private void EmailWorkReject()
        {
            try
            {
                string HierachyCategoryStatus = "00"; // Allow based on reqierment if there is No MDA if other wise allow "4"//
                ds = obj.MailForwardToUserOnRejection(lblMaxTransactionID.Text, FormID.ToString());

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
                        EmailSubject = "Asset WriteOff Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br>  Your Asset WriteOff Form Request against  Form ID # " + lblMaxTransactionID.Text + " has been disapproved by  " + ViewState["SessionUser"].ToString() + " <br><br> The reason of rejection is given below you can review your form on following url:" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "<br> <br> <br>Reject Remarks: " + txtRemarksReview.Text +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Assets Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblEmail.Text = "Asset WriteOff Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been rejected by you";
                        lblEmail.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        Page.MaintainScrollPositionOnPostBack = true;
                        lblEmail.Focus();
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "Email User" + ex.ToString();
            }
        }

        private void EmailWorkSendToAllfromReviwer()
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
                        EmailSubject = "Asset WriteOff Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br>  Your Asset WriteOff Form Request against  Form ID # " + lblMaxTransactionID.Text + " has been reviewed by  " + ViewState["SessionUser"].ToString() + " <br><br> This form can review on following url:" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Assets Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblmessage.Text = "Asset WriteOff Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been Submit by you";
                        sucess.Visible = true;
                        lblmessage.Focus();
                        error.Visible = false;
                        lblmessage.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                    }

                }
            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "Email User" + ex.ToString();
            }

        }

        private void EmailWorkSendApprovalFromApproval()
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
                        EmailSubject = "Asset WriteOff Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br>  Asset WriteOff Form Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br><br> You are requested to Approve Asset WriteOff Form Request on the following URL: " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Assets Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblEmail.Text = "Asset WriteOff Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
                        lblEmail.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        Page.MaintainScrollPositionOnPostBack = true;
                        lblEmail.Focus();
                        dvemaillbl.Visible = true;
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
                            UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                            EmailSubject = "Asset WriteOff Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                            EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> Asset WriteOff Form Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr. " + ViewState["SessionUser"].ToString().Replace(".", " ") + " <br><br> You are requested to review the Asset WriteOff Form information on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                                 "<br>Assets Application<br> Information Systems Dashboard";
                            SessionUser = Session["User_Name"].ToString();
                            DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            InsertEmail();
                            ViewState["Status"] = HierachyCategoryStatus.ToString();
                            lblEmail.Text = "Asset WriteOff Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
                            lblEmail.Focus();
                            Page.MaintainScrollPositionOnPostBack = false;
                            Page.MaintainScrollPositionOnPostBack = true;
                            lblEmail.Focus();
                            dvemaillbl.Visible = true;

                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "Approval Email" + ex.ToString();
            }
        }

        #endregion

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
        private void getUserDetail()
        {
            ds = obj.getUserDetail(Session["User_Name"].ToString());
            if (ds.Tables["tbluser_DisplayName"].Rows.Count > 0)
            {
                ViewState["SessionUser"] = ds.Tables["tbluser_DisplayName"].Rows[0]["DisplayName"].ToString();
                //ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
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
        private void ApplicationStatus()
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

            }
            ds = obj.InsertsysApplicationStatus(FormID.ToString(), TransatcionID.ToString(), ViewState["HID"].ToString(), Session["User_Name"].ToString(), Status.ToString(), Remarks.ToString().Trim());
        }
        private void BindsysApplicationStatus()
        {
            ds = obj.BindsysApplicationStatus(lblMaxTransactionID.Text, FormID.ToString());
            grdWStatus.DataSource = ds.Tables["BindsysApplicationStatus"];
            grdWStatus.DataBind();
            grdWStatus.Visible = true;
        }

        protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            li = rbType.SelectedValue;
            
            if (li == "Unit-1" || li == "Unit-2")
            {
                getUser();
                DataTable tblusermodulecategoryMerchandiser = (DataTable)ViewState["tblusermodulecategoryMerchandiser"];
                DataView dvDataMerchandiser = new DataView(tblusermodulecategoryMerchandiser);
                dvDataMerchandiser.RowFilter = "ModuleName like '%" + rbType.SelectedValue.ToString() + "%' and Category in ('HR & Compliance','Human Resources')";

                ddlHr.DataSource = dvDataMerchandiser;
                ddlHr.DataTextField = "DisplayName";
                ddlHr.DataValueField = "user_name";
                ddlHr.DataBind();
                ddlHr.Items.Insert(0, new ListItem("------Select------", "0"));


            }
           
            else if (li == "H.O")
            {
                DataTable tblusermodulecategoryMerchandiser = (DataTable)ViewState["tblusermodulecategoryMerchandiser"];
                DataView dvDataMerchandiser = new DataView(tblusermodulecategoryMerchandiser);
                dvDataMerchandiser.RowFilter = "ModuleName like '%" + rbType.SelectedValue.ToString() + "%' and Category = 'Merchandiser'";

                ddlHr.DataSource = dvDataMerchandiser;
                ddlHr.DataTextField = "DisplayName";
                ddlHr.DataValueField = "user_name";
                ddlHr.DataBind();
                ddlHr.Items.Insert(0, new ListItem("------Select------", "0"));
            }
        }

        protected void ddlHr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


      

    }
}