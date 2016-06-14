##Summarize a billion records using the Syncfusion Dashboard Platform
###Steps to Upload the DataSet to HDFS
	1- Open the command pormpt and locate %HADOOP_HOME%\bin 
	2- Execute the command - hdfs dfs -put <localfolderpath> /SparkSQLDemo/

###Steps to Run Syncfusion Bigdata ThriftApplication
####Creating the TABLE and loading the data into the TABLE
	1- Execute the command by passing it as arrgument to the application 
		Syncfusion.Bigdata.ThriftApplication.exe spark-thriftserverhostname:port "CREATE EXTERNAL table nyctrips(vendor_id string,pickup_datetime timestamp,dropoff_datetime timestamp,passenger_count double,trip_distance  double,pickup_longitude double,pickup_latitude double,rate_code double,store_and_fwd_flag string,dropoff_longitude double,dropoff_latitude double,payment_type  string,fare_amount double,surcharge double,mta_tax double,tip_amount double,tolls_amount double,total_amount double) ROW FORMAT DELIMITED FIELDS TERMINATED BY ','"
		Syncfusion.Bigdata.ThriftApplication.exe spark-thriftserverhostname:port "LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2009.csv' INTO TABLE nyctrips"
		Syncfusion.Bigdata.ThriftApplication.exe spark-thriftserverhostname:port "LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2010.csv' INTO TABLE nyctrips"
		Syncfusion.Bigdata.ThriftApplication.exe spark-thriftserverhostname:port "LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2011.csv' INTO TABLE nyctrips"
		Syncfusion.Bigdata.ThriftApplication.exe spark-thriftserverhostname:port "LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2012.csv' INTO TABLE nyctrips"
		Syncfusion.Bigdata.ThriftApplication.exe spark-thriftserverhostname:port "LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2013.csv' INTO TABLE nyctrips"
		Syncfusion.Bigdata.ThriftApplication.exe spark-thriftserverhostname:port "LOAD DATA INPATH '/SparkSQLDemo/nyc_taxi_data.csv' INTO TABLE nyctrips"
		Syncfusion.Bigdata.ThriftApplication.exe spark-thriftserverhostname:port "LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2015-01-06.csv' INTO TABLE nyctrips"

####Caching the Created table 
	1- Execute the command by passing it as arrgument to the application 
		Syncfusion.Bigdata.ThriftApplication.exe spark-thriftserverhostname:port "cache table nyctrips"


####Creating the Parquet table 
	1- Open the SPARK-SHELL by using the command
		%SAPRK_HOME%\bin > spark-shell --master yarn-client 
	2- After starting the spark-shell pas the command as below
	
		// Load the nyctrips table data into the Data Frame
		scala > var nycTripsData = sqlContext.sql("select * from nyctrips")
		// Save the DataFrame into the Parquet File
		scala > nycTripsData.saveAsParquetFile("/user/nycparquet")

####Create the table "nycparquettable" 
	1- Execute the command by passing it as arrgument to the application 
		Syncfusion.Bigdata.ThriftApplication.exe spark-thriftserverhostname:port "CREATE TABLE nycparquettable USING org.apache.spark.sql.parquet OPTIONS (path '/user/nycparquet')"
  
####Partitioning the Table as per the YEAR 
	1- Open the SPARK-SHELL by using the command
		%SAPRK_HOME%\bin > spark-shell --master yarn-client 
		
	2- After starting the spark-shell pas the command as below
	
	// Load the nyctrips table data into the Data Frame
	scala > var nycTripsData = sqlContext.sql("select * from nyctrips")
	scala > var partitionByYear = nycTripsData.repartition(year($"dropoff_datetime"))
	// Save the partitioned result into the new Table "nyctrips_partitionbyyear"
	scala > partitionByYear.saveAsTable("nyctrips_partitionbyyear")
