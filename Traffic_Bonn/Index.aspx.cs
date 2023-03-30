using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Traffic_Bonn
{
    public partial class Index : System.Web.UI.Page
    {
        CDA objCda = new CDA();
        private static int tempVaal =0;
        private static int tempval =0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                for (int i = 0; i < 96; i++)
                {
                    ddlTime.Items.Add(new ListItem(DateTime.MinValue.AddMinutes(i * 10).ToString("HH:mm:ss"), (i * 900000).ToString()));
                    dtltime.Items.Add(new ListItem(DateTime.MinValue.AddMinutes(i * 10).ToString("HH:mm:ss"), (i * 900000).ToString()));

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
                flagValue.Text = "[5]";






                //Bar chart using weather condition

                string[] rainyArray = new string[] { };
                string[] normalArray = new string[] { };
                string[] snowyArray = new string[] { };

                string queryByweatherCondition = "SELECT[time] = DATEADD(MONTH, DATEDIFF(MONTH, 0, [time]), 0),weather_condition,COUNT(incidentID) TotalCount FROM    traffic_bonn WHERE[time] >= '2021-12-01' AND[time] <= '2022-10-31' AND incidentID > 0 GROUP BY DATEADD(MONTH, DATEDIFF(MONTH, 0, [time]), 0), weather_condition; ";
                DataSet ds1 = objCda.GetDataSet(queryByweatherCondition, "SMFM");
                List<object> colValues = new List<object>();
                foreach (DataRow row in ds1.Tables[0].Rows)
                {
                    if (row[1].ToString() == "rain")
                    {

                        rainyArray = rainyArray.Append(row[2].ToString()).ToArray();
                    }
                    else if (row[1].ToString() == "snowy")
                    {
                        snowyArray = snowyArray.Append(row[2].ToString()).ToArray();
                    }
                    else if (row[1].ToString() == "normal")
                    {
                        normalArray = normalArray.Append(row[2].ToString()).ToArray();
                    }


                }
                string rainvalue = "[";
                rainvalue += string.Join(", ", rainyArray);
                rainvalue += "]";

                string normalValue = "[";
                normalValue += string.Join(", ", normalArray);
                normalValue += "]";

                string snowyValue = "[";
                snowyValue += string.Join(", ", snowyArray);
                snowyValue += "]";
                rainArrayScript.Text = rainvalue;
                noamalArrayScript.Text = normalValue;
                snowyArrayScript.Text = snowyValue;

                //traffic percentage during day

                string[] levelOneArray = new string[] { };
                string[] levelFourArray = new string[] { };
                string[] levelSevenArray = new string[] { };
                string[] levelTenArray = new string[] { };

                for (int i = 0; i < 24; i += 6)
                {
                    string queryIntervalTraffic = "";
                    if (i == 6)
                    {
                        queryIntervalTraffic = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn where [time] between '2022-01-22 0" + i + ":00:00' and '2022-01-22 " + (i + 6) + ":00:00') t group by t.range";
                    }
                    else if (i > 6 && i < 18)
                    {
                        queryIntervalTraffic = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn where [time] between '2022-01-22 " + i + ":00:00' and '2022-01-22 " + (i + 6) + ":00:00') t group by t.range";
                    }
                    else if (i == 18)
                    {
                        queryIntervalTraffic = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn where [time] between '2022-01-22 " + i + ":00:00' and '2022-01-22 23:59:59') t group by t.range";
                    }
                    else
                    {
                        queryIntervalTraffic = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn where [time] between '2022-01-22 0" + i + ":00:00' and '2022-01-22 0" + (i + 6) + ":00:00') t group by t.range";
                    }
                    DataSet ds2 = objCda.GetDataSet(queryIntervalTraffic, "SMFM");

                    foreach (DataRow row in ds2.Tables[0].Rows)
                    {
                        if (row[0].ToString() == "1")
                        {
                            levelOneArray = levelOneArray.Append(row[1].ToString()).ToArray();
                        }
                        else if (row[0].ToString() == "4")
                        {
                            levelFourArray = levelFourArray.Append(row[1].ToString()).ToArray();
                        }
                        else if (row[0].ToString() == "7")
                        {
                            levelSevenArray = levelSevenArray.Append(row[1].ToString()).ToArray();
                        }
                        else if (row[0].ToString() == "10")
                        {
                            levelTenArray = levelTenArray.Append(row[1].ToString()).ToArray();
                        }


                    }

                }
                if (levelOneArray.Count() == 0)
                {
                    levelOneArray = new string[] { "0", "0", "0", "0" };
                }
                else if (levelFourArray.Count() == 0)
                {
                    levelFourArray = new string[] { "0", "0", "0", "0" };
                }
                else if (levelSevenArray.Count() == 0)
                {
                    levelSevenArray = new string[] { "0", "0", "0", "0" };
                }
                else if (levelTenArray.Count() == 0)
                {
                    levelTenArray = new string[] { "0", "0", "0", "0" };
                }

                float value = 0;
                for (int i = 0; i < 4; i++)
                {
                    value = Int32.Parse(levelOneArray[i]) + Int32.Parse(levelFourArray[i]) + Int32.Parse(levelSevenArray[i]) + Int32.Parse(levelTenArray[i]);
                    levelOneArray[i] = ((Int32.Parse(levelOneArray[i]) / value) * 100).ToString("0.00");
                    levelFourArray[i] = ((Int32.Parse(levelFourArray[i]) / value) * 100).ToString("0.00");
                    levelSevenArray[i] = ((Int32.Parse(levelSevenArray[i]) / value) * 100).ToString("0.00");
                    levelTenArray[i] = ((Int32.Parse(levelTenArray[i]) / value) * 100).ToString("0.00");
                }

                string levelOneValue = "[";
                levelOneValue += string.Join(", ", levelOneArray);
                levelOneValue += "]";

                string levelFourValue = "[";
                levelFourValue += string.Join(", ", levelFourArray);
                levelFourValue += "]";

                string levelSevenValue = "[";
                levelSevenValue += string.Join(", ", levelSevenArray);
                levelSevenValue += "]";

                string levelTenValue = "[";
                levelTenValue += string.Join(", ", levelTenArray);
                levelTenValue += "]";
                LevelOneArrayScript.Text = levelOneValue;
                LevelFourArrayScript.Text = levelFourValue;
                LevelSevenArrayScript.Text = levelSevenValue;
                LeveltenArrayScript.Text = levelTenValue;

                //traffic percentage according to highway

                string[] levelOneArrayRoad = new string[] { };
                string[] levelFourArrayRoad = new string[] { };
                string[] levelSevenArrayRoad = new string[] { };
                string[] levelTenArrayRoad = new string[] { };

                for (int i = 0; i < 24; i += 6)
                {
                    string queryIntervalTrafficRoad = "";
                    if (i == 6)
                    {
                        queryIntervalTrafficRoad = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn a inner join mapData b on a.opath_id=b.fid where a.[time] between '2022-01-22 0" + i + ":00:00' and '2022-01-22 " + (i + 6) + ":00:00' and b.highway='motorway' ) t group by t.range";
                    }
                    else if (i > 6 && i < 18)
                    {
                        queryIntervalTrafficRoad = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn a inner join mapData b on a.opath_id=b.fid where a.[time] between '2022-01-22 " + i + ":00:00' and '2022-01-22 " + (i + 6) + ":00:00' and b.highway='motorway' ) t group by t.range";
                    }
                    else if (i == 18)
                    {
                        queryIntervalTrafficRoad = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn a inner join mapData b on a.opath_id=b.fid where a.[time] between '2022-01-22 " + i + ":00:00' and '2022-01-22 23:59:59' and b.highway='motorway' ) t group by t.range";
                    }
                    else
                    {
                        queryIntervalTrafficRoad = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn a inner join mapData b on a.opath_id=b.fid where a.[time] between '2022-01-22 0" + i + ":00:00' and '2022-01-22 0" + (i + 6) + ":00:00' and b.highway='motorway' ) t group by t.range";
                    }
                    DataSet ds3 = objCda.GetDataSet(queryIntervalTrafficRoad, "SMFM");

                    string[] numbers = { "1", "4", "7", "10" };

                    var result = numbers.Except(ds3.Tables[0].AsEnumerable().Select(r => r.Field<string>("Level")));

                    if (result.Any())
                    {
                        foreach (var item in result)
                        {
                            if (item == "7")
                            {
                                levelSevenArrayRoad = levelSevenArrayRoad.Append("0").ToArray();
                            }
                            else if (item == "10")
                            {
                                levelTenArrayRoad = levelTenArrayRoad.Append("0").ToArray();
                            }
                            else if (item == "1")
                            {
                                levelOneArrayRoad = levelOneArrayRoad.Append("0").ToArray();
                            }
                            else if (item == "4")
                            {
                                levelFourArrayRoad = levelFourArrayRoad.Append("0").ToArray();
                            }
                        }
                    }

                    foreach (DataRow row in ds3.Tables[0].Rows)
                    {


                        if (row[0].ToString() == "1")
                        {
                            levelOneArrayRoad = levelOneArrayRoad.Append(row[1].ToString()).ToArray();
                        }
                        else if (row[0].ToString() == "4")
                        {
                            levelFourArrayRoad = levelFourArrayRoad.Append(row[1].ToString()).ToArray();
                        }
                        else if (row[0].ToString() == "7")
                        {
                            levelSevenArrayRoad = levelSevenArrayRoad.Append(row[1].ToString()).ToArray();
                        }
                        else if (row[0].ToString() == "10")
                        {
                            levelTenArrayRoad = levelTenArrayRoad.Append(row[1].ToString()).ToArray();
                        }
                        else
                        {
                            int minLength = int.MaxValue;
                            string[] minArray = null;

                            foreach (string[] array in new[] { levelOneArrayRoad, levelFourArrayRoad, levelSevenArrayRoad, levelTenArrayRoad })
                            {
                                if (array.Length < minLength)
                                {
                                    minLength = array.Length;
                                    minArray = array;
                                }
                            }
                        }


                    }

                }



                float valueRoad = 0;
                for (int i = 0; i < 4; i++)
                {
                    valueRoad = Int32.Parse(levelOneArrayRoad[i]) + Int32.Parse(levelFourArrayRoad[i]) + Int32.Parse(levelSevenArrayRoad[i]) + Int32.Parse(levelTenArrayRoad[i]);
                    levelOneArrayRoad[i] = ((Int32.Parse(levelOneArrayRoad[i]) / valueRoad) * 100).ToString("0.00");
                    levelFourArrayRoad[i] = ((Int32.Parse(levelFourArrayRoad[i]) / valueRoad) * 100).ToString("0.00");
                    levelSevenArrayRoad[i] = ((Int32.Parse(levelSevenArrayRoad[i]) / valueRoad) * 100).ToString("0.00");
                    levelTenArrayRoad[i] = ((Int32.Parse(levelTenArrayRoad[i]) / valueRoad) * 100).ToString("0.00");
                }

                string levelOneValueRoad = "[";
                levelOneValueRoad += string.Join(", ", levelOneArrayRoad);
                levelOneValueRoad += "]";

                string levelFourValueRoad = "[";
                levelFourValueRoad += string.Join(", ", levelFourArrayRoad);
                levelFourValueRoad += "]";

                string levelSevenValueRoad = "[";
                levelSevenValueRoad += string.Join(", ", levelSevenArrayRoad);
                levelSevenValueRoad += "]";

                string levelTenValueRoad = "[";
                levelTenValueRoad += string.Join(", ", levelTenArrayRoad);
                levelTenValueRoad += "]";
                LevelOneArrayRoadScript.Text = levelOneValueRoad;
                LevelFourArrayRoadScript.Text = levelFourValueRoad;
                LevelSevenArrayRoadScript.Text = levelSevenValueRoad;
                LeveltenArrayRoadScript.Text = levelTenValueRoad;

                //incident according to time of day

                string[] trafficOnSelectedArray = new string[4];
                string queryTrafficByNight = "select count(incidentID) from traffic_bonn where incidentID> 0  and [time] between '2021-12-21 00:00:00' and '2021-12-21 05:59:59' and (source='TRAFFIC_OD' or source='TRAFFIC_HERE' or source='INCIDENT_HERE')";
                string trafficOfNight = objCda.getSingleValue(queryTrafficByNight, "SMFM").ToString();
                string queryTrafficByDay = "select count(incidentID) from traffic_bonn where incidentID> 0  and [time] between '2021-12-21 06:00:00' and '2021-12-21 11:59:59' and (source='TRAFFIC_OD' or source='TRAFFIC_HERE' or source='INCIDENT_HERE')";
                string trafficOfDay = objCda.getSingleValue(queryTrafficByDay, "SMFM").ToString();
                string queryTrafficByNoon = "select count(incidentID) from traffic_bonn where incidentID> 0  and [time] between '2021-12-21 12:00:00' and '2021-12-21 17:59:59' and (source='TRAFFIC_OD' or source='TRAFFIC_HERE' or source='INCIDENT_HERE')";
                string trafficOfNoon = objCda.getSingleValue(queryTrafficByNoon, "SMFM").ToString();
                string queryTrafficByEvening = "select count(incidentID) from traffic_bonn where incidentID> 0  and [time] between '2021-12-21 18:00:00' and '2021-12-21 23:59:59' and (source='TRAFFIC_OD' or source='TRAFFIC_HERE' or source='INCIDENT_HERE')";
                string trafficOfEvening = objCda.getSingleValue(queryTrafficByEvening, "SMFM").ToString();
                trafficOnSelectedArray[0] = trafficOfNight;
                trafficOnSelectedArray[1] = trafficOfDay;
                trafficOnSelectedArray[2] = trafficOfNoon;
                trafficOnSelectedArray[3] = trafficOfEvening;

                string trafficOnSelectedNightValue = "[";
                trafficOnSelectedNightValue += string.Join(", ", trafficOnSelectedArray);
                trafficOnSelectedNightValue += "]";

                dayArrayScript.Text = trafficOnSelectedNightValue;


                //pie chart showing percentage

                string incidentPercentage = "";
                string queryTotalTrafficADay = "select sum(traffic)  from traffic_bonn where[time] between '2022-01-22 00:00:00' and '2022-01-22 23:59:59'";
                string totalTrafficValue = objCda.getSingleValue(queryTotalTrafficADay, "SMFM").ToString();
                int arrayIndex = 0;
                string[] valuearray = new string[4];

                for (int i = 0; i < 24; i = i + 6)
                {
                    string query4 = "";


                    if (i == 6)
                    {
                        query4 = "select sum(traffic)  from traffic_bonn where[time] between '2022-01-22 0" + i + ":00:00' and '2022-01-22 " + (i + 6) + ":00:00'";
                    }
                    else if (i > 6 && i < 18)
                    {
                        query4 = "select sum(traffic)  from traffic_bonn where[time] between '2022-01-22 " + i + ":00:00' and '2022-01-22 " + (i + 6) + ":00:00'";
                    }
                    else if (i == 18)
                    {
                        query4 = "select sum(traffic)  from traffic_bonn where[time] between '2022-01-22 " + i + ":00:00' and '2022-01-22 23:59:59'";
                    }
                    else
                    {
                        query4 = "select sum(traffic)  from traffic_bonn where[time] between '2022-01-22 0" + i + ":00:00' and '2022-01-22 0" + (i + 6) + ":00:00'";
                    }
                    string values = objCda.getSingleValue(query4, "SMFM").ToString();



                    incidentPercentage = ((float.Parse(values) / float.Parse(totalTrafficValue)) * 100).ToString("0.00");
                    valuearray[arrayIndex] = incidentPercentage;
                    arrayIndex++;
                }
                string s = "[";
                s += string.Join(", ", valuearray);
                s += "]";
                //string test = "[55, 49, 44, 24, 15]";
                //piechartvalue.value = "[55, 49, 44, 24, 15]";

                litScript.Text = s;

            }
        }


        protected void ShowData_Click(object sender, EventArgs e)
        {
            string traffic = ddlTest.SelectedValue.ToString();
            string time = ddlTime.SelectedItem.ToString();
            string Totime = dtltime.SelectedItem.ToString();
            string day = datePicker.Text;
            string toDay =toDatePicker.Text;
            string dayAndTime = day + " " + time;
            string toDayAndTime = toDay + " " + Totime;

            if(ddlSelectType.SelectedValue == "Road")
            {
                List<string> myList = new List<string>();
                List<string> lowList = new List<string>();
                List<string> normalList = new List<string>();
                List<string> increasedList = new List<string>();
                List<string> jammedList = new List<string>();
                string normalQuery = "";
                string allQuery = "";
                string lowQuery = "";
                string increasedQuery = "";
                string jammedQuery = "";
                if (ddlTest.SelectedValue == "All")
                {
                    flagValue.Text = "[1]";
                    //lowQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time between '" + dayAndTime + "' and '" + toDayAndTime + "' and a.traffic < 1";
                    //lowQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time='2022-01-22 05:40:00' and a.traffic < 1";

                    //increasedQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time between '" + dayAndTime + "' and '" + toDayAndTime + "' and a.traffic > 4 and a.traffic < 7";
                    //jammedQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time between '" + dayAndTime + "' and '" + toDayAndTime + "' and a.traffic > 7";
                    ////DataSet lowDS = objCda.GetDataSet(lowQuery, "SMFM");
                    ////DataSet normalDS = objCda.GetDataSet(normalQuery, "SMFM");
                    ////DataSet increasedDS = objCda.GetDataSet(increasedQuery, "SMFM");
                    ////DataSet jammedDS = objCda.GetDataSet(jammedQuery, "SMFM");



                    allQuery = "SELECT AVG(traffic) as traffic,b.lat,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time between '" + dayAndTime + "' and '" + toDayAndTime + "' and incidentType='0' group by a.opath_id,b.lat,b.lon";

                    DataSet allDs = objCda.GetDataSet(allQuery, "SMFM");

                    DataSet lowDS = new DataSet();
                    DataTable lowDataTable = new DataTable();
                    lowDataTable.Columns.Add("lat", typeof(string));
                    lowDataTable.Columns.Add("lon", typeof(string));
                    lowDS.Tables.Add(lowDataTable);

                    DataSet normalDS = new DataSet();
                    DataTable normalDataTable = new DataTable();
                    normalDataTable.Columns.Add("lat", typeof(string));
                    normalDataTable.Columns.Add("lon", typeof(string));
                    normalDS.Tables.Add(normalDataTable);

                    DataSet increasedDS = new DataSet();
                    DataTable increasedDataTable = new DataTable();
                    increasedDataTable.Columns.Add("lat", typeof(string));
                    increasedDataTable.Columns.Add("lon", typeof(string));
                    increasedDS.Tables.Add(increasedDataTable);

                    DataSet jammedDS = new DataSet();
                    DataTable jammedDataTable = new DataTable();
                    jammedDataTable.Columns.Add("lat", typeof(string));
                    jammedDataTable.Columns.Add("lon", typeof(string));
                    jammedDS.Tables.Add(jammedDataTable);

                    
                    for(int i = 0; i < allDs.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToDouble(allDs.Tables[0].Rows[i]["traffic"]) <= 1)
                        {
                            DataRow lowDataRow = lowDataTable.NewRow();
                            // Set the values for the row
                            lowDataRow["lat"] = allDs.Tables[0].Rows[i]["lat"].ToString();
                            lowDataRow["lon"] = allDs.Tables[0].Rows[i]["lon"].ToString();
                            lowDataTable.Rows.Add(lowDataRow);
                        }
                        else if(Convert.ToDouble(allDs.Tables[0].Rows[i]["traffic"]) > 1 && Convert.ToDouble(allDs.Tables[0].Rows[i]["traffic"]) <= 4)
                        {
                            DataRow normalDataRow = normalDataTable.NewRow();
                            // Set the values for the row
                            normalDataRow["lat"] = allDs.Tables[0].Rows[i]["lat"].ToString();
                            normalDataRow["lon"] = allDs.Tables[0].Rows[i]["lon"].ToString();
                            normalDataTable.Rows.Add(normalDataRow);
                        }
                        else if (Convert.ToDouble(allDs.Tables[0].Rows[i]["traffic"]) > 4 && Convert.ToDouble(allDs.Tables[0].Rows[i]["traffic"]) <= 7)
                        {
                            DataRow increasedDataRow = increasedDataTable.NewRow();
                            // Set the values for the row
                            increasedDataRow["lat"] = allDs.Tables[0].Rows[i]["lat"].ToString();
                            increasedDataRow["lon"] = allDs.Tables[0].Rows[i]["lon"].ToString();
                            increasedDataTable.Rows.Add(increasedDataRow);
                        }
                        else
                        {
                            DataRow jammedDataRow = jammedDataTable.NewRow();
                            // Set the values for the row
                            jammedDataRow["lat"] = allDs.Tables[0].Rows[i]["lat"].ToString();
                            jammedDataRow["lon"] = allDs.Tables[0].Rows[i]["lon"].ToString();
                            jammedDataTable.Rows.Add(jammedDataRow);
                        }
                    }
                    if (lowDS != null)
                    {
                        foreach (DataRow row in lowDS.Tables[0].Rows)
                        {
                            List<LatLng> lowCordinates = new List<LatLng>();
                            string lat = row[0].ToString();
                            string lon = row[1].ToString();
                            string[] latArray = lat.Split(',');
                            string[] lonArray = lon.Split(',');
                            for (int i = 0; i < latArray.Length; i++)
                            {
                                lowCordinates.Add(new LatLng { lat = Convert.ToDouble(latArray[i]), lng = Convert.ToDouble(lonArray[i]) });
                            }
                            string lowCoordinatesJson = JsonConvert.SerializeObject(lowCordinates);
                            lowList.Add(lowCoordinatesJson);
                        }

                        string lowTest = JsonConvert.SerializeObject(lowList);
                        int lowCount = lowList.Count;
                        lowArray1.Text = lowTest;

                    }

                    else
                    {
                        lowArray1.Text = "[0]";
                    }

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

                            normalList.Add(normalCoordinatesJson);
                            int lowCount = normalList.Count;
                        }

                        string newTest = JsonConvert.SerializeObject(normalList);
                        normalArray.Text = newTest;

                    }

                    else
                    {
                        normalArray.Text = "[0]";
                    }


                    if (increasedDS != null)
                    {
                        foreach (DataRow row in increasedDS.Tables[0].Rows)
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

                            increasedList.Add(normalCoordinatesJson);
                        }

                        string newTest = JsonConvert.SerializeObject(normalList);
                        incraesedArray.Text = newTest;

                    }

                    else
                    {
                        incraesedArray.Text = "[0]";
                    }
                    if (jammedDS != null)
                    {
                        foreach (DataRow row in jammedDS.Tables[0].Rows)
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

                            jammedList.Add(normalCoordinatesJson);
                        }

                        string newTest = JsonConvert.SerializeObject(normalList);
                        jammedArray.Text = newTest;

                    }

                    else
                    {
                        jammedArray.Text = "[0]";
                    }
                }
                else
                {
                    flagValue.Text = "[5]";
                    if (ddlTest.SelectedValue == "Low")
                    {

                        normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time between '" + dayAndTime + "' and '" + toDayAndTime + "'and a.traffic < 1";

                    }
                    else if (ddlTest.SelectedValue == "Normal")
                    {
                        normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time between '" + dayAndTime + "' and '" + toDayAndTime + "'and a.traffic > 1 and a.traffic < 4";
                    }
                    else if (ddlTest.SelectedValue == "Increased")
                    {
                        normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time between '" + dayAndTime + "' and '" + toDayAndTime + "'and a.traffic > 4 and a.traffic < 7";
                    }
                    else
                    {
                        normalQuery = "SELECT b.lat ,b.lon FROM mapData b inner join traffic_bonn a on a.opath_id=b.fid where a.time between '" + dayAndTime + "' and '" + toDayAndTime + "'and a.traffic > 7";
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

            else
            {
                List<string> myList = new List<string>();
                List<string> lowList = new List<string>();
                List<string> normalList = new List<string>();
                List<string> increasedList = new List<string>();
                List<string> jammedList = new List<string>();
                string normalQuery = "";
                string lowQuery = "";
                string increasedQuery = "";
                string jammedQuery = "";

                //marker
                flagValue.Text = "[5]";
                if (ddlIncidentType.SelectedValue == "ACCIDENT")
                {
                    normalQuery = "SELECT LEFT(b.lat, CHARINDEX(',', b.lat + ',')-1) as lat,LEFT(b.lon, CHARINDEX(',', b.lon + ',')-1) as lon,COUNT(*) as count,a.opath_id  FROM  mapData b  INNER JOIN traffic_bonn a ON a.opath_id = b.fid WHERE a.time between '" + dayAndTime + "' and '" + toDayAndTime + "' AND a.incidentType = 'ACCIDENT'  GROUP BY a.opath_id, LEFT(b.lat, CHARINDEX(',', b.lat + ',')-1), LEFT(b.lon, CHARINDEX(',', b.lon + ',')-1);";
                }
                else if (ddlIncidentType.SelectedValue == "CONSTRUCTION")
                {
                    normalQuery = "SELECT LEFT(b.lat, CHARINDEX(',', b.lat + ',')-1) as lat,LEFT(b.lon, CHARINDEX(',', b.lon + ',')-1) as lon,COUNT(*) as count,a.opath_id  FROM  mapData b  INNER JOIN traffic_bonn a ON a.opath_id = b.fid WHERE a.time between '" + dayAndTime + "' and '" + toDayAndTime + "' AND a.incidentType = 'CONSTRUCTION'  GROUP BY a.opath_id, LEFT(b.lat, CHARINDEX(',', b.lat + ',')-1), LEFT(b.lon, CHARINDEX(',', b.lon + ',')-1);";
                }
                else if (ddlIncidentType.SelectedValue == "CONGESTION")
                {
                    normalQuery = "SELECT LEFT(b.lat, CHARINDEX(',', b.lat + ',')-1) as lat,LEFT(b.lon, CHARINDEX(',', b.lon + ',')-1) as lon,COUNT(*) as count,a.opath_id  FROM  mapData b  INNER JOIN traffic_bonn a ON a.opath_id = b.fid WHERE a.time between '" + dayAndTime + "' and '" + toDayAndTime + "' AND a.incidentType = 'CONGESTION'  GROUP BY a.opath_id, LEFT(b.lat, CHARINDEX(',', b.lat + ',')-1), LEFT(b.lon, CHARINDEX(',', b.lon + ',')-1);";
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

      
            


            string selectedDay = datePicker.Text;
            string toSelectedDay = toDatePicker.Text;


            string[] levelOneArray = new string[] { };
            string[] levelFourArray = new string[] { };
            string[] levelSevenArray = new string[] { };
            string[] levelTenArray = new string[] { };

            for (int i = 0; i < 24; i += 6)
            {
                string queryIntervalTraffic = "";
                if (i == 6)
                {
                    queryIntervalTraffic = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn where [time] between '" + selectedDay + " 0" + i + ":00:00' and '" + selectedDay + " " + (i + 6) + ":00:00') t group by t.range";
                }
                else if (i > 6 && i < 18)
                {
                    queryIntervalTraffic = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn where [time] between '" + selectedDay + " " + i + ":00:00' and '" + selectedDay + " " + (i + 6) + ":00:00') t group by t.range";
                }
                else if (i == 18)
                {
                    queryIntervalTraffic = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn where [time] between '" + selectedDay + " " + i + ":00:00' and '" + selectedDay + " 23:59:59') t group by t.range";
                }
                else
                {
                    queryIntervalTraffic = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn where [time] between '" + selectedDay + " 0" + i + ":00:00' and '" + selectedDay + " 0" + (i + 6) + ":00:00') t group by t.range";
                }
                DataSet ds2 = objCda.GetDataSet(queryIntervalTraffic, "SMFM");

                foreach (DataRow row in ds2.Tables[0].Rows)
                {
                    if (row[0].ToString() == "1")
                    {
                        levelOneArray = levelOneArray.Append(row[1].ToString()).ToArray();
                    }
                    else if (row[0].ToString() == "4")
                    {
                        levelFourArray = levelFourArray.Append(row[1].ToString()).ToArray();
                    }
                    else if (row[0].ToString() == "7")
                    {
                        levelSevenArray = levelSevenArray.Append(row[1].ToString()).ToArray();
                    }
                    else
                    {
                        levelTenArray = levelTenArray.Append(row[1].ToString()).ToArray();
                    }


                }

            }

            float value = 0;
            for (int i = 0; i < 4; i++)
            {
                value = Int32.Parse(levelOneArray[i]) + Int32.Parse(levelFourArray[i]) + Int32.Parse(levelSevenArray[i]) + Int32.Parse(levelTenArray[i]);
                levelOneArray[i] = ((Int32.Parse(levelOneArray[i]) / value) * 100).ToString("0.00");
                levelFourArray[i] = ((Int32.Parse(levelFourArray[i]) / value) * 100).ToString("0.00");
                levelSevenArray[i] = ((Int32.Parse(levelSevenArray[i]) / value) * 100).ToString("0.00");
                levelTenArray[i] = ((Int32.Parse(levelTenArray[i]) / value) * 100).ToString("0.00");
            }

            string levelOneValue = "[";
            levelOneValue += string.Join(", ", levelOneArray);
            levelOneValue += "]";

            string levelFourValue = "[";
            levelFourValue += string.Join(", ", levelFourArray);
            levelFourValue += "]";

            string levelSevenValue = "[";
            levelSevenValue += string.Join(", ", levelSevenArray);
            levelSevenValue += "]";

            string levelTenValue = "[";
            levelTenValue += string.Join(", ", levelTenArray);
            levelTenValue += "]";
            LevelOneArrayScript.Text = levelOneValue;
            LevelFourArrayScript.Text = levelFourValue;
            LevelSevenArrayScript.Text = levelSevenValue;
            LeveltenArrayScript.Text = levelTenValue;



            string[] trafficOnSelectedArray = new string[4];
            string queryTrafficByNight = "select count(incidentID) from traffic_bonn where incidentID> 0  and [time] between '" + selectedDay + " 00:00:00' and '" + selectedDay + " 05:59:59' and (source='TRAFFIC_OD' or source='TRAFFIC_HERE' or source='INCIDENT_HERE')";
            string trafficOfNight = objCda.getSingleValue(queryTrafficByNight, "SMFM").ToString();
            string queryTrafficByDay = "select count(incidentID) from traffic_bonn where incidentID> 0  and [time] between '" + selectedDay + " 06:00:00' and '" + selectedDay + " 11:59:59' and (source='TRAFFIC_OD' or source='TRAFFIC_HERE' or source='INCIDENT_HERE')";
            string trafficOfDay = objCda.getSingleValue(queryTrafficByDay, "SMFM").ToString();
            string queryTrafficByNoon = "select count(incidentID) from traffic_bonn where incidentID> 0  and [time] between '" + selectedDay + " 12:00:00' and '" + selectedDay + " 17:59:59' and (source='TRAFFIC_OD' or source='TRAFFIC_HERE' or source='INCIDENT_HERE')";
            string trafficOfNoon = objCda.getSingleValue(queryTrafficByNoon, "SMFM").ToString();
            string queryTrafficByEvening = "select count(incidentID) from traffic_bonn where incidentID> 0  and [time] between '" + selectedDay + " 18:00:00' and '" + selectedDay + " 23:59:59' and (source='TRAFFIC_OD' or source='TRAFFIC_HERE' or source='INCIDENT_HERE')";
            string trafficOfEvening = objCda.getSingleValue(queryTrafficByEvening, "SMFM").ToString();
            trafficOnSelectedArray[0] = trafficOfNight;
            trafficOnSelectedArray[1] = trafficOfDay;
            trafficOnSelectedArray[2] = trafficOfNoon;
            trafficOnSelectedArray[3] = trafficOfEvening;

            string trafficOnSelectedNightValue = "[";
            trafficOnSelectedNightValue += string.Join(", ", trafficOnSelectedArray);
            trafficOnSelectedNightValue += "]";

            dayArrayScript.Text = trafficOnSelectedNightValue;



            string incidentPercentage = "";
            string queryTotalTrafficADay = "select sum(traffic)  from traffic_bonn where[time] between '" + selectedDay + " 00:00:00' and '" + selectedDay + " 23:59:59'";
            string totalTrafficValue = objCda.getSingleValue(queryTotalTrafficADay, "SMFM").ToString();
            int arrayIndex = 0;
            string[] valuearray = new string[4];

            for (int i = 0; i < 24; i = i + 6)
            {
                string query4 = "";


                if (i == 6)
                {
                    query4 = "select sum(traffic)  from traffic_bonn where[time] between '" + selectedDay + " 0" + i + ":00:00' and '" + selectedDay + " " + (i + 6) + ":00:00'";
                }
                else if (i > 6 && i < 18)
                {
                    query4 = "select sum(traffic)  from traffic_bonn where[time] between '" + selectedDay + " " + i + ":00:00' and '" + selectedDay + " " + (i + 6) + ":00:00'";
                }
                else if (i == 18)
                {
                    query4 = "select sum(traffic)  from traffic_bonn where[time] between '" + selectedDay + " " + i + ":00:00' and '" + selectedDay + " 23:59:59'";
                }
                else
                {
                    query4 = "select sum(traffic)  from traffic_bonn where[time] between '" + selectedDay + " 0" + i + ":00:00' and '" + selectedDay + " 0" + (i + 6) + ":00:00'";
                }
                string values = objCda.getSingleValue(query4, "SMFM").ToString();



                incidentPercentage = ((float.Parse(values) / float.Parse(totalTrafficValue)) * 100).ToString("0.00");
                valuearray[arrayIndex] = incidentPercentage;
                arrayIndex++;
            }
            string s = "[";
            s += string.Join(", ", valuearray);
            s += "]";


            litScript.Text = s;
        }




        protected void ddlSelectTypeChanged(object sender, EventArgs e)
        {
            if (ddlSelectType.SelectedValue == "Marker")
            {
                ddlIncidentType.Visible = true;
                ddlTest.Visible = false;
            }
            else
            {
                ddlTest.Visible = true;
                ddlIncidentType.Visible = false;
            }
        }












            protected void ddlRoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDay = datePicker.Text;
            if(selectedDay == "")
            {
                selectedDay = "2022-01-22";
            }


            string[] levelOneArrayRoad = new string[] { };
            string[] levelFourArrayRoad = new string[] { };
            string[] levelSevenArrayRoad = new string[] { };
            string[] levelTenArrayRoad = new string[] { };

            for (int i = 0; i < 24; i += 6)
            {
                string queryIntervalTrafficRoad = "";
                if (i == 6)
                {
                    queryIntervalTrafficRoad = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn a inner join mapData b on a.opath_id=b.fid where a.[time] between '" + selectedDay + " 0" + i + ":00:00' and '" + selectedDay + " " + (i + 6) + ":00:00' and b.highway='" + ddlRoadType.SelectedItem.Value + "' ) t group by t.range";
                }
                else if (i > 6 && i < 18)
                {
                    queryIntervalTrafficRoad = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn a inner join mapData b on a.opath_id=b.fid where a.[time] between '" + selectedDay + " " + i + ":00:00' and '" + selectedDay + " " + (i + 6) + ":00:00' and b.highway='" + ddlRoadType.SelectedItem.Value + "' ) t group by t.range";
                }
                else if (i == 18)
                {
                    queryIntervalTrafficRoad = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn a inner join mapData b on a.opath_id=b.fid where a.[time] between '" + selectedDay + " " + i + ":00:00' and '" + selectedDay + " 23:59:59' and b.highway='" + ddlRoadType.SelectedItem.Value + "' ) t group by t.range";
                }
                else
                {
                    queryIntervalTrafficRoad = "select t.range as Level, count(*) as trafficCount from ( select case when traffic < 1 then '1' when traffic between 1 and 4 then '4' when traffic between 4 and 7 then '7' else '10' end as range from traffic_bonn a inner join mapData b on a.opath_id=b.fid where a.[time] between '" + selectedDay + " 0" + i + ":00:00' and '" + selectedDay + " 0" + (i + 6) + ":00:00' and b.highway='" + ddlRoadType.SelectedItem.Value + "' ) t group by t.range";
                }
                DataSet ds3 = objCda.GetDataSet(queryIntervalTrafficRoad, "SMFM");

                string[] numbers = { "1", "4", "7", "10" };

                var result = numbers.Except(ds3.Tables[0].AsEnumerable().Select(r => r.Field<string>("Level")));

                if (result.Any())
                {
                    foreach (var item in result)
                    {
                        if (item == "7")
                        {
                            levelSevenArrayRoad = levelSevenArrayRoad.Append("0").ToArray();
                        }
                        else if (item == "10")
                        {
                            levelTenArrayRoad = levelTenArrayRoad.Append("0").ToArray();
                        }
                        else if (item == "1")
                        {
                            levelOneArrayRoad = levelOneArrayRoad.Append("0").ToArray();
                        }
                        else if (item == "4")
                        {
                            levelFourArrayRoad = levelFourArrayRoad.Append("0").ToArray();
                        }
                    }
                }

                foreach (DataRow row in ds3.Tables[0].Rows)
                {


                    if (row[0].ToString() == "1")
                    {
                        levelOneArrayRoad = levelOneArrayRoad.Append(row[1].ToString()).ToArray();
                    }
                    else if (row[0].ToString() == "4")
                    {
                        levelFourArrayRoad = levelFourArrayRoad.Append(row[1].ToString()).ToArray();
                    }
                    else if (row[0].ToString() == "7")
                    {
                        levelSevenArrayRoad = levelSevenArrayRoad.Append(row[1].ToString()).ToArray();
                    }
                    else if (row[0].ToString() == "10")
                    {
                        levelTenArrayRoad = levelTenArrayRoad.Append(row[1].ToString()).ToArray();
                    }
                    else
                    {
                        int minLength = int.MaxValue;
                        string[] minArray = null;

                        foreach (string[] array in new[] { levelOneArrayRoad, levelFourArrayRoad, levelSevenArrayRoad, levelTenArrayRoad })
                        {
                            if (array.Length < minLength)
                            {
                                minLength = array.Length;
                                minArray = array;
                            }
                        }
                    }


                }

            }



            float valueRoad = 0;
            for (int i = 0; i < 4; i++)
            {
                valueRoad = Int32.Parse(levelOneArrayRoad[i]) + Int32.Parse(levelFourArrayRoad[i]) + Int32.Parse(levelSevenArrayRoad[i]) + Int32.Parse(levelTenArrayRoad[i]);
                levelOneArrayRoad[i] = ((Int32.Parse(levelOneArrayRoad[i]) / valueRoad) * 100).ToString("0.00");
                levelFourArrayRoad[i] = ((Int32.Parse(levelFourArrayRoad[i]) / valueRoad) * 100).ToString("0.00");
                levelSevenArrayRoad[i] = ((Int32.Parse(levelSevenArrayRoad[i]) / valueRoad) * 100).ToString("0.00");
                levelTenArrayRoad[i] = ((Int32.Parse(levelTenArrayRoad[i]) / valueRoad) * 100).ToString("0.00");
            }

            string levelOneValueRoad = "[";
            levelOneValueRoad += string.Join(", ", levelOneArrayRoad);
            levelOneValueRoad += "]";

            string levelFourValueRoad = "[";
            levelFourValueRoad += string.Join(", ", levelFourArrayRoad);
            levelFourValueRoad += "]";

            string levelSevenValueRoad = "[";
            levelSevenValueRoad += string.Join(", ", levelSevenArrayRoad);
            levelSevenValueRoad += "]";

            string levelTenValueRoad = "[";
            levelTenValueRoad += string.Join(", ", levelTenArrayRoad);
            levelTenValueRoad += "]";
            LevelOneArrayRoadScript.Text = levelOneValueRoad;
            LevelFourArrayRoadScript.Text = levelFourValueRoad;
            LevelSevenArrayRoadScript.Text = levelSevenValueRoad;
            LeveltenArrayRoadScript.Text = levelTenValueRoad;


        }


    }
}