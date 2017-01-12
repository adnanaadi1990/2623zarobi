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
    public partial class CreateMaterialMaster : System.Web.UI.Page
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
        public string FormID = "101";
        public string FormType = "N";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
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

        private void GetData()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
            Page.MaintainScrollPositionOnPostBack = true;
            ScriptManager ScriptManager1 = (ScriptManager)Page.Master.FindControl("ScriptManager1");
            ViewState["FormId"] = "101";
            ViewState["FormName"] = "Meterial Master";
            try
            {
                Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 60));
            }
            catch (Exception)
            {
                throw;
            }
            Page.MaintainScrollPositionOnPostBack = true;

            if (!IsPostBack)
            {
                dvemaillbl.Visible = true;
                Pack.Visible = false;
                txtSMC.BackColor = System.Drawing.Color.AliceBlue;
                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
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
                        lblSap.Visible = true;
                        txtSMC.Visible = true;
                        dvLock.Visible = true;
                        txtStandardPrice.Enabled = false;
                        BindPageLoad();
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        BD.Visible = true;
                        CF.Visible = true;
                        Prod.Visible = true;
                        Account.Visible = true;
                        Pack.Visible = true;
                        Purch.Visible = true;
                        SD.Visible = true;
                        QM.Visible = true;
                        MRP.Visible = true;
                        dvSMC.Visible = false;
                        btnSearch.Visible = false;
                        txtRemarksReview.Visible = true;
                        chkBatchManagement.Enabled = false;
                        ddlProdCatg.Visible = true;
                        ddlProdCatgsub1.Visible = true;
                        ddlProdCatgsub2.Visible = true;
                        txtMSG.Visible = false;
                        ddlMSG.Visible = true;
                        grdWStatus.Visible = true;
                        DisableControls(Page, false);
                        txtRemarks.Enabled = true;
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
                        getTransferUser();
                       

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
                        if (((string)ViewState["HID"]) == "1")
                        {
                            btnSaveSubmit.Visible = false;
                            btnSave.Visible = false;
                            txtRemarksReview.Visible = false;
                            btnApprover.Visible = false;
                            // btnReject.Visible = false;
                            btnTransfer.Visible = false;
                            txtSMC.Enabled = false;
                            lblSap.Visible = true;
                            txtSMC.Visible = true;
                            cbML.Enabled = false;
                        }

                        if (((string)ViewState["HID"]) == "2")
                        {
                            btnApprover.Visible = true;
                            Button1.Visible = true;
                            btnEdit.Visible = true;
                            txtRemarksReview.Enabled = true;
                            txtRemarks.Enabled = true;
                            btnTransfer.Visible = true;
                            ddlTransferUser.Enabled = true;
                            btnForward.Visible = true;
                            btnTransfer.Visible = true;
                        }
                        if (((string)ViewState["HID"]) == "4")
                        {
                            txtSMC.Enabled = true;
                            txtSMC.Visible = true;
                            lblSap.Visible = true;
                            btnSaveSubmit.Visible = true;
                            btnApprover.Visible = false;
                            Button1.Visible = true;
                            dvLock.Visible = true;
                            chkLock.Enabled = true;
                            btnTransfer.Visible = false;
                            txtRemarksReview.Enabled = true;
                        }
                        if (((string)ViewState["HID"]) == "3")
                        {
                            btnSave.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnSaveSubmit.Visible = false;
                            btnReviewed.Visible = true;
                            txtRemarksReview.Visible = false;
                            btnApprover.Visible = false;
                            Button1.Visible = false;
                            btnTransfer.Visible = false;
                            txtSMC.Enabled = false;
                            lblSap.Visible = true;
                            txtSMC.Visible = true;
                            cbML.Enabled = false;
                        }
                        controlForwardHide();

                    }
                    else
                    {
                        getUser();
                        getUserHOD();
                        DummyGrid();
                        getUserDetail();
                        GetTransactionID();
                        BindPageLoad();
                    }

                }
                catch (Exception ex)
                {
                    lblError.Text = ex.ToString();
                    dvemaillbl.Visible = true;
                }
            }

        }

        private int CheckRequiredFields(ref int p)
        {
            try
            {
                error.Visible = false;
                lblUpError.Text = "";
                Pack.Visible = false;
                BD.Visible = false;
                SD.Visible = false;
                CF.Visible = false;
                Prod.Visible = false;
                Account.Visible = false;
                Purch.Visible = false;
                MRP.Visible = false;
                QM.Visible = false;
                divEmail.Visible = false;

                FieldValidationCode FIELDV = new FieldValidationCode();
                //FOR GROUP PANELS POPULATE
                DataTable table1 = new DataTable();
                ds = FIELDV.GROUPVALIDATIONDATABASE(ddlMaterialType.SelectedValue.ToString());
                table1 = ds.Tables["sys_MType_MM_GroupValidation"];
                ContentPlaceHolder MainContent = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
                for (int i = 0; i < table1.Rows.Count; i++)
                {
                    string COLNAME = table1.Rows[i]["PanelName"].ToString();
                    HtmlControl Panel = (HtmlControl)MainContent.FindControl(COLNAME);
                    Panel.Visible = true;

                }
                //END FOR GROUP PANELS POPULATE
                ClearInputss(Page.Controls);
                //FOR REQUIRED FIELDS VALIDATION
                DataTable table = new DataTable();
                table.Clear();
                ds = FIELDV.FieldVALIDATIONDATABASE(ddlMaterialType.SelectedValue.ToString());
                table = ds.Tables["SYS_FIELDLISTINGRequired"];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string COLNAME = table.Rows[i]["DBID"].ToString();
                    if (COLNAME.StartsWith("txt"))
                    {
                        TextBox AgeTextBox = MainContent.FindControl(COLNAME) as TextBox;
                        AgeTextBox.BackColor = System.Drawing.Color.AliceBlue;
                    }
                    else if (COLNAME.StartsWith("ddl"))
                    {
                        DropDownList DD = MainContent.FindControl(COLNAME) as DropDownList;
                        DD.BackColor = System.Drawing.Color.AliceBlue;
                    }
                }
            }
            catch (Exception ex)
            {

                lblError.Text = ex.ToString();
                dvemaillbl.Visible = true;
            }
            return p = 1;
        }

        //-------------------------------------BUTTON EVENTS-------------------------------------------

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblmessage.Text = "";
                lblUpError.Text = "";
                sucess.Visible = false;
                error.Visible = false;
                ClearInputss(Page.Controls);

                cmd.CommandText = @"SP_SYS_FIELDLISTINGRequired";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@matTYPE", ddlMaterialType.SelectedValue.ToString());
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                ViewState["data"] = dt;

                int i = (dt.Rows.Count);

                DataTable table = new DataTable();
                table.Columns.Add("ComponentID", typeof(string));
                table.Columns.Add("ComponentValue", typeof(string));

                ContentPlaceHolder MainContent = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;

                for (int index = 0; index < i; index++)
                {
                    string CommID = dt.Rows[index]["DBID"].ToString();

                    if (CommID.ToString().StartsWith("txt"))
                    {
                        TextBox tx = MainContent.FindControl(CommID) as TextBox;
                        string txt = tx.Text.ToString();
                        table.Rows.Add(CommID, txt);
                    }

                    else if (CommID.ToString().StartsWith("ddl"))
                    {
                        DropDownList DD = MainContent.FindControl(CommID) as DropDownList;
                        string DDD = DD.SelectedValue.ToString();
                        table.Rows.Add(CommID, DDD);
                    }

                }

                //POPULATE DATA TABLE WITH REQUIRED FIELDS VALIDATION END WORKING


                //SHOWING FIELDS WITH PROMPT
                int Err = 0;
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    //int Err = 0;
                    string COMID = table.Rows[j]["ComponentID"].ToString();
                    string COMV = table.Rows[j]["ComponentValue"].ToString();
                    if (COMV == "" || COMV == "0")
                    {
                        if (COMID.StartsWith("txt"))
                        {
                            TextBox AgeTextBox = MainContent.FindControl(COMID) as TextBox;
                            AgeTextBox.BackColor = System.Drawing.Color.Red;
                            Err = 1;
                        }
                        else if (COMID.StartsWith("ddl"))
                        {
                            DropDownList DD = MainContent.FindControl(COMID) as DropDownList;
                            DD.BackColor = System.Drawing.Color.Red;
                            Err = 1;
                        }

                    }
                    if (Err == 1)
                    {
                        error.Visible = true;
                        Page.MaintainScrollPositionOnPostBack = false;
                        lblUpError.Focus();
                        lblUpError.Text = "Please fill All Required Fields";
                    }
                }
                //SHOWING FIELDS WITH PROMPT END
                //SAVING DATA
                if (Err == 0)
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
                    else if (lblHOD.Text == "")
                    {

                        lblmessage.Text = "";
                        lblUpError.Text = "H.O.D Should not be left blank!";
                        sucess.Visible = false;
                        error.Visible = true;
                        lblmessage.Focus();
                        sucess.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        return;
                    }
                    else if (txtRemarksReview.Text == "")
                    {

                        lblmessage.Text = "";
                        lblUpError.Text = "Remarks should not be left blank!";
                        sucess.Visible = false;
                        error.Visible = true;
                        lblmessage.Focus();
                        sucess.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        return;
                    }
                    else
                    {
                        Save();
                        lblmessage.Focus();
                        sucess.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                    }
                }
            }
            catch (Exception ex)
            {

                lblError.Text = ex.ToString();
                dvemaillbl.Visible = true;
            }
        }


        protected void btnSaveSubmit_Click(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = false;
            txtSMC.BackColor = System.Drawing.Color.White;
            lblError.Text = "";
            try
            {
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
                    return;
                }

                if (txtSMC.Text == "")
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
                    return;
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
                    try
                    {
                        ds = obj.UpdateMaterial(lblMaxTransactionID.Text, txtSMC.Text.Trim(), MLock.ToString());

                        string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                        if (message == "Record updated sucessfully!")
                        {
                            lblmessage.Visible = true;

                            EmailWorkFirstHaracheyMDA();
                            InsertEmailHOD();
                            ApplicationStatus();
                            BindsysApplicationStatus();
                            GetStatusHierachyCategoryControls();
                            sucess.Visible = true;
                            error.Visible = false;
                            Page.MaintainScrollPositionOnPostBack = false;
                        }
                        else
                        {
                            message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                            lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
                            lblmessage.Text = message + " # " + lblMaxTransactionID.Text;

                        }
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

        protected void btnReviewed_Click(object sender, EventArgs e)
        {
            try
            {

                EmailWorkFirstHaracheyReviwer();
                ApplicationStatus();
                BindsysApplicationStatus();
                GetStatusHierachyCategoryControls();
            }

            catch (Exception ex)
            {
                lblError.Text = "Rev" + ex.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                refreshpage();
                error.Visible = false;
                lblUpError.Text = "";
                Pack.Visible = false;
                BD.Visible = false;
                SD.Visible = false;
                CF.Visible = false;
                Prod.Visible = false;
                Account.Visible = false;
                Purch.Visible = false;
                MRP.Visible = false;
                QM.Visible = false;
                divEmail.Visible = false;
                dvSMC.Visible = true;
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        //-------------------------------------END BUTTON EVENTS-------------------------------------------

        //-------------------------------------DROPDOWN EVENTS-------------------------------------------

        protected void ddlAltBaseUnitEmpty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlUOMEmpty_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                }
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                }
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }

        }

        protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSLfromPlant();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        protected void ddlMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlMaterialType.SelectedValue == "0")
                {
                    error.Visible = false;
                    lblUpError.Text = "";
                    Pack.Visible = false;
                    BD.Visible = false;
                    SD.Visible = false;
                    CF.Visible = false;
                    Prod.Visible = false;
                    Account.Visible = false;
                    Purch.Visible = false;
                    MRP.Visible = false;
                    QM.Visible = false;
                    divEmail.Visible = false;
                    dvSMC.Visible = true;
                    ddlSearchMC.SelectedIndex = -1;
                    return;
                }
                dvSMC.Visible = false;
                sucess.Visible = false;
                error.Visible = false;

                int aa;
                int ab;
                ab = 0;
                aa = CheckRequiredFields(ref ab);    //Checking Required Mandatory Fields
                BindMaterialgroup();
                BindPlantMtype();
                BindPurchasingGroup();
                BindValuationClass();
                BindValuationCategoryMTYPE();
                BindBaseUnitOfMeasureMTYPR();
                BindMRPTypeMTYPE();
                BindMrpGroupMtype();
                BindMRPControllerMtype();
                txtRemarksReview.Visible = true;
                txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                if (aa != 0)
                {
                    //lblError.Text = "Please fill All Required Fields";
                    //    lblmessage.ForeColor = System.Drawing.Color.Red;
                    //lblmessage.Text = "Please fill All Required Fields";
                }
                // CheckRequiredFields();
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        protected void MG_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindMSGfromMG();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        //-------------------------------------END DROPDOWN EVENTS-------------------------------------------


        //-------------------------------------GRID-------------------------------------------

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var id = ((Label)GridView1.Rows[e.RowIndex].FindControl("Label1")).Text;

                DataTable dt = (DataTable)ViewState["ConvertionFacter"];
                DataColumn[] keyColumns = new DataColumn[1];
                keyColumns[0] = dt.Columns["sno"];
                dt.PrimaryKey = keyColumns;
                dt.Rows.Find(id).Delete();
                dt.AcceptChanges();
                ViewState["ConvertionFacter"] = dt;
                GridView1.DataSource = ViewState["ConvertionFacter"] as DataTable;
                GridView1.DataBind();
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        //-------------------------------------END GRID-------------------------------------------
        //-------------------------------------METHODS-------------------------------------------

        protected void getUser()
        {
            try
            {
                cmd.CommandText = "";
                cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserReviwer where FormName = 'MM'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();
                ddlNotificationMIS.DataSource = cmd.ExecuteReader();
                ddlNotificationMIS.DataTextField = "DisplayName";
                ddlNotificationMIS.DataValueField = "user_name";
                ddlNotificationMIS.DataBind();
                conn.Close();

                cmd.CommandText = "";
                cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserMDA where FormName = 'MM'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();
                ddlEmailMDA.DataSource = cmd.ExecuteReader();
                ddlEmailMDA.DataTextField = "DisplayName";
                ddlEmailMDA.DataValueField = "user_name";
                ddlEmailMDA.DataBind();
                conn.Close();

                ddlEmailMDA.Items.Insert(0, new ListItem("------Select------", "0"));
                ddlEmailReviwer.Items.Insert(0, new ListItem("------Select------", "0"));

                cmd.CommandText = " SELECT user_name,DisplayName FROM tbl_EmailToSpecificPerson where FormID = '101'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();
                ddlNotificationFI.DataSource = cmd.ExecuteReader();
                ddlNotificationFI.DataTextField = "DisplayName";
                ddlNotificationFI.DataValueField = "user_name";
                ddlNotificationFI.DataBind();
                conn.Close();

                for (int i = 0; i < ddlNotificationMIS.Items.Count - ddlNotificationMIS.Items.Count + 1; i++)
                {
                    ddlNotificationMIS.Items[i].Selected = true;
                }
                for (int i = 0; i < ddlNotificationFI.Items.Count - ddlNotificationFI.Items.Count + 1; i++)
                {
                    ddlNotificationFI.Items[i].Selected = true;
                }
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        protected void getTransferUser()
        {
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindPageLoad()
        {
            try
            {
                getTransferUser();
                //GetActiceDriectory();
                BindPlant();
                BindSAPMaterialCode();
                BindMaterialType();
                BindStorageLocation();
                // BindMaterialgroup();
                BindProductHierarchy();
                BindProductHierarchy2();
                BindProductHierarchy3();
                BindBaseUnitOfMeasure();
                //BindSplitValueation();
                BindProfitCenter();
                BindValuationCategory();
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                lblError.Text = "Ha ID Error" + ex.ToString();
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
                    Button1.Attributes.Add("disabled", "true");
                    btnApprover.Enabled = false;
                    btnReviewed.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSaveSubmit.Enabled = false;
                    btnEdit.Enabled = false;
                    btnTransfer.Attributes.Add("disabled", "true");
                    txtSMC.Attributes.Add("disabled", "true");
                    cbML.Enabled = false;
                    cbML.Visible = true;
                    chkLock.Visible = false;
                    Button1.Enabled = false;

                }
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                            ddlWeightunitBD.SelectedValue = reader["WeightUni"].ToString();
                            txtVolume.Text = reader["Volume"].ToString();
                            ddlVOLUMEUNIT.SelectedValue = reader["VolumeUnit"].ToString();
                            txtOldMaterialNumber.Text = reader["OldMaterailNo"].ToString();
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


                            BindPurchasingGroup();
                            ddlPurchasingGroup.SelectedValue = reader["Purchasing_Group"].ToString();

                            ddlOrderingUnit.SelectedValue = reader["OrderingUnit"].ToString();

                            txtPurchaseOrderText.Text = reader["PurchaseOrderText"].ToString();
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
                            dtcon.Clear();
                            cmd.CommandText = "";
                            cmd.CommandText = "EXEC SP_AltUnitOfMeasureGrid " + " @TransactionID  ='" + lblMaxTransactionID.Text + "'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = conn;
                            adp.SelectCommand = cmd;
                            adp.Fill(dtcon);
                            if (dtcon.Columns.Contains("sno"))
                            {
                                dtcon.Columns.Remove("sno");
                            }
                            DataColumn c = new DataColumn("sno", typeof(int));
                            c.AutoIncrement = true;
                            c.AutoIncrementSeed = 1;
                            c.AutoIncrementStep = 1;
                            dtcon.Columns.Add(c);
                            DataColumn t = new DataColumn("TransactionID", typeof(string));
                            dtcon.Columns.Add(t);
                            t.SetOrdinal(0);// to put the column in position 0;
                            GridView1.DataSource = dtcon;

                            for (int count = 0; count < dtcon.Rows.Count; count++)
                            {
                                dtcon.Rows[count]["sno"] = count + 1;
                                dtcon.Rows[count]["TransactionID"] = lblMaxTransactionID.Text;
                            }

                            ViewState["ConvertionFacter"] = dtcon;
                            GridView1.DataSource = (DataTable)ViewState["ConvertionFacter"];
                            GridView1.DataBind();
                            if (GridView1.Rows.Count >= 1)
                            {
                                GridView1.Visible = true;
                                GridView1.FooterRow.Visible = false;
                                GridView1.Columns[0].Visible = false;
                            }
                            else
                            {

                                GridView1.Visible = false;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void getautoMtypedata()
        {
            try
            {

                conn.Close();

                cmd.CommandText = @"select * from tbl_AutoMtypeMM where MaterialType = '" + ddlMaterialType.SelectedValue.ToString().Trim() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                adp.SelectCommand = cmd;
                dt.Clear();
                adp.Fill(dt);
                DataTableReader reader = dt.CreateDataReader();
                while (reader.Read())
                {
                    reader.Read();


                    //for (int i = 0; i < ddlPlant.Items.Count; i++)
                    //{
                    //    foreach (string category in reader[4].ToString().Split(','))
                    //    {
                    //        if (category != ddlPlant.Items[i].Value) continue;
                    //        ddlPlant.Items[i].Selected = true;
                    //        break;
                    //    }
                    //}  
                    ddlMG.SelectedValue = reader["MaterialGroup"].ToString();
                    bindMSGfromMG();
                    //  ddlMSG.SelectedValue = reader["MaterialSubGroup"].ToString();
                    //     ddlPurchasingGroup.SelectedValue = reader["Purchasing_Group"].ToString();
                    //string PH = reader[19].ToString();
                    //string[] lines = PH.Split(',');
                    //string aa = lines[0].Trim();
                    //string ab = lines[1].Trim();
                    //string ac = lines[2].Trim();
                    //ddlProdCatg.SelectedValue = aa.ToString().Trim();
                    //ddlProdCatgsub1.SelectedValue = ab.ToString().Trim();
                    //ddlProdCatgsub2.SelectedValue = ac.ToString().Trim();


                    //    ddlMrpType.SelectedValue = reader["MRPType"].ToString();
                    //   ddlMRPGroup.SelectedValue = reader["MRP_Group"].ToString();
                    //   ddlValuationClass.SelectedValue = reader["ValuationClass"].ToString();
                    //   ddlValuationCategory.SelectedValue = reader["ValuationCategory"].ToString();


                    //bindSLfromPlant();
                    //for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                    //{
                    //    foreach (string StorageLocation in reader[53].ToString().Split(','))
                    //    {
                    //        if (StorageLocation != ddlStorageLocation.Items[i].Value) continue;
                    //        ddlStorageLocation.Items[i].Selected = true;
                    //        break;
                    //    }
                    //}

                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindMaterialType()
        {
            try
            {
                ds = obj.BindMaterialType();
                ddlMaterialType.DataTextField = ds.Tables["MaterialType"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlMaterialType.DataValueField = ds.Tables["MaterialType"].Columns["MaterialTypecode"].ToString();             // to retrive specific  textfield name 
                ddlMaterialType.DataSource = ds.Tables["MaterialType"];      //assigning datasource to the dropdownlist
                ddlMaterialType.DataBind();  //binding dropdownlist
                ddlMaterialType.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindSAPMaterialCode()
        {
            try
            {
                ds = obj.getSAPMaterialCode();
                ddlSearchMC.DataTextField = ds.Tables["SAPMaterialCode"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlSearchMC.DataValueField = ds.Tables["SAPMaterialCode"].Columns["SAPMaterialCode"].ToString();             // to retrive specific  textfield name 
                ddlSearchMC.DataSource = ds.Tables["SAPMaterialCode"];      //assigning datasource to the dropdownlist
                ddlSearchMC.DataBind();  //binding dropdownlist
                ddlSearchMC.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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

                ddlOrderingUnit.DataTextField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString(); // text field name of table dispalyed in dropdown
                ddlOrderingUnit.DataValueField = ds.Tables["BaseUnitOfMeasure"].Columns["Baseuom"].ToString();             // to retrive specific  textfield name 
                ddlOrderingUnit.DataSource = ds.Tables["BaseUnitOfMeasure"];      //assigning datasource to the dropdownlist
                ddlOrderingUnit.DataBind();  //binding dropdownlist
                ddlOrderingUnit.Items.Insert(0, new ListItem("------Select------", "0"));

            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }

        }

        private void BindProductHierarchy()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    ds.Clear();
                    cmd.CommandText = "SP_BindProductHierarchy";
                    cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindProductHierarchy2()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    ds.Clear();
                    cmd.CommandText = "SP_BindProductHierarchy2";
                    cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }

        }

        private void BindProductHierarchy3()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    ds.Clear();
                    cmd.CommandText = "SP_BindProductHierarchy3";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    //// cmd.Parameters.AddWithValue("@Materialtypcode", "%" + ProductHierarchyH3.ToString() + "%");
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }

        }

        private void BindValuationCategory()
        {
            try
            {
                ds = obj.BindValuationCategory();
                ddlValuationCategory.DataTextField = ds.Tables["ValuationCategory"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlValuationCategory.DataValueField = ds.Tables["ValuationCategory"].Columns["ValuationCategorycode"].ToString();             // to retrive specific  textfield name 
                ddlValuationCategory.DataSource = ds.Tables["ValuationCategory"];      //assigning datasource to the dropdownlist
                ddlValuationCategory.DataBind();  //binding dropdownlist
                ddlValuationCategory.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindMaterialSubGroup()
        {
            try
            {
                cmd.CommandText = "SP_MaterialSubGroup";
                cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindVolumeunit()
        {
            try
            {
                cmd.CommandText = "SP_Volumeunit";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                adp.Fill(ds, "Volumeunit");
                ddlVOLUMEUNIT.DataTextField = ds.Tables["Volumeunit"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlVOLUMEUNIT.DataValueField = ds.Tables["Volumeunit"].Columns["Volumeunit"].ToString();             // to retrive specific  textfield name 
                ddlVOLUMEUNIT.DataSource = ds.Tables["Volumeunit"];      //assigning datasource to the dropdownlist
                ddlVOLUMEUNIT.DataBind();  //binding dropdownlist
                ddlVOLUMEUNIT.Items.Insert(0, new ListItem("------Select------", "0"));

                ddlVolumUnit.DataTextField = ds.Tables["Volumeunit"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlVolumUnit.DataValueField = ds.Tables["Volumeunit"].Columns["Volumeunit"].ToString();             // to retrive specific  textfield name 
                ddlVolumUnit.DataSource = ds.Tables["Volumeunit"];      //assigning datasource to the dropdownlist
                ddlVolumUnit.DataBind();  //binding dropdownlist
                //Adding "Please select" option in dropdownlist for validation
                ddlVolumUnit.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }

        }

        private void Bindweightunit()
        {
            try
            {
                cmd.CommandText = "SP_weightunit";
                cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }

        }

        private void BindDivision()
        {
            try
            {
                cmd.CommandText = "SP_Division";
                cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindItemCateguoryGroup()
        {
            try
            {
                cmd.CommandText = "SP_ItemCateguoryGroup";
                cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindLoomType()
        {
            try
            {
                cmd.CommandText = "SP_LoomType";
                cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindRoomReady()
        {
            try
            {
                cmd.CommandText = "SP_RoomReady";
                cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindSubDivision()
        {
            try
            {
                cmd.CommandText = "SP_SubDivision";
                cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindNOS()
        {
            try
            {
                cmd.CommandText = "SP_NOS";
                cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindTransportionGroup()
        {
            try
            {
                cmd.CommandText = "SP_TransportionGroup";
                cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindPackagingMaterialCateguory()
        {
            try
            {
                cmd.CommandText = "SP_PackagingMaterialCateguory";
                cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }

        }

        private void BindMrpGroup()
        {
            try
            {
                cmd.CommandText = "SP_MrpGrp";
                cmd.CommandType = CommandType.StoredProcedure;
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindMrpGroupMtype()
        {
            try
            {
                cmd.CommandText = "SP_BindMrpGroupMtype";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Materialtypcode", "%" + ddlMaterialType.SelectedValue + "%");
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindMRPControllerMtype()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        cmd.CommandText = "SP_BindMRPControllerMtype";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@Materialtypcode", "%" + ddlMaterialType.SelectedValue.ToString() + "%");
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
                        dvemaillbl.Visible = true;
                        lblError.Text = ex.ToString();
                    }
                }
            }
        }

        private void BindBackFlush()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        cmd.CommandText = "SP_BackFlush";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
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
                        dvemaillbl.Visible = true;
                        lblError.Text = ex.ToString();
                    }
                }
            }
        }

        private void BindPackagingMaterialType()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        cmd.CommandText = "SP_PackagingMaterialType";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
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
                        dvemaillbl.Visible = true;
                        lblError.Text = ex.ToString();
                    }
                }
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void BindGrid()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())//
                {
                    try
                    {
                        cmd.CommandText = "EXEC SP_AltUnitOfMeasureGrid" + " @TransactionID='" + lblMaxTransactionID.Text.ToString() + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
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
                    catch (SqlException ex)
                    {
                        dvemaillbl.Visible = true;
                        lblError.Text = ex.ToString();
                    }
                }
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        protected void Clear(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["ConvertionFacter"];
                dt.Clear();
                ViewState["ConvertionFacter"] = dt;
                GridView1.DataSource = (DataTable)ViewState["ConvertionFacter"];
                GridView1.DataBind();
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        protected void refreshpage()
        {
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "myscript", "setTimeout(function(){location.href='MeterialMaster.aspx';},15000);", true);
            ClearInputs(Page.Controls);
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
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                        ((DropDownList)ctrl).SelectedIndex = 0;
                    if (ctrl is ListBox)
                        ((ListBox)ctrl).SelectedIndex = -1;
                    ClearInputs(ctrl.Controls);
                    for (int i = 0; i < ddlNotificationMIS.Items.Count - ddlNotificationMIS.Items.Count + 1; i++)
                    {
                        ddlNotificationMIS.Items[i].Selected = true;
                    }
                    for (int i = 0; i < ddlNotificationFI.Items.Count - ddlNotificationFI.Items.Count + 1; i++)
                    {
                        ddlNotificationFI.Items[i].Selected = true;
                    }

                }

                DataTable dt = (DataTable)ViewState["ConvertionFacter"];
                dt.Clear();
                ViewState["ConvertionFacter"] = dt;
                GridView1.DataSource = (DataTable)ViewState["ConvertionFacter"];
                GridView1.DataBind();
                lblError.Text = "";
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                for (int i = 0; i <= ddlValuationType.Items.Count - 1; i++)
                {
                    if (ddlValuationType.Items[i].Selected)
                    {
                        if (ValuationType == "") { ValuationType = ddlValuationType.Items[i].Value.Trim(); }
                        else { ValuationType += ',' + ddlValuationType.Items[i].Value.Trim(); }
                    }
                    ValuationType = ValuationType.Trim();
                }
                string a = ddlProdCatg.SelectedValue;
                string b = ddlProdCatgsub1.SelectedValue;
                string c = ddlProdCatgsub2.SelectedValue;
                string Temp = a.Trim() + "," + b.Trim() + "," + c.Trim();
                string ProductCatg = Temp.ToString();
                string transactionID = lblMaxTransactionNo.Text.ToString();

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

                Result = ViewState["HOD"].ToString() + "," + NotificationFI + "," + Notification;
                cmd.CommandText = "";
                cmd.CommandText = "EXEC SP_SYS_MaterialMasterMain" + " @TransactionMain  ='" + lblMaxTransactionNo.Text + "', " +
                " @MaterialType  ='" + ddlMaterialType.SelectedValue + "', " +
                " @SAPMaterialCode  ='', " +
                " @Plant ='" + Plant.ToString() + "', " +
                " @ExToOtherPlant ='" + ExToPlant.ToString() + "', " +
                " @Description ='" + txtDescription.Text + "', " +
                " @BaseUnitofMeasure ='" + ddlMMBaseUnitOfMeasure.SelectedValue + "', " +
                " @MaterialGroup ='" + ddlMG.SelectedValue + "', " +
                " @MaterialSubGroup ='" + ddlMSG.SelectedValue + "', " +
                " @GrossWeight ='" + txtGROSSWEIGHT.Text + "', " +
                " @NetWeight ='" + txtNETWEIGHT.Text + "', " +
                " @WeightUni ='" + ddlWeightunitBD.SelectedValue + "', " +
                " @Volume ='" + txtVolume.Text + "', " +
                " @VolumeUnit ='" + ddlVOLUMEUNIT.SelectedValue + "', " +
                " @OldMaterailNo ='" + txtOldMaterialNumber.Text + "', " +
                " @Size_Dimension ='" + txtSizeDimensions.Text + "', " +
                " @Packeging_Material_Catg ='" + ddlBasicDataPackagingMaterialCateguory.SelectedValue + "', " +
                " @BatchManagmet ='" + chkBatchManagement.SelectedValue + "', " +
                " @ProductHierarchy ='" + ProductCatg.ToString() + "', " +
                " @DistributionChannel ='" + ddlDistributionChannel.SelectedValue + "', " +
                " @SalesOrg ='" + ddlSalesOrg.SelectedValue + "', " +
                " @SalesUnit ='" + ddlSalesUnit.SelectedValue + "', " +
                " @Division ='" + ddlDivision.SelectedValue + "', " +
                " @TaxClasification ='" + ddlTaxClassification.SelectedValue + "', " +
                " @Item_Catg_Group ='" + ddlItemCateguoryGroup.SelectedValue + "', " +
                " @LoomType ='" + ddlLoomType.SelectedValue + "', " +
                " @RoomReady ='" + ddlRoomReady.SelectedValue + "', " +
                " @SubDivision ='" + ddlSubDivision.SelectedValue + "', " +
                " @NOS ='" + ddlNOS.SelectedValue + "', " +
                " @Availabilitycheck ='" + ddlAvailabilitycheck.SelectedValue + "', " +
                " @TransportaionGroup ='" + ddlTransportionGroup.SelectedValue + "', " +
                " @LoadingGroup ='" + ddlLoadingGroup.SelectedValue + "', " +
                " @ProfitCenter ='" + ddlProfitCenter.SelectedValue + "', " +
                " @SalesOrderTax ='" + txtSalesodertext.Text + "', " +
                " @Material_Rebate_Rate ='" + ddlRate.SelectedValue + "', " +
                " @Rebate_Catg ='" + ddlRebatecategoryRate.SelectedValue + "', " +
                " @Purchasing_Group ='" + ddlPurchasingGroup.SelectedValue + "', " +
                " @OrderingUnit ='" + ddlOrderingUnit.SelectedValue + "', " +
                " @PurchaseOrderText ='" + txtPurchaseOrderText.Text + "', " +
                " @MRPType ='" + ddlMrpType.SelectedValue + "', " +
                " @MRP_Group ='" + ddlMRPGroup.SelectedValue + "', " +
                " @ReoderPoint ='" + txtReoderPoint.Text + "', " +
                " @MRPController ='" + ddlMRPController.SelectedValue + "', " +
                " @BackFlush ='" + ddlBackFlush.SelectedValue + "', " +
                " @Planned_Delivery_Time_In_Days ='" + txtPlannedDeliveryTimeInDays.Text + "', " +
                " @In_House_Production_Time_In_Days ='" + txtInHouseProductionTimeInDays.Text + "', " +
                " @Gr_Processing_Time_In_Days ='" + txtGRPROCESSINGTIMEINDAYS.Text + "', " +
                " @Safety_Stock ='" + txtSafetyStock.Text + "', " +
                " @Production_Unit_Of_Measure ='" + ddlProductionunit.SelectedValue + "', " +
                " @UnitOfIssue ='" + ddlUnitOfIssue.Text + "', " +
                " @Prodsupervisor ='" + ddlProdsupervisor.SelectedValue + "', " +
                " @ProdScheduleProfile ='" + ddlProdScheduleProfile.SelectedValue + "', " +
                " @Storage_Location ='" + StorageLocation.ToString() + "', " +
                " @Under_Delivery_Tollerance ='" + txtUnderDeliveryTollerance.Text + "', " +
                " @Ove_Delivery_Tollerance ='" + txtOverDeliveryTollerance.Text + "', " +
                " @TaskListUsage ='" + ddlTaskListUsage.Text + "', " +
                " @ValuationClass ='" + ddlValuationClass.SelectedValue + "', " +
                " @ValuationCategory ='" + ddlValuationCategory.Text + "', " +
                " @ValuationType ='" + ValuationType.ToString() + "', " +
                " @QMControlKey ='" + ddlQMControlKey.SelectedValue + "', " +
                " @InspectionSetup ='" + valuechkInspectionSetup.ToString() + "', " +
                " @QMprocactive ='" + valuechkQmProcActive.ToString() + "', " +
                " @ReorderPoint ='" + txtReoderPoint.Text + "', " +
                " @MinimumLotSize ='" + txtMinimumLotSize.Text + "', " +
                " @MaximumLotSize ='" + txtMaximumLotSize.Text + "', " +
                " @Maximumstocklevel ='" + txtMaximumstocklevel.Text + "', " +
                " @SchedMarginkey ='" + TxtSchedMarginkey.Text + "', " +
                " @PeriodIndicator ='" + ddlPeriodIndicator.SelectedValue + "', " +
                " @Strategygroup ='" + ddlStrategygroup.SelectedValue + "', " +
                " @Lotsize ='" + ddlLotsize.SelectedValue + "', " +
                " @Packaging_Material_Categuory ='" + ddlPackagingMaterialCateguory.SelectedValue + "', " +
                " @Packaging_Material_Type ='" + ddlPackagingMaterialType.Text + "', " +
                " @Allowed_Packaging_Weight ='" + txtAllowedPackagingWeight.Text + "', " +
                " @AllowedPackagingWeightUnit ='" + ddlWeightUnit.SelectedValue + "', " +
                " @AllowedPackagingVolme ='" + txtAllowedPackagingVolme.Text + "', " +
                " @AllowedPackagingVolmeUnit ='" + ddlVolumUnit.SelectedValue + "', " +
                " @ExcessWeightTolerance ='" + txtExcessWeightTolerance.Text + "', " +
                " @ExcessVolumnTolerance ='" + txtExcessVolumeTolerance.Text + "', " +
                " @APPROVAL ='" + Result.ToString() + "', " +
                " @REVIEWER ='" + EmailReviwer.ToString() + "', " +
                " @MDA ='" + EmailMDA.ToString() + "', " +
                " @ClosedBox ='" + RadioButtonList2.SelectedValue.ToString() + "', " +
                " @CreatedBy ='" + Session["User_Name"].ToString() + "', " +
                " @Remarks ='" + txtRemarksReview.Text.ToString() + "', " +
                " @Status ='" + FormType.ToString() + "'";


                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                ds.Clear();
                adp.SelectCommand = cmd;
                adp.Fill(ds, "Message");
                sucess.Visible = true;

                string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
                lblmessage.Text = message + " # " + lblMaxTransactionID.Text;

                convestionFactorInsert();
                DummyGrid();
                EmailWorkSendFirstApproval();
                lblmessage.Focus();
                error.Visible = false;
                lblmessage.Focus();
                Page.MaintainScrollPositionOnPostBack = false;
                refreshpage();
                GetTransactionID();
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
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        protected void convestionFactorInsert()
        {
            try
            {
                DataTable dtCurrentTable = (DataTable)ViewState["ConvertionFacter"];

                if (dtCurrentTable != null)
                {
                    string consString = ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(consString))
                    {
                        using (SqlCommand cmdSP = new SqlCommand("SP_SYS_ createAltUnitOfMeasure"))
                        {
                            cmdSP.CommandType = CommandType.StoredProcedure;
                            cmdSP.Connection = con;
                            for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                            {
                                if (dtCurrentTable.Rows[i][1] == DBNull.Value)
                                    dtCurrentTable.Rows[i].Delete();
                            }
                            //dtCurrentTable.AcceptChanges();@TransactionIDDelete
                            dtCurrentTable.PrimaryKey = null;
                            dtCurrentTable.Columns.Remove("sno");
                            cmdSP.Parameters.AddWithValue("@tblAltUnitOfMeasure", dtCurrentTable);
                            cmdSP.Parameters.AddWithValue("@TransactionIDDelete", lblMaxTransactionID.Text);
                            con.Open();
                            cmdSP.ExecuteNonQuery();
                            con.Close();
                            ViewState["ConvertionFacter"] = dtcon;
                            DataColumn c = new DataColumn("sno", typeof(int));
                            c.AutoIncrement = true;
                            c.AutoIncrementSeed = 1;
                            c.AutoIncrementStep = 1;
                            dtcon.Columns.Add(c);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        protected void convestionFactorDelete()
        {
            try
            {
                string consString1 = ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString1))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from tblAltUnitOfMeasure where TransactionID = @TransactionIDDel;"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;

                        cmd.Parameters.AddWithValue("@TransactionIDDel", lblMaxTransactionID.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                        TransactionID = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has sent you a New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " for approval. <br><br> Your  kind approval is required on the following URL: " +
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                        url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                        urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                        TransactionID = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Material Code Generated – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> SAP material code " + txtSMC.Text.Trim() + " has been issued against  new material creation request Form ID # " + lblMaxTransactionID.Text.ToString() + " <br><br> <br> <br> The form can be reviewed at the following URL: <br>  <br>" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                        url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                        urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                        TransactionID = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> A new material creation request against Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br> <br> You are kind approval is required for the information on the following URL: <br>  <br>" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
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

                            url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                            urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                            TransactionID = reader["TransactionID"].ToString();
                            FormCode = reader["FormID"].ToString();
                            UserName = reader["user_name"].ToString();
                            UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                            EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                            EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> A new material creation request against Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br> <br> You are requested to create a masterial code on the following URL: <br>  <br>" +
                            "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                            "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                            "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                                "<br>Material Master Application <br> Information Systems Dashboard";
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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

                        url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                        urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                        TransactionID = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br> A new material creation request against Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been disapproved by " + ViewState["SessionUser"].ToString() + " <br> <br> You are requested to create a masterial code on the following URL: <br>  <br>" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "<br> <br> <br><b>Reject Remarks: " + txtRemarksReview.Text + "</b> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        private void GetActiceDriectory()
        {
            try
            {
                Email UserEmail = new Email();
                DataTable table1 = new DataTable();
                UserEmail.GetUserDetails(ref table1);

                ddlEmailReviwer.DataTextField = "Name"; // text field name of table dispalyed in dropdown
                ddlEmailReviwer.DataValueField = "mail";
                ddlEmailReviwer.DataSource = table1;
                ddlEmailReviwer.DataBind();
                ddlEmailMDA.DataTextField = "Name"; // text field name of table dispalyed in dropdown
                ddlEmailMDA.DataValueField = "mail";
                ddlEmailMDA.DataSource = table1;
                ddlEmailMDA.DataBind();
                ViewState["DirectoryData"] = table1;

            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
                    if (c is RadioButtonList)
                    {
                        ((RadioButtonList)(c)).Enabled = State;
                    }
                    if (c is RadioButton)
                    {
                        ((RadioButton)(c)).Enabled = State;
                    }
                    DisableControls(c, State);
                    RadioButtonList2.Enabled = false;
                }
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }

        }
        //-------------------------------------END METHODS-------------------------------------------

        #region methodEmailWorks

        #endregion




        protected void btnApprover_Click(object sender, EventArgs e)
        {
            try
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
                    EmailWorkReject();
                    ClosedFormAfterReject();
                    //  ApplicationStatus();
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
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }

        }

        protected void ddlEmailReviwer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlEmailReviwer.SelectedValue != "")
                {
                    foreach (ListItem item in ddlEmailMDA.Items)
                    {
                        if (item.ToString() == ddlEmailReviwer.SelectedItem.Text)
                        {
                            item.Attributes.Add("disabled", "disabled");
                        }
                    }

                }

                else
                {
                    foreach (ListItem item1 in ddlEmailMDA.Items)
                    {

                        item1.Enabled = true;
                    }
                }
                Page.MaintainScrollPositionOnPostBack = true;
                btnSave.Focus();
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }
        protected void ddlEmailMDA_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlEmailMDA.SelectedValue != "")
                {
                    foreach (ListItem item in ddlEmailReviwer.Items)
                    {
                        if (item.ToString() == ddlEmailMDA.SelectedItem.Text)
                        {
                            item.Attributes.Add("disabled", "disabled");
                        }
                    }
                }
                else
                {
                    foreach (ListItem item1 in ddlEmailReviwer.Items)
                    {

                        item1.Enabled = true;
                    }
                }
                Page.MaintainScrollPositionOnPostBack = true;
                btnSave.Focus();
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSearchMC.SelectedValue.ToString() == "")
                {
                    lblUpError.Text = "Please select any exsisting material!";
                    error.Visible = true;
                    return;
                }
                string a = ddlSearchMC.SelectedValue.ToString().Trim();
                cmd.CommandText = @"Select * from tbl_SYS_MaterialMaster where SAPMaterialCode = '" + a.ToString() + "' and Status = 'N'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                adp.SelectCommand = cmd;
                dt.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    BD.Visible = true;
                    CF.Visible = true;
                    Prod.Visible = true;
                    Account.Visible = true;
                    Pack.Visible = true;
                    Purch.Visible = true;
                    SD.Visible = true;
                    QM.Visible = true;
                    MRP.Visible = true;
                    lblUpError.Text = "";
                    error.Visible = false;
                    divEmail.Visible = true;
                    DataTableReader reader = dt.CreateDataReader();
                    while (reader.Read())
                    {

                        reader.Read();

                        ddlMaterialType.SelectedValue = reader[2].ToString();
                        txtSMC.Text = reader[3].ToString();

                        BindPlantMtype();
                        for (int i = 0; i < ddlPlant.Items.Count; i++)
                        {
                            foreach (string category in reader[4].ToString().Split(','))
                            {
                                if (category != ddlPlant.Items[i].Value) continue;
                                ddlPlant.Items[i].Selected = true;
                                break;
                            }
                        }

                        txtDescription.Text = reader[6].ToString();
                        BindBaseUnitOfMeasureMTYPR();
                        ddlMMBaseUnitOfMeasure.SelectedValue = reader[7].ToString();
                        BindMaterialgroup();
                        ddlMG.SelectedValue = reader[8].ToString().Trim();
                        bindMSGfromMG();
                        ddlMSG.SelectedValue = reader[9].ToString();

                        txtGROSSWEIGHT.Text = reader[10].ToString();
                        txtNETWEIGHT.Text = reader[11].ToString();
                        ddlWeightunitBD.SelectedValue = reader[12].ToString();
                        txtVolume.Text = reader[13].ToString();
                        ddlVOLUMEUNIT.SelectedValue = reader[14].ToString();
                        txtOldMaterialNumber.Text = reader[15].ToString();
                        txtSizeDimensions.Text = reader[16].ToString();
                        ddlBasicDataPackagingMaterialCateguory.SelectedValue = reader[17].ToString();
                        chkBatchManagement.SelectedValue = reader[18].ToString();
                        string PH = reader[19].ToString();
                        string[] lines = PH.Split(',');
                        string aa = lines[0].Trim();
                        string ab = lines[1].Trim();
                        string ac = lines[2].Trim();
                        ddlProdCatg.SelectedValue = aa.ToString().Trim();
                        ddlProdCatgsub1.SelectedValue = ab.ToString().Trim();
                        ddlProdCatgsub2.SelectedValue = ac.ToString().Trim();
                        ddlDistributionChannel.SelectedValue = reader[20].ToString();
                        ddlSalesOrg.SelectedValue = reader[21].ToString();
                        ddlSalesUnit.SelectedValue = reader[22].ToString();
                        ddlDivision.SelectedValue = reader[23].ToString();
                        ddlTaxClassification.SelectedValue = reader[24].ToString();
                        ddlItemCateguoryGroup.SelectedValue = reader[25].ToString();
                        ddlLoomType.SelectedValue = reader[26].ToString();
                        ddlRoomReady.SelectedValue = reader[27].ToString();
                        ddlSubDivision.SelectedValue = reader[28].ToString();
                        ddlNOS.SelectedValue = reader[29].ToString();
                        ddlAvailabilitycheck.SelectedValue = reader[30].ToString();
                        ddlTransportionGroup.SelectedValue = reader[31].ToString();
                        ddlLoadingGroup.SelectedValue = reader[32].ToString();
                        ddlProfitCenter.SelectedValue = reader[33].ToString();
                        txtSalesodertext.Text = reader[34].ToString();
                        ddlRate.SelectedValue = reader[35].ToString();
                        ddlRebatecategoryRate.SelectedValue = reader[36].ToString();
                        BindPurchasingGroup();
                        ddlPurchasingGroup.SelectedValue = reader[37].ToString();
                        ddlOrderingUnit.SelectedValue = reader[38].ToString();
                        txtPurchaseOrderText.Text = reader[39].ToString();
                        BindMRPTypeMTYPE();
                        ddlMrpType.SelectedValue = reader[40].ToString();

                        BindMrpGroupMtype();
                        ddlMRPGroup.SelectedValue = reader[41].ToString();
                        txtReoderPoint.Text = reader[42].ToString();
                        BindMRPControllerMtype();
                        ddlMRPController.SelectedValue = reader[43].ToString();
                        ddlBackFlush.SelectedValue = reader[44].ToString();
                        txtPlannedDeliveryTimeInDays.Text = reader[45].ToString();
                        txtInHouseProductionTimeInDays.Text = reader[46].ToString();
                        txtGRPROCESSINGTIMEINDAYS.Text = reader[47].ToString();
                        txtSafetyStock.Text = reader[48].ToString();
                        ddlProductionunit.SelectedValue = reader[49].ToString();
                        ddlUnitOfIssue.SelectedValue = reader[50].ToString();
                        ddlProdsupervisor.SelectedValue = reader[51].ToString();
                        ddlProdScheduleProfile.SelectedValue = reader[52].ToString();
                        bindSLfromPlant();
                        for (int i = 0; i < ddlStorageLocation.Items.Count; i++)
                        {
                            foreach (string StorageLocation in reader[53].ToString().Split(','))
                            {
                                if (StorageLocation != ddlStorageLocation.Items[i].Value) continue;
                                ddlStorageLocation.Items[i].Selected = true;
                                break;
                            }
                        }
                        //txtStLocation.Text = reader[53].ToString();
                        txtUnderDeliveryTollerance.Text = reader[54].ToString();
                        txtOverDeliveryTollerance.Text = reader[55].ToString();
                        ddlTaskListUsage.SelectedValue = reader[56].ToString();
                        BindValuationClass();
                        ddlValuationClass.SelectedValue = reader[57].ToString();
                        BindValuationCategoryMTYPE();
                        ddlValuationCategory.SelectedValue = reader[58].ToString();
                        for (int i = 0; i < ddlValuationType.Items.Count; i++)
                        {
                            foreach (string category1 in reader[59].ToString().Split(','))
                            {
                                if (category1 != ddlValuationType.Items[i].Value) continue;
                                ddlValuationType.Items[i].Selected = true;
                                break;
                            }
                        }
                        ddlQMControlKey.SelectedValue = reader[60].ToString();

                        string InspectionSetup = "";
                        InspectionSetup = reader[61].ToString();
                        if (InspectionSetup == "1")
                        {
                            chkInspectionSetup.Checked = true;
                        }
                        else
                        {
                            chkInspectionSetup.Checked = false;
                        }
                        string QmProcActive = "";
                        QmProcActive = reader[62].ToString();
                        if (QmProcActive == "1")
                        {
                            chkQmProcActive.Checked = true;
                        }
                        else
                        {
                            chkQmProcActive.Checked = false;
                        }
                        txtMinimumLotSize.Text = reader[64].ToString();
                        txtMaximumLotSize.Text = reader[65].ToString();
                        txtMaximumstocklevel.Text = reader[66].ToString();
                        TxtSchedMarginkey.Text = reader[67].ToString();
                        ddlPeriodIndicator.SelectedValue = reader[68].ToString();
                        ddlStrategygroup.SelectedValue = reader[69].ToString();
                        ddlLotsize.SelectedValue = reader[70].ToString();
                        ddlPackagingMaterialCateguory.SelectedValue = reader[71].ToString();
                        ddlPackagingMaterialType.SelectedValue = reader[72].ToString();
                        txtAllowedPackagingWeight.Text = reader[73].ToString();
                        ddlWeightUnit.SelectedValue = reader[74].ToString();
                        txtAllowedPackagingVolme.Text = reader[75].ToString();
                        ddlVolumUnit.SelectedValue = reader[76].ToString();
                        txtExcessWeightTolerance.Text = reader[77].ToString();
                        txtExcessVolumeTolerance.Text = reader[78].ToString();
                        RadioButtonList2.SelectedValue = reader[82].ToString();
                        string ActionSelected = reader["Status"].ToString();

                        //dt.Clear();
                        //cmd.CommandText = "";
                        //cmd.CommandText = "EXEC SP_AltUnitOfMeasureGrid " + " @TransactionID  ='" + lblMaxTransactionID.Text + "'";
                        //cmd.CommandType = CommandType.Text;
                        //cmd.Connection = conn;
                        //adp.SelectCommand = cmd;
                        //adp.Fill(dt);
                        //DataColumn c = new DataColumn("sno", typeof(int));
                        //c.AutoIncrement = true;
                        //c.AutoIncrementSeed = 1;
                        //c.AutoIncrementStep = 1;
                        //dt.Columns.Add(c);
                        //GridView1.DataSource = dt;
                        //GridView1.DataBind();
                        //if (GridView1.Rows.Count >= 1)
                        //{
                        //    GridView1.Visible = true;
                        //    GridView1.FooterRow.Visible = false;
                        //    GridView1.Columns[0].Visible = false;
                        //}
                        //else
                        //{
                        //    GridView1.Visible = false;
                        //}

                    }
                    reader.Close();
                }
                else
                {
                    lblUpError.Text = "No Data Found";
                    error.Visible = true;
                }
                dvemaillbl.Visible = true;
            }
            catch (Exception ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdate.Visible = true;
                btnTransfer.Visible = false;
                btnEdit.Visible = false;
                DisableControls(Page, true);
                txtStandardPrice.Enabled = false;
                RadioButtonList2.Enabled = true;
                btnApprover.Visible = false;
                //btnReject.Visible = false;
                ddlPlant.Attributes.Remove("Disabled");
                ddlStorageLocation.Attributes.Remove("Disabled");
                ddlValuationType.Attributes.Remove("Disabled");
                GridView1.Columns[0].Visible = true;
                txtRemarksReview.Enabled = true;
                if (GridView1.Rows.Count >= 1)
                {
                    GridView1.Visible = true;
                    GridView1.FooterRow.Visible = true;
                    GridView1.Columns[0].Visible = true;
                }
                else
                {

                    GridView1.Visible = true;
                }
                ddlMaterialType.Enabled = false;
                //   DummyGrid();
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
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

                    string UpValuationType = "";
                    for (int i = 0; i <= ddlValuationType.Items.Count - 1; i++)
                    {
                        if (ddlValuationType.Items[i].Selected)
                        {
                            if (UpValuationType == "") { UpValuationType = ddlValuationType.Items[i].Value; }
                            else { UpValuationType += "," + ddlValuationType.Items[i].Value; }
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

                    cmd.CommandText = @"UPDATE tbl_SYS_MaterialMaster
      SET Plant = @UpPlant,
      Description = @UpDescription,
      BaseUnitofMeasure = @UpBaseUnitofMeasure,
      MaterialGroup = @UpMaterialGroup,
      MaterialSubGroup = @UpMaterialSubGroup,
      GrossWeight = @UpGrossWeight,
      NetWeight = @UpNetWeight,
      WeightUni = @UpWeightUni,
      Volume = @UpVolume,
      VolumeUnit = @UpVolumeUnit,
      OldMaterailNo = @UpOldMaterailNo,
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
      Purchasing_Group = @UpPurchasing_Group,
      OrderingUnit = @UpOrderingUnit,
      PurchaseOrderText = @UpPurchaseOrderText,
      MRPType = @UpMRPType,
      MRP_Group = @UpMRP_Group,
      ReoderPoint = @UpReoderPoint,
      MRPController = @UpMRPController,
      BackFlush = @UpBackFlush,
      Planned_Delivery_Time_In_Days = @UpPlanned_Delivery_Time_In_Days,
      In_House_Production_Time_In_Days = @UpIn_House_Production_Time_In_Days,
      Gr_Processing_Time_In_Days = @UpGr_Processing_Time_In_Days,
      Safety_Stock = @UpSafety_Stock,
      Production_Unit_Of_Measure = @UpProduction_Unit_Of_Measure,
      UnitOfIssue = @UpUnitOfIssue,
      Prodsupervisor = @UpProdsupervisor,
      ProdScheduleProfile = @UpProdScheduleProfile,
      Storage_Location = @UpStorage_Location,
      Under_Delivery_Tollerance = @UpUnder_Delivery_Tollerance,
      Ove_Delivery_Tollerance = @UpOve_Delivery_Tollerance,
      TaskListUsage = @UpTaskListUsage,
      ValuationClass = @UpValuationClass,
      ValuationCategory = @UpValuationCategory,
      ValuationType = @UpValuationType,
      QMControlKey = @UpQMControlKey,
      InspectionSetup = @UpInspectionSetup,
      QMprocactive = @UpQMprocactive,
      ReorderPoint = @UpReorderPoint,
      MinimumLotSize = @UpMinimumLotSize,
      MaximumLotSize = @UpMaximumLotSize,
      Maximumstocklevel = @UpMaximumstocklevel,
      SchedMarginkey = @UpSchedMarginkey,
      PeriodIndicator = @UpPeriodIndicator,
      Strategygroup = @UpStrategygroup,
      Lotsize = @UpLotsize,
      Packaging_Material_Categuory = @UpPackaging_Material_Categuory,
      Packaging_Material_Type = @UpPackaging_Material_Type,
      Allowed_Packaging_Weight = @UpAllowed_Packaging_Weight,
      AllowedPackagingWeightUnit = @UpAllowedPackagingWeightUnit,
      AllowedPackagingVolme = @UpAllowedPackagingVolme,
      AllowedPackagingVolmeUnit = @UpAllowedPackagingVolmeUnit,
      ExcessWeightTolerance = @UpExcessWeightTolerance,
      ExcessVolumnTolerance = @UpExcessVolumnTolerance,
      UpdateComment = @upUpdateComment,
      ClosedBox = @UpClosedBox where TransactionID = '" + lblMaxTransactionID.Text + "' ";
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
                    cmd.Parameters.AddWithValue("@UpPurchasing_Group", ddlPurchasingGroup.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpOrderingUnit", ddlOrderingUnit.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpPurchaseOrderText", txtPurchaseOrderText.Text);
                    cmd.Parameters.AddWithValue("@UpMRPType", ddlMrpType.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpMRP_Group", ddlMRPGroup.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpReoderPoint", txtReoderPoint.Text);
                    cmd.Parameters.AddWithValue("@UpMRPController", ddlMRPController.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpBackFlush", ddlBackFlush.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpPlanned_Delivery_Time_In_Days", txtPlannedDeliveryTimeInDays.Text);
                    cmd.Parameters.AddWithValue("@UpIn_House_Production_Time_In_Days", txtInHouseProductionTimeInDays.Text);
                    cmd.Parameters.AddWithValue("@UpGr_Processing_Time_In_Days", txtGRPROCESSINGTIMEINDAYS.Text);
                    cmd.Parameters.AddWithValue("@UpSafety_Stock", txtSafetyStock.Text);
                    cmd.Parameters.AddWithValue("@UpProduction_Unit_Of_Measure", ddlProductionunit.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpUnitOfIssue", ddlUnitOfIssue.Text);
                    cmd.Parameters.AddWithValue("@UpProdsupervisor", ddlProdsupervisor.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpProdScheduleProfile", ddlProdScheduleProfile.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpStorage_Location", UpStorage_Location.ToString());
                    cmd.Parameters.AddWithValue("@UpUnder_Delivery_Tollerance", txtUnderDeliveryTollerance.Text);
                    cmd.Parameters.AddWithValue("@UpOve_Delivery_Tollerance", txtOverDeliveryTollerance.Text);
                    cmd.Parameters.AddWithValue("@UpTaskListUsage", ddlTaskListUsage.Text);
                    cmd.Parameters.AddWithValue("@UpValuationClass", ddlValuationClass.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpValuationCategory", ddlValuationCategory.Text);
                    cmd.Parameters.AddWithValue("@UpValuationType", UpValuationType.ToString());
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
                    cmd.Parameters.AddWithValue("@UpPackaging_Material_Categuory", ddlPackagingMaterialCateguory.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpPackaging_Material_Type", ddlPackagingMaterialType.Text);
                    cmd.Parameters.AddWithValue("@UpAllowed_Packaging_Weight", txtAllowedPackagingWeight.Text);
                    cmd.Parameters.AddWithValue("@UpAllowedPackagingWeightUnit", ddlWeightUnit.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpAllowedPackagingVolme", txtAllowedPackagingVolme.Text);
                    cmd.Parameters.AddWithValue("@UpAllowedPackagingVolmeUnit", ddlVolumUnit.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpExcessWeightTolerance", txtExcessWeightTolerance.Text);
                    cmd.Parameters.AddWithValue("@UpExcessVolumnTolerance", txtExcessVolumeTolerance.Text);
                    cmd.Parameters.AddWithValue("@UpClosedBox", RadioButtonList2.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@upUpdateComment", txtRemarksReview.Text.ToString());

                    conn.Open();

                    int aa = cmd.ExecuteNonQuery();
                    if (aa > 0)
                    {
                        convestionFactorDelete();
                        convestionFactorInsert();
                        lblmessage.Text = "Record updated sucessfully!";
                        lblmessage.Focus();
                        sucess.Visible = true;
                        error.Visible = false;
                        Page.MaintainScrollPositionOnPostBack = false;
                        dt.Clear();
                        cmd.CommandText = "";
                        cmd.CommandText = "EXEC SP_AltUnitOfMeasureGrid " + " @TransactionID  ='" + lblMaxTransactionID.Text + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        adp.SelectCommand = cmd;
                        adp.Fill(dtcon);
                        GridView1.DataSource = dtcon;
                        GridView1.DataBind();
                        if (GridView1.Rows.Count >= 1)
                        {
                            GridView1.Visible = true;
                            GridView1.FooterRow.Visible = false;
                            GridView1.Columns[0].Visible = false;
                        }
                        else
                        {
                            GridView1.Visible = false;
                        }
                        btnTransfer.Visible = true;
                        btnApprover.Visible = true;
                        // btnReject.Visible = true;
                        btnApprover.Visible = true;
                        btnUpdate.Visible = false;
                        btnEdit.Visible = true;
                        DisableControls(Page, false);
                        this.ddlPlant.Attributes.Add("disabled", "");
                        this.ddlValuationType.Attributes.Add("disabled", "");
                        this.ddlStorageLocation.Attributes.Add("disabled", "");
                        txtRemarksReview.BackColor = System.Drawing.Color.AliceBlue;
                        txtRemarksReview.Visible = true;
                        //string url = HttpContext.Current.Request.Url.ToString();
                        //Response.Redirect(url.ToString());
                        lblEmail.ForeColor = System.Drawing.Color.Blue;
                    }
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }
        protected void btnForward_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransferUser.SelectedValue == "0")
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

                    lblEmail.Text = "*New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been transferred to " + ddlTransferUser.SelectedItem.Text + "";
                    Session["HC"] = "06";
                    btnApprover.Enabled = false;
                    //    btnReject.Attributes.Add("disabled", "true");
                    btnEdit.Visible = false;
                    txtRemarksReview.Enabled = false;
                    txtRemarks.Enabled = true;
                    btnTransfer.Attributes.Add("disabled", "true");
                    ddlTransferUser.Enabled = true;
                    btnUpdate.Visible = false;
                    ddlTransferUser.SelectedIndex = -1;
                }
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }




        #region Form Forward Working By adnan khan 03-10-2016
        /// <summary>   Form Forward Working By adnan khan 03-10-2016/////////////////////////////////////////////////////////////////////
        private void GetHarcheyNextData()
        {
            GetHarcheyID();
            DataTable HIDDataTable = (DataTable)ViewState["HIDDataSet"];
            ds = obj.GetHarachyNextData(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString(), ViewState["HID"].ToString());
          dt = ds.Tables["GetHarachyNextData"];
          ViewState["GetHarachyNextDataDataSet"] = dt;
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
                    url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                    urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                    TransactionID = lblMaxTransactionID.Text;
                    FormCode = FormID.ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "New Material Creation Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has sent you a New Material Creation Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " for approval. <br><br> Your  kind approval is required on the following URL: " +
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



        /// </summary>   Form Forward Working By adnan khan 03-10-2016/////////////////////////////////////////////////////////////////////
        #endregion


        protected void ddlValuationCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSplitValueationMTYP();
                Page.MaintainScrollPositionOnPostBack = true;
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = ex.ToString();
            }
        }



        protected void updateStdPrice()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdInsertEmail = new SqlCommand())
                {
                    cmdInsertEmail.Connection = connection;
                    cmdInsertEmail.CommandType = CommandType.Text;
                    cmdInsertEmail.CommandText = @"update tbl_SYS_MaterialMaster set StandardPrice = '" + txtStandardPrice.Text + "' where TransactionID = '" + lblMaxTransactionID.Text + "'";

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