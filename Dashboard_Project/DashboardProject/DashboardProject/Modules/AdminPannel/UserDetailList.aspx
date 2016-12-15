<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserDetailList.aspx.cs" Inherits="ITLDashboard.Modules.AdminPannel.UserDetailList" %>
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
    <link href="../../Style/GridStyleSheet.css" rel="stylesheet" />




    <script type="text/javascript">
        function pageLoad() {
            $('[id*=ddlDepartment],[id*=ddlDesignation],[id*=ddlHOD]').multiselect({
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
        function ConfirmOnDelete() {
            if (confirm("Do you really want to delete?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        $(function () {

            $('[id$=btnSave],[id$=btnSubmit],[id$=btnSaveSubmit],[id$=btnReviewed],[id$=btnReject],[id$=btnMDA],[id$=btnApproved]').click(function () {
                $('#<%=lblProgress.ClientID %>').show();
                $('#<%=lblProgress.ClientID %>').html("Please wait a while, your form is being processed.");

            });
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $('[id*=GridView1]').footable();
        });
    </script>



    <script type="text/javascript">
        $(function () {
            /*
             * this swallows backspace keys on any non-input element.
             * stops backspace -> back
             */
            var rx = /INPUT|SELECT|TEXTAREA/i;

            $(document).bind("keydown keypress", function (e) {
                if (e.which == 8) { // 8 == backspace
                    if (!rx.test(e.target.tagName) || e.target.disabled || e.target.readOnly) {
                        e.preventDefault();
                    }
                }
            });
        });


    </script>

    <style type="text/css">
        input[type=checkbox], input[type=radio] {
            margin: 10px 9px 0;
            margin-top: 1px 9px;
            line-height: normal;
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

        .footable {
        }

        .btn-primary {
            height: 36px;
        }

        .AlphabetPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
            letter-spacing: 0.0625em;
        }

        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container" style="width: 100%; margin-top: 20px;">

        <div class="alert alert-success" id="sucess" runat="server" visible="false">
            <asp:Label ID="lblmessage" runat="server" Font-Bold="False" ForeColor="Green" Font-Names="Berlin Sans FB"></asp:Label>
        </div>

        <div class="alert alert-danger" id="error" runat="server" visible="false">
            <asp:Label ID="lblUpError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
        </div>

        <div class="row">

            <div class="col-sm-7">
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">User Detail List</p>

            </div>
        </div>

        <div class="panel panel-default" runat="server" id="dvPnl">
            <div class="panel-heading"></div>
            <div class="panel-body">

                <span class="help-block"></span>

                <div class="row">
                    <div class="col-sm-3" runat="server" id="dvTransactionNo">
                        Transaction No
                                 <asp:Label ID="lblMaxTransactionNo" runat="server" CssClass="form-control"></asp:Label>
                    </div>


                </div>
                <span class="help-block"></span>
                <%--<%# Eval("Lenght") %>--%>
            </div>

        </div>

        <div class="panel-group" id="dvGD" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    User Detail List
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row" id="dvAllUser" runat="server">
                            <div class="col-sm-3">
                                Full Name
                                   <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" BackColor="AliceBlue"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Logon ID (System logon ID)
                                    <asp:TextBox ID="txtUserNameOther" runat="server" CssClass="form-control" CausesValidation="True"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Email ID
                                    <asp:TextBox ID="txtEmailIDOther" runat="server" CssClass="form-control" CausesValidation="True" TextMode="Email" BackColor="AliceBlue"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Location
                                   <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" BackColor="AliceBlue">
                                       <asp:ListItem>------Select------</asp:ListItem>
                                       <asp:ListItem Value="Unit-1">Unit 1</asp:ListItem>
                                       <asp:ListItem Value="Unit-2">Unit 2</asp:ListItem>
                                       <asp:ListItem Value="Head Office">Head Office</asp:ListItem>
                                   </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Department
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" BackColor="AliceBlue">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Designation
                                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" BackColor="AliceBlue">
                                        <asp:ListItem Value="SD">Software Devoloper</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                Mobile No.
                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Extension No.
                                    <asp:TextBox ID="txtExtensionNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                SAP ID
                                    <asp:TextBox ID="txtSAPID" runat="server" CssClass="form-control" BackColor="AliceBlue"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                               Head Of Department
                                    <asp:DropDownList ID="ddlHOD" runat="server" CssClass="form-control" BackColor="AliceBlue"></asp:DropDownList>
                            </div>
                        </div>

                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label1" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB" Text="Note: Logon ID (System logon ID) is based on system user logon id."></asp:Label>

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

                <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                <asp:Label ID="lblProgress" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>
            </div>
        </div>
        <span class="help-block"></span>
    </div>


    <div class="col-sm-12" style="text-align: center;">
        <br />
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="grpSa" Width="60px" OnClick="btnSave_Click"></asp:Button>
        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="grpSa" Width="100px" OnClick="btnUpdate_Click" Visible="False"></asp:Button>
        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" CausesValidation="False" Width="100px" OnClick="btnCancel_Click"></asp:Button>
        <asp:Label ID="lbltest" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
    </div>


    <div class="panel panel-default" style="text-align: center;" runat="server" id="Div1">
        <div class="panel-body">
            <span class="help-block"></span>
            <div class="col-sm-12" style="text-align: left;" runat="server" id="Div2">

       
            </div>
        </div>
        <span class="help-block"></span>
    </div>

    </div>
</asp:Content>
