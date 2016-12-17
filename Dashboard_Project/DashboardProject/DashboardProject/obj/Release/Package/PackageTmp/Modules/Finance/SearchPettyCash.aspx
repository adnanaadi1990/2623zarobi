<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SearchPettyCash.aspx.cs" Inherits="ITLDashboard.Modules.Finance.SearchPettyCash" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/blitzer/jquery-ui.css" rel="stylesheet" type="text/css" />

    <link href="../../Content/multiselect.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/miltiselect.js" type="text/javascript"></script>

    <link href="../../Style/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/footable.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-magnify/0.3.0/css/bootstrap-magnify.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-magnify/0.3.0/js/bootstrap-magnify.min.js"></script>

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
        $(function () {
            $('[id*=GridView1]', '[id*=grdWStatus]').footable();
        });
    </script>


    <script type="text/javascript">
        function pageLoad() {



            var fileName = $('#<%=lblFileName.ClientID%>').text();
            //var fileName = "02e0a481_9e5bc60a_modalpopup1CS.pdf"
            $("#btnShow").click(function () {
                if ($('#<%=lblFileName.ClientID%>').text() == "") {
                    $('#<%=lblError.ClientID %>').html("Unable To Open Document");
                    return;

                }
                var fileName = $('#<%=lblFileName.ClientID%>').text();
                $("#dialog").dialog({
                    modal: true,
                    title: fileName,
                    height: 685,
                    width: 1500,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    open: function () {
                        var object = "<object data=\"{FileName}\" type=\"application/pdf\" width=\"1450px\" height=\"700px\">";
                        object += "If you are unable to view file, you can download from <a href = \"{FileName}\">here</a>";
                        object += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                        object += "</object>";
                        object = object.replace(/{FileName}/g, "../../DashboardDocument/PettyCash/" + fileName);
                        var path = "../../DashboardDocument/PettyCash/" + fileName;
                        $("#dialog").html(object);

                    }
                });

            });

        }

    </script>

    <script type="text/javascript">
        function PrintGridData() {

            var prtGrid = document.getElementById('<%=grdWStatus.ClientID %>');
            var prtGrid1 = document.getElementById('<%=lblFileName.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1100,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write("<h1 style='color: blue;font-family: verdana;font-size: 300%;text-align:center;'>Petty Cash List</h1>  <br><br>File Name: " + prtGrid1.outerHTML);
            var printContent = document.getElementById('div_print');
            prtwin.document.write("<br><br>" + prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.print();
            prtwin.close();


        }
    </script>
    <script language="javascript" type="text/javascript">
        function PrintDivContent(divId) {

            pnlPC.style.display = 'none';
            pnlHD.style.display = 'none';
            pnlerror.style.display = 'none';
            pnlGrid.style.overflow = 'hidden';
            dvH.style.display = 'block';
            window.print();
            dvH.style.display = 'none';
            pnlPC.style.display = 'block';
            pnlHD.style.display = 'block';
            pnlerror.style.display = 'block';
            pnlGrid.style.overflow = 'hidden';

        }
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
         <p id="dvH" style="font-family:inherit; display:none; font-size: 30px !important;font-weight: bold;color:black; text-align:center;">Petty Cash Report<br /><br /></p>
    <div class="container" style="width: 100%; margin-top: 20px;" id="dvcnt">

        <div class="row" id="pnlHD">

            <div class="col-sm-7">
                <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Search Petty Cash</p>
            </div>
        </div>



        <div class="panel panel-default" id="pnlPC">
            <div class="panel-heading">Search Petty Cash</div>
            <div class="panel-body">

                <div class="row">
                    <%-- <asp:BoundField DataField="HierarchyCat" HeaderText="Description" ItemStyle-Width="150" />   --%>
                    <div class="col-sm-2">
                        Form ID&nbsp;
                                    <asp:TextBox runat="server" ID="txtFormID" CssClass="form-control"></asp:TextBox>

                    </div>
                    <div class="col-sm-8" runat="server" id="dvFileName" visible="false">
                        File Name
                                
                             <asp:Label ID="lblFileName" runat="server" CssClass="form-control"></asp:Label>
                    </div>
                     <span class="help-block"></span>                             
                            <div class="col-sm-12" runat="server" id="dvDescription" visible="false" >
                                     Description
                                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-sm-5" runat="server" id="dvShowDoc" visible="false">
                        <br />
 <asp:Button ID="btnShow"  CssClass="btn btn-primary" Text="Show Document" runat="server" OnClick="btnShowFile_Click"></asp:Button>
                                 <input runat="server" value="Print" type="button" id="btnPrint" class="btn btn-primary" onclick="PrintDivContent('dialog');"   visible="False" /></div>
                </div>
                <span class="help-block"></span>
                <div class="row">
                </div>
                <span class="help-block"></span>
                <div class="col-sm-12" style="text-align: center;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" ValidationGroup="grpSave" Width="100px" OnClick="btnSearch_Click"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" CausesValidation="False" Width="100px" OnClick="btnCancel_Click"></asp:Button>

                    &nbsp;</div>
            </div>

        </div>
        <div id="dialog" style="display: none">
        </div>
        <!-- Panel -->
        <div class="panel panel-default" style="text-align: center;" id="pnlerror">
            <div class="panel-body">
                <div class="col-sm-12" style="text-align: left;">
                    <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                </div>
            </div>
            <span class="help-block"></span>

        </div>



    </div>



    <div class="panel-body fixed-panel" id="pnlGrid">
        <div class="row">
            <div class="col-sm-12">

                <asp:GridView ID="grdWStatus" runat="server" CssClass="table table-hover table-bordered footable" PageSize="20" AutoGenerateColumns="False">


                    <Columns>

                        <asp:BoundField DataField="TransactionID" HeaderText="Transaction No" ItemStyle-Width="150">
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ItemStyle-Width="150">
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="RoughtingUserID" HeaderText="Routing Person" ItemStyle-Width="150">
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                           <asp:TemplateField HeaderText="Category" SortExpression="category">

                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbcategory" Text='<%# Bind("HierarchyCat") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" HorizontalAlign="Center" />
                            <HeaderStyle Width="14%" />
                        </asp:TemplateField>
                        <%-- <asp:BoundField DataField="HierarchyCat" HeaderText="Description" ItemStyle-Width="150" />   --%>
                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblHierarchyCat" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" HorizontalAlign="Center" />
                            <HeaderStyle Width="14%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="DateTime" HeaderText="Date & Time" ItemStyle-Width="150">
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>



            </div>


        </div>
        <div>
        </div>


    </div>

</asp:Content>
