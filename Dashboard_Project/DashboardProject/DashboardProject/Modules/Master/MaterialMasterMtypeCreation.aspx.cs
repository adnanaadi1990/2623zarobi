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

namespace ITLDashboard.Modules.Master
{
    public partial class MaterialMasterMTypeCreation : System.Web.UI.Page
    {
        string value = "";
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
        public string FormID = "103";
        public string FormType = "N";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        ComponentClass_FK objFK = new ComponentClass_FK();
        DataTable dt = new DataTable();
        DataTable dtcon = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsEmail = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        DataTable tableEmail = new DataTable();
        public decimal HIDdecimal;
        public decimal HIDSequance;
        public string FormCreatedBy = "";
        public string Finance = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            txtRemarksReview.Enabled = false;
            if (!IsPostBack)
            {
                // dvemaillbl.Visible = true;
                //   Pack.Visible = false;
                //   txtSMC.BackColor = System.Drawing.Color.AliceBlue;
                //  txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
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
                    //divEmail.Visible = true;
                    //  BD.Visible = true;
                    BindMaterialgroup();
                    BindPlantMtype();
                    BindPurchasingGroup();
                    BindValuationClass();
                    BindValuationCategoryMTYPE();
                    BindBaseUnitOfMeasureMTYPR();
                    BindMRPTypeMTYPE();
                    BindMrpGroupMtype();
                    BindMRPControllerMtype();
                    // txtRemarksReview.Visible = true;
                    ddlMSG.BackColor = System.Drawing.Color.AliceBlue;
                    ddlMG.BackColor = System.Drawing.Color.AliceBlue;
                    ddlMMBaseUnitOfMeasure.BackColor = System.Drawing.Color.AliceBlue;
                    txtDescription.BackColor = System.Drawing.Color.AliceBlue;
                    txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                    txtSMC.BackColor = System.Drawing.Color.AliceBlue;

                    ddlProdCatg.BackColor = System.Drawing.Color.AliceBlue;
                    ddlProdCatgsub1.BackColor = System.Drawing.Color.AliceBlue;
                    ddlProdCatgsub2.BackColor = System.Drawing.Color.AliceBlue;
                    ddlSalesUnit.BackColor = System.Drawing.Color.AliceBlue;
                    ddlItemCateguoryGroup.BackColor = System.Drawing.Color.AliceBlue;
                    ddlProfitCenter.BackColor = System.Drawing.Color.AliceBlue;
                    ddlProductionunit.BackColor = System.Drawing.Color.AliceBlue;
                    ddlUnitOfIssue.BackColor = System.Drawing.Color.AliceBlue;
                    ddlMrpType.BackColor = System.Drawing.Color.AliceBlue;
                    ddlMRPGroup.BackColor = System.Drawing.Color.AliceBlue;
                    ddlAvailabilitycheck.BackColor = System.Drawing.Color.AliceBlue;

                    if (Request.QueryString["TransactionNo"] != null)
                    {

                        txtStandardPrice.Enabled = false;
                        BindPageLoad();
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        BD.Visible = true;
                        //CF.Visible = true;
                        //Prod.Visible = true;
                        //Account.Visible = true;
                        //Pack.Visible = true;
                        //Purch.Visible = true;
                        //SD.Visible = true;
                        //QM.Visible = true;
                        //MRP.Visible = true;
                        dvSMC.Visible = false;
                        btnSearch.Visible = false;
                        txtRemarksReview.Visible = false;
                        chkBatchManagement.Enabled = false;
                        ddlProdCatg.Visible = true;
                        ddlProdCatgsub1.Visible = true;
                        ddlProdCatgsub2.Visible = true;
                        txtMSG.Visible = false;
                        ddlMSG.Visible = true;
                        grdWStatus.Visible = true;
                        DisableControls(Page, false);
                        txtRemarks.Enabled = false;
                        btnSave.Visible = false;
                        btnSaveSubmit.Visible = false;
                        btnCancel.Visible = false;
                        ddlStorageLocation.Visible = true;
                        txtStLocation.Visible = false;
                        this.ddlPlant.Attributes.Add("disabled", "");
                        this.ddlValuationType.Attributes.Add("disabled", "");
                        this.ddlStorageLocation.Attributes.Add("disabled", "");
                        this.pnlemail.Visible = false;
                        whenquerystringpass();

                        BindsysApplicationStatus();
                        GetHarcheyID();
                        getUserDetail();
                        GetStatusHierachyCategoryControls();
                        ddlPlant.Attributes.Remove("disabled");
                        for (int i = 0; i < ddlPlant.Items.Count; i++)
                        {
                            ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                        }
                        ddlStorageLocation.Attributes.Remove("disabled");
                        for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                        {
                            ddlStorageLocation.Items[i].Attributes.Add("disabled", "disabled");
                        }

                        ddlValuationType.Attributes.Remove("disabled");
                        for (int i = 0; i < ddlValuationType.Items.Count; i++)
                        {
                            ddlValuationType.Items[i].Attributes.Add("disabled", "disabled");
                        }

                        btnForward.Visible = false;
                        btnTUpdate.Visible = false;
                        btnFUpdate.Visible = false;
                        btnReject.Visible = false;
                        btnUpdate.Visible = false;
                         txtRemarksReview.Visible = true;
                        // txtRemarks.Enabled = false;
                        btnApprover.Visible = false;
                        btnEdit.Visible = false;
                        btnForward.Visible = false;
                        btnTransfer.Visible = false;
                        btnTransfer.Visible = false;

                        if (((string)ViewState["HID"]) == "1")
                        {
                            btnSaveSubmit.Visible = false;
                            btnSave.Visible = false;
                            txtRemarksReview.Visible = false;
                            btnApprover.Visible = false;
                            btnReject.Visible = false;
                            btnTransfer.Visible = false;
                            txtSMC.Enabled = false;
                            lblSap.Visible = true;
                            txtSMC.Visible = true;
                            dvSMC.Visible = false;
                            cbML.Enabled = false;
                            btnTUpdate.Visible = false;
                            btnFUpdate.Visible = false;
                            btnReject.Visible = false;
                            btnUpdate.Visible = false;
                            txtRemarks.Enabled = false;
                            btnApprover.Visible = false;
                            btnEdit.Visible = false;
                            btnForward.Visible = false;
                            btnTransfer.Visible = false;
                            controlForwardHide();
                        }

                        if (((string)ViewState["HID"]) == "2")
                        {
                            if ((((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Senior Merchandiser") ||
                                (((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Software Developer") ||
                              (((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Team Lead") ||
                                (((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Deputy Manager Production") ||
                                (((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Assistant Manager"))
                            {
                               
                                BD.Visible = true;
                                Prod.Visible = true;
                                SD.Visible = true;
                                QM.Visible = true;
                                MRP.Visible = true;
                                ddlProdCatg.Enabled = true;
                                ddlProdCatgsub1.Enabled = true;
                                ddlProdCatgsub2.Enabled = true;
                                ddlSalesOrg.Enabled = true;
                                ddlDistributionChannel.Enabled = true;
                                ddlSalesUnit.Enabled = true;
                                ddlDivision.Enabled = true;
                                ddlTaxClassification.Enabled = true;
                                ddlItemCateguoryGroup.Enabled = true;
                                ddlLoomType.Enabled = true;
                                ddlRoomReady.Enabled = true;
                                ddlSubDivision.Enabled = true;
                                ddlNOS.Enabled = true;
                                ddlTransportionGroup.Enabled = true;
                                ddlLoadingGroup.Enabled = true;
                                ddlProfitCenter.Enabled = true;
                                txtSalesodertext.Enabled = true;
                                ddlRate.Enabled = false;
                                ddlRebatecategoryRate.Enabled = false;
                                ///////////PROD////////////
                                ddlProductionunit.Enabled = true;
                                ddlUnitOfIssue.Enabled = true;
                                ddlProdsupervisor.Enabled = true;
                                ddlProdScheduleProfile.Enabled = true;
                                txtUnderDeliveryTollerance.Enabled = true;
                                txtOverDeliveryTollerance.Enabled = true;
                                ddlTaskListUsage.Enabled = true;
                                //////////QM////////////
                                ddlQMControlKey.Enabled = true;
                                chkInspectionSetup.Enabled = true;
                                chkQmProcActive.Enabled = true;
                                /////////MRP/////////////
                                ddlMrpType.Enabled = true;
                                ddlMRPGroup.Enabled = true;
                                txtReoderPoint.Enabled = true;
                                ddlMRPController.Enabled = true;
                                ddlAvailabilitycheck.Enabled = true;
                                txtMinimumLotSize.Enabled = true;
                                txtMaximumLotSize.Enabled = true;
                                txtMaximumstocklevel.Enabled = true;
                                ddlStrategygroup.Enabled = true;
                                ddlLotsize.Enabled = true;
                                TxtSchedMarginkey.Enabled = true;
                                ddlPeriodIndicator.Enabled = true;
                                //  DisableControls(Page, false);
                                ////////////BTN//////////////
                                btnReject.Visible = true;
                                btnUpdate.Visible = true;
                                txtRemarksReview.Visible = true;
                                txtRemarksReview.Enabled = true;
                                btnApprover.Visible = false;
                                btnEdit.Visible = false;
                                btnForward.Visible = false;
                                btnTransfer.Visible = false;
                                controlForwardHide();
                                rbNewWeightCheck.Enabled = true;
                            }
                            if ((((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Senior Manager") ||
                                (((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "SAP Business Analyst") ||
                                (((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "General Manager") ||
                                (((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Manager Operation"))
                            {
                                BD.Visible = true;
                                Prod.Visible = true;
                                SD.Visible = true;
                                QM.Visible = true;
                                MRP.Visible = true;
                                ddlProdCatg.Enabled = false;
                                ddlProdCatgsub1.Enabled = false;
                                ddlProdCatgsub2.Enabled = false;
                                ddlSalesOrg.Enabled = false;
                                ddlDistributionChannel.Enabled = false;
                                ddlSalesUnit.Enabled = false;
                                ddlDivision.Enabled = false;
                                ddlTaxClassification.Enabled = false;
                                ddlItemCateguoryGroup.Enabled = false;
                                ddlLoomType.Enabled = false;
                                ddlRoomReady.Enabled = false;
                                ddlSubDivision.Enabled = false;
                                ddlNOS.Enabled = false;
                                ddlTransportionGroup.Enabled = false;
                                ddlLoadingGroup.Enabled = false;
                                ddlProfitCenter.Enabled = false;
                                txtSalesodertext.Enabled = false;
                                ddlRate.Enabled = false;
                                ddlRebatecategoryRate.Enabled = false;
                                ///////////PROD////////////
                                ddlProductionunit.Enabled = false;
                                ddlUnitOfIssue.Enabled = false;
                                ddlProdsupervisor.Enabled = false;
                                ddlProdScheduleProfile.Enabled = false;
                                txtUnderDeliveryTollerance.Enabled = false;
                                txtOverDeliveryTollerance.Enabled = false;
                                ddlTaskListUsage.Enabled = false;
                                //////////QM////////////
                                ddlQMControlKey.Enabled = false;
                                chkInspectionSetup.Enabled = false;
                                chkQmProcActive.Enabled = false;
                                /////////MRP/////////////
                                ddlMrpType.Enabled = false;
                                ddlMRPGroup.Enabled = false;
                                txtReoderPoint.Enabled = false;
                                ddlMRPController.Enabled = false;
                                ddlAvailabilitycheck.Enabled = false;
                                txtMinimumLotSize.Enabled = false;
                                txtMaximumLotSize.Enabled = false;
                                txtMaximumstocklevel.Enabled = false;
                                ddlStrategygroup.Enabled = false;
                                ddlLotsize.Enabled = false;
                                TxtSchedMarginkey.Enabled = false;
                                ddlPeriodIndicator.Enabled = false;
                                ////////////BTN//////////////
                                btnReject.Visible = true;
                                btnUpdate.Visible = false;
                                txtRemarksReview.Visible = true;
                                txtRemarksReview.Enabled = true;
                                txtRemarks.Enabled = false;
                                btnApprover.Visible = true;
                                btnEdit.Visible = false;
                                btnForward.Visible = false;
                                btnTransfer.Visible = false;
                                controlForwardHide();
                            }
                            else if ((((string)ViewState["Department"]) == "Trade and Tax") || ((string)ViewState["Department"]) == "Trade and taxes")
                            {
                                BD.Visible = true;
                                Prod.Visible = true;
                                SD.Visible = true;
                                QM.Visible = true;
                                MRP.Visible = true;
                                ddlRate.Enabled = true;
                                ddlRebatecategoryRate.Enabled = true;
                                ////////////BTN//////////////
                                btnReject.Visible = true;
                                btnTUpdate.Visible = true;
                                btnUpdate.Visible = false;
                                txtRemarksReview.Visible = true;
                                txtRemarks.Enabled = false;
                                txtRemarksReview.Visible = true;
                                btnApprover.Visible = false;
                                btnEdit.Visible = false;
                                btnForward.Visible = false;
                                btnTransfer.Visible = false;
                                ddlRate.BackColor = System.Drawing.Color.AliceBlue;
                                ddlRebatecategoryRate.BackColor = System.Drawing.Color.AliceBlue;
                                controlForwardHide();
                            }

                            //if ((((string)ViewState["Designation"]) == "Deputy Manager") && (((string)ViewState["Department"]) == "Trade and Tax")
                            // || (((string)ViewState["Designation"]) == "Deputy Manager") && (((string)ViewState["Department"]) == "Trade and taxes"))
                            //{
                            //    BD.Visible = true;
                            //    Prod.Visible = true;
                            //    SD.Visible = true;
                            //    QM.Visible = true;
                            //    MRP.Visible = true;
                            //    ddlRate.Enabled = false;
                            //    ddlRebatecategoryRate.Enabled = false;
                            //    ////////////BTN//////////////
                            //    btnReject.Visible = true;
                            //    btnTUpdate.Visible = false;
                            //    btnUpdate.Visible = false;
                            //    txtRemarksReview.Visible = true;
                            //    txtRemarks.Enabled = false;
                            //    btnApprover.Visible = true;
                            //    btnEdit.Visible = false;
                            //    btnForward.Visible = false;
                            //    btnTransfer.Visible = false;
                            //    ddlRate.BackColor = System.Drawing.Color.AliceBlue;
                            //    ddlRebatecategoryRate.BackColor = System.Drawing.Color.AliceBlue;
                            //    controlForwardHide();
                            //}
                            else if ((((string)ViewState["Designation"]) == "Manager") && (((string)ViewState["Department"]) == "Marketing")
                             || (((string)ViewState["Designation"]) == "Senior Manager") && (((string)ViewState["Department"]) == "Marketing"))
                            {
                                BD.Visible = true;
                                Prod.Visible = true;
                                SD.Visible = true;
                                QM.Visible = true;
                                MRP.Visible = true;
                                Account.Visible = false;
                                ////////////BTN//////////////
                                btnReject.Visible = true;
                                btnTUpdate.Visible = false;
                                btnUpdate.Visible = false;
                                btnApprover.Visible = true;
                                txtRemarksReview.Visible = true;
                                txtRemarksReview.Enabled = true;
                                txtRemarks.Enabled = true;
                                btnEdit.Visible = false;
                                btnForward.Visible = false;
                                btnTransfer.Visible = false;
                                controlForwardHide();
                            }

                            else if (((string)ViewState["Department"]) == "Finance")
                            {
                                BD.Visible = true;
                                Prod.Visible = true;
                                SD.Visible = true;
                                QM.Visible = true;
                                MRP.Visible = true;
                                Account.Visible = true;
                                txtStandardPrice.BackColor = System.Drawing.Color.AliceBlue;
                                ddlValuationCategory.BackColor = System.Drawing.Color.AliceBlue;
                                ddlValuationType.BackColor = System.Drawing.Color.AliceBlue;
                                ddlValuationClass.BackColor = System.Drawing.Color.AliceBlue;
                                ddlValuationClass.Enabled = true;
                                ddlValuationCategory.Enabled = true;
                                ddlValuationType.Enabled = true;
                                txtStandardPrice.Enabled = true;
                                // DisableControls(Page, false);
                                ////////////BTN//////////////
                                btnReject.Visible = true;
                                btnFUpdate.Visible = true;
                                btnTUpdate.Visible = false;
                                btnUpdate.Visible = false;
                                txtRemarksReview.Visible = true;
                                txtRemarksReview.Enabled = true;
                                txtRemarks.Enabled = false;
                                btnApprover.Visible = false;
                                btnEdit.Visible = false;
                                btnForward.Visible = false;
                                btnTransfer.Visible = false;
                                // btnFUpdate.Visible = false;
                                controlForwardHide();
                            }
                            else if (((string)ViewState["Department"]) == "MIS")
                            {
                                BD.Visible = true;
                                Prod.Visible = true;
                                SD.Visible = true;
                                QM.Visible = true;
                                MRP.Visible = true;
                                Account.Visible = true;
                                ////////////BTN//////////////
                                btnReject.Visible = true;
                                btnTUpdate.Visible = false;
                                btnUpdate.Visible = false;
                                btnApprover.Visible = true;
                                txtRemarksReview.Visible = true;
                                txtRemarksReview.Enabled = true;
                                txtRemarks.Enabled = false;
                                btnEdit.Visible = false;
                                btnForward.Visible = false;
                                btnTransfer.Visible = false;
                                controlForwardHide();
                            }
                        }
                        if (((string)ViewState["HID"]) == "4")
                        {
                            BD.Visible = true;
                            Prod.Visible = true;
                            Account.Visible = true;
                            SD.Visible = true;
                            QM.Visible = true;
                            MRP.Visible = true;
                            Account.Visible = true;
                            ////////////BTN//////////////
                            txtSMC.Enabled = true;
                            txtSMC.Visible = true;
                            lblSap.Visible = true;
                            btnSaveSubmit.Visible = true;
                            btnApprover.Visible = false;
                            btnReject.Visible = true;
                            dvLock.Visible = true;
                            chkLock.Enabled = true;
                            btnTransfer.Visible = false;
                            txtRemarksReview.Visible = true;
                            txtRemarksReview.Enabled = true;
                            controlForwardHide();
                        }
                        if (((string)ViewState["HID"]) == "3")
                        {

                            //BD.Visible = true;
                            //Prod.Visible = true;
                            //SD.Visible = true;
                            //QM.Visible = true;
                            //MRP.Visible = true;
                            //Account.Visible = true;
                            btnSave.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnReviewed.Visible = true;
                            txtRemarksReview.Visible = true;
                            btnApprover.Visible = false;
                            btnReject.Visible = true;
                            btnTransfer.Visible = false;
                            txtSMC.Enabled = false;
                            lblSap.Visible = true;
                            txtSMC.Visible = true;
                            cbML.Enabled = false;
                            controlForwardHide();
                        }
                        btnTransfer.Visible = false;
                        btnForward.Visible = false;
                        controlForwardHide();
                    }
                    else
                    {
                        ds = objFK.FormDepartmentMarketing(Session["User_Name"].ToString());
                        if (ds.Tables["FormDepartmentMarketing"].Rows.Count > 0)
                        {
                            dt.Clear();
                            dt = ds.Tables["FormDepartmentMarketing"];
                            DataRow[] foundAuthors = dt.Select("user_name = '" + Session["User_Name"].ToString() + "'");
                            if (foundAuthors.Length != 0)
                            {
                                getUser();
                                //getUserHOD();
                                DummyGrid();
                                getUserDetail();
                                GetTransactionID();
                                BindPageLoad();
                            }
                            else
                            {
                                Response.Redirect("~/AccessDenied.aspx");
                            }
                        }
                        else
                        {
                            Response.Redirect("~/AccessDenied.aspx");
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void BindPageLoad()
        {
            try
            {
                //GetActiceDriectory();
                //  BindPlant();
                BindStorageLocation();
                // BindMaterialgroup();
                BindProductHierarchy();
                BindProductHierarchy2();
                BindProductHierarchy3();
                //  BindBaseUnitOfMeasure();
                //BindSplitValueation();
                BindProfitCenter();
                // BindValuationCategory();
                // BindPurchasingGroup();
                BindMRPController();
                BindMaterialSubGroup();
                // BindMRPType();
                BindLotSize();
                BindPeriodIndicator();
                BindStrategygroup();
                BindQMControlKey();
                BindAvailabilitycheck();
                BindRebateCategoryRate();
                BindRate();
                BindDistributionChannel();
                BindLoadingGroup();
                BindSalesTax();
                //BindValuationClass();
                BindProdnsupervisor();
                BindProdSchedProfile();
                BindTasklistusage();
                BindVolumeunit();
                Bindweightunit();
                BindItemCateguoryGroup();
                BindDivision();
                BindLoomType();
                BindRoomReady();
                BindSubDivision();
                BindNOS();
                BindTransportionGroup();
                BindPackagingMaterialCateguory();
                BindMrpGroup();
                BindBackFlush();
                BindPackagingMaterialType();
                BindLenght();

            }

            catch (Exception ex)
            {
                lblError.Text = "BindPageLoad" + ex.ToString();
            }
        }

        private void BindGrid()
        {
            try
            {

                cmd.CommandText = "SP_AltUnitOfMeasureGrid" + " @TransactionID='" + lblMaxTransactionID.Text.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                ds.Clear();
                adp.Fill(ds, "AltUnitOfMeasure");
                GridView1.DataSource = ds.Tables["AltUnitOfMeasure"];
                GridView1.DataBind();
                if (GridView1.Rows.Count >= 1)
                {
                    GridView1.Visible = true;
                }
                else
                {
                    GridView1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "BindGrid" + ex.ToString();
            }
        }

        protected void Add(object sender, EventArgs e)
        {

            try
            {
                lblgridError.Text = "";
                Control control = null;
                if (GridView1.FooterRow != null)
                {
                    control = GridView1.FooterRow;
                }
                else
                {
                    control = GridView1.Controls[0].Controls[0];
                }
                string customerName = (control.FindControl("ddlAltUnitOfMeasureCode") as DropDownList).Text;
                string companyName2 = (control.FindControl("txtNumerator") as TextBox).Text;
                string companyName3 = (control.FindControl("txtDenominator") as TextBox).Text;
                string companyName4 = (control.FindControl("txtLenght") as TextBox).Text;
                string companyName5 = (control.FindControl("txtWidth") as TextBox).Text;
                string companyName6 = (control.FindControl("txtheight") as TextBox).Text;
                //string customerName7 = (control.FindControl("ddlUOM") as DropDownList).Text;
                string customerName7 = ddlMMBaseUnitOfMeasure.SelectedValue.ToString();
                if (customerName7.ToString() == "0")
                {
                    lblgridError.Text = "Select any Base Unit of Measure from Basic Data";
                    return;
                }


                if (companyName2.ToString() == "")
                {
                    lblgridError.Text = "Numerator should not be left blank";
                    return;
                }
                if (companyName3.ToString() == "")
                {
                    lblgridError.Text = "Denominator should not be left blank";
                    return;
                }
                if (Int32.Parse(companyName2.ToString()) <= 0)
                {
                    lblgridError.Text = "Numerator should not be less then 0";
                    return;
                }
                if (Int32.Parse(companyName3.ToString()) <= 0)
                {
                    lblgridError.Text = "Denominator should not be less then 0";
                    return;
                }

                DataTable dt = (DataTable)ViewState["ConvertionFacter"];
                dt.Rows.Add(lblMaxTransactionID.Text, customerName.ToString().Trim(), companyName2.ToString().Trim(), companyName3.ToString().Trim(),
                   companyName4.ToString().Trim(), companyName5.ToString().Trim(), companyName6.ToString().Trim(), customerName7.ToString());
                ViewState["ConvertionFacter"] = dt;
                GridView1.DataSource = (DataTable)ViewState["ConvertionFacter"];
                GridView1.DataBind();
                ConvertionFactor.Focus();
                GridView1.Columns[0].Visible = true;

            }
            catch (Exception ex)
            {
                lblError.Text = "Add" + ex.ToString();
            }
        }

        private void DummyGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[8] { new DataColumn("TransactionID"), new DataColumn("AltUnitOfMeasureCode"), new DataColumn("Numerator"), new DataColumn("Denominator"), new DataColumn("Lenght"), new DataColumn("Width"), new DataColumn("height"), new DataColumn("UOM") });
                DataColumn c = new DataColumn("sno", typeof(int));
                c.AutoIncrement = true;
                c.AutoIncrementSeed = 1;
                c.AutoIncrementStep = 1;
                dt.Columns.Add(c);
                ViewState["ConvertionFacter"] = dt;
                GridView1.DataSource = (DataTable)ViewState["ConvertionFacter"];
                GridView1.DataBind();
                GridView1.Columns[0].Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = "DummyGrid" + ex.ToString();
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
                    //     lblHOD.Text = ds.Tables["getUserHOD"].Rows[0]["HODName"].ToString().Trim();
                }

            }
            catch (Exception ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "User HOD" + ex.ToString();
            }
        }

        protected void madatorycolor()
        {
            try
            {

                ddlMSG.BackColor = System.Drawing.Color.AliceBlue;
                ddlMG.BackColor = System.Drawing.Color.AliceBlue;
                ddlMMBaseUnitOfMeasure.BackColor = System.Drawing.Color.AliceBlue;
                txtDescription.BackColor = System.Drawing.Color.AliceBlue;
                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                ddlEmailMDA.BackColor = System.Drawing.Color.AliceBlue;

                ddlProdCatg.BackColor = System.Drawing.Color.AliceBlue;
                ddlProdCatgsub1.BackColor = System.Drawing.Color.AliceBlue;
                ddlProdCatgsub2.BackColor = System.Drawing.Color.AliceBlue;
                ddlSalesUnit.BackColor = System.Drawing.Color.AliceBlue;
                ddlItemCateguoryGroup.BackColor = System.Drawing.Color.AliceBlue;
                ddlProfitCenter.BackColor = System.Drawing.Color.AliceBlue;
                ddlProductionunit.BackColor = System.Drawing.Color.AliceBlue;
                ddlUnitOfIssue.BackColor = System.Drawing.Color.AliceBlue;
                ddlMrpType.BackColor = System.Drawing.Color.AliceBlue;
                ddlMRPGroup.BackColor = System.Drawing.Color.AliceBlue;
                ddlAvailabilitycheck.BackColor = System.Drawing.Color.AliceBlue;
            }

            catch (Exception ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "madatorycolor" + ex.ToString();
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
                lblError.Text = "GetTransactionID" + ex.ToString();
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
                    if (c is CheckBox)
                    {
                        ((CheckBox)(c)).Enabled = State;
                    }
                   
                    if (c is RadioButton)
                    {
                        ((RadioButton)(c)).Enabled = State;
                    }
                    DisableControls(c, State);
                    RadioButtonList2.Enabled = false;
                    ddlTransferUser.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "DisableControls" + ex.ToString();
            }
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
                lblError.Text = "GetHarcheyID" + ex.ToString();
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
                    btnApprover.Enabled = false;
                    btnReviewed.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSaveSubmit.Enabled = false;
                    btnEdit.Enabled = false;
                    btnTransfer.Attributes.Add("disabled", "true");
                    txtSMC.Attributes.Add("disabled", "true");
                    ddlRate.Attributes.Add("disabled", "true");
                    ddlRebatecategoryRate.Attributes.Add("disabled", "true");
                    cbML.Enabled = false;
                    cbML.Visible = true;
                    chkLock.Visible = false;
                    btnReject.Enabled = false;
                    btnReject.Attributes.Add("disabled", "true");
                    btnTransfer.Attributes.Add("disabled", "true");
                    btnFUpdate.Attributes.Add("disabled", "true");
                    btnTUpdate.Attributes.Add("disabled", "true");
                    btnUpdate.Attributes.Add("disabled", "true");
                    txtRemarksReview.Attributes.Add("disabled", "true");
                    btnUpdate.Attributes.Add("disabled", "true");
                    ddlValuationCategory.Attributes.Add("disabled", "true");
                    ddlValuationClass.Attributes.Add("disabled", "true");
                    ddlValuationType.Attributes.Add("disabled", "true");
                    txtStandardPrice.Attributes.Add("disabled", "true");
                    rbNewWeightCheck.Enabled = false;

                    rbNewWeightCheck.Items[0].Enabled = false;
                    rbNewWeightCheck.Items[1].Enabled = false;

                }
            }
            catch (Exception ex)
            {
                lblError.Text = "GetStatusHierachyCategoryControls" + ex.ToString();
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
                    ViewState["Designation"] = ds.Tables["tbluser_DisplayName"].Rows[0]["Designation"].ToString();
                    //ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
                }
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "getUserDetail" + ex.ToString();
            }
        }

        private void whenquerystringpass()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
                {
                    using (SqlCommand cmdGetData = new SqlCommand())
                    {

                        conn.Close();
                        string a = Request.QueryString["TransactionNo"].ToString();
                        cmdGetData.CommandText = @"SP_GetMaterialData";
                        cmdGetData.CommandType = CommandType.StoredProcedure;
                        cmdGetData.Connection = connection;
                        cmdGetData.Parameters.AddWithValue("@TMAIN", a.ToString());
                        adp.SelectCommand = cmdGetData;
                        dt.Clear();
                        adp.Fill(dt);
                        DataTableReader reader = dt.CreateDataReader();
                        while (reader.Read())
                        {
                            reader.Read();
                            lblMaxTransactionNo.Text = reader["TransactionMain"].ToString();
                            lblMaxTransactionID.Text = reader["TransactionID"].ToString();
                            getTransferUser();
                            ddlMaterialType.SelectedValue = reader["MaterialType"].ToString();
                            txtSMC.Text = reader["SAPMaterialCode"].ToString();
                            BindValuationCategoryMTYPE();
                            BindPlantMtype();

                            for (int i = 0; i < ddlPlant.Items.Count; i++)
                            {
                                foreach (string category in reader["Plant"].ToString().Split(','))
                                {
                                    if (category != ddlPlant.Items[i].Value) continue;
                                    ddlPlant.Items[i].Selected = true;
                                    break;
                                }
                            }
                            txtDescription.Text = reader["Description"].ToString();
                            BindBaseUnitOfMeasureMTYPR();
                            ddlMMBaseUnitOfMeasure.SelectedValue = reader["BaseUnitofMeasure"].ToString();
                            BindMaterialgroup();
                            ddlMG.SelectedValue = reader["MaterialGroup"].ToString().Trim();
                            ddlMSG.SelectedValue = reader["MaterialSubGroup"].ToString();
                            txtGROSSWEIGHT.Text = reader["GrossWeight"].ToString();
                            txtNETWEIGHT.Text = reader["NetWeight"].ToString();
                            if (reader["NetWeightCheck"].ToString() != "")
                            {
                                rbNewWeightCheck.SelectedValue = reader["NetWeightCheck"].ToString();
                            }
                            
                            ddlWeightunitBD.SelectedValue = reader["WeightUni"].ToString();
                            txtVolume.Text = reader["Volume"].ToString();
                            ddlVOLUMEUNIT.SelectedValue = reader["VolumeUnit"].ToString();
                            txtOldMaterialNumber.Text = reader["OldMaterailNo"].ToString();
                            txtCustomerNo.Text = reader["CustomerNo"].ToString();
                            txtSizeDimensions.Text = reader["Size_Dimension"].ToString();
                            ddlBasicDataPackagingMaterialCateguory.SelectedValue = reader["Packeging_Material_Catg"].ToString();
                            chkBatchManagement.SelectedValue = reader["BatchManagmet"].ToString();
                            string PH = reader["ProductHierarchy"].ToString();
                            string[] lines = PH.Split(',');
                            string aa = lines[0].Trim();
                            string ab = lines[1].Trim();
                            string ac = lines[2].Trim();
                            ddlProdCatg.SelectedValue = aa.ToString();
                            ddlProdCatgsub1.SelectedValue = ab.ToString();
                            ddlProdCatgsub2.SelectedValue = ac.ToString();
                            ddlDistributionChannel.SelectedValue = reader["DistributionChannel"].ToString();
                            ddlSalesOrg.SelectedValue = reader["SalesOrg"].ToString();
                            ddlSalesUnit.SelectedValue = reader["SalesUnit"].ToString();
                            ddlDivision.SelectedValue = reader["Division"].ToString();
                            ddlTaxClassification.SelectedValue = reader["TaxClasification"].ToString();
                            ddlItemCateguoryGroup.SelectedValue = reader["Item_Catg_Group"].ToString();
                            ddlLoomType.SelectedValue = reader["LoomType"].ToString();
                            ddlRoomReady.SelectedValue = reader["RoomReady"].ToString();
                            ddlSubDivision.SelectedValue = reader["SubDivision"].ToString();
                            ddlNOS.SelectedValue = reader["NOS"].ToString();
                            ddlAvailabilitycheck.SelectedValue = reader["Availabilitycheck"].ToString();
                            ddlTransportionGroup.SelectedValue = reader["TransportaionGroup"].ToString();
                            ddlLoadingGroup.SelectedValue = reader["LoadingGroup"].ToString();
                            ddlProfitCenter.SelectedValue = reader["ProfitCenter"].ToString();
                            txtSalesodertext.Text = reader["SalesOrderTax"].ToString();
                            ddlRate.SelectedValue = reader["Material_Rebate_Rate"].ToString();
                            ddlRebatecategoryRate.SelectedValue = reader["Rebate_Catg"].ToString();
                            BindMRPTypeMTYPE();
                            ddlMrpType.SelectedValue = reader["MRPType"].ToString();
                            ddlMRPGroup.SelectedValue = reader["MRP_Group"].ToString();
                            txtReoderPoint.Text = reader["ReoderPoint"].ToString();
                            ddlMRPController.SelectedValue = reader["MRPController"].ToString();
                            ddlProductionunit.SelectedValue = reader["Production_Unit_Of_Measure"].ToString();
                            ddlUnitOfIssue.SelectedValue = reader["UnitOfIssue"].ToString();
                            ddlProdsupervisor.SelectedValue = reader["Prodsupervisor"].ToString();
                            ddlProdScheduleProfile.SelectedValue = reader["ProdScheduleProfile"].ToString();
                            bindSLfromPlant();
                            for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                            {
                                foreach (string StorageLocation in reader["Storage_Location"].ToString().Split(','))
                                {
                                    if (StorageLocation != ddlStorageLocation.Items[i].Value) continue;
                                    ddlStorageLocation.Items[i].Selected = true;
                                    break;
                                }
                            }
                            txtUnderDeliveryTollerance.Text = reader["Under_Delivery_Tollerance"].ToString();
                            txtOverDeliveryTollerance.Text = reader["Ove_Delivery_Tollerance"].ToString();
                            ddlTaskListUsage.SelectedValue = reader["TaskListUsage"].ToString();
                            ddlQMControlKey.SelectedValue = reader["QMControlKey"].ToString();
                            string InspectionSetup = "";
                            InspectionSetup = reader["InspectionSetup"].ToString();
                            if (InspectionSetup == "1")
                            {
                                chkInspectionSetup.Checked = true;
                            }
                            else
                            {
                                chkInspectionSetup.Checked = false;
                            }
                            string QmProcActive = "";
                            QmProcActive = reader["QMprocactive"].ToString();
                            if (QmProcActive == "1")
                            {
                                chkQmProcActive.Checked = true;
                            }
                            else
                            {
                                chkQmProcActive.Checked = false;
                            }
                            txtMinimumLotSize.Text = reader["MinimumLotSize"].ToString();
                            txtMaximumLotSize.Text = reader["MaximumLotSize"].ToString();
                            txtMaximumstocklevel.Text = reader["Maximumstocklevel"].ToString();
                            TxtSchedMarginkey.Text = reader["SchedMarginkey"].ToString();
                            ddlPeriodIndicator.SelectedValue = reader["PeriodIndicator"].ToString();
                            ddlStrategygroup.SelectedValue = reader["Strategygroup"].ToString().Trim();
                            ddlLotsize.SelectedValue = reader["Lotsize"].ToString();
                            BindValuationClass();
                            ddlValuationClass.SelectedValue = reader["ValuationClass"].ToString();
                            ddlValuationCategory.SelectedValue = reader["ValuationCategory"].ToString();
                            BindSplitValueationMTYP();

                            for (int i = 0; i < ddlValuationType.Items.Count; i++)
                            {
                                foreach (string ValuationType in reader["ValuationType"].ToString().Split(','))
                                {
                                    if (ValuationType != ddlValuationType.Items[i].Value) continue;
                                    ddlValuationType.Items[i].Selected = true;
                                    break;
                                }
                            }

                            txtStandardPrice.Text = reader["StandardPrice"].ToString();
                            RadioButtonList2.SelectedValue = reader["ClosedBox"].ToString();
                            string MatLock = "";
                            MatLock = reader["Materiallock"].ToString();
                            if (MatLock == "1")
                            {
                                chkLock.Checked = true;
                                cbML.SelectedValue = "1";
                            }
                            else
                            {
                                chkLock.Checked = false;
                                cbML.SelectedValue = "0";
                            }
                            string ActionSelected = reader["Status"].ToString();

                            for (int i = 0; i < ddlPlant.Items.Count; i++)
                            {
                                ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                            }
                            for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                            {
                                ddlStorageLocation.Items[i].Attributes.Add("disabled", "disabled");
                            }
                            for (int i = 0; i < ddlValuationType.Items.Count; i++)
                            {
                                ddlValuationType.Items[i].Attributes.Add("disabled", "disabled");
                            }
                        }
                        reader.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "whenquerystringpass" + ex.ToString();
            }
        }


        protected void getUser()
        {
            try
            {
                cmd.CommandText = "";
                cmd.CommandText = "SP_getuserNotificationMIS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();
                ddlNotificationMIS.DataSource = cmd.ExecuteReader();
                ddlNotificationMIS.DataTextField = "DisplayName";
                ddlNotificationMIS.DataValueField = "user_name";
                ddlNotificationMIS.DataBind();
                ddlNotificationMIS.Items.Insert(0, new ListItem("------Select------", "0"));
                conn.Close();

                cmd.CommandText = "";
                cmd.CommandText = "SP_getuserMDA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();
                ddlEmailMDA.DataSource = cmd.ExecuteReader();
                ddlEmailMDA.DataTextField = "DisplayName";
                ddlEmailMDA.DataValueField = "user_name";
                ddlEmailMDA.DataBind();
                ddlEmailMDA.Items.Insert(0, new ListItem("------Select------", "0"));
                conn.Close();

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
                {
                    using (SqlCommand cmdgetdata = new SqlCommand())//
                    {
                        try
                        {
                            ds.Clear();
                            cmdgetdata.CommandText = "";
                            //cmd.CommandText = "SELECT COALESCE(MAX(MeterialNo), 0) +1 as TransactionID from tbl_SYS_MaterialMaster";
                            cmdgetdata.CommandText = "SP_getuserMHOD";
                            cmdgetdata.CommandType = CommandType.StoredProcedure;
                            cmdgetdata.Connection = connection;
                            adp.SelectCommand = cmdgetdata;
                            adp.Fill(dt);
                            ViewState["tblusermodulecategoryMerchandiser"] = dt;
                            //ddlMerchandiser.DataTextField = ds.Tables["tblusermodulecategoryMerchandiser"].Columns["DisplayName"].ToString(); // text field name of table dispalyed in dropdown
                            //ddlMerchandiser.DataValueField = ds.Tables["tblusermodulecategoryMerchandiser"].Columns["user_name"].ToString();             // to retrive specific  textfield name 
                            //ddlMerchandiser.DataSource = ds.Tables["tblusermodulecategoryMerchandiser"];      //assigning datasource to the dropdownlist
                            //ddlMerchandiser.DataBind();  //binding dropdownlist
                            ddlMerchandiser.Items.Insert(0, new ListItem("------Select------", "0"));
                        }
                        catch (Exception ex)
                        { ex.ToString(); }
                        finally
                        { conn.Close(); }

                    }
                }

                cmd.CommandText = "";
                cmd.CommandText = "SP_getuserTaxes";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();
                ddlTaxes.DataSource = cmd.ExecuteReader();
                ddlTaxes.DataTextField = "DisplayName";
                ddlTaxes.DataValueField = "user_name";
                ddlTaxes.DataBind();
                ddlTaxes.Items.Insert(0, new ListItem("------Select------", "0"));
                conn.Close();



                cmd.CommandText = "";
                cmd.CommandText = "SP_getuserMarketing";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();
                ddlMarketingHOD.DataSource = cmd.ExecuteReader();
                ddlMarketingHOD.DataTextField = "DisplayName";
                ddlMarketingHOD.DataValueField = "user_name";
                ddlMarketingHOD.DataBind();
                ddlMarketingHOD.Items.Insert(0, new ListItem("------Select------", "0"));
                conn.Close();


                cmd.CommandText = "SP_getuserNotificationFI";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();
                ddlNotificationFI.DataSource = cmd.ExecuteReader();
                ddlNotificationFI.DataTextField = "DisplayName";
                ddlNotificationFI.DataValueField = "user_name";
                ddlNotificationFI.DataBind();
                ddlNotificationFI.Items.Insert(0, new ListItem("------Select------", "0"));
                conn.Close();
                ddlMHOD.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "getUser" + ex.ToString();
            }
        }

        protected void getTransferUser()
        {//SELECT user_name,DisplayName FROM tbluser where user_name not in ('" + Session["User_Name"].ToString() + "')
            try
            {
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
                        if (ds.Tables.Contains("BindsysApplicationStatus"))
                        {
                            if (ds.Tables["BindsysApplicationStatus"].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables["BindsysApplicationStatus"].Rows.Count; i++)
                                {
                                    string val = ds.Tables["BindsysApplicationStatus"].Rows[i]["ID"].ToString().Trim();
                                    ListItem removeItem = ddlTransferUser.Items.FindByValue(val.ToString());
                                    ddlTransferUser.Items.Remove(removeItem);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "getTransferUser" + ex.ToString();
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
            catch (SqlException ex)
            {
                lblError.Text = "BindsysApplicationStatus" + ex.ToString();
            }
        }

        private void BindPlantMtype()
        {
            try
            {
                ddlPlant.Items.Clear();
                ds = obj.BindPlantMtype(ddlMaterialType.SelectedValue.ToString());
                ddlPlant.DataTextField = ds.Tables["BindPlantMtype"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlPlant.DataValueField = ds.Tables["BindPlantMtype"].Columns["PlantId"].ToString();             // to retrive specific  textfield name 
                ddlPlant.DataSource = ds.Tables["BindPlantMtype"];      //assigning datasource to the dropdownlist
                ddlPlant.DataBind();  //binding dropdownlist
                ddlPlant.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindPlantMtype" + ex.ToString();
            }
        }

        private void BindPlant()
        {
            try
            {
                ds = obj.BindPlant();
                ddlPlant.DataTextField = ds.Tables["Plant"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlPlant.DataValueField = ds.Tables["Plant"].Columns["PlantId"].ToString();             // to retrive specific  textfield name 
                ddlPlant.DataSource = ds.Tables["Plant"];      //assigning datasource to the dropdownlist
                ddlPlant.DataBind();  //binding dropdownlist
                ddlPlant.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindPlant" + ex.ToString();
            }
        }

        private void BindStorageLocation()
        {
            try
            {
                ds = obj.BindStorageLocation();
                ddlStorageLocation.DataTextField = ds.Tables["StorageLocation"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlStorageLocation.DataValueField = ds.Tables["StorageLocation"].Columns["StorageLocationcode"].ToString();             // to retrive specific  textfield name 
                ddlStorageLocation.DataSource = ds.Tables["StorageLocation"];      //assigning datasource to the dropdownlist
                ddlStorageLocation.DataBind();  //binding dropdownlist
                // Adding "Please select" option in dropdownlist for validation
                //ddlStorageLocation.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindStorageLocation" + ex.ToString();
            }
        }

        private void BindMaterialgroup()
        {
            try
            {
                ddlMG.Items.Clear();
                ds = obj.BindMaterialgroupMtype(ddlMaterialType.SelectedValue.ToString());
                ddlMG.DataTextField = ds.Tables["BindMaterialgroupMtype"].Columns["Description"].ToString().Trim(); // text field name of table dispalyed in dropdown
                ddlMG.DataValueField = ds.Tables["BindMaterialgroupMtype"].Columns["Materialgrpcode"].ToString().Trim();             // to retrive specific  textfield name 
                ddlMG.DataSource = ds.Tables["BindMaterialgroupMtype"];      //assigning datasource to the dropdownlist
                ddlMG.DataBind();  //binding dropdownlist
                //Adding "Please select" option in dropdownlist for validation
                ddlMG.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindMaterialgroup" + ex.ToString();
            }
        }

        private void BindLenght()
        {

        }

        private void BindBaseUnitOfMeasure()
        {
            try
            {
                ds = obj.BindBaseUnitOfMeasure();
                ddlMMBaseUnitOfMeasure.DataTextField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
                ddlMMBaseUnitOfMeasure.DataValueField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
                ddlMMBaseUnitOfMeasure.DataSource = ds.Tables["BaseUnitOfMeasure"];      //assigning datasource to the dropdownlist
                ddlMMBaseUnitOfMeasure.DataBind();  //binding dropdownlist
                ddlMMBaseUnitOfMeasure.Items.Insert(0, new ListItem("------Select------", "0"));

                ddlSalesUnit.DataTextField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
                ddlSalesUnit.DataValueField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
                ddlSalesUnit.DataSource = ds.Tables["BaseUnitOfMeasure"];      //assigning datasource to the dropdownlist
                ddlSalesUnit.DataBind();  //binding dropdownlist
                ddlSalesUnit.Items.Insert(0, new ListItem("------Select------", "0"));

                ddlProductionunit.DataTextField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
                ddlProductionunit.DataValueField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
                ddlProductionunit.DataSource = ds.Tables["BaseUnitOfMeasure"];      //assigning datasource to the dropdownlist
                ddlProductionunit.DataBind();  //binding dropdownlist
                ddlProductionunit.Items.Insert(0, new ListItem("------Select------", "0"));

                ddlUnitOfIssue.DataTextField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
                ddlUnitOfIssue.DataValueField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
                ddlUnitOfIssue.DataSource = ds.Tables["BaseUnitOfMeasure"];      //assigning datasource to the dropdownlist
                ddlUnitOfIssue.DataBind();  //binding dropdownlist
                ddlUnitOfIssue.Items.Insert(0, new ListItem("------Select------", "0"));

                //ddlOrderingUnit.DataTextField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
                //ddlOrderingUnit.DataValueField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
                //ddlOrderingUnit.DataSource = ds.Tables["BaseUnitOfMeasure"];      //assigning datasource to the dropdownlist
                //ddlOrderingUnit.DataBind();  //binding dropdownlist
                //ddlOrderingUnit.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindBaseUnitOfMeasure" + ex.ToString();
            }

        }

        private void BindBaseUnitOfMeasureMTYPR()
        {
            try
            {
                ddlMMBaseUnitOfMeasure.Items.Clear();
                ds = obj.BindBaseUnitOfMeasureMTYPE(ddlMaterialType.SelectedValue.ToString());
                ddlMMBaseUnitOfMeasure.DataTextField = ds.Tables["BindBaseUnitOfMeasureMTYPE"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
                ddlMMBaseUnitOfMeasure.DataValueField = ds.Tables["BindBaseUnitOfMeasureMTYPE"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
                ddlMMBaseUnitOfMeasure.DataSource = ds.Tables["BindBaseUnitOfMeasureMTYPE"];      //assigning datasource to the dropdownlist
                ddlMMBaseUnitOfMeasure.DataBind();  //binding dropdownlist
                ddlMMBaseUnitOfMeasure.Items.Insert(0, new ListItem("------Select------", "0"));

                ddlSalesUnit.DataTextField = ds.Tables["BindBaseUnitOfMeasureMTYPE"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
                ddlSalesUnit.DataValueField = ds.Tables["BindBaseUnitOfMeasureMTYPE"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
                ddlSalesUnit.DataSource = ds.Tables["BindBaseUnitOfMeasureMTYPE"];      //assigning datasource to the dropdownlist
                ddlSalesUnit.DataBind();  //binding dropdownlist
                ddlSalesUnit.Items.Insert(0, new ListItem("------Select------", "0"));

                ddlProductionunit.DataTextField = ds.Tables["BindBaseUnitOfMeasureMTYPE"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
                ddlProductionunit.DataValueField = ds.Tables["BindBaseUnitOfMeasureMTYPE"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
                ddlProductionunit.DataSource = ds.Tables["BindBaseUnitOfMeasureMTYPE"];      //assigning datasource to the dropdownlist
                ddlProductionunit.DataBind();  //binding dropdownlist
                ddlProductionunit.Items.Insert(0, new ListItem("------Select------", "0"));

                ddlUnitOfIssue.DataTextField = ds.Tables["BindBaseUnitOfMeasureMTYPE"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
                ddlUnitOfIssue.DataValueField = ds.Tables["BindBaseUnitOfMeasureMTYPE"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
                ddlUnitOfIssue.DataSource = ds.Tables["BindBaseUnitOfMeasureMTYPE"];      //assigning datasource to the dropdownlist
                ddlUnitOfIssue.DataBind();  //binding dropdownlist
                ddlUnitOfIssue.Items.Insert(0, new ListItem("------Select------", "0"));

                ddlOrderingUnit.DataTextField = ds.Tables["BindBaseUnitOfMeasureMTYPE"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
                ddlOrderingUnit.DataValueField = ds.Tables["BindBaseUnitOfMeasureMTYPE"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
                ddlOrderingUnit.DataSource = ds.Tables["BindBaseUnitOfMeasureMTYPE"];      //assigning datasource to the dropdownlist
                ddlOrderingUnit.DataBind();  //binding dropdownlist
                ddlOrderingUnit.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindBaseUnitOfMeasureMTYPR" + ex.ToString();
            }

        }

        private void BindSplitValueation()
        {
            try
            {
                ds = obj.BindSplitValueation();
                ddlValuationType.DataTextField = ds.Tables["ValuationType"].Columns["ValuationType"].ToString(); // text field name of table dispalyed in dropdown
                ddlValuationType.DataValueField = ds.Tables["ValuationType"].Columns["ValuationType"].ToString();             // to retrive specific  textfield name 
                ddlValuationType.DataSource = ds.Tables["ValuationType"];      //assigning datasource to the dropdownlist
                ddlValuationType.DataBind();  //binding dropdownlist
                //  ddlValuationType.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindSplitValueation" + ex.ToString();
            }
        }

        private void BindSplitValueationMTYP()
        {
            try
            {
                ddlValuationType.Items.Clear();
                ds = obj.BindSplitValueation(ddlValuationCategory.SelectedValue.ToString());
                ddlValuationType.DataTextField = ds.Tables["BindSplitValueation"].Columns["ValuationType"].ToString(); // text field name of table dispalyed in dropdown
                ddlValuationType.DataValueField = ds.Tables["BindSplitValueation"].Columns["ValuationType"].ToString();             // to retrive specific  textfield name 
                ddlValuationType.DataSource = ds.Tables["BindSplitValueation"];      //assigning datasource to the dropdownlist
                ddlValuationType.DataBind();  //binding dropdownlist
                //  ddlValuationType.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindSplitValueationMTYP" + ex.ToString();
            }
        }

        private void BindProfitCenter()
        {
            try
            {
                ds = obj.BindProfitCenter();
                ddlProfitCenter.DataTextField = ds.Tables["ProfitCenter"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlProfitCenter.DataValueField = ds.Tables["ProfitCenter"].Columns["ProfitCentercode"].ToString();             // to retrive specific  textfield name 
                ddlProfitCenter.DataSource = ds.Tables["ProfitCenter"];      //assigning datasource to the dropdownlist
                ddlProfitCenter.DataBind();  //binding dropdownlist
                //Adding "Please select" option in dropdownlist for validation
                ddlProfitCenter.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindProfitCenter" + ex.ToString();
            }
        }

        private void BindProductHierarchy()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    ds.Clear();
                    cmd.CommandText = "SELECT distinct [H1ID],[H1ID]+ ' ' + [H1Desc] as [H1Desc] FROM [dbo].[TBL_ProductHierarchy]";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "ProductHierarchy");
                    ddlProdCatg.DataTextField = ds.Tables["ProductHierarchy"].Columns["H1Desc"].ToString(); // text field name of table dispalyed in dropdown
                    ddlProdCatg.DataValueField = ds.Tables["ProductHierarchy"].Columns["H1ID"].ToString();             // to retrive specific  textfield name 
                    ddlProdCatg.DataSource = ds.Tables["ProductHierarchy"];      //assigning datasource to the dropdownlist
                    ddlProdCatg.DataBind();  //binding dropdownlist
                    ddlProdCatg.Items.Insert(0, new ListItem("------Select------", "0"));
                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindProductHierarchy" + ex.ToString();
            }
        }

        private void BindProductHierarchy2()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    ds.Clear();
                    cmd.CommandText = "SELECT distinct [H2ID],[H2ID]+ ' ' + [H2Desc] as [H2Desc] FROM [dbo].[TBL_ProductHierarchy]";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "ProductHierarchyH2");
                    ddlProdCatgsub1.DataTextField = ds.Tables["ProductHierarchyH2"].Columns["H2Desc"].ToString(); // text field name of table dispalyed in dropdown
                    ddlProdCatgsub1.DataValueField = ds.Tables["ProductHierarchyH2"].Columns["H2ID"].ToString();             // to retrive specific  textfield name 
                    ddlProdCatgsub1.DataSource = ds.Tables["ProductHierarchyH2"];      //assigning datasource to the dropdownlist
                    ddlProdCatgsub1.DataBind();  //binding dropdownlist
                    ddlProdCatgsub1.Items.Insert(0, new ListItem("------Select------", "0"));
                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindProductHierarchy2" + ex.ToString();
            }

        }

        private void BindProductHierarchy3()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    ds.Clear();
                    cmd.CommandText = "SELECT distinct [H3ID],[H3ID]+ ' ' + [H3Desc] as [H3Desc] FROM [dbo].[TBL_ProductHierarchy]";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;

                    adp.Fill(ds, "ProductHierarchyH3");
                    ddlProdCatgsub2.DataTextField = ds.Tables["ProductHierarchyH3"].Columns["H3Desc"].ToString(); // text field name of table dispalyed in dropdown
                    ddlProdCatgsub2.DataValueField = ds.Tables["ProductHierarchyH3"].Columns["H3ID"].ToString();             // to retrive specific  textfield name 
                    ddlProdCatgsub2.DataSource = ds.Tables["ProductHierarchyH3"];      //assigning datasource to the dropdownlist
                    ddlProdCatgsub2.DataBind();  //binding dropdownlist
                    ddlProdCatgsub2.Items.Insert(0, new ListItem("------Select------", "0"));

                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindProductHierarchy3" + ex.ToString();
            }

        }

        private void BindValuationCategoryMTYPE()
        {
            try
            {
                ddlValuationCategory.Items.Clear();
                ds = obj.BindValuationCategoryMTYPE(ddlMaterialType.SelectedValue.ToString());
                ddlValuationCategory.DataTextField = ds.Tables["ValuationCategoryMTYPE"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlValuationCategory.DataValueField = ds.Tables["ValuationCategoryMTYPE"].Columns["ValuationCategorycode"].ToString();             // to retrive specific  textfield name 
                ddlValuationCategory.DataSource = ds.Tables["ValuationCategoryMTYPE"];      //assigning datasource to the dropdownlist
                ddlValuationCategory.DataBind();  //binding dropdownlist
                ddlValuationCategory.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindValuationCategoryMTYPE" + ex.ToString();
            }
        }

        private void BindPurchasingGroup()
        {
            try
            {
                ddlPurchasingGroup.Items.Clear();
                ds = obj.BindPurchasingGroupMTYPE(ddlMaterialType.SelectedValue.ToString());
                ddlPurchasingGroup.DataTextField = ds.Tables["BindPurchasingGroupMTYPE"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlPurchasingGroup.DataValueField = ds.Tables["BindPurchasingGroupMTYPE"].Columns["PurchasingGroupcode"].ToString().Trim();             // to retrive specific  textfield name 
                ddlPurchasingGroup.DataSource = ds.Tables["BindPurchasingGroupMTYPE"];      //assigning datasource to the dropdownlist
                ddlPurchasingGroup.DataBind();  //binding dropdownlist
                ddlPurchasingGroup.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindPurchasingGroup" + ex.ToString();
            }

        }

        private void BindMRPController()
        {
            try
            {
                ds = obj.BindMRPController();
                ddlMRPController.DataTextField = ds.Tables["mrpController"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlMRPController.DataValueField = ds.Tables["mrpController"].Columns["mrpControllercode"].ToString();             // to retrive specific  textfield name 
                ddlMRPController.DataSource = ds.Tables["mrpController"];      //assigning datasource to the dropdownlist
                ddlMRPController.DataBind();  //binding dropdownlist
                ddlMRPController.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindMRPController" + ex.ToString();
            }
        }

        private void BindMRPType()
        {
            try
            {
                ds = obj.BindMRPType();
                ddlMrpType.DataTextField = ds.Tables["MRPType"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlMrpType.DataValueField = ds.Tables["MRPType"].Columns["mrptypecode"].ToString();             // to retrive specific  textfield name 
                ddlMrpType.DataSource = ds.Tables["MRPType"];      //assigning datasource to the dropdownlist
                ddlMrpType.DataBind();  //binding dropdownlist
                ddlMrpType.Items.Insert(0, new ListItem("------Select------", "0"));
                ddlMrpType.SelectedValue = "ND";
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindMRPType" + ex.ToString();
            }
        }

        private void BindMRPTypeMTYPE()
        {
            try
            {
                ddlMrpType.Items.Clear();
                ds = obj.BindMRPtypeMTYPE(ddlMaterialType.SelectedValue.ToString());
                ddlMrpType.DataTextField = ds.Tables["BindMRPtypeMTYPE"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlMrpType.DataValueField = ds.Tables["BindMRPtypeMTYPE"].Columns["mrptypecode"].ToString();             // to retrive specific  textfield name 
                ddlMrpType.DataSource = ds.Tables["BindMRPtypeMTYPE"];      //assigning datasource to the dropdownlist
                ddlMrpType.DataBind();  //binding dropdownlist
                ddlMrpType.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindMRPTypeMTYPE" + ex.ToString();
            }
        }

        private void BindLotSize()
        {
            try
            {
                ds = obj.BindLotSize();
                ddlLotsize.DataTextField = ds.Tables["LotSize"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlLotsize.DataValueField = ds.Tables["LotSize"].Columns["LotSizecode"].ToString();             // to retrive specific  textfield name 
                ddlLotsize.DataSource = ds.Tables["LotSize"];      //assigning datasource to the dropdownlist
                ddlLotsize.DataBind();  //binding dropdownlist
                ddlLotsize.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindLotSize" + ex.ToString();
            }
        }

        private void BindPeriodIndicator()
        {
            try
            {
                ds = obj.BindPeriodIndicator();
                ddlPeriodIndicator.DataTextField = ds.Tables["PeriodIndicator"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlPeriodIndicator.DataValueField = ds.Tables["PeriodIndicator"].Columns["PeriodIndicartorcode"].ToString();             // to retrive specific  textfield name 
                ddlPeriodIndicator.DataSource = ds.Tables["PeriodIndicator"];      //assigning datasource to the dropdownlist
                ddlPeriodIndicator.DataBind();  //binding dropdownlist
                ddlPeriodIndicator.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindPeriodIndicator" + ex.ToString();
            }
        }

        private void BindStrategygroup()
        {
            try
            {
                ds = obj.BindStrategygroup();
                ddlStrategygroup.DataTextField = ds.Tables["Strategygroup"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlStrategygroup.DataValueField = ds.Tables["Strategygroup"].Columns["Strategygroupcode"].ToString();             // to retrive specific  textfield name 
                ddlStrategygroup.DataSource = ds.Tables["Strategygroup"];      //assigning datasource to the dropdownlist
                ddlStrategygroup.DataBind();  //binding dropdownlist
                ddlStrategygroup.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindStrategygroup" + ex.ToString();
            }
        }

        private void BindQMControlKey()
        {
            try
            {
                ds = obj.BindQMControlKey();
                ddlQMControlKey.DataTextField = ds.Tables["QMControlKey"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlQMControlKey.DataValueField = ds.Tables["QMControlKey"].Columns["QMControlKeyCode"].ToString();             // to retrive specific  textfield name 
                ddlQMControlKey.DataSource = ds.Tables["QMControlKey"];      //assigning datasource to the dropdownlist
                ddlQMControlKey.DataBind();  //binding dropdownlist
                ddlQMControlKey.Items.Insert(0, new ListItem("------Select------", "0"));

                // ddlQMControlKey.SelectedValue = "0001";
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindQMControlKey" + ex.ToString();
            }
        }

        private void BindAvailabilitycheck()
        {
            try
            {
                ds = obj.BindAvailabilitycheck();
                ddlAvailabilitycheck.DataTextField = ds.Tables["Availabilitycheck"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlAvailabilitycheck.DataValueField = ds.Tables["Availabilitycheck"].Columns["Availabilitycheckcode"].ToString();             // to retrive specific  textfield name 
                ddlAvailabilitycheck.DataSource = ds.Tables["Availabilitycheck"];      //assigning datasource to the dropdownlist
                ddlAvailabilitycheck.DataBind();  //binding dropdownlist
                ddlAvailabilitycheck.Items.Insert(0, new ListItem("------Select------", "0"));
                ddlAvailabilitycheck.SelectedValue = "KP";
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindAvailabilitycheck" + ex.ToString();
            }
        }

        private void BindRebateCategoryRate()
        {
            try
            {
                ds = obj.BindRebateCategoryRate();
                ddlRebatecategoryRate.DataTextField = ds.Tables["RebateCategoryRate"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlRebatecategoryRate.DataValueField = ds.Tables["RebateCategoryRate"].Columns["Rebatecategorycode"].ToString();             // to retrive specific  textfield name 
                ddlRebatecategoryRate.DataSource = ds.Tables["RebateCategoryRate"];      //assigning datasource to the dropdownlist
                ddlRebatecategoryRate.DataBind();  //binding dropdownlist
                ddlRebatecategoryRate.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindRebateCategoryRate" + ex.ToString();
            }
        }

        private void BindRate()
        {
            try
            {
                ds = obj.BindRate();
                ddlRate.DataTextField = ds.Tables["Rate"].Columns["Rate"].ToString().Trim(); // text field name of table dispalyed in dropdown
                ddlRate.DataValueField = ds.Tables["Rate"].Columns["Rate"].ToString().Trim();             // to retrive specific  textfield name 
                ddlRate.DataSource = ds.Tables["Rate"];      //assigning datasource to the dropdownlist
                ddlRate.DataBind();  //binding dropdownlist
                ddlRate.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindRate" + ex.ToString();
            }
        }

        private void BindDistributionChannel()
        {
            try
            {
                ds = obj.BindDistributionChannel();
                ddlDistributionChannel.DataTextField = ds.Tables["DistributionChannel"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlDistributionChannel.DataValueField = ds.Tables["DistributionChannel"].Columns["DistributionChannelcode"].ToString();             // to retrive specific  textfield name 
                ddlDistributionChannel.DataSource = ds.Tables["DistributionChannel"];      //assigning datasource to the dropdownlist
                ddlDistributionChannel.DataBind();  //binding dropdownlist
                ddlDistributionChannel.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindDistributionChannel" + ex.ToString();
            }
        }

        private void BindLoadingGroup()
        {
            try
            {
                ds = obj.BindLoadingGroup();
                ddlLoadingGroup.DataTextField = ds.Tables["LoadingGroup"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlLoadingGroup.DataValueField = ds.Tables["LoadingGroup"].Columns["LoadingGroupcode"].ToString();             // to retrive specific  textfield name 
                ddlLoadingGroup.DataSource = ds.Tables["LoadingGroup"];      //assigning datasource to the dropdownlist
                ddlLoadingGroup.DataBind();  //binding dropdownlist
                ddlLoadingGroup.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindLoadingGroup" + ex.ToString();
            }
        }

        private void BindSalesTax()
        {
            try
            {
                ds = obj.BindSalesTax();
                ddlTaxClassification.DataTextField = ds.Tables["SalesTax"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlTaxClassification.DataValueField = ds.Tables["SalesTax"].Columns["TaxClassificationcode"].ToString();             // to retrive specific  textfield name 
                ddlTaxClassification.DataSource = ds.Tables["SalesTax"];      //assigning datasource to the dropdownlist
                ddlTaxClassification.DataBind();  //binding dropdownlist
                ddlTaxClassification.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindSalesTax" + ex.ToString();
            }
        }

        private void BindMaterialSubGroup()
        {
            try
            {
                cmd.CommandText = "SP_MaterialSubGroup";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "MeterialGroup");
                ddlMSG.DataTextField = ds.Tables["MeterialGroup"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlMSG.DataValueField = ds.Tables["MeterialGroup"].Columns["MaterialSubGroupcode"].ToString();             // to retrive specific  textfield name 
                ddlMSG.DataSource = ds.Tables["MeterialGroup"];      //assigning datasource to the dropdownlist
                ddlMSG.DataBind();  //binding dropdownlist
                ddlMSG.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindMaterialSubGroup" + ex.ToString();
            }
        }

        private void BindVolumeunit()
        {
            try
            {
                cmd.CommandText = "SP_Volumeunit";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "Volumeunit");
                ddlVOLUMEUNIT.DataTextField = ds.Tables["Volumeunit"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlVOLUMEUNIT.DataValueField = ds.Tables["Volumeunit"].Columns["Volumeunit"].ToString();             // to retrive specific  textfield name 
                ddlVOLUMEUNIT.DataSource = ds.Tables["Volumeunit"];      //assigning datasource to the dropdownlist
                ddlVOLUMEUNIT.DataBind();  //binding dropdownlist
                ddlVOLUMEUNIT.Items.Insert(0, new ListItem("------Select------", "0"));

                //ddlVolumUnit.DataTextField = ds.Tables["Volumeunit"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                //ddlVolumUnit.DataValueField = ds.Tables["Volumeunit"].Columns["Volumeunit"].ToString();             // to retrive specific  textfield name 
                //ddlVolumUnit.DataSource = ds.Tables["Volumeunit"];      //assigning datasource to the dropdownlist
                //ddlVolumUnit.DataBind();  //binding dropdownlist
                ////Adding "Please select" option in dropdownlist for validation
                //ddlVolumUnit.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindVolumeunit" + ex.ToString();
            }
        }

        private void Bindweightunit()
        {
            try
            {
                cmd.CommandText = "SP_weightunit";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "weightunit");
                ddlWeightUnit.DataTextField = ds.Tables["weightunit"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlWeightUnit.DataValueField = ds.Tables["weightunit"].Columns["Weightunitcode"].ToString();             // to retrive specific  textfield name 
                ddlWeightUnit.DataSource = ds.Tables["weightunit"];      //assigning datasource to the dropdownlist
                ddlWeightUnit.DataBind();  //binding dropdownlist
                ddlWeightUnit.Items.Insert(0, new ListItem("------Select------", "0"));


                ddlWeightunitBD.DataTextField = ds.Tables["weightunit"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlWeightunitBD.DataValueField = ds.Tables["weightunit"].Columns["Weightunitcode"].ToString();             // to retrive specific  textfield name 
                ddlWeightunitBD.DataSource = ds.Tables["weightunit"];      //assigning datasource to the dropdownlist
                ddlWeightunitBD.DataBind();  //binding dropdownlist
                ddlWeightunitBD.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "Bindweightunit" + ex.ToString();
            }

        }

        private void BindDivision()
        {
            try
            {
                cmd.CommandText = "SP_Division";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "Division");
                ddlDivision.DataTextField = ds.Tables["Division"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlDivision.DataValueField = ds.Tables["Division"].Columns["Divisioncode"].ToString();             // to retrive specific  textfield name 
                ddlDivision.DataSource = ds.Tables["Division"];      //assigning datasource to the dropdownlist
                ddlDivision.DataBind();  //binding dropdownlist
                ddlDivision.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindDivision" + ex.ToString();
            }

        }

        private void BindItemCateguoryGroup()
        {
            try
            {
                cmd.CommandText = "SP_ItemCateguoryGroup";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "ItemCateguoryGroup");
                ddlItemCateguoryGroup.DataTextField = ds.Tables["ItemCateguoryGroup"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlItemCateguoryGroup.DataValueField = ds.Tables["ItemCateguoryGroup"].Columns["ItemCateguoryGroupcode"].ToString();             // to retrive specific  textfield name 
                ddlItemCateguoryGroup.DataSource = ds.Tables["ItemCateguoryGroup"];      //assigning datasource to the dropdownlist
                ddlItemCateguoryGroup.DataBind();  //binding dropdownlist
                ddlItemCateguoryGroup.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindItemCateguoryGroup" + ex.ToString();
            }
        }

        private void BindLoomType()
        {
            try
            {
                cmd.CommandText = "SP_LoomType";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "LoomType");
                ddlLoomType.DataTextField = ds.Tables["LoomType"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlLoomType.DataValueField = ds.Tables["LoomType"].Columns["LoomTypecode"].ToString();             // to retrive specific  textfield name 
                ddlLoomType.DataSource = ds.Tables["LoomType"];      //assigning datasource to the dropdownlist
                ddlLoomType.DataBind();  //binding dropdownlist
                ddlLoomType.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindLoomType" + ex.ToString();
            }
        }

        private void BindRoomReady()
        {
            try
            {
                cmd.CommandText = "SP_RoomReady";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "RoomReady");
                ddlRoomReady.DataTextField = ds.Tables["RoomReady"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlRoomReady.DataValueField = ds.Tables["RoomReady"].Columns["RoomReadycode"].ToString();             // to retrive specific  textfield name 
                ddlRoomReady.DataSource = ds.Tables["RoomReady"];      //assigning datasource to the dropdownlist
                ddlRoomReady.DataBind();  //binding dropdownlist
                ddlRoomReady.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindRoomReady" + ex.ToString();
            }
        }

        private void BindSubDivision()
        {
            try
            {
                cmd.CommandText = "SP_SubDivision";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "SubDivision");
                ddlSubDivision.DataTextField = ds.Tables["SubDivision"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlSubDivision.DataValueField = ds.Tables["SubDivision"].Columns["SubDivisioncode"].ToString();             // to retrive specific  textfield name 
                ddlSubDivision.DataSource = ds.Tables["SubDivision"];      //assigning datasource to the dropdownlist
                ddlSubDivision.DataBind();
                ddlSubDivision.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindSubDivision" + ex.ToString();
            }
        }

        private void BindNOS()
        {
            try
            {
                cmd.CommandText = "SP_NOS";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "NOS");
                ddlNOS.DataTextField = ds.Tables["NOS"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlNOS.DataValueField = ds.Tables["NOS"].Columns["NOS"].ToString();             // to retrive specific  textfield name 
                ddlNOS.DataSource = ds.Tables["NOS"];      //assigning datasource to the dropdownlist
                ddlNOS.DataBind();  //binding dropdownlist
                ddlNOS.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindNOS" + ex.ToString();
            }
        }

        private void BindTransportionGroup()
        {
            try
            {
                cmd.CommandText = "SP_TransportionGroup";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "TransportionGroup");
                ddlTransportionGroup.DataTextField = ds.Tables["TransportionGroup"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlTransportionGroup.DataValueField = ds.Tables["TransportionGroup"].Columns["TransportionGroupcode"].ToString();             // to retrive specific  textfield name 
                ddlTransportionGroup.DataSource = ds.Tables["TransportionGroup"];      //assigning datasource to the dropdownlist
                ddlTransportionGroup.DataBind();  //binding dropdownlist
                ddlTransportionGroup.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindTransportionGroup" + ex.ToString();
            }
        }

        private void BindPackagingMaterialCateguory()
        {
            try
            {
                cmd.CommandText = "SP_PackagingMaterialCateguory";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "PackagingMaterialCateguory");
                ddlPackagingMaterialCateguory.DataTextField = ds.Tables["PackagingMaterialCateguory"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlPackagingMaterialCateguory.DataValueField = ds.Tables["PackagingMaterialCateguory"].Columns["PackagingMaterialCateguorycode"].ToString();             // to retrive specific  textfield name 
                ddlPackagingMaterialCateguory.DataSource = ds.Tables["PackagingMaterialCateguory"];      //assigning datasource to the dropdownlist
                ddlPackagingMaterialCateguory.DataBind();  //binding dropdownlist
                ddlPackagingMaterialCateguory.Items.Insert(0, new ListItem("------Select------", "0"));


                ddlBasicDataPackagingMaterialCateguory.DataTextField = ds.Tables["PackagingMaterialCateguory"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlBasicDataPackagingMaterialCateguory.DataValueField = ds.Tables["PackagingMaterialCateguory"].Columns["PackagingMaterialCateguorycode"].ToString();             // to retrive specific  textfield name 
                ddlBasicDataPackagingMaterialCateguory.DataSource = ds.Tables["PackagingMaterialCateguory"];      //assigning datasource to the dropdownlist
                ddlBasicDataPackagingMaterialCateguory.DataBind();  //binding dropdownlist
                ddlBasicDataPackagingMaterialCateguory.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindPackagingMaterialCateguory" + ex.ToString();
            }
        }

        private void BindMrpGroup()
        {
            try
            {
                cmd.CommandText = "SP_MrpGrp";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "MrpGrp");
                ddlMRPGroup.DataTextField = ds.Tables["MrpGrp"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlMRPGroup.DataValueField = ds.Tables["MrpGrp"].Columns["MrpGrpcode"].ToString();             // to retrive specific  textfield name 
                ddlMRPGroup.DataSource = ds.Tables["MrpGrp"];      //assigning datasource to the dropdownlist
                ddlMRPGroup.DataBind();  //binding dropdownlist
                ddlMRPGroup.Items.Insert(0, new ListItem("------Select------", "0"));


                ddlBasicDataPackagingMaterialCateguory.DataTextField = ds.Tables["PackagingMaterialCateguory"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlBasicDataPackagingMaterialCateguory.DataValueField = ds.Tables["PackagingMaterialCateguory"].Columns["PackagingMaterialCateguorycode"].ToString();             // to retrive specific  textfield name 
                ddlBasicDataPackagingMaterialCateguory.DataSource = ds.Tables["PackagingMaterialCateguory"];      //assigning datasource to the dropdownlist
                ddlBasicDataPackagingMaterialCateguory.DataBind();  //binding dropdownlist
                ddlBasicDataPackagingMaterialCateguory.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindMrpGroup" + ex.ToString();
            }
        }

        private void BindMrpGroupMtype()
        {
            try
            {
                cmd.CommandText = "select MrpGrpcode, MrpGrpcode + ' '+ Description as Description from tblMrpGrp where MaterialTypecode = '" + ddlMaterialType.SelectedValue + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "BindMrpGroupMtype");
                ddlMRPGroup.DataTextField = ds.Tables["BindMrpGroupMtype"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlMRPGroup.DataValueField = ds.Tables["BindMrpGroupMtype"].Columns["MrpGrpcode"].ToString();             // to retrive specific  textfield name 
                ddlMRPGroup.DataSource = ds.Tables["BindMrpGroupMtype"];      //assigning datasource to the dropdownlist
                ddlMRPGroup.DataBind();  //binding dropdownlist
                ddlMRPGroup.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindMrpGroupMtype" + ex.ToString();
            }
        }

        private void BindMRPControllerMtype()
        {
            try
            {
                cmd.CommandText = "SELECT mrpControllercode ,mrpControllercode+ ' ' + Description as Description FROM [dbo].tblmrpController   where MaterialTypecode  = '" + ddlMaterialType.SelectedValue + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "BindMRPControllerMtype");
                ddlMRPController.DataTextField = ds.Tables["BindMRPControllerMtype"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlMRPController.DataValueField = ds.Tables["BindMRPControllerMtype"].Columns["mrpControllercode"].ToString();             // to retrive specific  textfield name 
                ddlMRPController.DataSource = ds.Tables["BindMRPControllerMtype"];      //assigning datasource to the dropdownlist
                ddlMRPController.DataBind();  //binding dropdownlist
                ddlMRPController.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindMRPControllerMtype" + ex.ToString();
            }
        }

        private void BindBackFlush()
        {
            try
            {
                cmd.CommandText = "SP_BackFlush";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "BackFlush");
                ddlBackFlush.DataTextField = ds.Tables["BackFlush"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlBackFlush.DataValueField = ds.Tables["BackFlush"].Columns["BackFlushcode"].ToString();             // to retrive specific  textfield name 
                ddlBackFlush.DataSource = ds.Tables["BackFlush"];      //assigning datasource to the dropdownlist
                ddlBackFlush.DataBind();  //binding dropdownlist
                ddlBackFlush.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindBackFlush" + ex.ToString();
            }
        }

        private void BindPackagingMaterialType()
        {
            try
            {
                cmd.CommandText = "SP_PackagingMaterialType";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "PackagingMaterialType");
                ddlPackagingMaterialType.DataTextField = ds.Tables["PackagingMaterialType"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlPackagingMaterialType.DataValueField = ds.Tables["PackagingMaterialType"].Columns["PackagingMaterialTypecode"].ToString();             // to retrive specific  textfield name 
                ddlPackagingMaterialType.DataSource = ds.Tables["PackagingMaterialType"];      //assigning datasource to the dropdownlist
                ddlPackagingMaterialType.DataBind();  //binding dropdownlist
                ddlPackagingMaterialType.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindPackagingMaterialType" + ex.ToString();
            }
        }

        private void BindValuationClass()
        {
            try
            {
                ddlValuationClass.Items.Clear();
                ds = obj.BindValuationClassMtype(ddlMaterialType.SelectedValue.ToString());
                ddlValuationClass.DataTextField = ds.Tables["BindValuationClassMtype"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlValuationClass.DataValueField = ds.Tables["BindValuationClassMtype"].Columns["ValuationClasscode"].ToString();             // to retrive specific  textfield name 
                ddlValuationClass.DataSource = ds.Tables["BindValuationClassMtype"];      //assigning datasource to the dropdownlist
                ddlValuationClass.DataBind();  //binding dropdownlist
                ddlValuationClass.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindValuationClass" + ex.ToString();
            }
        }

        private void BindProdnsupervisor()
        {
            try
            {
                ds = obj.BindProdnsupervisor();
                ddlProdsupervisor.DataTextField = ds.Tables["Prodnsupervisor"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlProdsupervisor.DataValueField = ds.Tables["Prodnsupervisor"].Columns["prodnsupervisorcode"].ToString();             // to retrive specific  textfield name 
                ddlProdsupervisor.DataSource = ds.Tables["Prodnsupervisor"];      //assigning datasource to the dropdownlist
                ddlProdsupervisor.DataBind();  //binding dropdownlist
                ddlProdsupervisor.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindProdnsupervisor" + ex.ToString();
            }
        }

        private void BindProdSchedProfile()
        {
            try
            {
                ds = obj.BindProdSchedProfile();
                ddlProdScheduleProfile.DataTextField = ds.Tables["ProdSchedProfile"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlProdScheduleProfile.DataValueField = ds.Tables["ProdSchedProfile"].Columns["ProdSchedProfilecode"].ToString();             // to retrive specific  textfield name 
                ddlProdScheduleProfile.DataSource = ds.Tables["ProdSchedProfile"];      //assigning datasource to the dropdownlist
                ddlProdScheduleProfile.DataBind();  //binding dropdownlist
                ddlProdScheduleProfile.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindProdSchedProfile" + ex.ToString();
            }
        }

        private void BindTasklistusage()
        {
            try
            {
                ds = obj.BindTasklistusage();
                ddlTaskListUsage.DataTextField = ds.Tables["Tasklistusage"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlTaskListUsage.DataValueField = ds.Tables["Tasklistusage"].Columns["tasklistusagecode"].ToString();             // to retrive specific  textfield name 
                ddlTaskListUsage.DataSource = ds.Tables["Tasklistusage"];      //assigning datasource to the dropdownlist
                ddlTaskListUsage.DataBind();  //binding dropdownlist
                ddlTaskListUsage.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                lblError.Text = "BindTasklistusage" + ex.ToString();
            }
        }

        protected void ddlMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMaterialType.SelectedValue != "0")
                {
                    BD.Visible = true;
                    txtRemarksReview.Visible = false;
                    divEmail.Visible = true;
                    BindPlantMtype();
                    BindBaseUnitOfMeasureMTYPR();
                    BindMaterialgroup();
                    BindMRPTypeMTYPE();

                }
                else if (ddlMaterialType.SelectedValue == "0")
                {
                    BD.Visible = false;
                    txtRemarksReview.Visible = false;
                    divEmail.Visible = false;
                }

            }
            catch (SqlException ex)
            {
                lblError.Text = "ddlMaterialType_SelectedIndexChanged" + ex.ToString();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }



        protected void bindSLfromPlant()
        {
            try
            {
                string Plant = "";
                string strQuery = "";
                ddlStorageLocation.Items.Clear();
                for (int i = 0; i <= ddlPlant.Items.Count - 1; i++)
                {
                    if (ddlPlant.Items[i].Selected)
                    {
                        if (Plant == "") { Plant = ddlPlant.Items[i].Value; }
                        else { Plant += "," + ddlPlant.Items[i].Value; }
                    }

                }
                ddlStorageLocation.SelectedIndex = -1;
                strQuery = @"SELECT StorageLocationcode ,StorageLocationcode +''+Description As Description from TBLSTORAGELOCATION WHERE (ISNULL(@Plant,'')='' OR ',' + @Plant + ',' LIKE '%,' + CAST(PlantCode AS varchar) + ',%')";
                using (SqlCommand cmd = new SqlCommand())
                {
                    ds.Clear();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Plant", Plant.ToString());
                    cmd.CommandText = strQuery;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    conn.Open();
                    ds.Clear();
                    adp.Fill(ds, "SL");

                    ddlStorageLocation.DataTextField = ds.Tables["SL"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                    ddlStorageLocation.DataValueField = ds.Tables["SL"].Columns["StorageLocationcode"].ToString();             // to retrive specific  textfield name 
                    ddlStorageLocation.DataSource = ds.Tables["SL"];      //assigning datasource to the dropdownlist
                    ddlStorageLocation.DataBind();  //binding dropdownlist
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "bindSLfromPlant" + ex.ToString();
            }
        }

        protected void MG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bindMSGfromMG();
            }
            catch (SqlException ex)
            {
                lblError.Text = "MG_SelectedIndexChanged" + ex.ToString();
            }
        }

        protected void bindMSGfromMG()
        {
            try
            {
                // string value1 = MG.SelectedValue;
                string value1 = ddlMG.SelectedValue;
                ViewState["q"] = value1.ToString().Trim();
                String strQuery = "SELECT [MaterialSubGroupcode],[MaterialGroupcode],[MaterialSubGroupcode]+ ' ' + Description as Description FROM [dbo].[tblMaterialSubGroup]  where MaterialGroupcode = '" + value1.ToString().Trim() + "'";


                ds.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();
                adp.SelectCommand = cmd;
                ds.Clear();
                adp.Fill(ds, "MaterialSubGroup");
                ddlMSG.DataTextField = ds.Tables["MaterialSubGroup"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlMSG.DataValueField = ds.Tables["MaterialSubGroup"].Columns["MaterialSubGroupcode"].ToString();             // to retrive specific  textfield name 
                ddlMSG.DataSource = ds.Tables["MaterialSubGroup"];      //assigning datasource to the dropdownlist
                ddlMSG.DataBind();  //binding dropdownlist
                ddlMSG.Items.Insert(0, new ListItem("------Select------", "0"));
                conn.Close();
            }
            catch (SqlException ex)
            {
                lblError.Text = "bindMSGfromMG" + ex.ToString();
            }
        }

        protected void refreshpage()
        {
            try
            {
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "myscript", "setTimeout(function(){location.href='MeterialMaster.aspx';},15000);", true);
                ClearInputs(Page.Controls);
            }
            catch (SqlException ex)
            {
                lblError.Text = "refreshpage" + ex.ToString();
            }
        }

        void ClearInputss(ControlCollection ctrlss)
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
                    ClearInputss(ctrlsss.Controls);
                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "ClearInputss" + ex.ToString();
            }
        }

        void ClearInputs(ControlCollection ctrls)
        {
            try
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl is TextBox)
                        ((TextBox)ctrl).Text = string.Empty;

                    if (ctrl is DropDownList)
                        ((DropDownList)ctrl).SelectedIndex = -1;
                    if (ctrl is ListBox)
                        ((ListBox)ctrl).SelectedIndex = -1;
                    ClearInputs(ctrl.Controls);
                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "ClearInputs" + ex.ToString();
            }

        }

        protected void ddlProdCatg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                String strQuery = "SELECT Distinct  [H2ID],[H2ID]+ ' ' + H2Desc as H2Desc  FROM [dbo].[TBL_ProductHierarchy] where H1ID = @H1ID";
                using (SqlCommand cmd = new SqlCommand())
                {
                    ds.Clear();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@H1ID", ddlProdCatg.SelectedValue.ToString());
                    cmd.CommandText = strQuery;
                    cmd.Connection = conn;
                    conn.Open();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "ProductHierarchy");
                    ddlProdCatgsub1.DataTextField = ds.Tables["ProductHierarchy"].Columns["H2Desc"].ToString(); // text field name of table dispalyed in dropdown
                    ddlProdCatgsub1.DataValueField = ds.Tables["ProductHierarchy"].Columns["H2ID"].ToString();             // to retrive specific  textfield name 
                    ddlProdCatgsub1.DataSource = ds.Tables["ProductHierarchy"];      //assigning datasource to the dropdownlist
                    ddlProdCatgsub1.DataBind();  //binding dropdownlist
                    ddlProdCatgsub1.Items.Insert(0, new ListItem("------Select------", "0"));
                    conn.Close();
                    ddlProdCatgsub1.SelectedIndex = -1;
                    ddlProdCatgsub2.SelectedIndex = -1;


                    if (((string)ViewState["HID"]) == "2")
                    {
                        for (int i = 0; i < ddlPlant.Items.Count; i++)
                        {
                            ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                        }
                        for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                        {
                            ddlStorageLocation.Items[i].Attributes.Add("disabled", "disabled");
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "ddlProdCatg_SelectedIndexChanged" + ex.ToString();
            }

        }

        protected void ddlProdCatgsub1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String strQuery = "SELECT Distinct  [H3ID] ,[H3ID]+ ' ' + H3Desc as H3Desc  FROM [dbo].[TBL_ProductHierarchy] where H1ID = @H1ID and H2ID =@H2ID";
                using (SqlCommand cmd = new SqlCommand())
                {
                    ds.Clear();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@H1ID", ddlProdCatg.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@H2ID", ddlProdCatgsub1.SelectedValue.ToString());
                    cmd.CommandText = strQuery;
                    cmd.Connection = conn;
                    adp.SelectCommand = cmd;
                    ds.Clear();
                    adp.Fill(ds, "ProductHierarchy");
                    ddlProdCatgsub2.DataTextField = ds.Tables["ProductHierarchy"].Columns["H3Desc"].ToString(); // text field name of table dispalyed in dropdown
                    ddlProdCatgsub2.DataValueField = ds.Tables["ProductHierarchy"].Columns["H3ID"].ToString();             // to retrive specific  textfield name 
                    ddlProdCatgsub2.DataSource = ds.Tables["ProductHierarchy"];      //assigning datasource to the dropdownlist
                    ddlProdCatgsub2.DataBind();  //binding dropdownlist
                    ddlProdCatgsub2.Items.Insert(0, new ListItem("------Select------", "0"));
                    conn.Close();
                    if (ds.Tables["ProductHierarchy"].Columns["H3ID"].ToString().Contains(""))
                    {
                        ddlProdCatgsub2.SelectedIndex = 1;
                    }

                    if (((string)ViewState["HID"]) == "2")
                    {
                        for (int i = 0; i < ddlPlant.Items.Count; i++)
                        {
                            ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                        }
                        for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                        {
                            ddlStorageLocation.Items[i].Attributes.Add("disabled", "disabled");
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "ddlProdCatgsub1_SelectedIndexChanged" + ex.ToString();
            }
        }

        protected void ddlProdCatgsub2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlValuationCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSplitValueationMTYP();
                for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                {
                    ddlStorageLocation.Items[i].Attributes.Add("disabled", "disabled");
                }
                Page.MaintainScrollPositionOnPostBack = true;
            }
            catch (SqlException ex)
            {
                lblError.Text = "ddlValuationCategory_SelectedIndexChanged" + ex.ToString();
            }
        }

        protected void ddlEmailMDA_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlMaterialType.SelectedValue == "0")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please Select any Material Type";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblUpError.Focus();
                    error.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                if (ddlPlant.SelectedValue == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please Select any Plant";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblUpError.Focus();
                    error.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                if (ddlStorageLocation.SelectedValue == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please Select any Storage Location";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblUpError.Focus();
                    error.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                if (txtDescription.Text == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please select any Description!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtDescription.BackColor = System.Drawing.Color.Red;
                    return;
                }
                if (ddlMMBaseUnitOfMeasure.SelectedValue == "0")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please select any BUOM!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlMMBaseUnitOfMeasure.BackColor = System.Drawing.Color.Red;
                    return;
                }
                if (ddlMG.SelectedValue == "0")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please Select any Material Group!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlMG.BackColor = System.Drawing.Color.Red;
                    return;
                }
                if (ddlMSG.SelectedValue == "0")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please Select any Material Sub Group!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlMSG.BackColor = System.Drawing.Color.Red;
                    return;
                }
                else if (ddlNotificationFI.SelectedValue == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please Select any Specific (Finance)";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (ddlNotificationMIS.SelectedValue == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please Select any Specific (IS)";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (ddlEmailMDA.SelectedValue == "0")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please Select any MDA";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (ddlMarketingHOD.SelectedValue == "0")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Taxes H.O.D Should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                //else if (txtRemarksReview.Text == "")
                //{

                //    lblmessage.Text = "";
                //    lblUpError.Text = "Remarks should not be left blank!";
                //    sucess.Visible = false;
                //    error.Visible = true;
                //    lblmessage.Focus();
                //    sucess.Focus();
                //    Page.MaintainScrollPositionOnPostBack = false;
                //    txtRemarksReview.BackColor = System.Drawing.Color.Red;
                //    return;
                //}
                else
                {
                    Save();
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "btnSave_Click" + ex.ToString();
            }
        }

        protected void Save()
        {
            try
            {
                string aaa = ddlMaterialType.SelectedItem.Value;
                string FormID = "101";
                string MMCreatedBy = Session["User_Name"].ToString();
                string Plant = "";
                string ExToPlant = "";
                string EmailReviwer = "";
                string Result = "";
                string Notification = "";
                string NotificationFI = "";
                string EmailMDA = "";
                string StorageLocation = "";
                string ValuationType = "";

                for (int i = 0; i <= ddlPlant.Items.Count - 1; i++)
                {
                    if (ddlPlant.Items[i].Selected)
                    {
                        if (Plant == "") { Plant = ddlPlant.Items[i].Value; }
                        else { Plant += "," + ddlPlant.Items[i].Value; }
                    }

                }
                for (int i = 0; i <= ddlExtOtherPlant.Items.Count - 1; i++)
                {
                    if (ddlExtOtherPlant.Items[i].Selected)
                    {
                        if (ExToPlant == "") { ExToPlant = ddlExtOtherPlant.Items[i].Value; }
                        else { ExToPlant += "," + ddlExtOtherPlant.Items[i].Value; }
                    }
                }


                for (int i = 0; i <= ddlEmailReviwer.Items.Count - 1; i++)
                {
                    if (ddlEmailReviwer.Items[i].Selected)
                    {
                        if (EmailReviwer == "") { EmailReviwer = ddlEmailReviwer.Items[i].Value; }
                        else { EmailReviwer += "," + ddlEmailReviwer.Items[i].Value.Trim(); }
                    }
                }
                for (int i = 0; i <= ddlNotificationMIS.Items.Count - 1; i++)
                {
                    if (ddlNotificationMIS.Items[i].Selected)
                    {
                        if (Notification == "") { Notification = ddlNotificationMIS.Items[i].Value; }
                        else { Notification += "," + ddlNotificationMIS.Items[i].Value.Trim(); }
                    }
                }
                for (int i = 0; i <= ddlNotificationFI.Items.Count - 1; i++)
                {
                    if (ddlNotificationFI.Items[i].Selected)
                    {
                        if (NotificationFI == "") { NotificationFI = ddlNotificationFI.Items[i].Value; }
                        else { NotificationFI += "," + ddlNotificationFI.Items[i].Value.Trim(); }
                    }
                }
                for (int i = 0; i <= ddlEmailMDA.Items.Count - 1; i++)
                {
                    if (ddlEmailMDA.Items[i].Selected)
                    {
                        if (EmailMDA == "") { EmailMDA = ddlEmailMDA.Items[i].Value.Trim(); }
                        else { EmailMDA += "," + ddlEmailMDA.Items[i].Value.Trim(); }
                    }
                }
                for (int i = 0; i <= ddlStorageLocation.Items.Count - 1; i++)
                {
                    if (ddlStorageLocation.Items[i].Selected)
                    {
                        if (StorageLocation == "") { StorageLocation = ddlStorageLocation.Items[i].Value.Trim(); }
                        else { StorageLocation += ',' + ddlStorageLocation.Items[i].Value.Trim(); }
                    }
                    StorageLocation = StorageLocation.Trim();
                }



                Result = ddlMerchandiser.SelectedValue.ToString() + "," + ddlTaxes.SelectedValue.ToString() + "," + ddlMHOD.SelectedValue.ToString() + "," + ddlMarketingHOD.SelectedValue.ToString() + "," + ddlNotificationFI.SelectedValue.ToString() + "," + ddlNotificationMIS.SelectedValue.ToString();
                cmd.CommandText = "";
                cmd.CommandText = "SP_SYS_MaterialMasterMTYPE";

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                cmd.Parameters.AddWithValue("@MaterialType", ddlMaterialType.SelectedValue);
                cmd.Parameters.AddWithValue("@Plant", Plant.ToString());
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@BaseUnitofMeasure", ddlMMBaseUnitOfMeasure.SelectedValue);
                cmd.Parameters.AddWithValue("@MaterialGroup", ddlMG.SelectedValue);
                cmd.Parameters.AddWithValue("@MaterialSubGroup", ddlMSG.SelectedValue);
                cmd.Parameters.AddWithValue("@GrossWeight ", txtGROSSWEIGHT.Text);
                cmd.Parameters.AddWithValue("@NetWeight ", txtNETWEIGHT.Text);
                cmd.Parameters.AddWithValue("@WeightUni ", ddlWeightunitBD.SelectedValue);
                cmd.Parameters.AddWithValue("@Volume ", txtVolume.Text);
                cmd.Parameters.AddWithValue("@VolumeUnit ", ddlVOLUMEUNIT.SelectedValue);
                cmd.Parameters.AddWithValue("@OldMaterailNo ", txtOldMaterialNumber.Text);
                cmd.Parameters.AddWithValue("@Size_Dimension", txtSizeDimensions.Text);
                cmd.Parameters.AddWithValue("@Packeging_Material_Catg", ddlBasicDataPackagingMaterialCateguory.SelectedValue);
                cmd.Parameters.AddWithValue("@Storage_Location", StorageLocation.ToString());
                cmd.Parameters.AddWithValue("@BatchManagmet", chkBatchManagement.SelectedValue);
                cmd.Parameters.AddWithValue("@APPROVAL", Result.ToString());
                cmd.Parameters.AddWithValue("@MDA", EmailMDA.ToString());
                cmd.Parameters.AddWithValue("@ClosedBox", RadioButtonList2.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CreatedBy", Session["User_Name"].ToString());
                cmd.Parameters.AddWithValue("@Remarks", txtRemarksReview.Text.ToString());
                cmd.Parameters.AddWithValue("@Status", FormType.ToString());
                cmd.Parameters.AddWithValue("@CustomerNo", txtCustomerNo.Text.ToString());
                cmd.Connection = conn;
                ds.Clear();
                adp.SelectCommand = cmd;
                adp.Fill(ds, "Message");
                sucess.Visible = true;

                string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
                lblmessage.Text = message + " # " + lblMaxTransactionID.Text;
                DummyGrid();
                EmailWorkSendFirstApproval();
                lblmessage.Focus();
                error.Visible = false;
                lblmessage.Focus();
                Page.MaintainScrollPositionOnPostBack = false;
                refreshpage();
                BD.Visible = false;
                CF.Visible = false;
                Prod.Visible = false;
                Account.Visible = false;
                Pack.Visible = false;
                Purch.Visible = false;
                SD.Visible = false;
                QM.Visible = false;
                MRP.Visible = false;
                divEmail.Visible = false;
                ddlSearchMC.SelectedIndex = -1;
                GetTransactionID();
            }
            catch (SqlException ex)
            {
                lblError.Text = "Save" + ex.ToString();
            }
        }

        protected void btnSaveSubmit_Click(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = false;
            txtSMC.BackColor = System.Drawing.Color.White;
            lblError.Text = "";
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
                //    return;
                //}

                if (chkLock.Checked == false)
                {
                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "You must lock the material first.";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblUpError.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                    txtRemarksReview.Enabled = true;
                    return;
                }

                if (txtSMC.Text == "" || txtSMC.Text == "0")
                {
                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "SAP Material Code should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtSMC.BackColor = System.Drawing.Color.Red;
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                    txtRemarksReview.Enabled = true;
                    return;
                }


                cmd.CommandText = @"Select SAPMaterialCode from tbl_SYS_MaterialMaster where SAPMaterialCode = '" + txtSMC.Text.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                adp.SelectCommand = cmd;
                dt.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "SAP material code " + txtSMC.Text + " already exist!. Please provide a specific code";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtSMC.BackColor = System.Drawing.Color.Red;
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    string MLock = "";
                    if (chkLock != null && chkLock.Checked)
                    {
                        MLock = "1";
                    }
                    else
                    {
                        MLock = "0";
                    }

                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "";
                    sucess.Visible = false;
                    error.Visible = false;
                    ds = obj.UpdateMaterial(lblMaxTransactionID.Text, txtSMC.Text.Trim(), MLock.ToString());
                    //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup2();", true);
                    //lblmessage.Text = "SAP Material Code " + txtSMC.Text.Trim() + " has been saved against  Form ID # " + Request.QueryString["TransactionNo"].ToString();

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
                        Page.MaintainScrollPositionOnPostBack = false;

                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "Email Send Failed!";
                        lblError.Visible = true;
                    }

                }
                if (((string)ViewState["HID"]) == "2")
                {
                    for (int i = 0; i < ddlPlant.Items.Count; i++)
                    {
                        ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                    }
                    for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                    {
                        ddlStorageLocation.Items[i].Attributes.Add("disabled", "disabled");
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "MDA" + ex.ToString();
            }

        }

        protected void btnApprover_Click(object sender, EventArgs e)
        {
            try
            {
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
                    lblEmail.Focus();
                }
            }

            catch (Exception ex)
            {
                lblError.Text = "btnApprover_Click" + ex.ToString();
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
                    if (((string)ViewState["HID"]) == "2")
                    {
                        if ((((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Senior Merchandiser") ||
                                   (((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Software Developer") ||
                                 (((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Team Lead") ||
                                   (((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Deputy Manager Production") ||
                                   (((string)ViewState["Department"]) == "Merchandising") && (((string)ViewState["Designation"]) == "Assistant Manager"))
                        {
                            if (rbNewWeightCheck.SelectedValue == "Right")
                            {
                                lblmessage.Text = "";
                                lblUpError.Text = "Net Weight Check must be Wrong while Reject.";
                                sucess.Visible = false;
                                error.Visible = true;
                                lblmessage.Focus();
                                sucess.Focus();
                                Page.MaintainScrollPositionOnPostBack = false;
                                whenquerystringpass();
                                txtRemarksReview.Enabled = true;
                                return;
                            }
                            else
                            {
                                update_NetWeightCheck();
                                EmailWorkReject();
                                ClosedFormAfterReject();
                                //   ApplicationStatus();
                                BindsysApplicationStatus();
                                GetStatusHierachyCategoryControls();
                                Page.MaintainScrollPositionOnPostBack = true;
                                lblEmail.Focus();
                            }

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
            }

            catch (Exception ex)
            {
                lblError.Text = "btnReject_Click" + ex.ToString();
            }
        }

        protected void btnReviewed_Click(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                BD.Visible = true;
                Prod.Visible = true;
                SD.Visible = true;
                QM.Visible = true;
                MRP.Visible = true;
                btnUpdate.Visible = true;
                btnEdit.Visible = false;
                DisableControls(Page, true);
                whenquerystringpass();
            }
            catch (Exception ex)
            {
                lblError.Text = "btnEdit_Click" + ex.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbNewWeightCheck.SelectedValue == "Wrong")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Net Weight Check must be Right while Update.";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    //whenquerystringpass();
                    return;
                }
                else if (ddlProdCatg.SelectedValue == "0")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Prod Catg should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlProdCatg.BackColor = System.Drawing.Color.Red;

                    return;
                }
                if (ddlProdCatgsub1.SelectedValue == "0")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Prod Catg sub1 should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlProdCatgsub1.BackColor = System.Drawing.Color.Red;
                    return;
                }
                if (ddlProdCatgsub2.SelectedValue == "0")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Prod Catg sub2 should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlProdCatgsub2.BackColor = System.Drawing.Color.Red;

                    return;
                }
                if (ddlSalesUnit.SelectedValue == "0")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Sales Unit should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlSalesUnit.BackColor = System.Drawing.Color.Red;

                    return;
                }
                if (ddlItemCateguoryGroup.SelectedValue == "0")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Item Categuory Group should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlItemCateguoryGroup.BackColor = System.Drawing.Color.Red;

                    return;
                }
                if (ddlProfitCenter.SelectedValue == "0")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Profit Center should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlProfitCenter.BackColor = System.Drawing.Color.Red;

                    return;
                }
                if (ddlProductionunit.SelectedValue == "0")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Production unit should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlProductionunit.BackColor = System.Drawing.Color.Red;

                    return;
                }

                if (ddlUnitOfIssue.SelectedValue == "0")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Unit Of Issue should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlUnitOfIssue.BackColor = System.Drawing.Color.Red;

                    return;
                }

                if (ddlMrpType.SelectedValue == "0")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Mrp Type should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlMrpType.BackColor = System.Drawing.Color.Red;

                    return;
                }

                if (ddlMRPGroup.SelectedValue == "0")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "MRP Group should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlMRPGroup.BackColor = System.Drawing.Color.Red;

                    return;
                }

                if (ddlAvailabilitycheck.SelectedValue == "0")
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Availability check should not be left blank!";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlAvailabilitycheck.BackColor = System.Drawing.Color.Red;

                    return;
                }
                if (ddlStorageLocation.SelectedValue == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please Select any Storage Location";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblUpError.Focus();
                    error.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
               
                else
                {
                    string upplant = "";
                    for (int i = 0; i <= ddlPlant.Items.Count - 1; i++)
                    {
                        if (ddlPlant.Items[i].Selected)
                        {
                            if (upplant == "") { upplant = ddlPlant.Items[i].Value; }
                            else { upplant += "," + ddlPlant.Items[i].Value; }
                        }

                    }

                    string UpStorage_Location = "";
                    for (int i = 0; i <= ddlStorageLocation.Items.Count - 1; i++)
                    {
                        if (ddlStorageLocation.Items[i].Selected)
                        {
                            if (UpStorage_Location == "") { UpStorage_Location = ddlStorageLocation.Items[i].Value; }
                            else { UpStorage_Location += "," + ddlStorageLocation.Items[i].Value; }
                        }

                    }
                    string a = ddlProdCatg.SelectedValue;
                    string b = ddlProdCatgsub1.SelectedValue;
                    string c = ddlProdCatgsub2.SelectedValue;
                    string Temp = a.Trim() + "," + b.Trim() + "," + c.Trim();
                    string ProductCatg = Temp.ToString();
                    string valuechkInspectionSetup = "";
                    string valuechkQmProcActive = "";




                    if (chkInspectionSetup != null && chkInspectionSetup.Checked)
                    {
                        valuechkInspectionSetup = "1";
                    }
                    else
                    {
                        valuechkInspectionSetup = "0";
                    }

                    if (chkQmProcActive != null && chkQmProcActive.Checked)
                    {
                        valuechkQmProcActive = "1";
                    }
                    else
                    {
                        valuechkQmProcActive = "0";
                    }

                    cmd.CommandText = @"UPDATE tbl_SYS_MaterialMaster_FG
      SET Plant = @UpPlant,
      Description = @UpDescription,
      BaseUnitofMeasure = @UpBaseUnitofMeasure,
      MaterialGroup = @UpMaterialGroup,
      MaterialSubGroup = @UpMaterialSubGroup,
      GrossWeight = @UpGrossWeight,
      NetWeight = @UpNetWeight,
      NetWeightCheck = @NetWeightCheck,
      WeightUni = @UpWeightUni,
      Volume = @UpVolume,
      VolumeUnit = @UpVolumeUnit,
      OldMaterailNo = @UpOldMaterailNo,
      CustomerNo  = @UpCustomerNo,
      Size_Dimension = @UpSize_Dimension,
      Packeging_Material_Catg = @UpPackeging_Material_Catg,
      BatchManagmet = @UpBatchManagmet,
      ProductHierarchy = @UpProductHierarchy,
      DistributionChannel = @UpDistributionChannel,
      SalesOrg = @UpSalesOrg,
      SalesUnit = @UpSalesUnit,
      Division = @UpDivision,
      TaxClasification = @UpTaxClasification,
      Item_Catg_Group = @UpItem_Catg_Group,
      LoomType = @UpLoomType,
      RoomReady = @UpRoomReady,
      SubDivision = @UpSubDivision,
      NOS = @UpNOS,
      Availabilitycheck = @UpAvailabilitycheck,
      TransportaionGroup = @UpTransportaionGroup,
      LoadingGroup = @UpLoadingGroup,
      ProfitCenter = @UpProfitCenter,
      SalesOrderTax = @UpSalesOrderTax,
      Material_Rebate_Rate = @UpMaterial_Rebate_Rate,
      Rebate_Catg = @UpRebate_Catg,
      MRPType = @UpMRPType,
      MRP_Group = @UpMRP_Group,
      ReoderPoint = @UpReoderPoint,
      MRPController = @UpMRPController,  
      Production_Unit_Of_Measure = @UpProduction_Unit_Of_Measure,
      UnitOfIssue = @UpUnitOfIssue,
      Prodsupervisor = @UpProdsupervisor,
      ProdScheduleProfile = @UpProdScheduleProfile,
      Storage_Location = @UpStorage_Location,
      Under_Delivery_Tollerance = @UpUnder_Delivery_Tollerance,
      Ove_Delivery_Tollerance = @UpOve_Delivery_Tollerance,
      TaskListUsage = @UpTaskListUsage,
      QMControlKey = @UpQMControlKey,
      InspectionSetup = @UpInspectionSetup,
      QMprocactive = @UpQMprocactive, 
      MinimumLotSize = @UpMinimumLotSize,
      MaximumLotSize = @UpMaximumLotSize,
      Maximumstocklevel = @UpMaximumstocklevel,
      SchedMarginkey = @UpSchedMarginkey,
      PeriodIndicator = @UpPeriodIndicator,
      Strategygroup = @UpStrategygroup,
      Lotsize = @UpLotsize  
      where TransactionID = '" + lblMaxTransactionID.Text + "' ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@UpPlant", upplant.ToString());
                    cmd.Parameters.AddWithValue("@UpDescription", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@UpBaseUnitofMeasure", ddlMMBaseUnitOfMeasure.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpMaterialGroup", ddlMG.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpMaterialSubGroup", ddlMSG.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpGrossWeight", txtGROSSWEIGHT.Text);
                    cmd.Parameters.AddWithValue("@UpNetWeight", txtNETWEIGHT.Text);
                    cmd.Parameters.AddWithValue("@UpWeightUni", ddlWeightunitBD.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpVolume", txtVolume.Text);
                    cmd.Parameters.AddWithValue("@UpVolumeUnit", ddlVOLUMEUNIT.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpOldMaterailNo", txtOldMaterialNumber.Text);
                    cmd.Parameters.AddWithValue("@UpCustomerNo", txtCustomerNo.Text);
                    cmd.Parameters.AddWithValue("@UpSize_Dimension", txtSizeDimensions.Text);
                    cmd.Parameters.AddWithValue("@UpPackeging_Material_Catg", ddlBasicDataPackagingMaterialCateguory.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpBatchManagmet", chkBatchManagement.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpProductHierarchy", ProductCatg.ToString());
                    cmd.Parameters.AddWithValue("@UpDistributionChannel", ddlDistributionChannel.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpSalesOrg", ddlSalesOrg.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpSalesUnit", ddlSalesUnit.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpDivision", ddlDivision.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpTaxClasification", ddlTaxClassification.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpItem_Catg_Group", ddlItemCateguoryGroup.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpLoomType", ddlLoomType.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpRoomReady", ddlRoomReady.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpSubDivision", ddlSubDivision.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpNOS", ddlNOS.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpAvailabilitycheck", ddlAvailabilitycheck.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpTransportaionGroup", ddlTransportionGroup.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpLoadingGroup", ddlLoadingGroup.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpProfitCenter", ddlProfitCenter.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpSalesOrderTax", txtSalesodertext.Text);
                    cmd.Parameters.AddWithValue("@UpMaterial_Rebate_Rate", ddlRate.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpRebate_Catg", ddlRebatecategoryRate.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpMRPType", ddlMrpType.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpMRP_Group", ddlMRPGroup.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpReoderPoint", txtReoderPoint.Text);
                    cmd.Parameters.AddWithValue("@UpMRPController", ddlMRPController.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpProduction_Unit_Of_Measure", ddlProductionunit.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpUnitOfIssue", ddlUnitOfIssue.Text);
                    cmd.Parameters.AddWithValue("@UpProdsupervisor", ddlProdsupervisor.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpProdScheduleProfile", ddlProdScheduleProfile.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpStorage_Location", UpStorage_Location.ToString());
                    cmd.Parameters.AddWithValue("@UpUnder_Delivery_Tollerance", txtUnderDeliveryTollerance.Text);
                    cmd.Parameters.AddWithValue("@UpOve_Delivery_Tollerance", txtOverDeliveryTollerance.Text);
                    cmd.Parameters.AddWithValue("@UpTaskListUsage", ddlTaskListUsage.Text);
                    cmd.Parameters.AddWithValue("@UpQMControlKey", ddlQMControlKey.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpInspectionSetup", valuechkInspectionSetup.ToString());
                    cmd.Parameters.AddWithValue("@UpQMprocactive", valuechkQmProcActive.ToString());
                    cmd.Parameters.AddWithValue("@UpReorderPoint", txtReoderPoint.Text);
                    cmd.Parameters.AddWithValue("@UpMinimumLotSize", txtMinimumLotSize.Text);
                    cmd.Parameters.AddWithValue("@UpMaximumLotSize", txtMaximumLotSize.Text);
                    cmd.Parameters.AddWithValue("@UpMaximumstocklevel", txtMaximumstocklevel.Text);
                    cmd.Parameters.AddWithValue("@UpSchedMarginkey", TxtSchedMarginkey.Text);
                    cmd.Parameters.AddWithValue("@UpPeriodIndicator", ddlPeriodIndicator.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpStrategygroup", ddlStrategygroup.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@UpLotsize", ddlLotsize.SelectedValue);
                    cmd.Parameters.AddWithValue("@NetWeightCheck", rbNewWeightCheck.SelectedValue);


                    conn.Open();

                    int aa = cmd.ExecuteNonQuery();
                    if (aa > 0)
                    {
                        lblmessage.Text = "Record updated sucessfully!";
                        lblmessage.Focus();
                        sucess.Visible = true;
                        error.Visible = false;
                        Page.MaintainScrollPositionOnPostBack = false;
                        btnEdit.Visible = false;
                        DisableControls(Page, false);
                        this.ddlPlant.Attributes.Add("disabled", "");
                        this.ddlValuationType.Attributes.Add("disabled", "");
                        this.ddlStorageLocation.Attributes.Add("disabled", "");
                        txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                        txtRemarksReview.Visible = true;
                        //string url = HttpContext.Current.Request.Url.ToString();
                        //Response.Redirect(url.ToString());
                        lblEmail.ForeColor = System.Drawing.Color.Blue;

                        btnReject.Visible = true;
                        btnUpdate.Visible = false;
                        txtRemarksReview.Visible = true;
                        txtRemarksReview.Enabled = true;
                        btnApprover.Visible = true;
                        btnEdit.Visible = false;
                        btnForward.Visible = true;
                        btnTransfer.Visible = true;


                        whenquerystringpass();
                        controlForwardHide();
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = "btnUpdate_Click" + ex.ToString();
            }
        }

        protected void btnTUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRate.SelectedValue == "0")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Material Rebate Rate should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlRate.BackColor = System.Drawing.Color.Red;
                    if (((string)ViewState["HID"]) == "2")
                    {
                        for (int i = 0; i < ddlPlant.Items.Count; i++)
                        {
                            ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                        }
                        for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                        {
                            ddlStorageLocation.Items[i].Attributes.Add("disabled", "disabled");
                        }
                    }
                    return;
                }

                else if (ddlRebatecategoryRate.SelectedValue == "0")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Rebate Category should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlRebatecategoryRate.BackColor = System.Drawing.Color.Red;
                    if (((string)ViewState["HID"]) == "2")
                    {
                        for (int i = 0; i < ddlPlant.Items.Count; i++)
                        {
                            ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                        }
                        for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                        {
                            ddlStorageLocation.Items[i].Attributes.Add("disabled", "disabled");
                        }
                    }
                    return;
                }

                cmd.CommandText = @"UPDATE tbl_SYS_MaterialMaster
                    SET  Material_Rebate_Rate = @UpMaterial_Rebate_Rate,
                    Rebate_Catg = @UpRebate_Catg
                    where TransactionID = '" + lblMaxTransactionID.Text + "' ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@UpMaterial_Rebate_Rate", ddlRate.SelectedValue);
                cmd.Parameters.AddWithValue("@UpRebate_Catg", ddlRebatecategoryRate.SelectedValue);
                conn.Open();

                int aa = cmd.ExecuteNonQuery();
                conn.Close();
                if (aa > 0)
                {
                    lblmessage.Text = "Record updated sucessfully!";
                    lblmessage.Focus();
                    sucess.Visible = true;
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                    btnTransfer.Visible = true;
                    btnReject.Visible = true;
                    btnApprover.Visible = true;
                    btnUpdate.Visible = false;
                    btnTUpdate.Visible = false;
                    btnEdit.Visible = false;
                    btnTransfer.Visible = true;
                    DisableControls(Page, false);
                    this.ddlPlant.Attributes.Add("disabled", "");
                    this.ddlValuationType.Attributes.Add("disabled", "");
                    this.ddlStorageLocation.Attributes.Add("disabled", "");
                    txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                    txtRemarksReview.Visible = true;
                    txtRemarksReview.Enabled = true;
                    //string url = HttpContext.Current.Request.Url.ToString();
                    //Response.Redirect(url.ToString());
                    lblEmail.ForeColor = System.Drawing.Color.Blue;

                    btnReject.Visible = true;
                    btnTUpdate.Visible = false;
                    btnUpdate.Visible = false;
                    txtRemarksReview.Enabled = true;
                    txtRemarksReview.Visible = true;
                    btnApprover.Visible = true;
                    btnEdit.Visible = false;
                    btnForward.Visible = true;
                    btnTransfer.Visible = true;
                    controlForwardHide();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "btnTUpdate_Click" + ex.ToString();
            }

        }

        protected void update_NetWeightCheck()
        {
            cmd.CommandText = @"SP_UpdateNetWeightCheck";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TransactionID", lblMaxTransactionID.Text);
            cmd.Parameters.AddWithValue("@NetWeightCheck", rbNewWeightCheck.SelectedValue);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        protected void btnFUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlValuationClass.SelectedValue == "0")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Select any valuation class";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlValuationClass.BackColor = System.Drawing.Color.Red;
                    return;
                }

                else if (txtStandardPrice.Text == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Standerd price should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtStandardPrice.BackColor = System.Drawing.Color.Red;
                    return;
                }
                else if (ddlValuationType.SelectedValue == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Valuation Type  should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlValuationType.BackColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {

                    string VTYPE = "";

                    for (int i = 0; i <= ddlValuationType.Items.Count - 1; i++)
                    {
                        if (ddlValuationType.Items[i].Selected)
                        {
                            if (VTYPE == "") { VTYPE = ddlValuationType.Items[i].Value; }
                            else { VTYPE += "," + ddlValuationType.Items[i].Value; }
                        }

                    }

                    cmd.CommandText = @"SP_SYS_UpdateFGMMFI";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@ValuationClass", ddlValuationClass.SelectedValue);
                    cmd.Parameters.AddWithValue("@ValuationCategory", ddlValuationCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@ValuationType", VTYPE.ToString());
                    cmd.Parameters.AddWithValue("@StandardPrice", txtStandardPrice.Text);
                    cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                    cmd.Parameters.AddWithValue("@TransactionID", lblMaxTransactionID.Text);
                    cmd.Parameters.AddWithValue("@Plant", ddlPlant.SelectedValue);
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
                        btnTUpdate.Visible = false;
                        btnUpdate.Visible = false;
                        txtRemarksReview.Visible = true;
                        txtRemarksReview.Enabled = true;
                        txtRemarks.Enabled = false;
                        btnApprover.Visible = true;
                        btnEdit.Visible = false;
                        btnForward.Visible = true;
                        btnTransfer.Visible = true;
                        DisableControls(Page, false);
                        this.ddlPlant.Attributes.Add("disabled", "");
                        this.ddlValuationType.Attributes.Add("disabled", "");
                        this.ddlStorageLocation.Attributes.Add("disabled", "");
                        txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                        txtRemarksReview.Visible = true;
                        txtRemarksReview.Enabled = true;
                        //string url = HttpContext.Current.Request.Url.ToString();
                        //Response.Redirect(url.ToString());
                        lblEmail.ForeColor = System.Drawing.Color.Blue;
                        txtStandardPrice.BackColor = System.Drawing.Color.AliceBlue;
                        controlForwardHide();
                    }

                }
                if (((string)ViewState["HID"]) == "2")
                {
                    for (int i = 0; i < ddlPlant.Items.Count; i++)
                    {
                        ddlPlant.Items[i].Attributes.Add("disabled", "disabled");
                    }
                    for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                    {
                        ddlStorageLocation.Items[i].Attributes.Add("disabled", "disabled");
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "btnFUpdate_Click" + ex.ToString();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

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
                    lblEmail.Text = "*New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been transferred to " + ddlTransferUser.SelectedItem.Text + "";
                    Session["HC"] = "06";
                    btnApprover.Enabled = false;
                    btnReject.Attributes.Add("disabled", "true");
                    btnEdit.Visible = false;
                    txtRemarksReview.Enabled = false;
                    txtRemarks.Enabled = true;

                    ddlTransferUser.Enabled = true;
                    btnUpdate.Visible = false;
                    ddlTransferUser.SelectedIndex = -1;
                    lblEmail.Focus();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "btnForward_Click" + ex.ToString();
            }
        }

        /////////////////////////////



        #region methodEmailWorks

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
                        EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has sent you a New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " for approval. <br><br> Your kind approval is required on the following URL: " +
                            "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                            "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                            "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
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
            catch (Exception ex)
            {
                lblError.Text = "EmailWorkSendFirstApproval" + ex.ToString();
            }

        }

        private void EmailWorkFirstHaracheyReviwer()
        {
            try
            {
                string HierachyCategory = "4";
                string HierachyCategoryStatus = "03";
                ds = obj.MailForwardFromReviwerToMDA(lblMaxTransactionID.Text, FormID.ToString(), HierachyCategory.ToString());

                if (ds.Tables["MailForwardToAllFromMDA"].Rows.Count > 0)
                {
                    DataTableReader reader = ds.Tables["MailForwardToAllFromMDA"].CreateDataReader();
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

                            mm.Subject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                            //,<br> <br>   I have Following request against " + " TransactionNo " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                            mm.Body = ViewState["UserName"].ToString(); //<a href= " + "" + url.ToString() + "?SMCode=" + txtSMC.Text + ">  " + url.ToString() + "?SMCode=" + txtSMC.Text + "</a>

                            string url = Request.Url.ToString();
                            mm.Body = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>  New material creation request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been reviewed by  Mr. " + ViewState["SessionUser"] + " <br> You may check the form on the following URL:<br> <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> <br><br>" +
                            "Material Master Application <br> Information Systems Dashboard";
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
                            //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup2();", true);
                            lblEmail.Text = "*New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been marked “reviewed” by you.";
                            //  btnReviewed.Enabled = false;
                            btnReviewed.Style["visibility"] = "hidden";
                            ViewState["Status"] = HierachyCategoryStatus.ToString();
                        }

                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblError.Text = "EmailWorkFirstHaracheyReviwer" + ex.ToString();
            }
        }

        private void EmailWorkFirstHaracheyMDA()
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
                        url = Request.Url.ToString();
                        TransactionID = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br><br>  SAP material code " + txtSMC.Text.Trim() + " has been issued against  new material creation request Form ID # " + lblMaxTransactionID.Text.ToString() + " <br><br> The form can be reviewed at the following URL:<br> <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>  This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Material Master Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();

                        lblmessage.Text = "SAP Material Code " + txtSMC.Text.Trim() + " has been saved against  Form ID # " + lblMaxTransactionID.Text;

                        lblmessage.ForeColor = System.Drawing.Color.Green;
                        conn.Close();
                        sucess.Visible = true;
                        error.Visible = false;
                        lblmessage.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        txtSMC.BackColor = System.Drawing.Color.White;
                        Page.MaintainScrollPositionOnPostBack = false;
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblError.Text = "EmailWorkFirstHaracheyMDA" + ex.ToString();
            }
        }

        private void EmailWorkApproved()
        {
            try
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
                        EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> A new material creation request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br> <br> You are kind approval is required for the information on the following URL: <br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Material Master Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();


                        lblEmail.Text = "*New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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
                            EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                            EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> A new material creation request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br><br> You are requested to create a material code information on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message.<br>" +
                                 "Material Master Application <br> Information Systems Dashboard";
                            SessionUser = Session["User_Name"].ToString();
                            DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            InsertEmail();
                            ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                            lblEmail.Text = "*New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
                            lblEmail.Focus();
                            Page.MaintainScrollPositionOnPostBack = false;
                            Page.MaintainScrollPositionOnPostBack = true;
                            lblEmail.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "EmailWorkApproved" + ex.ToString();
            }
        }

        private void EmailWorkFormForwarding()
        {
            try
            {
                string HierachyCategoryStatus = "06";
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
                        EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> A new material creation request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been forward by by " + ViewState["SessionUser"].ToString() + " <br> <br> You are kind approval is required for the information on the following URL: <br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Material Master Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();

                        lblEmail.Text = "*New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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
                            EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                            EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> A new material creation request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br><br> You are requested to create a material code information on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message.<br>" +
                                 "Material Master Application <br> Information Systems Dashboard";
                            SessionUser = Session["User_Name"].ToString();
                            DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            InsertEmail();
                            ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                            lblEmail.Text = "*New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
                            lblEmail.Focus();
                            Page.MaintainScrollPositionOnPostBack = false;
                            Page.MaintainScrollPositionOnPostBack = true;
                            lblEmail.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "EmailWorkFormForwarding" + ex.ToString();
            }
        }

        private void EmailWorkReject()
        {
            try
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
                        EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>  Your new material creation request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been disapproved by  " + ViewState["SessionUser"].ToString() + " <br><br> The reason of rejection is given below you can review your form on following url:<br><br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a>" +
                                "<br> <br> <br><b>Reject Remarks: " + txtRemarksReview.Text + "</b> " +
                              " <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br>" +
                            "<br>Material Master Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = "00"; // For Status Reject
                        lblEmail.Text = "*New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been rejected by you";
                        lblEmail.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        Page.MaintainScrollPositionOnPostBack = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "EmailWorkReject" + ex.ToString();
            }
        }

        protected void InsertEmail()
        {
            try
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
            catch (Exception ex)
            {
                lblError.Text = "InsertEmail" + ex.ToString();
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
            catch (Exception ex)
            {
                lblError.Text = "InsertEmailHOD" + ex.ToString();
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
                            cmdInsert.Parameters.AddWithValue("@TransferredTo", ddlTransferUser.SelectedValue.ToString());
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
            catch (Exception ex)
            {
                lblError.Text = "ClosedFormAfterReject" + ex.ToString();
            }
        }

        #endregion

        #region
        /// <summary>  /////////////////////////////////////////////////////////////////////
        /// 
        private void GetHarcheyNextData()
        {
            GetHarcheyID();
            DataTable HIDDataTable = (DataTable)ViewState["HIDDataSet"];
            ds = obj.GetHarachyNextData(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString(), ViewState["HID"].ToString());
            ViewState["GetHarachyNextDataDataSet"] = ds.Tables["GetHarachyNextData"];
            if (HIDDataTable.Rows.Count > 0)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
                {
                    using (SqlCommand cmdInsertEmail = new SqlCommand())//
                    {
                        int ResultSequance = 0;
                        int ResultSerialNo = 0;
                        int Value = Convert.ToInt32(HIDDataTable.Rows[0]["Sequance"]);
                        int _Temp = Convert.ToInt32(2);
                        ResultSequance = Value + _Temp;
                        int SerialNo = Convert.ToInt32(HIDDataTable.Rows[0]["SerialNo"]);
                        int _TempSerialNo = Convert.ToInt32(2);
                        ResultSerialNo = SerialNo + _TempSerialNo;

                        DateTime today = DateTime.Now;
                        cmdInsertEmail.Connection = connection;
                        cmdInsertEmail.CommandType = CommandType.Text;
                        cmdInsertEmail.CommandText = @"INSERT INTO sysWorkFlow
           (FormID,TransactionID,CreatedBy,HierachyCategory,RoughtingUserID,Sequance,DateTime,SerialNo)
     VALUES  ('" + FormID.ToString() + "','" + lblMaxTransactionID.Text.ToString() + "','" + HIDDataTable.Rows[0]["CreatedBy"] + "','" + HIDDataTable.Rows[0]["HierachyCategory"] + "','" + HIDDataTable.Rows[0]["RoughtingUserID"] + "','" + ResultSequance + "','" + today.ToString() + "','" + ResultSerialNo + "')";

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
            DataTable GetHarachyNextDataDataSet = (DataTable)ViewState["GetHarachyNextDataDataSet"];


            if (ds.Tables["GetHarachyNextData"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["GetHarachyNextData"].Rows.Count; i++)
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
                    {
                        using (SqlCommand cmdInsertEmail = new SqlCommand())
                        {
                            int value = (int)ds.Tables["GetHarachyNextData"].Rows[i]["Sequance"] + 2;
                            cmdInsertEmail.Connection = connection;
                            cmdInsertEmail.CommandType = CommandType.Text;
                            cmdInsertEmail.CommandText = @"update sysWorkFlow set Sequance = '" + value + "' where TransactionID = '" + lblMaxTransactionID.Text + "' and FormID = '" + FormID.ToString() + "' and HierachyCategory = '" + ViewState["HID"].ToString() + "'  and RoughtingUserID like '" + ds.Tables["GetHarachyNextData"].Rows[i]["RoughtingUserID"] + "%'  and Sequance = '" + ds.Tables["GetHarachyNextData"].Rows[i]["Sequance"] + "'  and SerialNo = '" + ds.Tables["GetHarachyNextData"].Rows[i]["SerialNo"] + "'";

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

        private void GetHarcheyDataAndInsertNextRow()
        {
            ds = obj.GetHarachyNextData(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString(), ViewState["HID"].ToString());
            if (ds.Tables["GetHarachyNextData"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["GetHarachyNextData"].Rows.Count - 1; i++)
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
                    {
                        using (SqlCommand cmdInsertEmail = new SqlCommand())//
                        {
                            int ResultSequance = 0;
                            int Value = Convert.ToInt32(ds.Tables["GetHarachyNextData"].Rows[i]["Sequance"]);

                            int _Temp = Convert.ToInt32(1);
                            // ds.Tables["GetHarachyNextData"].Rows[i]["RoughtingUserID"] + "%'
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
           ,DateTime)
     VALUES  ('" + FormID.ToString() + "','" + lblMaxTransactionID.Text.ToString() + "','" + ds.Tables["GetHarachyNextData"].Rows[i]["CreatedBy"].ToString() + "','" + ds.Tables["GetHarachyNextData"].Rows[i]["HierachyCategory"] + "','" + ds.Tables["GetHarachyNextData"].Rows[i]["RoughtingUserID"].ToString() + "','" + ResultSequance + "','" + today.ToString() + "')";

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
                    int _ResultSerialNo = 0;
                    int Value = Convert.ToInt32(ViewState["Sequance"]);
                    int _Temp = Convert.ToInt32(1);
                    ResultSequance = Value + _Temp;

                    int _ValueSerialNo = Convert.ToInt32(ViewState["SerialNo"]);
                    int TempSerialNo = Convert.ToInt32(1);
                    _ResultSerialNo = _ValueSerialNo + TempSerialNo;

                    DateTime today = DateTime.Now;
                    cmdInsertEmail.Connection = connection;
                    cmdInsertEmail.CommandType = CommandType.Text;
                    cmdInsertEmail.CommandText = @"INSERT INTO sysWorkFlow
           (FormID
           ,TransactionID
           ,CreatedBy
           ,HierachyCategory
           ,RoughtingUserID
           ,Sequance
           ,DateTime
            ,SerialNo)
     VALUES  ('" + FormID.ToString() + "','" + lblMaxTransactionID.Text.ToString() + "','" + ViewState["FormCreatedBy"].ToString() + "','" + ViewState["HID"] + "','" + ddlTransferUser.SelectedValue.ToString() + "','" + ResultSequance + "','" + today.ToString() + "','" + _ResultSerialNo + "')";

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

        protected void ddlValuationClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlMHOD.Items.Clear();
                ddlMerchandiser.Items.Clear();
                bindSLfromPlant();
                getUser();
                DataTable tblusermodulecategoryMerchandiser = (DataTable)ViewState["tblusermodulecategoryMerchandiser"];
                DataView dvDataMerchandiser = new DataView(tblusermodulecategoryMerchandiser);
                dvDataMerchandiser.RowFilter = "ModuleName like '%" + ddlPlant.SelectedValue.ToString() + "%' and Category = 'Merchandiser'";

                ddlMerchandiser.DataSource = dvDataMerchandiser;
                ddlMerchandiser.DataTextField = "DisplayName";
                ddlMerchandiser.DataValueField = "user_name";
                ddlMerchandiser.DataBind();
                ddlMerchandiser.Items.Insert(0, new ListItem("------Select------", "0"));

                //    DataView dvDataMerchandiserHOD = new DataView(tblusermodulecategoryMerchandiser);
                dvDataMerchandiser.RowFilter = "ModuleName like '%" + ddlPlant.SelectedValue.ToString() + "%' and Category = 'Merchandiser HOD'";

                ddlMHOD.DataSource = dvDataMerchandiser;
                ddlMHOD.DataTextField = "DisplayName";
                ddlMHOD.DataValueField = "user_name";
                ddlMHOD.DataBind();
                ddlMHOD.Items.Insert(0, new ListItem("------Select------", "0"));
                //ds.Tables.Add(dvData.ToTable("tblusermodulecategoryMerchandiser"));
                //ddlMerchandiser.DataTextField = ds.Tables["tblusermodulecategoryMerchandiser"].Columns["DisplayName"].ToString(); // text field name of table dispalyed in dropdown
                //ddlMerchandiser.DataValueField = ds.Tables["tblusermodulecategoryMerchandiser"].Columns["user_name"].ToString();             // to retrive specific  textfield name 
                //ddlMerchandiser.DataSource = ds.Tables["tblusermodulecategoryMerchandiser"];      //assigning datasource to the dropdownlist
                //ddlMerchandiser.DataBind();  //binding dropdownlist
                //ddlMerchandiser.Items.Insert(0, new ListItem("------Select------", "0"));

                if (ddlPlant.SelectedValue == "1000")
                {
                    ddlMG.SelectedValue = "0006";
                    bindMSGfromMG();
                    ddlMG.Enabled = false;
                }
                else if (ddlPlant.SelectedValue == "2000")
                {
                    ddlMG.SelectedValue = "0007";
                    bindMSGfromMG();
                    ddlMG.Enabled = false;
                }
                else if (ddlPlant.SelectedValue == "3000")
                {
                    ddlMG.SelectedValue = "0005";
                    bindMSGfromMG();
                    ddlMG.Enabled = false;
                }
                else if (ddlPlant.SelectedValue == "7000")
                {
                    ddlMG.Enabled = true;
                    bindMSGfromMG();
                }
            }
            catch (SqlException ex)
            {
                lblError.Text = "ddlPlant_SelectedIndexChanged" + ex.ToString();
            }
        }
        /// </summary>   /////////////////////////////////////////////////////////////////////
        #endregion






        ////////////////////



    }


}