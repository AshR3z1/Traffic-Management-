<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="Traffic_Bonn.Map" %>

<html>
  <head>
    <title>Simple Map</title>
    <script src="https://polyfill.io/v3/polyfill.min.js?features=default"></script>
  
       <script>
           var myList = <asp:Literal id="arrayaaa" runat="server" />
           console.log(myList);

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
                   var dropdown = document.getElementById("ddlTest");
                   var selectedValue = dropdown.value;
                   console.log(selectedValue);
                   map = new google.maps.Map(document.getElementById('map'), {
                       center: coordinates[0][0],
                       zoom: 12
                   });
                   if (selectedValue != "Jammed") {
                       //Loops through all polyline paths and draws each on the map.
                       for (let i = 0; i < coordinates.length; i++) {
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
                       console.log("Heda")
                   }
               }
               
           }
           window.initMap = initMap;
       </script>

  </head>
  <body>
     <form id="form1" runat="server">
        <asp:TextBox ID="datePicker" runat="server" TextMode="Date"></asp:TextBox>
         <asp:DropDownList ID="ddlTime" runat="server"></asp:DropDownList>
         <asp:DropDownList  runat="server" ID="ddlTest" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                        <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                        <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                        <asp:ListItem Text="Increased" Value="Increased"></asp:ListItem>
                        <asp:ListItem Text="Jammed" Value="Jammed"></asp:ListItem>
                    </asp:DropDownList>
         <br />

         <asp:Button ID="ShowData" runat="server" Text="Show" OnClick="ShowData_Click" />

    <div id="map" style="width:100%;height:500px;"></div>
         </form>

    <!-- 
      The `defer` attribute causes the callback to execute after the full HTML
      document has been parsed. For non-blocking uses, avoiding race conditions,
      and consistent behavior across browsers, consider loading using Promises
      with https://www.npmjs.com/package/@googlemaps/js-api-loader.
      -->
      <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDOy54y7cxhONY3fNQebXH3AOBD6wD40AY&callback=initMap" async defer></script> 

         
  </body>
</html>