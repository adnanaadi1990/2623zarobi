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

namespace DashboardProject.Modules.Inventorymanagement
{
    public partial class QuotationApprovalWorkflow : System.Web.UI.Page
    {
        public string PdfPath;
        public string FormID = "QAF01";
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
        ComponentClass_AD objAD = new ComponentClass_AD();
        ComponentClass_FD objFD = new ComponentClass_FD();
        ComponentClass obj = new ComponentClass();

        protected void Page_Load(object sender, EventArgs e)
        {
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
                        cmd.CommandText = " SELECT * from  tbl_Inventoryadjustment where TransactionMain = @TNo";
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
                            txtDocNo.Text = reader["DocumentNo"].ToString();
                            lblFileName.Text = reader["FileName"].ToString();
                            txtDescription.Text = reader["Description"].ToString();
                        }


                        BindsysApplicationStatus();
                      //  GetSockDetail();
                        GetHarcheyID();
                        getUserDetail();
                        GetStatusHierachyCategoryControls();
                        DVERROR.Visible = true;
                        txtDescription.BackColor = System.Drawing.Color.AliceBlue;
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
                            txtDocNo.Enabled = false;
                            txtRemarksReview.Visible = true;

                        }
                        if (((string)ViewState["HID"]) == "4")
                        {
                            DVERROR.Visible = true;
                            btnApproved.Visible = false;
                            btnReject.Visible = true;
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
                            txtDocNo.Enabled = true;
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

                    }
                    else
                    {
                        DVERROR.Visible = true;
                        GetTransactionID();
                        BindUser();
                        getUserHOD();
                        getUserDetail();
                        txtDescription.BackColor = System.Drawing.Color.AliceBlue;
                    }
                }
                catch (SqlException ex)
                {
                    dvemaillbl.Visible = true;
                    lblError.Text = "Page_Load" + ex.ToString();
                }
            }
       }
        #region Methods
        //////////////////////////////////////////--------------Methods---------------//////////////////////////////////////////////////////
        //--///Bind Application Method///
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
                lblError.Text = "BindsysApplicationStatus" + ex.ToString();
            }
        }
        /////////GetStatusHierachyCategoryControls Method////////
        private void GetStatusHierachyCategoryControls()
        {

            try
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
                    txtRemarksReview.Enabled = false;
                    txtDocNo.Enabled = false;

                }
            }

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "GetStatusHierachyCategoryControls" + ex.ToString();
            }
        }
        //---////////////////////////--------------------------getUserDetails Method----------------------//////////////////////////
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
                lblError.Text = "getUserDetail" + ex.ToString();
            }
        }
        //-///////////////-----------------------GetHarcheyID Method-------------------------------///////////////////////////
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
            }

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "GetHarcheyID" + ex.ToString();
            }
        }
        /////////////////////////////-----------------BindUser Method-----------------------///////////////////////////////
        private void BindUser()
        {
            try
            {
              
                cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserMDA where FormName = 'IAA'";
                //cmd.CommandText = "SELECT * FROM tbluser where user_name = 'abdul.qadir'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();
                ddlEmailMDA.DataSource = cmd.ExecuteReader();
                ddlEmailMDA.DataTextField = "DisplayName";
                ddlEmailMDA.DataValueField = "user_name";
                ddlEmailMDA.DataBind();
                conn.Close();
                ddlEmailMDA.Items.Insert(0, new ListItem("------Select------", "0"));
            }

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "BindUser" + ex.ToString();
            }
        }
        ///////////////----------------------------------getUserHOD Method---------------------------------//////////////////////////////
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
                lblError.Text = "getUserHOD" + ex.ToString();
            }
        }
        ///////////////----------------------------------GetTransactionID Method---------------------------------//////////////////////////////
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
        ///////////////----------------------------------ClosedFormAfterReject Method---------------------------------//////////////////////////////
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

                    catch (SqlException ex)
                    {
                        dvemaillbl.Visible = true;
                        lblError.Text = "ClosedFormAfterReject" + ex.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }
        ///////////////----------------------------------Update Working Method---------------------------------//////////////////////////////
        private void UpdateWorking()
        {
            try
            {
                cmd.CommandText = @"update tbl_QuotationApproval set DocumentNo = @DocumentNo 
                               where TransactionID = @TransID ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@DocumentNo", txtDocNo.Text);
                cmd.Parameters.AddWithValue("@TransID", lblMaxTransactionID.Text);
                conn.Open();
                int a = cmd.ExecuteNonQuery();
                if (a == 1)
                {
                    EmailWorkSendMDA();
                    ApplicationStatus();
                    BindsysApplicationStatus();
                    InsertEmailHOD();
                    GetStatusHierachyCategoryControls();


                    lblmessage.Text = "Documnet No " + txtDocNo.Text + " has been issued against  New Petty Cash Request Form ID #  " + lblMaxTransactionID.Text + " ";
                    lblmessage.ForeColor = System.Drawing.Color.Green;
                    conn.Close();
                    sucess.Visible = true;
                    error.Visible = false;
                    lblmessage.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    btnPrint.Visible = true;
                    dvCheque.Visible = true;
                    GetSockDetail();
                }
            }

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "UpdateWorking" + ex.ToString();
            }
        }
        ///////////////----------------------------------Get Stock Details Method---------------------------------//////////////////////////////
        private void GetSockDetail()
        {
            try
            {
                ds = objAD.getDeadStock(lblMaxTransactionID.Text.ToString());
                grdDetail.DataSource = ds.Tables["getDeadStock"];
                grdDetail.DataBind();
            }

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "GetSockDetail" + ex.ToString();
            }
        }
        #endregion
        ///////////////----------------------------------Events---------------------------------//////////////////////////////
        //-------------------------------------------Upload Button------------------------///////////////////
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
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
                        fleUpload.PostedFile.SaveAs(Server.MapPath("~/DashboardDocument/InventoryAdjustment/" + character.ToString() + "_" + filename));
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

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "btnUpload_Click" + ex.ToString();
            }
        }
        //-///////////////----------------------------------Remove Button------------------------///////////////////
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                FilePath = "~/DashboardDocument/QuotationApprovalWorkflow/" + lblFileName.Text.ToString();
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

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "btnDelete_Click" + ex.ToString();
            }
        }

        //-///////////////----------------------------------Show Button------------------------///////////////////

        protected void btnShowFile_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                string pdfFileToDisplay = "../../DashboardDocument/InventoryAdjustment/" + lblFileName.Text;
                string pdfFileToDisplay1 = "DashboardDocument/InventoryAdjustment/" + lblFileName.Text;
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

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "btnShowFile_Click" + ex.ToString();
            }

        }
        //-///////////////----------------------------------SAve Button------------------------///////////////////
        protected void btnSave_Click(object sender, EventArgs e)
        {
            sucess.Visible = false;
            lblmessage.Text = "";
            try
            {
                if (lblFileName.Text == "")
                {
                    lblError.Text = "Please Upload any file.";
                    return;
                }
                if (ddlEmailMDA.SelectedValue == "0")
                {
                    ddlEmailMDA.BackColor = System.Drawing.Color.Red;
                    lblUpError.Text = "Please select any MDA";
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
                string Notification = "";

             
                FilePath = "~/DashboardDocument/InventoryAdjustment/" + "InventoryAdjustment" + lblFileName.Text.ToString();
                string Approval = ViewState["HOD"].ToString();
                cmd.CommandText = "Exec SP_SYS_create_InventoryManagment" + " @TransactionMain='" + lblMaxTransactionNo.Text + "', " +
                        " @FileName='" + lblFileName.Text + "', " +
                        " @Description='" + txtDescription.Text + "', " +
                        " @FilePath='" + FilePath.ToString() + "', " +
                        " @APPROVAL='" + Approval.ToString() + "', " +
                        " @REVIEWER='', " +
                        " @MDA='" + ddlEmailMDA.SelectedValue + "', " +
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
                lblMaxTransactionID.Text = "";
                GetTransactionID();
                btnDelete.Visible = false;
                btnShowFile.Visible = false;
                btnUpload.Visible = true;
                lblFileName.Text = "";
                txtRemarksReview.Text = "";
                txtDescription.Text = "";

              
            }


            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "btnSave_Click" + ex.ToString();
            }

            finally
            {
            }

        }

        protected void btnSaveSubmit_Click(object sender, EventArgs e)
        {

        }
        //-///////////////----------------------------------Approved Button------------------------///////////////////
        protected void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                ds = objFD.InsertAllHODS(FormID.ToString(), lblMaxTransactionID.Text, Session["User_Name"].ToString());
                EmailWorkApproved();
                ApplicationStatus();
                BindsysApplicationStatus();

            }
            catch (Exception ex)
            {
                lblError.Text = "Approver" + ex.ToString();
            }
 
        }
        //-///////////////----------------------------------Reject Button------------------------///////////////////
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
                    //   ApplicationStatus();
                    BindsysApplicationStatus();
                    GetStatusHierachyCategoryControls();
                }
            }

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "btnReject_Click" + ex.ToString();
            }
        }

        //-///////////////----------------------------------Approved Button------------------------///////////////////
        protected void btnMDA_Click(object sender, EventArgs e)
        {
            try
            {
                txtDocNo.ForeColor = System.Drawing.Color.Black;
                if (txtDocNo.Text == "")
                {

                    lblError.Text = "";
                    lblEmail.Text = "";
                    lblmessage.Text = "";
                    lblUpError.Text = "Document No should not be left blank";
                    sucess.Visible = true;
                    error.Visible = true;
                    lblmessage.Focus();
                    sucess.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtDocNo.BackColor = System.Drawing.Color.Red;
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    UpdateWorking();
                    GetSockDetail();
                }
            }

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "btnMDA_Click" + ex.ToString();
            }
        }
        //-///////////////----------------------------------Reset Form Button------------------------///////////////////
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            Response.Redirect(url.ToString());
        }
        #region EmailMethods
        //-///////////////----------------------------------Email Methods------------------------///////////////////
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

                    catch (SqlException ex)
                    {
                        dvemaillbl.Visible = true;
                        lblError.Text = "InsertEmailHOD" + ex.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
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
                        EmailSubject = "Quotation Approval Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has sent you a Quotation Approval Request against Form ID # " + lblMaxTransactionID.Text.ToString() + " for approval. <br><br> Your kind approval is required on the following URL: " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Inventory Management Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();

                    }
                }
            }

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "EmailWorkSendFirstApproval" + ex.ToString();
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
                        TransactionIDEmail = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Quotation Approval Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + 
                        " has sent you a Quotation Approval Request against Form ID # " + lblMaxTransactionID.Text.ToString() + 
                        " for approval. <br><br> Your kind approval is required on the following URL: <br><br><a href =" + url.ToString() + ">" + url.ToString() +
                        "</a> <br> <br> This is an auto-generated email from IS Dashboard, <br>you do not need to reply to this message.<br>" +
                        "<br>Inventory Management Application <br> Information Systems Dashboard";
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
            }

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
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
                        url = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "dashboard.itl.local");
                        urlMobile = Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Authority, "125.209.88.218:3110");
                        TransactionIDEmail = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Quotation Approval Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + "has sent you a Quotation Approval Request against Form ID # " + lblMaxTransactionID.Text.ToString() + " for approval. <br><br> Your kind approval is required on the following URL: " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Inventory Management Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        lblEmail.Text = "Inventory Adjustment Approval Request Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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
                            TransactionIDEmail = reader["TransactionID"].ToString();
                            FormCode = reader["FormID"].ToString();
                            UserName = reader["user_name"].ToString();
                            UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                            EmailSubject = "Quotation Approval Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                            EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() +
                            " has sent you a Quotation Approval Request against Form ID # " + lblMaxTransactionID.Text.ToString() +
                            " for approval. <br><br> You can create a Document No on the following URL: <br><br><a href =" + url.ToString() + ">" + url.ToString() + 
                            "</a> <br> <br> This is an auto-generated email from IS Dashboard, <br>you do not need to reply to this message.<br>" +
                            "<br>Inventory Management Application <br> Information Systems Dashboard";
                            SessionUser = Session["User_Name"].ToString();
                            DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            InsertEmail();
                            ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                            lblEmail.Text = "Quotation Approval Request Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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
                lblError.Text = "EmailWorkApproved" + ex.ToString();
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
                        TransactionIDEmail = reader["TransactionID"].ToString();
                        FormCode = reader["FormID"].ToString();
                        UserName = reader["user_name"].ToString();
                        UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                        EmailSubject = "Quotation Approval Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br> Quotation Approval Request – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been disapproved by  " + ViewState["SessionUser"].ToString() + " <br><br> The reason of rejection is given below you can review your form on following url: " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "<br> <br> <br><b>Reject Remarks: " + txtRemarksReview.Text +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                            "<br>Inventory Management Application  <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = "00"; // For Status Reject
                        lblEmail.Text = "Dead Stock Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " has been rejected by you";
                        lblEmail.Focus();
                        Page.MaintainScrollPositionOnPostBack = false;
                        Page.MaintainScrollPositionOnPostBack = true;
                    }
                }
            }

            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "EmailWorkReject" + ex.ToString();
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
                        EmailSubject = "Quotation Approval Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ",<br> <br>  Documnet No " + txtDocNo.Text + " has been  created by " + ViewState["SessionUser"].ToString().Replace(".", " ") + " Quotation Approval Request – Form ID # " + lblMaxTransactionID.Text.ToString() + " <br><br> The form can be reviewed at the following URL: " +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Inventory Management Application<br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                    }
                }

            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "EmailWorkSendMDA" + ex.ToString();
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

                        catch (SqlException ex)
                        {
                            dvemaillbl.Visible = true;
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

                    catch (SqlException ex)
                    {
                        dvemaillbl.Visible = true;
                        lblError.Text = "InsertEmail" + ex.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }    
        #endregion

       
    }
}