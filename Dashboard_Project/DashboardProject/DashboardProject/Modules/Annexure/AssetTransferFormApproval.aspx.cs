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

namespace ITLDashboard.Modules.Annexure
{
    public partial class AssetTransferFormApproval : System.Web.UI.Page
    {
        public string FormID = "ATFA501";
        private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();



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


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                ddlFromCurrentLocation.BackColor = System.Drawing.Color.AliceBlue;
                ddlFromStore.BackColor = System.Drawing.Color.AliceBlue;
                ddlFromCostCenter.BackColor = System.Drawing.Color.AliceBlue;
                ddlToNewLocation.BackColor = System.Drawing.Color.AliceBlue;
                ddlToStore.BackColor = System.Drawing.Color.AliceBlue;
                ddlToCostCenter.BackColor = System.Drawing.Color.AliceBlue;
                txtTagNo.BackColor = System.Drawing.Color.AliceBlue;
                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                txtQuantity.BackColor = System.Drawing.Color.AliceBlue;
                //Convert.ToDateTime(txtDate.Text).ToString("dd-MM-yyyy");
                //string Date1 = Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy").Trim();
                //txtDate.Text = Date1.ToString();
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
                    txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                    if (Request.QueryString["TransactionNo"] != null)
                    {
                        getDataWhenQueryStringPass();
                        GetHarcheyID();
                        getUserDetail();
                        GetStatusHierachyCategoryControls();
                        BindsysApplicationStatus();
                        if (((string)ViewState["HID"]) == "1")
                        {
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            divEmail.Visible = false;
                            btnReject.Visible = false;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            DisableControls(Page, false);
                        }

                        if (((string)ViewState["HID"]) == "2")
                        {
                            btnApprover.Visible = true;
                            btnReject.Visible = true;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            divEmail.Visible = false;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            DisableControls(Page, false);
                            ClearInputscolor(Page.Controls);
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.Enabled = true;
                            txtRemarks.Enabled = true;
                        }

                        if (((string)ViewState["HID"]) == "4")
                        {
                            btnReviewed.Visible = true;
                            btnApprover.Visible = false;
                            btnReject.Visible = true;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            divEmail.Visible = false;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            DisableControls(Page, false);
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.Enabled = true;
                            ClearInputscolor(Page.Controls);
                            txtRemarks.Enabled = true;
                        }
                    }
                    else
                    {
                        GetTransactionID();
                        getUser();
                        getUserHOD();
                        getUserDetail();
                        BindLocation();
                        BindCostCenter();
                        BindAssetStorageLocation();
                        BindBaseUnitOfMeasure();
                        

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        /////////////////////////////////////////////////Methods///////////////////////////////////////////////////////////
        protected void madatorycolor()
        {

            ddlFromCurrentLocation.BackColor = System.Drawing.Color.AliceBlue;
            ddlFromStore.BackColor = System.Drawing.Color.AliceBlue;
            ddlFromCostCenter.BackColor = System.Drawing.Color.AliceBlue;
            ddlToNewLocation.BackColor = System.Drawing.Color.AliceBlue;
            ddlToStore.BackColor = System.Drawing.Color.AliceBlue;
            ddlToCostCenter.BackColor = System.Drawing.Color.AliceBlue;
            txtTagNo.BackColor = System.Drawing.Color.AliceBlue;
            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
            txtQuantity.BackColor = System.Drawing.Color.AliceBlue;
        }

        private void BindCostCenter()
        {
            ds = obj.BindCostCenter();
            ddlFromCostCenter.DataTextField = ds.Tables["tbl_AssetCostCenter"].Columns["CostCenterDesc"].ToString(); // text field name of table dispalyed in dropdown
            ddlFromCostCenter.DataValueField = ds.Tables["tbl_AssetCostCenter"].Columns["CostCentercode"].ToString();             // to retrive specific  textfield name 
            ddlFromCostCenter.DataSource = ds.Tables["tbl_AssetCostCenter"];      //assigning datasource to the dropdownlist
            ddlFromCostCenter.DataBind();  //binding dropdownlist

            ddlToCostCenter.DataTextField = ds.Tables["tbl_AssetCostCenter"].Columns["CostCenterDesc"].ToString(); // text field name of table dispalyed in dropdown
            ddlToCostCenter.DataValueField = ds.Tables["tbl_AssetCostCenter"].Columns["CostCentercode"].ToString();             // to retrive specific  textfield name 
            ddlToCostCenter.DataSource = ds.Tables["tbl_AssetCostCenter"];      //assigning datasource to the dropdownlist
            ddlToCostCenter.DataBind();  //binding dropdownlist
        }

        private void BindLocation()
        {
            ds = obj.BindLocation();
            ddlFromCurrentLocation.DataTextField = ds.Tables["tbl_Assetlocation"].Columns["CurrentLocationdesc"].ToString(); // text field name of table dispalyed in dropdown
            ddlFromCurrentLocation.DataValueField = ds.Tables["tbl_Assetlocation"].Columns["CurrentLocationcode"].ToString();             // to retrive specific  textfield name 
            ddlFromCurrentLocation.DataSource = ds.Tables["tbl_Assetlocation"];      //assigning datasource to the dropdownlist
            ddlFromCurrentLocation.DataBind();  //binding dropdownlist


            ddlToNewLocation.DataTextField = ds.Tables["tbl_Assetlocation"].Columns["CurrentLocationdesc"].ToString(); // text field name of table dispalyed in dropdown
            ddlToNewLocation.DataValueField = ds.Tables["tbl_Assetlocation"].Columns["CurrentLocationcode"].ToString();             // to retrive specific  textfield name 
            ddlToNewLocation.DataSource = ds.Tables["tbl_Assetlocation"];      //assigning datasource to the dropdownlist
            ddlToNewLocation.DataBind();  //binding dropdownlist
        }

        private void BindAssetStorageLocation()
        {
            ds = obj.BindAssetStorageLocation();
            ddlFromStore.DataTextField = ds.Tables["tblstoragelocation"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
            ddlFromStore.DataValueField = ds.Tables["tblstoragelocation"].Columns["StorageLocationcode"].ToString();             // to retrive specific  textfield name 
            ddlFromStore.DataSource = ds.Tables["tblstoragelocation"];      //assigning datasource to the dropdownlist
            ddlFromStore.DataBind();  //binding dropdownlist

            ddlToStore.DataTextField = ds.Tables["tblstoragelocation"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
            ddlToStore.DataValueField = ds.Tables["tblstoragelocation"].Columns["StorageLocationcode"].ToString();             // to retrive specific  textfield name 
            ddlToStore.DataSource = ds.Tables["tblstoragelocation"];      //assigning datasource to the dropdownlist
            ddlToStore.DataBind();  //binding dropdownlist

        }

        private void BindBaseUnitOfMeasure()
        {

            ds = obj.BindBaseUnitOfMeasure();
            ddlUnitOfMeasure.DataTextField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
            ddlUnitOfMeasure.DataValueField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
            ddlUnitOfMeasure.DataSource = ds.Tables["BaseUnitOfMeasure"];      //assigning datasource to the dropdownlist
            ddlUnitOfMeasure.DataBind();  //binding dropdownlist
            ddlUnitOfMeasure.Items.Insert(0, new ListItem("------Select------", "0"));

        }


        /////////////////////////////////////////////////Methods///////////////////////////////////////////////////////////

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

        ///////////////////////////////////////////////////Methods///////////////////////////////////////////////////////////


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
                btnReviewed.Enabled = false;
                btnCancel.Enabled = false;
                txtRemarksReview.Attributes.Add("disabled", "true");
            }
        }

        private void getUser()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdgetData = new SqlCommand())
                {
                    cmdgetData.CommandText = "SELECT user_name,DisplayName FROM tbluserApprovalHOD where FormID = 'ATFA' and Designation = 'COO'";
                    cmdgetData.CommandType = CommandType.Text;
                    cmdgetData.Connection = connection;
                    connection.Open();
                    ddlCOO.DataSource = cmdgetData.ExecuteReader();
                    ddlCOO.DataTextField = "DisplayName";
                    ddlCOO.DataValueField = "user_name";
                    ddlCOO.DataBind();
                    ddlCOO.Items.Insert(0, new ListItem("------Select------", "0"));
                    connection.Close();

                    cmdgetData.CommandText = "SELECT user_name,DisplayName FROM tbluserMDA where FormName = 'ATFA'";
                    cmdgetData.CommandType = CommandType.Text;
                    cmdgetData.Connection = connection;
                    connection.Open();

                    ddlReviewer.DataSource = cmdgetData.ExecuteReader();
                    ddlReviewer.DataTextField = "DisplayName";
                    ddlReviewer.DataValueField = "user_name";
                    ddlReviewer.DataBind();
                    ddlReviewer.Items.Insert(0, new ListItem("------Select------", "0"));
                    connection.Close();

                    cmdgetData.CommandText = "SELECT user_name,DisplayName FROM tbluserApprovalHOD where FormID = 'ATFA' and Designation = 'CFO'";
                    cmdgetData.CommandType = CommandType.Text;
                    cmdgetData.Connection = connection;
                    connection.Open();

                    ddlCFO.DataSource = cmdgetData.ExecuteReader();
                    ddlCFO.DataTextField = "DisplayName";
                    ddlCFO.DataValueField = "user_name";
                    ddlCFO.DataBind();
                    ddlCFO.Items.Insert(0, new ListItem("------Select------", "0"));
                    connection.Close();
                    ddlCFO.SelectedIndex = 1;
                    ddlCOO.SelectedIndex = 1;
                    ddlReviewer.SelectedIndex = 1;

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

        protected void getDataWhenQueryStringPass()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdGetData = new SqlCommand())
                {
                    string TI = Request.QueryString["TransactionNo"].ToString().Trim();
                    getUser();
                    getUserHOD();
                    getUserDetail();
                    BindLocation();
                    BindCostCenter();
                    BindAssetStorageLocation();
                    BindBaseUnitOfMeasure();

                    cmd.CommandText = "";
                    cmd.CommandText = "SELECT * from tbl_AssetTransferForm where TransactionMain = @TI";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@TI", TI.ToString().Trim());
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Data");
                    if (ds.Tables["Data"].Rows.Count > 0)
                    {
                        lblMaxTransactionNo.Text = ds.Tables["Data"].Rows[0]["TransactionMain"].ToString().Trim();
                        lblMaxTransactionID.Text = ds.Tables["Data"].Rows[0]["TransactionID"].ToString().Trim();
                        txtDate.Text = ds.Tables["Data"].Rows[0]["Date"].ToString().Trim();
                        txtDescription.Text = ds.Tables["Data"].Rows[0]["Description"].ToString().Trim();
                        ddlFromCurrentLocation.SelectedValue = ds.Tables["Data"].Rows[0]["FromCurrentLocation"].ToString().Trim();
                        ddlFromStore.SelectedValue = ds.Tables["Data"].Rows[0]["FromStore"].ToString().Trim();
                        ddlFromCostCenter.SelectedValue = ds.Tables["Data"].Rows[0]["FromCostCenter"].ToString().Trim();
                        ddlToNewLocation.SelectedValue = ds.Tables["Data"].Rows[0]["ToNewLocation"].ToString().Trim();
                        ddlToStore.SelectedValue = ds.Tables["Data"].Rows[0]["ToStore"].ToString().Trim();
                        ddlToCostCenter.SelectedValue = ds.Tables["Data"].Rows[0]["ToCostCenter"].ToString().Trim();
                        txtItemDescription.Text = ds.Tables["Data"].Rows[0]["ItemDescription"].ToString().Trim();
                        txtTagNo.Text = ds.Tables["Data"].Rows[0]["TagNo"].ToString().Trim();
                        txtSerialNo.Text = ds.Tables["Data"].Rows[0]["SerialNo"].ToString().Trim();
                        txtModelNoFarCodePartNo.Text = ds.Tables["Data"].Rows[0]["ModelNoFarCodePartNo"].ToString().Trim();
                        txtMake.Text = ds.Tables["Data"].Rows[0]["Make"].ToString().Trim();
                        ddlUnitOfMeasure.SelectedValue = ds.Tables["Data"].Rows[0]["UnitOfMeasure"].ToString().Trim();
                        txtQuantity.Text = ds.Tables["Data"].Rows[0]["Quantity"].ToString().Trim();
                        txtRemarksAD.Text = ds.Tables["Data"].Rows[0]["RemarksAD"].ToString().Trim();

                    }
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
               
                if (ddlFromCurrentLocation.SelectedValue == "0")
                {
                    lblUpError.Text = "Current Location field should not be left blank!.";
                    error.Visible = true;
                    ddlFromCurrentLocation.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                if (ddlFromStore.SelectedValue == "0")
                {
                    lblUpError.Text = "From Store field should not be left blank!.";
                    error.Visible = true;
                    ddlFromStore.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                if (ddlFromCostCenter.SelectedValue == "0")
                {
                    lblUpError.Text = "From Cost Center field should not be left blank!.";
                    error.Visible = true;
                    ddlFromCostCenter.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                if (ddlToNewLocation.SelectedValue == "0")
                {
                    lblUpError.Text = "To New Location field should not be left blank!.";
                    error.Visible = true;
                    ddlToNewLocation.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                if (ddlToStore.SelectedValue == "0")
                {
                    lblUpError.Text = "To Store field should not be left blank!.";
                    error.Visible = true;
                    ddlToStore.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                if (ddlToCostCenter.SelectedValue == "0")
                {
                    lblUpError.Text = "To Cost Center field should not be left blank!.";
                    error.Visible = true;
                    ddlToCostCenter.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                if (txtTagNo.Text == "")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtTagNo.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                if (ddlCOO.SelectedValue == "0")
                {
                    lblUpError.Text = "Select Any Chief Operating Officer!.";
                    error.Visible = true;
                    ddlCOO.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (ddlCFO.SelectedValue == "0")
                {
                    lblUpError.Text = "Select Any Chief Finance Officer!.";
                    error.Visible = true;
                    ddlCFO.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (ddlReviewer.SelectedValue == "0")
                {
                    lblUpError.Text = "Select Any Specific Reviewer (FI)!.";
                    error.Visible = true;
                    ddlReviewer.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else
                {

                    string one = ViewState["HOD"].ToString();
                    string two = ddlCOO.SelectedValue;
                    string three = ddlCFO.SelectedValue;
                    string Reviwer = ddlReviewer.SelectedValue;

                    string finale = one + "," + two + "," + three;
                    cmd.CommandText = "";
                    cmd.CommandText = "SP_SYS_AssetTransferForm";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                    cmd.Parameters.AddWithValue("@Date", txtDate.Text);
 

                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@FromCurrentLocation", ddlFromCurrentLocation.SelectedValue);
                    cmd.Parameters.AddWithValue("@FromStore", ddlFromStore.SelectedValue);
                    cmd.Parameters.AddWithValue("@FromCostCenter", ddlFromCostCenter.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToNewLocation", ddlToNewLocation.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToStore", ddlToStore.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToCostCenter", ddlToCostCenter.SelectedValue);
                    cmd.Parameters.AddWithValue("@ItemDescription", txtItemDescription.Text);
                    cmd.Parameters.AddWithValue("@TagNo", txtTagNo.Text);
                    cmd.Parameters.AddWithValue("@SerialNo", txtSerialNo.Text);
                    cmd.Parameters.AddWithValue("@ModelNoFarCodePartNo", txtModelNoFarCodePartNo.Text);
                    cmd.Parameters.AddWithValue("@Make", txtMake.Text);
                    cmd.Parameters.AddWithValue("@UnitOfMeasure", ddlUnitOfMeasure.SelectedValue);
                    cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                    cmd.Parameters.AddWithValue("@RemarksAD", txtRemarksAD.Text);
                    cmd.Parameters.AddWithValue("@APPROVAL", finale.ToString());
                    cmd.Parameters.AddWithValue("@MDA", ddlReviewer.SelectedValue);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["User_Name"].ToString());
                    cmd.Parameters.AddWithValue("@Remarks", txtRemarksReview.Text);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Message");
                    sucess.Visible = true;
                    if (ds.Tables["Message"].Rows.Count > 0)
                    {
                        string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                        lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
                        lblmessage.Text = message + " # " + lblMaxTransactionID.Text;
                        EmailWorkSendFirstApproval();
                        lblmessage.Focus();
                        error.Visible = false;
                        Page.MaintainScrollPositionOnPostBack = false;
                        GetTransactionID();
                        ClearInputs(Page.Controls);
                    }
                    else
                    {
                        lblUpError.Text = "Please fill the required data in list.";
                        error.Visible = true;
                        Page.MaintainScrollPositionOnPostBack = false;
                        return;
                    }


                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {

                {
                    getDataWhenQueryStringPass();
                    sucess.Visible = false;
                    lblmessage.Text = "";
                    EmailWorkSendApprovalFromApproval();
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

        protected void btnReviewed_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    EmailWorkSendToAllfromReviwer();
                    InsertEmailHOD();
                    ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            Response.Redirect(url.ToString());
            ClearInputs(Page.Controls);
            // setinitialrow();
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
                    EmailWorkSendFirstApprovalOnRejection();
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

        ////////////////////////////////////////////////Email Working////////////////////////////////////////////////////////

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
                        EmailSubject = "Asset Transfer Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " have sent you a Asset Transfer Form Request against  Form ID # " + lblMaxTransactionID.Text + " for approval. <br><br> Your kind approval is required on the following URL: " +
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

        private void EmailWorkSendFirstApprovalOnRejection()
        {
            try
            {
                string HierachyCategoryStatus = "00"; // Allow based on reqierment if there is No MDA if other wise allow "4"//
                ds = obj.MailForwardToUserOnRejection(lblMaxTransactionID.Text, FormID.ToString());

                if (ds.Tables["MailForwardToUserOnRejection"].Rows.Count > 0)
                {

                    DataTableReader reader = ds.Tables["MailForwardUserToApprover"].CreateDataReader();
                    while (reader.Read())
                    {
                        url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                        urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                        TransactionIDEmail = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Asset Transfer Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br>  Your Asset Transfer Form Request against  Form ID # " + lblMaxTransactionID.Text + " has been disapproved by  " + ViewState["SessionUser"].ToString() + " <br><br> The reason of rejection is given below you can review your form on following url:" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "<br> <br> <br>Reject Remarks: " + txtRemarksReview.Text +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Assets Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblEmail.Text = "Asset Transfer Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been rejected by you";
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
                        EmailSubject = "Asset Transfer Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";            
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br>  Your Asset Transfer Form Request against  Form ID # " + lblMaxTransactionID.Text + " has been reviewed by  " + ViewState["SessionUser"].ToString() + " <br><br> This form can review on following url:" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Assets Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblmessage.Text = "Asset Transfer Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been reviewed by you";
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
                        EmailSubject = "Asset Transfer Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br>  Asset Transfer Form Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br><br> You are requested to Approve Asset Transfer Form Request on the following URL: " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Assets Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblEmail.Text = "Asset Transfer Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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
                            EmailSubject = "Asset Transfer Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                            EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> Asset Transfer Form Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr. " + ViewState["SessionUser"].ToString().Replace(".", " ") + " <br><br> You are requested to review the Asset Transfer Form information on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                                 "<br>Assets Application<br> Information Systems Dashboard";
                            SessionUser = Session["User_Name"].ToString();
                            DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            InsertEmail();
                            ViewState["Status"] = HierachyCategoryStatus.ToString();
                            lblEmail.Text = "Asset Transfer Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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

        //////////////////////////////////////////////////Email Working////////////////////////////////////////////////////////


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
    }
}