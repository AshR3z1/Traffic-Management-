using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace Traffic_Bonn
{
    public class LatLng
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public partial class Map : System.Web.UI.Page
    {
        CDA objCda = new CDA();
        protected void Page_Load(object sender, EventArgs e)
        {

            //string normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='2022-01-22 05:40:00' and a.traffic < 1";
            //DataSet normalDS = objCda.GetDataSet(normalQuery, "SMFM");
            //string increasedQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='2022-01-22 05:40:00' and a.traffic > 1 and a.traffic < 5";
            //DataSet increasedDS = objCda.GetDataSet(increasedQuery, "SMFM");
            //string jamQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='2022-01-22 05:40:00' and a.traffic > 5";
            //DataSet jamDS = objCda.GetDataSet(jamQuery, "SMFM");

            //List<LatLng> normalCordinates = new List<LatLng>();
            //List<LatLng> increasedCoordinates = new List<LatLng>();
            //List<LatLng> jamCoordinates = new List<LatLng>();
            //if (normalDS.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow row in normalDS.Tables[0].Rows)
            //    {
            //        string latValues = row[0].ToString();
            //        string lonValues = row[1].ToString();
            //        string[] latArray = latValues.Split(',');
            //        string[] lonArray = lonValues.Split(',');
            //        for (int i = 0; i < latArray.Length; i++)
            //        {
            //            normalCordinates.Add(new LatLng { lat = Convert.ToDouble(latArray[i]), lng = Convert.ToDouble(lonArray[i]) });
            //        }
            //        break;
            //    }
            //}

            //if (increasedDS.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow row in increasedDS.Tables[0].Rows)
            //    {
            //        string latValues = row[0].ToString();
            //        string lonValues = row[1].ToString();
            //        string[] latArray = latValues.Split(',');
            //        string[] lonArray = lonValues.Split(',');
            //        for (int i = 0; i < latArray.Length; i++)
            //        {
            //            increasedCoordinates.Add(new LatLng { lat = Convert.ToDouble(latArray[i]), lng = Convert.ToDouble(lonArray[i]) });
            //        }

            //    }
            //}

            //if (jamDS != null)
            //{
            //    foreach (DataRow row in jamDS.Tables[0].Rows)
            //    {
            //        string latValues = row[0].ToString();
            //        string lonValues = row[1].ToString();
            //        string[] latArray = latValues.Split(',');
            //        string[] lonArray = lonValues.Split(',');
            //        for (int i = 0; i < latArray.Length; i++)
            //        {
            //            jamCoordinates.Add(new LatLng { lat = Convert.ToDouble(latArray[i]), lng = Convert.ToDouble(lonArray[i]) });
            //        }

            //    }
            //}


            //string normalCoordinatesJson = JsonConvert.SerializeObject(normalCordinates);
            //string increasedCoordinatesJson = JsonConvert.SerializeObject(increasedCoordinates);
            //string jamCoordinatesJson = JsonConvert.SerializeObject(jamCoordinates);
            //array.Text = normalCoordinatesJson;
            //array2.Text = increasedCoordinatesJson;
            //array3.Text = jamCoordinatesJson;



            if (!IsPostBack)
            {

                for (int i = 0; i < 96; i++)
                {
                    ddlTime.Items.Add(new ListItem(DateTime.MinValue.AddMinutes(i * 15).ToString("HH:mm:ss"), (i * 900000).ToString()));

                }

                List<string> myList = new List<string>();

                string normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='2022-01-22 05:40:00' and a.traffic < 1";
                DataSet normalDS = objCda.GetDataSet(normalQuery, "SMFM");

                foreach (DataRow row in normalDS.Tables[0].Rows)
                {
                    List<LatLng> normalCordinates = new List<LatLng>();
                    string lat = row[0].ToString();
                    string lon = row[1].ToString();
                    string[] latArray = lat.Split(',');
                    string[] lonArray = lon.Split(',');
                    for (int i = 0; i < latArray.Length; i++)
                    {
                        normalCordinates.Add(new LatLng { lat = Convert.ToDouble(latArray[i]), lng = Convert.ToDouble(lonArray[i]) });
                    }
                    string normalCoordinatesJson = JsonConvert.SerializeObject(normalCordinates);
                    myList.Add(normalCoordinatesJson);
                }

                string newTest = JsonConvert.SerializeObject(myList);
                arrayaaa.Text = newTest;

            }
            
            //string script = string.Format("initMap({0});", coordinatesJson);
            //ClientScript.RegisterStartupScript(this.GetType(), "Map", script, true);
            //coordinates.Add(new LatLng { lat = 50.5897973, lng = 7.0390291 });
            //coordinates.Add(new LatLng { lat = 50.5900153, lng = 7.0386797 });
            //coordinates.Add(new LatLng { lat = 50.5902096, lng = 7.038224 });
            //coordinates.Add(new LatLng { lat = 50.5905053, lng = 7.0375132 });

            //coordinates2.Add(new LatLng { lat = 50.5897973, lng = 7.0390291 });
            //coordinates2.Add(new LatLng { lat = 50.5904682, lng = 7.0372313 });
            //coordinates2.Add(new LatLng { lat = 50.5909136, lng = 7.036023 });
            //coordinates2.Add(new LatLng { lat = 50.5913937, lng = 7.0347369 });




        }



        protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
        {

            //List<string> myList = new List<string>();
            //string normalQuery = "";
            //if (ddlTest.SelectedValue == "Low") {
            //    normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='2022-01-22 05:40:00' and a.traffic < 1";
            //}
            //else if (ddlTest.SelectedValue == "Normal") {
            //    normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='2022-01-22 05:40:00' and a.traffic > 1 and a.traffic < 4";
            //}
            //else if (ddlTest.SelectedValue == "Increased") {
            //    normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='2022-01-22 05:40:00' and a.traffic > 4 and a.traffic < 7";
            //}
            //else {
            //    normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='2022-01-22 05:40:00' and a.traffic > 7";
            //}
            
            
           
            
            //DataSet normalDS = objCda.GetDataSet(normalQuery, "SMFM");
            //if (normalDS!=null)
            //{
            //    foreach (DataRow row in normalDS.Tables[0].Rows)
            //    {
            //        List<LatLng> normalCordinates = new List<LatLng>();
            //        string lat = row[0].ToString();
            //        string lon = row[1].ToString();
            //        string[] latArray = lat.Split(',');
            //        string[] lonArray = lon.Split(',');
            //        for (int i = 0; i < latArray.Length; i++)
            //        {
            //            normalCordinates.Add(new LatLng { lat = Convert.ToDouble(latArray[i]), lng = Convert.ToDouble(lonArray[i]) });
            //        }
            //        string normalCoordinatesJson = JsonConvert.SerializeObject(normalCordinates);
            //        myList.Add(normalCoordinatesJson);
            //    }

            //    string newTest = JsonConvert.SerializeObject(myList);
            //    arrayaaa.Text = newTest;

            //}
            //else
            //{
            //    arrayaaa.Text = "[0]";
            //}



        }

        protected void ShowData_Click(object sender, EventArgs e)
        {
            string traffic = ddlTest.SelectedValue.ToString();
            string  time= ddlTime.SelectedItem.ToString();
            string day = datePicker.Text;

            string dayAndTime = day + " " + time;



            List<string> myList = new List<string>();
            string normalQuery = "";
            if (ddlTest.SelectedValue == "Low")
            {
                normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='"+dayAndTime+"' and a.traffic < 1";
            }
            else if (ddlTest.SelectedValue == "Normal")
            {
                normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='" + dayAndTime + "' and a.traffic > 1 and a.traffic < 4";
            }
            else if (ddlTest.SelectedValue == "Increased")
            {
                normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='" + dayAndTime + "' and a.traffic > 4 and a.traffic < 7";
            }
            else
            {
                normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='" + dayAndTime + "' and a.traffic > 7";
            }




            DataSet normalDS = objCda.GetDataSet(normalQuery, "SMFM");
            if (normalDS != null)
            {
                foreach (DataRow row in normalDS.Tables[0].Rows)
                {
                    List<LatLng> normalCordinates = new List<LatLng>();
                    string lat = row[0].ToString();
                    string lon = row[1].ToString();
                    string[] latArray = lat.Split(',');
                    string[] lonArray = lon.Split(',');
                    for (int i = 0; i < latArray.Length; i++)
                    {
                        normalCordinates.Add(new LatLng { lat = Convert.ToDouble(latArray[i]), lng = Convert.ToDouble(lonArray[i]) });
                    }
                    string normalCoordinatesJson = JsonConvert.SerializeObject(normalCordinates);
                    myList.Add(normalCoordinatesJson);
                }

                string newTest = JsonConvert.SerializeObject(myList);
                arrayaaa.Text = newTest;

            }

            else
            {
                arrayaaa.Text = "[0]";
            }
        }
    }

}