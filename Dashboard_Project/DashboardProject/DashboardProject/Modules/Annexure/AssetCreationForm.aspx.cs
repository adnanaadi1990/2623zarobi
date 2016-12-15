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

namespace ITLDashboard.Modules.Annexure
{
    public partial class AssetCreationForm : System.Web.UI.Page
    {
        public string FormID = "ACFA01";
        private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();

        public string TransactionID = "";
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
        public string User_ID = "";
        public string FormName = "";
        public string Form_ID = "";
        public string urlMobile = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            txtRemarksReview.Enabled = true;
            if (!IsPostBack)
            {
                //txtRemarksReview.Visible = false;
                dvManufacturer.Visible = true;
                DvSerialNumber.Visible = true;
                DvAUCNumber.Visible = true;
                ddlPlant.BackColor = System.Drawing.Color.AliceBlue;
                ddlLocation.BackColor = System.Drawing.Color.AliceBlue;
                ddlCostCenter.BackColor = System.Drawing.Color.AliceBlue;
                txtQuantity.BackColor = System.Drawing.Color.AliceBlue;
                txtAssetDescription.BackColor = System.Drawing.Color.AliceBlue;
                txtManufacturer.BackColor = System.Drawing.Color.AliceBlue;
                txtSerialNumber.BackColor = System.Drawing.Color.AliceBlue;
                txtAUCNumber.BackColor = System.Drawing.Color.AliceBlue;
                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                txtAssetNo.BackColor = System.Drawing.Color.AliceBlue;

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
                        //dvType.Visible = false;
                        getDataWhenQueryStringPass();
                        GetHarcheyID();
                        getUserDetail();
                        GetStatusHierachyCategoryControls();
                        BindsysApplicationStatus();

                        if (((string)ViewState["HID"]) == "1")
                        {
                            rbAction.Items[0].Enabled = false;
                            rbAction.Items[1].Enabled = false;
                            rbAction.Items[0].Attributes.Add("disabled", "disabled");
                            rbAction.Items[1].Attributes.Add("disabled", "disabled");
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            divEmail.Visible = false;
                            btnReject.Visible = false;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            DisableControls(Page, false);
                            txtAssetNo.Enabled = false;
                            dvAssetNo.Visible = true;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                        }
                        if (((string)ViewState["HID"]) == "2")
                        {
                            rbAction.Enabled = false;
                            rbAction.Items[0].Enabled = false;
                            rbAction.Items[0].Enabled = false;
                            rbAction.Items[1].Enabled = false;
                            rbAction.Items[0].Attributes.Add("disabled", "disabled");
                            rbAction.Items[1].Attributes.Add("disabled", "disabled");
                            btnApprover.Visible = true;
                            btnReject.Visible = true;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            divEmail.Visible = false;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            DisableControls(Page, false);
                            ClearInputscolor(Page.Controls);
                            txtRemarks.Enabled = true;
                            txtAssetNo.Enabled = false;
                            dvAssetNo.Visible = true;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.Enabled = true;


                            for (int i = 0; i < ddlPlant.Items.Count; i++)
                            {
                                ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                            }

                        }
                        if (((string)ViewState["HID"]) == "3")
                        {
                            rbAction.Enabled = false;
                            rbAction.Items[0].Enabled = false;

                            rbAction.Items[0].Enabled = false;
                            rbAction.Items[1].Enabled = false;
                            rbAction.Items[0].Attributes.Add("disabled", "disabled");
                            rbAction.Items[1].Attributes.Add("disabled", "disabled");
                            btnApprover.Visible = false;
                            btnReject.Visible = true;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            divEmail.Visible = false;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            DisableControls(Page, false);
                            ClearInputscolor(Page.Controls);
                            txtRemarks.Enabled = true;
                            txtAssetNo.Enabled = false;
                            dvAssetNo.Visible = true;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.Enabled = true;

                            for (int i = 0; i < ddlPlant.Items.Count; i++)
                            {
                                ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                            }

                        }
                        if (((string)ViewState["HID"]) == "4")
                        {
                            rbAction.Enabled = false;
                            rbAction.Items[0].Enabled = false;

                            rbAction.Items[0].Enabled = false;
                            rbAction.Items[1].Enabled = false;
                            rbAction.Items[0].Attributes.Add("disabled", "disabled");
                            rbAction.Items[1].Attributes.Add("disabled", "disabled");
                            dvAssetNo.Visible = true;
                            btnSaveSubmit.Visible = true;
                            btnApprover.Visible = false;
                            btnReject.Visible = true;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            divEmail.Visible = false;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            DisableControls(Page, false);
                            ClearInputscolor(Page.Controls);
                            txtAssetNo.Enabled = true;
                            dvAssetNo.Visible = true;
                            txtRemarks.Enabled = true;
                            txtAssetNo.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.Enabled = true;


                            for (int i = 0; i < ddlPlant.Items.Count; i++)
                            {
                                ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                            }
                        }
                    }
                    else
                    {
                        GetTransactionID();
                        BindPlant();
                        BindLocation();
                        BindCostCenter();
                        getUser();
                        getUserHOD();
                        getUserDetail();

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
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
                if (ddlPlant.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlPlant.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (txtAssetDescription.Text == "")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtAssetDescription.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (txtQuantity.Text == "")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtQuantity.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (ddlCostCenter.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlCostCenter.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (ddlLocation.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlLocation.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else if (txtManufacturer.Text == "" && rbAction.SelectedValue == "Asset")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtManufacturer.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtSerialNumber.Text == "" && rbAction.SelectedValue == "Asset")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtSerialNumber.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtAUCNumber.Text == "" && rbAction.SelectedValue == "Asset")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtAUCNumber.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }                   
                else if (ddlEmailMDA.SelectedValue == "0")
                {
                    lblUpError.Text = "Select any Finance Department!.";
                    error.Visible = true;
                    ddlEmailMDA.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else
                {
                    string Result = "";
                    string Notification = "";

                    Result = ViewState["HOD"].ToString();
                    //+ "," + ddlReviewer.SelectedValue + "," + ddlFC.SelectedValue

                    cmd.CommandText = "";
                    cmd.CommandText = "SP_SYS_AssetCreationForm";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                    cmd.Parameters.AddWithValue("@Type", rbAction.SelectedValue);
                    cmd.Parameters.AddWithValue("@AssetDescription", txtAssetDescription.Text);
                    cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                    cmd.Parameters.AddWithValue("@CostCenter", ddlCostCenter.SelectedValue);
                    cmd.Parameters.AddWithValue("@Plant", ddlPlant.SelectedValue);
                    cmd.Parameters.AddWithValue("@Location", ddlLocation.SelectedValue);
                    cmd.Parameters.AddWithValue("@Reservationnumber", txtReservationNumber.Text);
                    cmd.Parameters.AddWithValue("@Manufacturer", txtManufacturer.Text);
                    cmd.Parameters.AddWithValue("@SerialNumber", txtSerialNumber.Text);
                    cmd.Parameters.AddWithValue("@AUCNumber", txtAUCNumber.Text);
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

        protected void btnSaveSubmit_Click(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = false;
           // txtAssetNo.BackColor = System.Drawing.Color.White;
            lblError.Text = "";
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
                if (txtAssetNo.Text == "")
                {
                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "Asset No should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtAssetNo.BackColor = System.Drawing.Color.Red;
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }


                cmd.CommandText = @"Select AssetNo from tbl_AssetCreationForm where AssetNo = '" + txtAssetNo.Text.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                adp.SelectCommand = cmd;
                dt.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "Asset No " + txtAssetNo.Text + " already exist!. Please provide a specific code";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtAssetNo.BackColor = System.Drawing.Color.Red;
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "";
                    sucess.Visible = false;
                    error.Visible = false;
                    ds = obj.UpdateAssetNo(lblMaxTransactionID.Text, txtAssetNo.Text.Trim());
                    //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup2();", true);
                    //lblmessage.Text = "SAP Material Code " + txtSMC.Text.Trim() + " has been saved against  Form ID # " + Request.QueryString["TransactionNo"].ToString();

                    lblmessage.Visible = true;
                    EmailWorkSendMDA();
                    InsertEmailHOD();
                    ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                    getDataWhenQueryStringPass();
                    sucess.Visible = true;
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "MDA" + ex.ToString();
            }

        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {

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
                //    txtRemarksReview.BackColor = System.Drawing.Color.Red;
                //    Page.MaintainScrollPositionOnPostBack = false;
                //    return;
                //}
                //else
                {
                    getDataWhenQueryStringPass();
                    sucess.Visible = false;
                    lblmessage.Text = "";
                    EmailWorkApproval();
                    ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Approver" + ex.ToString();
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
                    EmailReject();
                    ClosedFormAfterReject();
                    //  ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                    Page.MaintainScrollPositionOnPostBack = true;
                    txtRemarksReview.BackColor = System.Drawing.Color.White;
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }


        /////////////////////////////////////////////Methods////////////////////////////////////////////////////////////////////////////

        protected void rdAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbAction.SelectedValue == "Asset")
            {
                dvManufacturer.Visible = true;
                DvSerialNumber.Visible = true;
                DvAUCNumber.Visible = true;
                MaintainScrollPositionOnPostBack = true;

                txtManufacturer.Text = "";
                txtSerialNumber.Text = "";
                txtAUCNumber.Text = "";
                txtManufacturer.BackColor = System.Drawing.Color.AliceBlue;
                BindPlant();
            }
            else if (rbAction.SelectedValue == "AUC")
            {
                dvManufacturer.Visible = true;
                DvSerialNumber.Visible = false;
                DvAUCNumber.Visible = false;
                MaintainScrollPositionOnPostBack = true;

                txtManufacturer.Text = "";
                txtSerialNumber.Text = "";
                txtManufacturer.BackColor = System.Drawing.Color.White;
                ddlPlant.Items.Clear();
                BindPlant();
            }
        }

        protected void madatorycolor()
        {
            ddlPlant.BackColor = System.Drawing.Color.AliceBlue;
            ddlLocation.BackColor = System.Drawing.Color.AliceBlue;
            ddlCostCenter.BackColor = System.Drawing.Color.AliceBlue;
            txtQuantity.BackColor = System.Drawing.Color.AliceBlue;
            txtAssetDescription.BackColor = System.Drawing.Color.AliceBlue;
            txtReservationNumber.BackColor = System.Drawing.Color.AliceBlue;
            txtReservationNumber.BackColor = System.Drawing.Color.AliceBlue;
            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
            txtAssetNo.BackColor = System.Drawing.Color.AliceBlue;
        }

        private void BindPlant()
        {

            ds = obj.getPlantDistinct();
            ddlPlant.DataTextField = ds.Tables["getPlantDistinct"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
            ddlPlant.DataValueField = ds.Tables["getPlantDistinct"].Columns["PlantId"].ToString();             // to retrive specific  textfield name 
            ddlPlant.DataSource = ds.Tables["getPlantDistinct"];      //assigning datasource to the dropdownlist
            ddlPlant.DataBind();  //binding dropdownlist
            ddlPlant.Items.Insert(0, new ListItem("------Select------", "0"));
            if (rbAction.SelectedValue == "Asset")
            {
                ListItem removeItem = ddlPlant.Items.FindByValue("5000");
                ddlPlant.Items.Remove(removeItem);
            }
        }

        private void BindCostCenter()
        {
            ds = obj.BindCostCenter();
            ddlCostCenter.DataTextField = ds.Tables["tbl_AssetCostCenter"].Columns["CostCenterDesc"].ToString(); // text field name of table dispalyed in dropdown
            ddlCostCenter.DataValueField = ds.Tables["tbl_AssetCostCenter"].Columns["CostCentercode"].ToString();             // to retrive specific  textfield name 
            ddlCostCenter.DataSource = ds.Tables["tbl_AssetCostCenter"];      //assigning datasource to the dropdownlist
            ddlCostCenter.DataBind();  //binding dropdownlist
        }

        private void BindLocation()
        {
            ds = obj.BindLocation();
            ddlLocation.DataTextField = ds.Tables["tbl_Assetlocation"].Columns["CurrentLocationdesc"].ToString(); // text field name of table dispalyed in dropdown
            ddlLocation.DataValueField = ds.Tables["tbl_Assetlocation"].Columns["CurrentLocationcode"].ToString();             // to retrive specific  textfield name 
            ddlLocation.DataSource = ds.Tables["tbl_Assetlocation"];      //assigning datasource to the dropdownlist
            ddlLocation.DataBind();  //binding dropdownlist
        }

        private void GetTransactionID()
        {
            ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
            lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
            ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();

            cmd.CommandText = "";
            cmd.CommandText = @"SELECT COALESCE(MAX(TransactionID), 0)  +1 as TransactionID from tbl_AssetTransferFormApprovalDetail";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            adp.Fill(ds, "TransactionID");
            lblMaxTransactionID.Text = ds.Tables["TransactionID"].Rows[0]["TransactionID"].ToString();
        }

        private void GetHarcheyID()
        {
            ds = obj.GetHarachyCustomerMaster(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
            dt = ds.Tables["HID"];
            ViewState["HID"] = "1";
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

        private void getUser()
        {
            cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserMDA where FormName = 'CM'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlEmailMDA.DataSource = cmd.ExecuteReader();
            ddlEmailMDA.DataTextField = "DisplayName";
            ddlEmailMDA.DataValueField = "user_name";
            ddlEmailMDA.DataBind();
            ddlEmailMDA.Items.Insert(0, new ListItem("------Select------", "0"));
            conn.Close();
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

        private void BindsysApplicationStatus()
        {
            ds = obj.BindsysApplicationStatus(lblMaxTransactionID.Text, FormID.ToString());
            grdWStatus.DataSource = ds.Tables["BindsysApplicationStatus"];
            grdWStatus.DataBind();
            grdWStatus.Visible = true;
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
                ClearInputscolor(ctrlsss.Controls);
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
                btnReject.Attributes.Add("disabled", "true");
                btnApprover.Enabled = false;
                btnSaveSubmit.Enabled = false;
                btnCancel.Enabled = false;
                txtAssetNo.Enabled = false;
                txtRemarksReview.Attributes.Add("disabled", "true");
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

        protected void getDataWhenQueryStringPass()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdGetData = new SqlCommand())
                {
                    string TI = Request.QueryString["TransactionNo"].ToString().Trim();
                    BindPlant();
                    BindLocation();
                    BindCostCenter();
                    getUser();
                    getUserHOD();
                    getUserDetail();

                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT * from tbl_AssetCreationForm where TransactionMain = @TI";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@TI", TI.ToString().Trim());
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Data");
                    if (ds.Tables["Data"].Rows.Count > 0)
                    {
                        lblMaxTransactionNo.Text = ds.Tables["Data"].Rows[0]["TransactionMain"].ToString().Trim();
                        lblMaxTransactionID.Text = ds.Tables["Data"].Rows[0]["TransactionID"].ToString().Trim();
                        txtAssetNo.Text = ds.Tables["Data"].Rows[0]["AssetNo"].ToString().Trim();

                        //txtAssetNo.Text = ds.Tables["Data"].Rows[0]["AssetNo"].ToString().Trim();

                        rbAction.SelectedValue = ds.Tables["Data"].Rows[0]["Type"].ToString().Trim();
                        txtAssetDescription.Text = ds.Tables["Data"].Rows[0]["AssetDescription"].ToString().Trim();
                        txtQuantity.Text = ds.Tables["Data"].Rows[0]["Quantity"].ToString().Trim();
                        ddlCostCenter.SelectedValue = ds.Tables["Data"].Rows[0]["CostCenter"].ToString().Trim();
                        ddlPlant.SelectedValue = ds.Tables["Data"].Rows[0]["Plant"].ToString().Trim();
                        ddlLocation.SelectedValue = ds.Tables["Data"].Rows[0]["Location"].ToString().Trim();
                        txtReservationNumber.Text = ds.Tables["Data"].Rows[0]["ReservationNumber"].ToString().Trim();
                        txtManufacturer.Text = ds.Tables["Data"].Rows[0]["Manufacturer"].ToString().Trim();
                        txtSerialNumber.Text = ds.Tables["Data"].Rows[0]["SerialNumber"].ToString().Trim();
                        txtAUCNumber.Text = ds.Tables["Data"].Rows[0]["AUCNumber"].ToString().Trim();

                        for (int i = 0; i < ddlPlant.Items.Count; i++)
                        {
                            ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                        }

                        if (rbAction.SelectedValue == "Asset")
                        {
                            dvManufacturer.Visible = true;
                            DvSerialNumber.Visible = true;
                            DvAUCNumber.Visible = true;
                            MaintainScrollPositionOnPostBack = true;
                            txtManufacturer.BackColor = System.Drawing.Color.AliceBlue;
                        }
                        else
                        {
                            dvManufacturer.Visible = true;
                            DvSerialNumber.Visible = false;
                            DvAUCNumber.Visible = false;
                            MaintainScrollPositionOnPostBack = true;
                            txtManufacturer.BackColor = System.Drawing.Color.White;
                        }
                    }
                }
            }
        }


        /////////////////////////////////////////////Methods////////////////////////////////////////////////////////////////////////////

        #region Email Working
        ///////////////////////////////////////////////////Email Working///////////////////////////////////////////////////////////

        private void EmailWorkSendFirstApproval()
        {
            try
            {//
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
                        EmailSubject = "Asset Creation Request – Form ID #" + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has sent you a Asset Creation Request against  Form ID # " + lblMaxTransactionID.Text + " for approval. <br><br> Your kind approval is required on the following URL: " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Assets Application<br> Information Systems Dashboard";
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

        private void EmailWorkSendMDA()
        {
            try
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
                        EmailSubject = "Asset Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + "<br> <br> Asset No " + txtAssetNo.Text.Trim() + " has been issued against  Asset Creation Request Form ID # " + lblMaxTransactionID.Text +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Assets Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();


                        lblmessage.Text = "Asset No " + txtAssetNo.Text.Trim() + " has been saved against  Form ID # " + lblMaxTransactionID.Text;
                        lblmessage.ForeColor = System.Drawing.Color.Green;
                        conn.Close();
                        sucess.Visible = true;
                        error.Visible = false;
                        lblmessage.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        txtAssetNo.BackColor = System.Drawing.Color.White;
                        Page.MaintainScrollPositionOnPostBack = false;


                    }
                }
                else
                {

                }
            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "Email MDA" + ex.ToString();
            }
        }

        private void EmailReject()
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
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "Asset Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br> Asset Creation Request against  Form ID # " + lblMaxTransactionID.Text + " has been disapproved by  " + ViewState["SessionUser"].ToString().Replace(".", " ") + " <br> The reason of rejection is given below you can review your form on following url:" +
                    "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                    "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                    "<br> <br> <br>Reject Remarks: " + txtRemarksReview.Text +
                    "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                    "<br>Assets Application<br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();
                    ViewState["Status"] = HierachyCategoryStatus.ToString();
                    lblEmail.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    Page.MaintainScrollPositionOnPostBack = true;
                    lblEmail.Text = "*Asset Creation Request against  Form ID # " + lblMaxTransactionID.Text + " has been rejected by you";
                    dvemaillbl.Visible = true;
                }
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
                        TransactionID = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Asset Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br> Asset Creation Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr. " + ViewState["SessionUser"].ToString().Replace(".", " ") + " <br><br> You are requested to Approve the Asset Creation Request on the following URL:" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Assets Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblEmail.Focus();
                        Page.MaintainScrollPositionOnPostBack = true;
                        lblEmail.Text = "*Asset Creation Request against  Form ID # " + lblMaxTransactionID.Text + " has been approved by you";
                        dvemaillbl.Visible = true;

                    }
                }
                else
                {
                    if (ds.Tables["MailForwardFormApprover"].Rows.Count > 0)
                    {
                        // Allow based on reqierment if there is No MDA if other wise allow "4"//
                        while (reader.Read())
                        {
                            url = Request.Url.ToString();
                            TransactionID = reader["TransactionID"].ToString();
                            FormCode = reader["FormID"].ToString();
                            UserName = reader["user_name"].ToString();
                            UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                            EmailSubject = "Asset Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                            EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> Asset Creation Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr. " + ViewState["SessionUser"].ToString().Replace(".", " ") + " <br><br> You are requested to Asset code information on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                                 "<br>Assets Application<br> Information Systems Dashboard";
                            SessionUser = Session["User_Name"].ToString();
                            DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            InsertEmail();
                            ViewState["Status"] = HierachyCategoryStatus.ToString();
                            lblEmail.Focus();
                            Page.MaintainScrollPositionOnPostBack = false;
                            Page.MaintainScrollPositionOnPostBack = true;
                            lblEmail.Text = "*Asset Creation Request against  Form ID # " + lblMaxTransactionID.Text + " has been approved by you";
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

        ///////////////////////////////////////////////////EmailMethods//////////////////////////////////////////////////////////
    }
}