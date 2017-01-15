<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PendingEmailSending.aspx.cs" Inherits="ITLDashboard.Modules.AdminPannel.PendingEmailSending" %>
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
            $('[id*=ddlUserName]').multiselect({
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
        function ConfirmEmail() {
            if (confirm("Do you really want send this mail") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        $(function () {

            $('[id$=btnSave],[id$=btnSubmit],[id$=btnSaveSubmit],[id$=btnReviewed],[id$=btnReject],[id$=btnMDA],[id*=lnksend]').click(function () {
                $('#<%=lblProgress.ClientID %>').show();
                $('#<%=lblProgress.ClientID %>').html("Please wait a while, your mail is sending.....");
                $(window).scrollTop(0);

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
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Pending User Email Sending</p>

            </div>
        </div>



        <div class="panel-group" id="dvGD" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Email Sending Form
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">


                        <span class="help-block"></span>

                        <div class="row">

                            <div class="col-sm-3">
                                Form ID From
                                <asp:TextBox ID="txtfromID" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-sm-3">
                                Form ID To
                                    <asp:TextBox ID="txtToID" runat="server" CssClass="form-control"></asp:TextBox>
                                <%--<%# Eval("Lenght") %>--%>
                            </div>
                            <div class="col-sm-4">
                                Form Name
                                     <asp:DropDownList ID="ddlFormName" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div id="Div1" class="col-sm-4" runat="server" visible="false" >
                                User Name
                                     <asp:ListBox ID="ddlUserName" runat="server" CssClass="form-control"></asp:ListBox>
                            </div>
                            <div class="col-sm-2">
                                <br />
                                <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary" runat="server" OnClick="btnSearch_Click" CausesValidation="False"></asp:Button>
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

                    <asp:Label ID="lblEmail" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
                    <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                    <asp:Label ID="lblProgress" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>
                </div>
            </div>
            <span class="help-block"></span>
        </div>
        <div class="panel-group">
            <div class="panel panel-default fixed-panel">

                <div class="panel-collapse " role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:GridView ID="grdData" CssClass="table table-hover table-bordered footable" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Visible="False" AllowPaging="True" OnPageIndexChanging="grdData_PageIndexChanging" OnSelectedIndexChanging="grdData_SelectedIndexChanging">

                                    <%--    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" CssClass="pagination" />--%>
                                    <PagerStyle CssClass="pagination-ys" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        No records found for the search criteria.
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                            <HeaderStyle Width="1%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" ItemStyle-Width="10" />
                                           <asp:BoundField DataField="FormCode" HeaderText="Form Code" ItemStyle-Width="10" />
                                        <asp:BoundField DataField="UserName" HeaderText="User Name" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="UserEmail" HeaderText="User Email" ItemStyle-Width="150" />
                                         <asp:BoundField DataField="EmailSubject" HeaderText="User Name" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="EmailBody" HeaderText="User Email" ItemStyle-Width="150" />
                                        <%--   <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                            <HeaderStyle Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Form ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTransactionID" Text='<%# Bind("TransactionID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                            <HeaderStyle Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblUserName" Text='<%# Bind("UserName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                            <HeaderStyle Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Email">
                                            <ItemTemplate>
                                                <div style="word-wrap: break-word; width: 80px;">
                                                    <asp:Label runat="server" ID="lbluseremail" Text='<%# Bind("UserEmail") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="2" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmailSubject" Text='<%# Bind("EmailSubject") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="14%" />
                                            <HeaderStyle Width="14%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Content">
                                            <ItemTemplate>
                                                <div style="word-wrap: break-word; width: 400px;">
                                                    <asp:Label runat="server" ID="lblEmailBody" Text='<%# Bind("EmailBody") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle Width="14%" />
                                            <HeaderStyle Width="14%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnksend" runat="server" CausesValidation="false" CommandName="Select" Text="Send" OnClientClick="return ConfirmEmail();"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                            <HeaderStyle Width="1%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label runat="server" ID="abc" Visible="false">
                You are viewing page # :
        <b><%=grdData.PageIndex + 1%></b>
         Total Number Of Pages: 
       <b> <%=grdData.PageCount%></b>
        Showing Record:
        <b> <%=grdData.Rows.Count%></b>
         <%-- Total Number Of Record 
         <b><%=GridView2.Rows.Count * GridView2.PageCount%></b>--%>
                                </asp:Label>
                            </div>
                            <div class="col-sm-12">
                                <asp:Button ID="btnBulkInsertion" Text="Bulk Email Send" CssClass="btn btn-primary" runat="server" OnClick="btnBulkInsertion_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



    </div>
</asp:Content>
