<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="InvoiceWorkflow.aspx.cs" Inherits="DashboardProject.Modules.Finance.InvoiceWorkflow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
    <script src="../../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/blitzer/jquery-ui.css" rel="stylesheet" type="text/css" />

    <link href="../../Content/multiselect.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/miltiselect.js" type="text/javascript"></script>

    <link href="../../Style/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/footable.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=txtSAPDNo').keyup(function () {
                if (this.value.match(/[^,.0-9 ]/g)) {
                    this.value = this.value.replace(/[^,.0-9 ]/g, '');
                }
            });
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

    <script type="text/javascript">
        $(function () {

            $('[id$=btnApproved],[id$=btnSave],[id$=btnSubmit],[id$=btnSaveSubmit],[id$=btnReviewed],[id$=btnReject],[id$=btnMDA]').click(function () {
                $('#<%=lblProgress.ClientID %>').show();
                $('#<%=lblProgress.ClientID %>').html("Please wait a while, your form is being processed.");

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
        $(function () {
            $('[id*=grdWStatus]').footable();
        });
    </script>
    <script language="javascript" type="text/javascript">
        function PrintDivContent() {
            pnlHD.style.display = 'none';
            dvH.style.display = 'block';
            //pnlHD
            window.print();
            pnlHD.style.display = 'block';
            dvH.style.display = 'None';
            //pnlbtnerror.style.display = 'block';
        }
    </script>
    <script type="text/javascript">
        function pageLoad() {
            $('[id*=ddlEmailApproval],[id*=ddlEmailApproval2nd],[id*=DropDownList1],[id*=DropDownList2],[id*=DropDownList3],[id*=DropDownList4],[id*=DropDownList5],[id*=DropDownList6],[id*=DropDownList7],[id*=DropDownList8],[id*=DropDownList9],[id*=DropDownList10],[id*=DropDownList11],[id*=DropDownList12],[id*=ddlEmailMDA]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search for something...',
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p id="dvH" style="font-family: inherit; display: none; font-size: 35px !important; font-weight: bold; color: black; text-align: center;">
        Invoice Workflow Report<br />
        <br />
    </p>
    <div id="pnlHD">
        <div class="container" style="width: 100%; margin-top: 20px;">


            <div class="alert alert-success" id="sucess" runat="server" visible="false">
                <asp:Label ID="lblmessage" runat="server" Font-Bold="False" ForeColor="Green" Font-Names="Berlin Sans FB"></asp:Label>
            </div>

            <div class="alert alert-danger" id="error" runat="server" visible="false">
                <asp:Label ID="lblUpError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB"></asp:Label>
            </div>

            <div class="row">

                <div class="col-sm-7">
                    <p style="font-family: inherit; font-size: 35px !important; font-weight: normal; color: hsla(160, 10%, 18%, 0.35)">Invoice Workflow</p>
                </div>
            </div>


            <div id="Div1" class="panel panel-default" runat="server">
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
                            <div class="col-sm-3">
                                SAP Document No
                                 <asp:TextBox ID="txtSAPDNo" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="col-sm-7">
                                File Name
                                
                             <asp:Label ID="lblFileName" runat="server" CssClass="form-control"></asp:Label>
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


                                <asp:Button ID="btnShowFile" CssClass="btn btn-primary" Text="Show Document" runat="server" OnClick="btnShowFile_Click" Visible="False"></asp:Button>


                                <asp:Button ID="btnDownload" CssClass="btn btn-primary" Text="Download Document" runat="server" OnClick="btnDownload_Click" Visible="False"></asp:Button>


                                <input id="btnShow" type="button" class="btn btn-primary" value="Show Document" style="display: none" />
                                <input runat="server" value="Print" type="button" id="btnPrint" class="btn btn-primary" onclick="PrintDivContent('dialog');" visible="false" />
                                <br />
                            </div>
                        </div>
                        <span class="help-block"></span>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMSGIWF" runat="server" Text="Note: Please first upload IWF before saving the form"></asp:Label>
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
                                    1st Approver
                                       <%--<asp:DropDownList ID="ddlEmailApproval"  CssClass="form-control" runat="server">
                                       
                                        </asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlEmailApproval" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    2nd Approver
                                       <asp:DropDownList ID="ddlEmailApproval2nd" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    3rd Approver
                                       <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    4th Approver
                                       <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    5th Approver
                                       <asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    6th Approver
                                       <asp:DropDownList ID="DropDownList4" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    7th Approver
                                       <asp:DropDownList ID="DropDownList5" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    8th Approver
                                       <asp:DropDownList ID="DropDownList6" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    9th Approver
                                       <asp:DropDownList ID="DropDownList7" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    10th Approver
                                       <asp:DropDownList ID="DropDownList8" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>

                                <div class="col-sm-4">
                                    11th Approver  
                                       <asp:DropDownList ID="DropDownList9" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    12th Approver  
                                       <asp:DropDownList ID="DropDownList10" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    13th Approver  
                                       <asp:DropDownList ID="DropDownList11" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    14th Approver  
                                       <asp:DropDownList ID="DropDownList12" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    Reviwer
                                       <asp:DropDownList ID="ddlEmailMDA" CssClass="form-control" runat="server">
                                       </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>

            </div>
            <div class="panel panel-default" style="text-align: center;">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtRemarksReview" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" PlaceHolder="Comment Box" Visible="true" BackColor="AliceBlue"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-12" style="text-align: left;" runat="server" id="dvemaillbl">
                        <%-- <iframe src="../../AA/Files/VendorCreationForm.version1.0.pdf" width="400"></iframe>--%>

                        <asp:Label ID="lblEmail" runat="server" Font-Bold="False" ForeColor="Blue" Font-Names="Berlin Sans FB"></asp:Label>
                        <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red" Font-Names="Berlin Sans FB" Font-Size="Medium"></asp:Label>
                        <asp:Label ID="lblProgress" runat="server" Font-Bold="False" ForeColor="Black" Font-Names="Berlin Sans FB"></asp:Label>

                    </div>
                </div>
                <span class="help-block"></span>

            </div>

            <div class="col-sm-12" style="text-align: center;">
                <asp:Button ID="btnSave" runat="server" OnClientClick="return AllowOneClick()" CssClass="btn btn-primary" Text="Save" Width="100px" OnClick="btnSave_Click"></asp:Button>
                <asp:Button ID="btnSaveSubmit" runat="server" OnClientClick="return AllowOneClick()" CssClass="btn btn-primary" Text="Save / Submit" Width="100px" Visible="False" ValidationGroup="grpSave" CausesValidation="False" OnClick="btnSaveSubmit_Click"></asp:Button>
                <asp:Button ID="btnApproved" OnClientClick="return AllowOneClick()" runat="server" CssClass="btn btn-primary" Text="Approve" OnClick="btnApproved_Click" Width="100px" Visible="False" CausesValidation="False"></asp:Button>
                <asp:Button ID="btnReject" OnClientClick="return AllowOneClick()" runat="server" CssClass="btn btn-primary" Text="Reject" OnClick="btnReject_Click" Width="100px" Visible="False"></asp:Button>
                <asp:Button ID="btnReviewed" OnClientClick="return AllowOneClick()" runat="server" CssClass="btn btn-primary" Text="Reviewed" Width="100px" Visible="False" OnClick="btnReviewed_Click"></asp:Button>
                <asp:Button ID="btnMDA" OnClientClick="return AllowOneClick()" runat="server" CssClass="btn btn-primary" OnClick="btnMDA_Click" Text="Submit" Width="100px" Visible="False" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset Form" Width="100px" OnClick="btnCancel_Click"></asp:Button>

                <div id="dialog" style="display: none">
                </div>
                <%-- <iframe src="../../AA/Files/VendorCreationForm.version1.0.pdf" width="400"></iframe>--%>
                <%-- <iframe src="../../AA/Files/VendorCreationForm.version1.0.pdf" width="400"></iframe>--%>
            </div>
        </div>
    </div>
    <!--Main Div-->
    <div class="panel-body fixed-panel">
        <div class="row">
            <div id="dvDetail" runat="server" class="col-sm-12" visible="false">
                <asp:GridView ID="grdDetail" CssClass="table table-hover table-bordered footable" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="True">
                    <EmptyDataTemplate>
                        No Data</td>
                    </EmptyDataTemplate>

                </asp:GridView>
            </div>
        </div>
        <span class="help-block"></span>
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
</asp:Content>
