<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Traffic_Bonn.Index" %>

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
      <script language="javascript">
          var CheckValue = "All";

          var flag = <asp:Literal id="flagValue" runat="server" />;

          var lowList = "";
          var normalList = "";
          var increasedList = "";
          var jammedList = "";
          var lowCoordinates = [];
          var normalCoordinates = [];
          var increasedCoordinates = [];
          var jammedCoordinates = [];
          //All
          if (flag[0] === 1) {
              console.log("no value")
              lowList = <asp:Literal id="lowArray1" runat="server" />

              normalList = <asp:Literal id="normalArray" runat="server" />
              increasedList = <asp:Literal id="incraesedArray" runat="server" />
              jammedList = <asp:Literal id="jammedArray" runat="server" />

              //var lowList = <asp:Literal id="lowArray" runat="server" />
              lowCoordinates = [];
              normalCoordinates = [];
              increasedCoordinates = [];
              jammedCoordinates = [];
              if (lowList.length > 0) {
                  lowList.forEach(function (item) {
                      lowCoordinates.push(JSON.parse(item));
                  });
              }
              if (normalList.length > 0) {
                  normalList.forEach(function (item) {
                      normalCoordinates.push(JSON.parse(item));
                  });
              }
              if (increasedList.length > 0) {
                  increasedList.forEach(function (item) {
                      increasedCoordinates.push(JSON.parse(item));
                  });
                  if (jammedList.length > 0) {
                      jammedList.forEach(function (item) {
                          jammedCoordinates.push(JSON.parse(item));
                      });
                  }
              }
              var map;
              function initMap() {

                  if (lowList[0] == 0 && normalList[0] == 0 && increasedList[0] == 0 && jammedList[0] == 0) {
                      map = new google.maps.Map(document.getElementById('map'), {

                          center: new google.maps.LatLng(50.691396, 7.1484236),
                          zoom: 12
                      });
                  }
                  else {
                      var dropdown = document.getElementById("ddlTest");
                      var selectedValue = dropdown.value;
                      console.log(selectedValue);
                      map = new google.maps.Map(document.getElementById('map'), {
                          center: lowCoordinates[0][0],
                          zoom: 12
                      });
                      if (selectedValue != "Jammed") {

                          //Loops through all polyline paths and draws each on the map.
                          for (let i = 0; i < lowCoordinates.length; i++) {

                              var flightPath = new google.maps.Polyline({
                                  path: lowCoordinates[i],
                                  geodesic: true,
                                  
                                  strokeColor: '#53FF1A',
                                  strokeOpacity: 1,
                                  strokeWeight: 4,
                              });
                              flightPath.setMap(map);

                          }
                          console.log(normalCoordinates.length)
                          for (let i = 0; i < normalCoordinates.length; i++) {

                              var flightNormalPath = new google.maps.Polyline({
                                  path: normalCoordinates[i],
                                  geodesic: true,
                                  strokeColor: '#A6FF4D',
                                  strokeOpacity: 0.5,
                                  strokeWeight: 4,
                              });

                              flightNormalPath.setMap(map);
                          }
                          for (let i = 0; i < increasedCoordinates.length; i++) {

                              var flightIncreasedPath = new google.maps.Polyline({
                                  path: increasedCoordinates[i],
                                  geodesic: true,
                                  strokeColor: '#FFCC00',
                                  strokeOpacity: 0.5,
                                  strokeWeight: 4,
                              });
                              flightIncreasedPath.setMap(map);

                          }
                          for (let i = 0; i < jammedCoordinates.length; i++) {

                              var flightJammedPath = new google.maps.Polyline({
                                  path: jammedCoordinates[i],
                                  geodesic: true,
                                  strokeColor: '#FE0505',
                                  strokeOpacity: 1,
                                  strokeWeight: 4,
                              });
                              flightJammedPath.setMap(map);

                          }

                      }
                      else {
                          console.log("Heda")
                      }
                  }

              }
              window.initMap = initMap
          }
          //Single polyline
          else {
              console.log("has value")
              var myList = <asp:Literal id="arrayaaa" runat="server" />
              var coordinates = [];
              if (myList.length > 0) {
                  myList.forEach(function (item) {
                      coordinates.push(JSON.parse(item));
                  });
              }
              console.log(coordinates[0][0])
              var map;
              function initMap() {

                  if (myList[0] == 0) {
                      map = new google.maps.Map(document.getElementById('map'), {

                          center: new google.maps.LatLng(50.691396, 7.1484236),
                          zoom: 12
                      });
                  }
                  else {
                      var dropdown = document.getElementById("ddlSelectType");
                      var selectedValue = dropdown.value;
                      console.log(selectedValue);
                      
                      map = new google.maps.Map(document.getElementById('map'), {
                          center: coordinates[0][0],
                          zoom: 12
                      });
                      if (selectedValue == "Road") {
                          console.log(coordinates[1])
                          console.log("Road hoise")
                          //Loops through all polyline paths and draws each on the map.
                          for (let i = 0; i < coordinates.length; i++) {
                              
                              //MARKER ARRAY
                              var flightPath = new google.maps.Polyline({
                                  path: coordinates[i],
                                  geodesic: true,
                                  strokeColor: '#FF0000',
                                  strokeOpacity: 1.0,
                                  strokeWeight: 4,
                              });

                              flightPath.setMap(map);
                          }

                      }
                      else {
                          console.log("marker Hoise")
                          
                          //Loops through all polyline paths and draws each on the map.
                          for (let i = 0; i < coordinates.length; i++) {

                              //MARKER ARRAY
                              console.log(coordinates[i])
                              const marker = new google.maps.Marker({
                                  position: coordinates[i][0],
                                  map: map,
                              });
                          }
                      }
                  }

              }
              window.initMap = initMap;
          }

      </script>
    <%-- <!-- Template Javascript -->
    <script src="js/main.js"></script>--%>
