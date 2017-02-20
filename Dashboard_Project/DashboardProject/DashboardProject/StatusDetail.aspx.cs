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
        public string val = "";
        public string PathString = "";
        public string Script = "";
        public string TranID = "";
        public string FormName = "";
        public string value = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Pass();
            }
        }
        protected void Pass()
        {
            if (Request.QueryString["TransactionNo"] != null && Request.QueryString["FormName"] != null)
            {
                TranID = Request.QueryString["TransactionNo"].ToString();
                FormName = Request.QueryString["FormName"].ToString();

                if (HttpContext.Current.Session["Application"] == null)
                {
                    //  HttpContext.Current.Session["Application"] = "";
                    HttpContext.Current.Session["Application"] = "";
                }
                else
                {
                    value = "";
                }
                ds = obj.getFormNameByFormID(value.ToString(), FormName.ToString());
                if (ds.Tables["SP_FormDetailByFormID"].Rows.Count > 0)
                {
                    PathString = ds.Tables["SP_FormDetailByFormID"].Rows[0]["Path"].ToString();
                    Script = ds.Tables["SP_FormDetailByFormID"].Rows[0]["Script"].ToString();
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString))
                    {
                        using (SqlCommand cmdScript = new SqlCommand(Script))//
                        {
                            cmdScript.CommandType = CommandType.Text;
                            cmdScript.Parameters.AddWithValue("@TNo", TranID.ToString());
                            cmdScript.Connection = connection;

                            adp.SelectCommand = cmdScript;
                            dt.Clear();
                            adp.Fill(dt);
                            DataTableReader reader = dt.CreateDataReader();
                            while (reader.Read())
                            {
                                reader.Read();
                                val = reader["TransactionMain"].ToString();
                                Response.Redirect(PathString.Trim() + "?TransactionNo=" + val);
                            }
                        }
                    }
                }
            }
        }
    }
<<<<<<< HEAD
}
=======
}//Test 2 :) How r u what's up; test one
>>>>>>> refs/remotes/origin/master
