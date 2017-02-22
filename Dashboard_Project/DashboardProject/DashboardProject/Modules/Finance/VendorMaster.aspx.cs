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
namespace ITLDashboard.Modules.Finance
{
    public partial class VendorMaster : System.Web.UI.Page
    {
        public string PdfPath;
        public string FormID = "301";
        public string FilePath = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsEmail = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        ComponentClass obj = new ComponentClass();

        public string TransactionID = "";
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
            try
            {
                Page.MaintainScrollPositionOnPostBack = true;
                txtRemarksReview.Enabled = true;
                if (!IsPostBack)
                {

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
                            divEmail.Visible = false;
                            getDataWhenQueryStringPass();
                            GetHarcheyID();
                            getUserDetail();
                            GetStatusHierachyCategoryControls();
                            BindsysApplicationStatus();
                            ClearInputscolor(Page.Controls);

                            if (((string)ViewState["HID"]) == "1")
                            {
                                btnCancel.Visible = false;
                                dvVendorCode.Visible = false;
                                btnSave.Visible = false;
                                btnMDA.Visible = false;
                                btnCancel.Visible = false;
                                btnMDA.Visible = false;
                                dvVendorCode.Visible = false;
                                DisableControls(Page, false);
                                dvAD.Visible = false;
                                dvPD.Visible = false;
                                dvWTR.Visible = false;
                                dvFormID.Visible = true;
                                dvTransactionNo.Visible = false;
                                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                                DisableControls(Page, false);
                            }
                            if (((string)ViewState["HID"]) == "4")
                            {
                                DisableControl();
                                btnSave.Visible = false;
                                btnMDA.Visible = false;
                                btnCancel.Visible = false;
                                btnMDA.Visible = true;
                                dvVendorCode.Visible = true;
                                txtVendorCode.Enabled = true;

                                dvAD.Visible = true;
                                dvPD.Visible = true;
                                dvWTR.Visible = true;
                                dvFormID.Visible = true;
                                dvTransactionNo.Visible = false;
                                btnReject.Visible = true;
                                txtVendorCode.BackColor = System.Drawing.Color.AliceBlue;
                                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                                txtRemarksReview.Enabled = true;


                            }
                            if (((string)ViewState["HID"]) == "2")
                            {
                                DisableControl();
                                btnSave.Visible = false;
                                btnMDA.Visible = false;
                                btnCancel.Visible = false;
                                btnMDA.Visible = false;
                                dvVendorCode.Visible = false;

                                txtVendorCode.Enabled = true;
                                dvAD.Visible = false;
                                dvPD.Visible = false;
                                dvWTR.Visible = false;
                                dvFormID.Visible = true;
                                dvTransactionNo.Visible = false;
                                btnApproved.Visible = true;
                                btnReject.Visible = true;
                                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                                txtRemarksReview.Enabled = true;

                            }
                            if (((string)ViewState["HID"]) == "5")
                            {
                                DisableControl();
                                btnSave.Visible = false;
                                btnMDA.Visible = false;
                                btnCancel.Visible = false;
                                btnMDA.Visible = false;
                                dvVendorCode.Visible = true;
                                txtVendorCode.Enabled = true;
                                dvAD.Visible = true;
                                dvPD.Visible = true;
                                dvWTR.Visible = true;
                                dvFormID.Visible = true;
                                dvTransactionNo.Visible = false;
                                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                                txtRemarksReview.Enabled = true;
                            }
                        }
                        else
                        {
                            getUserHOD();
                            getSortKey();
                            getReconAccount();
                            BindEmailUser();
                            GetTransactionID();
                            getAccountGroup();
                            getTermsofpayment();
                            getWHTaxType();
                            getPurchasingOrg();
                            madatorycolor();
                            getUserDetail();
                            getCountry();
                            getRegion();

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Page_Load" + ex.ToString();
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

                else if (ddlTitle.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlTitle.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtName.Text == "")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtName.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (ddlPurchasingOrganization.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlPurchasingOrganization.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (ddlCompanyCode.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlCompanyCode.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtStreet.Text == "")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtStreet.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (ddlCountry.SelectedValue == "0")
                {
                    lblUpError.Text = "Please select any country!.";
                    error.Visible = true;
                    ddlCountry.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                //else if (txtTaxPayerCNIC.Text == "")
                //{
                //    lblUpError.Text = "Fill all required field!.";
                //    error.Visible = true;
                //    txtTaxPayerCNIC.BackColor = System.Drawing.Color.Red;
                //    Page.MaintainScrollPositionOnPostBack = false;
                //    return;
                //}
                //else if (txtTaxPayerNTN.Text == "")
                //{
                //    lblUpError.Text = "Fill all required field!.";
                //    error.Visible = true;
                //    txtTaxPayerNTN.BackColor = System.Drawing.Color.Red;
                //    Page.MaintainScrollPositionOnPostBack = false;
                //    return;
                //}

                else if (ddlEmailApproval2nd.SelectedValue == "0")
                {
                    lblUpError.Text = "Select Procurement Department (Authority) field!.";
                    error.Visible = true;
                    ddlEmailMDA.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (ddlEmailMDA.SelectedValue == "0")
                {
                    lblUpError.Text = "Select MDA(Finance Department) field!.";
                    error.Visible = true;
                    ddlEmailMDA.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else
                {


                    string Approval = ViewState["HOD"].ToString() + "," + ddlEmailApproval2nd.SelectedValue.Trim();
                    string CreatedBy = Session["User_Name"].ToString();


                    cmd.CommandText = "Exec SP_SYS_CreateVendorMaster" + " @TransactionMain = '" + lblMaxTransactionNo.Text + "', " +
                    " @Title = '" + ddlTitle.Text + "', " +
                    " @Name = '" + txtName.Text.Replace("'", "") + "', " +
                    " @PurchasingOrganization = '" + ddlPurchasingOrganization.SelectedValue.ToString() + "', " +
                    " @CompanyCode = '" + ddlCompanyCode.SelectedValue.ToString() + "', " +
                    " @HouseNumber = '" + txtHouseNo.Text.ToString() + "', " +
                    " @Street = '" + txtStreet.Text.ToString() + "', " +
                    " @Country = '" + ddlCountry.SelectedValue.ToString() + "', " +
                    " @City = '" + txtCity.Text.ToString() + "', " +
                    " @Region = '" + ddlRegion.SelectedValue.ToString() + "', " +
                    " @PostalCode = '" + txtPostalCode.Text.ToString() + "', " +
                    " @TaxPayerCNIC = '" + txtTaxPayerCNIC.Text.ToString() + "', " +
                    " @PassportNo = '" + txtPassportNo.Text.ToString() + "', " +
                    " @TaxPayerNTN = '" + txtTaxPayerNTN.Text.ToString() + "', " +
                    " @CDCNumber = '" + txtCDCNumber.Text.ToString() + "', " +
                    " @GSTNo = '" + txtGSTNo.Text.ToString() + "', " +
                    " @Telephone = '" + txtTelephone.Text.ToString() + "', " +
                    " @Email = '" + txtEmail.Text.ToString() + "', " +
                    " @Natureofvendor = '" + txtNatureofVendor.Text.ToString() + "', " +
                    " @APPROVAL = '" + Approval.ToString() + "', " +
                    " @MDA = '" + ddlEmailMDA.SelectedValue.ToString() + "', " +
                    " @CreatedBy = '" + Session["User_Name"].ToString() + "', " +
                    " @Remarks = '" + txtRemarksReview.Text.ToString() + "'";


                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Message");
                    sucess.Visible = true;

                    string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                    lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
                    lblmessage.Text = message + " # " + lblMaxTransactionID.Text;

                    if (ds.Tables["Message"].Rows.Count > 0)
                    {
                        EmailWorkSendFirstApproval();
                        GetTransactionID();
                        sucess.Visible = true;
                        error.Visible = false;
                        email.Visible = false;
                        lblmessage.Focus();
                        sucess.Focus();
                        ClearInputs(Page.Controls);
                        Page.MaintainScrollPositionOnPostBack = false;
                        madatorycolor();
                    }


                }
            }
            catch (Exception ex)
            {
                lblError.Text = "btnSave_Click" + ex.ToString();
            }
            finally
            {
                conn.Close();
            }


        }


        ////////////////////////////////////////////////////Methods////////////////////////////////////////////////////////////////////////////////

        void ClearInputscolor(ControlCollection ctrlss)
        {
            try
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
            catch (Exception ex)
            {
                lblError.Text = "ClearInputscolor" + ex.ToString();
            }
        }

        protected void madatorycolor()
        {
            try
            {
                ddlTitle.BackColor = System.Drawing.Color.AliceBlue;
                txtName.BackColor = System.Drawing.Color.AliceBlue;
                ddlAccountGroup.BackColor = System.Drawing.Color.AliceBlue;
                ddlPurchasingOrganization.BackColor = System.Drawing.Color.AliceBlue;
                ddlCompanyCode.BackColor = System.Drawing.Color.AliceBlue;
                txtStreet.BackColor = System.Drawing.Color.AliceBlue;
                ddlCountry.BackColor = System.Drawing.Color.AliceBlue;
                txtCDCNumber.BackColor = System.Drawing.Color.AliceBlue;
                //txtTaxPayerCNIC.BackColor = System.Drawing.Color.AliceBlue;
                //txtTaxPayerNTN.BackColor = System.Drawing.Color.AliceBlue;
                ddlEmailApproval2nd.BackColor = System.Drawing.Color.AliceBlue;
                ddlEmailMDA.BackColor = System.Drawing.Color.AliceBlue;
                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
            }
            catch (Exception ex)
            {
                lblError.Text = "madatorycolor" + ex.ToString();
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
                    //btnApproved.Enabled = false;
                    btnReject.Attributes.Add("disabled", "true");
                    btnApproved.Enabled = false;
                    btnMDA.Enabled = false;
                    btnCancel.Enabled = false;
                    //btnSaveSubmit.Enabled = false;
                    //btnEdit.Enabled = false;
                    txtRemarksReview.Attributes.Add("disabled", "true");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "GetStatusHierachyCategoryControls" + ex.ToString();
            }
        }


        private void getSortKey()
        {
            try
            {
                ds = obj.BindShortKey();
                ddlSortKey.DataTextField = ds.Tables["tblVMSortKey"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlSortKey.DataValueField = ds.Tables["tblVMSortKey"].Columns["SortKeyNo"].ToString();             // to retrive specific  textfield name 
                ddlSortKey.DataSource = ds.Tables["tblVMSortKey"];      //assigning datasource to the dropdownlist
                ddlSortKey.DataBind();  //binding dropdownlist
                ddlSortKey.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getSortKey" + ex.ToString();
            }
        }
        private void getReconAccount()
        {
            try
            {
                ds = obj.BindReconAccount();
                ddlReconAccount.DataTextField = ds.Tables["tblVMReconAccount"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlReconAccount.DataValueField = ds.Tables["tblVMReconAccount"].Columns["ReconAccountNo"].ToString();             // to retrive specific  textfield name 
                ddlReconAccount.DataSource = ds.Tables["tblVMReconAccount"];      //assigning datasource to the dropdownlist
                ddlReconAccount.DataBind();  //binding dropdownlist
                ddlReconAccount.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getReconAccount" + ex.ToString();
            }

        }
        private void getWHTaxType()
        {
            try
            {
                ds = obj.BindWHTaxType();
                ddlWHTaxType.DataTextField = ds.Tables["tblVMWHTaxType"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlWHTaxType.DataValueField = ds.Tables["tblVMWHTaxType"].Columns["WHTaxType"].ToString();             // to retrive specific  textfield name 
                ddlWHTaxType.DataSource = ds.Tables["tblVMWHTaxType"];      //assigning datasource to the dropdownlist
                ddlWHTaxType.DataBind();  //binding dropdownlist
                // ddlWHTaxType.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getWHTaxType" + ex.ToString();
            }

        }

        private void getTermsofpayment()
        {
            try
            {
                ds = obj.BindTermsofpayment();
                ddlADTermsofpayment.DataTextField = ds.Tables["tblVMTermsofpayment"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlADTermsofpayment.DataValueField = ds.Tables["tblVMTermsofpayment"].Columns["Termsofpayment"].ToString();             // to retrive specific  textfield name 
                ddlADTermsofpayment.DataSource = ds.Tables["tblVMTermsofpayment"];      //assigning datasource to the dropdownlist
                ddlADTermsofpayment.DataBind();  //binding dropdownlist
                ddlADTermsofpayment.Items.Insert(0, new ListItem("------Select------", "0"));

                ddlPDTermsOfPayment.DataTextField = ds.Tables["tblVMTermsofpayment"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlPDTermsOfPayment.DataValueField = ds.Tables["tblVMTermsofpayment"].Columns["Termsofpayment"].ToString();             // to retrive specific  textfield name 
                ddlPDTermsOfPayment.DataSource = ds.Tables["tblVMTermsofpayment"];      //assigning datasource to the dropdownlist
                ddlPDTermsOfPayment.DataBind();  //binding dropdownlist
                ddlPDTermsOfPayment.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getTermsofpayment" + ex.ToString();
            }

        }

        private void getAccountGroup()
        {
            try
            {
                ds = obj.BindAccountGroup();
                ddlAccountGroup.DataTextField = ds.Tables["tblVMAccountGroup"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlAccountGroup.DataValueField = ds.Tables["tblVMAccountGroup"].Columns["Accountgroup"].ToString();             // to retrive specific  textfield name 
                ddlAccountGroup.DataSource = ds.Tables["tblVMAccountGroup"];      //assigning datasource to the dropdownlist
                ddlAccountGroup.DataBind();  //binding dropdownlist
                ddlAccountGroup.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getAccountGroup" + ex.ToString();
            }

        }

        private void getPurchasingOrg()
        {
            try
            {
                ds = obj.BindPurchasingOrg();
                ddlPurchasingOrganization.DataTextField = ds.Tables["tblVMPurchasingOrganization"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlPurchasingOrganization.DataValueField = ds.Tables["tblVMPurchasingOrganization"].Columns["PurchasingOrganization"].ToString();             // to retrive specific  textfield name 
                ddlPurchasingOrganization.DataSource = ds.Tables["tblVMPurchasingOrganization"];      //assigning datasource to the dropdownlist
                ddlPurchasingOrganization.DataBind();  //binding dropdownlist
                ddlPurchasingOrganization.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getPurchasingOrg" + ex.ToString();
            }

        }
        private void getCountry()
        {
            try
            {
                ds = obj.BindCountry();
                ddlCountry.DataTextField = ds.Tables["tblCMCountry"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
                ddlCountry.DataValueField = ds.Tables["tblCMCountry"].Columns["Country"].ToString().Trim();             // to retrive specific  textfield name 
                ddlCountry.DataSource = ds.Tables["tblCMCountry"];      //assigning datasource to the dropdownlist
                ddlCountry.DataBind();  //binding dropdownlist
                ddlCountry.Items.Insert(0, new ListItem("------Select------", "0"));

                ddlWHTaxCountry.DataTextField = ds.Tables["tblCMCountry"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
                ddlWHTaxCountry.DataValueField = ds.Tables["tblCMCountry"].Columns["Country"].ToString().Trim();             // to retrive specific  textfield name 
                ddlWHTaxCountry.DataSource = ds.Tables["tblCMCountry"];      //assigning datasource to the dropdownlist
                ddlWHTaxCountry.DataBind();  //binding dropdownlist
                ddlWHTaxCountry.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getCountry" + ex.ToString();
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


        private void BindEmailUser()
        {
            try
            {
                cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserMDA where FormName = 'VM'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();
                ddlEmailMDA.DataSource = cmd.ExecuteReader();
                ddlEmailMDA.DataTextField = "DisplayName";
                ddlEmailMDA.DataValueField = "user_name";
                ddlEmailMDA.DataBind();
                ddlEmailMDA.Items.Insert(0, new ListItem("------Select------", "0"));
                cmd.Clone();
                conn.Close();

                cmd.CommandText = "SELECT * FROM tbluserApprovalHOD where Designation = 'PDA' and FormID = 'VM'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();
                ddlEmailApproval2nd.DataSource = cmd.ExecuteReader();
                ddlEmailApproval2nd.DataTextField = "DisplayName";
                ddlEmailApproval2nd.DataValueField = "user_name";
                ddlEmailApproval2nd.DataBind();
                ddlEmailApproval2nd.Items.Insert(0, new ListItem("------Select------", "0"));
                conn.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = "BindEmailUser" + ex.ToString();
            }
        }
        private void GetHarcheyID()
        {
            try
            {
                ds = obj.GetHarachyCustomerMaster(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
                dt = ds.Tables["HID"];
                ViewState["HIDDataSet"] = dt;
                ViewState["HID"] = "1";
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
            catch (Exception ex)
            {
                lblError.Text = "GetHarcheyID" + ex.ToString();
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

        private void getRegion()
        {
            try
            {
                ds = obj.BindRegion();
                ddlRegion.DataTextField = ds.Tables["tblCMRegion"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
                ddlRegion.DataValueField = ds.Tables["tblCMRegion"].Columns["Region"].ToString().Trim();             // to retrive specific  textfield name 
                ddlRegion.DataSource = ds.Tables["tblCMRegion"];      //assigning datasource to the dropdownlist
                ddlRegion.DataBind();  //binding dropdownlist
                ddlRegion.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getRegion" + ex.ToString();
            }
        }

        protected void btnApproved_Click(object sender, EventArgs e)
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
                    EmailWorkApproval();
                    ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "btnApproved_Click" + ex.ToString();
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
                    //   ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                    error.Visible = false;
                    sucess.Visible = false;
                    email.Visible = true;
                    Page.MaintainScrollPositionOnPostBack = true;
                    txtRemarksReview.BackColor = System.Drawing.Color.White;
                    lblEmail.Focus();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "btnReject_Click" + ex.ToString();
            }
        }

        protected void btnMDA_Click(object sender, EventArgs e)
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

                txtVendorCode.BackColor = System.Drawing.Color.White;
                lblError.Text = "";

                if (txtVendorCode.Text == "")
                {
                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "Vendor Code Code should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtVendorCode.BackColor = System.Drawing.Color.Red;
                    txtVendorCode.ForeColor = System.Drawing.Color.White;
                    lblmessage.ForeColor = System.Drawing.Color.Red;

                }
                else
                {

                    lblUpError.Text = "";
                    cmd.CommandText = @"Select  VendorCode from tbl_FI_VendorMaster where VendorCode = @VendorCode";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@VendorCode", txtVendorCode.Text);
                    adp.SelectCommand = cmd;
                    dt.Clear();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        lblEmail.Text = "";
                        lblmessage.Text = "";
                        lblUpError.Text = "Vendor Code " + txtVendorCode.Text + " already exist!. Please provide a specific code";
                        sucess.Visible = false;
                        error.Visible = true;
                        lblmessage.Focus();
                        sucess.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        txtVendorCode.BackColor = System.Drawing.Color.Red;
                        lblmessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        UpdateWorking();
                    }
                }

            }
            catch (Exception ex)
            {
                lblError.Text = "btnMDA_Click" + ex.ToString();
            }
        }

        protected void ClosedFormAfterReject()
        {
            try
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
                        catch (Exception ex)
                        {
                            lblError.Text = "ClosedFormAfterReject" + ex.ToString();
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "ClosedFormAfterReject" + ex.ToString();
            }

        }

        protected void getDataWhenQueryStringPass()
        {
            getSortKey();
            getReconAccount();
            BindEmailUser();
            getAccountGroup();
            getTermsofpayment();
            getWHTaxType();
            getPurchasingOrg();
            getCountry();
            try
            {
                string TI = Request.QueryString["TransactionNo"].ToString();
                cmd.CommandText = "";
                cmd.CommandText = "select * from tbl_FI_VendorMaster where TransactionMain = @TNo";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@TNo", TI.ToString());
                adp.SelectCommand = cmd;
                adp.Fill(ds, "Data");
                lblMaxTransactionNo.Text = ds.Tables["Data"].Rows[0]["TransactionMain"].ToString();
                lblMaxTransactionID.Text = ds.Tables["Data"].Rows[0]["TransactionID"].ToString();
                txtVendorCode.Text = ds.Tables["Data"].Rows[0]["VendorCode"].ToString();
                ddlTitle.SelectedValue = ds.Tables["Data"].Rows[0]["Title"].ToString();
                txtName.Text = ds.Tables["Data"].Rows[0]["Name"].ToString();
                ddlAccountGroup.SelectedValue = ds.Tables["Data"].Rows[0]["Accountgroup"].ToString();
                ddlPurchasingOrganization.SelectedValue = ds.Tables["Data"].Rows[0]["PurchasingOrganization"].ToString();
                ddlCompanyCode.SelectedValue = ds.Tables["Data"].Rows[0]["CompanyCode"].ToString();
                txtHouseNo.Text = ds.Tables["Data"].Rows[0]["HouseNumber"].ToString();
                txtStreet.Text = ds.Tables["Data"].Rows[0]["Street"].ToString();
                txtHouseNo.Text = ds.Tables["Data"].Rows[0]["HouseNumber"].ToString();
                ddlCountry.SelectedValue = ds.Tables["Data"].Rows[0]["Country"].ToString();
                txtCity.Text = ds.Tables["Data"].Rows[0]["City"].ToString();
                txtPostalCode.Text = ds.Tables["Data"].Rows[0]["PostalCode"].ToString();
                ddlRegion.SelectedValue = ds.Tables["Data"].Rows[0]["Region"].ToString();
                txtTaxPayerCNIC.Text = ds.Tables["Data"].Rows[0]["TaxPayerCNIC"].ToString();
                txtPassportNo.Text = ds.Tables["Data"].Rows[0]["PassportNo"].ToString();
                txtTaxPayerNTN.Text = ds.Tables["Data"].Rows[0]["TaxPayerNTN"].ToString();
                txtCDCNumber.Text = ds.Tables["Data"].Rows[0]["CDCNumber"].ToString();
                txtGSTNo.Text = ds.Tables["Data"].Rows[0]["GSTNo"].ToString();
                txtTelephone.Text = ds.Tables["Data"].Rows[0]["Telephone"].ToString();
                txtEmail.Text = ds.Tables["Data"].Rows[0]["Email"].ToString();
                txtNatureofVendor.Text = ds.Tables["Data"].Rows[0]["Natureofvendor"].ToString();
                txtOrderCurrency.Text = ds.Tables["Data"].Rows[0]["OrderCurrency"].ToString();
                txtMinimumOrderValue.Text = ds.Tables["Data"].Rows[0]["MinimumOrderValue"].ToString();
                ddlPDTermsOfPayment.SelectedValue = ds.Tables["Data"].Rows[0]["Termsofpayment"].ToString();
                ddlSchemaGroupVendor.SelectedValue = ds.Tables["Data"].Rows[0]["SchemaGroupVendor"].ToString();
                string GRCheck = ds.Tables["Data"].Rows[0]["GRCheck"].ToString();
                if (GRCheck == "1")
                {
                    rbGrCheck.SelectedValue = "1";
                }
                else
                {
                    rbGrCheck.SelectedValue = "0";
                }
                ddlSortKey.SelectedValue = ds.Tables["Data"].Rows[0]["SortKey"].ToString();
                ddlReconAccount.SelectedValue = ds.Tables["Data"].Rows[0]["ReconAccount"].ToString();
                ddlADTermsofpayment.SelectedValue = ds.Tables["Data"].Rows[0]["CCDTermsofpayment"].ToString();
                txtPaymentMethods.Text = ds.Tables["Data"].Rows[0]["PaymentMethods"].ToString();
                txtPreviousAccount.Text = ds.Tables["Data"].Rows[0]["PreviousAccount"].ToString();
                txtLiableCheck.Text = ds.Tables["Data"].Rows[0]["LiableCheck"].ToString();
                txtExemptionCertificate.Text = ds.Tables["Data"].Rows[0]["ExemptionCertificate"].ToString();
                ddlExemptionReasons.SelectedValue = ds.Tables["Data"].Rows[0]["ExemptionReasons"].ToString();
                //          txtExemptionFromDate.Text = ds.Tables["Data"].Rows[0]["ExemptionFromDate"].ToString();
                if (ds.Tables["Data"].Rows[0]["ExemptionFromDate"] != null)
                {
                    txtExemptionFromDate.Text = ds.Tables["Data"].Rows[0]["ExemptionFromDate"].ToString();
                }
                if (ds.Tables["Data"].Rows[0]["ExemptionToDate"] != "")
                {
                    txtExemptionToDate.Text = ds.Tables["Data"].Rows[0]["ExemptionToDate"].ToString();
                }


                ddlWHTaxCountry.Text = ds.Tables["Data"].Rows[0]["WHTaxCountry"].ToString();
                //ddlWHTaxType.SelectedValue = ds.Tables["Data"].Rows[0]["WHTaxType"].ToString();

                for (int i = 0; i < ddlWHTaxType.Items.Count; i++)
                {
                    foreach (string category in ds.Tables["Data"].Rows[0]["WHTaxType"].ToString().Split(','))
                    {
                        if (category != ddlWHTaxType.Items[i].Value) continue;
                        ddlWHTaxType.Items[i].Selected = true;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                lblError.Text = "ClosedFormAfterReject" + ex.ToString();
            }
        }

        protected void UpdateWorking()
        {
            try
            {
                string TaxType = "";

                for (int i = 0; i <= ddlWHTaxType.Items.Count - 1; i++)
                {
                    if (ddlWHTaxType.Items[i].Selected)
                    {
                        if (TaxType == "") { TaxType = ddlWHTaxType.Items[i].Value; }
                        else { TaxType += "," + ddlWHTaxType.Items[i].Value; }
                    }
                    TaxType = TaxType.Trim();
                }
                cmd.CommandText = @"update tbl_FI_VendorMaster set VendorCode = right(replicate('0',9) + convert(varchar(18), @VCode) , 9)
        ,Accountgroup = @Accountgroup
       ,OrderCurrency = @OrderCurrency
      ,MinimumOrderValue = @MinimumOrderValue
      ,Termsofpayment = @Termsofpayment
      ,SchemaGroupVendor = @SchemaGroupVendor
      ,GRCheck = @GRCheck
      ,SortKey = @SortKey
      ,ReconAccount = @ReconAccount
      ,CCDTermsofpayment = @CCDTermsofpayment
      ,PaymentMethods = @PaymentMethods
      ,PreviousAccount = @PreviousAccount
      ,LiableCheck = @LiableCheck
      ,ExemptionCertificate = @ExemptionCertificate
      ,ExemptionReasons = @ExemptionReasons
      ,ExemptionFromDate = LEFT(CONVERT(VARCHAR, @ExemptionFromDate, 120), 10)
      ,ExemptionToDate = LEFT(CONVERT(VARCHAR, @ExemptionToDate, 120), 10)
      ,WHTaxCountry = @WHTaxCountry
      ,WHTaxType = @WHTaxType
      where TransactionID = @TCode";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@VCode", txtVendorCode.Text);
                cmd.Parameters.AddWithValue("@TCode", lblMaxTransactionID.Text);
                cmd.Parameters.AddWithValue("@Accountgroup", ddlAccountGroup.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@OrderCurrency", txtOrderCurrency.Text);
                cmd.Parameters.AddWithValue("@MinimumOrderValue", txtMinimumOrderValue.Text);
                cmd.Parameters.AddWithValue("@Termsofpayment", ddlPDTermsOfPayment.SelectedValue);
                cmd.Parameters.AddWithValue("@SchemaGroupVendor", ddlSchemaGroupVendor.SelectedValue);
                cmd.Parameters.AddWithValue("@GRCheck", rbGrCheck.SelectedValue);
                cmd.Parameters.AddWithValue("@SortKey", ddlSortKey.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@ReconAccount", ddlReconAccount.SelectedValue);
                cmd.Parameters.AddWithValue("@CCDTermsofpayment", ddlADTermsofpayment.SelectedValue);
                cmd.Parameters.AddWithValue("@PaymentMethods", txtPaymentMethods.Text);
                cmd.Parameters.AddWithValue("@PreviousAccount", txtPreviousAccount.Text);
                cmd.Parameters.AddWithValue("@LiableCheck", txtLiableCheck.Text);
                cmd.Parameters.AddWithValue("@ExemptionCertificate", txtExemptionCertificate.Text);
                cmd.Parameters.AddWithValue("@ExemptionReasons", ddlExemptionReasons.SelectedValue);

                string aa = txtExemptionFromDate.Text.ToString();
                string ab = txtExemptionToDate.Text.ToString();

                //  DateTime date1 = DateTime.ParseExact(aa.ToString(), "yyyy-mm-dd", null);
                //  DateTime date2 = DateTime.ParseExact(ab.ToString(), "yyyy-mm-dd", null);



                cmd.Parameters.AddWithValue("@ExemptionFromDate", aa.ToString());
                cmd.Parameters.AddWithValue("@ExemptionToDate", ab.ToString());
                cmd.Parameters.AddWithValue("@WHTaxCountry", ddlWHTaxCountry.Text);
                cmd.Parameters.AddWithValue("@WHTaxType", TaxType.ToString());
                conn.Close();
                conn.Open();
                int a = cmd.ExecuteNonQuery();
                if (a == 1)
                {
                    EmailWorkSendMDA();
                    InsertEmailHOD();
                    ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                    lblmessage.Text = "Vendor Code " + txtVendorCode.Text + " has been issued against  New Vendor Master Creation Request Form ID #  " + lblMaxTransactionID.Text + " ";
                    lblmessage.ForeColor = System.Drawing.Color.Green;
                    conn.Close();
                    sucess.Visible = true;
                    error.Visible = false;
                    email.Visible = false;
                    lblmessage.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "UpdateWorking" + ex.ToString();
            }

        }


        protected void DisableControls(Control parent, bool State)
        {
            try
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
                    rbGrCheck.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "DisableControls" + ex.ToString();
            }
        }

        protected void DisableControl()
        {
            try
            {
                txtVendorCode.Enabled = false;
                ddlTitle.Enabled = false;
                txtName.Enabled = false;
                ddlPurchasingOrganization.Enabled = false;
                ddlCompanyCode.Enabled = false;
                txtHouseNo.Enabled = false;
                txtStreet.Enabled = false;
                txtHouseNo.Enabled = false;
                ddlCountry.Enabled = false;
                txtCity.Enabled = false;
                txtPostalCode.Enabled = false;
                ddlRegion.Enabled = false;
                txtTaxPayerCNIC.Enabled = false;
                txtPassportNo.Enabled = false;
                txtTaxPayerNTN.Enabled = false;
                txtCDCNumber.Enabled = false;
                txtGSTNo.Enabled = false;
                txtTelephone.Enabled = false;
                txtEmail.Enabled = false;
                txtNatureofVendor.Enabled = false;
            }
            catch (Exception ex)
            {
                lblError.Text = "DisableControl" + ex.ToString();
            }
        }

        private void ClearInputs(ControlCollection ctrls)
        {
            try
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
            catch (Exception ex)
            {
                lblError.Text = "ClearInputs" + ex.ToString();
            }
        }

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
                        EmailSubject = "New Vendor Master Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " have sent you a Vendor Master Creation Request against Form ID # " + lblMaxTransactionID.Text + " for approval. <br><br> Your kind approval is required on the following URL: " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Vendor Master Finance Application<br> Information Systems Dashboard";
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
                        EmailSubject = "New Vendor Master Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> Vendor Code " + txtVendorCode.Text.Trim() + " has been issued against New Vendor Master Creation Request Form ID # " + lblMaxTransactionID.Text + " <br><br> The form can be reviewed at the following URL:" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Vendor Master Finance Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        email.Visible = true;
                        lblEmail.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        InsertEmail();
                        lblEmail.Text = "*New Vendor Master Creation Request against  Form ID # " + lblMaxTransactionID.Text + " has been rejected by you";
                        Page.MaintainScrollPositionOnPostBack = true;
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "Email MDA" + ex.ToString();
            }
        }

        private void EmailReject()
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
                        TransactionID = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "New Vendor Master Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " New Vendor Master Creation Request against  Form ID # " + lblMaxTransactionID.Text + " has been disapproved by  " + ViewState["SessionUser"].ToString() + " <br> The reason of rejection is given below you can review your form on following url:" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                       "<br> <br> <br>Reject Remarks: " + txtRemarksReview.Text +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Vendor Master Finance  Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        email.Visible = true;
                        lblEmail.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        InsertEmail();
                        lblEmail.Text = "*New Vendor Master Creation Request against  Form ID # " + lblMaxTransactionID.Text + " has been rejected by you";
                        email.Visible = true;
                        Page.MaintainScrollPositionOnPostBack = false;
                        ViewState["Status"] = HierachyCategoryStatus.ToString();

                    }


                }
            }
            catch (Exception ex)
            {
                lblError.Text = "EmailReject" + ex.ToString();
            }
        }

        private void ApplicationStatus()
        {
            try
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
                            catch (Exception ex)
                            {
                                lblError.Text = "ApplicationStatus" + ex.ToString();
                            }
                            finally
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "ApplicationStatus" + ex.ToString();
            }
        }

        private void BindsysApplicationStatus()
        {
            try
            {
                ds = obj.BindsysApplicationStatus(lblMaxTransactionID.Text, FormID.ToString());
                grdWStatus.DataSource = ds.Tables["BindsysApplicationStatus"];
                grdWStatus.DataBind();
                grdWStatus.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = "BindsysApplicationStatus" + ex.ToString();
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
                        EmailSubject = "New Vendor Master Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + "New Vendor Master Creation Request against Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr. " + ViewState["SessionUser"].ToString() + "  Your kind approval is required on the following URL:" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Vendor Master Finance Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        email.Visible = true;
                        lblEmail.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        InsertEmail();
                        lblEmail.Text = "*New Vendor Master Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";

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
                            UserEmail = reader["user_email"].ToString();
                            EmailSubject = "New Vendor Master Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                            EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> New Vendor Master Creation Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr. " + ViewState["SessionUser"].ToString() + " <br><br> You are requested to create a customer master code information on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                                 "<br>Finance Petty Cash Application<br> Information Systems Dashboard";
                            SessionUser = Session["User_Name"].ToString();
                            DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            ViewState["Status"] = HierachyCategoryStatus.ToString();
                            email.Visible = true;
                            lblEmail.Focus();
                            Page.MaintainScrollPositionOnPostBack = false;
                            InsertEmail();
                            lblEmail.Text = "*New Vendor Master Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";


                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "EmailWorkApproval" + ex.ToString();
            }

        }

        ///////////////////////////////////////////////////Email Working///////////////////////////////////////////////////////////
        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected void InsertEmail()
        {
            try
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
                        catch (Exception ex)
                        {
                            lblError.Text = "InsertEmail" + ex.ToString();
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "InsertEmail" + ex.ToString();
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string url = HttpContext.Current.Request.Url.ToString();
                Response.Redirect(url.ToString());
            }
            catch (Exception ex)
            {
                lblError.Text = "btnCancel_Click" + ex.ToString();
            }
        }


        protected void InsertEmailHOD()
        {
            try
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
                        catch (Exception ex)
                        {
                            lblError.Text = "btnCancel_Click" + ex.ToString();
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblError.Text = "btnCancel_Click" + ex.ToString();
            }
        }
    }
}