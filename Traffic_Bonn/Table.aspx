<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Table.aspx.cs" Inherits="Traffic_Bonn.Table" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
    <title>DarkPan - Bootstrap 5 Admin Template</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="keywords" />
    <meta content="" name="description" />

    <!-- Favicon -->
    <link href="img/favicon.ico" rel="icon">

    <!-- Google Web Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;600&family=Roboto:wght@500;700&display=swap" rel="stylesheet" />

    <!-- Icon Font Stylesheet -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.4.1/font/bootstrap-icons.css" rel="stylesheet" />

    <!-- Libraries Stylesheet -->
    <link href="lib/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet" />
    <link href="lib/tempusdominus/css/tempusdominus-bootstrap-4.min.css" rel="stylesheet" />

    <!-- Customized Bootstrap Stylesheet -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <!-- Template Stylesheet -->
    <link href="css/style.css" rel="stylesheet" />
    <!-- JavaScript Libraries -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/4.2.0/chart.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="lib/chart/chart.min.js"></script>
    <script src="lib/easing/easing.min.js"></script>
    <script src="lib/waypoints/waypoints.min.js"></script>
    <script src="lib/owlcarousel/owl.carousel.min.js"></script>
    <script src="lib/tempusdominus/js/moment.min.js"></script>
    <script src="lib/tempusdominus/js/moment-timezone.min.js"></script>
    <script src="lib/tempusdominus/js/tempusdominus-bootstrap-4.min.js"></script>
    <script src="https://polyfill.io/v3/polyfill.min.js?features=default"></script>
</head>
<body style="background-color:white">
    <form id="form1" runat="server">
        <div>
                    <div class="row">
            <nav class="navbar navbar-expand-lg navbar-light bg-light">
          
            <h2 style="padding-left:10px" class="navbar-brand">Traffic System</h2>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item active">
                        <a class="nav-link" href="Index.aspx"><strong>Charts & Maps</strong></a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Table.aspx"><strong>Table</strong></a>
                    </li>
                </ul>
            </div>
        </nav>
        </div>
        <asp:Button ID="btnIncidentCount" runat="server" Text="Show Incident count" OnClick="btnIncidentCount_Click" />
        <asp:Button ID="btnShowData" runat="server" Text="Show Data" OnClick="btnShowData_Click" />
        <br />
        <asp:Label ID="lblIncident" runat="server" Text=""></asp:Label>
        <br />
        <asp:TextBox ID="fromDatePicker" runat="server" TextMode="Date"></asp:TextBox>
        <asp:TextBox ID="toDatePicker" runat="server" TextMode="Date"></asp:TextBox>
        <asp:Button ID="btnDateRange" runat="server" Text="Show Incident In date range" OnClick="btnDateRange_Click" />
        <br />
        <asp:Label ID="lblTraffic" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnPrevious" runat="server" Text="previous" OnClick="btnPrevious_Click" Enabled="false" />
        <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" Enabled="false" />
        <asp:Button ID="btnIncidentPrev" runat="server" Text="Previous" OnClick="btnIncidentPrev_Click" Enabled="false" />
        <asp:Button ID="btnIncidentNext" runat="server" Text="Next" OnClick="btnIncidentNext_Click" Enabled="false" />

               <!-- Sale & Revenue Start -->
        <div class="container-fluid pt-4 px-4">
            <div class="row g-4">


                <div class="col-sm-6 col-xl-6">
                    <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                        <i class="fa fa-chart-area fa-3x text-primary"></i>
                        <div class="ms-3">
                            <p class="mb-2">Total Traffic</p>
                            <h6 class="mb-0" id="lblTotalTraffic" runat="server" style="color:black"></h6>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-xl-6">
                    <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                        <i class="fa fa-chart-pie fa-3x text-primary"></i>
                        <div class="ms-3">
                            <p class="mb-2">Total Incident</p>
                            <h6 class="mb-0" id="lblTotalIncident" runat="server"></h6>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Sale & Revenue End -->
        <!-- Recent Sales Start -->
        <div class="container-fluid pt-4 px-4">
            <div class="bg-secondary text-center rounded p-4">
                <div class="d-flex align-items-center justify-content-between mb-4">
                    <h6 class="mb-10">Traffic Data</h6>
                    <asp:DropDownList runat="server" ID="ddlTest" AutoPostBack="true" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                        <asp:ListItem Text="rain" Value="rain"></asp:ListItem>
                        <asp:ListItem Text="snowy" Value="snowy"></asp:ListItem>
                        <asp:ListItem Text="normal" Value="normal"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="table-responsive">
                    <table class="table text-start align-middle table-bordered table-hover mb-0">
                        <thead>
                            <tr class="text-black">
                                <th scope="col">Opath Id</th>
                                <th scope="col">Source</th>
                                <th scope="col">Time</th>
                                <th scope="col">Speed</th>
                                <th scope="col">Traffic</th>
                                <th scope="col">Incident Type</th>
                                <th scope="col">Incident Id</th>
                                <th scope="col">Incident Comment</th>
                                <th scope="col">Construction Blockage</th>
                                <th scope="col">Consumption(l/h)</th>
                                <th scope="col">Rpm(u/min)</th>
                                <th scope="col">Throttle Position</th>
                                <th scope="col">CO2(kg/h)</th>
                                <th scope="col">Vehicle Id</th>
                                <th scope="col">Id</th>
                                <th scope="col">Opath</th>
                                <th scope="col">Time</th>
                                <th scope="col">Weather Condition</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("opath_id") %></td>
                                            <td><%#Eval("source") %></td>
                                            <td><%#Eval("time") %></td>
                                            <td><%#Eval("speed") %></td>
                                            <td><%#Eval("traffic") %></td>
                                            <td><%#Eval("incidentType") %></td>
                                            <td><%#Eval("incidentID") %></td>
                                            <td><%#Eval("incidentComments") %></td>
                                            <td><%#Eval("Construction_Blockage") %></td>
                                            <td><%#Eval("Consumptionl_h") %></td>
                                            <td><%#Eval("Rpmu_min") %></td>
                                            <td><%#Eval("Throttle_Position") %></td>
                                            <td><%#Eval("CO2kg_h") %></td>
                                            <td><%#Eval("vehicle_id") %></td>
                                            <td><%#Eval("id") %></td>
                                            <td><%#Eval("opath") %></td>
                                            <td><%#Eval("p_time") %></td>
                                            <td><%#Eval("weather_condition") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>




                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <!-- Recent Sales End -->
        </div>
    </form>
</body>
</html>
