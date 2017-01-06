﻿using System.Web;
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

namespace DashboardProject.Modules.Annexure
{
    public partial class AssetWriteoffForm : System.Web.UI.Page
    {
        public string FormID = "AWOF501";
        private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        ComponentClass obj = new ComponentClass();
        public string TransatcionID = "";
        public string HierachyCategory = "";
        public string Status = "";
        public string Remarks = "";
        public string SessionID = "";
        public string url = "";
        public string urlMobile = "";
        public string TransactionIDEmail = "";
        public string FormCode = "";
        public string UserName = "";
        public string UserEmail = "";
        public string EmailSubject = "";
        public string EmailBody = "";
        public string SessionUser = "";
        public string DateTimeNow = "";



        protected void Page_Load(object sender, EventArgs e)
        {
            txtRemarks.BackColor = System.Drawing.Color.AliceBlue;
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
                txtRemarks.BackColor = System.Drawing.Color.AliceBlue;
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
                        grdDetail.Visible = true;
                        divEmail.Visible = false;
                        btnApproved.Visible = false;
                        btnReject.Visible = false;
                        GridView1.Visible = true;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        txtRemarks.Enabled = false;
                        txtRemarks.Visible = false;

                    }
                    if (((string)ViewState["HID"]) == "2")
                    {
                        grdDetail.Visible = true;
                        btnReject.Visible = true;
                        btnSave.Visible = false;
                        btnApproved.Visible = true;
                        btnReject.Visible = true;
                        btnCancel.Visible = false;
                        divEmail.Visible = false;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        txtRemarks.BackColor = System.Drawing.Color.AliceBlue;
                        txtRemarks.Enabled = true;
                        txtRemarks.Visible = true;
                    }
                    if (((string)ViewState["HID"]) == "4")
                    {
                        grdDetail.Visible = true;
                        btnReject.Visible = true;
                        btnSave.Visible = false;
                        btnApproved.Visible = false;
                        btnCancel.Visible = false;
                        divEmail.Visible = false;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        btnReviewed.Visible = true;
                        txtRemarks.BackColor = System.Drawing.Color.AliceBlue;
                        txtRemarks.Enabled = true;
                        txtRemarks.Visible = true;
                    }
                }
                else
                {
                    setinitialrow();
                    GetTransactionID();
                    getUser();
                    getUserHOD();
                    getUserDetail();
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region Grid_Working
        
    private void setinitialrow()
      {
        DataTable table = new DataTable("data");
        if (table.Rows.Count == 0)
        {
            table.Columns.Add("TransactionMain");
            table.Columns.Add("TransactionID");
            table.Columns.Add("AssetCode");
            table.Columns.Add("Description");
            table.Columns.Add("DateofWriteOff");
            table.Columns.Add("Reasonsjustificationforwriteoff");
            table.Columns.Add("Recommendationforscrapdisposal");
        }
        DataRow row = table.NewRow();
        table.Rows.Add(row);
        GridView1.DataSource = table;
        GridView1.DataBind();

    }

    protected void deleteRowEvent(object sender, EventArgs e)
    {
        DataTable data = new DataTable();
        LinkButton delete = (LinkButton)sender;
        GridViewRow container = (GridViewRow)delete.NamingContainer;

        if (data.Rows.Count == 0)
        {
            data.Columns.Add("TransactionMain");
            data.Columns.Add("TransactionID");
            data.Columns.Add("AssetCode");
            data.Columns.Add("Description");
            data.Columns.Add("DateofWriteOff");
            data.Columns.Add("Reasonsjustificationforwriteoff");
            data.Columns.Add("Recommendationforscrapdisposal");
        }
        foreach (GridViewRow row in GridView1.Rows)
        {
            TextBox txtAssetCode = (TextBox)row.FindControl("txtAssetCode");
            TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
            TextBox txtDateofWriteOff = (TextBox)row.FindControl("txtDateofWriteOff");
            TextBox txtReasonsjustificationforwriteoff = (TextBox)row.FindControl("txtReasonsjustificationforwriteoff");
            TextBox txtRecommendationforscrapdisposal = (TextBox)row.FindControl("txtRecommendationforscrapdisposal");


            data.Rows.Add(lblMaxTransactionNo.Text,
             lblMaxTransactionID.Text,
             txtAssetCode.Text,
             txtDescription.Text,
             txtDateofWriteOff.Text,
             txtReasonsjustificationforwriteoff.Text,
             txtRecommendationforscrapdisposal.Text);
        }
        DataRow newrow = data.NewRow();
        data.Rows.RemoveAt(container.RowIndex);
        GridView1.DataSource = data;
        ViewState["GridView1"] = data;
        GridView1.DataBind();
        setData(data);

        if (data.Rows.Count == 0)
        {
            setinitialrow();
            //bindGrid();
        }
    }

    protected void AddRowEvent(object sender, EventArgs e)
    {
        lblUpError.Text = "";
        error.Visible = false;
        lblmessage.Text = "";
        error.Visible = false;
        DataTable data = new DataTable();
        if (data.Rows.Count == 0)
        {
            data.Columns.Add("TransactionMain");
            data.Columns.Add("TransactionID");
            data.Columns.Add("AssetCode");
            data.Columns.Add("Description"); 
            data.Columns.Add("DateofWriteOff");
            data.Columns.Add("Reasonsjustificationforwriteoff");
            data.Columns.Add("Recommendationforscrapdisposal");

        }

        foreach (GridViewRow row in GridView1.Rows)
        {
            TextBox txtAssetCode = (TextBox)row.FindControl("txtAssetCode");
            TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
            TextBox txtDateofWriteOff = (TextBox)row.FindControl("txtDateofWriteOff");
            TextBox txtReasonsjustificationforwriteoff = (TextBox)row.FindControl("txtReasonsjustificationforwriteoff");
            TextBox txtRecommendationforscrapdisposal = (TextBox)row.FindControl("txtRecommendationforscrapdisposal");



            data.Rows.Add(lblMaxTransactionNo.Text,
            lblMaxTransactionID.Text,
             txtAssetCode.Text,
             txtDescription.Text,
             txtDateofWriteOff.Text,
             txtReasonsjustificationforwriteoff.Text,
             txtRecommendationforscrapdisposal.Text);
        }

        DataRow newrow = data.NewRow();
        data.Rows.InsertAt(newrow, GridView1.Rows.Count + 1);
        GridView1.DataSource = data;
        ViewState["GridView1"] = data;
        GridView1.DataBind();
        setData(data);

    }

    protected void setData(DataTable table)//, List<string> List1, List<string> List2
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            TextBox txtAssetCode = (TextBox)row.FindControl("txtAssetCode");
            txtAssetCode.Text = table.Rows[row.RowIndex]["AssetCode"].ToString();

            TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
            txtDescription.Text = table.Rows[row.RowIndex]["Description"].ToString();

            TextBox txtDateofWriteOff = (TextBox)row.FindControl("txtDateofWriteOff");
            txtDateofWriteOff.Text = table.Rows[row.RowIndex]["DateofWriteOff"].ToString();

            TextBox txtReasonsjustificationforwriteoff = (TextBox)row.FindControl("txtReasonsjustificationforwriteoff");
            txtReasonsjustificationforwriteoff.Text = table.Rows[row.RowIndex]["Reasonsjustificationforwriteoff"].ToString();

            TextBox txtRecommendationforscrapdisposal = (TextBox)row.FindControl("txtRecommendationforscrapdisposal");
            txtRecommendationforscrapdisposal.Text = table.Rows[row.RowIndex]["Recommendationforscrapdisposal"].ToString();

        }
    }

    protected void Grid1datainsert()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["GridView1"];
        if (dtCurrentTable != null)
        {
            string consString = ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SYS_ createAssestsWriteOffForm"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtCurrentTable.Rows[i][1] == DBNull.Value)
                            dtCurrentTable.Rows[i].Delete();
                    }
                    //dtCurrentTable.AcceptChanges();
                    cmd.Parameters.AddWithValue("@tbl_AssetsWriteOffFormMain", dtCurrentTable);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
        else
        {
            lblUpError.Text = "Please fill the required data in list.";
            error.Visible = true;
            Page.MaintainScrollPositionOnPostBack = false;
            return;
        }
    }

    #endregion
