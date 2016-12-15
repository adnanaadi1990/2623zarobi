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
using ITLDashboard.Classes;
using System.Net;
using System.Net.Mail;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.DirectoryServices;
using System.Security.Principal;
using System.Web.UI.HtmlControls;

namespace ITLDashboard
{
    public partial class StatusDetail : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsEmail = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        ComponentClass obj = new ComponentClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["TransactionNo"] != null && Request.QueryString["FormName"] != null)
                {
                    string TranID = Request.QueryString["TransactionNo"].ToString();
                    string FormName = Request.QueryString["FormName"].ToString();
                    ds = obj.BindsysApplicationStatus(TranID.ToString(), FormName.ToString());
                    grdWStatus.DataSource = ds.Tables["BindsysApplicationStatus"];
                    grdWStatus.DataBind();
                    grdWStatus.Visible = true;
                }
            }
        }
    }
}