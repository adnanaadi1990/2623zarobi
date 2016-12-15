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
using Telerik.Web.UI;
using ITLDashboard.Classes;

namespace ITLDashboard.Modules.Reports
{
    public partial class AssetCreationFromReport : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ITLConnection"].ConnectionString.ToString());
        ComponentClass obj = new ComponentClass();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable table = new DataTable();
        public string FormID = "ACFA01";
        public int Coint = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User_Name"] == null)
                {
                    Response.Redirect("~/SingleLogin.aspx");
                }
                BindPlant();
                BindCostCenter();
                BindLocation();
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCostCenter.SelectedValue == "0")
                {
                    ddlCostCenter.SelectedValue = "";
                }
                if (ddlLocation.SelectedValue == "0")
                {
                    ddlLocation.SelectedValue = "";
                }
                if (ddlPlant.SelectedValue == "0")
                {
                    ddlPlant.SelectedValue = "";
                }

                lblError.Text = "";
                if (txtFormIDto.Text == "")
                {
                    txtFormIDto.Text = txtFormIDfrom.Text;
                }
                RadGrid1.Visible = true;
                ds.Clear();
                cmd.CommandText = "";
                cmd.CommandText = @"SP_AssetCreationFrom";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@FormIDFrom", txtFormIDfrom.Text);
                cmd.Parameters.AddWithValue("@FormIDto", txtFormIDto.Text);
                cmd.Parameters.AddWithValue("@userName", txtUN.Text);
                cmd.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text);
                cmd.Parameters.AddWithValue("@CostCenter", ddlCostCenter.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Plant", ddlPlant.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Location", ddlLocation.SelectedValue.ToString());

                adp.SelectCommand = cmd;
                adp.Fill(dt);
                ViewState["data"] = dt;
                RadGrid1.DataSource = dt;
                RadGrid1.DataBind();
                RadGrid1.Visible = true;
                ClearInputs(Page.Controls);
            }

            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
            finally
            { conn.Close(); }
        }

        private void BindPlant()
        {

            ds = obj.getPlantDistinct();
            ddlPlant.DataTextField = ds.Tables["getPlantDistinct"].Columns["Description"].ToString(); // text field name of table dispalyed in dropdown
            ddlPlant.DataValueField = ds.Tables["getPlantDistinct"].Columns["PlantId"].ToString();             // to retrive specific  textfield name 
            ddlPlant.DataSource = ds.Tables["getPlantDistinct"];      //assigning datasource to the dropdownlist
            ddlPlant.DataBind();  //binding dropdownlist
            ddlPlant.Items.Insert(0, new ListItem("------Select------", ""));
        }

        private void BindCostCenter()
        {
            ds = obj.BindCostCenter();
            ddlCostCenter.DataTextField = ds.Tables["tbl_AssetCostCenter"].Columns["CostCenterDesc"].ToString(); // text field name of table dispalyed in dropdown
            ddlCostCenter.DataValueField = ds.Tables["tbl_AssetCostCenter"].Columns["CostCentercode"].ToString();             // to retrive specific  textfield name 
            ddlCostCenter.DataSource = ds.Tables["tbl_AssetCostCenter"];      //assigning datasource to the dropdownlist
            ddlCostCenter.DataBind();  //binding dropdownlist
            ddlCostCenter.Items.Insert(0, new ListItem("------Select------", ""));
        }

        private void BindLocation()
        {
            ds = obj.BindLocation();
            ddlLocation.DataTextField = ds.Tables["tbl_Assetlocation"].Columns["CurrentLocationdesc"].ToString(); // text field name of table dispalyed in dropdown
            ddlLocation.DataValueField = ds.Tables["tbl_Assetlocation"].Columns["CurrentLocationcode"].ToString();             // to retrive specific  textfield name 
            ddlLocation.DataSource = ds.Tables["tbl_Assetlocation"];      //assigning datasource to the dropdownlist
            ddlLocation.DataBind();  //binding dropdownlist
            ddlLocation.Items.Insert(0, new ListItem("------Select------", ""));
        }


        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("20"));
                PageSizeCombo.FindItemByText("20").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("200"));
                PageSizeCombo.FindItemByText("200").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //Populate the Radgrid      
        }

        protected void btnBind_Click(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_GroupsChanging(object sender, Telerik.Web.UI.GridGroupsChangingEventArgs e)
        {
            dt = (DataTable)ViewState["data"];
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            dt = (DataTable)ViewState["data"];
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();

            if (e.CommandName == "Select")
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    string str = item["FormID"].Text;

                    RadWindow win = new RadWindow();
                    win.ID = "window1";
                    win.VisibleOnPageLoad = true;
                    win.Width = 1000;
                    win.EnableViewState = false;
                    win.NavigateUrl = "~/StatusDetail.aspx?TransactionNo=" + str.ToString() + "&FormName=" + FormID.ToString() + "";
                    RadWindowManager1.Controls.Add(win);
                }

            }

        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
            {
                GridCommandItem cmdItem = (GridCommandItem)RadGrid1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;
                ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;
            }
        }

        void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;

                if (ctrl is DropDownList)
                    ((DropDownList)ctrl).SelectedIndex = 0;
                if (ctrl is ListBox)
                    ((ListBox)ctrl).SelectedIndex = -1;
                ClearInputs(ctrl.Controls);
            }
        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {

            foreach (TableCell cell in e.Item.Cells)
            {
                cell.Style["font-size"] = "12px";
                cell.Style["font-family"] = "Arial";//"Courier New"; 
                cell.Style["vertical-align"] = "middle";
                cell.Style["font-weight"] = "normal";
                cell.Style["text-align"] = "left";
                cell.Style["background-color"] = "#f0f7f0";
                cell.Style["color"] = "Black";
                cell.Style["text-decoration"] = "none";
            }

            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem headerItem = (GridHeaderItem)e.Item;
                headerItem.Style["font-size"] = "14px";
                headerItem.Style["font-family"] = "Arial";//"Courier New"; 
                headerItem.Style["vertical-align"] = "middle";
                headerItem.Style["font-weight"] = "bold";
                headerItem.Style["text-align"] = "left";
                headerItem.Style["background-color"] = "#f0f7f0";
                headerItem.Style["color"] = "Black";
                headerItem.Style["text-decoration"] = "none";

                foreach (TableCell cell in headerItem.Cells)
                {
                    cell.Style["font-weight"] = "normal";
                    cell.Style["text-align"] = "left";
                    cell.Style["font-size"] = "12px";
                    cell.Style["font-family"] = "Arial";
                    cell.Style["vertical-align"] = "middle";
                    cell.Style["background-color"] = "#f0f7f0";
                    cell.Style["color"] = "Black";
                    cell.Style["text-decoration"] = "none";
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string url = HttpContext.Current.Request.Url.ToString();
                Response.Redirect(url.ToString());
            }

            catch (SqlException oException)
            {
                lblError.Text = oException.ToString();
            }
        }

        private void ApplyStylesToPDFExport(GridTableView view)
        {
            // Get access to the header of the grid
            GridItem headerItem = view.GetItems(GridItemType.Header)[0];

            // Apply some css style to the header
            foreach (TableCell cell in headerItem.Cells)
            {
                cell.Style["font-weight"] = "bold";
                cell.Style["font-family"] = "Calibri";
                cell.Style["text-align"] = "left";
                cell.Style["vertical-align"] = "middle";
                cell.Style["font-size"] = "12px";
            }

            // Get access to the date of the grid
            GridItem[] dataItems = view.GetItems(GridItemType.Item);

            // Apply some css style to the data items
            foreach (GridItem item in dataItems)
            {
                foreach (TableCell cell in item.Cells)
                {
                    cell.Style["font-family"] = "Verdana";
                    cell.Style["text-align"] = "left";
                    cell.Style["vertical-align"] = "middle";
                    cell.Style["font-size"] = "12px";
                    cell.Style["font-weight"] = "normal";
                }
            }
        }
        public void ConfigureExport()
        {
            RadGrid1.MasterTableView.GetColumn("Select").Visible = false;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.FileName = "Material Report (" + DateTime.Now + ")";
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                if (RadGrid1.Items.Count > 0)
                {
                    GridPagerItem pagerItem = (GridPagerItem)RadGrid1.MasterTableView.GetItems(GridItemType.Pager)[1];
                    pagerItem.Display = false;
                    foreach (GridDataItem item in RadGrid1.Items)
                    {
                        item["Select"].Visible = false;
                    }
                    ConfigureExport();
                    //  ApplyStylesToPDFExport(RadGrid1.MasterTableView);
                    RadGrid1.MasterTableView.ExportToExcel();
                }
                else
                {
                    lblError.Text = "No data found unable to export!";
                }
            }

            catch (SqlException oException)
            {
                lblError.Text = oException.ToString();
            }
        }
    }
}