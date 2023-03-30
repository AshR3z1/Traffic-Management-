using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Xml;
using System.Web;
namespace Traffic_Bonn
{
    public class CDA
    {
        Hashtable connTable = new Hashtable();
        SqlConnection myConnection = null;
        SqlCommand cmd = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;

        //HttpRequest Request = HttpContext.Current.Request;

        public CDA()
        {
            connTable.Add("SMFM", "Data Source=ASUS\\MSSQLSERVER02;Initial Catalog=Test;Integrated Security=True;");
        }
        public string RawConnectionStr()
        {
            
            const string ConStr = "Data Source=ASUS\\MSSQLSERVER02;Initial Catalog=Test;Integrated Security=True;";

            return ConStr;
        }

        public SqlDataReader getList(string query, string dbName)
        {
            myConnection = new SqlConnection(connTable[dbName].ToString());
            SqlCommand cmd = new SqlCommand(query, myConnection);
            SqlDataReader dr;
            myConnection.Open();
            dr = cmd.ExecuteReader();
            cmd = null;
            return dr;

        }

        public string ExecuteNonQuery(string query, string dbName)
        {

            string rValue;
            myConnection = new SqlConnection(connTable[dbName].ToString());
            try
            {
                using (SqlConnection connection = new SqlConnection(connTable[dbName].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        try
                        {
                            rValue = cmd.ExecuteNonQuery().ToString();
                        }
                        catch (Exception e)
                        {
                            //ErrorLog(e.Message.ToString(), query);
                            rValue = e.Message;
                            connection.Close();
                        }

                        if (rValue != "-1")
                            rValue = rValue;
                        else throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLog(ex.Message.ToString(), query);
                rValue = ex.Message;
            }

            return rValue;



        }

        public object getSingleValue(string query, string dbName)
        {
            //setting = ConfigurationManager.ConnectionStrings[dbName];
            myConnection = new SqlConnection(connTable[dbName].ToString());

            try
            {
                cmd = new SqlCommand(query, myConnection);
                myConnection.Open();
                object retValue = cmd.ExecuteScalar();
                return retValue;
            }
            catch (Exception ex)
            {

                //ErrorLog(ex.Message.ToString(), query);
                return (object)ex.Message.ToString();
            }
            finally
            {
                myConnection.Close();
                cmd = null;
                myConnection = null;
                query = null;
                dbName = null;
            }
        }

        public DataSet GetDataSet(string query, string dbName)
        {
            myConnection = new SqlConnection(connTable[dbName].ToString());

            ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(connTable[dbName].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                        adapter = new SqlDataAdapter();
                        cmd.CommandTimeout = 5000;
                        connection.Open();
                        adapter.SelectCommand = cmd;
                        try
                        {

                            adapter.Fill(ds);

                        }
                        catch (Exception e)
                        {
                            //ErrorLog(e.Message.ToString(), query);
                            ds = null;
                            connection.Close();
                        }
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            ds = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //ErrorLog(e.Message.ToString(), query);
                ds = null;
            }
            return ds;




        }
    }
}