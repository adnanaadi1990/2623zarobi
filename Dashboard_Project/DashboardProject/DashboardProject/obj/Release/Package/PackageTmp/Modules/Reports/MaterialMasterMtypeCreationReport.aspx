<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MaterialMasterMtypeCreationReport.aspx.cs" Inherits="DashboardProject.Modules.Reports.MaterialMasterMtype_creationReport" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
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

      .rgCommandTable {
            text-align: left;
            display: block;
        }
    </style>
    <style type="text/css">
        .rgPageFirst, .rgPagePrev, .rgPageNext, .rgPageLast {
            display: none;
        }
        .btn-primary {}
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="width: 100%; margin-top: 20px;">
        <div class="row">

            <div class="col-sm-7">
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Material Master Report For Finished Goods</p>
            </div>
        </div>



        <div class="panel panel-default">
            <div class="panel-heading">Create Material Master Report For Finished Goods</div>
            <div class="panel-body">



                <table style="width: 75%;" class="table table-hover table-bordered footable">
                    <tr>
                        <td>Form id from </td>
                        <td>
                            <asp:TextBox ID="txtFormIDfrom" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>Form id to</td>
                        <td>
                            <asp:TextBox ID="txtFormIDto" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>User Name </td>
                        <td>
                            <asp:TextBox ID="txtUN" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>Sap Material Code</td>
                        <td>
                            <asp:TextBox ID="txtSMC" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Plant </td>
                        <td>
                            <asp:TextBox ID="txtPlant" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>Storege Location</td>
                        <td>
                            <asp:TextBox ID="txtSL" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Approver</td>
                        <td>
                            <asp:TextBox ID="txtApprover" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>Reviewer</td>
                        <td>
                            <asp:TextBox ID="txtReviewer" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>MDA</td>
                        <td>
                            <asp:TextBox ID="txtMDA" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>Material Type</td>
                        <td>
                            <asp:TextBox ID="txtMT" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Material Group</td>
                        <td>
                            <asp:TextBox ID="txtMG" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

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
                                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" ShowGroupPanel="True" Visible="False" OnItemCreated="RadGrid1_ItemCreated">
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

                                            <telerik:GridBoundColumn DataField="FormID" FilterControlAltText="Filter column column" FooterText="Form ID" HeaderText="Form ID" UniqueName="FormID">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MaterialType" FilterControlAltText="Filter column1 column" HeaderText="Material Type" UniqueName="column1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SapMaterialCode" FilterControlAltText="Filter column2 column" HeaderText="Sap Material Code" UniqueName="column2">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Plant" FilterControlAltText="Filter column3 column" HeaderText="Plant Code" UniqueName="column3">
                                            </telerik:GridBoundColumn>




                                            <telerik:GridBoundColumn DataField="SL" FilterControlAltText="Filter column4 column" HeaderText="Storage Location" UniqueName="column4">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ValuationType" FilterControlAltText="Filter column5 column" HeaderText="Valuation Type" UniqueName="column5">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MaterialDescription" FilterControlAltText="Filter column6 column" HeaderText="Material Description" UniqueName="column6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="BUOM" FilterControlAltText="Filter column7 column" HeaderText="Base Unit" UniqueName="column7">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MaterialGroup" FilterControlAltText="Filter column8 column" HeaderText="Material Group" UniqueName="column8">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MaterialSubGroup" FilterControlAltText="Filter column9 column" HeaderText="Material Sub Group" UniqueName="column9">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GrossWeight" FilterControlAltText="Filter column10 column" HeaderText="Gross Weight" UniqueName="column10">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NetWeight" FilterControlAltText="Filter column11 column" HeaderText="Net Weight" UniqueName="column11">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="WeightUnit" FilterControlAltText="Filter column12 column" HeaderText="Weight Unit" UniqueName="column12">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Volume" FilterControlAltText="Filter column13 column" HeaderText="Volume" UniqueName="column13">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="VolumeUnit" FilterControlAltText="Filter column14 column" HeaderText="Volume Unit" UniqueName="column14">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="OldMaterialNo" FilterControlAltText="Filter column15 column" HeaderText="Old Material No" UniqueName="column15">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SizeDimension" FilterControlAltText="Filter column16 column" HeaderText="Size Dimension" UniqueName="column16">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="BatchManagement" FilterControlAltText="Filter column17 column" HeaderText="Batch Management" UniqueName="column17">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CreatedBy" FilterControlAltText="Filter column18 column" HeaderText="Created By" UniqueName="column18">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CreationDate" FilterControlAltText="Filter column19 column" HeaderText="Creation Date" UniqueName="column19">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CreationTime" FilterControlAltText="Filter column20 column" HeaderText="Creation Time" UniqueName="column20">
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
