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

namespace ITLDashboard.Modules.SBApp
{
    public partial class TransportRequestForm : System.Web.UI.Page
    {
        public string FormID = "TRF01";
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
        public string urlMobile = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtRemarksReview.Visible = false; ;
                ddlApplicableArea.BackColor = System.Drawing.Color.AliceBlue;
                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
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

                    ddlApplicableArea.BackColor = System.Drawing.Color.AliceBlue;
                    txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                    if (Request.QueryString["TransactionNo"] != null)
                    {
                        //dvType.Visible = false;
                        BindPageLoad();
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        grdWStatus.Visible = true;
                        DisableControls(Page, false);
                        txtRemarks.Enabled = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        ddlApplicableArea.Visible = true;
                        ddlTransportTo.Visible = true;
                        txtRemarksReview.Enabled = false;
                        txtRemarksReview.Visible = false;
                        this.pnlemail.Visible = false;
                        whenquerystringpass();
                        BindsysApplicationStatus();
                        GetHarcheyID();
                        getUserDetail();
                        GetStatusHierachyCategoryControls();
                        if (((string)ViewState["HID"]) == "1")
                        {
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnSubmit.Visible = false;
                            btnApprover.Visible = false;
                            Button1.Visible = false;
                            txtRemarksReview.Enabled = true;
                            txtRemarksReview.Visible = true;
                            ddlApplicableArea.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            for (int i = 0; i < ddlApplicableArea.Items.Count; i++)
                            {
                                ddlApplicableArea.Items[i].Attributes.Add("disabled", "disabled");
                            }
                        }
                        if (((string)ViewState["HID"]).StartsWith("2"))
                        {
                            btnApprover.Visible = true;
                            btnSubmit.Visible = false;
                            txtRemarksReview.Enabled = true;
                            txtRemarksReview.Visible = true;
                            ddlApplicableArea.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            Button1.Visible = true;
                            txtRemarks.Enabled = true;
                            btnSubmitFC.Visible = false;

                            if (((string)ViewState["Sequance"]) == "3")
                            {
                                btnApprover.Visible = true;
                                btnSubmit.Visible = false;
                                Button1.Visible = true;
                                txtRemarks.Enabled = true;
                                btnSubmitFC.Visible = false;
                                txtRemarksReview.Enabled = true;
                                txtRemarksReview.Visible = true;
                                ddlApplicableArea.BackColor = System.Drawing.Color.AliceBlue;
                                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                                for (int i = 0; i < ddlTransportTo.Items.Count; i++)
                                {
                                    ddlTransportTo.Items[i].Attributes.Add("disabled", "disabled");
                                }
                                EnabledListItem();
                            }
                            for (int i = 0; i < ddlTransportTo.Items.Count; i++)
                            {
                                ddlTransportTo.Items[i].Attributes.Add("disabled", "disabled");
                            }

                        }
                        if (((string)ViewState["HID"]) == "4")
                        {
                            btnApprover.Visible = false;
                            Button1.Visible = true;
                            txtRemarks.Enabled = true;
                            btnSubmit.Visible = true;
                            txtRemarksReview.Enabled = true;
                            txtRemarksReview.Visible = true;
                            ddlApplicableArea.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;

                            for (int i = 0; i < ddlTransportTo.Items.Count; i++)
                            {
                                ddlTransportTo.Items[i].Attributes.Add("disabled", "disabled");
                            }
                        }
                    }
                    else
                    {
                        getUserDetail();
                        getUser();
                        getUserHOD();
                        madatorycolor();
                        GetTransactionID();
                        BindPageLoad();
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
            BindTransportTo();
            ////BindPlant();
            ////BindStorageLocation();
            ////BindMovementType();
            ////BindOrdertype();

        }

        protected void disabledListItem()
        {
            for (int i = 0; i < ddlApplicableArea.Items.Count; i++)
            {
                ddlApplicableArea.Items[i].Attributes.Add("disabled", "disabled");
            }
            for (int i = 0; i < ddlTransportTo.Items.Count; i++)
            {
                ddlTransportTo.Items[i].Attributes.Add("disabled", "disabled");
            }
        }

