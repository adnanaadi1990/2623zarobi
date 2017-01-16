<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoRightsUser.aspx.cs" Inherits="ITLDashboard.NoRightsUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
<%--        <link href="Style/style.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="http://netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>--%>
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <style>
    .wrapper { float: none; clear: left; display: table; table-layout: fixed;  }
    img.img-responsive { display: table-cell; max-width: 100%; }
</style>
     <script type="text/javascript">
         history.pushState(null, null, 'AccessDenied.aspx');
         window.addEventListener('popstate', function (event) {
             history.pushState(null, null, 'AccessDenied.aspx');
         });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
                     
         <div class="wrapper col-md-12"> 
    <img class="img-responsive" width="700px" src="Images/Norights.png"/>
          
              </div>
            </div>
          <span class="help-block"></span>
          <div class="row">
         <div class="wrapper col-md-3">     
       <a href="Logout.aspx" class="btn btn-info btn-lg">
          <span class="glyphicon glyphicon-home"></span> Click Here (It will redirect you to Login Screen)
        </a>
</div>
              </div>
    </form>
<script src="js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->

    <!-- Plugin JavaScript -->
    <script src="js/jquery.easing.min.js"></script>
    <script src="js/jquery.fittext.js"></script>
    <script src="js/wow.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="js/creative.js"></script>
</body>


 

</html>

