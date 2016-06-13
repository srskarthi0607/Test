package main                                                                                                                                                           

import (
    "fmt"
    "os"
	"math/rand"
	"net"
    "strconv"
	"path/filepath"
	)

func Send(channelSend chan string,j int) {

connection, _ := net.Dial("tcp", "172.16.102.23:2222")
timeinSeconds := int64(1463894875)
 	 switch j{
	 case 1:
       for i := 1; i<=100; i++ {
				//Randomly generates the Cpu usage
				dataPoints :=rand.Intn(100)	
				//Increments the time value by 1 seconds
				timeinSeconds += 1
				 //Convert the Integer value to Stirng
				 timeStamp := strconv.FormatInt(timeinSeconds,10)
				 convertedDatapoints := strconv.Itoa(dataPoints)
				 //Building the Query Which Creates 100s of Metrics and each metrics will having following values
				 text :="put sys.cpu.usage " + timeStamp +" " + convertedDatapoints + " core=4"
				 //Bind the Data points to TSDB server
				 fmt.Fprintf(connection,text + "\n" )
				 //file.WriteString(t+"\t"+convertedDatapoints + "\n")
				 writetoChannel := timeStamp + "\t" +convertedDatapoints
				 //Sending to the channel to wirte the data points
				 channelSend <- writetoChannel		
    }
}	

    
}


func Write(writeFile chan string, j int){

//Switch will create the metrics accordingly 
 switch j {
    case 1:
	//creating relatviePath from the root directory
	Path, _ := filepath.Abs("../GO/Files/cpu_usage.txt")
    file, err := os.Create(Path)
		if err != nil {
			return
		}
	//It drains the channel and writes into the File.
	for message := range writeFile {
		file.WriteString(message + "\n")
	}
	//closing the channel
	close(writeFile)
	
    }

}



func main() {
		dataChannel := make(chan string)
	
	//Starts 1 Go Routine
	for j:=1; j<=1; j++{
	
		go  Send(dataChannel,j)
		go Write(dataChannel,j)
		 
	}
	
	//It will waits for user input,which allow the GO routines to complete its Work.
	fmt.Scanln();
	
}