        protected void EnabledListItem()
        {
            for (int i = 0; i < ddlTransportTo.Items.Count; i++)
            {
                ddlTransportTo.Items[i].Attributes.Remove("disabled");
            }
        }

        ///////////////////////////////////////////////////Button Controls//////////////////////////////////////////////////////////

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlApplicableArea.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlApplicableArea.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (ddlTransportTo.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlTransportTo.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (ddlReviewer.SelectedValue == "")
                {
                    lblUpError.Text = "Select Reviewer field!.";
                    error.Visible = true;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (ddlEmailMDA.SelectedValue == "0")
                {
                    lblUpError.Text = "Select Basis Administrator field!.";
                    error.Visible = true;
                    ddlEmailMDA.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else
                {
                    string Result = "";
                    string Notification = "";
                    string TransportTo = "";

                    for (int i = 0; i <= ddlTransportTo.Items.Count - 1; i++)
                    {
                        if (ddlTransportTo.Items[i].Selected)
                        {
                            if (TransportTo == "") { TransportTo = ddlTransportTo.Items[i].Value.Trim(); }
                            else { TransportTo += ',' + ddlTransportTo.Items[i].Value.Trim(); }
                        }
                        TransportTo = TransportTo.Trim();
                    }

                    Result = ViewState["HOD"].ToString() + "," + ddlReviewer.SelectedValue + "," + ddlFC.SelectedValue;

                    cmd.CommandText = "";
                    cmd.CommandText = "SP_SYS_TransportRequestForm";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                    ////  cmd.Parameters.AddWithValue("@SAPID", txtSAPID.Text);
                    cmd.Parameters.AddWithValue("@ApplicableArea", ddlApplicableArea.SelectedValue);
                    cmd.Parameters.AddWithValue("@TransportTo", TransportTo.ToString());
                    cmd.Parameters.AddWithValue("@TRNo", TxtTRNo.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                whenquerystringpass();
                for (int i = 0; i < ddlApplicableArea.Items.Count; i++)
                {
                    ddlApplicableArea.Items[i].Attributes.Add("disabled", "disabled");
                }
                for (int i = 0; i < ddlTransportTo.Items.Count; i++)
                {
                    ddlTransportTo.Items[i].Attributes.Add("disabled", "disabled");
                }
                {

                    EmailWorkFirstHaracheyMDA();
                    InsertEmailHOD();
                    ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                    // whenquerystringpass();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Rev" + ex.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            Response.Redirect(url.ToString());

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

        protected void btnApprover_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtRemarksReview.Text == "")
                //{

                //    lblmessage.Text = "";
                //    lblUpError.Text = "Remarks should not be left blank!";
                //    sucess.Visible = false;
                //    error.Visible = true;
                //    lblmessage.Focus();
                //    sucess.Focus();
                //    Page.MaintainScrollPositionOnPostBack = false;
                //    txtRemarksReview.BackColor = System.Drawing.Color.Red;
                //    whenquerystringpass();
                //    // bindSLfromPlant();
                //    return;
                //}
                //else
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
                lblError.Text = "Approver" + ex.ToString();
            }
        }

        protected void btnReject_Click1(object sender, EventArgs e)
        {

        }

        ///////////////////////////////////////////////////Button Controls//////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////DROPDOWN EVENTS//////////////////////////////////////////////////////////

        protected void ddlApplicableArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                bindModuleCatg();
                bindModuleFC();
                ddlEmailMDA.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        protected void bindModuleCatg()
        {
            string strQuery2 = "";
            strQuery2 = @"Select user_name,DisplayName from tblusermodulecategory where ModuleName Like '%" + ddlApplicableArea.SelectedValue.ToString() + "%' and Category like '%Reviewer%'";
            using (SqlCommand cmd = new SqlCommand())
            {
                ds.Clear();
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.AddWithValue("@Modules", "'%" + ddlDBModules.SelectedValue.ToString() + "%'");
                cmd.CommandText = strQuery2;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                conn.Open();
                ds.Clear();
                adp.Fill(ds, "tblusermodulecategory");

                ddlReviewer.DataTextField = ds.Tables["tblusermodulecategory"].Columns["DisplayName"].ToString(); // text field name of table dispalyed in dropdown
                ddlReviewer.DataValueField = ds.Tables["tblusermodulecategory"].Columns["user_name"].ToString();             // to retrive specific  textfield name 
                ddlReviewer.DataSource = ds.Tables["tblusermodulecategory"];      //assigning datasource to the dropdownlist
                ddlReviewer.DataBind();  //binding dropdownlist
                conn.Close();
                ddlReviewer.Items.Insert(0, new ListItem("------Select------", "0"));
                ddlReviewer.SelectedIndex = 1;
            }
        }

        protected void bindModuleFC()
        {
            string strQuery = "";
            strQuery = @"Select user_name,DisplayName from tblusermodulecategory where ModuleName like '%" + ddlApplicableArea.SelectedValue.ToString() + "%' and Category = 'Function Consultant'";
            using (SqlCommand cmd = new SqlCommand())
            {
                ds.Clear();
                cmd.CommandType = CommandType.Text;
                // cmd.Parameters.AddWithValue("@Modules", ddlDBModules.SelectedValue.ToString());
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                conn.Open();
                adp.Fill(ds, "FC");
                ddlFC.DataTextField = ds.Tables["FC"].Columns["DisplayName"].ToString(); // text field name of table dispalyed in dropdown
                ddlFC.DataValueField = ds.Tables["FC"].Columns["user_name"].ToString();             // to retrive specific  textfield name 
                ddlFC.DataSource = ds.Tables["FC"];      //assigning datasource to the dropdownlist
                ddlFC.DataBind();  //binding dropdownlist
                conn.Close();
                ddlFC.Items.Insert(0, new ListItem("------Select------", "0"));
                ddlFC.SelectedIndex = 1;
            }
        }

        protected void btnSubmitFC_Click(object sender, EventArgs e)
        {

        }

        protected void btnForward_Click(object sender, EventArgs e)
        {

        }

        ///////////////////////////////////////////////////DROPDOWN EVENTS//////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////Methods//////////////////////////////////////////////////////////

        protected void getUser()
        {
            cmd.CommandText = "";
            cmd.CommandText = "Select user_name,DisplayName from tblusermodulecategory where Category like '%SAP Basis Consultant%'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlEmailMDA.DataSource = cmd.ExecuteReader();
            ddlEmailMDA.DataTextField = "DisplayName";
            ddlEmailMDA.DataValueField = "user_name";
            ddlEmailMDA.DataBind();
            conn.Close();
            ddlEmailMDA.Items.Insert(0, new ListItem("------Select------", "0"));
            ddlFC.Items.Insert(0, new ListItem("------Select------", "0"));
            ddlReviewer.Items.Insert(0, new ListItem("------Select------", "0"));

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
                Button1.Attributes.Add("disabled", "true");
                btnApprover.Enabled = false;
                btnSubmit.Enabled = false;
                btnCancel.Enabled = false;
                btnSubmitFC.Enabled = false;
                txtRemarksReview.Attributes.Add("disabled", "true");
                disabledListItem();
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
            ddlApplicableArea.BackColor = System.Drawing.Color.AliceBlue;
            ddlTransportTo.BackColor = System.Drawing.Color.AliceBlue;
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

        private void BindTransportTo()
        {
            ds = objFK.BindTransportTo();
            ddlTransportTo.DataTextField = ds.Tables["tbl_TransportTo"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
            ddlTransportTo.DataValueField = ds.Tables["tbl_TransportTo"].Columns["TransportTo"].ToString();             // to retrive specific  textfield name 
            ddlTransportTo.DataSource = ds.Tables["tbl_TransportTo"];      //assigning datasource to the dropdownlist
            ddlTransportTo.DataBind();  //binding dropdownlist
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
                    cmdGetData.CommandText = @"Select * from tbl_TransportRequestForm
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
                        ////   txtSAPID.Text = reader[2].ToString();
                        ddlApplicableArea.SelectedValue = reader["ApplicableArea"].ToString();
                        ddlTransportTo.Items.Clear();
                        BindTransportTo();


                        for (int i = 0; i < ddlTransportTo.Items.Count; i++)
                        {
                            foreach (string category1 in reader[3].ToString().Trim().Split(','))
                            {
                                if (category1 != ddlTransportTo.Items[i].Value) continue;
                                ddlTransportTo.Items[i].Selected = true;
                                break;
                            }
                        }
                        if (reader[3].ToString().Trim() != "")
                        {
                            for (int i = 0; i < ddlTransportTo.Items.Count; i++)
                            {
                                ddlTransportTo.Items[i].Attributes.Add("disabled", "disabled");
                            }
                        }
                        TxtTRNo.Text = reader[4].ToString();
                        txtDescription.Text = reader[5].ToString();

                    }
                    reader.Close();
                }
            }
        }

        ///////////////////////////////////////////////////Methods//////////////////////////////////////////////////////////

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
                    url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                    urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110") + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "Transport Request Form – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + "has sent you a Transport Request Form against  Form ID # " + lblMaxTransactionID.Text.ToString() + " for approval. <br><br> Your kind approval is required on the following URL:  " +
                    "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                    "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                    "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                    "<br>User Rights Application<br> Information Systems Dashboard";
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
                    url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                    urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "Transport Request Form – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br><br> Authorization has been issued against  Transport Request Form Form ID # " + lblMaxTransactionID.Text.ToString() + " <br><br> The form can be reviewed at the following URL: " +
                    "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                    "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                    "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                    "<br>User Rights Application<br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();

                    lblmessage.Text = " has been saved against  Form ID # " + lblMaxTransactionID.Text;

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
                    url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                    urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "Transport Request Form – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ",<br> <br> A Transport Request Form against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br> <br> You are requested to provide authorization for the information on the following URL:,<br> <br> A Transport Request Form against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br> <br> You are requested to provide authorization for the information on the following URL:,<br> <br> A Transport Request Form against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br> <br> You are requested to provide authorization for the information on the following URL:,<br> <br> A Transport Request Form against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br> <br> You are requested to provide authorization for the information on the following URL:,<br> <br> A Transport Request Form against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br> <br> You are requested to provide authorization for the information on the following URL: " +
                    "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                    "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                    "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                    "<br>User Rights Application  <br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();
                    lblEmail.Text = "*Transport Request Form against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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
                        EmailSubject = "Transport Request Form – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> A Transport Request Form against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br><br> You can authorized a person on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message.<br>" +
                             "User Rights Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                        lblEmail.Text = "*Transport Request Form against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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
                    url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                    urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "Transport Request Form – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>" + ",<br> <br>  Your Transport Request Form against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been disapproved by  " + ViewState["SessionUser"].ToString() + " <br><br> The reason of rejection is given below you can review your form on following url: " +
                    "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                    "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                    "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>User Rights Application <br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();
                    ViewState["Status"] = "00"; // For Status Reject
                    lblEmail.Text = "*Transport Request Form against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been rejected by you";
                    lblEmail.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    Page.MaintainScrollPositionOnPostBack = true;
                }
            }

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
           (TransactionID,FormCode,UserName,UserEmail,EmailSubject,EmailBody,DateTime,SessionUser) VALUES ('" + TransactionID.ToString() + "','" + FormCode.ToString() + "','" + UserName.ToString() + "','" + UserEmail.ToString() + "','" + EmailSubject.ToString() + "','" + EmailBody.ToString() + "','" + DateTimeNow.ToString() + "','" + SessionUser.ToString() + "')";

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
        #endregion

        ///////////////////////////////////////////////////EmailMethods//////////////////////////////////////////////////////////

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
    }
}