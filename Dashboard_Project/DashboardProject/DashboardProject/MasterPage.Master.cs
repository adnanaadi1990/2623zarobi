using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Configuration;
using System.Collections;
using System.Text;
using System.IO;
using ITLDashboard.Classes;
namespace ITLDashboard
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataTable dtcon = new DataTable();
        DataSet ds = new DataSet();
        internal class SiteDataItem
        {
            private string text;
            private int id;
            private int parentId;

            public string Text
            {
                get { return text; }
                set { text = value; }
            }


            public int ID
            {
                get { return id; }
                set { id = value; }
            }

            public int ParentID
            {
                get { return parentId; }
                set { parentId = value; }
            }

            public SiteDataItem(int id, int parentId, string text)
            {
                this.id = id;
                this.parentId = parentId;
                this.text = text;
            }

        }
        protected void page_Init(object sender, EventArgs e)
        {
            BindToDataSet(RadTreeView1);
        }

        private static void BindToDataSet(RadTreeView treeView)
        {//HttpContext.Current.Session["Module"] == null || 
            try
            {
                string value = "";
                if (HttpContext.Current.Session["Application"] == null)
                {
                    //  HttpContext.Current.Session["Application"] = "";
                    HttpContext.Current.Session["Application"] = "";
                }
                else
                {
                    value = "%" + HttpContext.Current.Session["Application"].ToString().ToString() + "%";
                    if (value == "%%")
                    {
                        //  HttpContext.Current.Session["Application"] = "";
                        value = "";
                    }
                    SqlCommand cmd = new SqlCommand();
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
                    SqlDataAdapter DAdapter = new SqlDataAdapter();
                    //cmd.CommandText = "Exec SP_Call_DashboardMenu" + " @Module = '" + HttpContext.Current.Session["Module"].ToString().ToString() + "', " +
                    //       " @Application = '" + HttpContext.Current.Session["Application"].ToString().ToString() + "'";
                    cmd.CommandText = "Exec SP_Call_DashboardMenu" + " @Application = '" + value + "'";
                    // cmd.Parameters.AddWithValue("@Form_Name", Form_Name.ToString());
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet DS = new DataSet();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    da.SelectCommand = cmd;
                    da.Fill(DS, "Message");


                    treeView.DataTextField = "FormName";
                    treeView.DataFieldID = "ID";
                    treeView.DataValueField = "ID";
                    treeView.DataFieldParentID = "ParentID";
                    treeView.DataSource = DS.Tables["Message"];
                    treeView.DataBind();
                    // HttpContext.Current.Session["Application"] = "";
                }
            }
            catch (Exception ex)
            {
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User_Name"] == null)
                {
                    HttpContext con = HttpContext.Current;
                    con.Request.Url.ToString();
                    Session["Test"] = con.Request.Url.ToString();
                    Response.Redirect("ITLLogin.aspx");
                }
                else
                {
                    HttpContext con = HttpContext.Current;
                    con.Request.Url.ToString();
                    Session["Test"] = con.Request.Url.ToString();
                    //  BindToDataSet(RadTreeView1);
                    lnkMain.Visible = true;
                    btnTcodeSearch.Visible = false;
                    txtSearch.Visible = false;
                    dvSearchbtn.Visible = false;
                    RadTreeView1.Visible = false;
                    if (Request.QueryString["TransactionNo"] != null || Request.QueryString["TransactionNo"] != null || Request.QueryString["User"] != null)
                    {
                        
                        lnkMain.Visible = true;
                        btnTcodeSearch.Visible = false;
                        txtSearch.Visible = false;
                        dvSearchbtn.Visible = false;
                        RadTreeView1.Visible = false;
                    }
                    else
                    {
                        getUserDetail();
                        btnTcodeSearch.Visible = true;
                        txtSearch.Visible = true;
                        lnkMain.Visible = true;
                        dvSearchbtn.Visible = true;
                        RadTreeView1.Visible = true;
                    }

                }
            }
        }
        private void getUserDetail()
        {
            ds = obj.getUserDetail(Session["User_Name"].ToString());
            if (ds.Tables["tbluser_DisplayName"].Rows.Count > 0)
            {
                lblUSerName.Text = "Welcome:  " + ds.Tables["tbluser_DisplayName"].Rows[0]["DisplayName"].ToString();
                //ViewState["HID"] = ds.Tables["HID"].Rows[0]["HierachyCategory"].ToString();
            }
            else
            {
                Response.Redirect("~/NoRightsUser.aspx");
            }
        }

        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Session["User_Name"] = "";
            Session["Test"] = "";
            Session.Abandon();
            Session["Application"] = "";
            Session["Module"] = "";
            Response.Redirect("~/ITLLogin.aspx");


        }
        protected void btnTcodeSearch_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());

            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text.ToString()))
                {
                    string Client_ID, SearchForm, PID, FID, path;
                    SearchForm = txtSearch.Text;
                    //string ParentName = Convert.ToString(Session["ParentName"]);
                    //int FormID = Convert.ToInt32(Session["FormID"]);

                    DataTable dt = new DataTable();

                    SqlDataAdapter adp = new SqlDataAdapter("select * from sys_forms where TCode =  UPPER('" + SearchForm + "')", conn);
                    adp.Fill(dt);
                    Client_ID = (dt.Rows[0]["Client_ID"].ToString());
                    PID = (dt.Rows[0]["ParentForm_ID"].ToString());
                    FID = (dt.Rows[0]["Form_ID"].ToString());
                    // ParentID = PID.ToString();
                    // FormID = FID.ToString();
                    //Session["ClientID"] = Client_ID;
                    //Session["ParentID"] = ParentID;
                    //  Session["FormID"] = FormID;
                    path = (dt.Rows[0]["path"].ToString());


                    Response.Redirect(path.ToString(), false);

                }
            }

            catch (Exception exe)
            {



            }
        }
        protected void RadTreeView1_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            if (RadTreeView1.SelectedNode.Value == "1")
            {
                Response.Redirect("~/Main.aspx#APPLICATION");
            }
            string ParentID = RadTreeView1.SelectedNode.ParentNode.Value.ToString();
            string FormID = RadTreeView1.SelectedNode.Value.ToString();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());

            try
            {


                string Client_ID, SearchForm, PID, FID, path;
                SearchForm = RadTreeView1.SelectedNode.ToString();

                //string ParentName = Convert.ToString(Session["ParentName"]);
                //int FormID = Convert.ToInt32(Session["FormID"]);




                DataTable dt = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter("select * from NewMainHierarchy1 where ParentID = '" + ParentID + "' and ID = '" + FormID + "'", conn);
                adp.Fill(dt);
                Client_ID = (dt.Rows[0]["Client_ID"].ToString());
                PID = (dt.Rows[0]["ParentID"].ToString());
                FID = (dt.Rows[0]["ID"].ToString());
                // ParentID = PID.ToString();
                // FormID = FID.ToString();
                //Session["ClientID"] = Client_ID;
                //Session["ParentID"] = ParentID;
                //  Session["FormID"] = FormID;
                path = (dt.Rows[0]["path"].ToString());
                Response.Redirect(path.ToString(), false);


            }

            catch (Exception exe)
            {



            }
        }
        protected void lnkMain_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main.aspx#APPLICATION");

        }

    }
}