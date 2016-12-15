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

namespace ITLDashboard.Modules.AdminPannel
{
    public partial class AdminApprovalForm : System.Web.UI.Page
    {
        public string FormID = "UEF01";
        //private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();


        public string DisplayName = "";
        public string UserName = "";
        public string UserEmail = "";
        public string FormName = "";

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

                ViewState["CurrentAlphabet"] = "ALL";
                this.GenerateAlphabets();
                GetTransactionID();
                methodCall();
                madatorycolor();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (RBList.SelectedValue == "")
            {
                lblUpError.Text = "Please Select any Entry Type!.";
                error.Visible = true;
                txtDisplayName.BackColor = System.Drawing.Color.Red;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            ClearInputscolor(Page.Controls);

            if (RBList.SelectedValue == "tbluser")
            {
                if (txtDisplayName.Text == "")
                {

                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtDisplayName.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtUserName.Text == "")
                {

                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtUserName.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtEmailID.Text == "")
                {

                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtEmailID.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else
                {
                    DisplayName = txtDisplayName.Text;
                    UserName = txtUserName.Text;
                    UserEmail = txtEmailID.Text;

                }
            }
            else
            {
                if (ddlDisplayNameOther.SelectedValue == "0")
                {

                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlDisplayNameOther.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtUserNameOther.Text == "")
                {

                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtUserNameOther.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtEmailIDOther.Text == "")
                {

                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    txtEmailIDOther.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (ddlFormNameOther.SelectedValue == "0")
                {

                    lblUpError.Text = "Fill all required field!.";
                    error.Visible = true;
                    ddlFormNameOther.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else
                {
                    if (RBList.SelectedValue == "tbl_EmailToSpecificPerson")
                    {
                        DisplayName = ddlDisplayNameOther.SelectedValue;
                        UserName = txtUserNameOther.Text;
                        UserEmail = txtEmailIDOther.Text;
                        string PH = ddlFormNameOther.SelectedValue.ToString();
                        string[] lines = PH.Split(',');
                        string aa = lines[0].Trim();
                        string ab = lines[1].Trim();
                        FormName = ab.ToString();
                    }
                    else
                    {
                        DisplayName = ddlDisplayNameOther.SelectedValue;
                        UserName = txtUserNameOther.Text;
                        UserEmail = txtEmailIDOther.Text;
                        FormName = ddlFormNameOther.SelectedValue;
                        string PH = ddlFormNameOther.SelectedValue.ToString();
                        string[] lines = PH.Split(',');
                        string aa = lines[0].Trim();
                        string ab = lines[1].Trim();
                        FormName = aa.ToString();
                    }
                }

            }

            cmd.CommandText = "";
            cmd.CommandText = "SP_SYS_CreateAdminAuthrizationData";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
            cmd.Parameters.AddWithValue("@DisplayName", DisplayName.ToString());
            cmd.Parameters.AddWithValue("@user_name", UserName.ToString());
            cmd.Parameters.AddWithValue("@UserEmail ", UserEmail.ToString());
            cmd.Parameters.AddWithValue("@FormName  ", FormName.ToString());
            cmd.Parameters.AddWithValue("@TableName  ", RBList.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CreatedBy", Session["User_Name"].ToString());
            cmd.Parameters.AddWithValue("@FormID", FormID.ToString());



            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            adp.SelectCommand = cmd;
            adp.Fill(ds, "Message");
            sucess.Visible = true;

            string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
            lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
            lblmessage.Text = message + " # " + lblMaxTransactionID.Text;

            //EmailWorkSendFirstApproval();
            lblmessage.Focus();
            error.Visible = false;
            Page.MaintainScrollPositionOnPostBack = false;
            getTablesDetails();
            ClearInputs(Page.Controls);


            ClearInputscolor(Page.Controls);
            GetTransactionID();
            madatorycolor();
            getUser();
        }

        protected void btnMDA_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputs(Page.Controls);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {

        }

        protected void btnReviewed_Click(object sender, EventArgs e)
        {

        }

        private void methodCall()
        {
            getUser();
            getFromName();
        }

        protected void getDataWhenQueryStringPass()
        {
            string TI = Request.QueryString["TransactionNo"].ToString();
            methodCall();

            cmd.CommandText = "";
            cmd.CommandText = "SELECT * from tbl_AdminAuthrizationData where TransactionMain = @TI";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@TI", TI.ToString());
            adp.SelectCommand = cmd;
            adp.Fill(ds, "Data");

            string Value = ds.Tables["Data"].Rows[0]["TableName"].ToString();


            lblMaxTransactionNo.Text = ds.Tables["Data"].Rows[0]["TransactionMain"].ToString();
            lblMaxTransactionID.Text = ds.Tables["Data"].Rows[0]["TransactionID"].ToString();
            RBList.SelectedValue = ds.Tables["Data"].Rows[0]["TableName"].ToString();
            this.dvNewUser.Visible = false;
            this.dvAllUser.Visible = false;
            if (Value.ToString() == "tbluser")
            {
                this.dvNewUser.Visible = true;
                txtDisplayName.Text = ds.Tables["Data"].Rows[0]["DisplayName"].ToString();
                txtUserName.Text = ds.Tables["Data"].Rows[0]["user_name"].ToString();
                txtEmailID.Text = ds.Tables["Data"].Rows[0]["UserEmail"].ToString();
            }
            else
            {
                this.dvAllUser.Visible = true;
                ddlDisplayNameOther.SelectedValue = ds.Tables["Data"].Rows[0]["DisplayName"].ToString();
                txtUserNameOther.Text = ds.Tables["Data"].Rows[0]["user_name"].ToString();
                txtEmailIDOther.Text = ds.Tables["Data"].Rows[0]["UserEmail"].ToString();
                ddlFormNameOther.SelectedValue = ds.Tables["Data"].Rows[0]["FormName"].ToString();
            }


            //txtDisplayName.Text = ds.Tables["Data"].Rows[0]["DisplayName"].ToString();
            //txtUserName.Text = ds.Tables["Data"].Rows[0]["UserName"].ToString();
            //txtEmailID.Text = ds.Tables["Data"].Rows[0]["EmailID"].ToString();

            //ddlDisplayNameOther.SelectedValue = ds.Tables["Data"].Rows[0]["DisplayName"].ToString();
            //txtUserNameOther.Text = ds.Tables["Data"].Rows[0]["UserName"].ToString();
            //txtEmailIDOther.Text = ds.Tables["Data"].Rows[0]["EmailID"].ToString();
            //ddlFormNameOther.SelectedValue = ds.Tables["Data"].Rows[0]["FormName"].ToString();

        }

        private void GetHarcheyID()
        {
            ds = obj.GetHarachyPettyCash(Session["User_Name"].ToString(), lblMaxTransactionID.Text, FormID.ToString());
            lblMaxTransactionID.Text = ds.Tables["HID"].Rows[0]["TransactionID"].ToString();
            ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
        }

        private void GetTransactionID()
        {
            ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
            lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
            ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();

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
                btnCancel.Enabled = false;
            }
        }


        protected void getUser()
        {
            conn.Close();
            string strQuery = @"Select user_name,user_email,DisplayName from tbluser";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(ds, "tblUser");

            ddlDisplayNameOther.DataTextField = ds.Tables["tblUser"].Columns["DisplayName"].ToString(); // text field name of table dispalyed in dropdown
            ddlDisplayNameOther.DataValueField = ds.Tables["tblUser"].Columns["DisplayName"].ToString();             // to retrive specific  textfield name 
            ddlDisplayNameOther.DataSource = ds.Tables["tblUser"];      //assigning datasource to the dropdownlist
            ddlDisplayNameOther.DataBind();  //binding dropdownlist
            ddlDisplayNameOther.Items.Insert(0, new ListItem("------Select------", "0"));
            conn.Close();
        }

        protected void getFromName()
        {
            string strQuery = @"SELECT FormName,Form_ID +','+isnull(FormIDCode,'') as Form_ID from tblFormsDetail";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(ds, "tblFormsDetail");

            ddlFormNameOther.DataTextField = ds.Tables["tblFormsDetail"].Columns["FormName"].ToString(); // text field name of table dispalyed in dropdown
            ddlFormNameOther.DataValueField = ds.Tables["tblFormsDetail"].Columns["Form_ID"].ToString();             // to retrive specific  textfield name 
            ddlFormNameOther.DataSource = ds.Tables["tblFormsDetail"];      //assigning datasource to the dropdownlist
            ddlFormNameOther.DataBind();  //binding dropdownlist
            ddlFormNameOther.Items.Insert(0, new ListItem("------Select------", "0"));

            ddlSearchForm.DataTextField = ds.Tables["tblFormsDetail"].Columns["FormName"].ToString(); // text field name of table dispalyed in dropdown
            ddlSearchForm.DataValueField = ds.Tables["tblFormsDetail"].Columns["Form_ID"].ToString();             // to retrive specific  textfield name 
            ddlSearchForm.DataSource = ds.Tables["tblFormsDetail"];      //assigning datasource to the dropdownlist
            ddlSearchForm.DataBind();  //binding dropdownlist
            ddlSearchForm.Items.Insert(0, new ListItem("------Select------", "0"));

            conn.Close();
        }

        protected void RBList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dvNewUser.Visible = false;
            this.dvAllUser.Visible = false;
            AlphabetPager.Visible = true;
            SearchForm.Visible = true;
            madatorycolor();
            if (this.RBList.SelectedValue == "tbluser")
            {
                this.dvNewUser.Visible = true;
            }
            else
            {
                this.dvAllUser.Visible = true;

            }
            getTablesDetails();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            abc.Visible = true;
            getUser();
        }

        protected void getTablesDetails()
        {
            try
            {
                ddlDisplayNameOther.SelectedIndex = -1;
                txtDisplayName.Text = "";
                txtEmailIDOther.Text = "";
                txtEmailID.Text = "";
                ddlFormNameOther.SelectedIndex = -1;
                txtUserName.Text = "";
                txtDisplayName.Text = "";
                txtUserNameOther.Text = "";


                string strQuery = "";
                if (this.RBList.SelectedValue == "tbluser")
                {

                    strQuery = @"SELECT * FROM tbluser WHERE DisplayName LIKE '" + ViewState["CurrentAlphabet"].ToString() + "' + '%' OR '" + ViewState["CurrentAlphabet"].ToString() + "' = 'ALL'";
                    //  strQuery = @"select * from  tbluser";
                }
                else if (this.RBList.SelectedValue == "tbl_EmailToSpecificPerson")
                {

                    strQuery = @"Select A.user_name,A.user_email,A.DisplayName,FD.Form_ID+','+FD.FormIDCode as FormName 
                            from tbl_EmailToSpecificPerson A 
                            left outer join tblFormsDetail FD on a.FormID = FD.FormIDCode WHERE DisplayName LIKE '" + ViewState["CurrentAlphabet"].ToString() + "' + '%' OR '" + ViewState["CurrentAlphabet"].ToString() + "' = 'ALL'";
                }
                else
                {

                    strQuery = @"Select A.user_name,A.user_email,A.DisplayName,FD.Form_ID+','+FD.FormIDCode as FormName from " + RBList.SelectedValue + " A left outer join tblFormsDetail FD on a.FormName = FD.Form_ID WHERE DisplayName LIKE '" + ViewState["CurrentAlphabet"].ToString() + "' + '%' OR '" + ViewState["CurrentAlphabet"].ToString() + "' = 'ALL' ";
                }


                ds.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                conn.Open();
                ds.Clear();
                adp.Fill(dt);
                if (this.RBList.SelectedValue == "tbluser")
                {
                    DataColumn c = new DataColumn("FormName", typeof(string));
                    dt.Columns.Add(c);
                }
                ViewState["paging"] = dt;
                grdData.DataSource = dt;
                grdData.DataBind();
                grdData.Visible = true;
            }
            catch (Exception ex)
            {
            }

        }

        ///////////////////////////////////////////////////Methods//////////////////////////////////////////////////////////


        private void GenerateAlphabets()
        {
            List<ListItem> alphabets = new List<ListItem>();
            ListItem alphabet = new ListItem();
            alphabet.Value = "ALL";
            alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
            alphabets.Add(alphabet);
            for (int i = 65; i <= 90; i++)
            {
                alphabet = new ListItem();
                alphabet.Value = Char.ConvertFromUtf32(i);
                alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
                alphabets.Add(alphabet);
            }
            rptAlphabets.DataSource = alphabets;
            rptAlphabets.DataBind();
        }

        protected void Alphabet_Click(object sender, EventArgs e)
        {
            LinkButton lnkAlphabet = (LinkButton)sender;
            ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
            this.GenerateAlphabets();
            grdData.PageIndex = 0;
            getTablesDetails();
        }

        protected void madatorycolor()
        {
            if (this.RBList.SelectedValue == "tbluser")
            {
                txtDisplayName.BackColor = System.Drawing.Color.AliceBlue;
                txtUserName.BackColor = System.Drawing.Color.AliceBlue;
                txtEmailID.BackColor = System.Drawing.Color.AliceBlue;
            }
            else
            {
                ddlDisplayNameOther.BackColor = System.Drawing.Color.AliceBlue;
                txtUserNameOther.BackColor = System.Drawing.Color.AliceBlue;
                txtEmailIDOther.BackColor = System.Drawing.Color.AliceBlue;
                ddlFormNameOther.BackColor = System.Drawing.Color.AliceBlue;
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
                if (c is RadioButtonList)
                {
                    ((RadioButtonList)(c)).Enabled = State;
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
                if (ctrlsss is RadioButtonList)
                    ((RadioButtonList)ctrlsss).BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                ClearInputscolor(ctrlsss.Controls);
            }
        }

        ///////////////////////////////////////////////////Methods//////////////////////////////////////////////////////////

        protected void btnApproved_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
        protected void ddlDisplayNameOther_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedID = ddlDisplayNameOther.SelectedItem.Value;
            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"Select user_name,user_email,DisplayName from tbluser where DisplayName = @DN";
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            cmd.Parameters.AddWithValue("@DN", selectedID.ToString());
            conn.Open();
            using (SqlDataReader theReader = cmd.ExecuteReader())
            {
                if (theReader.HasRows)
                {
                    // Get the first row
                    theReader.Read();

                    // Set the text box values
                    txtUserNameOther.Text = theReader.GetString(0);
                    txtEmailIDOther.Text = theReader.GetString(1);
                    //  ddlDisplayNameOther.SelectedValue = theReader.GetString(0);

                }
            }
            conn.Close();

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string PH = ddlFormNameOther.SelectedValue.ToString();
            string[] lines = PH.Split(',');
            string aa = "";
            string UpdateQuery = "";
            if (this.RBList.SelectedValue == "tbl_EmailToSpecificPerson")
            {
                aa = lines[1].Trim();
                UpdateQuery = @"update tbl_EmailToSpecificPerson set FormID = @FormName where user_name = @UserName";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = UpdateQuery;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@UserName", txtUserNameOther.Text.ToString());
                cmd.Parameters.AddWithValue("@FormName", aa.ToString());
                conn.Open();
                int a = cmd.ExecuteNonQuery();
                conn.Close();
                if (a > 0)
                {
                    lblmessage.Text = "Record Updated successfully";
                    sucess.Visible = true;
                    lblmessage.Focus();
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                    getTablesDetails();
                    ClearInputs(Page.Controls);

                }
                else
                {
                    lblmessage.Text = "Record can't updated";
                    sucess.Visible = true;
                    lblmessage.Focus();
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                }
            }
            if (this.RBList.SelectedValue == "tbluser")
            {
                aa = lines[0].Trim();
                btnUpdate.Visible = false;
                //            UpdateQuery = @"UPDATE tbluser SET user_name = @UserName,
                //                user_email = @EmailID ,DisplayName = @DisplayName
                //	            where user_name = '" + txtUserName.Text.ToString() + "'";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_sys_UpdateUsers";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.ToString());
                cmd.Parameters.AddWithValue("@DisplayName", txtDisplayName.Text.ToString());
                cmd.Parameters.AddWithValue("@EmailID", txtEmailID.Text.ToString());
                cmd.Parameters.AddWithValue("@UserNameupdate", hfUserNameupdate.Value.ToString());
                conn.Open();
                int a = cmd.ExecuteNonQuery();
                conn.Close();
                if (a < 0)
                {
                    lblmessage.Text = "Record Updated successfully";
                    sucess.Visible = true;
                    lblmessage.Focus();
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                    getTablesDetails();
                    ClearInputs(Page.Controls);

                }
                else
                {
                    lblmessage.Text = "Record can't updated";
                    sucess.Visible = true;
                    lblmessage.Focus();
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                }
            }

            else
            {
                btnUpdate.Visible = true;
                aa = lines[0].Trim();
                UpdateQuery = @"update " + RBList.SelectedValue + " set FormName = @FormName where user_name = @UserName";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = UpdateQuery;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@UserName", txtUserNameOther.Text.ToString());
                cmd.Parameters.AddWithValue("@FormName", aa.ToString());
                conn.Open();
                int a = cmd.ExecuteNonQuery();
                conn.Close();
                if (a > 0)
                {
                    lblmessage.Text = "Record Updated successfully";
                    sucess.Visible = true;
                    lblmessage.Focus();
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                    getTablesDetails();
                    ClearInputs(Page.Controls);

                }
                else
                {
                    lblmessage.Text = "Record can't updated";
                    sucess.Visible = true;
                    lblmessage.Focus();
                    error.Visible = false;
                    Page.MaintainScrollPositionOnPostBack = false;
                }
            }



            btnUpdate.Visible = false;
            btnSave.Visible = true;
            GenerateAlphabets();
            abc.Visible = false;
            getUser();


        }
        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            DataTable dtpaging = (DataTable)ViewState["paging"];
            grdData.DataSource = dtpaging;
            grdData.DataBind();
        }
        protected void grdData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            var UserName = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblUserName")).Text;
            var UserEmail = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lbluseremail")).Text;
            var DisplayName = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblDisplayName")).Text;
            var FormName = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblFormName")).Text;
            //var FormNamehf = ((HiddenField)grdData.Rows[e.NewSelectedIndex].FindControl("hfFormName")).Value;

            if (this.RBList.SelectedValue == "tbluser")
            {

                txtDisplayName.Text = DisplayName.ToString();
                txtUserName.Text = UserName.ToString();
                hfUserNameupdate.Value = UserName.ToString();
                txtEmailID.Text = UserEmail.ToString();

            }
            else
            {

                if (DisplayName == "")
                {
                    ddlDisplayNameOther.SelectedIndex = -1;
                }
                else
                {
                    ddlDisplayNameOther.SelectedValue = DisplayName.ToString();
                }

                txtUserNameOther.Text = UserName.ToString();
                if (FormName == "")
                {
                    ddlFormNameOther.SelectedIndex = -1;
                }
                else
                {
                    ddlFormNameOther.SelectedValue = FormName.ToString();
                }

                txtEmailIDOther.Text = UserEmail.ToString();
            }

        }
        protected void grdData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string DeleteQuery = "";
            string Column = "";
            var id = ((Label)grdData.Rows[e.RowIndex].FindControl("lblUserName")).Text;
            var FormName = ((Label)grdData.Rows[e.RowIndex].FindControl("lblFormName")).Text;

