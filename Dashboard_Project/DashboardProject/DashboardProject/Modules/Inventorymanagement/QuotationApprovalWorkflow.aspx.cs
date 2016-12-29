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
                        GetSockDetail();
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
            for (int i = 0; i < ddlNotification.Items.Count; i++)
            {
                ddlNotification.Items[i].Selected = true;
                ddlNotification.Items[i].Attributes.Add("disabled", "disabled");
            }
       }
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
        /////////////////////////////-----------------/BindUser Method-----------------------///////////////////////////////
        private void BindUser()
        {
            try
            {
                cmd.CommandText = "SELECT * FROM tblEmailSequenceWise where FormID = @FormID order by Sequance asc";
                //cmd.CommandText = "SELECT * FROM tbluser where user_name = 'adnan.yousufzai'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                conn.Open();
                ddlNotification.DataSource = cmd.ExecuteReader();
                cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                ddlNotification.DataTextField = "DisplayName";
                ddlNotification.DataValueField = "user_name";
                ddlNotification.DataBind();
                conn.Close();
                for (int i = 0; i < ddlNotification.Items.Count; i++)
                {
                    ddlNotification.Items[i].Selected = true;
                    ddlNotification.Items[i].Attributes.Add("disabled", "disabled");
                }

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
                FilePath = "~/DashboardDocument/InventoryAdjustment/" + lblFileName.Text.ToString();
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
        //-///////////////----------------------------------Remove Button------------------------///////////////////

     }
}