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
    public partial class CustomerMaster : System.Web.UI.Page
    {

        public string FormID = "302";
        private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();

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
            Page.MaintainScrollPositionOnPostBack = true;
            txtRemarksReview.Enabled = true;
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
                try
                {
                    txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                    if (Request.QueryString["TransactionNo"] != null)
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        btnMDA.Visible = false;
                        btnReject.Visible = false;
                        btnApproved.Visible = false;
                        getDataWhenQueryStringPass();
                        divEmail.Visible = false;
                        getUserDetail();
                        GetHarcheyID();
                        GetStatusHierachyCategoryControls();
                        BindsysApplicationStatus();
                        if (((string)ViewState["HID"]) == "1")
                        {
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            dvCustomerCode.Visible = false;
                            dvWHT.Visible = false;
                            dvCCD.Visible = false;
                            dvSAD.Visible = false;
                            dvSADPF.Visible = false;
                            DisableControls(Page, false);
                            divEmail.Visible = false;                        
                            btnReject.Visible = false;
                            GridView1.Visible = true;
                            GridView4.Visible = false;
                            GridView2.Visible = true;
                            GridView3.Visible = false;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            txtcustomerCode.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            ddlTitle.Enabled = false;
                            txtName.Enabled = false;
                            ddlAccountGroup.Enabled = false;
                            ddlSalesOrganization.Enabled = false;
                            ddlCompanyCode.Enabled = false;
                            ddlDistributionChannel.Enabled = false;
                            ddlDivision.Enabled = false;
                            txtHouseNumber.Enabled = false;
                            txtStreet.Enabled = false;
                            ddlCountry.Enabled = false;
                            txtCity.Enabled = false;
                            ddlRegion.Enabled = false;
                            txtPostalCode.Enabled = false;
                            txtTaxPayerCNIC.Enabled = false;
                            txtTaxPayerNTN.Enabled = false;
                            txtGSTNo.Enabled = false;
                            txtTelephone.Enabled = false;
                            txtEmail.Enabled = false;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            getCMWithHoldingTaxDataDataWhenQueryStringPass();
                            getCMPartnerFunctionsDataDataWhenQueryStringPass();

                        }
                        if (((string)ViewState["HID"]) == "2")
                        {
                            btnSave.Visible = false;
                            btnMDA.Visible = false;
                            btnCancel.Visible = false;
                            btnMDA.Visible = false;                           
                            txtcustomerCode.Enabled = true;
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            btnApproved.Visible = true;
                            btnReject.Visible = true;
                            divEmail.Visible = false;
                            txtcustomerCode.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            ddlTitle.Enabled = false;
                            txtName.Enabled = false;
                            ddlAccountGroup.Enabled = false;
                            ddlSalesOrganization.Enabled = false;
                            ddlCompanyCode.Enabled = false;
                            ddlDistributionChannel.Enabled = false;
                            ddlDivision.Enabled = false;
                            txtHouseNumber.Enabled = false;
                            txtStreet.Enabled = false;
                            ddlCountry.Enabled = false;
                            txtCity.Enabled = false;
                            ddlRegion.Enabled = false;
                            txtPostalCode.Enabled = false;
                            txtTaxPayerCNIC.Enabled = false;
                            txtTaxPayerNTN.Enabled = false;
                            txtGSTNo.Enabled = false;
                            txtTelephone.Enabled = false;
                            txtEmail.Enabled = false;
                            dvTransactionNo.Visible = false;
                            dvCustomerCode.Visible = false;
                            txtRemarksReview.Enabled = true;
                            getCMWithHoldingTaxDataDataWhenQueryStringPass();
                            getCMPartnerFunctionsDataDataWhenQueryStringPass();
                        }
                        if (((string)ViewState["HID"]) == "4")
                        {
                            ClearInputscolor(Page.Controls);
                            txtcustomerCode.BackColor = System.Drawing.Color.AliceBlue;
                            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                            dvCustomerCode.Visible = true;
                            ddlTitle.Enabled = false;
                            txtName.Enabled = false;
                            ddlAccountGroup.Enabled = true;
                            ddlSalesOrganization.Enabled = false;
                            ddlCompanyCode.Enabled = false;
                            ddlDistributionChannel.Enabled = false;
                            ddlDivision.Enabled = false;
                            txtHouseNumber.Enabled = false;
                            txtStreet.Enabled = false;
                            ddlCountry.Enabled = false;
                            txtCity.Enabled = false;
                            ddlRegion.Enabled = false;
                            txtPostalCode.Enabled = false;
                            txtTaxPayerCNIC.Enabled = false;
                            txtTaxPayerNTN.Enabled = false;
                            txtGSTNo.Enabled = false;
                            txtTelephone.Enabled = false;
                            txtEmail.Enabled = false;
                            btnApproved.Visible = false;
                            btnReject.Visible = true;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnMDA.Visible = true;
                            divEmail.Visible = false;
                            dvWHT.Visible = true;
                            dvCCD.Visible = true;
                            dvSAD.Visible = true;
                            dvSADPF.Visible = true;                          
                            dvFormID.Visible = true;
                            dvTransactionNo.Visible = false;
                            GridView1.Visible = true;
                            GridView2.Visible = true;
                            txtRemarksReview.Enabled = true;
                            getCMWithHoldingTaxDataDataWhenQueryStringPass();
                            getCMPartnerFunctionsDataDataWhenQueryStringPass();

                        }

                    }
                    else
                    {
                        GetTransactionID();
                        methodCall();
                        getUserHOD();
                        getUser();
                        madatorycolor();
                        getUserDetail();

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
                Div1.Visible = true;
                lblError.Text = "User Detail" + ex.ToString();
            }
        }

        protected void getUserHOD()
        {
            try
            {
                Div1.Visible = false;
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
                Div1.Visible = true;
                lblError.Text = "User HOD" + ex.ToString();
            }
        }
        private void methodCall()
        {
            setinitialrow();
            bindGrid();
            setinitialrow2();
            bindGrid2();
            getAccountGrp();
            getSalesOrganization();
            getDistributionChannel();
            getDivision();
            getCountry();
            getRegion();
            getSortKey();
            getReconAccount();
            getTermsofpayment();
            getPaymentBlock();
            getSalesdistrict();
            getCurrency();
            getCustpricproc();
            getIncoterms();
            getTermsofpaymt();
            getTax();
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
                if (ddlTitle.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlTitle.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                if (txtName.Text == "")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtName.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                if (ddlSalesOrganization.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlSalesOrganization.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                if (ddlCompanyCode.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlCompanyCode.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                if (txtStreet.Text == "")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtStreet.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                if (ddlCountry.SelectedValue == "0")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlCountry.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                if (ddlEmailMDA.SelectedValue == "0")
                {
                    lblUpError.Text = "Select (Finance Department) field!.";
                    error.Visible = true;
                    ddlEmailMDA.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                string Result = "";
                Result = ViewState["HOD"].ToString();
                cmd.CommandText = "";
                cmd.CommandText = "SP_SYS_CreateCustomerMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                cmd.Parameters.AddWithValue("@Title", ddlTitle.SelectedValue);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Accountgroup", ddlAccountGroup.SelectedValue);
                cmd.Parameters.AddWithValue("@SalesOrganization", ddlSalesOrganization.SelectedValue);
                cmd.Parameters.AddWithValue("@CompanyCode", ddlCompanyCode.SelectedValue);
                cmd.Parameters.AddWithValue("@DistributionChannel", ddlDistributionChannel.SelectedValue);
                cmd.Parameters.AddWithValue("@Division", ddlDivision.SelectedValue);
                cmd.Parameters.AddWithValue("@HouseNumber", txtHouseNumber.Text);
                cmd.Parameters.AddWithValue("@Street", txtStreet.Text);
                cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                cmd.Parameters.AddWithValue("@Region", ddlRegion.Text);
                cmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text.ToString());
                cmd.Parameters.AddWithValue("@TaxPayerCNIC", txtTaxPayerCNIC.Text);
                cmd.Parameters.AddWithValue("@TaxPayerNTN", txtTaxPayerNTN.Text);
                cmd.Parameters.AddWithValue("@GSTNo", txtGSTNo.Text);
                cmd.Parameters.AddWithValue("@Telephone", txtTelephone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@APPROVAL", Result.ToString());
                cmd.Parameters.AddWithValue("@MDA", ddlEmailMDA.SelectedValue);
                cmd.Parameters.AddWithValue("@CreatedBy", Session["User_Name"].ToString());
                cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
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
                sucess.Visible = true;
                Page.MaintainScrollPositionOnPostBack = false;
                ClearInputs(Page.Controls);


                ClearInputscolor(Page.Controls);
                GetTransactionID();
                madatorycolor();
            }

            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
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
                if (txtcustomerCode.Text == "")
                {
                    lblUpError.Text = "Customer code should not be left blank!.";
                    error.Visible = true;
                    txtcustomerCode.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;


                }
                else
                {
                    lblUpError.Text = "";
                    cmd.CommandText = @"Select  CustomerCode from tbl_FI_CustomerMaster where CustomerCode = '" + txtcustomerCode.Text.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    adp.SelectCommand = cmd;
                    dt.Clear();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        lblEmail.Text = "";
                        lblmessage.Text = "";
                        lblUpError.Text = "Customer Code " + txtcustomerCode.Text + " already exist!. Please provide a specific code";
                        sucess.Visible = false;
                        error.Visible = true;
                        lblmessage.Focus();
                        sucess.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        txtcustomerCode.BackColor = System.Drawing.Color.Red;
                        lblmessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblEmail.Text = "";
                        lblmessage.Text = "";
                        lblUpError.Text = "";
                        sucess.Visible = true;
                        error.Visible = false;
                        Grid1datainsert();
                        Grid2datainsert();
                        updateFWorking();
                        EmailWorkSendMDA();
                        InsertEmailHOD();
                        ApplicationStatus();
                        BindsysApplicationStatus();
                        GetStatusHierachyCategoryControls();
                        lblmessage.Text = "Customer Code " + txtcustomerCode.Text + " has been issued against  New Customer Master Creation Request Form ID #  " + lblMaxTransactionID.Text + " ";
                        lblmessage.ForeColor = System.Drawing.Color.Green;
                        txtcustomerCode.BackColor = System.Drawing.Color.LightBlue;
                        DisableControls(Page, true);
                        Page.MaintainScrollPositionOnPostBack = false;
                        lblmessage.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Rev" + ex.ToString();
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
                    catch (Exception ex)
                    {
                        lblError.Text = "InsertEmailHOD" + ex.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
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
                    error.Visible = false;
                    lblUpError.Text = "";
                    sucess.Visible = false;
                    lblmessage.Text = "";
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

        protected void btnReviewed_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try{
            ClearInputs(Page.Controls);
            }
            catch (Exception ex)
            {
                lblError.Text = "btnReviewed_Click" + ex.ToString();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        ///////////////////////////////////////////////////GridView1//////////////////////////////////////////////////////////

        private void bindGrid()
        {
            try{
            conn.Open();
            cmd.CommandText = "select Wthttype, Wthttype + ' ' + WthttypeDes as Description from tblCMWthttype";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            ds.Clear();
            adp.Fill(ds, "Data");
            DropDownList list = (DropDownList)GridView1.Rows[0].FindControl("ddlwthttype");
            DropDownList list2 = (DropDownList)GridView1.Rows[0].FindControl("ddlwtaxcode");
            list.DataSource = ds.Tables["Data"];
            list.DataTextField = "Description";
            list.DataValueField = "Wthttype";
            list.DataBind();
            list2.DataSource = ds.Tables["Data"];
            list2.DataTextField = "Description";
            list2.DataValueField = "Wthttype";
            list2.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "bindGrid" + ex.ToString();
            }
            }

        private void setinitialrow()
        {
            try{
            DataTable table = new DataTable("data");
            if (table.Rows.Count == 0)
            {
                table.Columns.Add("TransactionID");
                table.Columns.Add("wth.t.type");
                table.Columns.Add("w/tax.code");
                table.Columns.Add("w/tax");
                table.Columns.Add("Oblig.from");
                table.Columns.Add("Oblig.to");
                table.Columns.Add("W/tax.number");
                table.Columns.Add("Exemption number");
                table.Columns.Add("Exemption rate");
                table.Columns.Add("Exemption.reas");
                table.Columns.Add("Exempt.from");
                table.Columns.Add("Exempt To");
                table.Columns.Add("Name");

            }
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            GridView1.DataSource = table;
            GridView1.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "setinitialrow" + ex.ToString();
            }
            
        }

        protected void deleteRowEvent(object sender, EventArgs e)
        {
            try{
            DataTable data = new DataTable();
            LinkButton delete = (LinkButton)sender;
            GridViewRow container = (GridViewRow)delete.NamingContainer;
            List<string> List1 = new List<string>();
            List<string> List2 = new List<string>();

            if (data.Rows.Count == 0)
            {
                data.Columns.Add("TransactionID");
                data.Columns.Add("wth.t.type");
                data.Columns.Add("w/tax.code");
                data.Columns.Add("w/tax");
                data.Columns.Add("Oblig.from");
                data.Columns.Add("Oblig.to");
                data.Columns.Add("W/tax.number");
                data.Columns.Add("Exemption number");
                data.Columns.Add("Exemption rate");
                data.Columns.Add("Exemption.reas");
                data.Columns.Add("Exempt.from");
                data.Columns.Add("Exempt To");
                data.Columns.Add("Name");
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                DropDownList ddlwthttype = (DropDownList)row.FindControl("ddlwthttype");
                DropDownList ddlwtaxcode = (DropDownList)row.FindControl("ddlwtaxcode");
                CheckBox Cbwtax = (CheckBox)row.FindControl("Cbwtax");
                TextBox txtObligfrom = (TextBox)row.FindControl("txtObligfrom");
                TextBox txtObligto = (TextBox)row.FindControl("txtObligto");
                TextBox txtWtaxnumber = (TextBox)row.FindControl("txtWtaxnumber");
                TextBox txtExemptionnumber = (TextBox)row.FindControl("txtExemptionnumber");
                TextBox txtExemptionrate = (TextBox)row.FindControl("txtExemptionrate");
                TextBox txtExemptionreas = (TextBox)row.FindControl("txtExemptionreas");
                TextBox txtExemptfrom = (TextBox)row.FindControl("txtExemptfrom");
                TextBox txtExemptTo = (TextBox)row.FindControl("txtExemptTo");
                TextBox txtName = (TextBox)row.FindControl("txtName");

                if (List1.Count == 0)
                {
                    foreach (ListItem item in ddlwthttype.Items)
                    {
                        List1.Add(item.ToString());
                    }

                }
                if (List2.Count == 0)
                {
                    foreach (ListItem item in ddlwtaxcode.Items)
                    {
                        List2.Add(item.ToString());
                    }

                }
                data.Rows.Add(lblMaxTransactionID.Text,
                ddlwthttype.SelectedItem.Text,
                ddlwtaxcode.SelectedItem.Text,
                value.ToString(),
                txtObligfrom.Text,
                txtObligto.Text,
                txtWtaxnumber.Text,
                txtExemptionnumber.Text,
                txtExemptionrate.Text,
                txtExemptionreas.Text,
                txtExemptfrom.Text,
                txtExemptTo.Text,
                txtName.Text);
            }
            DataRow newrow = data.NewRow();
            data.Rows.RemoveAt(container.RowIndex);
            GridView1.DataSource = data;
            GridView1.DataBind();
            setData(data, List1, List2);

            if (data.Rows.Count == 0)
            {
                setinitialrow();
                bindGrid();
            }
            }
            catch (Exception ex)
            {
                lblError.Text = "deleteRowEvent" + ex.ToString();
            }
        }

        protected void AddRowEvent(object sender, EventArgs e)
        {
            try{
            List<string> List1 = new List<string>();
            List<string> List2 = new List<string>();
            DataTable data = new DataTable();
            if (data.Rows.Count == 0)
            {
                data.Columns.Add("TransactionID");
                data.Columns.Add("wth.t.type");
                data.Columns.Add("w/tax.code");
                data.Columns.Add("w/tax");
                data.Columns.Add("Oblig.from");
                data.Columns.Add("Oblig.to");
                data.Columns.Add("W/tax.number");
                data.Columns.Add("Exemption number");
                data.Columns.Add("Exemption rate");
                data.Columns.Add("Exemption.reas");
                data.Columns.Add("Exempt.from");
                data.Columns.Add("Exempt To");
                data.Columns.Add("Name");
            }

            foreach (GridViewRow row in GridView1.Rows)
            {
                DropDownList ddlwthttype = (DropDownList)row.FindControl("ddlwthttype");
                DropDownList ddlwtaxcode = (DropDownList)row.FindControl("ddlwtaxcode");
                CheckBox Cbwtax = (CheckBox)row.FindControl("Cbwtax");
                if (Cbwtax != null && Cbwtax.Checked)
                {
                    value = "1";
                }
                else
                {
                    value = "0";
                }
                TextBox txtObligfrom = (TextBox)row.FindControl("txtObligfrom");
                TextBox txtObligto = (TextBox)row.FindControl("txtObligto");
                TextBox txtWtaxnumber = (TextBox)row.FindControl("txtWtaxnumber");
                TextBox txtExemptionnumber = (TextBox)row.FindControl("txtExemptionnumber");
                TextBox txtExemptionrate = (TextBox)row.FindControl("txtExemptionrate");
                TextBox txtExemptionreas = (TextBox)row.FindControl("txtExemptionreas");
                TextBox txtExemptfrom = (TextBox)row.FindControl("txtExemptfrom");
                TextBox txtExemptTo = (TextBox)row.FindControl("txtExemptTo");
                TextBox txtName = (TextBox)row.FindControl("txtName");


                if (List1.Count == 0)
                {
                    foreach (ListItem item in ddlwthttype.Items)
                    {
                        List1.Add(item.ToString());
                    }
                    if (List2.Count == 0)

                        foreach (ListItem item in ddlwtaxcode.Items)
                        {
                            List2.Add(item.ToString());
                        }
                }
                data.Rows.Add(lblMaxTransactionID.Text,
                 ddlwthttype.SelectedItem.Text,
                 ddlwtaxcode.SelectedItem.Text,
                 value.ToString(),
                 txtObligfrom.Text,
                 txtObligto.Text,
                 txtWtaxnumber.Text,
                 txtExemptionnumber.Text,
                 txtExemptionrate.Text,
                 txtExemptionreas.Text,
                 txtExemptfrom.Text,
                 txtExemptTo.Text,
                 txtName.Text);
            }
            DataRow newrow = data.NewRow();
            data.Rows.InsertAt(newrow, GridView1.Rows.Count + 1);
            GridView1.DataSource = data;
            ViewState["Grid1"] = data;
            GridView1.DataBind();
            setData(data, List1, List2);
            }
            catch (Exception ex)
            {
                lblError.Text = "AddRowEvent" + ex.ToString();
            }
        }

        protected void setData(DataTable table, List<string> List1, List<string> List2)
        {
            try{
            foreach (GridViewRow row in GridView1.Rows)
            {
                DropDownList ddlwthttype = (DropDownList)row.FindControl("ddlwthttype");
                DropDownList ddlwtaxcode = (DropDownList)row.FindControl("ddlwtaxcode");
                ddlwthttype.Items.Clear();
                ddlwtaxcode.Items.Clear();
                foreach (string item in List1)
                {
                    ddlwthttype.Items.Add(item);
                }



                foreach (string item2 in List2)
                {
                    ddlwtaxcode.Items.Add(item2);
                }


                ddlwthttype.SelectedItem.Text = table.Rows[row.RowIndex]["wth.t.type"].ToString();
                ddlwtaxcode.SelectedItem.Text = table.Rows[row.RowIndex]["w/tax.code"].ToString();
                CheckBox Cbwtax = (CheckBox)row.FindControl("w/tax");
                value = table.Rows[row.RowIndex]["w/tax"].ToString();
                TextBox txtObligfrom = (TextBox)row.FindControl("txtObligfrom");
                txtObligfrom.Text = table.Rows[row.RowIndex]["Oblig.from"].ToString();
                TextBox txtObligTo = (TextBox)row.FindControl("txtObligTo");
                txtObligTo.Text = table.Rows[row.RowIndex]["Oblig.to"].ToString();
                TextBox txtWtaxnumber = (TextBox)row.FindControl("txtWtaxnumber");
                txtWtaxnumber.Text = table.Rows[row.RowIndex]["W/tax.number"].ToString();
                TextBox txtExemptionnumber = (TextBox)row.FindControl("txtExemptionnumber");
                txtExemptionnumber.Text = table.Rows[row.RowIndex]["Exemption number"].ToString();
                TextBox txtExemptionrate = (TextBox)row.FindControl("txtExemptionrate");
                txtExemptionrate.Text = table.Rows[row.RowIndex]["Exemption rate"].ToString();
                TextBox txtExemptionreas = (TextBox)row.FindControl("txtExemptionreas");
                txtExemptionreas.Text = table.Rows[row.RowIndex]["Exemption.reas"].ToString();
                TextBox txtExemptfrom = (TextBox)row.FindControl("txtExemptfrom");
                txtExemptfrom.Text = table.Rows[row.RowIndex]["Exempt.from"].ToString();
                TextBox txtExemptTo = (TextBox)row.FindControl("txtExemptTo");
                txtExemptTo.Text = table.Rows[row.RowIndex]["Exempt To"].ToString();
                TextBox txtName = (TextBox)row.FindControl("txtName");
                txtName.Text = table.Rows[row.RowIndex]["Name"].ToString();
            }
            }
            catch (Exception ex)
            {
                lblError.Text = "setData" + ex.ToString();
            }
        }

        ///////////////////////////////////////////////////GridView1//////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////GridView2//////////////////////////////////////////////////////////

        private void bindGrid2()
        {
            try{
            cmd.CommandText = "SELECT PF,PF + ' ' + PFDes as  Description FROM tblCMPf ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            ds.Clear();
            adp.Fill(ds, "DataGrid2");
            DropDownList list = (DropDownList)GridView2.Rows[0].FindControl("ddlPF");
            list.DataSource = ds.Tables["DataGrid2"];
            list.DataTextField = "Description";
            list.DataValueField = "PF";
            list.DataBind();
            conn.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = "bindGrid2" + ex.ToString();
            }
        }

        private void setinitialrow2()
        {
            try{
            DataTable table = new DataTable("data2");
            if (table.Rows.Count == 0)
            {
                table.Columns.Add("TransactionID");
                table.Columns.Add("PF");
                table.Columns.Add("PartnerFunction");
                table.Columns.Add("Number");
                table.Columns.Add("Name");
                table.Columns.Add("PartnerDescription");
                table.Columns.Add("Default");

            }
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            GridView2.DataSource = table;
            GridView2.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "setinitialrow2" + ex.ToString();
            }
        }

        protected void deleteRowEvent2(object sender, EventArgs e)
        {
            try{
            DataTable data = new DataTable();
            LinkButton delete = (LinkButton)sender;
            GridViewRow container = (GridViewRow)delete.NamingContainer;
            List<string> List1 = new List<string>();

            if (data.Rows.Count == 0)
            {
                data.Columns.Add("TransactionID");
                data.Columns.Add("PF");
                data.Columns.Add("PartnerFunction");
                data.Columns.Add("Number");
                data.Columns.Add("Name");
                data.Columns.Add("PartnerDescription");
                data.Columns.Add("Default");
            }
            foreach (GridViewRow row in GridView2.Rows)
            {
                DropDownList ddlPF = (DropDownList)row.FindControl("ddlPF");
                TextBox txtPartnerFunction = (TextBox)row.FindControl("txtPartnerFunction");
                TextBox txtNumber = (TextBox)row.FindControl("txtNumber");
                TextBox txtName = (TextBox)row.FindControl("txtName");
                TextBox txtPartnerDescription = (TextBox)row.FindControl("txtPartnerDescription");
                CheckBox CbwDefault = (CheckBox)row.FindControl("CbwDefault");

                if (List1.Count == 0)
                {
                    foreach (ListItem item in ddlPF.Items)
                    {
                        List1.Add(item.ToString());
                    }
                }

                data.Rows.Add(lblMaxTransactionID.Text, ddlPF.SelectedItem.Text,
                txtPartnerFunction.Text,
                txtNumber.Text,
                txtName.Text,
                txtPartnerDescription.Text,
                value.ToString());

            }

            DataRow newrow = data.NewRow();
            data.Rows.RemoveAt(container.RowIndex);
            GridView2.DataSource = data;
            GridView2.DataBind();
            setData2(data, List1);

            if (data.Rows.Count == 0)
            {
                setinitialrow2();
                bindGrid2();
            }
            }
            catch (Exception ex)
            {
                lblError.Text = "deleteRowEvent2" + ex.ToString();
            }
        }

        protected void AddRowEvent2(object sender, EventArgs e)
        {
            try{
            List<string> List1 = new List<string>();
            DataTable data = new DataTable();
            if (data.Rows.Count == 0)
            {
                data.Columns.Add("TransactionID");
                data.Columns.Add("PF");
                data.Columns.Add("PartnerFunction");
                data.Columns.Add("Number");
                data.Columns.Add("Name");
                data.Columns.Add("PartnerDescription");
                data.Columns.Add("Default");
            }

            foreach (GridViewRow row in GridView2.Rows)
            {
                DropDownList ddlPF = (DropDownList)row.FindControl("ddlPF");
                TextBox txtPartnerFunction = (TextBox)row.FindControl("txtPartnerFunction");
                TextBox txtNumber = (TextBox)row.FindControl("txtNumber");
                TextBox txtName = (TextBox)row.FindControl("txtName");
                TextBox txtPartnerDescription = (TextBox)row.FindControl("txtPartnerDescription");
                CheckBox CbwDefault = (CheckBox)row.FindControl("CbwDefault");
                if (CbwDefault != null && CbwDefault.Checked)
                {
                    value = "1";
                }
                else
                {
                    value = "0";
                }


                if (List1.Count == 0)
                {

                    foreach (ListItem item in ddlPF.Items)
                    {
                        List1.Add(item.ToString());
                    }
                }

                data.Rows.Add(lblMaxTransactionID.Text, ddlPF.SelectedItem.Text,
                txtPartnerFunction.Text,
                txtNumber.Text,
                txtName.Text,
                txtPartnerDescription.Text,
                value.ToString());



            }
            DataRow newrow = data.NewRow();
            data.Rows.InsertAt(newrow, GridView2.Rows.Count + 1);
            GridView2.DataSource = data;
            ViewState["Grid2"] = data;
            GridView2.DataBind();
            setData2(data, List1);
            }
            catch (Exception ex)
            {
                lblError.Text = "AddRowEvent2" + ex.ToString();
            }
        }

        protected void setData2(DataTable table, List<string> List1)
        {
            try{
            foreach (GridViewRow row in GridView2.Rows)
            {
                DropDownList ddlPF = (DropDownList)row.FindControl("ddlPF");
                ddlPF.Items.Clear();
                foreach (string item in List1)
                {
                    ddlPF.Items.Add(item);
                }


                ddlPF.SelectedItem.Text = table.Rows[row.RowIndex]["PF"].ToString();
                TextBox txtPartnerFunction = (TextBox)row.FindControl("txtPartnerFunction");
                txtPartnerFunction.Text = table.Rows[row.RowIndex]["PartnerFunction"].ToString();
                TextBox txtNumber = (TextBox)row.FindControl("txtNumber");
                txtNumber.Text = table.Rows[row.RowIndex]["Number"].ToString();
                TextBox txtName = (TextBox)row.FindControl("txtName");
                txtName.Text = table.Rows[row.RowIndex]["Name"].ToString();
                TextBox txtPartnerDescription = (TextBox)row.FindControl("txtPartnerDescription");
                txtPartnerDescription.Text = table.Rows[row.RowIndex]["PartnerDescription"].ToString();
                CheckBox CbwDefault = (CheckBox)row.FindControl("Default");
                value = table.Rows[row.RowIndex]["Default"].ToString();

            }
            }
            catch (Exception ex)
            {
                lblError.Text = "setData2" + ex.ToString();
            }
        }

        ///////////////////////////////////////////////////GridView2//////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////Methods//////////////////////////////////////////////////////////

        private void GetStatusHierachyCategoryControls()
        {
            try{
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
                btnCancel.Enabled = false;
                btnMDA.Enabled = false;
                txtRemarksReview.Attributes.Add("disabled", "true");
            }
            }
            catch (Exception ex)
            {
                lblError.Text = "GetStatusHierachyCategoryControls" + ex.ToString();
            }
        }

        private void ClearInputs(ControlCollection ctrls)
        {
            try{
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
            catch (Exception ex)
            {
                lblError.Text = "ClearInputs" + ex.ToString();
            }
            }

        private void updateFWorking()
        {
            try{
            cmd.CommandText = "";
            cmd.CommandText = @"UPdate tbl_FI_CustomerMaster set 
                                    CustomerCode = @CustomerCode,
                                    SortKey = @SortKey,
                                    ReconAccount = @ReconAccount,
                                    Termsofpayment = @Termsofpayment,
                                    PaymentMethods = @PaymentMethods,
                                    PaymentBlock = @PaymentBlock,
                                    Singlepayment = @Singlepayment,
                                    Salesdistrict = @Salesdistrict,
                                    Currency = @Currency,
                                    Custpricproc = @Custpricproc,
                                    Incoterms = @Incoterms,
                                    Termsofpaymentsales = @Termsofpaymentsales,
                                    Tax = @Tax where TransactionID = @TransactionID";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@CustomerCode", txtcustomerCode.Text);
            cmd.Parameters.AddWithValue("@SortKey", ddlSortKey.SelectedValue);
            cmd.Parameters.AddWithValue("@ReconAccount", ddlReconAccount.SelectedValue);
            cmd.Parameters.AddWithValue("@Termsofpayment", ddlADTermsofpayment.SelectedValue);
            cmd.Parameters.AddWithValue("@PaymentMethods", txtPaymentMethods.Text);
            cmd.Parameters.AddWithValue("@PaymentBlock", ddlPaymentBlock.SelectedValue);
            string singlepayment = "";
            if (CheckBox1.Checked)
            {
                singlepayment = "1";
            }
            else
            {
                singlepayment = "0";
            }
            cmd.Parameters.AddWithValue("@Singlepayment", singlepayment.ToString());
            cmd.Parameters.AddWithValue("@Salesdistrict", ddlSalesdistrict.SelectedValue);
            cmd.Parameters.AddWithValue("@Currency", ddlCurrency.SelectedValue);
            cmd.Parameters.AddWithValue("@Custpricproc", ddlCustpricproc.SelectedValue);
            cmd.Parameters.AddWithValue("@Incoterms", ddlIncoterms.SelectedValue);
            cmd.Parameters.AddWithValue("@Termsofpaymentsales", ddlTermsofpayment.SelectedValue);
            cmd.Parameters.AddWithValue("@Tax", ddlTax.SelectedValue);
            cmd.Parameters.AddWithValue("@TransactionID", lblMaxTransactionID.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = "updateFWorking" + ex.ToString();
            }
        }

        private void getUser()
        {
            try{
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
            catch (Exception ex)
            {
                lblError.Text = "getUser" + ex.ToString();
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
                Div1.Visible = true;
                lblError.Text = "Transaction ID" + ex.ToString();
            }
        }

        private void getAccountGrp()
        {
            try{
            ds = obj.BindAccountGrp();
            ddlAccountGroup.DataTextField = ds.Tables["tblCMAccountGrp"].Columns["Accountgroup"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlAccountGroup.DataValueField = ds.Tables["tblCMAccountGrp"].Columns["Accountgroup"].ToString().Trim();             // to retrive specific  textfield name 
            ddlAccountGroup.DataSource = ds.Tables["tblCMAccountGrp"];      //assigning datasource to the dropdownlist
            ddlAccountGroup.DataBind();  //binding dropdownlist
            ddlAccountGroup.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getAccountGrp" + ex.ToString();
            }
        }

        private void getSalesOrganization()
        {
            try{
            ds = obj.BindSalesOrganization();
            ddlSalesOrganization.DataTextField = ds.Tables["tblCMSalesOrganization"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlSalesOrganization.DataValueField = ds.Tables["tblCMSalesOrganization"].Columns["SalesOrganization"].ToString().Trim();             // to retrive specific  textfield name 
            ddlSalesOrganization.DataSource = ds.Tables["tblCMSalesOrganization"];      //assigning datasource to the dropdownlist
            ddlSalesOrganization.DataBind();  //binding dropdownlist
            ddlSalesOrganization.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getSalesOrganization" + ex.ToString();
            }
            }

        private void getDistributionChannel()
        {
            try{
            ds = obj.BindDistributionChannl();
            ddlDistributionChannel.DataTextField = ds.Tables["tblCMDistributionChannel"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlDistributionChannel.DataValueField = ds.Tables["tblCMDistributionChannel"].Columns["DistributionChannel"].ToString().Trim();             // to retrive specific  textfield name 
            ddlDistributionChannel.DataSource = ds.Tables["tblCMDistributionChannel"];      //assigning datasource to the dropdownlist
            ddlDistributionChannel.DataBind();  //binding dropdownlist
            ddlDistributionChannel.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getDistributionChannel" + ex.ToString();
            }
            }

        private void getDivision()
        {
            try{
            ds = obj.BindDivision();
            ddlDivision.DataTextField = ds.Tables["tblCMDivision"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlDivision.DataValueField = ds.Tables["tblCMDivision"].Columns["Division"].ToString().Trim();             // to retrive specific  textfield name 
            ddlDivision.DataSource = ds.Tables["tblCMDivision"];      //assigning datasource to the dropdownlist
            ddlDivision.DataBind();  //binding dropdownlist
            ddlDivision.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getDivision" + ex.ToString();
            }
            }

        private void getCountry()
        {
            try{
            ds = obj.BindCountry();
            ddlCountry.DataTextField = ds.Tables["tblCMCountry"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlCountry.DataValueField = ds.Tables["tblCMCountry"].Columns["Country"].ToString().Trim();             // to retrive specific  textfield name 
            ddlCountry.DataSource = ds.Tables["tblCMCountry"];      //assigning datasource to the dropdownlist
            ddlCountry.DataBind();  //binding dropdownlist
            ddlCountry.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getCountry" + ex.ToString();
            }
            }

        private void getRegion()
        {
            try{
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

        private void getSortKey()
        {
            try{
            ds = obj.BindShortKey();
            ddlSortKey.DataTextField = ds.Tables["tblVMSortKey"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlSortKey.DataValueField = ds.Tables["tblVMSortKey"].Columns["SortKeyNo"].ToString().Trim();             // to retrive specific  textfield name 
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
            try{
            ds = obj.BindReconAccnt();
            ddlReconAccount.DataTextField = ds.Tables["tblCMReconAccount"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlReconAccount.DataValueField = ds.Tables["tblCMReconAccount"].Columns["ReconAccount"].ToString().Trim();             // to retrive specific  textfield name 
            ddlReconAccount.DataSource = ds.Tables["tblCMReconAccount"];      //assigning datasource to the dropdownlist
            ddlReconAccount.DataBind();  //binding dropdownlist
            ddlReconAccount.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getReconAccount" + ex.ToString();
            }
            }

        private void getTermsofpayment()
        {
            try{
            ds = obj.BindTermsofpaymnt();
            ddlADTermsofpayment.DataTextField = ds.Tables["tblCMTermsofPayment"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlADTermsofpayment.DataValueField = ds.Tables["tblCMTermsofPayment"].Columns["TermsofPayment"].ToString().Trim();             // to retrive specific  textfield name 
            ddlADTermsofpayment.DataSource = ds.Tables["tblCMTermsofPayment"];      //assigning datasource to the dropdownlist
            ddlADTermsofpayment.DataBind();  //binding dropdownlist
            ddlADTermsofpayment.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getTermsofpayment" + ex.ToString();
            }
            }

        private void getPaymentBlock()
        {
            try{
            ds = obj.BindPaymentBlock();
            ddlPaymentBlock.DataTextField = ds.Tables["tblCMPaymentBlock"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlPaymentBlock.DataValueField = ds.Tables["tblCMPaymentBlock"].Columns["PaymentBlock"].ToString().Trim();             // to retrive specific  textfield name 
            ddlPaymentBlock.DataSource = ds.Tables["tblCMPaymentBlock"];      //assigning datasource to the dropdownlist
            ddlPaymentBlock.DataBind();  //binding dropdownlist
            ddlPaymentBlock.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getPaymentBlock" + ex.ToString();
            }
            }

        private void getSalesdistrict()
        {
            try{

            ds = obj.BindSalesdistrict();
            ddlSalesdistrict.DataTextField = ds.Tables["tblCMSalesDistrict"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlSalesdistrict.DataValueField = ds.Tables["tblCMSalesDistrict"].Columns["Salesdistrict"].ToString().Trim();             // to retrive specific  textfield name 
            ddlSalesdistrict.DataSource = ds.Tables["tblCMSalesDistrict"];      //assigning datasource to the dropdownlist
            ddlSalesdistrict.DataBind();  //binding dropdownlist
            ddlSalesdistrict.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getSalesdistrict" + ex.ToString();
            }
            }

        private void getCurrency()
        {
            try{
            ds = obj.BindCurrency();
            ddlCurrency.DataTextField = ds.Tables["tblCMCurrency"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlCurrency.DataValueField = ds.Tables["tblCMCurrency"].Columns["Currency"].ToString().Trim();             // to retrive specific  textfield name 
            ddlCurrency.DataSource = ds.Tables["tblCMCurrency"];      //assigning datasource to the dropdownlist
            ddlCurrency.DataBind();  //binding dropdownlist
            ddlCurrency.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getCurrency" + ex.ToString();
            }
            }

        private void getCustpricproc()
        {
            try{
            ds = obj.BindCustpricproc();
            ddlCustpricproc.DataTextField = ds.Tables["tblCMCustPricProc"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlCustpricproc.DataValueField = ds.Tables["tblCMCustPricProc"].Columns["Custpricproc"].ToString().Trim();             // to retrive specific  textfield name 
            ddlCustpricproc.DataSource = ds.Tables["tblCMCustPricProc"];      //assigning datasource to the dropdownlist
            ddlCustpricproc.DataBind();  //binding dropdownlist
            ddlCustpricproc.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getCustpricproc" + ex.ToString();
            }
            }

        private void getIncoterms()
        {
            try{
            ds = obj.BindIncoterms();
            ddlIncoterms.DataTextField = ds.Tables["tblCMIncoterms"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlIncoterms.DataValueField = ds.Tables["tblCMIncoterms"].Columns["Incoterms"].ToString().Trim();             // to retrive specific  textfield name 
            ddlIncoterms.DataSource = ds.Tables["tblCMIncoterms"];      //assigning datasource to the dropdownlist
            ddlIncoterms.DataBind();  //binding dropdownlist
            ddlIncoterms.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getIncoterms" + ex.ToString();
            }
            }

        private void getTermsofpaymt()
        {
            try{
            ds = obj.BindTermsofpaymnt();
            ddlTermsofpayment.DataTextField = ds.Tables["tblCMTermsofPayment"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlTermsofpayment.DataValueField = ds.Tables["tblCMTermsofPayment"].Columns["TermsofPayment"].ToString().Trim();             // to retrive specific  textfield name 
            ddlTermsofpayment.DataSource = ds.Tables["tblCMTermsofPayment"];      //assigning datasource to the dropdownlist
            ddlTermsofpayment.DataBind();  //binding dropdownlist
            ddlTermsofpayment.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getTermsofpaymt" + ex.ToString();
            }
            }

        private void getTax()
        {
            try{
            ds = obj.BindTax();
            ddlTax.DataTextField = ds.Tables["tblCMTax"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
            ddlTax.DataValueField = ds.Tables["tblCMTax"].Columns["Tax"].ToString().Trim();             // to retrive specific  textfield name 
            ddlTax.DataSource = ds.Tables["tblCMTax"];      //assigning datasource to the dropdownlist
            ddlTax.DataBind();  //binding dropdownlist
            ddlTax.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (Exception ex)
            {
                lblError.Text = "getTax" + ex.ToString();
            }
            }

        protected void getDataWhenQueryStringPass()
        {
            try{
            string TI = Request.QueryString["TransactionNo"].ToString().Trim();
            methodCall();

            cmd.CommandText = "";
            cmd.CommandText = "SELECT * from tbl_FI_CustomerMaster where TransactionMain = @TI";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TI", TI.ToString().Trim());
            adp.SelectCommand = cmd;
            adp.Fill(ds, "Data");
            if (ds.Tables["Data"].Rows.Count > 0)
            {
                lblMaxTransactionNo.Text = ds.Tables["Data"].Rows[0]["TransactionMain"].ToString().Trim();
                lblMaxTransactionID.Text = ds.Tables["Data"].Rows[0]["TransactionID"].ToString().Trim();
                txtcustomerCode.Text = ds.Tables["Data"].Rows[0]["CustomerCode"].ToString().Trim();
                ddlTitle.SelectedValue = ds.Tables["Data"].Rows[0]["Title"].ToString().Trim();
                txtName.Text = ds.Tables["Data"].Rows[0]["Name"].ToString().Trim();
                ddlAccountGroup.SelectedValue = ds.Tables["Data"].Rows[0]["AccountGroup"].ToString().Trim();
                //ddlAccountGroup.SelectedValue = ds.Tables["Data"].Rows[0]["AccountGroup"].ToString().Trim();
                ddlSalesOrganization.SelectedValue = ds.Tables["Data"].Rows[0]["SalesOrganization"].ToString().Trim();
                ddlCompanyCode.SelectedValue = ds.Tables["Data"].Rows[0]["CompanyCode"].ToString().Trim();
                ddlDistributionChannel.SelectedValue = ds.Tables["Data"].Rows[0]["DistributionChannel"].ToString().Trim();
                ddlDivision.SelectedValue = ds.Tables["Data"].Rows[0]["Division"].ToString().Trim();
                txtHouseNumber.Text = ds.Tables["Data"].Rows[0]["HouseNumber"].ToString().Trim();
                txtStreet.Text = ds.Tables["Data"].Rows[0]["Street"].ToString().Trim();
                ddlCountry.SelectedValue = ds.Tables["Data"].Rows[0]["Country"].ToString().Trim();
                txtCity.Text = ds.Tables["Data"].Rows[0]["City"].ToString().Trim();
                ddlRegion.SelectedValue = ds.Tables["Data"].Rows[0]["Region"].ToString().Trim();
                txtPostalCode.Text = ds.Tables["Data"].Rows[0]["PostalCode"].ToString().Trim();
                txtTaxPayerCNIC.Text = ds.Tables["Data"].Rows[0]["TaxPayerCNIC"].ToString().Trim();
                txtTaxPayerNTN.Text = ds.Tables["Data"].Rows[0]["TaxPayerNTN"].ToString().Trim();
                txtGSTNo.Text = ds.Tables["Data"].Rows[0]["GSTNo"].ToString().Trim();
                txtTelephone.Text = ds.Tables["Data"].Rows[0]["Telephone"].ToString().Trim();
                txtEmail.Text = ds.Tables["Data"].Rows[0]["Email"].ToString().Trim();
                ddlSortKey.SelectedValue = ds.Tables["Data"].Rows[0]["SortKey"].ToString().Trim();
                ddlReconAccount.SelectedValue = ds.Tables["Data"].Rows[0]["ReconAccount"].ToString().Trim();
                ddlADTermsofpayment.SelectedValue = ds.Tables["Data"].Rows[0]["Termsofpayment"].ToString().Trim();
                txtPaymentMethods.Text = ds.Tables["Data"].Rows[0]["PaymentMethods"].ToString().Trim();
                ddlPaymentBlock.SelectedValue = ds.Tables["Data"].Rows[0]["PaymentBlock"].ToString().Trim();
                string aa = ds.Tables["Data"].Rows[0]["Singlepayment"].ToString().Trim();
                if (aa == "1")
                {
                    CheckBox1.Checked = true;
                }
                else
                {
                    CheckBox1.Checked = false;
                }


                ddlSalesdistrict.SelectedValue = ds.Tables["Data"].Rows[0]["Salesdistrict"].ToString().Trim();
                ddlCurrency.SelectedValue = ds.Tables["Data"].Rows[0]["Currency"].ToString().Trim();
                ddlCustpricproc.SelectedValue = ds.Tables["Data"].Rows[0]["Custpricproc"].ToString().Trim();
                ddlIncoterms.SelectedValue = ds.Tables["Data"].Rows[0]["Incoterms"].ToString().Trim();
                ddlTermsofpayment.SelectedValue = ds.Tables["Data"].Rows[0]["Termsofpayment"].ToString().Trim();
                ddlTax.SelectedValue = ds.Tables["Data"].Rows[0]["Tax"].ToString().Trim();
            }
            }
            catch (Exception ex)
            {
                lblError.Text = "getDataWhenQueryStringPass" + ex.ToString();
            }
        }

        protected void getCMWithHoldingTaxDataDataWhenQueryStringPass()
        {
            try{
            string TI = lblMaxTransactionID.Text;
            cmd.CommandText = "";
            cmd.CommandText = @"SELECT TransactionID
      ,WTaxCode
      ,Withttype,
      CASE [WTax] 
	WHEN '1' THEN 'Checked'
    ELSE 'Unchecked'
	end as [w/tax]
      ,Obligfrom
      ,Obligto
      ,Wtaxnumber
      ,Exemptionnumber
      ,Exemptionrate
      ,Exemptionreas
      ,Exemptfrom
      ,ExemptTo
      ,Name
      FROM tblCMWithHoldingTax where TransactionID = @TIWHT";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TIWHT", TI.ToString().Trim());
            adp.SelectCommand = cmd;
            adp.Fill(ds, "tblCMWithHoldingTaxData");
            GridView4.DataSource = ds.Tables["tblCMWithHoldingTaxData"];
            GridView4.DataBind();
            if (ds.Tables["tblCMWithHoldingTaxData"].Rows.Count > 0)
            {
                GridView1.Visible = false;
                GridView4.Visible = true;
            }
            }
            catch (Exception ex)
            {
                lblError.Text = "getCMWithHoldingTaxDataDataWhenQueryStringPass" + ex.ToString();
            }

        }

        protected void getCMPartnerFunctionsDataDataWhenQueryStringPass()
        {
            try{
            string TI = lblMaxTransactionID.Text;
            cmd.CommandText = "";
            cmd.CommandText = @"SELECT TransactionID
      ,PF
      ,PartnerFunction
      ,Number
      ,Name
      ,PartnerDescription,
       CASE [DefaultG] 
	WHEN '1' THEN 'Checked'
    ELSE 'Unchecked'
	end as [Default]
      FROM tblCMPartnerFunctions where TransactionID = @TIPF";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TIPF", TI.ToString().Trim());
            adp.SelectCommand = cmd;
            adp.Fill(ds, "tblCMPartnerFunctionsData");
            GridView3.DataSource = ds.Tables["tblCMPartnerFunctionsData"];
            GridView3.DataBind();
            if (ds.Tables["tblCMWithHoldingTaxData"].Rows.Count > 0)
            {
                GridView2.Visible = false;
                GridView3.Visible = true;
            }
            }
            catch (Exception ex)
            {
                lblError.Text = "getCMPartnerFunctionsDataDataWhenQueryStringPass" + ex.ToString();
            }

        }

        protected void DisableControls(Control parent, bool State)
        {
            try{
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
            catch (Exception ex)
            {
                lblError.Text = "DisableControls" + ex.ToString();
            }
        }

        void ClearInputscolor(ControlCollection ctrlss)
        {
            try{
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
            try{
            ddlTitle.BackColor = System.Drawing.Color.AliceBlue;
            txtName.BackColor = System.Drawing.Color.AliceBlue;
            ddlSalesOrganization.BackColor = System.Drawing.Color.AliceBlue;
            ddlCompanyCode.BackColor = System.Drawing.Color.AliceBlue;
            txtStreet.BackColor = System.Drawing.Color.AliceBlue;
            ddlCountry.BackColor = System.Drawing.Color.AliceBlue;
            //txtTaxPayerCNIC.BackColor = System.Drawing.Color.AliceBlue;
            //txtTaxPayerNTN.BackColor = System.Drawing.Color.AliceBlue;
            ddlEmailMDA.BackColor = System.Drawing.Color.AliceBlue;
            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
            ddlCountry.BackColor = System.Drawing.Color.AliceBlue;
            }
            catch (Exception ex)
            {
                lblError.Text = "madatorycolor" + ex.ToString();
            }
        }

        ///////////////////////////////////////////////////Methods//////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////Grid Methods//////////////////////////////////////////////////////////

        protected void Grid1datainsert()
        {
            try{
            DataTable dtCurrentTable = (DataTable)ViewState["Grid1"];
            if (dtCurrentTable != null)
            {
                string consString = ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_SYS_CM_WithHoldingTax"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                        {
                            if (dtCurrentTable.Rows[i][1] == DBNull.Value)
                                dtCurrentTable.Rows[i].Delete();
                        }
                        //dtCurrentTable.AcceptChanges();
                        cmd.Parameters.AddWithValue("@tblWithHoldingTax", dtCurrentTable);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            //else
            //{
            //    GridView1.DataSource = table;
            //    GridView1.DataMember = "data";
            //    GridView1.DataBind();
            //}
            }
            catch (Exception ex)
            {
                lblError.Text = "Grid1datainsert" + ex.ToString();
            }
        }

        protected void Grid2datainsert()
        {
            try{
            DataTable dtCurrentTable = (DataTable)ViewState["Grid2"];
            if (dtCurrentTable != null)
            {
                string consString = ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_SYS_CM_PartnerFuntions"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                        {
                            if (dtCurrentTable.Rows[i][1] == DBNull.Value)
                                dtCurrentTable.Rows[i].Delete();
                        }
                        dtCurrentTable.AcceptChanges();
                        cmd.Parameters.AddWithValue("@tblPartnerFuntions", dtCurrentTable);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            }
            catch (Exception ex)
            {
                lblError.Text = "Grid2datainsert" + ex.ToString();
            }
        }

        private void GetHarcheyID()
        {
            try{
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
                        EmailSubject = "New Customer Master Creation Request – Form ID #" + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + "has sent you a New Customer Master Creation Request against  Form ID # " + lblMaxTransactionID.Text + " for approval.  <br><br> Your kind approval is required on the following URL: " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Customer Master Finance Application<br> Information Systems Dashboard";
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
                        EmailSubject = "New Customer Master Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has been issued against  Customer Master Creation Request Form ID # " + lblMaxTransactionID.Text + " <br><br> The form can be reviewed at the following URL:" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Customer Master Finance Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
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
            try{
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
                    EmailSubject = "New Customer Master Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + "has been disapproved by  " + ViewState["SessionUser"].ToString().Replace(".", " ") + " <br> The reason of rejection is given below you can review your form on following url:" +
                    "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                    "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                    "<br> <br> <br>Reject Remarks: " + txtRemarksReview.Text +
                    "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                    "<br>Customer Master Finance Application <br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();
                    ViewState["Status"] = HierachyCategoryStatus.ToString();
                    lblEmail.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    Page.MaintainScrollPositionOnPostBack = true;
                    lblEmail.Text = "*New Customer Master Creation Request against  Form ID # " + lblMaxTransactionID.Text + " has been rejected by you";
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
            catch (Exception ex)
            {
                lblError.Text = "ApplicationStatus" + ex.ToString();
            }
            
        }

        private void BindsysApplicationStatus()
        {
            try{
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
                        EmailSubject = "New Customer Master Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has been approved by Mr. " + ViewState["SessionUser"].ToString().Replace(".", " ") + " <br><br> Your kind approval is required for New Customer Master Creation Request on the following URL:  " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Customer Master Finance Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblEmail.Focus();
                        Page.MaintainScrollPositionOnPostBack = true;
                        lblEmail.Text = "*New Customer Master Creation Request against  Form ID # " + lblMaxTransactionID.Text + " has been approved by you";

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
                            EmailSubject = "New Customer Master Creation Request – Form ID # " + lblMaxTransactionID.Text + "";
                            EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> New Customer Master Creation Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr. " + ViewState["SessionUser"].ToString().Replace(".", " ") + " <br><br> You are requested to create a customer code information on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                                 "<br>Customer Master Finance Application<br> Information Systems Dashboard";
                            SessionUser = Session["User_Name"].ToString();
                            DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            InsertEmail();
                            ViewState["Status"] = HierachyCategoryStatus.ToString();
                            lblEmail.Focus();
                            Page.MaintainScrollPositionOnPostBack = false;
                            Page.MaintainScrollPositionOnPostBack = true;
                            lblEmail.Text = "*New Customer Master Creation Request against  Form ID # " + lblMaxTransactionID.Text + " has been approved by you";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "EmailWorkApproval" + ex.ToString();
            }

        }


        ///////////////////////////////////////////////////Email Working//////////////////////////////////////////////////////////
        #endregion

        protected void InsertEmail()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
  
                using (SqlCommand cmdInsertEmail = new SqlCommand("SP_InsertEmailAddress"))
                {
                    cmdInsertEmail.Connection = connection;
                    cmdInsertEmail.CommandType = CommandType.StoredProcedure;
                    cmdInsertEmail.Parameters.AddWithValue("@TransactionID", TransactionID.ToString());
                    cmdInsertEmail.Parameters.AddWithValue("@FormCode", FormCode.ToString());
                    cmdInsertEmail.Parameters.AddWithValue("@UserName",UserName.ToString() );
                    cmdInsertEmail.Parameters.AddWithValue("@UserEmail", UserEmail.ToString());
                    cmdInsertEmail.Parameters.AddWithValue("@EmailSubject",EmailSubject.ToString() );
                    cmdInsertEmail.Parameters.AddWithValue("@EmailBody", EmailBody.ToString());
                    cmdInsertEmail.Parameters.AddWithValue("@SessionUser", SessionUser.ToString() );
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
    }
}