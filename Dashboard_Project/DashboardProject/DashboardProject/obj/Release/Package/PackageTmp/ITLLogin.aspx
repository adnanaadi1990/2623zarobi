<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ITLLogin.aspx.cs" Inherits="ITLDashboard.ITLLogin" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Style/style.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        p {
            padding: 30px;
            font-size: 32px;
            font-weight: bold;
            text-align: center;
            /*background: #f2f2f2;*/
        }
    </style>
    <style type="text/css">
        .bs-example {
            margin: 20px;
        }
    </style>
    <link href="Style/style.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="http://netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <%--JQuery POPUP--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ShowPopup2(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Confirmation",
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
    <style>
        .ui-widget-header, .ui-state-default, ui-button {
            background: #b9cd6d;
            border: 1px solid #b9cd6d;
            color: #FFFFFF;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function ShowPopup() {
            $("#btnShowPopup").click();
        }
    </script>
    <%--End Popup--%>

   <script type = "text/javascript" >
       //function preventBack() { window.history.forward(); }
       //setTimeout("preventBack()", 0);
       //window.onunload = function () { null };
       history.pushState(null, null, 'ITLLogin.aspx');
       window.addEventListener('popstate', function (event) {
           history.pushState(null, null, 'ITLLogin.aspx');
       });
</script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <div class="bs-example">
                <h2 style="font-family: fantasy;">
                    <asp:Image ID="Image1" ImageUrl="~/Images/ITL LOGO.png" Height="100px" Width="100px" runat="server" />
                    International Textile Limited</h2>
            </div>

            <div class="omb_login">
                <h3 class="omb_authTitle">
                    <asp:LinkButton ID="btnShowPopup" runat="server" Text="Show Popup" OnClick="btnShowPopup_Click" class="btn btn-primary btn-lg"
                        data-toggle="modal" data-target="#myModal" Visible="False"></asp:LinkButton>
                </h3>

                <div class="row omb_row-sm-offset-3">
                    <div class="col-xs-12 col-sm-6">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-user"></i></span>
                            <asp:TextBox ID="txtloginID" runat="server" CssClass="form-control" placeholder="User ID"></asp:TextBox>
                        </div>
                        <span class="help-block"></span>

                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                            <asp:TextBox ID="txtloginpass" runat="server" CssClass="form-control" placeholder="password" TextMode="Password">*</asp:TextBox>
                        </div>
                        <span class="help-block"></span>

                        <div class="input-group">
                            <span class="input-group-addon"><i class=""></i></span>
                            <asp:DropDownList ID="ddlServer" runat="server" CssClass="form-control">

                                <asp:ListItem Value="LDAP://itl.local">internationaltextile</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                        <span class="help-block">
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                        </span>

                        <asp:Button ID="btnlogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnlogin_Click" />

                    </div>
                </div>
                <div class="row omb_row-sm-offset-3">
                    <div class="col-xs-12 col-sm-3">
                        <%-- <label class="checkbox">
                        <%--  <span class="help-block">Forgot password?</span>--%>
                        <%--</label>--%>
                    </div>
                    <div class="col-xs-12 col-sm-3">
                        <p class="omb_forgotPwd">
                        </p>
                    </div>
                </div>
            </div>



        </div>




        <%--POPUP SignUp--%>
        <div class="bs-example">
            <div id="myModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">

                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">User Registration</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-4">User ID</div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="grpSubmit" ErrorMessage="Please Type User ID" ControlToValidate="txtUserID" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-4">User Name</div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtusername" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="grpSubmit" ErrorMessage="Please Type User Name" ControlToValidate="txtusername" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-4">Password</div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password">*</asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="grpSubmit" runat="server" ErrorMessage="Please Type Password" ControlToValidate="txtPassword" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-4">User Email</div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="grpSubmit" runat="server" ErrorMessage="Please Type User Email" ControlToValidate="txtemail" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="grpSubmit" ErrorMessage="Please enter valid email address. Eg. Something@domain.com" ControlToValidate="txtemail" Text="*" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>


                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-4">Designation</div>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Designation Must be select" InitialValue="0" ControlToValidate="ddlDesignation" Text="*" ValidationGroup="grpSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <div class="row">
                                <div class="col-sm-4">Department</div>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="grpSubmit" runat="server" ErrorMessage="Department Must be select" ControlToValidate="ddlDept" Text="*" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <span class="help-block"></span>
                            <span class="help-block"></span>
                            <div class="row">
                            </div>
                            <div class="row">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-12">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="grpSubmit" DisplayMode="BulletList" />
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal" style="width: 100px;">Close</button>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="grpSubmit" OnClick="btnSubmit_Click" Width="100px" />

                        </div>
                    </div>

                </div>
            </div>
        </div>


    </form>
    <div id="dialog" style="display: none">
        <asp:Label ID="lblmessage" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
    </div>
</body>
</html>
