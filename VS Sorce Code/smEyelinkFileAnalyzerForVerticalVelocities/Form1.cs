using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EyelinkFileAnalizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //This function helps the Drag and Drop functionality work
        private void DragAndDropTB_DragDrop(object sender, DragEventArgs e)
        {
            string[] files1 = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file1 in files1)
            {
                DragAndDropTB.Text = file1;
            }
        }
        //This function helps the Drag and Drop functionality work
        private void DragAndDropTB_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        //This helper function takes the 4 list paramitors and finds the ESACC lines in the converted .edf file
        //and uses that data to populate the lists
        private void GetEndSaccadeData(ref List<Saccade> rightEyeUp, ref List<Saccade> rightEyeDown, ref List<Saccade> leftEyeUp, ref List<Saccade> leftEyeDown, int testNumber)
        {
            //find input file directory and create a file stream
            string fileDirectory = DragAndDropTB.Text;
            FileStream inputFile = new FileStream(fileDirectory, FileMode.Open, FileAccess.Read);
            bool startRecording = false;
            if (testNumber == 1) startRecording = true; 
            //Read Line by Line
            using (var streamReader = new StreamReader(inputFile))// helps with line by line
            {
                string line;//helps with line by line
                while ((line = streamReader.ReadLine()) != null)//helps with line by line
                {
                    if ((testNumber == 1) && line.Contains("TEST2")) break;
                    if (testNumber == 2)
                    {
                        if (line.Contains("TEST2")) startRecording = true;
                        if (line.Contains("TEST3")) break;
                    }
                    if (testNumber == 3)
                    {
                        if (line.Contains("TEST3")) startRecording = true;
                    }

                    //will skip recording if bool is false 
                    if (startRecording == false) continue;

                    //Find End Saccade data line
                    // Data format: ESACC <Eye Tracked> <STime> <ETime> <Dur> <Sxpos> <Sypos> <Expos> <Eypos> <Ampl> <PeakVel> 

                    if (line.Contains("ESACC"))
                    {
                        string eyeTracked = "";
                        double startTime = 0;
                        double endTime = 0;
                        double duration = 0;
                        double startXPosition = 0;
                        double startYPosition = 0;
                        double endXPosition = 0;
                        double endYPosition = 0;
                        double amplitude = 0;
                        double peakVelocity = 0;
                        double averageVelocity = 0;
                        string direction = "";

                        string[] splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                        if (splitData.Length < 11)
                        {
                            MessageBox.Show("File does not have velocity data recorded\nForm1.cs:84");
                            break;
                        }
                        if (splitData[1] == "R")
                        {
                            eyeTracked = "Right";
                        }
                        if (splitData[1] == "L")
                        {
                            eyeTracked = "Left";
                        }

                        startTime = Convert.ToDouble(splitData[2]);
                        endTime = Convert.ToDouble(splitData[3]);
                        duration = Convert.ToDouble(splitData[4]);

                        // check for missing data
                        if (splitData[5] == ".") startXPosition = 0;
                        else startXPosition = Convert.ToDouble(splitData[5]);
                        if (splitData[6] == ".") startYPosition = 0;
                        else startYPosition = Convert.ToDouble(splitData[6]);
                        if (splitData[7] == ".") endXPosition = 0;
                        else endXPosition = Convert.ToDouble(splitData[7]);
                        if (splitData[8] == ".") endYPosition = 0;
                        else endYPosition = Convert.ToDouble(splitData[8]);
                        if (splitData[9] == ".") amplitude = 0;
                        else if (Convert.ToDouble(splitData[9]) > 45) amplitude = 0;
                        else amplitude = Convert.ToDouble(splitData[9]);
                        if (splitData[10] == ".") peakVelocity = 0;
                        else peakVelocity = Convert.ToDouble(splitData[10]);

                        // get average Velocity
                        averageVelocity = amplitude / (duration / 1000);

                        if ((startYPosition > 312) && (startYPosition < 712)) direction = "Center To ";
                        if (startYPosition > 712) direction = "Top To ";
                        if (startYPosition < 312) direction = "Bottom To ";
                        if ((endYPosition > 312) && (endYPosition < 712)) direction += "Center";
                        if (endYPosition > 712) direction += "Top";
                        if (endYPosition < 312) direction += "Bottom";

                        //add to appropriate list of Saccade classes
                        if ((eyeTracked == "Right") && (direction == "Center To Top") && (amplitude > 5))
                        {  
                            rightEyeUp.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
                                              startYPosition, endXPosition, endYPosition, amplitude, peakVelocity,
                                                  averageVelocity, direction));
                        }
                        if ((eyeTracked == "Right") && (direction == "Center To Bottom") && (amplitude > 5))
                        {
                            rightEyeDown.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
                                                  startYPosition, endXPosition, endYPosition, amplitude, peakVelocity,
                                                      averageVelocity, direction));
                        }
                        if ((eyeTracked == "Left") && (direction == "Center To Top") && (amplitude > 5))
                        { 
                            leftEyeUp.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
                                               startYPosition, endXPosition, endYPosition, amplitude, peakVelocity,
                                                   averageVelocity, direction));
                        }
                        if ((eyeTracked == "Left") && (direction == "Center To Bottom") && (amplitude > 5))
                        {
                            leftEyeDown.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
                                                startYPosition, endXPosition, endYPosition, amplitude, peakVelocity,
                                                    averageVelocity, direction));
                        }
                    }
                }
            }
            inputFile.Close();
        }
        //Reads all samples for what ever list is passed through listToRead has the start and end times of all saccades
        private void RightSampleReader (ref List<Saccade> listToRead, ref List<List<double>> finalList)
        {
            //find input file directory and create a file stream
            string fileDirectory = DragAndDropTB.Text;
            FileStream inputFile = new FileStream(fileDirectory, FileMode.Open, FileAccess.Read);

            // read file line by line
            using (var streamReader = new StreamReader(inputFile))
            {
                for (int i = 0; i < listToRead.Count; i++)
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        //This line seperates all data from a line with differing levels of whitespace to populate the split data array
                        string[] splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        //Weed out lines that are not samples to find ones that begin saccades 
                        if ((splitData.Length < 11) || (splitData[0] != Convert.ToString(listToRead[i].getStartTime()))) continue;
                        else
                        {
                            List<double> sublist = new List<double>();//must create sublist to properly add to a list of lists

                            bool keepGoing = true;
                            while (keepGoing)
                            {
                                int num = 0;
                                // check if data is missing or if the first part of data is not a nubmer
                                if ((splitData.Length < 11) || (splitData[10] == ".") || (!(int.TryParse(splitData[0], out num)))) 
                                {
                                    line = streamReader.ReadLine();
                                    splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                                    continue;
                                }
                                //keep going till we reach the endtime of saccade
                                if (Convert.ToInt32(splitData[0]) < Convert.ToInt32(listToRead[i].getEndTime()))
                                {
                                    sublist.Add(Convert.ToDouble(splitData[10]));
                                    line = streamReader.ReadLine();
                                    splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                                    continue;
                                }
                                else
                                {
                                    sublist.Add(Convert.ToDouble(splitData[10]));
                                    keepGoing = false;
                                    break;
                                }
                            }
                            finalList.Add(sublist);
                            break;
                        }
                    }
                }
            }
            inputFile.Close();
        }
        private void LeftSampleReader(ref List<Saccade> listToRead, ref List<List<double>> finalList)
        {
            //find input file directory and create a file stream
            string fileDirectory = DragAndDropTB.Text;
            FileStream inputFile = new FileStream(fileDirectory, FileMode.Open, FileAccess.Read);

            // read file line by line
            using (var streamReader = new StreamReader(inputFile))
            {
                for (int i = 0; i < listToRead.Count; i++)
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        //This line seperates all data from a line with differing levels of whitespace to populate the split data array
                        string[] splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        //Weed out lines that are not samples to find ones that begin saccades 
                        if ((splitData.Length < 11) || (splitData[0] != Convert.ToString(listToRead[i].getStartTime()))) continue;
                        else
                        {
                            List<double> sublist = new List<double>();//must create sublist to properly add to a list of lists

                            bool keepGoing = true;
                            while (keepGoing)
                            {
                                int num = 0;
                                // check if data is missing or if the first part of data is not a nubmer
                                if ((splitData.Length < 11) || (splitData[8] == ".") || (!(int.TryParse(splitData[0], out num))))
                                {
                                    line = streamReader.ReadLine();
                                    splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                                    continue;
                                }
                                //keep going till we reach the endtime of saccade
                                if (Convert.ToInt32(splitData[0]) < Convert.ToInt32(listToRead[i].getEndTime()))
                                {
                                    sublist.Add(Convert.ToDouble(splitData[8]));
                                    line = streamReader.ReadLine();
                                    splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                                    continue;
                                }
                                else
                                {
                                    sublist.Add(Convert.ToDouble(splitData[8]));
                                    keepGoing = false;
                                    break;
                                }
                            }
                            finalList.Add(sublist);
                            break;
                        }
                    }
                }
            }
            inputFile.Close();
        }
        //Calls the sample reader on all four eye movements
        private void GetSampleData(ref List<Saccade> rightEyeUp, ref List<Saccade> rightEyeDown, ref List<Saccade> leftEyeUp, ref List<Saccade> leftEyeDown, ref List<List<double>> rightEyeUpSamples, ref List<List<double>> rightEyeDownSamples, ref List<List<double>> leftEyeUpSamples, ref List<List<double>> leftEyeDownSamples)
        {
            RightSampleReader(ref rightEyeUp, ref rightEyeUpSamples);
            RightSampleReader(ref rightEyeDown, ref rightEyeDownSamples);
            LeftSampleReader(ref leftEyeUp, ref leftEyeUpSamples);
            LeftSampleReader(ref leftEyeDown, ref leftEyeDownSamples);   
        }
        //Creates a PDF report file in the same directory as input file. The report contians data and graphs
        private double FindPeakValue(List<double> averageList)
        {
            //change all numbers to positive
            for (int i = 0; i < averageList.Count; i++)
            {
                if (averageList[i] < 0) averageList[i] *= -1;
            }
            double maxValue = averageList[0];
            for (int i = 0; i < averageList.Count; i++)
            {
                if (maxValue < averageList[i]) maxValue = averageList[i];
            }
            return maxValue;
        }
        private double CalculateStandardDiviation(List<double> list)
        {
            double solution = 0;
            double mean = 0;
            foreach (double item in list)
            {
                mean += item;
            }
            mean /= list.Count;
            List<double> listOfResults = new List<double>();
            foreach (double item in list)
            {
                listOfResults.Add(Math.Pow((item - mean), 2));
            }
            foreach (double item in listOfResults)
            {
                solution += item;
            }
            solution /= listOfResults.Count;
            solution = Math.Sqrt(solution);
            return Math.Round(solution, 2);
        }
        private double FindPeakVelocityStandardDivation(List<List<double>> listOfLists)
        {
            List<double> listOfPeaks = new List<double>();
            foreach (List<double> list in listOfLists)
            {
                listOfPeaks.Add(FindPeakValue(list));
            }
            return CalculateStandardDiviation(listOfPeaks);
        }
        private KeyValuePair<int,double> GetPeakAccelerationPoint(List<double> averageList)
        {
            List<double> accelerationList = new List<double>();
            double pointToAdd = 0;
            for (int i = 0; i < (averageList.Count - 1); i++)
            {
                double vel1 = averageList[i];
                double vel2 = averageList[i + 1];
                pointToAdd = ((vel2 - vel1) / (.002));
                accelerationList.Add(pointToAdd);
            }
            double peakAcceleration = accelerationList[0];
            int peakAccelerationIndex = 0;
            for (int i = 0; i < accelerationList.Count; i++)
            {
                if (peakAcceleration < accelerationList[i])
                {
                    peakAcceleration = accelerationList[i];
                    peakAccelerationIndex = (i * 2) + 1; // acceleration value is not same as list value
                }
            }
            KeyValuePair<int, double> peakPoint = new KeyValuePair<int, double>(peakAccelerationIndex, peakAcceleration);
            return peakPoint;
        }
        private void ChartGraph(ref List<List<double>> listOfData, string textOfBox)
        {
            //clear graph
            int originalCount = chart1.Series.Count;
            for (int i = 0; i < originalCount; i++)
            {
                chart1.Series.RemoveAt(0);
            }

            chart1.Titles[0].Text = textOfBox;

            List<double> averageList = new List<double>();
            if (textOfBox.Contains("Right Eye") && textOfBox.Contains("Sursumduction"))
            {
                averageList = CalculateAverageSet(ref listOfData);
                if (textOfBox.Contains("Acceleration"))
                {
                    ChartAcceleration(averageList);
                }
                else if (textOfBox.Contains("Average"))
                {
                    ChartAverage(averageList);
                }
                else
                {
                    ChartAverage(averageList);
                    ChartAllSets(listOfData);
                }
            }
            if (textOfBox.Contains("Right Eye") && textOfBox.Contains("Infraduction"))
            {
                averageList = CalculateAverageSet(ref listOfData);
                if (textOfBox.Contains("Acceleration"))
                {
                    ChartAcceleration(averageList);
                }
                else if (textOfBox.Contains("Average"))
                {
                    ChartAverage(averageList);
                }
                else
                {
                    ChartAverage(averageList);
                    ChartAllSets(listOfData);
                }
            }
            if (textOfBox.Contains("Left Eye") && textOfBox.Contains("Sursumduction"))
            {
                averageList = CalculateAverageSet(ref listOfData);
                if (textOfBox.Contains("Acceleration"))
                {
                    ChartAcceleration(averageList);
                }
                else if (textOfBox.Contains("Average"))
                {
                    ChartAverage(averageList);
                }
                else
                {
                    ChartAverage(averageList);
                    ChartAllSets(listOfData);
                }
            }
            if (textOfBox.Contains("Left Eye") && textOfBox.Contains("Infraduction"))
            {
                averageList = CalculateAverageSet(ref listOfData);
                if (textOfBox.Contains("Acceleration"))
                {
                    ChartAcceleration(averageList);
                }
                else if (textOfBox.Contains("Average"))
                {
                    ChartAverage(averageList);
                }
                else
                {
                    ChartAverage(averageList);
                    ChartAllSets(listOfData);
                }
            }
        }
        private void StartButton_Click(object sender, EventArgs e)
        {   
            try
            {
                string fileDirectory = DragAndDropTB.Text;
                string fileType = fileDirectory.Substring(fileDirectory.LastIndexOf('.'));

                //Checks for the correct file type
                if ((fileType != ".asc") && (fileType != ".txt"))
                {
                    MessageBox.Show("Please Enter Correct File Type\n(.edf converted to .asc)\nForm1.cs:439");
                    DragAndDropTB.Text = string.Empty;
                    return;
                }
                string sameFolder = fileDirectory.Substring(0, fileDirectory.LastIndexOf('.'));
                progressBar1.Value = 5;

                string pdfPath = sameFolder + "Report.pdf";
                string imagePath = sameFolder + "Pic.gif";//create a .gif file used to make graphs
                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
                doc.Open();

                //FOR LOOP FOR 3 TESTS
                for (int test = 1; test < 4; test++)
                {
                    List<Saccade> rightEyeUp = new List<Saccade>();
                    List<Saccade> rightEyeDown = new List<Saccade>();
                    List<Saccade> leftEyeUp = new List<Saccade>();
                    List<Saccade> leftEyeDown = new List<Saccade>();

                    bool rightEyeUpEmpty = false;
                    bool rightEyeDownEmpty = false;
                    bool leftEyeUpEmpty = false;
                    bool leftEyeDownEmpty = false;

                    GetEndSaccadeData(ref rightEyeUp, ref rightEyeDown, ref leftEyeUp, ref leftEyeDown, test);
                    //Check if data is valid
                    if (rightEyeUp.Count == 0)
                    {
                        MessageBox.Show("Warning:\nNo Right Eye Up Data Detected\nForm1.cs:465");
                        rightEyeUpEmpty = true;
                    }
                    if (rightEyeDown.Count == 0)
                    {
                        MessageBox.Show("Warning:\nNo Right Eye Down Data Detected\nForm1.cs:466");
                        rightEyeDownEmpty = true;
                    }
                    if (leftEyeUp.Count == 0)
                    {
                        MessageBox.Show("Warning:\nNo Left Eye Up Data Detected\nForm1.cs:467");
                        leftEyeUpEmpty = true;
                    }
                    if (leftEyeDown.Count == 0)
                    {
                        MessageBox.Show("Warning:\nNo Left Eye Down Data Detected\nForm1.cs:468");
                        leftEyeDownEmpty = true;
                    }
                    //progressBar1.Value = 0;
                    //DragAndDropTB.Text = string.Empty;
                    //return;
                    
                    //get sample data 
                    List<List<double>> rightEyeUpSamples = new List<List<double>>();
                    List<List<double>> rightEyeDownSamples = new List<List<double>>();
                    List<List<double>> leftEyeUpSamples = new List<List<double>>();
                    List<List<double>> leftEyeDownSamples = new List<List<double>>();

                    if (!rightEyeUpEmpty) RightSampleReader(ref rightEyeUp, ref rightEyeUpSamples);
                    if (!rightEyeDownEmpty) RightSampleReader(ref rightEyeDown, ref rightEyeDownSamples);
                    if (!leftEyeUpEmpty) LeftSampleReader(ref leftEyeUp, ref leftEyeUpSamples);
                    if (!leftEyeDownEmpty) LeftSampleReader(ref leftEyeDown, ref leftEyeDownSamples);
                    
                    // GetSampleData(ref rightEyeUp, ref rightEyeDown, ref leftEyeUp, ref leftEyeDown, ref rightEyeUpSamples, ref rightEyeDownSamples, ref leftEyeUpSamples, ref leftEyeDownSamples);
                    //Declaration of averageList
                    List<double> averageList = new List<double>();
                    try
                    {
                        switch(test)
                        {
                            case 1:
                                doc.Add(new Paragraph("TEST 1: HEAD STRAIGHT \n\n"));
                                break;
                            case 2:
                                doc.Add(new Paragraph("TEST 2: HEAD RIGHT TURN \n\n"));
                                break;
                            case 3:
                                doc.Add(new Paragraph("TEST 3: HEAD LEFT TURN \n\n"));
                                break;
                        }
                        //RIGHT EYE UP
                        doc.Add(new Paragraph("Right Eye Sursumduction: \n"));
                        if (!rightEyeUpEmpty)
                        {
                            double rightEyeAdbTotal = 0;
                            for (int i = 0; i < rightEyeUp.Count; i++)
                            {
                                rightEyeAdbTotal += rightEyeUp[i].getAverageVelocity();
                            }
                            rightEyeAdbTotal /= rightEyeUp.Count;

                            //calculate peak velocity based on graph data 
                            if (removeOutliersBT.Checked == true) RemoveOutliers(ref rightEyeUpSamples);
                            averageList = CalculateAverageSet(ref rightEyeUpSamples);
                            double peakValue = FindPeakValue(averageList);
                            //Output variables
                            doc.Add(new Paragraph("Average Velocity: " + Math.Round(rightEyeAdbTotal, 2) + " degrees/second\n"));
                            doc.Add(new Paragraph("Peak Velocity of Average Curve: " + Math.Round(peakValue, 2) + " degrees/second\n"));
                            //Calculate Standard Divation
                            double standardDeviation = FindPeakVelocityStandardDivation(rightEyeUpSamples);
                            doc.Add(new Paragraph("Peak Velocity Standard Deviation: " + Math.Round(standardDeviation, 2)));

                            //get sets graph
                            //textOfBox.Text = "Right Eye Sursumductions";

                            ChartGraph(ref rightEyeUpSamples, "Right Eye Sursumductions");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            Image gif = Image.GetInstance(imagePath);
                            doc.Add(gif);
                            //get average graph
                            //textOfBox.Text = "Right Eye Average Sursumduction";
                            ChartGraph(ref rightEyeUpSamples, "Right Eye Average Sursumduction");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            gif = Image.GetInstance(imagePath);
                            doc.Add(gif);

                            doc.NewPage();
                            //Print Acceleration Graph
                            KeyValuePair<int, double> peakAccelerationDataPoint = new KeyValuePair<int, double>();
                            peakAccelerationDataPoint = GetPeakAccelerationPoint(averageList);
                            doc.Add(new Paragraph("Peak Acceleration: " + Math.Round(peakAccelerationDataPoint.Value, 2) + " degrees/second^2\n"));
                            doc.Add(new Paragraph("Time At Peak Acceleration: " + peakAccelerationDataPoint.Key + " milliseconds\n"));

                            //get accelration graph
                            //comboBox1.Text = "Right Eye Average Acceleration Sursumduction";
                            ChartGraph(ref rightEyeUpSamples, "Right Eye Average Acceleration Sursumduction");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            gif = Image.GetInstance(imagePath);
                            doc.Add(gif);
                        }
                        else
                        {
                            doc.Add(new Paragraph("Error: No Right Eye Up Data Found"));
                        }
                        doc.NewPage();
                
                        //RIGHT EYE DOWN
                        doc.Add(new Paragraph("Right Eye Infraduction:"));
                        if (!rightEyeDownEmpty)
                        {
                            double averageTotal = 0;
                            for (int i = 0; i < rightEyeDown.Count; i++)
                            {
                                averageTotal += rightEyeDown[i].getAverageVelocity();
                            }
                            averageTotal /= rightEyeDown.Count;

                            if (removeOutliersBT.Checked == true) RemoveOutliers(ref rightEyeDownSamples);
                            averageList = CalculateAverageSet(ref rightEyeDownSamples);

                            double peakValue = FindPeakValue(averageList);

                            doc.Add(new Paragraph("Average Velocity: " + Math.Round(averageTotal, 2) + " degrees/second\n"));
                            doc.Add(new Paragraph("Peak Velocity of Average Curve: " + Math.Round(peakValue, 2) + " degrees/second\n"));
                            //Calculate Standard Divation
                            double standardDeviation = FindPeakVelocityStandardDivation(rightEyeDownSamples);
                            doc.Add(new Paragraph("Peak Velocity Standard Deviation: " + Math.Round(standardDeviation, 2)));

                            //get sets graph
                            //comboBox1.Text = "Right Eye Infraductions";
                            ChartGraph(ref rightEyeDownSamples, "Right Eye Infraductions");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            Image gif = Image.GetInstance(imagePath);
                            doc.Add(gif);
                            //get average graph
                            //comboBox1.Text = "Right Eye Average Infraduction";
                            ChartGraph(ref rightEyeDownSamples, "Right Eye Average Infraduction");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            gif = Image.GetInstance(imagePath);
                            doc.Add(gif);

                            doc.NewPage();
                            //Print Acceleration Graph
                            KeyValuePair<int, double> peakAccelerationDataPoint = new KeyValuePair<int, double>();
                            peakAccelerationDataPoint = GetPeakAccelerationPoint(averageList);
                            doc.Add(new Paragraph("Peak Acceleration: " + Math.Round(peakAccelerationDataPoint.Value, 2) + " degrees/second^2\n"));
                            doc.Add(new Paragraph("Time At Peak Acceleration: " + peakAccelerationDataPoint.Key + " milliseconds\n"));

                            //get accelration graph
                            //comboBox1.Text = "Right Eye Average Acceleration Infraduction";
                            ChartGraph(ref rightEyeDownSamples, "Right Eye Average Acceleration Infraduction");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            gif = Image.GetInstance(imagePath);
                            doc.Add(gif);
                        }
                        else
                        {
                            doc.Add(new Paragraph("Error: No Right Eye Down Data Found"));
                        }
                        doc.NewPage();

                        //LEFT EYE UP
                        doc.Add(new Paragraph("Left Eye Sursumduction:"));
                        if (!leftEyeUpEmpty)
                        {
                            double averageTotal = 0;
                            for (int i = 0; i < leftEyeUp.Count; i++)
                            {
                                averageTotal += leftEyeUp[i].getAverageVelocity();
                            }
                            averageTotal /= leftEyeUp.Count;

                            if (removeOutliersBT.Checked == true) RemoveOutliers(ref leftEyeUpSamples);
                            averageList = CalculateAverageSet(ref leftEyeUpSamples);

                            double peakValue = FindPeakValue(averageList);
                            doc.Add(new Paragraph("Average Velocity: " + Math.Round(averageTotal, 2) + " degrees/second\n"));
                            doc.Add(new Paragraph("Peak Velocity of Average Curve: " + Math.Round(peakValue, 2) + " degrees/second\n"));
                            //Calculate Standard Divation
                            double standardDeviation = FindPeakVelocityStandardDivation(leftEyeUpSamples);
                            doc.Add(new Paragraph("Peak Velocity Standard Deviation: " + Math.Round(standardDeviation, 2)));
                            //get sets graph
                            //comboBox1.Text = "Left Eye Sursumductions";
                            ChartGraph(ref leftEyeUpSamples, "Left Eye Sursumductions");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            Image gif = Image.GetInstance(imagePath);
                            doc.Add(gif);
                            //get average graph
                            //comboBox1.Text = "Left Eye Average Sursumduction";
                            ChartGraph(ref leftEyeUpSamples, "Left Eye Average Sursumduction");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            gif = Image.GetInstance(imagePath);
                            doc.Add(gif);
                        
                            doc.NewPage();
                            //Print Acceleration Graph
                            KeyValuePair<int, double> peakAccelerationDataPoint = new KeyValuePair<int, double>();
                            peakAccelerationDataPoint = GetPeakAccelerationPoint(averageList);
                            doc.Add(new Paragraph("Peak Acceleration: " + Math.Round(peakAccelerationDataPoint.Value, 2) + " degrees/second^2\n"));
                            doc.Add(new Paragraph("Time At Peak Acceleration: " + peakAccelerationDataPoint.Key + " milliseconds\n"));

                            //get accelration graph
                            //comboBox1.Text = "Left Eye Average Acceleration Sursumduction";
                            ChartGraph(ref leftEyeUpSamples, "Left Eye Average Acceleration Sursumduction");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            gif = Image.GetInstance(imagePath);
                            doc.Add(gif);
                        }
                        else
                        {
                            doc.Add(new Paragraph("Error: No Left Eye Up Data Found"));
                        }
                        doc.NewPage();

                        //LEFT EYE DOWN
                        doc.Add(new Paragraph("Left Eye Infraduction:"));
                        if (!leftEyeDownEmpty)
                        {
                            double averageTotal = 0;
                            for (int i = 0; i < leftEyeDown.Count; i++)
                            {
                                averageTotal += leftEyeDown[i].getAverageVelocity();
                            }
                            averageTotal /= leftEyeDown.Count;

                            if (removeOutliersBT.Checked == true) RemoveOutliers(ref leftEyeDownSamples);
                            averageList = CalculateAverageSet(ref leftEyeDownSamples);
                        
                            double peakValue = FindPeakValue(averageList);
                            doc.Add(new Paragraph("Average Velocity: " + Math.Round(averageTotal, 2) + " degrees/second\n"));
                            doc.Add(new Paragraph("Peak Velocity of Average Curve: " + Math.Round(peakValue, 2) + " degrees/second\n"));
                            //Calculate Standard Divation
                            double standardDeviation = FindPeakVelocityStandardDivation(leftEyeDownSamples);
                            doc.Add(new Paragraph("Peak Velocity Standard Deviation: " + Math.Round(standardDeviation, 2)));

                            //get sets graph
                            //comboBox1.Text = "Left Eye Infraductions";
                            ChartGraph(ref leftEyeDownSamples, "Left Eye Infraductions");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            Image gif = Image.GetInstance(imagePath);
                            doc.Add(gif);
                        
                            //get average graph
                            //comboBox1.Text = "Left Eye Average Infraduction";
                            ChartGraph(ref leftEyeDownSamples, "Left Eye Average Infraduction");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            gif = Image.GetInstance(imagePath);
                            doc.Add(gif);

                        
                            doc.NewPage();
                            //Print Acceleration Graph
                            KeyValuePair<int, double> peakAccelerationDataPoint = new KeyValuePair<int, double>();
                            peakAccelerationDataPoint = GetPeakAccelerationPoint(averageList);
                            doc.Add(new Paragraph("Peak Acceleration: " + Math.Round(peakAccelerationDataPoint.Value, 2) + " degrees/second^2\n"));
                            doc.Add(new Paragraph("Time At Peak Acceleration: " + peakAccelerationDataPoint.Key + " milliseconds\n"));

                            //get accelration graph
                            //textOfBox.Text = "Left Eye Average Acceleration Infraduction";
                            ChartGraph(ref leftEyeDownSamples, "Left Eye Average Acceleration Infraduction");//NEW
                            chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                            gif = Image.GetInstance(imagePath);
                            doc.Add(gif);
                        }
                        else
                        {
                            doc.Add(new Paragraph("Error: No Left Eye Down Data Found"));
                        }
                        doc.NewPage();
                        
                        //delete image file used to crate pdf
                        File.Delete(imagePath);
                        progressBar1.Value += 20;
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("The report .pdf file is already open. \n Close it and try again\nForm1.cs:697");
                        progressBar1.Value = 0;
                        return;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Non IOEXCeption Error\n" + exc + "\nForm1.cs:703");
                        progressBar1.Value = 0;
                        return;
                    }
                }
                progressBar1.Value = 0;
                doc.Close();
                DragAndDropTB.Text = string.Empty;
            }
            //Exception Handelining 
            catch (FileNotFoundException exseption)
            {
                MessageBox.Show("Error: File Not Found\nForm1.cs:715");
                DragAndDropTB.Text = string.Empty;
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error: Please Enter A Valid File\nForm1.cs:721");
                return;
            }
            catch (UnauthorizedAccessException exseption)
            {
                MessageBox.Show("Error: No Permission To Access The File\nForm1.cs:726");
                DragAndDropTB.Text = string.Empty;
                return;
            }
        }
        // gives the raw velocity over time data in a text file 
        private void AcclerationDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                string fileDirectory = DragAndDropTB.Text;
                string fileType = fileDirectory.Substring(fileDirectory.LastIndexOf('.'));
               
                //Check file type
                if ((fileType != ".asc") && (fileType != ".txt"))
                {
                    MessageBox.Show("Please Enter Correct File Type\n(.edf converted to .asc)\nForm1.cs:742");
                    DragAndDropTB.Text = string.Empty;
                    return;
                }
                
                string sameFolder = fileDirectory.Substring(0, fileDirectory.LastIndexOf('.'));
                FileStream outputFile = new FileStream(sameFolder + "RawData.txt", FileMode.Create, FileAccess.Write);

                for (int test = 1; test < 4; test++)
                {
                    string firstString = "";
                    switch (test)
                    {
                        case 1:
                            firstString = "TEST 1: HEAD STRAIGHT";
                            break;
                        case 2:
                            firstString = "TEST 2: HEAD RIGHT TURN";
                            break;
                        case 3:
                            firstString = "TEST 3: HEAD LEFT TURN";
                            break;
                    }

                    string outputString = firstString; //Cration of outputString
                    byte[] bytes = Encoding.ASCII.GetBytes(outputString);// Cration of bytes
                    byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);// Creation of newline


                    outputFile.Write(bytes, 0, outputString.Length);
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Flush();

                    List<Saccade> rightEyeUp = new List<Saccade>();
                    List<Saccade> rightEyeDown = new List<Saccade>();
                    List<Saccade> leftEyeUp = new List<Saccade>();
                    List<Saccade> leftEyeDown = new List<Saccade>();

                    GetEndSaccadeData(ref rightEyeUp, ref rightEyeDown, ref leftEyeUp, ref leftEyeDown, test);

                    if ((rightEyeUp.Count == 0) || (rightEyeDown.Count == 0) || (leftEyeUp.Count == 0) || (leftEyeDown.Count == 0))
                    {
                        if (rightEyeUp.Count == 0) MessageBox.Show("File Cannot Be Analyzed\nNo Right Eye Up Data Detected\nForm1.cs:785");
                        else if (rightEyeDown.Count == 0) MessageBox.Show("File Cannot Be Analyzed\nNo Right Eye Down Data Detected\nForm1.cs:786");
                        else if (leftEyeUp.Count == 0) MessageBox.Show("File Cannot Be Analyzed\nNo Left Eye Up Data Detected\nForm1.cs:787");
                        else MessageBox.Show("File Cannot be Analyzed\nNo Left Eye Down Data Detected\nForm1.cs:788");
                        progressBar1.Value = 0;
                        outputFile.Close();
                        File.Delete(sameFolder + "RawData.txt");
                        DragAndDropTB.Text = string.Empty;
                        return;
                    }

                    List<List<double>> rightEyeUpSamples = new List<List<double>>();
                    List<List<double>> rightEyeDownSamples = new List<List<double>>();
                    List<List<double>> leftEyeUpSamples = new List<List<double>>();
                    List<List<double>> leftEyeDownSamples = new List<List<double>>();

                    GetSampleData(ref rightEyeUp, ref rightEyeDown, ref leftEyeUp, ref leftEyeDown, ref rightEyeUpSamples, ref rightEyeDownSamples, ref leftEyeUpSamples, ref leftEyeDownSamples);
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref rightEyeUpSamples);
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref rightEyeDownSamples);
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref leftEyeUpSamples);
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref leftEyeDownSamples);

                    //Output formating
                    outputString = "Right Eye Sursumductions Vertical Velocity Over Time: "; //Cration of outputString
                    bytes = Encoding.ASCII.GetBytes(outputString);// Cration of bytes
                    newline = Encoding.ASCII.GetBytes(Environment.NewLine);// Creation of newline

                    
                    outputFile.Write(bytes, 0, outputString.Length);
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Flush();
                    
                    for (int i = 0; i < rightEyeUpSamples.Count; i++)
                    {
                        outputString = "Set " + (i + 1);
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputString = "Time (ms) , Vel (deg/sec)";
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                        for (int j = 0; j < rightEyeUpSamples[i].Count; j++)
                        {
                            outputString = (j * 2) + " , " + Convert.ToString(rightEyeUpSamples[i][j]);
                            bytes = Encoding.ASCII.GetBytes(outputString);
                            outputFile.Write(bytes, 0, outputString.Length);
                            outputFile.Write(newline, 0, newline.Length);
                            outputFile.Flush();
                        }
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                    }

                    outputString = "Right Eye Infraductions Vertical Velocity Over Time: "; //change
                    bytes = Encoding.ASCII.GetBytes(outputString);
                    outputFile.Write(bytes, 0, outputString.Length);
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Flush();

                    for (int i = 0; i < rightEyeDownSamples.Count; i++)//chagne
                    {
                        outputString = "Set " + (i + 1);
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputString = "Time (ms) , Vel (deg/sec)";
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                        for (int j = 0; j < rightEyeDownSamples[i].Count; j++)//change
                        {

                            outputString = (j * 2) + " , " + Convert.ToString(rightEyeDownSamples[i][j]);//change
                            bytes = Encoding.ASCII.GetBytes(outputString);
                            outputFile.Write(bytes, 0, outputString.Length);
                            outputFile.Write(newline, 0, newline.Length);
                            outputFile.Flush();
                        }
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                    }

                    outputString = "Left Eye Sursumductions Vertical Velocity Over Time: "; //change
                    bytes = Encoding.ASCII.GetBytes(outputString);
                    outputFile.Write(bytes, 0, outputString.Length);
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Flush();

                    for (int i = 0; i < leftEyeUpSamples.Count; i++)//chagne
                    {
                        outputString = "Set " + (i + 1);
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputString = "Time (ms) , Vel (deg/sec)";
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                        for (int j = 0; j < leftEyeUpSamples[i].Count; j++)//change
                        {

                            outputString = (j * 2) + " , " + Convert.ToString(leftEyeUpSamples[i][j]);//change
                            bytes = Encoding.ASCII.GetBytes(outputString);
                            outputFile.Write(bytes, 0, outputString.Length);
                            outputFile.Write(newline, 0, newline.Length);
                            outputFile.Flush();
                        }
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                    }
                    
                    outputString = "Left Eye Infraductions Vertical Velocity Over Time: "; //change
                    bytes = Encoding.ASCII.GetBytes(outputString);
                    outputFile.Write(bytes, 0, outputString.Length);
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Flush();
                    
                    
                    for (int i = 0; i < leftEyeDownSamples.Count; i++)//chagne
                    {
                        outputString = "Set " + (i + 1);
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputString = "Time (ms) , Vel (deg/sec)";
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                        for (int j = 0; j < leftEyeDownSamples[i].Count; j++)//change
                        {

                            outputString = (j * 2) + " , " + Convert.ToString(leftEyeDownSamples[i][j]);//change
                            bytes = Encoding.ASCII.GetBytes(outputString);
                            outputFile.Write(bytes, 0, outputString.Length);
                            outputFile.Write(newline, 0, newline.Length);
                            outputFile.Flush();
                        }
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                    }
                }
                //Close files and clear message
                outputFile.Close();
                DragAndDropTB.Text = string.Empty;
            }
            catch (FileNotFoundException exseption)
            {
                MessageBox.Show("Error: File Not Found\nForm1.cs:941");
                DragAndDropTB.Text = string.Empty;
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error: Please Enter A Valid File\nForm1.cs:947");
                return;
            }
            catch (UnauthorizedAccessException exseption)
            {
                MessageBox.Show("Error: No Permission To Access The File\nForm1.cs:952");
                DragAndDropTB.Text = string.Empty;
                return;
            }
        }
        //finds smallest set of data to not have out of bounds error
        private double FindMinSetTime(List<List<double>> listToCheck)
        {
            double minVal = listToCheck[0].Count;
            for (int i = 0; i < listToCheck.Count; i++)
            {
                if (minVal > listToCheck[i].Count) minVal = listToCheck[i].Count;
            }
            return minVal;
        }
        //remeoves werid data and the minimum timed value
        private void RemoveOutliers(ref List<List<Double>> listToCheck)
        {
            for (int i = 0; i < listToCheck.Count; i++)
            {
                if (listToCheck[i].Count < 2)
                {
                    listToCheck.RemoveAt(i);
                    i--;
                }
                double lastPoint = listToCheck[i][(listToCheck[i].Count - 1)];
                double secondToLast = listToCheck[i][(listToCheck[i].Count - 2)];
                double diffrence = secondToLast - lastPoint;
                //make positive
                if (diffrence < 0) diffrence *= -1;
                if (diffrence > 100)
                {
                    listToCheck.RemoveAt(i);
                    i--;
                }
            }
            //Remove Min set 
            int minIndex = 0;
            int minVal = listToCheck[0].Count;
            for (int i = 0; i < listToCheck.Count; i++)
            {
                if (minVal > listToCheck[i].Count)
                {
                    minVal = listToCheck[i].Count;
                    minIndex = i;
                }
            }
            listToCheck.RemoveAt(minIndex);
            //Remove Max set
            /*int maxIndex = 0;
            int maxVal = listToCheck[0].Count;
            for (int i = 0; i < listToCheck.Count; i++)
            {
                if (maxVal < listToCheck[i].Count)
                {
                    maxVal = listToCheck[i].Count;
                    maxIndex = i;
                }
            }
            listToCheck.RemoveAt(maxIndex);*/
        }
        //calculates average of sets and returns it as a new list
        private List<double> CalculateAverageSet(ref List<List<double>> listOfSets)
        {
            List<double> averageList = new List<double>();
            double totalaverage = 0;
            double firstMinTime = FindMinSetTime(listOfSets);
            for (int j = 0; j < firstMinTime ; j++)
            {
                totalaverage = 0;
                for (int i = 0; i < listOfSets.Count; i++)
                {
                    totalaverage += listOfSets[i][j];
                }
                totalaverage /= listOfSets.Count;
                averageList.Add(totalaverage);
            }
            return averageList;
        }
        //charts average on graph
        private void ChartAverage(List<double> averageList)
        {
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Time (ms)";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Velocity (deg/sec) ";
            chart1.Series.Add("Average");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            for (int i = 0; i < averageList.Count; i++)
            { 
                this.chart1.Series["Average"].Points.AddXY((i * 2), averageList[i]);
            }
        }
        //graphs all sets given 
        private void ChartAllSets(List<List<double>> listsToChart)
        {
            for (int i = 0; i < listsToChart.Count; i++)
            {
                chart1.Series.Add("Set" + (i + 1));
                chart1.Series["Set" + (i + 1)].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                for (int j = 0; j < listsToChart[i].Count; j++)
                {
                    this.chart1.Series["Set" + (i + 1)].Points.AddXY((j * 2), listsToChart[i][j]);
                }
            }
        }
        //graphs accleration of input list
        private void ChartAcceleration(List<double> averageList)
        {
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Acceleration (deg/sec^2)";
            chart1.Series.Add("Acceleration");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            List<double> accelerationList = new List<double>();
            double pointToAdd = 0;
            for (int i = 0; i < (averageList.Count - 1); i++)
            {
                double vel1 = averageList[i];
                double vel2 = averageList[i + 1];
                pointToAdd = ((vel2 - vel1) / (.002));
                accelerationList.Add(pointToAdd);
            } 
            chart1.Series[0].Points.AddXY(0, 0);
            for (int i = 0; i < accelerationList.Count; i++)
            {
                chart1.Series[0].Points.AddXY(((i * 2) + 1), accelerationList[i]);
            }
        }
        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //clear graph
                int originalCount = chart1.Series.Count;
                for (int i = 0; i < originalCount; i++)
                {
                    chart1.Series.RemoveAt(0);
                }
                
                //do nothing if it was swiched to empty string 
                if (comboBox1.Text == "") return;

                // check if file is in box
                if (DragAndDropTB.Text == "")
                {
                    MessageBox.Show("Please Enter A File\nForm1.cs:1094");
                    comboBox1.Text = "";
                    testSelectionBox.Text = "";
                    return;
                }
                List<Saccade> rightEyeUp = new List<Saccade>();
                List<Saccade> rightEyeDown = new List<Saccade>();
                List<Saccade> leftEyeUp = new List<Saccade>();
                List<Saccade> leftEyeDown = new List<Saccade>();

                int trialNumber = 0;
                if (testSelectionBox.Text == "Test 1: Head Straight") trialNumber = 1;
                if (testSelectionBox.Text == "Test 2: Head Right Turn") trialNumber = 2;
                if (testSelectionBox.Text == "Test 3: Head Left Turn") trialNumber = 3;
                if (trialNumber == 0)
                {
                    MessageBox.Show("Select A Test First\nForm1.cs:1110");
                    comboBox1.Text = "";
                    return;
                }
                GetEndSaccadeData(ref rightEyeUp, ref rightEyeDown, ref leftEyeUp, ref leftEyeDown, trialNumber);

                List<List<double>> rightEyeUpSamples = new List<List<double>>();
                List<List<double>> rightEyeDownSamples = new List<List<double>>();
                List<List<double>> leftEyeUpSamples = new List<List<double>>();
                List<List<double>> leftEyeDownSamples = new List<List<double>>();

                GetSampleData(ref rightEyeUp, ref rightEyeDown, ref leftEyeUp, ref leftEyeDown, ref rightEyeUpSamples, ref rightEyeDownSamples, ref leftEyeUpSamples, ref leftEyeDownSamples);
                //Set Title 
                chart1.Titles[0].Text = comboBox1.Text;
                
                List<double> averageList = new List<double>();
                if (comboBox1.Text.Contains("Right Eye") && comboBox1.Text.Contains("Sursumduction"))
                {
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref rightEyeUpSamples);
                    averageList = CalculateAverageSet(ref rightEyeUpSamples);
                    if (comboBox1.Text.Contains("Acceleration"))
                    {
                        ChartAcceleration(averageList);
                    }
                    else if (comboBox1.Text.Contains("Average"))
                    {
                        ChartAverage(averageList);
                    }
                    else
                    {
                        ChartAverage(averageList);
                        ChartAllSets(rightEyeUpSamples);
                    }
                }
                if (comboBox1.Text.Contains("Right Eye") && comboBox1.Text.Contains("Infraduction"))
                {
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref rightEyeDownSamples);
                    averageList = CalculateAverageSet(ref rightEyeDownSamples);
                    if (comboBox1.Text.Contains("Acceleration"))
                    {
                        ChartAcceleration(averageList);
                    }
                    else if (comboBox1.Text.Contains("Average"))
                    {
                        ChartAverage(averageList);
                    }
                    else
                    {
                        ChartAverage(averageList);
                        ChartAllSets(rightEyeDownSamples);
                    }
                }
                if (comboBox1.Text.Contains("Left Eye") && comboBox1.Text.Contains("Sursumduction"))
                {
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref leftEyeUpSamples);
                    averageList = CalculateAverageSet(ref leftEyeUpSamples);
                    if (comboBox1.Text.Contains("Acceleration"))
                    {
                        ChartAcceleration(averageList);
                    }
                    else if (comboBox1.Text.Contains("Average"))
                    {
                        ChartAverage(averageList);
                    }
                    else
                    {
                        ChartAverage(averageList);
                        ChartAllSets(leftEyeUpSamples);
                    }
                }
                if (comboBox1.Text.Contains("Left Eye") && comboBox1.Text.Contains("Infraduction"))
                {
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref leftEyeDownSamples);
                    averageList = CalculateAverageSet(ref leftEyeDownSamples);
                    if (comboBox1.Text.Contains("Acceleration"))
                    {
                        ChartAcceleration(averageList);
                    }
                    else if (comboBox1.Text.Contains("Average"))
                    {
                        ChartAverage(averageList);
                    }
                    else
                    {
                        ChartAverage(averageList);
                        ChartAllSets(leftEyeDownSamples);
                    }
                }
            }
            catch (FileNotFoundException exseption)
            {
                MessageBox.Show("Error: File Not Found\nForm1.cs:1201");
                DragAndDropTB.Text = string.Empty;
                progressBar1.Value = 0;
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error: Please Enter A Valid File\nForm1.cs:1208");
                progressBar1.Value = 0;
                return;
            }
            catch (UnauthorizedAccessException exseption)
            {
                MessageBox.Show("Error: Unable to Access The File\nForm1.cs:1214");
                DragAndDropTB.Text = string.Empty;
                progressBar1.Value = 0;
                return;
            }
        }
        
        private void DragAndDropTB_TextChanged(object sender, EventArgs e)
        {
            //clear graph
            int originalCount = chart1.Series.Count;
            for (int i = 0; i < originalCount; i++)
            {
                chart1.Series.RemoveAt(0);
            }
            comboBox1.Text = string.Empty;
            chart1.Titles[0].Text = "";
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            DragAndDropTB.Text = string.Empty;
            return;
        }
        
        // opens a select file dialog box
        private void ChooseFileBT_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    DragAndDropTB.Text = file;
                    string text = File.ReadAllText(file);
                    size = text.Length;
                }
                catch (IOException)
                {
                }
            }
        }
        //Creates 2nd form 
        private void BtnFormTwo_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2();
            secondForm.Show();
        }
        
        private void TestSelectionBox_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }
    }
}