</head>
<body style="background-color:white">
    <form id="form1" runat="server">
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


        <asp:HiddenField runat="server" ID="pieChartValue" />

        <div class="container-fluid pt-4 px-4">
            <div class="row">
                <div class="col-md-3">
                    <asp:TextBox ID="datePicker" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:TextBox ID="toDatePicker" runat="server" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlTime" runat="server"></asp:DropDownList>
                     <asp:DropDownList ID="dtltime" runat="server"></asp:DropDownList>
                </div>
                <div class="col-md-3">
                    
                    <asp:DropDownList runat="server" ID="ddlSelectType" OnSelectedIndexChanged="ddlSelectTypeChanged" AutoPostBack="true">
                        <asp:ListItem Text="Road" Value="Road"></asp:ListItem>
                        <asp:ListItem Text="Marker" Value="Marker"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList runat="server" ID="ddlTest" Visible="true">
                        <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                        <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                        <asp:ListItem Text="Increased" Value="Increased"></asp:ListItem>
                        <asp:ListItem Text="Jammed" Value="Jammed"></asp:ListItem>
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList runat="server" ID="ddlIncidentType" Visible="false">
                        <asp:ListItem Text="Accident" Value="ACCIDENT"></asp:ListItem>
                        <asp:ListItem Text="Construntion" Value="CONSTRUCTION"></asp:ListItem>
                        <asp:ListItem Text="Congestion" Value="CONGESTION"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="ShowData" runat="server" Text="Show" OnClick="ShowData_Click" />
                </div>
            </div>
            <br />
            <div class="row g-4">
                <div class="col-sm-12 col-xl-12">
                    <div id="map" style="width: 100%; height: 500px;"></div>
                </div>

                <div class="col-sm-12 col-xl-6">
                    <div class="bg-secondary rounded h-100 p-4" style="background-color:white !important" >
                        <h6 class="mb-4" style="color:black !important">Percentage of Traffic during the day</h6>
                        <canvas id="pie-Chart"></canvas>
                    </div>
                </div>
                <div class="col-sm-12 col-xl-6">
                    <div class="bg-secondary rounded h-100 p-4"  style="background-color:white !important">
                        <h6 class="mb-4" style="color:black !important">Total incident according to weather</h6>
                        <canvas id="worldwide-sales"></canvas>
                    </div>
                </div>
                <div class="col-sm-12 col-xl-6">
                    <div class="bg-secondary rounded h-100 p-4"  style="background-color:white !important">
                        <h6 class="mb-4" style="color:black !important">Incident during the day</h6>
                        <canvas id="bar-chart"></canvas>
                    </div>
                </div>
                <div class="col-sm-12 col-xl-6">
                    <div class="bg-secondary rounded h-100 p-4"  style="background-color:white !important">
                        <h6 class="mb-4" style="color:black !important">Percentage of traffic during the day</h6>
                        <canvas id="trafficByDayType"></canvas>
                    </div>
                </div>
                <div class="col-sm-12 col-xl-6">
                    <div class="bg-secondary rounded h-100 p-4"  style="background-color:white !important">
                        <asp:DropDownList runat="server" ID="ddlRoadType" AutoPostBack="true" OnSelectedIndexChanged="ddlRoad_SelectedIndexChanged">
                            <asp:ListItem Text="Main Road" Value="primary"></asp:ListItem>
                            <asp:ListItem Text="Motorway" Value="motorway"></asp:ListItem>
                            <asp:ListItem Text="Residential" Value="residential"></asp:ListItem>
                        </asp:DropDownList>
                        <h6 class="mb-4" style="color:black !important">Percentage of traffic during the day</h6>
                        <canvas id="trafficByRoadType"></canvas>
                    </div>
                </div>
            </div>
        </div>

        <!-- JavaScript Libraries -->

    </form>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDOy54y7cxhONY3fNQebXH3AOBD6wD40AY&callback=initMap" async defer></script>
  
    <script>

        //Pie-Chart\

        var values = document.getElementById('pieChartValue').value
        console.log(values)
        var ctx5 = document.getElementById('pie-Chart')
        var myChart5 = new Chart(ctx5, {
            type: "pie",
            data: {
                labels: ["Night", "Morning", "Noon", "Evening"],
                datasets: [{
                    backgroundColor: [
                        "rgba(3, 182, 252)",
                        "rgba(235, 22, 22)",
                        "rgba(240, 197, 55)",
                        "rgba(157, 55, 240)"


                    ],

                    data: <asp:Literal id="litScript" runat="server" />

                    /*data:[2.64,2.20,2.22,2.21,2.04]*/
                }]
            },
            options: {

                responsive: true
            }
        });


        // Bar chart according to month

        var ctx1 = document.getElementById('worldwide-sales')
        var myChart1 = new Chart(ctx1, {
            type: "bar",
            data: {
                labels: ["December", "January"],
                datasets: [{
                    label: "Rainy",
                    data: <asp:Literal id="rainArrayScript" runat="server" />,
                    backgroundColor: "rgba(3, 182, 252)"
                },
                {
                    label: "Normal",
                    data: <asp:Literal id="noamalArrayScript" runat="server" />,
                    backgroundColor: "rgba(235, 22, 22)"
                },
                {
                    label: "Snowy",
                    data: <asp:Literal id="snowyArrayScript" runat="server" />,
                    backgroundColor: "rgba(240, 197, 55)"
                }
                ]
            },
            options: {
                scales: {
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Months of the Year'
                        }
                    }],
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Values'
                        }
                    }]
                },
                responsive: true
            }
        });

        //bar chart incidents

        // Single Bar Chartvar ctx1 = document.getElementById('bar-char')
        var ctx4 = document.getElementById('bar-chart');
        var myChart4 = new Chart(ctx4, {
            type: "bar",
            data: {
                labels: ["Night", "Morning", "Afternoon", "Evening"],
                datasets: [{
                    backgroundColor: [
                        "rgba(3, 182, 252)",
                        "rgba(235, 22, 22)",
                        "rgba(240, 197, 55)",
                        "rgba(157, 55, 240)"
                    ],
                    data: <asp:Literal id="dayArrayScript" runat="server" />
                }]
            },
            options: {
                responsive: true
            }
        });

        //bar chart traffic
        var ctx5 = document.getElementById('trafficByDayType')
        var myChart5 = new Chart(ctx5, {
            type: "bar",
            data: {
                labels: ["Night", "Morning", "Afternoon", "Evening"],
                datasets: [{
                    label: "Low",
                    data: <asp:Literal id="LevelOneArrayScript" runat="server" />,
                    backgroundColor: "rgba(3, 182, 252)"
                },
                {
                    label: "Normal",
                    data: <asp:Literal id="LevelFourArrayScript" runat="server" />,
                    backgroundColor: "rgba(235, 22, 22)"
                },
                {
                    label: "Increased",
                    data: <asp:Literal id="LevelSevenArrayScript" runat="server" />,
                    backgroundColor: "rgba(240, 197, 55)"
                },
                {
                    label: "Jammed",
                    data: <asp:Literal id="LeveltenArrayScript" runat="server" />,
                    backgroundColor: "rgba(157, 55, 240)"
                }
                ]
            },
            options: {
                responsive: true
            }
        });

        //bar chart traffic according to road type
        var ctx6 = document.getElementById('trafficByRoadType')
        var myChart6 = new Chart(ctx6, {
            type: "bar",

            data: {
                labels: ["Night", "Morning", "Afternoon", "Evening"],
                datasets: [{
                    label: "Low",
                    data: <asp:Literal id="LevelOneArrayRoadScript" runat="server" />,
                    backgroundColor: "rgba(3, 182, 252)"
                },
                {
                    label: "Normal",
                    data: <asp:Literal id="LevelFourArrayRoadScript" runat="server" />,
                    backgroundColor: "rgba(235, 22, 22)"
                },
                {
                    label: "Increased",
                    data: <asp:Literal id="LevelSevenArrayRoadScript" runat="server" />,
                    backgroundColor: "rgba(240, 197, 55)"
                },
                {
                    label: "Jammed",
                    data: <asp:Literal id="LeveltenArrayRoadScript" runat="server" />,
                    backgroundColor: "rgba(157, 55, 240)"
                }

                ]
            },
            options: {
                responsive: true
            }
        });

        console.log(myChart5)
    </script>
</body>
</html>
