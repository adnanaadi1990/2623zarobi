<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Admin_Main.aspx.cs" Inherits="ITLDashboard.Modules.AdminPannel.Admin_Main" %>
<!DOCTYPE html>
<html lang="en">
<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Admin Application</title>

    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../../css/bootstrap.min.css" type="text/css">

    <!-- Custom Fonts -->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Merriweather:400,300,300italic,400italic,700,700italic,900,900italic' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" href="font-awesome/css/font-awesome.min.css" type="text/css">

    <!-- Plugin CSS -->
    <link rel="stylesheet" href="../../css/animate.min.css" type="text/css">

    <!-- Custom CSS -->
    <link rel="stylesheet" href="../../css/creative.css" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript">
        function DivClickedUEF() {
            var btnHiddenUEF = $('#<%= btnUEF.ClientID %>');
            if (btnHiddenUEF != null) {
                btnHiddenUEF.click();
            }
        }
        function DivClickedMtypeAF() {
            var btnHiddenMtypeAF = $('#<%= btnMtypeAF.ClientID %>');
            if (btnHiddenMtypeAF != null) {
                btnHiddenMtypeAF.click();
            }
        }
        function DivClickedUserAuthorizationForm() {
            var btnHiddenUserAuthorizationFormF = $('#<%= btnUAF.ClientID %>');
            if (btnHiddenUserAuthorizationFormF != null) {
                btnHiddenUserAuthorizationFormF.click();
            }
        }

        function DivClickedHODListForm() {
            var btnHiddenUserAuthorizationFormHODList = $('#<%= btnHODList.ClientID %>');
            if (btnHiddenUserAuthorizationFormHODList != null) {
                btnHiddenUserAuthorizationFormHODList.click();
            }
        }

        function DivClickedEmailSendingForm() {
            var btnHiddenEmailSendingForm = $('#<%= btnEmailSF.ClientID %>');
            if (btnHiddenEmailSendingForm != null) {
                btnHiddenEmailSendingForm.click();
            }
        }
    </script>
</head>

<body id="page-top">

    <nav id="mainNav" class="navbar navbar-default navbar-fixed-top" style="background-color: black;">

        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>

            <a href="#page-top">
                <img src="../../img/portfolio/logoHe1.png" style="width: 154px; margin-left: 10px; padding-top: 2px;"></a>
        </div>


        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav navbar-right">
                <li>
                    <a style="color: #f05f40;">Welcome :
                        <asp:Label runat="server" ID="lblUSerName" Font-Bold="true" Font-Names="Arial Narrow" Font-Size="Small"> </asp:Label>

                    </a>
                </li>
                <li>
                        <a class="page-scroll" href="../../Main.aspx" style="width: 112px;">Main Page</a>
                </li>
                <li>
                    <a class="page-scroll" href="../../Logout.aspx" style="width: 112px;">Logout</a>
                </li>

            </ul>
        </div>
        <!-- /.navbar-collapse -->

        <!-- /.container-fluid -->
    </nav>







    <form id="Form1" runat="server">
        <section class="no-padding" id="APPLICATION" style="margin-top: 52px;">
            <div class="container-fluid">
                <div class="row no-gutter">
                    <div class="col-lg-4 col-sm-6">
                        <a href="#" class="portfolio-box">
                            <asp:ImageButton ID="btnUEF" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/UserEntryForm.jpg" CausesValidation="False" OnClick="btnUEF_Click"></asp:ImageButton>
                            <%--<img src="../../img/portfolio/VMC.png" class="img-responsive" alt="">--%>
                            <div class="portfolio-box-caption">
                                <div class="portfolio-box-caption-content" onclick="javascript:DivClickedUEF(); return true;">
                                    <div class="project-category text-faded">Form</div>
                                    <div class="project-name">
                                      User Entry Form
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-4 col-sm-6">
                       <a href="#" class="portfolio-box">
                         <asp:ImageButton ID="btnMtypeAF" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/M-TypeAdminForm.jpg" CausesValidation="False" OnClick="btnMtypeAF_Click"></asp:ImageButton>
                            <%--<img src="../../img/portfolio/CM.jpg" class="img-responsive" alt="">--%>
                            <div class="portfolio-box-caption">
                                <div class="portfolio-box-caption-content" onclick="javascript:DivClickedMtypeAF(); return true;">
                                    <div class="project-category text-faded">Form</div>
                                    <div class="project-name">M-Type Admin Form</div>
                                </div>
                            </div>
                        </a>
                    </div>
                     <div class="col-lg-4 col-sm-6">
                       <a href="#" class="portfolio-box">
                         <asp:ImageButton ID="btnUAF" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/UserAuthorizationForm.jpg" CausesValidation="False" OnClick="btnUAF_Click"></asp:ImageButton>
                            <%--<img src="../../img/portfolio/CM.jpg" class="img-responsive" alt="">--%>
                            <div class="portfolio-box-caption">
                                <div class="portfolio-box-caption-content" onclick="javascript:DivClickedUserAuthorizationForm(); return true;">
                                    <div class="project-category text-faded">Form</div>
                                    <div class="project-name">User Authorization Form</div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-4 col-sm-6">
                       <a href="#" class="portfolio-box">
                         <asp:ImageButton ID="btnHODList" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/HOD.jpg" CausesValidation="False" OnClick="btnHODList_Click"></asp:ImageButton>
                            <%--<img src="../../img/portfolio/CM.jpg" class="img-responsive" alt="">--%>
                            <div class="portfolio-box-caption">
                                <div class="portfolio-box-caption-content" onclick="javascript:DivClickedHODListForm(); return true;">
                                    <div class="project-category text-faded">Form</div>
                                    <div class="project-name">H.O.D List</div>
                                </div>
                            </div>
                        </a>
                    </div>

                      <div class="col-lg-4 col-sm-6">
                       <a href="#" class="portfolio-box">
                         <asp:ImageButton ID="btnEmailSF" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/Emailsending.jpg" CausesValidation="False" OnClick="btnEmailSF_Click"></asp:ImageButton>
                            <%--<img src="../../img/portfolio/CM.jpg" class="img-responsive" alt="">--%>
                            <div class="portfolio-box-caption">
                                <div class="portfolio-box-caption-content" onclick="javascript:DivClickedEmailSendingForm(); return true;">
                                    <div class="project-category text-faded">Form</div>
                                    <div class="project-name">Email Sending Form</div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </section>
        <!-- Contact Section -->
    </form>
    <!-- jQuery -->
    <script src="../../js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../../js/bootstrap.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="../../js/jquery.easing.min.js"></script>
    <script src="../../js/jquery.fittext.js"></script>
    <script src="../../js/wow.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="../../js/creative.js"></script>
</body>

</html>