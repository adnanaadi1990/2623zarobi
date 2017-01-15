<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TransportRequestReport.aspx.cs" Inherits="DashboardProject.Modules.Reports.TransportRequestReport" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
    <script src="../../Scripts/jquery-1.9.1.min.js"></script>
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
        function pageLoad() {
            $('[id*=ddlTransportTo],[id*=ddlStorageLocation],[id*=ddlPlant],[id*=ddlSAPID]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search for something...',
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true
            });

        }
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
    </style>
    <style type="text/css">
        .rgPageFirst, .rgPagePrev, .rgPageNext, .rgPageLast {
            display: none;
        }

      .rgCommandTable {
            text-align: left;
            display: block;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width: 100%; margin-top: 20px;">
        <div class="row">

            <div class="col-sm-7">
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Transport Request Form Report</p>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Transport Request Form Report</div>
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
                        Applicable Area
                                     <asp:DropDownList ID="ddlApplicableArea" runat="server" CssClass="form-control" AutoPostBack="True">
                                         <asp:ListItem Value="0">-----Select-----</asp:ListItem>
                                         <asp:ListItem Value="MM">MM Material Management</asp:ListItem>
                                         <asp:ListItem Value="PP">PP Production Planing</asp:ListItem>
                                         <asp:ListItem Value="SD">SD Sales Distribution</asp:ListItem>
                                         <asp:ListItem Value="QM">QM Quality Management</asp:ListItem>
                                         <asp:ListItem Value="FICO">FICO (Financial Accounting) and CO (Controlling)</asp:ListItem>
                                     </asp:DropDownList>
                    </div>
                    <div class="col-sm-4" runat="server" id="Div2">
                        TransportTo
                                 <asp:ListBox ID="ddlTransportTo" runat="server" CssClass="form-control"></asp:ListBox>
                    </div>
                    <div class="col-sm-4" runat="server" id="dvRR" >
                        TRNo
                                 <asp:TextBox ID="txtTRNo" runat="server" CssClass="form-control" CausesValidation="True"></asp:TextBox>
                    </div>
                       <div class="col-sm-4" runat="server" id="Div3">
                        Description
                                 <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" CausesValidation="True"></asp:TextBox>
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
                                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0" vertical="None" ShowGroupPanel="True" Visible="False" OnItemCreated="RadGrid1_ItemCreated">
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
                                            <telerik:GridBoundColumn DataField="ApplicableArea" FilterControlAltText="Filter column2 column" HeaderText="Applicable Area" UniqueName="column2">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TransportTo" FilterControlAltText="Filter column2 column" HeaderText="Transport To" UniqueName="column2">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TRNo" FilterControlAltText="Filter column3 column" HeaderText="TRNo" UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Description" FilterControlAltText="Filter column4 column" HeaderText="Description" UniqueName="column4">
                                            </telerik:GridBoundColumn>  
                                            <telerik:GridBoundColumn DataField="Approver" FilterControlAltText="Filter column6 column" HeaderText="Approver" UniqueName="column6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MDA" FilterControlAltText="Filter column7 column" HeaderText="MDA" UniqueName="column7">
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
