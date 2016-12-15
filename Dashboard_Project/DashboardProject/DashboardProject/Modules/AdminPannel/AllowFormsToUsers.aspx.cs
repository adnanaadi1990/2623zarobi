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
    public partial class AllowFormsToUsers : System.Web.UI.Page
    {
        public string FormID = "UAF01";
        //private string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        public int a;

        public string User_ID = "";
        public string UserName = "";
        public string FormName = "";
        public string Form_ID = "";
        public string value = "";
        public string Remarks = "";

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

                getUser();
                getFromName();
                GetTransactionID();
                madatorycolor();

            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            {
                if (RBList.SelectedValue == "")
                {
                    lblUpError.Text = "Please Select any Entry Type!.";
                    error.Visible = true;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                ClearInputscolor(Page.Controls);

                if (RBList.SelectedValue != "tblForm_Restricted_Check")
                {
                    if (ddlUserName.SelectedValue == "0")
                    {
                        lblUpError.Text = "Fill all required field!.";
                        error.Visible = true;
                        ddlUserName.BackColor = System.Drawing.Color.Red;
                        Page.MaintainScrollPositionOnPostBack = false;
                        return;
                    }
                    else if (ddlFormName.SelectedValue == "0")
                    {

                        lblUpError.Text = "Fill all required field!.";
                        error.Visible = true;
                        ddlFormName.BackColor = System.Drawing.Color.Red;
                        Page.MaintainScrollPositionOnPostBack = false;
                        return;
                    }

                    else
                    {

                        UserName = ddlUserName.SelectedItem.Text;
                        User_ID = ddlUserName.SelectedValue;
                        FormName = ddlFormName.SelectedItem.Text;
                        Form_ID = ddlFormName.SelectedValue;
                        string[] lines = Form_ID.Split(',');
                        string aa = lines[0].Trim();
                        Form_ID = aa.ToString();
                        value = rbStatus.SelectedValue.ToString();
                    }
                }
                else
                {
                    if (ddlFormNameOther.SelectedValue == "0")
                    {
                        lblUpError.Text = "Fill all required field!.";
                        error.Visible = true;
                        ddlFormNameOther.BackColor = System.Drawing.Color.Red;
                        Page.MaintainScrollPositionOnPostBack = false;
                        return;
                    }
                    else
                    {
                        FormName = ddlFormNameOther.SelectedItem.Text;
                        Form_ID = ddlFormNameOther.SelectedValue;
                        string[] lines = Form_ID.Split(',');
                        string aa = lines[0].Trim();
                        Form_ID = aa.ToString();
                        if (rbRestricted.SelectedValue.ToString() == "0")
                        {
                            value = "0";
                            Remarks = "Allow To Every One";
                        }
                        else
                        {
                            value = "1";
                            Remarks = "Allow To Specific Person";
                        }


                    }

                }

                cmd.CommandText = "";
                cmd.CommandText = "SP_SYS_CreateAdminAuthrizationForms";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@TransactionMain", lblMaxTransactionNo.Text);
                cmd.Parameters.AddWithValue("@user_name", User_ID.ToString());
                cmd.Parameters.AddWithValue("@FormName", FormName.ToString());
                cmd.Parameters.AddWithValue("@TableName ", RBList.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CreatedBy", Session["User_Name"].ToString());
                cmd.Parameters.AddWithValue("@Form_ID  ", Form_ID.ToString());
                cmd.Parameters.AddWithValue("@FormID", FormID.ToString());
                cmd.Parameters.AddWithValue("@Status", value.ToString());
                cmd.Parameters.AddWithValue("@Remarks", Remarks.ToString());



                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                adp.SelectCommand = cmd;
                adp.Fill(ds, "Message");
                sucess.Visible = true;

                string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
                lblmessage.Text = message + " against Form ID # " + lblMaxTransactionID.Text;

                //EmailWorkSendFirstApproval();
                lblmessage.Focus();
                error.Visible = false;
                Page.MaintainScrollPositionOnPostBack = false;
                getTablesDetails();
                ClearInputs(Page.Controls);


                ClearInputscolor(Page.Controls);
                GetTransactionID();
                madatorycolor();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputs(Page.Controls);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {

        }

        ///////////////////////////////////////////////////Methods//////////////////////////////////////////////////////////

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

        protected void madatorycolor()
        {

            if (this.RBList.SelectedValue == "tblForm_Restricted_Check")
            {

                ddlFormNameOther.BackColor = System.Drawing.Color.AliceBlue;

            }
            else
            {
                ddlUserName.BackColor = System.Drawing.Color.AliceBlue;
                ddlFormName.BackColor = System.Drawing.Color.AliceBlue;


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

        protected void getUser()
        {
            string strQuery = @"Select user_name,user_email,DisplayName from tbluser";

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(ds, "tblUser");

            ddlUserName.DataTextField = ds.Tables["tblUser"].Columns["DisplayName"].ToString(); // text field name of table dispalyed in dropdown
            ddlUserName.DataValueField = ds.Tables["tblUser"].Columns["user_name"].ToString();             // to retrive specific  textfield name 
            ddlUserName.DataSource = ds.Tables["tblUser"];      //assigning datasource to the dropdownlist
            ddlUserName.DataBind();  //binding dropdownlist
            ddlUserName.Items.Insert(0, new ListItem("------Select------", "0"));
            conn.Close();
        }

        protected void getFromName()
        {
            string strQuery = @"SELECT FormName,Form_ID from tblFormsDetail";

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

            ddlFormName.DataTextField = ds.Tables["tblFormsDetail"].Columns["FormName"].ToString(); // text field name of table dispalyed in dropdown
            ddlFormName.DataValueField = ds.Tables["tblFormsDetail"].Columns["Form_ID"].ToString();             // to retrive specific  textfield name 
            ddlFormName.DataSource = ds.Tables["tblFormsDetail"];      //assigning datasource to the dropdownlist
            ddlFormName.DataBind();  //binding dropdownlist
            ddlFormName.Items.Insert(0, new ListItem("------Select------", "0"));

            ddlSearchForm.DataTextField = ds.Tables["tblFormsDetail"].Columns["FormName"].ToString(); // text field name of table dispalyed in dropdown
            ddlSearchForm.DataValueField = ds.Tables["tblFormsDetail"].Columns["Form_ID"].ToString();             // to retrive specific  textfield name 
            ddlSearchForm.DataSource = ds.Tables["tblFormsDetail"];      //assigning datasource to the dropdownlist
            ddlSearchForm.DataBind();  //binding dropdownlist
            ddlSearchForm.Items.Insert(0, new ListItem("------Select------", "0"));

            conn.Close();
        }

        protected void getTablesDetails()
        {
            ddlUserName.SelectedIndex = -1;
            ddlFormNameOther.SelectedIndex = -1;
            rbRestricted.SelectedValue = "0";
            rbStatus.SelectedValue = "Active";
            ddlFormName.SelectedIndex = -1;



            string strQuery = "";
            if (this.RBList.SelectedValue == "tblForm_Restricted_Check")
            {
                strQuery = @"Select Code as ID,Form_Name as FormName,Form_ID,Restricted as Active,Remarks from tblForm_Restricted_Check order by Code asc";
            }
            else
            {
                strQuery = @"Select ID,UserName, FormName, Form_ID,Active as Active from tblAllow_Forms order by ID asc";
            }

            ds.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            conn.Open();
            ds.Clear();
            adp.Fill(dt);
            if (this.RBList.SelectedValue == "tblAllow_Forms")
            {
                DataColumn c = new DataColumn("Remarks", typeof(string));
                dt.Columns.Add(c);
            }
            else
            {
                DataColumn f = new DataColumn("UserName", typeof(string));
                dt.Columns.Add(f);
            }
            ViewState["paging"] = dt;
            grdData.DataSource = dt;
            grdData.DataBind();
            grdData.Visible = true;


        }



        protected void RBList_SelectedIndexChanged(object sender, EventArgs e)
        {

            error.Visible = false;
            sucess.Visible = false;
            this.dvAllowForm.Visible = false;
            this.dvRestricted.Visible = false;
            ddlSearchForm.SelectedIndex = -1;
            madatorycolor();
            if (this.RBList.SelectedValue == "tblForm_Restricted_Check")
            {
                this.dvRestricted.Visible = true;
            }
            else
            {
                this.dvAllowForm.Visible = true;
            }
            getTablesDetails();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            DataTable dtpaging = (DataTable)ViewState["paging"];
            grdData.DataSource = dtpaging;   // 6 feb 2014
            grdData.DataBind();
        }
        protected void grdData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string DeleteQuery = "";
            string Column = "";

            if (RBList.SelectedValue == "")
            {
                lblUpError.Text = "Please Select any Entry Type!.";
                error.Visible = true;
                Page.MaintainScrollPositionOnPostBack = false;
                return;
            }
            else
            {
                if (this.RBList.SelectedValue == "tblForm_Restricted_Check")
                {
                    Column = "Code";
                }
                else
                {
                    Column = "ID";
                }

                var id = ((Label)grdData.Rows[e.RowIndex].FindControl("lblID")).Text;

                DeleteQuery = @"delete from " + RBList.SelectedValue + " where " + Column.ToString() + " = @DeleteID";

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = DeleteQuery;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@DeleteID", id.ToString());
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
                    getTablesDetails();
                    Page.MaintainScrollPositionOnPostBack = false;
                }
            }


        }
        protected void grdData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            var ID = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblID")).Text;
            var UserName = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblUserName")).Text;
            var FormName = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblFormName")).Text;
            var FormID = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblFormID")).Text;
            var Remarks = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblRemarks")).Text;
            var Active = ((Label)grdData.Rows[e.NewSelectedIndex].FindControl("lblActive")).Text;
            //var FormNamehf = ((HiddenField)grdData.Rows[e.NewSelectedIndex].FindControl("hfFormName")).Value;

            if (this.RBList.SelectedValue == "tblForm_Restricted_Check")
            {
                ddlFormNameOther.Attributes.Add("disabled", "false");
                hfCode.Value = ID.ToString();
                ddlFormNameOther.SelectedValue = FormID.ToString();
                rbRestricted.SelectedItem.Text = Remarks.ToString();
            }
            else
            {
                ddlUserName.Attributes.Add("disabled", "false");
                hfCode.Value = ID.ToString();
                ddlUserName.SelectedValue = UserName.ToString();
                ddlFormName.SelectedValue = FormID.ToString();
                rbStatus.SelectedValue = Active.ToString();
            }
        }
        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (this.RBList.SelectedValue == "tblForm_Restricted_Check")
            {
                grdData.Columns[3].Visible = false;
                grdData.Columns[6].Visible = true;
            }
            else
            {
                grdData.Columns[3].Visible = true;
                grdData.Columns[6].Visible = false;
            }


        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                lblError.Text = "";
                string PH = ddlFormNameOther.SelectedValue.ToString();
                string[] lines = PH.Split(',');
                string aa = "";
                string text = "";
                string UpdateQuery = "";

                if (RBList.SelectedValue == "tblForm_Restricted_Check")
                {
                    aa = lines[1].Trim();
                    UpdateQuery = @"update tblForm_Restricted_Check set Restricted = @Restricted,Remarks =@Remarks where code = @code";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = UpdateQuery;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Restricted", rbRestricted.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Remarks", rbRestricted.SelectedItem.Text.ToString());
                    cmd.Parameters.AddWithValue("@code", hfCode.Value.ToString());
                    conn.Open();
                    a = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    if (rbStatus.SelectedValue == "")
                    {
                        lblError.Text = "Please Select Status";
                    }
                    else
                    {
                        aa = lines[0].Trim();
                        UpdateQuery = @"update tblAllow_Forms set FormName = @FormName,Form_ID = @Form_ID, Active = @Active where ID = @code";
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = UpdateQuery;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@UserName", ddlUserName.SelectedValue);
                        cmd.Parameters.AddWithValue("@Form_ID", ddlFormName.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@FormName", ddlFormName.SelectedItem.Text.ToString());
                        cmd.Parameters.AddWithValue("@Active", rbStatus.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@code", hfCode.Value.ToString());
                        conn.Open();
                        a = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
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
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                ddlUserName.Attributes.Add("disabled", "true");
                ddlFormNameOther.Attributes.Add("disabled", "true");
                //GenerateAlphabets();
                //abc.Visible = false;
            }
            catch (Exception ex)
            {
            }
        }
        protected void ddlSearchForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBList.SelectedValue == "")
                {
                    lblUpError.Text = "Please Select any Entry Type!.";
                    error.Visible = true;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                ddlUserName.SelectedIndex = -1;
                ddlFormNameOther.SelectedIndex = -1;
                rbRestricted.SelectedValue = "0";
                rbStatus.SelectedValue = "Active";
                ddlFormName.SelectedIndex = -1;

                string ValueForm = "";
                ValueForm = ddlSearchForm.SelectedValue.ToString();
                string[] lines = ValueForm.Split(',');
                string aa = "";

                string strQuery = "";
                if (this.RBList.SelectedValue == "tblForm_Restricted_Check")
                {
                    aa = lines[0].Trim();
                    strQuery = @"Select Code as ID,Form_Name as FormName,Form_ID,Restricted as Active,Remarks from tblForm_Restricted_Check where Form_ID = '" + aa.ToString() + "' order by Code asc";
                }
                else
                {
                    aa = lines[0].Trim();
                    strQuery = @"Select ID,UserName, FormName, Form_ID,Active as Active from tblAllow_Forms where Form_ID = '" + aa.ToString() + "' order by ID asc";
                }

                ds.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                adp.SelectCommand = cmd;
                conn.Open();
                ds.Clear();
                adp.Fill(dt);
                if (this.RBList.SelectedValue == "tblAllow_Forms")
                {
                    DataColumn c = new DataColumn("Remarks", typeof(string));
                    dt.Columns.Add(c);
                }
                else
                {
                    DataColumn f = new DataColumn("UserName", typeof(string));
                    dt.Columns.Add(f);
                }
                ViewState["paging"] = dt;
                grdData.DataSource = dt;
                grdData.DataBind();
                grdData.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }
    }
}