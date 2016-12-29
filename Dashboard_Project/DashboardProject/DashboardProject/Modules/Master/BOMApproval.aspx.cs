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

namespace DashboardProject.Modules.Master
{
    public partial class BOMApproval : System.Web.UI.Page
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
        public string FormID = "102";
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    if (Request.QueryString["TransactionNo"] != null)
                    {

                    }
                    else
                    {
                        BindGrid();
                        BindPlant();
                        GetTransactionID();
                        getUserHOD();
                        mandatcolor();
                        BindUser();
                        getUserDetail();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Page_Load" + ex.ToString();
            }
        }

        private void mandatcolor()
        {
            try
            {
                txtBaseQuantity.BackColor = System.Drawing.Color.AliceBlue;
                ddlPlant.BackColor = System.Drawing.Color.AliceBlue;
                ddlStorageLocation.BackColor = System.Drawing.Color.AliceBlue;
                txtMaterial.BackColor = System.Drawing.Color.AliceBlue;
                txtDescription.BackColor = System.Drawing.Color.AliceBlue;
            }
            catch (Exception ex)
            {
                lblError.Text = "mandatcolor" + ex.ToString();
            }

        }


        #region GridWorking

        private void BindGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[7] { new DataColumn("TransactionID"), new DataColumn("ComponentType"), new DataColumn("Material"), new DataColumn("MaterialDescription"), new DataColumn("Quantity"), new DataColumn("UOM"), new DataColumn("StoreLocation") });
                DataColumn c = new DataColumn("sno", typeof(int));
                c.AutoIncrement = true;
                c.AutoIncrementSeed = 1;
                c.AutoIncrementStep = 1;
                dt.Columns.Add(c);
                ViewState["BOMGrid"] = dt;
                GridView1.DataSource = (DataTable)ViewState["BOMGrid"];
                GridView1.DataBind();
                GridView1.Columns[0].Visible = true;
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
                if (ddlPlant.SelectedValue != "")
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
                    string customerName = (control.FindControl("ddlComponentType") as DropDownList).SelectedValue;
                    string companyName2 = (control.FindControl("txtMaterial") as TextBox).Text;
                    string companyName3 = (control.FindControl("txtMaterialDescription") as TextBox).Text;
                    string companyName4 = (control.FindControl("txtQuantity") as TextBox).Text;
                    string companyName5 = (control.FindControl("ddlUOM") as DropDownList).SelectedValue;
                    string companyName6 = (control.FindControl("ddlStLoc") as DropDownList).SelectedValue;

                    if (customerName.ToString() == "")
                    {
                        lblgridError.Text = "Component Type should not be left blank";
                        return;
                    }
                    if (companyName2.ToString() == "")
                    {
                        lblgridError.Text = "Material No should not be left blank";
                        return;
                    }
                    if (companyName3.ToString() == "")
                    {
                        lblgridError.Text = "Material Description should not be left blank";
                        return;
                    }
                    if (companyName4.ToString() == "")
                    {
                        lblgridError.Text = "Quantity should not be left blank";
                        return;
                    }
                    if (companyName5.ToString() == "")
                    {
                        lblgridError.Text = "UOM should not be left blank";
                        return;
                    }
                    if (companyName6.ToString() == "")
                    {
                        lblgridError.Text = "Store Location should not be left blank";
                        return;
                    }
                    else
                    {
                        DataTable dt = (DataTable)ViewState["BOMGrid"];
                        dt.Rows.Add("", customerName.ToString().Trim(), companyName2.ToString().Trim(), companyName3.ToString().Trim(),
                           companyName4.ToString().Trim(), companyName5.ToString().Trim(), companyName6.ToString().Trim());
                        ViewState["BOMGrid"] = dt;
                        GridView1.DataSource = (DataTable)ViewState["BOMGrid"];
                        GridView1.DataBind();
                        GridView1.Columns[0].Visible = true;


                        float GTotal = 0f;
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            String total = (GridView1.Rows[i].FindControl("lblQuantity") as Label).Text;
                            GTotal += Convert.ToSingle(total);
                        }
                        lblSum.Text = GTotal.ToString();
                    }
                }
                else
                {
                    lblgridError.Text = "Select any plant!";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Add" + ex.ToString();
            }
        }


        private DataSet GetData(string query)
        {
            string conString = ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Plantcode", ddlPlant.SelectedValue.ToString());
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        #endregion



        private void BindPlant()
        {
            try
            {
                ds.Clear();
                ds = obj.BindPlant();
                ddlPlant.DataTextField = ds.Tables["Plant"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
                ddlPlant.DataValueField = ds.Tables["Plant"].Columns["PlantId"].ToString();             // to retrive specific  textfield name 
                ddlPlant.DataSource = ds.Tables["Plant"];      //assigning datasource to the dropdownlist
                ddlPlant.DataBind();  //binding dropdownlist
                ddlPlant.Items.Insert(0, new ListItem("------Select------", ""));
            }
            catch (Exception ex)
            {
                lblError.Text = "BindPlant" + ex.ToString();
            }
        }

        private void GetTransactionID()
        {
            try
            {
                ds = obj.GetTransactionMaxPettyCash(FormID.ToString());
                lblMaxTransactionNo.Text = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();
                ViewState["MaterialMaxID"] = ds.Tables["MaterialMaxID"].Rows[0]["TransactionID"].ToString();

                //ds = obj.GetTransactionMax();
                //lblMaxTransactionID.Text = ds.Tables["MaterialMasterTrID"].Rows[0]["TransactionID"].ToString();
            }
            catch (Exception ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "GetTransactionID" + ex.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                txtBaseQuantity.BackColor = System.Drawing.Color.White;
                ddlPlant.BackColor = System.Drawing.Color.White;
                ddlStorageLocation.BackColor = System.Drawing.Color.White;
                txtMaterial.BackColor = System.Drawing.Color.White;
                txtDescription.BackColor = System.Drawing.Color.White;
                if (txtBaseQuantity.Text == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Base Quantity should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblUpError.Focus();
                    error.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtBaseQuantity.BackColor = System.Drawing.Color.Red;
                }
                else if (txtMaterial.Text == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Material should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblUpError.Focus();
                    error.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtMaterial.BackColor = System.Drawing.Color.Red;
                }
                else if (txtDescription.Text == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Material Description should not be left blank";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblUpError.Focus();
                    error.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtDescription.BackColor = System.Drawing.Color.Red;
                }
                else if (ddlPlant.SelectedValue == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please Select any plant";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblUpError.Focus();
                    error.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlPlant.BackColor = System.Drawing.Color.Red;
                }
                else if (ddlStorageLocation.SelectedValue == "")
                {

                    lblmessage.Text = "";
                    lblUpError.Text = "Please Select any Storage sLocation";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblUpError.Focus();
                    error.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    ddlStorageLocation.BackColor = System.Drawing.Color.Red;
                }
                else if (txtBaseQuantity.Text != lblSum.Text)
                {
                    lblmessage.Text = "";
                    lblUpError.Text = "Line item Quantity in not equal to Header Base Quantity";
                    sucess.Visible = false;
                    error.Visible = true;
                    lblUpError.Focus();
                    error.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                }
                else
                {
                    string Notification = "";

                    for (int i = 0; i <= ddlNotification.Items.Count - 1; i++)
                    {
                        if (ddlNotification.Items[i].Selected)
                        {
                            if (Notification == "") { Notification = ddlNotification.Items[i].Value; }
                            else { Notification += "," + ddlNotification.Items[i].Value; }
                        }

                    }

                    string conString = ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString;
                    SqlCommand cmd = new SqlCommand("SP_SYS_Create_BOM_Approval");
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            string Approval = ViewState["HOD"].ToString() ;
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@TransactionMain", ddlPlant.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@MaterialNo", txtMaterial.Text.ToString());
                            cmd.Parameters.AddWithValue("@MaterialDesc", txtDescription.Text.ToString());
                            cmd.Parameters.AddWithValue("@Plant", ddlPlant.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@StorageLocation", ddlStorageLocation.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@ProdLotSizeFrom", txtProductionLotSizefrom.Text.ToString());
                            cmd.Parameters.AddWithValue("@ProdLotSizeTo", txtProductionLotSizeTo.Text.ToString());
                            cmd.Parameters.AddWithValue("@ProductionVersion", txtProductionVersion.Text.ToString());
                            cmd.Parameters.AddWithValue("@ProdVersionDesc", txtProductionVersionDescription.Text.ToString());
                            cmd.Parameters.AddWithValue("@BOMValidFrom", txtBOMValidFrom.Text.ToString());
                            cmd.Parameters.AddWithValue("@BOMValidTo", txtBOMValidTo.Text.ToString());
                            decimal yourValue = Convert.ToDecimal(txtBaseQuantity.Text);
                            cmd.Parameters.AddWithValue("@QTY", yourValue);
                            cmd.Parameters.AddWithValue("@APPROVAL", Approval.ToString());
                            cmd.Parameters.AddWithValue("@REVIEWER", "");
                            cmd.Parameters.AddWithValue("@MDA", ddlEmailMDA.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@CreatedBy", Session["User_Name"].ToString());
                            cmd.Parameters.AddWithValue("@Remarks", txtRemarksReview.Text.ToString());
                            sda.SelectCommand = cmd;
                            adp.SelectCommand = cmd;
                            adp.Fill(ds, "Message");
                            sucess.Visible = true;

                            string message = ds.Tables["Message"].Rows[0]["Dec"].ToString().Trim();
                            lblMaxTransactionID.Text = ds.Tables["Message1"].Rows[0]["TransactionID"].ToString().Trim();
                            lblmessage.Text = message + " # " + lblMaxTransactionID.Text;
                            insertLineItem();
                            lblmessage.Focus();
                            error.Visible = false;
                            lblmessage.Focus();
                            Page.MaintainScrollPositionOnPostBack = false;
                            EmailWorkSendFirstApproval();
                            lblMaxTransactionID.Text = "";
                            GetTransactionID();

                            for (int i = 0; i < ddlNotification.Items.Count; i++)
                            {
                                ddlNotification.Items[i].Selected = true;
                                ddlNotification.Items[i].Attributes.Add("disabled", "disabled");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lblError.Text = "btnSave_Click" + ex.ToString();
            }
        }

        protected void btnSaveSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {

 
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {

        }

        protected void btnMDA_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        decimal sum = 0;
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.EmptyDataRow)
                {

                    conn.Close();

                    cmd.CommandText = "";
                    //Find the DropDownList in the Row
                    DropDownList ddlStLoc = (e.Row.FindControl("ddlStLoc") as DropDownList);
                    ddlStLoc.DataSource = GetData("SP_StorageLocationPlantWise");
                    ddlStLoc.DataTextField = "Description";
                    ddlStLoc.DataValueField = "StorageLocationcode";
                    ddlStLoc.DataBind();
                    //Add Default Item in the DropDownList
                    ddlStLoc.Items.Insert(0, new ListItem("Please select"));



                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {

                    conn.Close();

                    cmd.CommandText = "";
                    //Find the DropDownList in the Row
                    DropDownList ddlStLoc = (e.Row.FindControl("ddlStLoc") as DropDownList);
                    ddlStLoc.DataSource = GetData("SP_StorageLocationPlantWise");
                    ddlStLoc.DataTextField = "Description";
                    ddlStLoc.DataValueField = "StorageLocationcode";
                    ddlStLoc.DataBind();
                    //Add Default Item in the DropDownList
                    ddlStLoc.Items.Insert(0, new ListItem("Please select"));
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "OnRowDataBound" + ex.ToString();
            }
        }

        protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlStorageLocation.DataSource = GetData("SP_StorageLocationPlantWise");
                ddlStorageLocation.DataTextField = "Description";
                ddlStorageLocation.DataValueField = "StorageLocationcode";
                ddlStorageLocation.DataBind();
                //Add Default Item in the DropDownList
                ddlStorageLocation.Items.Insert(0, new ListItem("------Select------", ""));
                GridView1.DataSource = (DataTable)ViewState["BOMGrid"];
                GridView1.DataBind();
                lblgridError.Text = "";
                for (int i = 0; i < ddlNotification.Items.Count; i++)
                {
                    ddlNotification.Items[i].Selected = true;
                    ddlNotification.Items[i].Attributes.Add("disabled", "disabled");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "ddlPlant_SelectedIndexChanged" + ex.ToString();
            }
        }

        protected void OnDataBound(object sender, EventArgs e)
        {
            try
            {

                conn.Close();

                cmd.CommandText = "";
                //Find the DropDownList in the Row
                DropDownList ddlCountries = GridView1.FooterRow.FindControl("ddlStLoc") as DropDownList;
                ddlCountries.DataSource = GetData("SP_StorageLocation");
                ddlCountries.DataTextField = "Description";
                ddlCountries.DataValueField = "StorageLocationcode";
                ddlCountries.DataBind();
                //Add Default Item in the DropDownList
                ddlCountries.Items.Insert(0, new ListItem("Please select"));
            }
            catch (Exception ex)
            {
                lblError.Text = "OnDataBound" + ex.ToString();
            }
        }

        protected void insertLineItem()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["BOMGrid"];

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    dt.Rows[i]["TransactionID"] = lblMaxTransactionID.Text;
                    dt.AcceptChanges();
                }

                if (dt.Rows.Count > 0)
                {
                    string consString = ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(consString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name
                            sqlBulkCopy.DestinationTableName = "dbo.tbl_BOM_Approval_ITEM";

                            //[OPTIONAL]: Map the DataTable columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("Sno", "Sequance");
                            sqlBulkCopy.ColumnMappings.Add("TransactionID", "TransactionID");
                            sqlBulkCopy.ColumnMappings.Add("ComponentType", "ComType");
                            sqlBulkCopy.ColumnMappings.Add("Material", "MaterialNo");
                            sqlBulkCopy.ColumnMappings.Add("MaterialDescription", "MaterialDesc");
                            sqlBulkCopy.ColumnMappings.Add("Quantity", "QTY");
                            sqlBulkCopy.ColumnMappings.Add("UOM", "UOM");
                            sqlBulkCopy.ColumnMappings.Add("StoreLocation", "StorageLocation");
                            con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "insertLineItem" + ex.ToString();
            }
        }

        #region methodEmailWorks

        private void EmailWorkSendFirstApproval()
        {

            ds = obj.MailForwardUserToApprover(lblMaxTransactionID.Text, FormID.ToString());

            if (ds.Tables["MailForwardUserToApprover"].Rows.Count > 0)
            {
                DataTableReader reader = ds.Tables["MailForwardUserToApprover"].CreateDataReader();
                while (reader.Read())
                {
                    url = HttpContext.Current.Request.Url.AbsoluteUri + "?TransactionNo=" + ViewState["MaterialMaxID"] + "";
                    TransactionID = reader["TransactionID"].ToString();
                    FormCode = reader["FormID"].ToString();
                    UserName = reader["user_name"].ToString();
                    UserEmail = reader["user_email"].ToString(); //ViewState["SessionUser"].ToString();
                    EmailSubject = "BOM Approval Request – Form ID # " + lblMaxTransactionID.Text.ToString() + "";
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br> <br>   " + ViewState["SessionUser"].ToString() + " has sent you a New BOM Approval Request against  Form ID # " + lblMaxTransactionID.Text.ToString() + " for approval. <br><br> Your kind approval is required on the following URL: <br><br><a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br> This is an auto-generated email from IS Dashboard, <br>you do not need to reply to this message.<br>" +
                        "<br>BOM Approval Application <br> Information Systems Dashboard";
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

        private void EmailWorkFirstHaracheyReviwer()
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
                        ViewState["Status"] = HierachyCategoryStatus.ToString();
                    }

                }

            }
            else
            {

            }
        }

        private void EmailWorkFirstHaracheyMDA()
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
                    EmailBody = "Dear Mr " + "" + UserName.ToString() + ",<br><br>  SAP material code " + txtBillOfMaterial.Text.Trim() + " has been issued against  new material creation request Form ID # " + lblMaxTransactionID.Text.ToString() + " <br><br> The form can be reviewed at the following URL:<br> <a href =" + url.ToString() + ">" + url.ToString() + "</a> <br> <br>  This is an auto-generated email from IS Dashboard,<br> you do not need to reply to this message." +
                    "<br>Material Master Application <br> Information Systems Dashboard";
                    SessionUser = Session["User_Name"].ToString();
                    DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    InsertEmail();

                    lblmessage.Text = "SAP Material Code " + txtBillOfMaterial.Text.Trim() + " has been saved against  Form ID # " + lblMaxTransactionID.Text;

                    lblmessage.ForeColor = System.Drawing.Color.Green;
                    conn.Close();
                    sucess.Visible = true;
                    error.Visible = false;
                    lblmessage.Focus();
                    Page.MaintainScrollPositionOnPostBack = false;
                    txtBillOfMaterial.BackColor = System.Drawing.Color.White;
                    Page.MaintainScrollPositionOnPostBack = false;
                    ViewState["Status"] = HierachyCategoryStatus.ToString();
                }

            }
            else
            {

            }
        }

        private void EmailWorkApproved()
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

        private void EmailWorkFormForwarding()
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

        private void EmailWorkReject()
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

        protected void InsertEmail()
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

        #endregion


        private void BindsysApplicationStatus()
        {
            try
            {
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
                    txtBillOfMaterial.Enabled = false;

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
                    //ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
                }
            }
            catch (SqlException ex)
            {
                dvemaillbl.Visible = true;
                lblError.Text = "User Detail" + ex.ToString();
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
            }
            catch (Exception ex)
            {
                lblError.Text = "GetHarcheyID" + ex.ToString();
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
        private void BindUser()
        {
            try
            {
                cmd.CommandText = "SELECT * FROM tblEmailSequanceWise where FormID = '" + FormID.ToString() + "' order by Sequance asc";
                //cmd.CommandText = "SELECT * FROM tbluser where user_name = 'adnan.yousufzai'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                conn.Open();
                ddlNotification.DataSource = cmd.ExecuteReader();
                ddlNotification.DataTextField = "DisplayName";
                ddlNotification.DataValueField = "user_name";
                ddlNotification.DataBind();
                conn.Close();
                for (int i = 0; i < ddlNotification.Items.Count; i++)
                {
                    ddlNotification.Items[i].Selected = true;
                    ddlNotification.Items[i].Attributes.Add("disabled", "disabled");
                }

                cmd.CommandText = "SELECT user_name,DisplayName FROM tbluserMDA where FormName = 'DSA'";
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
            catch (Exception ex)
            {
                lblError.Text = "BindUser" + ex.ToString();
            }
        }

    }

}