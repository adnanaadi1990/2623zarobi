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
namespace DashboardProject.Modules.HR
{
    public partial class CRBooking : System.Web.UI.Page
    {
        public string FormID = "CRBooking01";
        private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        public string TransatcionID = "";
        public string li = "";
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
                }
                else
                {
                    GetTransactionID();
                    getUser();
                    getGridData();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                if (txtBookingDate.Text == "")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtBookingDate.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtTimeFrom.Text == "")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtTimeFrom.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtTimeTo.Text == "")
                {
                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtTimeTo.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else
                {
                    getGridData();
                    DataTable tblgetDatBookingCRCheck = (DataTable)ViewState["getDatBookingCR"];
                    DataView dvtblgetDatBookingCRCheck = new DataView(tblgetDatBookingCRCheck);
                    foreach (DataRow dr in tblgetDatBookingCRCheck.Rows)
                    {
                        if (dr["BookingDate"].ToString() == txtBookingDate.Text)
                        {
                            if (dr["TimeFrom"].ToString() == txtTimeFrom.Text)
                            {
                                lblError.Text = rbType.SelectedItem.Text + " is alredy booked for above given date and time.";
                                return;
                            }
                        }
                    }
                    lblError.Text = "";
                    lblUpError.Text = "";
                    lblmessage.Text = "";
                    cmd.CommandText = "";
                    cmd.CommandText = "SP_SYS_CRBooking";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                    cmd.Parameters.AddWithValue("@Date", txtBookingDate.Text);
                    cmd.Parameters.AddWithValue("@Location", rbType.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeFrom", txtTimeFrom.Text);
                    cmd.Parameters.AddWithValue("@TimeTo", txtTimeTo.Text);
                    cmd.Parameters.AddWithValue("@Purpose", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@APPROVAL", ddlHr.SelectedValue);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["User_Name"].ToString());
                    cmd.Parameters.AddWithValue("@Remarks", txtRemarksReview.Text);
                    //cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    adp.SelectCommand = cmd;
                    adp.Fill(ds, "Message");
                    sucess.Visible = true;


                    string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                    lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
                    lblmessage.Text = message + " # " + lblMaxTransactionID.Text;


                    lblmessage.Focus();
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                    // EmailWorkSendFirstApproval();
                    ClearInputs(Page.Controls);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        protected void btnMDA_Click(object sender, EventArgs e)
        {

        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

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
        }
        private void GetTransactionID()
        {
            ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
            lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
            ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();

        }
        private void getUser()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdgetData = new SqlCommand())
                {

                    cmdgetData.CommandText = @"SELECT user_name,DisplayName,Department,Location
 FROM tbluser where Department = 'Human Resources' or Department ='HR & Compliance'";
                    cmdgetData.CommandType = CommandType.Text;
                    cmdgetData.Connection = connection;
                    connection.Open();

                    adp.SelectCommand = cmdgetData;
                    adp.Fill(dt);
                    ViewState["tblusermodulecategoryMerchandiser"] = dt;
                    DataTable tblusermodulecategoryMerchandiser = (DataTable)ViewState["tblusermodulecategoryMerchandiser"];
                    DataView dvDataMerchandiser = new DataView(tblusermodulecategoryMerchandiser);
                    dvDataMerchandiser.RowFilter = "Location = 'Head Office' and Department in ('HR & Compliance','Human Resources')";

                    ddlHr.DataSource = dvDataMerchandiser;
                    ddlHr.DataTextField = "DisplayName";
                    ddlHr.DataValueField = "user_name";
                    ddlHr.DataBind();
                    ddlHr.Items.Insert(0, new ListItem("------Select------", "0"));
                    connection.Close();
                    ddlHr.SelectedIndex = 1;
                }
            }
        }
        private void getGridData()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
            {
                using (SqlCommand cmdgetData = new SqlCommand())
                {
                    cmdgetData.CommandText = "";
                    cmdgetData.CommandText = "getDatBookingCR";
                    cmdgetData.CommandType = CommandType.StoredProcedure;
                    cmdgetData.Connection = connection;
                    connection.Open();

                    adp.SelectCommand = cmdgetData;
                    adp.Fill(ds, "getDatBookingCR");
                    GridView1.DataSource = ds.Tables["getDatBookingCR"];
                    GridView1.DataBind();
                    dt = ds.Tables["getDatBookingCR"];
                    ViewState["getDatBookingCR"] = dt;
                }
            }
        }
    }
}