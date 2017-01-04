<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="InventoryAdjustmentApproval.aspx.cs" Inherits="DashboardProject.Modules.Inventorymanagement.InventoryAdjustmentApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <%--<link href="../../Content/bootstrap.min.css" rel="stylesheet" />--%>

    <link href="../../Content/multiselect.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/miltiselect.js" type="text/javascript"></script>

    <link href="../../Style/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/footable.min.js"></script>


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

            $('[id$=btnApproved],[id$=btnSave],[id$=btnSubmit],[id$=btnSaveSubmit],[id$=btnReviewed],[id$=btnReject],[id$=btnMDA]').click(function () {
                $('#<%=lblProgress.ClientID %>').show();
                $('#<%=lblProgress.ClientID %>').html("Please wait a while, your form is being processed.");

            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=grdWStatus]').footable();
        });
    </script>
    <script language="javascript" type="text/javascript">
        function PrintDivContent() {
            dvH.style.display = 'block';
            pnlPC.style.display = 'none';
            window.print();
            pnlPC.style.display = 'block';
            dvH.style.display = 'none';
        }
    </script>
   

    <script type="text/javascript">
        function pageLoad() {

            $('[id*=ddlNotification]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search for something...',
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true
            });
        }
        $(function () {
            $('[id*=txtAmount]').keyup(function () {
                if (this.value.match(/[^,.0-9 ]/g)) {
                    this.value = this.value.replace(/[^,.0-9 ]/g, '');
                }
            });
        });
        var clicked = false;
        function AllowOneClick() {
            if (!clicked) {
                clicked = true;
                return true;
            }
            return false;
        }

    </script>


    <script type="text/javascript">
        history.pushState(null, null, document.URL);
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, document.URL);
        });

    </script>
    <style type="text/css">
        .fixed-panel {
        min-height:10%;
        max-height:10%;
        overflow-y:scroll;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <p id="dvH" style="font-family: inherit; display: none; font-size: 30px !important; font-weight: bold; color: black; text-align: center;">
      Inventory Approval Report<br />
        <br />
    </p>
    <div class="container" style="margin-top: 20px; width:100%;">
            <div class="container-fluid">
        <div id="pnlPC">
            <div class="alert alert-success" id="sucess" runat="server" visible="false">
                <asp:Label ID="lblmessage" runat="server" Font-Bold="False" ForeColor="Green" Font-Names="Berlin Sans FB"></asp:Label>
            </div>

            <div class="alert alert-danger" id="error" runat="server" visible="false">
                <asp:Label ID="lblUpError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
            </div>

            <div class="row">

                <div class="col-sm-7" id="pnlHD">
                    <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Inventory Adjustment Approval</p>
                </div>
            </div>


            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion6" href="#collapse6"></a>
                    </h4>
                </div>
                <div id="collapse6" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-3" runat="server" id="dvTransactionNo">
                                Transaction No
                                 <asp:Label ID="lblMaxTransactionNo" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="col-sm-3" runat="server" id="dvFormID" visible="false">
                                Form ID
                                 <asp:Label ID="lblMaxTransactionID" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="col-sm-7">
                                File Name
                                
                             <asp:Label ID="lblFileName" runat="server" CssClass="form-control"></asp:Label>
                            </div>

                        </div>
                        <span class="help-block"></span>
                        <div class="row" runat="server" id="Div1">

                            <div class="col-sm-12">
                                Description
                                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row" runat="server" id="dvCheque">
                            <div class="col-sm-3">
                                Document No
                                <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-3" runat="server" id="dvBrows">
                                Browse File
                                    
                                <asp:FileUpload ID="fleUpload" CssClass="form-control" runat="server" />
                            </div>

                            <div class="col-sm-8" runat="server" id="dvUpload">
                                <br />
                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Upload" Width="80px" OnClick="btnUpload_Click"></asp:Button>


                                <asp:Button ID="btnDelete" CssClass="btn btn-primary" Text="Remove File" runat="server" OnClick="btnDelete_Click" Visible="False"></asp:Button>


                                <input id="btnShow" type="button" class="btn btn-primary" value="Show Document" style="display: none;" />
                                <asp:Button ID="btnShowFile" CssClass="btn btn-primary" Text="Show Document" runat="server" OnClick="btnShowFile_Click" Visible="False"></asp:Button>
                                <input runat="server" value="Print" type="button" id="btnPrint" class="btn btn-primary" onclick="PrintDivContent('dialog');" visible="False" />
                                <%--<%# Eval("Numerator") %>--%>
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
                        <div class="panel-body">
                            <div class="row" style="text-align: center;">
                          <div class="col-sm-4">
                                Head Of Deparment
                                       <asp:Label ID="lblHOD" runat="server" CssClass="form-control"></asp:Label>
                            </div>
     
                                

                                <div class="col-sm-4">
                                    Specific (Approver)  
                                       <asp:ListBox ID="ddlNotification" SelectionMode="Multiple" runat="server">
                                       </asp:ListBox>
                                </div>
<div class="col-sm-4">
                                    IS Representative  
                                       <asp:DropDownList ID="ddlEmailMDA" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
            </div>


            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtRemarksReview" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" PlaceHolder="Comment Box" Visible="true" BackColor="AliceBlue"></asp:TextBox>
                        </div>
                    </div>
                    <span class="help-block"></span>
                    <div class="col-sm-12" style="text-align: left;" runat="server" id="dvemaillbl">
                        <%-- <iframe src="../../AA/Files/VendorCreationForm.version1.0.pdf" width="400"></iframe>--%>
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>
                    </div>
                </div>
                <span class="help-block"></span>
            </div>

            <span class="help-block"></span>
            <div id="dvimage" runat="server" class="col-sm-12">
                <span class="help-block"></span>
                <div id="pnlbtnerror">
                    <div class="panel panel-default" style="text-align: center;" id="DVERROR" runat="server">
                        <div class="panel-body">

                            <div id="Div2" class="col-sm-12" style="text-align: left;" runat="server">
                                <%--<asp:Button ID="btnReject11" runat="server" CssClass="btn btn-primary" Text="Reject" Width="100px" Visible="False"></asp:Button>--%>

                                <asp:Label ID="lblEmail" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
                                <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB" Font-Size="Medium"></asp:Label>
                                <asp:Label ID="lblProgress" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>

                            </div>
                        </div>
                        <span class="help-block"></span>

                    </div>

                    <div class="col-sm-12" style="text-align: center;">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClientClick="return AllowOneClick()" Text="Save" Width="100px" OnClick="btnSave_Click"></asp:Button>
                        <asp:Button ID="btnSaveSubmit" runat="server" CssClass="btn btn-primary" Text="Save / Submit" Width="100px" Visible="False" ValidationGroup="grpSave" OnClientClick="return AllowOneClick()" CausesValidation="False" OnClick="btnSaveSubmit_Click"></asp:Button>
                        <asp:Button ID="btnApproved" runat="server" CssClass="btn btn-primary" Text="Approve" OnClick="btnApproved_Click" Width="100px" Visible="False" OnClientClick="return AllowOneClick()"></asp:Button>
                        <%-- <iframe src="../../AA/Files/VendorCreationForm.version1.0.pdf" width="400"></iframe>--%>
                      <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary" Text="Reject" OnClick="btnReject_Click" Width="100px" CausesValidation="False" Visible="False"></asp:Button>
                        <asp:Button ID="btnMDA" runat="server" CssClass="btn btn-primary" OnClientClick="return AllowOneClick()" OnClick="btnMDA_Click" Text="Submit" Width="100px" Visible="False" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" Width="100px" OnClick="btnCancel_Click"></asp:Button>


                        <%-- <iframe src="../../AA/Files/VendorCreationForm.version1.0.pdf" width="400"></iframe>--%>
                        <%-- <iframe src="../../AA/Files/VendorCreationForm.version1.0.pdf" width="400"></iframe>--%>
                    </div>
                </div>


            </div>
        </div>
        <div class="panel-body fixed-panel " id="grd">
            <div class="row"></div>
            <span class="help-block"></span>
            <div class="row">
                <div id="dvDetail" runat="server" class="col-sm-12 fixed-panel">
                    <asp:GridView ID="grdDetail" CssClass="table table-hover table-bordered footable" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="True" Visible="False">
                        <EmptyDataTemplate>
                            No Data</td>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            <span class="help-block"></span>
            <div class="row">
                <div class="col-sm-12 fixed-panel"  >
                    <asp:GridView ID="grdWStatus" CssClass="table table-hover table-bordered footable" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" Visible="False">
                        <EmptyDataTemplate>
                            No Data</td>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Form ID" SortExpression="TransactionID">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblTransactionID" Text='<%# Bind("TransactionID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="User" SortExpression="User_name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUserName" Text='<%# Bind("RoughtingUserID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Category" SortExpression="category">

                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbcategory" Text='<%# Bind("HierarchyCat") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Time" SortExpression="DateTime">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDateTime" Text='<%# Bind("DateTime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRemarks" Text='<%# Bind("Remarks") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="14%" />
                                <HeaderStyle Width="14%" />
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>


            </div>
        </div>
</div>

    </div>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Select Any Action</h4>
                </div>
                <div class="modal-body" style="height: 125px;">

                    <div class="col-sm-2"><b>Remarks</b></div>
                    <div class="col-sm-9" style="text-align: center;">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Height="50px" TextMode="MultiLine"></asp:TextBox>
                    </div>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 60px;">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