            string[] Fname = FormName.Split(',');
            string Val = "";

            //   DeleteQuery = @"delete from " + RBList.SelectedValue + " where user_name = @UserName";


            if (this.RBList.SelectedValue == "tbluser")
            {
                DeleteQuery = @"delete from tbluser where user_name = @UserName";
            }
            else if (this.RBList.SelectedValue == "tbl_EmailToSpecificPerson")
            {

                Val = Fname[1].Trim();
                DeleteQuery = @"delete from tbl_EmailToSpecificPerson WHERE FormID = @FormName and user_name = @UserName";
            }
            else
            {
                Val = Fname[0].Trim();
                DeleteQuery = @"delete  from " + RBList.SelectedValue + "  WHERE FormName = @FormName and user_name = @UserName";
            }

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = DeleteQuery;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@UserName", id.ToString());
            cmd.Parameters.AddWithValue("@FormName", Val.ToString());
            conn.Open();
            int a = cmd.ExecuteNonQuery();
            conn.Close();
            if (a > 0)
            {
                lblmessage.Text = "Record deleted successfully";
                sucess.Visible = true;
                lblmessage.Focus();
                error.Visible = false;
                Page.MaintainScrollPositionOnPostBack = false;
                getTablesDetails();
                ClearInputs(Page.Controls);

            }
            else
            {
                lblmessage.Text = "Record can't deleted";
                sucess.Visible = true;
                lblmessage.Focus();
                error.Visible = false;
                Page.MaintainScrollPositionOnPostBack = false;
            }
        }
        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (this.RBList.SelectedValue == "tbluser")
            {
                grdData.Columns[5].Visible = false;
            }
            else
            {
                grdData.Columns[5].Visible = true;
            }

        }
        protected void ddlSearchForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                error.Visible = false;
                sucess.Visible = false;
                lblEmail.Text = "";

                if (RBList.SelectedValue == "")
                {
                    lblUpError.Text = "Please Select any Entry Type!.";
                    error.Visible = true;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                ddlDisplayNameOther.SelectedIndex = -1;
                txtDisplayName.Text = "";
                txtEmailIDOther.Text = "";
                txtEmailID.Text = "";
                ddlFormNameOther.SelectedIndex = -1;
                txtUserName.Text = "";
                txtDisplayName.Text = "";
                txtUserNameOther.Text = "";


                string strQuery = "";
                string ValueForm = "";
                ValueForm = ddlSearchForm.SelectedValue.ToString();
                string[] lines = ValueForm.Split(',');
                string aa = "";

                if (this.RBList.SelectedValue == "tbluser")
                {

                    strQuery = @"Select * from tbluser";
                }
                else if (this.RBList.SelectedValue == "tbl_EmailToSpecificPerson")
                {

                    aa = lines[1].Trim();

                    strQuery = @"Select A.user_name,A.user_email,A.DisplayName,FD.Form_ID+','+FD.FormIDCode as FormName 
                            from tbl_EmailToSpecificPerson A 
                            left outer join tblFormsDetail FD on a.FormID = FD.FormIDCode WHERE FormID = '" + aa.ToString() + "'";
                }
                else
                {
                    aa = lines[0].Trim();
                    strQuery = @"Select A.user_name,A.user_email,A.DisplayName,FD.Form_ID+','+FD.FormIDCode as FormName from " + RBList.SelectedValue + " A left outer join tblFormsDetail FD on a.FormName = FD.Form_ID  WHERE A.FormName = '" + aa.ToString() + "'";
                }


                ds.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                conn.Open();
                ds.Clear();
                adp.Fill(dt);
                if (this.RBList.SelectedValue == "tbluser")
                {
                    DataColumn c = new DataColumn("FormName", typeof(string));
                    dt.Columns.Add(c);
                }
                ViewState["paging"] = dt;
                grdData.DataSource = dt;
                grdData.DataBind();
                grdData.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
    }
}