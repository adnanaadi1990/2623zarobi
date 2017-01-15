<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PendingList.aspx.cs" Inherits="ITLDashboard.Modules.Reports.PendingList" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        function pageLoad() {
            $("#divExtandToOtherPlant").hide();

            //$('[id*=ddlApplication]').multiselect({
            //    includeSelectAllOption: true,
            //    buttonWidth: '100%',
            //    enableFiltering: true,
            //    filterPlaceholder: 'Search for something...',
            //    maxHeight: 200,
            //    enableCaseInsensitiveFiltering: true
            //});


        }
    </script>
    <script type="text/javascript">
        $(function () {

            $('[id$=btnView]').click(function () {
                $('#<%=lblProgress.ClientID %>').show();
                $('#<%=lblProgress.ClientID %>').html("Please wait a while, your form is being processed.");

            });
        });
    </script>


    <style type="text/css">
        input[type=radio] {
            margin: 0px 7px 7px;
            display: inline-block;
        }
    </style>
    <style type="text/css">
        .fixed-panel {
            min-height: 10%;
            max-height: 10%;
            overflow-y: scroll;
        }

        .form-control {
        }

        .listContainer {
            background-color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="width: 100%; margin-top: 20px;">

        <div class="panel-group" id="dvPD" runat="server">
            <div id="dvAtt" class="panel panel-default">
                <div class="panel-heading">
                    Pending Users List</div>
                <div class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">

                        <div class="row">

                            <div class="col-sm-6">
                                Application
                                  <asp:DropDownList ID="ddlApplication" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Form ID From
                                <asp:TextBox ID="txtfromID" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-sm-3">
                                Form ID To
                                    <asp:TextBox ID="txtToID" runat="server" CssClass="form-control"></asp:TextBox>
                                <%--<%# Eval("Lenght") %>--%>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
       



        <div class="panel panel-default" style="text-align: center;" runat="server" id="dvlbl">
            <div class="panel-body">
                <span class="help-block"></span>
                <div class="col-sm-12" style="text-align: left;" runat="server" id="dvemaillbl">
                    <%--<%# Eval("Lenght") %>--%>

                    <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                    <asp:Label ID="lblProgress" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>
                    
                </div>
            </div>
            <span class="help-block"></span>
        </div>

        <div class="col-sm-12" style="text-align: center;">
            <asp:Button ID="btnView" runat="server" CssClass="btn btn-primary" Text="View Report" ValidationGroup="grpS" Width="100px" OnClick="btnView_Click"></asp:Button>
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" CausesValidation="False" Width="100px" OnClick="btnCancel_Click"></asp:Button>
        </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="false"></rsweb:ReportViewer>
    </div>
</asp:Content>

