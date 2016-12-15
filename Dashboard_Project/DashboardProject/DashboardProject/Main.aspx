<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ITLDashboard.Main" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
<meta http-equiv="refresh" content="60">
    <title>Dashboard</title>

    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css" type="text/css">

    <!-- Custom Fonts -->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Merriweather:400,300,300italic,400italic,700,700italic,900,900italic' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" href="font-awesome/css/font-awesome.min.css" type="text/css">

    <!-- Plugin CSS -->
    <link rel="stylesheet" href="css/animate.min.css" type="text/css">

    <!-- Custom CSS -->
    <link rel="stylesheet" href="css/creative.css" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript">
        history.pushState(null, null, 'Main.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'Main.aspx');
        });

    </script>
    <script type="text/javascript">
        function DivClickedMM() {
            var btnHiddenMM = $('#<%= btnMM.ClientID %>');
            if (btnHiddenMM != null) {
                btnHiddenMM.click();
            }
        }
        function DivClickedFI() {
            var btnHiddenFI = $('#<%= btnFI.ClientID %>');
            if (btnHiddenFI != null) {
                btnHiddenFI.click();
            }
        }

        function DivClickedHR() {
            var btnHiddenHR = $('#<%= btnHR.ClientID %>');
          if (btnHiddenHR != null) {
              btnHiddenHR.click();
          }
      }
      function DivClickedAnnexure() {
          var btnHiddenAnnexure = $('#<%= btnAnnexure.ClientID %>');
          if (btnHiddenAnnexure != null) {
              btnHiddenAnnexure.click();
          }
      }
      function DivClickedSAPBasis() {
          var btnHiddenSAPBasis = $('#<%= btnSAPBasis.ClientID %>');
            if (btnHiddenSAPBasis != null) {
                btnHiddenSAPBasis.click();
            }
        }
        function DivClickedAdmin() {
            var btnHiddenAdmin = $('#<%= btnAdmin.ClientID %>');
          if (btnHiddenAdmin != null) {
              btnHiddenAdmin.click();
          }
      }

        function DivClickedIM() {
            var btnHiddenIM = $('#<%= btnIM.ClientID %>');
            if (btnHiddenIM != null) {
                btnHiddenIM.click();
            }
        }
        
    </script>
</head>

<body id="page-top">

    <nav id="mainNav" class="navbar navbar-default navbar-fixed-top">

        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>

            <a href="#page-top">
                <img src="img/portfolio/logoHe1.png" style="width: 154px; margin-left: 10px; padding-top: 2px;"></a>
        </div>

            <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav navbar-right">
                <li>
                    <a style="color: #f05f40; font-weight:bold;"> Wellcome:
                        <asp:Label runat="server" ID="lblUSerName" Font-Bold="true" Font-Names="Arial Narrow" Font-Size="Small"> </asp:Label>

                    </a>
                </li>
                <li>
                    <a class="page-scroll" href="https://portal.office.com" target="_blank">Office 365</a>
                </li>
                <li>
                    <a class="page-scroll" href="#Introduction">Introduction</a>
                </li>
                <li>
                    <a class="page-scroll" href="#Procedure">Procedure</a>
                </li>
                <li>
                    <a class="page-scroll" href="#APPLICATION">APPLICATIONS</a>
                </li>
                <li>
                    <a class="page-scroll" href="#Helpdesk" style="width: 112px;">Helpdesk</a>
                </li>
                <li>
                    <a class="page-scroll" href="Logout.aspx" style="width: 112px;">Logout</a>
                </li>
            </ul>
        </div>
     <%--   <div class="collapse navbar-collapse" style="width:100%; color:white;">
            <ul class="nav">
                <li>
           <marquee><h5><strong>Good News! s</strong> Dashboard is now presenting user define pending list <a href="Modules/Reports/PendingListByUser.aspx" target="_blank"><b> Click here to redirect</b></a></h5></marquee>
                </li>
              
            </ul>
        </div>--%>

    </nav>

    <header>
        <div class="header-content">
            <div class="header-content-inner">
                <img src="img/portfolio/paper-planes7.png" style="width: 150px; padding-bottom: 5px;">
                 <h1> <asp:Label runat="server" ID="Label1" Font-Bold="true" Font-Names="Arial Narrow" Font-Size="Smaller"> </asp:Label></h1>
                <h1>INTERNATIONAL TEXTILE LIMITED</h1>
                <p>DASH BOARD </p>
                <hr>
               
                <a href="#Introduction" class="btn btn-primary btn-xl page-scroll">Find Out More</a>
            </div>
        </div>
    </header>

    <section class="bg-primary" id="Introduction">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 text-center">
                    <h2 class="section-heading">Introduction!</h2>
                    <hr class="light">
                    <p class="text-faded">
                        Pacing up with the dynamism of the industry and business practices, International 

Textile Limited has achieved another milestone in automating all business documentation. 

ITL Dashboard is a single platform for the users to access all workflow applications 

