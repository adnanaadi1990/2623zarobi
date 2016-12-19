using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITLDashboard.Modules.HR
{
    public partial class MonthlyAttendance : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        SqlCommand cmd = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
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
                getEmployee();

            }


        }


        protected void getEmployee()
        {
            cmd.CommandText = @"select E.EID as EmployeeID, CONVERT(VARCHAR(500), CONVERT(VARCHAR(500), E.EName)) as EmployeeName from Employee as E
                            left outer join Department as D on E.DID = D.DID";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            ddlEmpID.DataSource = cmd.ExecuteReader();
            ddlEmpID.DataTextField = "EmployeeName";
            ddlEmpID.DataValueField = "EmployeeID";
            ddlEmpID.DataBind();
            conn.Close();
            ddlEmpID.Items.Insert(0, new ListItem("------------Select------------", "0"));
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {

                txtDateFrom.BackColor = System.Drawing.Color.White;
                txtDateTo.BackColor = System.Drawing.Color.White;
                if (ddlEmpID.SelectedValue == "0")
                {
                    lblError.Text = "Fill all required field!.";
                    ddlEmpID.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtDateFrom.Text == "")
                {
                    lblError.Text = "Fill all required field!.";
                    txtDateFrom.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }
                else if (txtDateTo.Text == "")
                {
                    lblError.Text = "Fill all required field!.";
                    txtDateTo.BackColor = System.Drawing.Color.Red;
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                else
                {
                    DateTime fromdate = DateTime.Parse(Convert.ToDateTime(txtDateFrom.Text).ToShortDateString());
                    DateTime todate = DateTime.Parse(Convert.ToDateTime(txtDateTo.Text).ToShortDateString());
                    if (todate < fromdate)
                    {
                        lblError.Text = "(Date To) should not be less then (Date From)";
                        return;
                    }
                    DataSet ds = new DataSet();

                    ds.Clear();
                    //DataTable DT = GetSPResult();
                    string query = "";
                    query = "EXEC SP_getMontlyAttendenceMain " + "@DateFrom ='" + txtDateFrom.Text + "', @DateTo ='" + txtDateTo.Text + "', @Report_type ='" + rbRptType.SelectedValue.ToString() + "' , @EID = '" + ddlEmpID.SelectedValue + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                    ds.Clear();
                    Adapter.Fill(ds, "Result");
                    if (ds.Tables["Result"].Rows.Count > 0)
                    {
                      //  ReportViewer1.Visible = true;
                        ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables["Result"]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                        ViewState["EID"] = ds.Tables["Result1"].Rows[0]["EmployeID"].ToString().Trim();

                        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(DateWise);
                        this.ReportViewer1.LocalReport.Refresh();


                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        //    string path = Path.Combine(Server.MapPath("~/ReportHR"), "ReportMain.rdlc");
                        //   rptViewer.LocalReport.ReportPath = path;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath(@"~\Modules\ReportHR\ReportMain.rdlc");
                        //LocalReport report = new LocalReport();
                        // report.ReportPath = Server.MapPath(@"~\Modules\ReportHR\ReportMain.rdlc");



                        // ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        //ReportViewer1.LocalReport.ReportPath = ("ReportMain.rdlc");
                        // ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Dashboard/Modules/ReportHR/ReportMain.rdlc");

                        ReportParameter EID = new ReportParameter("EID", ds.Tables["Result1"].Rows[0]["EmployeID"].ToString().Trim());
                        ReportViewer1.LocalReport.SetParameters(EID);

                        ReportParameter ENAME = new ReportParameter("ENAME", ds.Tables["Result1"].Rows[0]["EmployeName"].ToString().Trim());
                        ReportViewer1.LocalReport.SetParameters(ENAME);

                        ReportParameter DNAME = new ReportParameter("DNAME", ds.Tables["Result1"].Rows[0]["Department"].ToString().Trim());
                        ReportViewer1.LocalReport.SetParameters(DNAME);

                        ReportParameter To = new ReportParameter("To", txtDateTo.Text);
                        ReportViewer1.LocalReport.SetParameters(To);

                        ReportParameter From = new ReportParameter("From", txtDateFrom.Text);
                        ReportViewer1.LocalReport.SetParameters(From);

                        ReportViewer1.LocalReport.Refresh();

                        //byte[] bytes = ReportViewer1.LocalReport.Render("PDF");

                        ////display pdf in browser
                        //Response.AddHeader("Content-Disposition", "inline; filename=MyReport.pdf");
                        //Response.ContentType = "application/pdf";
                        //Response.BinaryWrite(bytes);
                        //Response.End();

                        byte[] file = ReportViewer1.LocalReport.Render("PDF");

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "inline;filename=Test.pdf");
                        Response.Buffer = true;
                        Response.Clear();
                        Response.BinaryWrite(file);
                        Response.End();
                        ddlEmpID.SelectedIndex = -1;
                        txtDateFrom.Text = "";
                        txtDateTo.Text = "";
                        rbRptType.SelectedValue = "NormalDays";
                        lblError.Text = "";
                    }

                    else
                    {
                        lblError.Text = "No Data Found!..";
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
        }

        void DateWise(object sender, SubreportProcessingEventArgs e)
        {
            string test = e.Parameters["Date"].Values[0].ToString();
            string query = @"SELECT A_In.EID, E.EName,CAST(CheckTime AS date) as Date, D.DName, A_In.CID, A_In.BID, FORMAT(A_In.CheckTime, 'hh:mm tt') AS TimeIn, FORMAT
                             ((SELECT  MIN(CheckTime) AS Expr1 FROM   Attendance AS A_Out
                                 WHERE (CheckType LIKE 'O') AND (EID = A_In.EID) AND (CheckTime > A_In.CheckTime)), 'hh:mm tt') AS TimeOut
                                 FROM Attendance AS A_In LEFT OUTER JOIN
                                 Employee AS E ON A_In.EID = E.EID LEFT OUTER JOIN
                                  Department AS D ON E.DID = D.DID
                                  WHERE (A_In.CheckType LIKE 'I') AND (A_In.EID =  '" + ViewState["EID"].ToString() + "' and CAST(CheckTime AS date)  = '" + test.ToString() + "' )  ORDER BY A_In.EID, CAST(CheckTime AS Time) asc";
            SqlCommand cmd = new SqlCommand(query, conn);
            // conn.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            //    string test = e.Parameters["Date"].Values[0].ToString();
            //    cmd.Parameters.AddWithValue("@Date", test.ToString());
            //cmd.Parameters.AddWithValue("@CompanyId", Session["CompanyID"].ToString());
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            DataSet ds11 = new DataSet();
            ds11.Clear();
            Adapter.Fill(ds11, "SubReport");

            ReportDataSource rptDataSourse = new ReportDataSource("DataSet1", ds11.Tables["SubReport"]);
            e.DataSources.Add(rptDataSourse);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlEmpID.SelectedIndex = -1;
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            rbRptType.SelectedValue = "NormalDays";

        }
    }
}