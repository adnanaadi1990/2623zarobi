<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="VendorMasterReport.aspx.cs" Inherits="ITLDashboard.Modules.Reports.VendorMasterReport" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script src="../../Scripts/bootstrap.min.js"></script>
    <%--<link href="../../Content/bootstrap.min.css" rel="stylesheet" />--%>

    <link href="../../Content/multiselect.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/miltiselect.js" type="text/javascript"></script>

    <link href="../../Style/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/footable.min.js"></script>
    <script type="text/javascript">
        function ShowPopup() {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Material Master",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true,
                    height: 650,
                    width: 500,
                    left: 0,
                });
            });
        };
    </script>
      <script type="text/javascript">
          history.pushState(null, null, document.URL);
          window.addEventListener('popstate', function (event) {
              history.pushState(null, null, document.URL);
          });

    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=grdWStatus]').footable();
        });
    </script>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .modalBackground {
            height: 500px;
            overflow: auto;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            overflow: auto;
            background-color: #FFFFFF;
            border: 3px solid #0DA9D0;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                margin-bottom: 5px;
            }
    </style>
    <style type="text/css">
        .fixed-panel {
            min-height: 10%;
            max-height: 10%;
            overflow-y: scroll;
        }

        .form-control {
            height: 34px;
        }
    </style>
    <style type="text/css">
        .rwStatusbarRow {
            display: none !important;
            height: auto;
        }

        .AutoShrink {
            width: 240px !important;
        }
    </style>
    <style type="text/css">
        .rgPageFirst, .rgPagePrev, .rgPageNext, .rgPageLast {
            display: none;
        }

        .btn-primary {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width: 100%; margin-top: 20px;">
        <div class="row">

            <div class="col-sm-7">
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Vendor Master Report</p>
            </div>
        </div>



        <div class="panel panel-default">
            <div class="panel-heading">Vendor Master Report</div>
            <div class="panel-body">

                <div class="row">
                        
                    <div class="col-sm-4" runat="server" id="dvTransactionNo">
                        Form Id From 
                         <asp:TextBox ID="txtFormIDfrom" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-4" runat="server" id="dvFormID">
                        Form Id To
                          <asp:TextBox ID="txtFormIDto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    
                    <div class="col-sm-4" runat="server" id="Div1">
                        User Name 
                           <asp:TextBox ID="txtUN" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-4" runat="server" id="Div2" >
                        Vendor Code
                         <asp:TextBox ID="txtVC" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-4" runat="server" id="Div3">
                        Account Group
                         <asp:DropDownList ID="ddlAccountGroup" runat="server" CssClass="form-control">
                         </asp:DropDownList>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <%--<telerik:radajaxmanager runat="server" ID="RadAjaxManager1">
        <AjaxSettings>
            <telerik:ajaxsetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:ajaxupdatedcontrol ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>--%>





                            <div class="fixed-panel">
                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True"
                                    OnNeedDataSource="RadGrid1_NeedDataSource"
                                    OnPreRender="RadGrid1_PreRender"
                                    OnItemCommand="RadGrid1_ItemCommand"
                                    OnGroupsChanging="RadGrid1_GroupsChanging"
                                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0" Vertical="None" ShowGroupPanel="True" Visible="False" OnItemCreated="RadGrid1_ItemCreated">
                                    <ClientSettings AllowColumnsReorder="true" AllowDragToGroup="true" EnableRowHoverStyle="true" AllowGroupExpandCollapse="True" ReorderColumnsOnClient="True" ColumnsReorderMethod="Reorder" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                                        <Animation AllowColumnReorderAnimation="true" AllowColumnRevertAnimation="true" ColumnReorderAnimationDuration="2000" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <ExportSettings ExportOnlyData="true" IgnorePaging="true" OpenInNewWindow="true">
                                    </ExportSettings>
                                    <MasterTableView CommandItemDisplay="Top" CommandItemSettings-ShowExportToExcelButton="true">
                                    </MasterTableView>
                                    <MasterTableView AllowFilteringByColumn="False" GroupLoadMode="Client" HierarchyLoadMode="Client" ShowGroupFooter="True" CssClass="AutoShrink">
                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                            <HeaderStyle Width="30px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                            <HeaderStyle Width="30px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridButtonColumn CommandName="Select" Text="Select" UniqueName="Select">
                                            </telerik:GridButtonColumn>

                                            <telerik:GridBoundColumn DataField="TransactionID" FilterControlAltText="Filter column column" FooterText="Form ID" HeaderText="Form ID" UniqueName="FormID">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="VendorCode" FilterControlAltText="Filter column1 column" HeaderText="Vendor Code" UniqueName="column1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Title" FilterControlAltText="Filter column2 column" HeaderText="Title" UniqueName="column2">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter column3 column" HeaderText="Name" UniqueName="column3">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Accountgroup" FilterControlAltText="Filter column4 column" HeaderText="Account Group" UniqueName="column4">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PurchasingOrganization" FilterControlAltText="Filter column5 column" HeaderText="Purchasing Organization" UniqueName="column5">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CompanyCode" FilterControlAltText="Filter column6 column" HeaderText="Company Code" UniqueName="column6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Street" FilterControlAltText="Filter column7 column" HeaderText="Street" UniqueName="column7">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Country" FilterControlAltText="Filter column8 column" HeaderText="Country" UniqueName="column8">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="City" FilterControlAltText="Filter column9 column" HeaderText="City" UniqueName="column9">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Region" FilterControlAltText="Filter column10 column" HeaderText="Region" UniqueName="column10">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PostalCode" FilterControlAltText="Filter column11 column" HeaderText="Postal Code" UniqueName="column11">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TaxPayerCNIC" FilterControlAltText="Filter column12 column" HeaderText="Tax Payer CNIC" UniqueName="column12">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PassportNo" FilterControlAltText="Filter column13 column" HeaderText="Passport No" UniqueName="column13">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TaxPayerNTN" FilterControlAltText="Filter column14 column" HeaderText="Tax Payer NTN" UniqueName="column14">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CDCNumber" FilterControlAltText="Filter column15 column" HeaderText="CDC Number" UniqueName="column15">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GSTNo" FilterControlAltText="Filter column16 column" HeaderText="GST No" UniqueName="column16">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Telephone" FilterControlAltText="Filter column17 column" HeaderText="Telephone" UniqueName="column17">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter column18 column" HeaderText="Email" UniqueName="column18">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Natureofvendor" FilterControlAltText="Filter column19 column" HeaderText="Nature of vendor" UniqueName="column19">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="OrderCurrency" FilterControlAltText="Filter column20 column" HeaderText="Order Currency" UniqueName="column20">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="MinimumOrderValue" FilterControlAltText="Filter column20 column" HeaderText="Minimum Order Value" UniqueName="column20">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="SchemaGroupVendor" FilterControlAltText="Filter column20 column" HeaderText="Schema Group Vendor" UniqueName="column20">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GRCheck" FilterControlAltText="Filter column20 column" HeaderText="GR Check" UniqueName="column20">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ReconAccount" FilterControlAltText="Filter column20 column" HeaderText="Recon Account" UniqueName="column20">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CCDTermsofpayment" FilterControlAltText="Filter column20 column" HeaderText="Terms of payment" UniqueName="column20">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="PaymentMethods" FilterControlAltText="Filter column20 column" HeaderText="Payment Methods" UniqueName="column20">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PreviousAccount" FilterControlAltText="Filter column20 column" HeaderText="Previous Account" UniqueName="column20">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="LiableCheck" FilterControlAltText="Filter column20 column" HeaderText="Liable Check" UniqueName="column20">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ExemptionCertificate" FilterControlAltText="Filter column20 column" HeaderText="Exemption Certificate" UniqueName="column20">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="ExemptionReasons" FilterControlAltText="Filter column20 column" HeaderText="Exemption Reasons" UniqueName="column20">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ExemptionFromDate" FilterControlAltText="Filter column20 column" HeaderText="Exemption From Date" UniqueName="column20">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ExemptionToDate" FilterControlAltText="Filter column20 column" HeaderText="Exemption To Date" UniqueName="column20">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="WHTaxCountry" FilterControlAltText="Filter column20 column" HeaderText="WHTax Country" UniqueName="column20">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="WHTaxType" FilterControlAltText="Filter column20 column" HeaderText="WHTax Type" UniqueName="column20">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="CreatedBy" FilterControlAltText="Filter column20 column" HeaderText="Created By" UniqueName="column20">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CreatedDateTime" FilterControlAltText="Filter column20 column" HeaderText="Created Date Time" UniqueName="column20">
                                            </telerik:GridBoundColumn>



                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                            </EditColumn>
                                        </EditFormSettings>
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" CssClass="AutoShrink" />
                                        <FilterItemStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="false" />
                                    </MasterTableView>
                                    <FilterMenu EnableImageSprites="False">
                                    </FilterMenu>
                                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                                    </HeaderContextMenu>
                                </telerik:RadGrid>

                            </div>

                        </div>
                    </div>
                </div>

                <span class="help-block"></span>

            </div>
        </div>

        <!-- Panel -->


        <div class="panel-body">
            <div class="col-sm-12" style="text-align: left;">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="grpSave" DisplayMode="BulletList" />
                <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
            </div>
            <div class="col-sm-12" style="text-align: center;">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" ValidationGroup="grpSave" Width="100px" OnClick="btnSearch_Click" UseSubmitBehavior="False" ViewStateMode="Enabled"></asp:Button>
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" CausesValidation="False" Width="100px" OnClick="btnCancel_Click"></asp:Button>
                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary" OnClick="btnExport_Click" Text="Export To Excel" Visible="False" />
            </div>

        </div>
        <span class="help-block"></span>

    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
</asp:Content>