related to International Textile's business operations. Users can conveniently create, 

review, edit and approve various forms and this cuts down the need to route physical 

documents and waste stationery.<br>
                        Information Systems Department believes in the philosophy of change and improvement:
                        <br>
                        <br>
                        "There is always space for improvement, no matter how long you've been in the business."
                          
                        <br>
                        <b><span class="label label-default" style="margin-left: 500px;">"Oscar De La Hoya"</span></b>




                    </p>

                </div>
            </div>
        </div>
    </section>

    <section id="Procedure">
        <div class="container">

            <div class="row">
                <div class="col-lg-12 text-center">
                    <h2 class="section-heading">Procedure</h2>
                    <hr class="primary">
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-lg-3 col-md-6 text-center">
                    <div class="service-box">
                        <i class="fa fa-4x fa fa-pencil-square-o wow bounceIn text-primary"></i>
                        <h3>Creation</h3>
                        <p class="text-muted">Create and submit new forms.</p>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 text-center">
                    <div class="service-box">
                        <i class="fa fa-4x  fa fa-book wow bounceIn text-primary" data-wow-delay=".1s"></i>
                        <h3>Review</h3>
                        <p class="text-muteds" style="color: #777;">Assess and evaluate form data.</p>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 text-center">
                    <div class="service-box">
                        <i class="fa fa-4x fa fa-check-square-o wow bounceIn text-primary" data-wow-delay=".2s"></i>
                        <h3>Approval</h3>
                        <p class="text-muted">Approve/Disapprove</p>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 text-center">
                    <div class="service-box">
                        <i class="fa fa-4x fa-newspaper-o wow bounceIn text-primary" data-wow-delay=".2s" style="visibility: visible; animation-delay: 0.2s; animation-name: bounceIn;"></i>
                        <h3>Reporting</h3>
                        <p class="text-muted">View reports for all of the Dashboard forms.</p>
                    </div>
                </div>

            </div>
        </div>
    </section>




    <form id="Form1" runat="server">
        <section class="no-padding" id="APPLICATION">
            <div class="container-fluid">
                <div class="row no-gutter">
                    <div class="col-lg-4 col-sm-6">
                        <a href="#" class="portfolio-box">
                            <asp:ImageButton ID="btnMM" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/module4.png" OnClick="btnMM_Click" CausesValidation="False" />

                            <div class="portfolio-box-caption" onclick="javascript:DivClickedMM(); return true;">
                                <div class="portfolio-box-caption-content">
                                    <div class="project-category text-faded">APPLICATION</div>
                                    <div class="project-name">
                                        Material's Management
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-4 col-sm-6">
                        <a href="#" class="portfolio-box">
                            <%--   <img src="img/portfolio/module2.png" class="img-responsive" alt="">--%>
                            <asp:ImageButton ID="btnFI" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/module2.png" OnClick="btnFI_Click" />
                            <div class="portfolio-box-caption" onclick="javascript:DivClickedFI(); return true;">
                                <div class="portfolio-box-caption-content">
                                    <div class="project-category text-faded">APPLICATION</div>
                                    <div class="project-name">
                                        Finance
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-4 col-sm-6">
                        <a href="#" class="portfolio-box">
                            <img src="img/portfolio/module3.png" class="img-responsive" alt="">
                            <div class="portfolio-box-caption">
                                <div class="portfolio-box-caption-content">
                                    <div class="project-category text-faded">APPLICATION</div>
                                    <div class="project-name">Sales Distribution</div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-4 col-sm-6">
                        <a href="#" class="portfolio-box">
                            <img src="img/portfolio/module1.png" class="img-responsive" alt="">
                            <div class="portfolio-box-caption">
                                <div class="portfolio-box-caption-content">
                                    <div class="project-category text-faded">APPLICATION</div>
                                    <div class="project-name">
                                        Quality Management
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-4 col-sm-6">
                        <a href="#" class="portfolio-box">
                            <img src="img/portfolio/module5-2.png" class="img-responsive" alt="">
                            <div class="portfolio-box-caption">
                                <div class="portfolio-box-caption-content">
                                    <div class="project-category text-faded">APPLICATION</div>
                                    <div class="project-name">Production Planning</div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-4 col-sm-6">
                        <a href="#" class="portfolio-box">
                            <asp:ImageButton ID="btnHR" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/HR.png" OnClick="btnHR_Click" />
                            <div class="portfolio-box-caption" onclick="javascript:DivClickedHR(); return true;">
                                <div class="portfolio-box-caption-content">
                                    <div class="project-category text-faded">APPLICATION</div>
                                    <div class="project-name">Human Resources</div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-4 col-sm-6">
                        <a href="#" class="portfolio-box">
                            <asp:ImageButton ID="btnAnnexure" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/Annexure.jpg" OnClick="btnAnnexure_Click" />
                            <div class="portfolio-box-caption" onclick="javascript:DivClickedAnnexure(); return true;">
                                <div class="portfolio-box-caption-content">
                                    <div class="project-category text-faded">APPLICATION</div>
                                    <div class="project-name">Assets Application</div>
                                </div>
                            </div>
                        </a>
                    </div>
                       <div class="col-lg-4 col-sm-6">
                        <a href="#" class="portfolio-box">
                            <asp:ImageButton ID="btnSAPBasis" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/UserRightsApp.jpg" OnClick="btnSAPBasis_Click" />
                            <div class="portfolio-box-caption" onclick="javascript:DivClickedSAPBasis(); return true;">
                                <div class="portfolio-box-caption-content">
                                    <div class="project-category text-faded">APPLICATION</div>
                                    <div class="project-name">User Rights Application</div>
                                </div>
                            </div>
                        </a>
                    </div>
                        <div class="col-lg-4 col-sm-6">
                        <a href="#" class="portfolio-box">
                            <asp:ImageButton ID="btnIM" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/UserRightsApp.jpg" OnClick="btnIM_Click" />
                            <div class="portfolio-box-caption" onclick="javascript:DivClickedIM(); return true;">
                                <div class="portfolio-box-caption-content">
                                    <div class="project-category text-faded">APPLICATION</div>
                                    <div class="project-name">Inventory Management</div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-4 col-sm-6" runat="server" id="dvAdmin" visible="false">
                        <a href="#" class="portfolio-box">
                            <asp:ImageButton ID="btnAdmin" runat="server" CssClass="img-responsive" ImageUrl="~/img/portfolio/admin.jpg" OnClick="btnAdmin_Click"></asp:ImageButton>
                            <div class="portfolio-box-caption" onclick="javascript:DivClickedAdmin(); return true;">
                                <div class="portfolio-box-caption-content">
                                    <div class="project-category text-faded">APPLICATION</div>
                                    <div class="project-name">Admin Panel</div>
                                </div>
                            </div>
                        </a>
                    </div>

                </div>
            </div>
        </section>
        <br>

        <!-- Contact Section -->
        <section id="Helpdesk">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <h2 class="section-heading">Helpdesk</h2>
                        <hr>
                        <h3 class="section-subheading text-muted">DASHBOARD MIS ADMINISTRATION</h3>
                        <br>
                        <br>
                    </div>
                </div>
                <br>
                <div class="row">
                    <div class="col-lg-12">
                        <div id="contactForm" runat="server">
                            <div class="row" style="text-align: left;">

                                <div class="col-sm-12" style="text-align: left;">
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" runat="server" placeholder="Your Message *" ID="txtMessage"></asp:TextBox>

                                        <asp:Label runat="server" ID="lblMessage" ForeColor="Green"></asp:Label>
                                        <p class="help-block text-danger"></p>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-lg-12 text-center">
                                    <div id="success"></div>
                                    <br>
                                    <asp:Button ID="btnSend" runat="server" CssClass="btn btn-xl" Text="Send Message" OnClick="btnSend_Click" ValidationGroup="grpSend"></asp:Button>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
    <section class="no-padding" id="Section1" style="background-color: black; color: white;">
        <br>
        <div class="container-fluid section-title wow fadeInDown animated">
            <div class="row container">
                <div class="col-sm-4" style="text-align: center;">
                    <h2>Head Office</h2>
                    <p style="font-size: 12px;">
                        205-208, Park Towers,
            <br>
                        Shahrae Firdousi, Clifton,
			<br>
                        Karachi-75600 Pakistan.
            <br>
                        Phones: +9221-35832929
             <br>
                        Fax: +9221-35830400
                    </p>

                </div>
                <div class="col-sm-4" style="text-align: center;">
                    <h2>Unit 1:</h2>
                    <p style="font-size: 12px;">
                        Plot 16, Sector 17,
            <br>
                        Korangi Industrial Area,
			<br>
                        Karachi-Pakistan.
            <br>
                        Phones: +9221-35060063-64 
            <br>
                        Fax: +9221-35060393
                    </p>

                </div>
                <div class="col-sm-4" style="text-align: center;">
                    <h2>Unit 2:</h2>
                    <p style="font-size: 12px;">
                        Plot 12 & 27, Sector 20,
            <br>
                        Korangi Industrial Area,
			<br>
                        Karachi-Pakistan.
            <br>
                        Phones: +9221-35043630-1
            <br>
                        Fax: +9221-35040755
                    </p>
                </div>
            </div>
        </div>
        <hr class="bottom" style="margin-bottom: 0px;">
    </section>
    <div class="row">

        <div class="col-sm-12" style="background-color: black; color: white;">
            <footer align="center">
                <p style="font-size: 14px; padding-top: 16px; margin-bottom: 0px; padding-bottom: 20px;">© Copyright International Textile ltd.</p>
            </footer>
        </div>
    </div>

    <!-- jQuery -->
    <script src="js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="js/jquery.easing.min.js"></script>
    <script src="js/jquery.fittext.js"></script>
    <script src="js/wow.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="js/creative.js"></script>

</body>

</html>
