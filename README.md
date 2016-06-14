####Create and load the data into the Table 
1. Below is the query to create table nyctrips and load data into it.

#####Query
  LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2009.csv' INTO TABLE nyctrips
  LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2010.csv' INTO TABLE nyctrips
  LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2011.csv' INTO TABLE nyctrips
  LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2012.csv' INTO TABLE nyctrips
  LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2013.csv' INTO TABLE nyctrips
  LOAD DATA INPATH '/SparkSQLDemo/nyc_taxi_data.csv' INTO TABLE nyctrips
  LOAD DATA INPATH '/SparkSQLDemo/yellow_tripdata_2015-01-06.csv' INTO TABLE nyctrips

2. Execute the commands given below to create and load the data into the table nyctrips using ThriftApplication 
