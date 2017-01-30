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
    public partial class ServiceMasterRequestForm : System.Web.UI.Page
    {
        public string FormID = "SMRF01";
        //private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        ComponentClass_FK objFK = new ComponentClass_FK();
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
        public string Remarks = "";
        public string TransactionID = "";
        public string FormCode = "";
        public string UserEmail = "";
        public string EmailSubject = "";
        public string EmailBody = "";
        public string SessionUser = "";
        public string DateTimeNow = "";
        public string url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtRemarksReview.Visible = false;
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

                try
                {

                    txtDescription.BackColor = System.Drawing.Color.AliceBlue;
                    txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                    txtSMC.BackColor = System.Drawing.Color.AliceBlue;

                    if (Request.QueryString["TransactionNo"] != null)
                    {
                        ////dvType.Visible = false;
                        BindPageLoad();
                        BindValuationMethod();
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        grdWStatus.Visible = true;
                        DisableControls(Page, false);
                        txtRemarks.Enabled = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        ddlServiceCategory.Visible = true;
                        ddlBUOM.Visible = true;
                        ddlMSG.Visible = true;
                        ddlDivision.Visible = true;
                        ddlValuation.Visible = true;
                        txtRemarksReview.Visible = true;
                        this.pnlemail.Visible = false;
                        this.ddlServiceCategory.Attributes.Add("disabled", "");
                        this.ddlBUOM.Attributes.Add("disabled", "");
                        this.ddlMSG.Attributes.Add("disabled", "");
                        this.ddlDivision.Attributes.Add("disabled", "");
                        //this.ddlValuation.Attributes.Add("disabled", "");

                        whenquerystringpass();
                        BindsysApplicationStatus();
                        GetHarcheyID();
                        getUserDetail();
                        GetStatusHierachyCategoryControls();

                        if (((string)ViewState["HID"]) == "1")
                        {
                            //txtDescription.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            //txtSMC.BackColor = System.Drawing.Color.AliceBlue;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnSubmit.Visible = false;
                            btnApprover.Visible = false;
                            btnReject.Visible = false;
                            txtRemarksReview.Visible = false;
                            ddlValuation.Enabled = false;
                            ddlValuation.Visible = true;
                            controlForwardHide();
                        }

                        if (((string)ViewState["HID"]) == "2")
                        {
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            btnApprover.Visible = true;
                            btnFUpdate.Visible = false;
                            btnSubmit.Visible = false;
                            txtRemarksReview.Enabled = true;
                            btnReject.Visible = true;
                            txtRemarks.Enabled = true;
                            ddlValuation.Enabled = false;
                            ddlValuation.Visible = true;
                            controlForwardHide();

                            if (((string)ViewState["Sequance"]) == "2")
                            {
                                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                                btnApprover.Visible = true;                        
                                txtRemarksReview.Enabled = true;
                                btnReject.Visible = true;
                                txtRemarks.Enabled = true;
                                ddlValuation.Enabled = true;
                                ddlValuation.Visible = true;
                                controlForwardHide();
                            }

                            if (((string)ViewState["Sequance"]) == "3")
                            {
                                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                                btnApprover.Visible = true;
                                txtRemarksReview.Enabled = true;
                                btnReject.Visible = true;
                                txtRemarks.Enabled = true;
                                ddlValuation.Enabled = false;
                                ddlValuation.Visible = true;
                                controlForwardHide();
                            }

                        }

                     
                        if (((string)ViewState["HID"]) == "4")
                        {
                            txtSMC.BackColor = System.Drawing.Color.AliceBlue;
                            txtDescription.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.Visible = true;
                            txtRemarksReview.Enabled = true;
                            btnFUpdate.Visible = false;
                            btnApprover.Visible = false;
                            btnReject.Visible = true;
                            txtRemarks.Enabled = false;
                            btnSubmit.Visible = true;
                            txtSMC.Enabled = true;
                            txtSMC.Visible = true;
                            //lblSap.Visible = false;
                            ////  ddlValuation.Attributes.Add("disabled", "false");
                            btnEdit.Visible = false;
                            //dvVC.Visible = true;
                            ddlValuation.Enabled = false;
                            ddlValuation.Visible = true;
                            divSMC.Visible = true;
                            ///this.ddlValuation.Attributes.Add("disabled", "false");    
                            this.ddlValuation.Attributes.Add("disabled", "");
                            this.ddlServiceCategory.Attributes.Add("disabled", "");
                            this.ddlBUOM.Attributes.Add("disabled", "");
                            this.ddlMSG.Attributes.Add("disabled", "");
                            this.ddlDivision.Attributes.Add("disabled", "");
                            //this.ddlValuation.Attributes.Add("disabled", "");

                            controlForwardHide();
                        }
                    }
                    else
                    {
                        ds = obj.CheckSapID(Session["User_Name"].ToString());
                        string Value = ds.Tables["SAPID"].Rows[0]["SAPID"].ToString();
                        if (Value != "")
                        {
                            ////dvType.Visible = true;
                            getUserDetail();
                            getUser();
                            getUserHOD();
                            madatorycolor();
                            GetTransactionID();
                            BindPageLoad();
                        }
                        else
                        {
                            Response.Redirect("~/AccessDenied.aspx");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }


        private void BindPageLoad()
        {
            getUserSAPID();
            BindDivision();
            BindMaterialGroup();
            BindServiceCategory();
            // BindValuation();
            BindBaseUnit();

        }

        ///////////////////////////////////////////////////BTN EVENT///////////////////////////////////////////////////////////


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlServiceCategory.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlServiceCategory.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (ddlBUOM.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlBUOM.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (ddlMSG.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlMSG.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (ddlIS.SelectedValue == "")
                {
                    lblUpError.Text = "Select IS Department field!.";
                    error.Visible = true;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (ddlReviewer.SelectedValue == "")
                {
                    lblUpError.Text = "Select FI Department field!.";
                    error.Visible = true;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (ddlEmailMDA.SelectedValue == "0")
                {
                    lblUpError.Text = "Select Service Master Administrator field!.";
                    error.Visible = true;
                    ddlEmailMDA.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else
                {
                    string Result = "";
                    string Notification = "";

                    Result = ViewState["HOD"].ToString() + "," + ddlReviewer.SelectedValue + "," + ddlIS.SelectedValue;

                    cmd.CommandText = "";
                    cmd.CommandText = "SP_SYS_ServiceMasterRequest";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                    cmd.Parameters.AddWithValue("@SAPID", txtSAPID.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@ServiceCategory", ddlServiceCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@BUOM", ddlBUOM.SelectedValue);
                    cmd.Parameters.AddWithValue("@MSG", ddlMSG.SelectedValue);
                    cmd.Parameters.AddWithValue("@Division", ddlDivision.SelectedValue);
                    cmd.Parameters.AddWithValue("@APPROVAL", Result.ToString());
                    cmd.Parameters.AddWithValue("@MDA", ddlEmailMDA.SelectedValue);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["User_Name"].ToString());
                    cmd.Parameters.AddWithValue("@Remarks", txtRemarksReview.Text);


                    cmd.CommandType = CommandType.StoredProcedure;
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
                    Page.MaintainScrollPositionOnPostBack = false;
                    ClearInputs(Page.Controls);
                    ClearInputscolor(Page.Controls);
                    GetTransactionID();
                    madatorycolor();
                }
            }

            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        protected void btnApprover_Click(object sender, EventArgs e)
        {
            try
            {

                if (((string)ViewState["HID"]) == "2" && (((string)ViewState["Sequance"]) == "2"))
                {
                    updateFI();
                    error.Visible = false;
                    lblUpError.Text = "";
                    sucess.Visible = false;
                    lblmessage.Text = "";
                    EmailWorkApproved();
                    ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                    Page.MaintainScrollPositionOnPostBack = true;
                    txtRemarksReview.BackColor = System.Drawing.Color.White;
                    lblEmail.Focus();
                    whenquerystringpass();
                }
                else
                {
                    error.Visible = false;
                    lblUpError.Text = "";
                    sucess.Visible = false;
                    lblmessage.Text = "";
                    EmailWorkApproved();
                    ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                    Page.MaintainScrollPositionOnPostBack = true;
                    txtRemarksReview.BackColor = System.Drawing.Color.White;
                    lblEmail.Focus();
                    whenquerystringpass();
                }
                   
                
            }

            catch (Exception ex)
            {
                lblError.Text = "btnApprover_Click" + ex.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = false;
            txtSMC.BackColor = System.Drawing.Color.AliceBlue;
            lblError.Text = "";
            try
            {
               
                 if (txtSMC.Text == "")
                {
                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "Service Master Code should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtSMC.BackColor = System.Drawing.Color.Red;
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "";
                    sucess.Visible = false;
                    error.Visible = false;
                    ds = objFK.UpdateServiceMaster(lblMaxTransactionID.Text, txtSMC.Text.Trim(), ddlValuation.SelectedValue.ToString());
                    string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                    //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup2();", true);
                    lblmessage.Text = "Service Master Code " + txtSMC.Text.Trim() + "" + message + " Form ID # " + Request.QueryString["TransactionNo"].ToString();

                    lblmessage.Visible = true;
                    try
                    {
                        EmailWorkFirstHaracheyMDA();
                        InsertEmailHOD();
                        ApplicationStatus();
                        BindsysApplicationStatus();
                        GetStatusHierachyCategoryControls();
                        sucess.Visible = true;
                        error.Visible = false;
                        this.ddlValuation.Attributes.Add("disabled", "");
                        Page.MaintainScrollPositionOnPostBack = false;

                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "Email Send Failed!";
                        lblError.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                lblError.Text = "MDA" + ex.ToString();
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
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtRemarksReview.BackColor = System.Drawing.Color.Red;
                    whenquerystringpass();
                    return;
                }
                else
                {
                    EmailWorkReject();
                    ClosedFormAfterReject();
                    //   ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                    Page.MaintainScrollPositionOnPostBack = true;
                    lblEmail.Focus();
                }
            }

            catch (Exception ex)
            {
                lblError.Text = "Reject" + ex.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            Response.Redirect(url.ToString());
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void UpdateSerialNumberAll()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsertEmail = new SqlCommand())//
                {

                    cmdInsertEmail.CommandText = "UpdateSerialNo";
                    cmdInsertEmail.CommandType = CommandType.StoredProcedure;
                    cmdInsertEmail.Connection = connection;
                    adp.SelectCommand = cmdInsertEmail;
                    cmdInsertEmail.Parameters.AddWithValue("@TransactionID", lblMaxTransactionID.Text);
                    cmdInsertEmail.Parameters.AddWithValue("@FormID", FormID.ToString());

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

        private void EMailForwardToForwarder()
        {

            ds = obj.MailForwardToForwarder(ddlTransferUser.SelectedValue.ToString());

            if (ds.Tables["MailForwardToForwarder"].Rows.Count > 0)
            {
                DataTableReader reader = ds.Tables["MailForwardToForwarder"].CreateDataReader();
                while (reader.Read())
                {
                    string url = Request.Url.ToString();
                    TransactionID = lblMaxTransactionID.Text.ToString();
                    FormCode = FormID.ToString();
                    UserName = reader["DisplayName"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has forward you a New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " for approval. <br><br> Your kind approval is required on the following URL: <br><br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, <br>you do not need to reply to this message.<br>" +
                        "<br>Material Master Application <br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();
                }
            }
            //}
            else
            {

            }

        }

        protected void btnForward_Click(object sender, EventArgs e)
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
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtRemarksReview.BackColor = System.Drawing.Color.Red;
                    whenquerystringpass();
                    return;
                }
                else if (ddlTransferUser.SelectedValue == "0")
                {
                    error.Visible = true;
                    lblUpError.Text = "Select any Transfer user";
                    lblUpError.Focus();
                    error.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                }
                else
                {
                    error.Visible = false;
                    lblUpError.Text = "";
                    GetHarcheyNextData();
                    InsertTransferEmail();
                    string HierachyCategoryStatus = "06";
                    ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                    ApplicationStatus();
                    BindsysApplicationStatus();
                    UpdateSerialNumberAll();
                    EMailForwardToForwarder();
                    GetStatusHierachyCategoryControls();

                    sucess.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = true;
                    lblEmail.Text = "*Service Master Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been transferred to " + ddlTransferUser.SelectedItem.Text + "";
                    Session["HC"] = "06";
                    btnApprover.Enabled = false;
                    btnReject.Attributes.Add("disabled", "true");
                    btnEdit.Visible = false;
                    txtRemarksReview.Enabled = false;
                    txtRemarks.Enabled = true;
                    ddlTransferUser.Enabled = true;
                    btnFUpdate.Visible = false;
                    ddlTransferUser.SelectedIndex = -1;
                    lblEmail.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        ////////////////////////////////////////////Drop Down///////////////////////////////////////////////////////////////////

        protected void getUser()
        {
            cmd.CommandText = "";
            cmd.CommandText = "select * from tbluserMDA where FormName = 'SMRF'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlEmailMDA.DataSource = cmd.ExecuteReader();
            ddlEmailMDA.DataTextField = "DisplayName";
            ddlEmailMDA.DataValueField = "user_name";
            ddlEmailMDA.DataBind();
            conn.Close();


            cmd.CommandText = "";
            cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserReviwer where FormName = 'SMRF'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlReviewer.DataSource = cmd.ExecuteReader();
            ddlReviewer.DataTextField = "DisplayName";
            ddlReviewer.DataValueField = "user_name";
            ddlReviewer.DataBind();
            conn.Close();


            cmd.CommandText = "";
            cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserApproval where FormName = 'SMRF'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlIS.DataSource = cmd.ExecuteReader();
            ddlIS.DataTextField = "DisplayName";
            ddlIS.DataValueField = "user_name";
            ddlIS.DataBind();
            conn.Close();


            ddlEmailMDA.Items.Insert(0, new ListItem("------Select------", "0"));
            ddlReviewer.Items.Insert(0, new ListItem("------Select------", "0"));
            ddlIS.Items.Insert(0, new ListItem("------Select------", "0"));

        }

        protected void getTransferUser()
        {//SELECT user_name,DisplayName FROM tbluser where user_name not in ('" + Session["User_Name"].ToString() + "')
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdgetTransferUser = new SqlCommand())
                {
                    cmdgetTransferUser.CommandText = @"SELECT User_name,DisplayName
                                FROM tbluser WHERE User_name not IN
                               ((SELECT RoutingID 
                               FROM [tbl_SysHierarchyControl]
	                           where FormID = '" + FormID.ToString() + "' and TransactionID = '" + lblMaxTransactionID.Text + "'" +
                                       "and Status = '06' ))";
                    cmdgetTransferUser.CommandType = CommandType.Text;
                    cmdgetTransferUser.Connection = conn;
                    adp.SelectCommand = cmdgetTransferUser;
                    adp.Fill(ds, "getTransferUser");
                    ddlTransferUser.DataTextField = ds.Tables["getTransferUser"].Columns["DisplayName"].ToString(); // text field name of table dispalyed in dropdown
                    ddlTransferUser.DataValueField = ds.Tables["getTransferUser"].Columns["User_name"].ToString();             // to retrive specific  textfield name 
                    ddlTransferUser.DataSource = ds.Tables["getTransferUser"];      //assigning datasource to the dropdownlist
                    ddlTransferUser.DataBind();  //binding dropdownlist
                    ddlTransferUser.Items.Insert(0, new ListItem("------Select------", "0"));
                }
            }
        }

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

        protected void getUserSAPID()
        {
            string strQuery = @"Select SAPID from tbluser where user_name = '" + Session["user_name"].ToString() + "'";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(ds, "tbl_UserSAPID");

            string Value = ds.Tables["tbl_UserSAPID"].Rows[0]["SAPID"].ToString();
            txtSAPID.Text = Value.ToString();
            conn.Close();
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

        private void GetStatusHierachyCategoryControls()
        {
            ds = obj.GetStatusHierachyCategoryControl(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString(), ViewState["HID"].ToString());
            if (ds.Tables["tbl_SysHierarchyControl"].Rows.Count > 0)
            {
                ViewState["StatusHierachyCategory"] = ds.Tables["tbl_SysHierarchyControl"].Rows[0]["Status"].ToString();
            }
            if (((string)ViewState["StatusHierachyCategory"]) == "01" || ((string)ViewState["StatusHierachyCategory"]) == "02" || ((string)ViewState["StatusHierachyCategory"]) == "03" || ((string)ViewState["StatusHierachyCategory"]) == "04" || ((string)ViewState["StatusHierachyCategory"]) == "00" || ((string)ViewState["StatusHierachyCategory"]) == "06")
            {
                btnSave.Enabled = false;
                //btnApproved.Enabled = false;
                txtSMC.Attributes.Add("disabled", "true");
                ddlValuation.Attributes.Add("disabled", "true");
                btnReject.Attributes.Add("disabled", "true");
                btnApprover.Enabled = false;
                btnSubmit.Enabled = false;
                btnCancel.Enabled = false;
                ////btnSubmitFC.Enabled = false;
                txtRemarksReview.Attributes.Add("disabled", "true");
                ////disabledListItem();
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

        protected void madatorycolor()
        {
            ddlServiceCategory.BackColor = System.Drawing.Color.AliceBlue;
            ddlBUOM.BackColor = System.Drawing.Color.AliceBlue;
            ddlMSG.BackColor = System.Drawing.Color.AliceBlue;
            ddlDivision.BackColor = System.Drawing.Color.AliceBlue;
            ddlValuation.BackColor = System.Drawing.Color.AliceBlue;
            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;

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

                if (c is RadioButtonList)
                {
                    ((RadioButtonList)(c)).Enabled = State;
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

        protected void refreshpage()
        {
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "myscript", "setTimeout(function(){location.href='MeterialMaster.aspx';},15000);", true);
            ClearInputs(Page.Controls);
        }

        private void BindsysApplicationStatus()
        {
            ds = obj.BindsysApplicationStatus(lblMaxTransactionID.Text, FormID.ToString());
            grdWStatus.DataSource = ds.Tables["BindsysApplicationStatus"];
            grdWStatus.DataBind();
            grdWStatus.Visible = true;
        }

        private void GetHarcheyNextData()
        {
            ds = obj.GetHarachyNextData(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString(), ViewState["HID"].ToString());
            if (ds.Tables["GetHarachyNextData"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["GetHarachyNextData"].Rows.Count; i++)
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
                    {
                        using (SqlCommand cmdInsertEmail = new SqlCommand())
                        {
                            int value = (int)ds.Tables["GetHarachyNextData"].Rows[i]["Sequance"] + 1;
                            cmdInsertEmail.Connection = connection;
                            cmdInsertEmail.CommandType = CommandType.Text;
                            cmdInsertEmail.CommandText = @"update sysWorkFlow set Sequance = '" + value + "' where TransactionID = '" + lblMaxTransactionID.Text + "' and FormID = '" + FormID.ToString() + "' and HierachyCategory = '" + ViewState["HID"].ToString() + "'  and RoughtingUserID like '" + ds.Tables["GetHarachyNextData"].Rows[i]["RoughtingUserID"] + "%'";

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

            }
        }

        protected void InsertTransferEmail()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsertEmail = new SqlCommand())//
                {
                    int ResultSequance = 0;
                    int Value = Convert.ToInt32(ViewState["Sequance"]);

                    int _Temp = Convert.ToInt32(1);

                    ResultSequance = Value + _Temp;
                    DateTime today = DateTime.Today;
                    cmdInsertEmail.Connection = connection;
                    cmdInsertEmail.CommandType = CommandType.Text;
                    cmdInsertEmail.CommandText = @"INSERT INTO sysWorkFlow
           (FormID
           ,TransactionID
           ,CreatedBy
           ,HierachyCategory
           ,RoughtingUserID
           ,Sequance
           ,DateTime) VALUES 
          ('" + FormID.ToString() + "','" + lblMaxTransactionID.Text.ToString() + "','" + ViewState["FormCreatedBy"].ToString() + "','" + ViewState["HID"] + "','" + ddlTransferUser.SelectedValue.ToString() + "','" + ResultSequance + "','" + today.ToString() + "')";

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

        void ClearInputss(ControlCollection ctrlss)
        {
            foreach (Control ctrlsss in ctrlss)
            {
                if (ctrlsss is TextBox)
                    ((TextBox)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                if (ctrlsss is DropDownList)
                    ((DropDownList)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                if (ctrlsss is ListBox)
                    ((ListBox)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                ClearInputss(ctrlsss.Controls);
            }
        }

        private void whenquerystringpass()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdGetData = new SqlCommand())
                {
                    conn.Close();
                    string a = Request.QueryString["TransactionNo"].ToString();
                    cmdGetData.CommandText = @"Select * from tbl_FI_ServiceMasterRequest
                            where TransactionMain = '" + a.ToString() + "'";
                    cmdGetData.CommandType = CommandType.Text;
                    cmdGetData.Connection = connection;

                    adp.SelectCommand = cmdGetData;
                    dt.Clear();
                    adp.Fill(dt);
                    ViewState["DATA"] = dt;
                    DataTableReader reader = dt.CreateDataReader();
                    while (reader.Read())
                    {
                        reader.Read();
                        lblMaxTransactionNo.Text = reader[0].ToString();
                        lblMaxTransactionID.Text = reader[1].ToString();
                        txtSMC.Text = reader[2].ToString();
                        txtSAPID.Text = reader[3].ToString();
                        txtDescription.Text = reader["Description"].ToString();
                        ddlServiceCategory.SelectedValue = reader["ServiceCategory"].ToString();
                        ddlBUOM.SelectedValue = reader["BUOM"].ToString();
                        ddlMSG.SelectedValue = reader["MSG"].ToString();
                        ddlValuation.SelectedValue = reader["ValuationClass"].ToString();
                    }
                    reader.Close();
                }
            }
        }

        ////////////////////////////////////////////methods/////////////////////////////////////////////////////////

        private void updateFI()
        {
            try
            {

                if (ddlValuation.SelectedValue == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Valuation Type  should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlValuation.BackColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {

                    string VTYPE = "";

                    for (int i = 0; i <= ddlValuation.Items.Count - 1; i++)
                    {
                        if (ddlValuation.Items[i].Selected)
                        {
                            if (VTYPE == "") { VTYPE = ddlValuation.Items[i].Value; }
                            else { VTYPE += "," + ddlValuation.Items[i].Value; }
                        }

                    }

                    cmd.CommandText = @"SP_SYS_UpdateValuatoinClass";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                    cmd.Parameters.AddWithValue("@TransactionID", lblMaxTransactionID.Text);
                    cmd.Parameters.AddWithValue("@ValuationClass", ddlValuation.SelectedValue);
                    conn.Open();

                    int aa = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (aa == -1)
                    {
                        lblmessage.Text = "Record updated sucessfully!";
                        lblmessage.Focus();
                        sucess.Visible = true;
                        error.Visible = false;
                        Page.MaintainScrollPositionOnPostBack = false;
                        btnReject.Visible = true;
                        btnFUpdate.Visible = false;

                        txtRemarksReview.Visible = true;
                        txtRemarksReview.Enabled = true;
                        txtRemarks.Enabled = false;
                        btnApprover.Visible = true;
                        btnEdit.Visible = false;
                        btnForward.Visible = true;
                        btnTransfer.Visible = true;
                        DisableControls(Page, false);

                        this.ddlServiceCategory.Attributes.Add("disabled", "");
                        this.ddlBUOM.Attributes.Add("disabled", "");
                        this.ddlMSG.Attributes.Add("disabled", "");
                        this.ddlDivision.Attributes.Add("disabled", "");

                        txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                        txtRemarksReview.Visible = true;
                        txtRemarksReview.Enabled = true;
                        lblEmail.ForeColor = System.Drawing.Color.Blue;
                        controlForwardHide();
                        error.Visible = false;
                        btnForward.Visible = false;
                        btnTransfer.Visible = false;
                    }
                }
              
            }
            catch (Exception ex)
            {
                lblError.Text = "updateFI" + ex.ToString();
            }
        }

        private void controlForwardHide()
        {
            try
            {

                ds = obj.controlFowardControl(FormID.ToString(), lblMaxTransactionID.Text.ToString(), ViewState["HID"].ToString(), "06", Session["User_Name"].ToString());
                if (ds.Tables["controlFowardControl"].Rows.Count > 0)
                {
                    btnForward.Visible = false;
                    btnTransfer.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Ha ID Error" + ex.ToString();
            }
        }

        private void BindServiceCategory()
        {
            ds = objFK.BindServiceCategory();
            ddlServiceCategory.DataTextField = ds.Tables["tbl_ServiceCategory"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
            ddlServiceCategory.DataValueField = ds.Tables["tbl_ServiceCategory"].Columns["ServiceCategoryCode"].ToString();             // to retrive specific  textfield name 
            ddlServiceCategory.DataSource = ds.Tables["tbl_ServiceCategory"];      //assigning datasource to the dropdownlist
            ddlServiceCategory.DataBind();  //binding dropdownlist
            ddlServiceCategory.Items.Insert(0, new ListItem("------Select------", "0"));
        }

        private void BindBaseUnit()
        {
            ds = objFK.BindBaseUnit();
            ddlBUOM.DataTextField = ds.Tables["tblBaseunitofmeasure"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
            ddlBUOM.DataValueField = ds.Tables["tblBaseunitofmeasure"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
            ddlBUOM.DataSource = ds.Tables["tblBaseunitofmeasure"];      //assigning datasource to the dropdownlist
            ddlBUOM.DataBind();  //binding dropdownlist
            ddlBUOM.Items.Insert(0, new ListItem("------Select------", "0"));
            ddlBUOM.SelectedValue = "AU";
        }

        private void BindMaterialGroup()
        {
            ds = objFK.BindMaterialService();
            ddlMSG.DataTextField = ds.Tables["tblMaterialgrp"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
            ddlMSG.DataValueField = ds.Tables["tblMaterialgrp"].Columns["Materialgrpcode"].ToString();             // to retrive specific  textfield name 
            ddlMSG.DataSource = ds.Tables["tblMaterialgrp"];      //assigning datasource to the dropdownlist
            ddlMSG.DataBind();  //binding dropdownlist
            ddlMSG.Items.Insert(0, new ListItem("------Select------", "0"));
        }

        private void BindDivision()
        {
            ds = objFK.BindDivision();
            ddlDivision.DataTextField = ds.Tables["tblDivision"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
            ddlDivision.DataValueField = ds.Tables["tblDivision"].Columns["Divisioncode"].ToString();             // to retrive specific  textfield name 
            ddlDivision.DataSource = ds.Tables["tblDivision"];      //assigning datasource to the dropdownlist
            ddlDivision.DataBind();  //binding dropdownlist
            ddlDivision.Items.Insert(0, new ListItem("------Select------", "0"));
            ddlDivision.SelectedValue = "10";
        }

        private void BindValuationMethod()
        {
            ds.Clear();
            //ds = objFK.BindValuation();
            //ddlValuationClass.DataTextField = ds.Tables["tblValuationClass"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
            //ddlValuationClass.DataValueField = ds.Tables["tblValuationClass"].Columns["ValuationClasscode"].ToString();             // to retrive specific  textfield name 
            //ddlValuationClass.DataSource = ds.Tables["tblValuationClass"];      //assigning datasource to the dropdownlist
            //ddlValuationClass.DataBind();  //binding dropdownlist


            cmd.CommandText = "";
            cmd.CommandText = "select  ValuationClasscode,ValuationClasscode +' '+ Description as Description from tblValuationClass";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlValuation.DataSource = cmd.ExecuteReader();
            ddlValuation.DataTextField = "Description";
            ddlValuation.DataValueField = "ValuationClasscode";
            ddlValuation.DataBind();
            conn.Close();

            ddlValuation.Items.Insert(0, new ListItem("------Select------", ""));

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


        ////////////////////////////////////////////methods/////////////////////////////////////////////////////////

        #region methodEmailWorks

        ///////////////////////////////////////////////////EmailMethods//////////////////////////////////////////////////////////

        private void EmailWorkSendFirstApproval()
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
                    EmailSubject = "Service Master Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has sent you a Service Master Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " for approval. <br><br> Your kind approval is required on the following URL: <br><br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, <br>you do not need to reply to this message.<br>" +
                        "<br>Service Master Finance Application<br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    //   InsertEmail();

                }
            }
            //}
            else
            {

            }

        }

        private void EmailWorkFirstHaracheyMDA()
        {
            string HierachyCategory = "4";
            string HierachyCategoryStatus = "04"; // Allow based on reqierment if there is No MDA if other wise allow "4"//
            ds = obj.MailForwardToAllFromMDA(lblMaxTransactionID.Text, FormID.ToString(), HierachyCategory.ToString());

            if (ds.Tables["MailForwardToAllFromMDA"].Rows.Count > 0)
            {
                DataTableReader reader = ds.Tables["MailForwardToAllFromMDA"].CreateDataReader();
                while (reader.Read())
                {
                    url = Request.Url.ToString();
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "Service Master Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br><br>  SAP material code " + txtSMC.Text.Trim() + " has been issued against Service Master Request Form ID # " + lblMaxTransactionID.Text.ToString() + " <br><br> The form can be reviewed at the following URL:<br> <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>  This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                    "<br>Service Master Finance Application <br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    //   InsertEmail();

                    lblmessage.Text = "SAP Service Master Code " + txtSMC.Text.Trim() + " has been saved against  Form ID # " + lblMaxTransactionID.Text;

                    lblmessage.ForeColor = System.Drawing.Color.Green;
                    conn.Close();
                    sucess.Visible = true;
                    error.Visible = false;
                    lblmessage.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ////  txtSMC.BackColor = System.Drawing.Color.White;
                    Page.MaintainScrollPositionOnPostBack = false;
                    ViewState["Status"] = HierachyCategoryStatus.ToString();
                }

            }
            else
            {

            }
        }

        private void EmailWorkApproved()
        {
            string HierachyCategoryStatus = "02";
            ds = obj.MailForwardFormApprover(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
            string Value = ds.Tables["MailForwardFormApprover"].Rows[0]["HierachyCategory"].ToString();
            DataTableReader reader = ds.Tables["MailForwardFormApprover"].CreateDataReader();
            if (ds.Tables["MailForwardFormApprover"].Rows.Count > 0 && Value == "2")
            {
                while (reader.Read())
                {
                    url = Request.Url.ToString();
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "Service Master Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> A Service Master Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br> <br> You are requested to provide authorization for the information on the following URL: <br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Service Master Finance Application<br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    //   InsertEmail();

                    lblEmail.Text = "*Service Master Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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

                        url = Request.Url.ToString();
                        TransactionID = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Service Master Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> A Service Master Request against Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br><br> You can authorized a person on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message.<br>" +
                             "Service Master Finance Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                        lblEmail.Text = "*Service Master Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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
                    url = Request.Url.ToString();
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "Service Master Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>  Your Service Master Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been disapproved by  " + ViewState["SessionUser"].ToString() + " <br><br> The reason of rejection is given below you can review your form on following url:<br><br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a>" +
                            "<br> <br> <br><b>Reject Remarks: " + txtRemarksReview.Text + "</b> " +
                          " <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br>" +
                        "<br>Service Master Finance Application<br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();
                    ViewState["Status"] = "00"; // For Status Reject
                    lblEmail.Text = "*Service Master Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been rejected by you";
                    lblEmail.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    Page.MaintainScrollPositionOnPostBack = true;
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

        #endregion





        ///////////////////////////////////////////////////EmailMethods//////////////////////////////////////////////////////////

    }
}