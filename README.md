# Traffic-Management-
Software required:


Install python 3.9
https://www.python.org/getit/windows/


Install Visual studio 22
https://visualstudio.microsoft.com/


Microsoft SQL server Management studio 18
https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16


Csv files location:
https://drive.google.com/drive/folders/1e53X6P9gk923xuWUG6b8ez1iR6XZkCF8?fbclid=IwAR3PgLS497FZB7o8PcQxD3fPonzkeolrtpVzSS-hI-cbSS8eiKWFmZPWkQ0


Enviorment setup:

Python script

Step 1
Open Traffic_Bonn.ipynb file

Step 2
Change directory to where you keep the csv files.
"for file in os.listdir(os.getcwd())"

Step 3
Change connection server name to your sql server name
Server="sql server name"

Creating 2nd table in the sql server:

Step 1
Download the "shp_coords_bonn.xlsx" file from the google drive

Step 2
Open sql server management studio.

Step 3
Right click on "Test" database.
Go to Tasks and select Import Data

Step 4
Select Data source to Microsoft Excel and select the "shp_coords_bonn.xlsx" file

Step 5
Select Destination to SQl server Native client 11.0 and database to "Test"
Click next

Step 6
Keep the option copy data from one or more tables or views on. 

Step 7
Finish thew task.

Step 8
Rename the table name from "shp_coords_bonn" to "mapData"



Visual studio

Change the connection string in CDA.cs file to your connection string

Function where the connection string need to be changed

public CDA()
        {
            connTable.Add("SMFM", "Data Source=*server name*;Initial Catalog=Test;Integrated Security=True;");
        }

