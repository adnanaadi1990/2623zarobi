<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PowerLogin.aspx.cs" Inherits="DashboardProject.PowerLogin" %>

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
                    <asp:Image ID="Image1" ImageUrl="~/Images/ITLLOGO.png" Height="100px" Width="100px" runat="server" />
                    International Textile Limited</h2>
            </div>

            <div class="omb_login">

                <div class="row omb_row-sm-offset-3">
                    <div class="col-xs-12 col-sm-6">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-user"></i></span>
                            <asp:TextBox ID="txtloginID" runat="server" CssClass="form-control" placeholder="User ID"></asp:TextBox>
                        </div>
                        <span class="help-block">
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                        </span>

                        <asp:Button ID="btnlogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnlogin_Click" />

                    </div>
                </div>
            </div>



        </div>


    </form>
</body>
</html>