//------////////////////////////////////Methods//////////////////////////////////////////////////
    private void GetTransactionID()
    {
        ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
        lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
        ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();

        cmd.CommandText = "";
        cmd.CommandText = @"SELECT COALESCE(MAX(TransactionID), 0)  +1 as TransactionID from tbl_AssetsWriteOffFormDetail";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;
        adp.SelectCommand = cmd;
        adp.Fill(ds, "TransactionID");
        lblMaxTransactionID.Text = ds.Tables["TransactionID"].Rows[0]["TransactionID"].ToString();
    }

    private void GetHarcheyID()
    {
        ds = obj.GetHarachyCustomerMaster(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
        if (ds.Tables["HID"].Rows.Count > 0)
        {
            lblMaxTransactionID.Text = ds.Tables["HID"].Rows[0]["TransactionID"].ToString();
            ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
        }
    }

    private void GetStatusHierachyCategoryControls()
    {
        ds = obj.GetStatusHierachyCategory(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
        if (ds.Tables["StatusHierachyCategory"].Rows.Count > 0)
        {
            ViewState["StatusHierachyCategory"] = ds.Tables["StatusHierachyCategory"].Rows[0]["Status"].ToString();
        }
        if (((string)ViewState["StatusHierachyCategory"]) == "01" || ((string)ViewState["StatusHierachyCategory"]) == "02" || ((string)ViewState["StatusHierachyCategory"]) == "03" || ((string)ViewState["StatusHierachyCategory"]) == "04")
        {
            btnSave.Enabled = false;
            btnApproved.Enabled = false;
            btnReviewed.Enabled = false;
            btnCancel.Enabled = false;
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

        }
        ds.Clear();
        ds = obj.InsertsysApplicationStatus(FormID.ToString(), TransatcionID.ToString(), ViewState["HID"].ToString(), Session["User_Name"].ToString(), Status.ToString(), Remarks.ToString().Trim());
    }

  private void getUser()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdgetData = new SqlCommand())
                {
                    cmdgetData.CommandText = "SELECT user_name,DisplayName FROM tbluserApprovalHOD where FormID = 'AWOF501' and Designation = 'COO'";
                    cmdgetData.CommandType = CommandType.Text;
                    cmdgetData.Connection = connection;
                    connection.Open();
                    ddlCOO.DataSource = cmdgetData.ExecuteReader();
                    ddlCOO.DataTextField = "DisplayName";
                    ddlCOO.DataValueField = "user_name";
                    ddlCOO.DataBind();
                    ddlCOO.Items.Insert(0, new ListItem("------Select------", "0"));
                    connection.Close();

                    cmdgetData.CommandText = "SELECT user_name,DisplayName FROM tbluserMDA where FormName = 'AWF'";
                    cmdgetData.CommandType = CommandType.Text;
                    cmdgetData.Connection = connection;
                    connection.Open();

                    ddlReviewer.DataSource = cmdgetData.ExecuteReader();
                    ddlReviewer.DataTextField = "DisplayName";
                    ddlReviewer.DataValueField = "user_name";
                    ddlReviewer.DataBind();
                    ddlReviewer.Items.Insert(0, new ListItem("------Select------", "0"));
                    connection.Close();

                    cmdgetData.CommandText = "SELECT user_name,DisplayName FROM tbluserApprovalHOD where FormID = 'AWOF501' and Designation = 'CFO'";
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

    protected void getDataWhenQueryStringPass()
    {
        string TI = Request.QueryString["TransactionNo"].ToString();
        cmd.CommandText = "";
        cmd.CommandText = @"SELECT T2.TransactionID as [Transaction ID]
      ,T2.AssetCode as [Asset Code]
      ,T2.Description as [Description and Category of Assets]
      ,T2.DateofPurchase as [Date of Purchase]
      ,T2.Cost
      ,T2.AccumulatedDepreciation as [Accumulated Depreciation]
      ,T2.NetBookValue as [Net Book Value]
      ,T2.DateofWriteOff as [Date of Write Off]
      ,T2.Reasonsjustificationforwriteoff as [Reasons / justification for writeoff]
      ,T2.Recommendationforscrapdisposal as [Recommendation for scrap / disposal]
      FROM tbl_AssetsWriteOffFormDetail T1
      inner join tbl_AssetWriteOffForm T2
      on T1.TransactionID = T2.TransactionID
      where T1.TransactionMain = @TI";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@TI", TI.ToString());
        adp.SelectCommand = cmd;
        adp.Fill(ds, "Data");
        if (ds.Tables["Data"].Rows.Count > 0)
        {
            lblMaxTransactionID.Text = ds.Tables["Data"].Rows[0]["Transaction ID"].ToString();
            grdDetail.DataSource = ds.Tables["Data"];
            grdDetail.DataBind();
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

//-//////////////////////////////////////Button Events/////////////////////////////////////////////
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCurrentTable = (DataTable)ViewState["GridView1"];
                if (dtCurrentTable != null)
                {
                    lblUpError.Text = "";
                    error.Visible = false;
                    lblmessage.Text = "";
                    error.Visible = false;

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
                        cmd.CommandText = "SP_SYS_AssestsWriteOffFormDetail";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                        cmd.Parameters.AddWithValue("@APPROVAL", finale.ToString());
                        cmd.Parameters.AddWithValue("@Reviwer", Reviwer.ToString());
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["User_Name"].ToString());
                        cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                        cmd.Parameters.AddWithValue("@Remarks", txtRemarks.ToString());

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
                            Email_WorkingEmailWorkSendFirstReviwer();
                            Grid1datainsert();
                            lblmessage.Focus();
                            error.Visible = false;
                            Page.MaintainScrollPositionOnPostBack = false;
                            GetTransactionID();
                            setinitialrow();
                            lblmessage.Text = "";
                        }
                    }
                }
                else
                {
                    lblUpError.Text = "Please fill the required data in list.";
                    error.Visible = true;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            EmailWorkSendToAllfromApproval();
            ApplicationStatus();
            BindsysApplicationStatus();
            GetStatusHierachyCategoryControls();
        }

        protected void btnReviewed_Click(object sender, EventArgs e)
        {
            EmailWorkSendToAllfromReviwer();
            ApplicationStatus();
            BindsysApplicationStatus();
            GetStatusHierachyCategoryControls();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputs(Page.Controls);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                EmailWorkSendFirstApprovalOnRejection();
                ApplicationStatus();
                BindsysApplicationStatus();
                GetStatusHierachyCategoryControls();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }

        }
        #region Email

        private void Email_WorkingEmailWorkSendFirstReviwer()
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
                        EmailSubject = "Asset Write Off Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " have sent you a Asset Write Off Form Request against  Form ID # " + lblMaxTransactionID.Text + " for approval. <br><br> Your kind approval is required" +
                        "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Asset WriteOff Application <br> Information Systems Dashboard";
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
                        EmailSubject = "Asset Write Off Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br>  Your Asset Write Off Form Request against  Form ID # " + lblMaxTransactionID.Text + " has been disapproved by  " + ViewState["SessionUser"].ToString() + " <br><br> The reason of rejection is given below:" + "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "<br> <br> <br>Reject Remarks: " + txtRemarks.Text +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Asset Write Off Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblEmail.Text = "Asset Write Off Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been rejected by you";
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
                        EmailSubject = "Asset Write Off Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br>  Your Asset Write Off Form Request against  Form ID # " + lblMaxTransactionID.Text + " has been reviewed by  " + ViewState["SessionUser"].ToString() + "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                        "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " +
                        "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                        "<br>Asset Write Off Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblmessage.Text = "Asset Write Off Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been reviewed by you";
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

        private void EmailWorkSendToAllfromApproval()
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
                        EmailSubject = "Asset Write Off Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                        EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br>  Asset Write Off Form Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by " + ViewState["SessionUser"].ToString() + " <br><br> You are requested to Approve Asset Write Off Form Request" +
                         "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>" +
                         "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() + "</a> <br> <br> " + "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                         "<br>Asset Write Off Application <br> Information Systems Dashboard";
                        SessionUser = Session["User_Name"].ToString();
                        DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        InsertEmail();
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                        lblEmail.Text = "Asset Write Off Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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
                            EmailSubject = "Asset Write Off Form Request against  – Form ID # " + lblMaxTransactionID.Text + "";
                            EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + ",<br> <br>  Asset Write Off Form Request against  Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr." + ViewState["SessionUser"].ToString() + " <br><br> > You are requested to review the Asset Write Off Form information " + 
                                "The form can be reviewed at the following URL within ITL Network:<br><a href =" + url.ToString() + ">" + url.ToString() + 
                                "</a> <br> <br>" + "To access the form outside ITL network, please use the following URL:<br><a href =" + urlMobile.ToString() + ">" + urlMobile.ToString() +
                                "</a> <br> <br> " + "This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                                "<br>Asset Write Off Application <br> Information Systems Dashboard";
                            SessionUser = Session["User_Name"].ToString();
                            DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            InsertEmail();
                            ViewState["Status"] = HierachyCategoryStatus.ToString();
                            lblEmail.Text = "Asset Write Off Form Request against  – Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
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
    
    }
}