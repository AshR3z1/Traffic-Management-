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
    public partial class Table : System.Web.UI.Page
    {
        CDA objCda = new CDA();
        private static int tempVaal = 0;
        private static int tempval = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string query = "SELECT COUNT(*) as Incident_ccount FROM traffic_bonn where incidentID > 0";
                string incidentValue = objCda.getSingleValue(query, "SMFM").ToString();

                lblTotalIncident.InnerText = incidentValue;
                string query1 = "SELECT COUNT(*) as Incident_ccount FROM traffic_bonn where traffic > 0";
                string trafficValue = objCda.getSingleValue(query1, "SMFM").ToString();

                lblTotalTraffic.InnerText = trafficValue;
            }

        }



        protected void btnNext_Click(object sender, EventArgs e)
        {



            tempVaal = tempVaal + 1;
            if (tempVaal > 1)
            {
                btnPrevious.Enabled = true;
            }

            string query = "DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT SET @PageNumber=" + tempVaal + " SET @RowsOfPage=100 SELECT * FROM traffic_bonn ORDER BY  (SELECT NULL)  OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY";
            DataSet ds1 = objCda.GetDataSet(query, "SMFM");
            Repeater1.DataSource = ds1;
            Repeater1.DataBind();




        }



        protected void btnIncidentCount_Click(object sender, EventArgs e)
        {
            //string query = "SELECT COUNT(*) as Incident_ccount FROM traffic_bonn where incidentID > 0";
            string query = "SELECT COUNT(*) as Incident_ccount FROM traffic_bonn where incidentID > 0";
            string incidentValue = objCda.getSingleValue(query, "SMFM").ToString();

            lblTotalIncident.InnerText = incidentValue;

        }

        protected void btnDateRange_Click(object sender, EventArgs e)
        {
            btnIncidentNext.Enabled = true;
            btnNext.Enabled = false;
            btnPrevious.Enabled = false;
            if (string.IsNullOrEmpty(fromDatePicker.Text) || string.IsNullOrEmpty(toDatePicker.Text))
            {
                lblTraffic.Text = "Please select both date range";
                lblIncident.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                string query = "SELECT COUNT(*) as Incident_count from traffic_bonn where time between convert(datetime, '" + fromDatePicker.Text + "', 101) and convert(datetime, '" + toDatePicker.Text + "', 101) and incidentID > 0";
                string trafficValue = objCda.getSingleValue(query, "SMFM").ToString();
                tempval = tempval + 1;
                if (tempval > 1)
                {
                    btnPrevious.Enabled = true;
                }

                string query1 = "DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT SET @PageNumber=" + tempval + " SET @RowsOfPage=100 SELECT * from traffic_bonn where time between convert(datetime, '" + fromDatePicker.Text + "', 101) and convert(datetime, '" + toDatePicker.Text + "', 101) and incidentID > 0 ORDER BY  (SELECT NULL)  OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY";
                // string query1 = "SELECT * from traffic_bonn where time between convert(datetime, '" + fromDatePicker.Text + "', 101) and convert(datetime, '" + toDatePicker.Text + "', 101) and incidentID > 0";
                DataSet ds1 = objCda.GetDataSet(query1, "SMFM");
                Repeater1.DataSource = ds1;
                Repeater1.DataBind();
                lblTotalIncident.InnerHtml = "Total Incident happened : " + trafficValue + "";
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {

            tempVaal = tempVaal - 1;
            if (tempVaal == 1)
            {
                btnPrevious.Enabled = false;
            }

            string query = "DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT SET @PageNumber=" + tempVaal + " SET @RowsOfPage=100 SELECT * FROM traffic_bonn ORDER BY (SELECT NULL)  OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY";
            DataSet ds1 = objCda.GetDataSet(query, "SMFM");
            Repeater1.DataSource = ds1;
            Repeater1.DataBind();


        }

        protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
        {

            string query = "SELECT top (100) * FROM traffic_bonn where incidentID > 0 and weather_condition='" + ddlTest.SelectedItem.Value + "'";
            DataSet ds1 = objCda.GetDataSet(query, "SMFM");
            Repeater1.DataSource = ds1;
            Repeater1.DataBind();


        }



        protected void btnIncidentNext_Click(object sender, EventArgs e)
        {
            tempval = tempval + 1;
            if (tempval > 1)
            {
                btnIncidentPrev.Enabled = true;
            }
            string query1 = "DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT SET @PageNumber=" + tempval + " SET @RowsOfPage=100 SELECT * from traffic_bonn where time between convert(datetime, '" + fromDatePicker.Text + "', 101) and convert(datetime, '" + toDatePicker.Text + "', 101) and incidentID > 0 ORDER BY  (SELECT NULL)  OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY";
            // string query1 = "SELECT * from traffic_bonn where time between convert(datetime, '" + fromDatePicker.Text + "', 101) and convert(datetime, '" + toDatePicker.Text + "', 101) and incidentID > 0";
            DataSet ds1 = objCda.GetDataSet(query1, "SMFM");
            Repeater1.DataSource = ds1;
            Repeater1.DataBind();
        }

        protected void btnIncidentPrev_Click(object sender, EventArgs e)
        {
            tempval = tempval - 1;
            if (tempval == 1)
            {
                btnIncidentPrev.Enabled = false;
            }
            string query1 = "DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT SET @PageNumber=" + tempval + " SET @RowsOfPage=100 SELECT * from traffic_bonn where time between convert(datetime, '" + fromDatePicker.Text + "', 101) and convert(datetime, '" + toDatePicker.Text + "', 101) and incidentID > 0 ORDER BY  (SELECT NULL)  OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY";
            // string query1 = "SELECT * from traffic_bonn where time between convert(datetime, '" + fromDatePicker.Text + "', 101) and convert(datetime, '" + toDatePicker.Text + "', 101) and incidentID > 0";
            DataSet ds1 = objCda.GetDataSet(query1, "SMFM");
            Repeater1.DataSource = ds1;
            Repeater1.DataBind();
        }

        protected void btnShowData_Click(object sender, EventArgs e)
        {
            btnNext.Enabled = true;
            btnIncidentNext.Enabled = false;
            btnIncidentPrev.Enabled = false;



            tempVaal = tempVaal + 1;
            if (tempVaal > 1)
            {
                btnPrevious.Enabled = true;
            }

            string query = "DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT SET @PageNumber=" + tempVaal + " SET @RowsOfPage=100 SELECT * FROM traffic_bonn ORDER BY  (SELECT NULL)  OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY";
            DataSet ds1 = objCda.GetDataSet(query, "SMFM");
            Repeater1.DataSource = ds1;
            Repeater1.DataBind();
        }
    }
}