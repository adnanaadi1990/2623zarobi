<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ShortLeave.aspx.cs" Inherits="DashboardProject.Modules.HR.ShortLeave" %>

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

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "jQuery Dialog Popup",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };
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

    <script type="text/javascript">
        function pageLoad() {
            $('[id*=txtPostalCode],[id*=txtcustomerCode]').keyup(function () {
                if (this.value.match(/[^,.0-9 ]/g)) {
                    this.value = this.value.replace(/[^,.0-9 ]/g, '');
                }
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
            height: 34px;
        }

        .footable {
        }

        .btn-primary {
            height: 36px;
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
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Short Leave</p>

            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading"></div>
            <div class="panel-body">

                <span class="help-block"></span>

                <div class="row">
                    <div class="col-sm-3" runat="server" id="dvTransactionNo">
                        Transaction No
                                 <asp:Label ID="lblMaxTransactionNo" runat="server" CssClass="form-control"></asp:Label>
                    </div>
                    <div class="col-sm-3" runat="server" id="dvFormID" visible="false">
                        Form ID
                                 <asp:Label ID="lblMaxTransactionID" runat="server" CssClass="form-control"></asp:Label>
                    </div>

                </div>
                <span class="help-block"></span>
                <div class="row" runat="server" visible="false" id="dvCustomerCode">
                    <%--<%# Eval("Lenght") %>--%>
                </div>
            </div>
        </div>

        <div class="panel-group" id="dvGD" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    General Data(To be filled by user)             
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                Location:
                                <asp:RadioButtonList ID="rbType" runat="server" OnSelectedIndexChanged="rbType_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="True">
                                    <asp:ListItem Value="Unit-1" Selected="True">Unit 1</asp:ListItem>
                                    <asp:ListItem Value="Unit-2">Unit 2</asp:ListItem>
                                    <asp:ListItem Value="H.O">Head Office</asp:ListItem>
                                </asp:RadioButtonList>

                            </div>
                            </div>
                           <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-4">
                                Employee Name
                                    &nbsp;<asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control">                                   
                                    </asp:TextBox>
                            </div>

                            <div class="col-sm-4">
                                Employee Card No

                                    <asp:TextBox ID="txtEmployeeCardNo" runat="server" CssClass="form-control" CausesValidation="True" MaxLength="8"></asp:TextBox>
                            </div>

                            <div class="col-sm-4">
                                Department
                                    <%--<%# Eval("Lenght") %>--%>
                                <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control">
                                </asp:TextBox>
                                <%--<%# Eval("Lenght") %>--%>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-3">
                                Division
                                    <asp:TextBox ID="txtDivision" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                Date
                                    <asp:TextBox ID="txtDate" Width="200px" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>


                            <div class="col-sm-6">
                                Reason(s) of Leaving
                                 
                                     <asp:RadioButtonList ID="RBList" runat="server" RepeatDirection="Horizontal">
                                         <asp:ListItem Selected="True">Unit 1</asp:ListItem>
                                         <asp:ListItem>Unit 2</asp:ListItem>
                                         <asp:ListItem>Official Work</asp:ListItem>
                                         <asp:ListItem>Personal Work</asp:ListItem>
                                     </asp:RadioButtonList>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="panel-group" id="dvGK" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Gate Keeper
                </div>
                <div id="Div7" class="panel-collapse collapse in" role="tabpanel">
                    <div class="panel-body">
                        <div class="row">
                        </div>
                        <span class="help-block"></span>
                        <div class="row">

                            <div class="col-sm-3">
                                Time Out
                                    &nbsp;<asp:TextBox ID="txtTimeOut" Width="150px" runat="server" CssClass="form-control" TextMode="Time" Enabled="False"></asp:TextBox>
                            </div>

                            <div class="col-sm-3">
                                Time In
                                          <asp:TextBox ID="txtTimeIn" Width="150px" runat="server" CssClass="form-control" CausesValidation="True" TextMode="Time" Enabled="False"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divEmail" runat="server">
            <asp:Panel ID="pnlemail" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Email Approval
                    </div>
                    <div class="panel-body" style="text-align: center;">
                        <div class="row">
                            <div class="col-sm-4">
                                H.O.D
                                <asp:Label ID="lblHOD" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-4">
                                HR & OD
                                <asp:DropDownList ID="ddlHr" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlHr_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                Corporate Service Department
                                <asp:DropDownList ID="ddlCSD" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <div class="panel panel-default" style="text-align: center;" runat="server" id="dvlbl">
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:TextBox ID="txtRemarksReview" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" PlaceHolder="Comment Box" Visible="False" BackColor="AliceBlue"></asp:TextBox>
                    </div>
                </div>
                <span class="help-block"></span>
                <div class="col-sm-12" style="text-align: left;" runat="server" id="dvemaillbl">
                    <%--<%# Eval("Lenght") %>--%>
                    <asp:Label ID="lblEmail" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
                    <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                    <asp:Label ID="lblProgress" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>
                </div>
            </div>
            <span class="help-block"></span>
        </div>
    </div>

    <div class="col-sm-12" style="text-align: center;">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="grpSa" Width="60px" OnClick="btnSave_Click"></asp:Button>
        <asp:Button ID="btnMDA" runat="server" CssClass="btn btn-primary" Text="Save / Submit" Width="120px" ValidationGroup="grpSave" CausesValidation="False" OnClick="btnMDA_Click" Visible="False"></asp:Button>
        <asp:Button ID="btnApproved" runat="server" CssClass="btn btn-primary" Text="Approval" CausesValidation="False" Width="100px" Visible="False" OnClick="btnApproved_Click"></asp:Button>

        <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Reject" OnClick="btnReject_Click" Width="100px" Visible="False" CausesValidation="False"></asp:Button>
        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" CausesValidation="False" Width="100px" OnClick="btnCancel_Click"></asp:Button>
        <asp:Label ID="lbltest" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
    </div>

    <div class="panel-body fixed-panel">
        <div class="row">
            <div class="col-sm-12">
                <asp:GridView ID="grdWStatus" CssClass="table table-hover table-bordered footable" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Visible="False">
                    <EmptyDataTemplate>
                        No Data</td>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="User Name" SortExpression="User_name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblUserName" Text='<%# Bind("RoughtingUserID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" SortExpression="category">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbcategory" Text='<%# Bind("HierarchyCat") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" HorizontalAlign="Center" />
                            <HeaderStyle Width="14%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" />
                            <HeaderStyle Width="14%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Time" SortExpression="DateTime">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDateTime" Text='<%# Bind("DateTime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" />
                            <HeaderStyle Width="14%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <!-- Modal -->

</asp:Content>
