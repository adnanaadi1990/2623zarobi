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


namespace ITLDashboard
{
    public partial class Default : System.Web.UI.Page
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
                BindsysApplicationStatus();
            }
        }
        private void BindsysApplicationStatus()
        {
            ds.Clear();
            cmd.CommandText = "";
            cmd.CommandText = @"SELECT 
T.FormID,
T.TransactionID,
T.CreatedBy,
--Replace(T.RoughtingUserID, '.' ,' ')  as RoughtingUserID,
Replace(isnull(T3.DisplayName,T.RoughtingUserID), '.' ,' ')  as RoughtingUserID,
CASE T1.Status 
	WHEN '01' THEN 'Created'
    WHEN '02' THEN 'Approved'
	WHEN '03' THEN 'Reviewed'
	WHEN '04' THEN 'Updated'
	WHEN '05' THEN 'Email Checked'
	when '00' then 'Rejected'
    ELSE 'Pending'
	end as Status,
T1.Remarks,
CASE T2.Description 
	WHEN 'User' THEN 'User'
    WHEN 'Approver' THEN 'Approver'
	WHEN 'Reviewer' THEN 'Reviewer'
	WHEN 'MDA' THEN 'Administrator'
	WHEN 'Notification' THEN 'Email Notification'
	
	end as HierarchyCat,
--T2.Description as HierarchyCat,
T1.DateTime
FROM 
sysWorkFlow T
left outer join sysApplicationStatus T1 ON T.FormID = T1.FormID and T.TransactionID = T1.TransactionID AND T.HierachyCategory = T1.HierachyCategory and T.RoughtingUserID = T1.RoughtingUserID
LEFT OUTER JOIN SYS_HierarchyCateguory T2 ON T.HierachyCategory = T2.HCID
LEFT OUTER JOIN tbluser T3 ON T.RoughtingUserID = T3.user_name
where T.FormID = '301' order by T.TransactionID asc ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            adp.Fill(ds, "aa");

            grdWStatus.DataSource = ds.Tables["aa"];
            grdWStatus.DataBind();
            grdWStatus.Visible = false;
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            grdWStatus.PageIndex = e.NewPageIndex;
            this.BindsysApplicationStatus();
        }
    }
}