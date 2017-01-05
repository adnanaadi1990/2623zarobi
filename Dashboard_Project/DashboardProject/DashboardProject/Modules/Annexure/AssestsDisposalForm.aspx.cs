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

namespace DashboardProject.Modules.Annexure
{

    public partial class AssestsDisposalForm : System.Web.UI.Page
    {
        public string FormID = "ADF501";
        private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
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

                if (Request.QueryString["TransactionNo"] != null)
                {
                    getDataWhenQueryStringPass();
                    GetHarcheyID();
                    GetStatusHierachyCategoryControls();
                    BindsysApplicationStatus();
                    if (((string)ViewState["HID"]) == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;

                        grdDetail.Visible = true;
                        divEmail.Visible = false;
                        btnApproved.Visible = true;
                        LinkButton1.Visible = false;
                        btnReject.Visible = false;
                        GridView1.Visible = true;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;

                    }
                    if (((string)ViewState["HID"]) == "2")
                    {
                        grdDetail.Visible = true;
                        btnReject.Visible = true;
                        btnSave.Visible = false;
                        btnApproved.Visible = true;
                        btnCancel.Visible = false;
                        divEmail.Visible = false;
                        LinkButton1.Visible = true;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                    }
                    if (((string)ViewState["HID"]) == "3")
                    {
                        grdDetail.Visible = true;
                        btnReject.Visible = true;
                        btnSave.Visible = false;
                        btnApproved.Visible = false;
                        btnCancel.Visible = false;
                        divEmail.Visible = false;
                        LinkButton1.Visible = true;
                        dvFormID.Visible = true;
                        dvTransactionNo.Visible = false;
                        btnReviwer.Visible = true;
                    }
                }
                else
                {
                    setinitialrow();
                    GetTransactionID();
                    getUser();
                }
            }
        }
        private void GetTransactionID()
        {
            ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
            lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
            ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();

            cmd.CommandText = "";
            cmd.CommandText = @"SELECT COALESCE(MAX(TransactionID), 0)  +1 as TransactionID from tbl_AssestsDisposalFormDetail";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            adp.Fill(ds, "TransactionID");
            lblMaxTransactionID.Text = ds.Tables["TransactionID"].Rows[0]["TransactionID"].ToString();
        }

        private void GetHarcheyID()
        {
            ds = obj.GetHarachyCustomerMaster(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
            lblMaxTransactionID.Text = ds.Tables["HID"].Rows[0]["TransactionID"].ToString();
            ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
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
            ds = obj.InsertsysApplicationStatus(FormID.ToString(), TransatcionID.ToString(), ViewState["HID"].ToString(), Session["User_Name"].ToString(), Status.ToString(), Remarks.ToString().Trim());
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
                LinkButton1.Attributes.Add("disabled", "true");
                btnReviwer.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        private void getUser()
        {
            cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserApproval where FormName = 'ADF'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlApproval1.DataSource = cmd.ExecuteReader();
            ddlApproval1.DataTextField = "DisplayName";
            ddlApproval1.DataValueField = "user_name";
            ddlApproval1.DataBind();
            ddlApproval1.Items.Insert(0, new ListItem("------Select------", "0"));

            conn.Close();
            conn.Open();
            ddlApproval2.DataSource = cmd.ExecuteReader();
            ddlApproval2.DataTextField = "DisplayName";
            ddlApproval2.DataValueField = "user_name";
            ddlApproval2.DataBind();
            conn.Close();
            ddlApproval2.Items.Insert(0, new ListItem("------Select------", "0"));
            conn.Open();
            ddlApproval3.DataSource = cmd.ExecuteReader();
            ddlApproval3.DataTextField = "DisplayName";
            ddlApproval3.DataValueField = "user_name";
            ddlApproval3.DataBind();
            conn.Close();
            ddlApproval3.Items.Insert(0, new ListItem("------Select------", "0"));
        }

        #region Grid_Working

        private void bindGrid()
        {
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

        private void setinitialrow()
        {
            DataTable table = new DataTable("data");
            if (table.Rows.Count == 0)
            {
                table.Columns.Add("TransactionMain");
                table.Columns.Add("TransactionID");
                table.Columns.Add("AssetCode");
                table.Columns.Add("Description");
                table.Columns.Add("DateofPurchase");
                table.Columns.Add("Cost");
                table.Columns.Add("AccumulatedDepreciation");
                table.Columns.Add("NetBookValue");
                table.Columns.Add("DateofDisposal");
                table.Columns.Add("ModeofDisposal");
                table.Columns.Add("SalesProceeds");
                table.Columns.Add("GainOrLossonDisposal");
                table.Columns.Add("ReasonsJustificationforDisposal");
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
            List<string> List1 = new List<string>();
            List<string> List2 = new List<string>();

            if (data.Rows.Count == 0)
            {
                data.Columns.Add("TransactionMain");
                data.Columns.Add("TransactionID");
                data.Columns.Add("AssetCode");
                data.Columns.Add("Description");
                data.Columns.Add("DateofPurchase");
                data.Columns.Add("Cost");
                data.Columns.Add("AccumulatedDepreciation");
                data.Columns.Add("NetBookValue");
                data.Columns.Add("DateofDisposal");
                data.Columns.Add("ModeofDisposal");
                data.Columns.Add("SalesProceeds");
                data.Columns.Add("GainOrLossonDisposal");
                data.Columns.Add("ReasonsJustificationforDisposal");
                data.Columns.Add("CreatedBy");
                data.Columns.Add("CreateDate");
                data.Columns.Add("Approver");
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox txtAssetCode = (TextBox)row.FindControl("txtAssetCode");
                TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                TextBox txtDateofPurchase = (TextBox)row.FindControl("txtDateofPurchase");
                TextBox txtCost = (TextBox)row.FindControl("txtCost");
                TextBox txtAccumulatedDepreciation = (TextBox)row.FindControl("txtAccumulatedDepreciation");
                TextBox txtNetBookValue = (TextBox)row.FindControl("txtNetBookValue");
                TextBox txtDateofDisposal = (TextBox)row.FindControl("txtDateofDisposal");
                TextBox txtModeofDisposal = (TextBox)row.FindControl("txtModeofDisposal");
                TextBox txtSalesProceeds = (TextBox)row.FindControl("txtSalesProceeds");
                TextBox txtGainOrLossonDisposal = (TextBox)row.FindControl("txtGainOrLossonDisposal");
                TextBox txtReasonsJustificationforDisposal = (TextBox)row.FindControl("txtReasonsJustificationforDisposal");

                data.Rows.Add("1",
                 txtAssetCode.Text,
                 txtDescription.Text,
                 txtDateofPurchase.Text.ToString(),
                 txtCost.Text,
                 txtAccumulatedDepreciation.Text,
                 txtNetBookValue.Text,
                 txtDateofDisposal.Text,
                 txtModeofDisposal.Text,
                 txtSalesProceeds.Text,
                 txtGainOrLossonDisposal.Text,
                 txtReasonsJustificationforDisposal.Text);
            }
            DataRow newrow = data.NewRow();
            data.Rows.RemoveAt(container.RowIndex);
            GridView1.DataSource = data;
            GridView1.DataBind();
            setData(data);

            if (data.Rows.Count == 0)
            {
                setinitialrow();
                bindGrid();
            }
        }

        protected void AddRowEvent(object sender, EventArgs e)
        {
            DataTable data = new DataTable();
            if (data.Rows.Count == 0)
            {
                data.Columns.Add("TransactionMain");
                data.Columns.Add("TransactionID");
                data.Columns.Add("AssetCode");
                data.Columns.Add("Description");
                data.Columns.Add("DateofPurchase");
                data.Columns.Add("Cost");
                data.Columns.Add("AccumulatedDepreciation");
                data.Columns.Add("NetBookValue");
                data.Columns.Add("DateofDisposal");
                data.Columns.Add("ModeofDisposal");
                data.Columns.Add("SalesProceeds");
                data.Columns.Add("GainOrLossonDisposal");
                data.Columns.Add("ReasonsJustificationforDisposal");
            }

            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox txtAssetCode = (TextBox)row.FindControl("txtAssetCode");
                TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                TextBox txtDateofPurchase = (TextBox)row.FindControl("txtDateofPurchase");
                TextBox txtCost = (TextBox)row.FindControl("txtCost");
                TextBox txtAccumulatedDepreciation = (TextBox)row.FindControl("txtAccumulatedDepreciation");
                TextBox txtNetBookValue = (TextBox)row.FindControl("txtNetBookValue");
                TextBox txtDateofDisposal = (TextBox)row.FindControl("txtDateofDisposal");
                TextBox txtModeofDisposal = (TextBox)row.FindControl("txtModeofDisposal");
                TextBox txtSalesProceeds = (TextBox)row.FindControl("txtSalesProceeds");
                TextBox txtGainOrLossonDisposal = (TextBox)row.FindControl("txtGainOrLossonDisposal");
                TextBox txtReasonsJustificationforDisposal = (TextBox)row.FindControl("txtReasonsJustificationforDisposal");


                data.Rows.Add(lblMaxTransactionNo.Text,
                    lblMaxTransactionID.Text,
                 txtAssetCode.Text,
                 txtDescription.Text,
                 txtDateofPurchase.Text.ToString(),
                 txtCost.Text,
                 txtAccumulatedDepreciation.Text,
                 txtNetBookValue.Text,
                 txtDateofDisposal.Text,
                 txtModeofDisposal.Text,
                 txtSalesProceeds.Text,
                 txtGainOrLossonDisposal.Text,
                 txtReasonsJustificationforDisposal.Text);
            }
            DataRow newrow = data.NewRow();
            data.Rows.InsertAt(newrow, GridView1.Rows.Count + 1);
            GridView1.DataSource = data;
            ViewState["Grid1"] = data;
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

                TextBox txtDateofPurchase = (TextBox)row.FindControl("txtDateofPurchase");
                txtDateofPurchase.Text = table.Rows[row.RowIndex]["DateofPurchase"].ToString();

                TextBox txtCost = (TextBox)row.FindControl("txtCost");
                txtCost.Text = table.Rows[row.RowIndex]["Cost"].ToString();

                TextBox txtAccumulatedDepreciation = (TextBox)row.FindControl("txtAccumulatedDepreciation");
                txtAccumulatedDepreciation.Text = table.Rows[row.RowIndex]["AccumulatedDepreciation"].ToString();

                TextBox txtNetBookValue = (TextBox)row.FindControl("txtNetBookValue");
                txtNetBookValue.Text = table.Rows[row.RowIndex]["NetBookValue"].ToString();

                TextBox txtDateofDisposal = (TextBox)row.FindControl("txtDateofDisposal");
                txtDateofDisposal.Text = table.Rows[row.RowIndex]["DateofDisposal"].ToString();


                TextBox txtModeofDisposal = (TextBox)row.FindControl("txtModeofDisposal");
                txtModeofDisposal.Text = table.Rows[row.RowIndex]["ModeofDisposal"].ToString();

                TextBox txtSalesProceeds = (TextBox)row.FindControl("txtSalesProceeds");
                txtSalesProceeds.Text = table.Rows[row.RowIndex]["SalesProceeds"].ToString();

                TextBox txtGainOrLossonDisposal = (TextBox)row.FindControl("txtGainOrLossonDisposal");
                txtGainOrLossonDisposal.Text = table.Rows[row.RowIndex]["GainOrLossonDisposal"].ToString();

                TextBox txtReasonsJustificationforDisposal = (TextBox)row.FindControl("txtReasonsJustificationforDisposal");
                txtReasonsJustificationforDisposal.Text = table.Rows[row.RowIndex]["ReasonsJustificationforDisposal"].ToString();
            }
        }

        protected void Grid1datainsert()
        {
            DataTable dtCurrentTable = (DataTable)ViewState["Grid1"];
            if (dtCurrentTable != null)
            {
                string consString = ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_SYS_ createAssestsDisposalForm"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                        {
                            if (dtCurrentTable.Rows[i][1] == DBNull.Value)
                                dtCurrentTable.Rows[i].Delete();
                        }
                        //dtCurrentTable.AcceptChanges();
                        cmd.Parameters.AddWithValue("@tbl_AssestsDisposalForm", dtCurrentTable);
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

        #region Email_Working

        private void EmailWorkSendFirstApproval()
        {
            try
            {
                string HierachyCategoryStatus = "02"; // Allow based on reqierment if there is No MDA if other wise allow "4"//
                ds = obj.MailForwardUserToApprover(lblMaxTransactionID.Text, FormID.ToString());

                if (ds.Tables["MailForwardUserToApprover"].Rows.Count > 0)
                {
                    DataTableReader reader = ds.CreateDataReader();
                    while (reader.Read())
                    {
                        var to = new MailAddress(reader["user_email"].ToString(),
                                                   reader["user_name"].ToString());
                        ViewState["UserName"] = reader["RoughtingUserID"].ToString();
                        string aa = Request.CurrentExecutionFilePath;
                        string ab = HttpContext.Current.Request.Url.Authority;
                        string aaa = ab + aa;

                        using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", reader["user_email"].ToString()))
                        {

                            mm.Subject = "Assets Dispoal Form Request – Form ID # " + lblMaxTransactionID.Text + "";
                            //,<br> <br>   I have Following request againts " + " TransactionNo " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                            mm.Body = ViewState["UserName"].ToString(); //<a href= " + "" + url.ToString() + "?SMCode=" + txtSMC.Text + ">  " + url.ToString() + "?SMCode=" + txtSMC.Text + "</a>

                            string url = HttpContext.Current.Request.Url.AbsoluteUri + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                            mm.Body = "Dear Mr " + "" + ViewState["UserName"] + ",<br> <br>   I have sent you a Assets Dispoal Form Request against Form ID # " + lblMaxTransactionID.Text + " for approval. <br><br> Your kind approval is required on the following URL: <br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                            "<br>Annexure Application <br> Information Systems Dashboard";
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
                            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                            ViewState["Status"] = HierachyCategoryStatus.ToString();
                            GetStatusHierachyCategoryControls();
                        }
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
                string HierachyCategoryStatus = "02"; // Allow based on reqierment if there is No MDA if other wise allow "4"//
                ds = obj.MailForwardToUserOnRejection(lblMaxTransactionID.Text, FormID.ToString());

                if (ds.Tables["MailForwardToUserOnRejection"].Rows.Count > 0)
                {
                    DataTableReader reader = ds.CreateDataReader();
                    while (reader.Read())
                    {
                        var to = new MailAddress(reader["user_email"].ToString(),
                                                   reader["user_name"].ToString());
                        ViewState["UserName"] = reader["RoughtingUserID"].ToString();
                        string aa = Request.CurrentExecutionFilePath;
                        string ab = HttpContext.Current.Request.Url.Authority;
                        string aaa = ab + aa;

                        using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", reader["user_email"].ToString()))
                        {

                            mm.Subject = "Assets Dispoal Form Request – Form ID # " + lblMaxTransactionID.Text + "";
                            //,<br> <br>   I have Following request againts " + " TransactionNo " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                            mm.Body = ViewState["UserName"].ToString(); //<a href= " + "" + url.ToString() + "?SMCode=" + txtSMC.Text + ">  " + url.ToString() + "?SMCode=" + txtSMC.Text + "</a>

                            string url = HttpContext.Current.Request.Url.AbsoluteUri + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                            mm.Body = "Dear Mr " + "" + ViewState["UserName"] + ",<br> <br>  Your Assets Dispoal Form Request Request against Form ID # " + lblMaxTransactionID.Text + " has been disapproved by  " + Session["User_Name"].ToString() + " <br><br> You can review the reason of rejection using the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> " +
                            "<br> <br> <br>Reject Remarks: " + txtRemarks.Text +
                            " <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                           "<br><br>Annexure Application <br> Information Systems Dashboard";

                            mm.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();

                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential NetworkCred = new NetworkCredential("dashboard@internationaltextile.com", "itldashboard$$");
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                            lblmessage.Text = "Assets Dispoal Form Request against Form ID # " + lblMaxTransactionID.Text.ToString() + " has been rejected by you";
                            lblmessage.ForeColor = System.Drawing.Color.Green;
                            conn.Close();
                            sucess.Visible = true;
                            error.Visible = false;
                            lblmessage.Focus();
                            Page.MaintainScrollPositionOnPostBack = false;
                            ViewState["Status"] = HierachyCategoryStatus.ToString();

                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "Email User" + ex.ToString();
            }


        }

        private void EmailWorkSendToApprovalFromApprover()
        {// 
            ds.Clear();
            string HierachyCategoryStatus = "02"; // Allow based on reqierment if there is No MDA if other wise allow "4"//
            ds = obj.MailForwardFormApprover(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
            string Value = ds.Tables["MailForwardFormApprover"].Rows[0]["HierachyCategory"].ToString();
            DataTableReader reader = ds.CreateDataReader();
            if (ds.Tables["MailForwardFormApprover"].Rows.Count > 0 && Value == "2")
            {
                while (reader.Read())
                {
                    var to = new MailAddress(reader["user_email"].ToString(),
                                               reader["user_name"].ToString());
                    ViewState["UserName"] = reader["RoughtingUserID"].ToString();
                    string aa = Request.CurrentExecutionFilePath;
                    string ab = HttpContext.Current.Request.Url.Authority;
                    string aaa = ab + aa;

                    using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", reader["user_email"].ToString()))
                    {

                        mm.Subject = "Assets Dispoal Form Request – Form ID # " + lblMaxTransactionID.Text + "";
                        //,<br> <br>   I have Following request againts " + " TransactionNo " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                        mm.Body = ViewState["UserName"].ToString(); //<a href= " + "" + url.ToString() + "?SMCode=" + txtSMC.Text + ">  " + url.ToString() + "?SMCode=" + txtSMC.Text + "</a>
                        string MeterialCode = lblMaxTransactionID.Text.ToString();
                        string url = Request.Url.ToString();
                        mm.Body = "Dear Mr " + "" + ViewState["UserName"] + ",<br> <br> Assets Dispoal Form Request against Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr. " + Session["User_Name"].ToString() + " <br><br> You are requested to Approve the Assets Dispoal Form Request on the following URL: <br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message." +
                        "<br><br>Annexure Application Form Application<br> Information Systems Dashboard";

                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("dashboard@internationaltextile.com", "itldashboard$$");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                        lblEmail.Text = "Assets Dispoal Form Request against Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
                        ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved

                    }

                }

            }
            else
            {
                if (ds.Tables["MailForwardFormApprover"].Rows.Count > 0)
                {
                    while (reader.Read())
                    {
                        var to = new MailAddress(reader["user_email"].ToString(),
                                                   reader["user_name"].ToString());
                        ViewState["UserName"] = reader["RoughtingUserID"].ToString();
                        string aa = Request.CurrentExecutionFilePath;
                        string ab = HttpContext.Current.Request.Url.Authority;
                        string aaa = ab + aa;

                        using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", reader["user_email"].ToString()))
                        {

                            mm.Subject = "Assets Dispoal Form Request – Form ID # " + lblMaxTransactionID.Text + "";
                            //,<br> <br>   I have Following request againts " + " Meterial No " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                            mm.Body = ViewState["UserName"].ToString(); //<a href= " + "" + url.ToString() + "?SMCode=" + txtSMC.Text + ">  " + url.ToString() + "?SMCode=" + txtSMC.Text + "</a>
                            string MeterialCode = lblMaxTransactionID.Text.ToString();
                            string url = Request.Url.ToString();
                            mm.Body = "Dear Mr " + "" + ViewState["UserName"] + ",<br> <br> Assets Dispoal Form Request against Form ID #  " + lblMaxTransactionID.Text.ToString() + " has been approved by Mr. " + Session["User_Name"].ToString() + " <br><br> You are requested to review the Assets Dispoal Form information on the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                             "<br>Annexure Application Form Application<br> Information Systems Dashboard";
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
                            //   ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup2();", true);
                            lblEmail.Text = "Assets Dispoal Form Request against Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
                            ViewState["Status"] = HierachyCategoryStatus.ToString(); // For Status Approved
                        }

                    }

                }
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
                    DataTableReader reader = ds.CreateDataReader();
                    while (reader.Read())
                    {
                        var to = new MailAddress(reader["user_email"].ToString(),
                                                   reader["user_name"].ToString());
                        ViewState["UserName"] = reader["RoughtingUserID"].ToString();
                        string aa = Request.CurrentExecutionFilePath;
                        string ab = HttpContext.Current.Request.Url.Authority;
                        string aaa = ab + aa;

                        using (MailMessage mm = new MailMessage("dashboard@internationaltextile.com", reader["user_email"].ToString()))
                        {

                            mm.Subject = "Assets Dispoal Form Request – Form ID # " + lblMaxTransactionID.Text + "";
                            //,<br> <br>   I have Following request againts " + " TransactionNo " + txtSMC.Text + " has been send. <br> Please See the following page ID:  " + "" + ViewState["FormId"].ToString() + " Form Name " + "" + ViewState["FormName"].ToString() + "URL of Page :<a href= " + "" + url.ToString() + "?MeterialNo=" + txtSMC.Text + ">  " + url.ToString() + "?MeterialNo=" + txtSMC.Text + "</a><br>  For more assistment feel free to cordinate. <br><br><br>     Regard<br> ABCDEG ";
                            mm.Body = ViewState["UserName"].ToString(); //<a href= " + "" + url.ToString() + "?SMCode=" + txtSMC.Text + ">  " + url.ToString() + "?SMCode=" + txtSMC.Text + "</a>

                            string url = HttpContext.Current.Request.Url.AbsoluteUri + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                            mm.Body = "Dear Mr " + "" + ViewState["UserName"] + ",<br> <br>  Your Assets Dispoal Form Request Request against Form ID # " + lblMaxTransactionID.Text + " has been disapproved by  " + Session["User_Name"].ToString() + " <br><br> You can review the reason of rejection using the following URL:<br>  <a href =" + url.ToString() + ">" + url.ToString() + "</a> " +
                            "<br> <br> <br>Reject Remarks: " + txtRemarks.Text +
                            " <br> <br> This is an auto-generated email from IS Dashboard, you do not need to reply to this message.<br><br>" +
                           "<br><br>Annexure Application <br> Information Systems Dashboard";

                            mm.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();

                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential NetworkCred = new NetworkCredential("dashboard@internationaltextile.com", "itldashboard$$");
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                            lblmessage.Text = "Assets Dispoal Form Request against Form ID # " + lblMaxTransactionID.Text.ToString() + " has been approved by you";
                            lblmessage.ForeColor = System.Drawing.Color.Green;
                            conn.Close();
                            sucess.Visible = true;
                            error.Visible = false;
                            lblmessage.Focus();
                            Page.MaintainScrollPositionOnPostBack = false;

                            ViewState["Status"] = HierachyCategoryStatus.ToString();
                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                lblError.Text = "Email User" + ex.ToString();
            }


        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dtCurrentTable = (DataTable)ViewState["Grid1"];
                if (dtCurrentTable != null)
                {
                    lblUpError.Text = "";
                    error.Visible = false;
                    lblmessage.Text = "";
                    error.Visible = false;
                    if (ddlApproval1.SelectedValue == "0")
                    {
                        lblUpError.Text = "Select any Chief Opreating Officer!.";
                        error.Visible = true;
                        ddlApproval1.BackColor = System.Drawing.Color.Red;
                        Page.MaintainScrollPositionOnPostBack = false;
                        return;
                    }
                    else if (ddlApproval2.SelectedValue == "0")
                    {
                        lblUpError.Text = "Select Chief Procurement Officer!.";
                        error.Visible = true;
                        ddlApproval2.BackColor = System.Drawing.Color.Red;
                        Page.MaintainScrollPositionOnPostBack = false;
                        return;
                    }
                    else if (ddlApproval3.SelectedValue == "0")
                    {
                        lblUpError.Text = "Select Chief Financial Officer!.";
                        error.Visible = true;
                        ddlApproval3.BackColor = System.Drawing.Color.Red;
                        Page.MaintainScrollPositionOnPostBack = false;
                        return;
                    }
                    else
                    {
                        string one = ddlApproval1.SelectedValue;
                        string two = ddlApproval2.SelectedValue;
                        string MDA = ddlApproval3.SelectedValue;

                        string finale = one + "," + two;
                        cmd.CommandText = "";
                        cmd.CommandText = "SP_SYS_AssestsDisposalFormDetail";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                        cmd.Parameters.AddWithValue("@APPROVAL", finale.ToString());
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["User_Name"].ToString());
                        cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                        cmd.Parameters.AddWithValue("@Reviwer", MDA.ToString());

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

                            Grid1datainsert();
                            lblmessage.Focus();
                            error.Visible = false;
                            Page.MaintainScrollPositionOnPostBack = false;
                            setinitialrow();
                            GetTransactionID();
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

        protected void getDataWhenQueryStringPass()
        {
            string TI = Request.QueryString["TransactionNo"].ToString();
            cmd.CommandText = "";
            cmd.CommandText = @"SELECT T2.TransactionID As [Transaction ID]
        ,T2.AssetCode As [Asset Code]
        ,T2.Description As [Description]
       ,T2.DateofPurchase  As [Date of Purchase]
      ,T2.Cost As Cost
      ,T2.AccumulatedDepreciation As [Accumulated Depreciation]
      ,T2.NetBookValue As [Net Book Value]
      ,T2.DateofDisposal As [Date of Disposal]
      ,T2.ModeofDisposal As [Mode of Disposal]
      ,T2.SalesProceeds As [Sales Proceeds]
      ,T2.GainOrLossonDisposal As [Gain Or Losson Disposal]
      ,T2.ReasonsJustificationforDisposal As [Reasons Justification for Disposal]
      from tbl_AssestsDisposalFormDetail T1
      inner join tbl_AssestsDisposalForm T2
      on T1.TransactionID = T2.TransactionID
      where T1.TransactionMain = @TI";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TI", TI.ToString());
            adp.SelectCommand = cmd;
            adp.Fill(ds, "Data");
            lblMaxTransactionID.Text = ds.Tables["Data"].Rows[0]["Transaction ID"].ToString();
            grdDetail.DataSource = ds.Tables["Data"];
            grdDetail.DataBind();



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

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                EmailWorkSendToApprovalFromApprover();
                ApplicationStatus();
                BindsysApplicationStatus();
                GetStatusHierachyCategoryControls();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        protected void btnReviwer_Click(object sender, EventArgs e)
        {
            EmailWorkSendToAllfromReviwer();
            ApplicationStatus();
            BindsysApplicationStatus();
            GetStatusHierachyCategoryControls();
        }
    }
}