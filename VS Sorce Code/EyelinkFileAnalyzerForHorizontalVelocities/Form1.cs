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
        private void GetEndSaccadeData(ref List<Saccade> rightEyeAbductions, ref List<Saccade> rightEyeAdductions, ref List<Saccade> leftEyeAbductions, ref List<Saccade> leftEyeAdductions)
        {
            //find input file directory and create a file stream
            string fileDirectory = DragAndDropTB.Text;
            FileStream inputFile = new FileStream(fileDirectory, FileMode.Open, FileAccess.Read);

            //Read Line by Line
            using (var streamReader = new StreamReader(inputFile))// helps with line by line
            {
                string line;//helps with line by line
                while ((line = streamReader.ReadLine()) != null)//helps with line by line
                {
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
                            MessageBox.Show("File Does Not Have Velocity Data Recorded\nForm1.cs:69");
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

                        if ((startXPosition > 1720) && (startXPosition < 2120)) direction = "Center To ";
                        if (startXPosition > 2120) direction = "Right To ";
                        if (startXPosition < 1720) direction = "Left To ";
                        if ((endXPosition > 1720) && (endXPosition < 2120)) direction += "Center";
                        if (endXPosition > 2120) direction += "Right";
                        if (endXPosition < 1720) direction += "Left";

                        //add to appropriate list of Saccade classes
                        if ((eyeTracked == "Right") && (direction == "Center To Right") && (amplitude > 5))
                        {
                            rightEyeAbductions.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
                                                startYPosition, endXPosition, endYPosition, amplitude, peakVelocity,
                                                    averageVelocity, direction));
                        }
                        if ((eyeTracked == "Right") && (direction == "Center To Left") && (amplitude > 5))
                        {
                            rightEyeAdductions.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
                                                startYPosition, endXPosition, endYPosition, amplitude, peakVelocity,
                                                    averageVelocity, direction));
                        }
                        if ((eyeTracked == "Left") && (direction == "Center To Left") && (amplitude > 5))
                        {
                            leftEyeAbductions.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
                                                startYPosition, endXPosition, endYPosition, amplitude, peakVelocity,
                                                    averageVelocity, direction));
                        }
                        if ((eyeTracked == "Left") && (direction == "Center To Right") && (amplitude > 5))
                        {
                            leftEyeAdductions.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
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
                                if ((splitData.Length < 11) || (splitData[9] == ".") || (!(int.TryParse(splitData[0], out num)))) 
                                {
                                    line = streamReader.ReadLine();
                                    splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                                    continue;
                                }
                                //keep going till we reach the endtime of saccade
                                else if (Convert.ToInt32(splitData[0]) < Convert.ToInt32(listToRead[i].getEndTime()))
                                {
                                    sublist.Add(Convert.ToDouble(splitData[9]));
                                    line = streamReader.ReadLine();
                                    splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                                    continue;
                                }
                                else
                                {
                                    sublist.Add(Convert.ToDouble(splitData[9]));
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
                                if ((splitData.Length < 11) || (splitData[7] == ".") || (!(int.TryParse(splitData[0], out num))))
                                {
                                    line = streamReader.ReadLine();
                                    splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                                    continue;
                                }
                                //keep going till we reach the endtime of saccade
                                if (Convert.ToInt32(splitData[0]) < Convert.ToInt32(listToRead[i].getEndTime()))
                                {
                                    sublist.Add(Convert.ToDouble(splitData[7]));
                                    line = streamReader.ReadLine();
                                    splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                                    continue;
                                }
                                else
                                {
                                    sublist.Add(Convert.ToDouble(splitData[7]));
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
        private void GetSampleData(ref List<Saccade> rightEyeAbductions, ref List<Saccade> rightEyeAdductions, ref List<Saccade> leftEyeAbductions, ref List<Saccade> leftEyeAdductions, ref List<List<double>> horizontalVelocitiesOverTimeRightEyeAbd, ref List<List<double>> horizontalVelocitiesOverTimeRightEyeAdd, ref List<List<double>> horizontalVelocitiesOverTimeLeftEyeAbd, ref List<List<double>> horizontalVelocitiesOverTimeLeftEyeAdd)
        {
            RightSampleReader(ref rightEyeAbductions, ref horizontalVelocitiesOverTimeRightEyeAbd);
            RightSampleReader(ref rightEyeAdductions, ref horizontalVelocitiesOverTimeRightEyeAdd);
            LeftSampleReader(ref leftEyeAbductions, ref horizontalVelocitiesOverTimeLeftEyeAbd);
            LeftSampleReader(ref leftEyeAdductions, ref horizontalVelocitiesOverTimeLeftEyeAdd);   
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
            KeyValuePair<int, double> peakPoint = new KeyValuePair<int, double>(peakAccelerationIndex,peakAcceleration);
            return peakPoint;
        }
        private double GetAverageAmplitude(List<Saccade> listOfSaccades)
        {
            double average = 0;
            for (int i = 0; i < listOfSaccades.Count; i++)
            {
                average += listOfSaccades[i].getAmplitude();
            }
            average /= listOfSaccades.Count;

            return average;
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
                    MessageBox.Show("Please Enter Correct File Type\n(.edf converted to .asc)\nForm1.cs:356");
                    DragAndDropTB.Text = string.Empty;
                    return;
                }
                
                string sameFolder = fileDirectory.Substring(0, fileDirectory.LastIndexOf('.'));
                progressBar1.Value = 5;
                List<Saccade> rightEyeAbductions = new List<Saccade>();
                List<Saccade> rightEyeAdductions = new List<Saccade>();
                List<Saccade> leftEyeAbductions = new List<Saccade>();
                List<Saccade> leftEyeAdductions = new List<Saccade>();
                
                GetEndSaccadeData(ref rightEyeAbductions, ref rightEyeAdductions, ref leftEyeAbductions, ref leftEyeAdductions);
                //Check if data is valid
                if ((rightEyeAbductions.Count == 0) || (rightEyeAdductions.Count == 0) || (leftEyeAbductions.Count == 0) || (leftEyeAdductions.Count == 0))
                {
                    if (rightEyeAbductions.Count == 0) MessageBox.Show("File Cannot Be Analyzed\nNo Right Eye Abductions Detected\nForm1.cs:372");
                    else if (rightEyeAdductions.Count == 0) MessageBox.Show("File Cannot Be Analyzed\nNo Right Eye Adductions Detected\nForm1.cs:373");
                    else if (leftEyeAbductions.Count == 0) MessageBox.Show("File Cannot Be Analyzed\nNo Left Eye Abductions Detected\nForm1.cs:374");
                    else MessageBox.Show("File Cannot be Analyzed\nNo Left Eye Adductions Detected\nForm1.cs:375");
                    progressBar1.Value = 0;
                    DragAndDropTB.Text = string.Empty;
                    return;
                }
                //get sample data 
                List<List<double>> horizontalVelocitiesOverTimeRightEyeAbd = new List<List<double>>();
                List<List<double>> horizontalVelocitiesOverTimeRightEyeAdd = new List<List<double>>();
                List<List<double>> horizontalVelocitiesOverTimeLeftEyeAbd = new List<List<double>>();
                List<List<double>> horizontalVelocitiesOverTimeLeftEyeAdd = new List<List<double>>();
                
                GetSampleData(ref rightEyeAbductions, ref rightEyeAdductions, ref leftEyeAbductions, ref leftEyeAdductions, ref horizontalVelocitiesOverTimeRightEyeAbd, ref horizontalVelocitiesOverTimeRightEyeAdd, ref horizontalVelocitiesOverTimeLeftEyeAbd, ref horizontalVelocitiesOverTimeLeftEyeAdd);
                //Declaration of averageList
                List<double> averageList = new List<double>();
                progressBar1.Value = 7;
                string pdfPath = sameFolder + "Report.pdf";
                string imagePath = sameFolder + "Pic.gif";//create a .gif file used to make graphs
                Document doc = new Document();
                try
                {
                    PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
                    doc.Open();
                    
                    //RIGHT EYE ABDUCTION
                    doc.Add(new Paragraph("Right Eye Abduction: \n"));
                    double rightEyeAdbTotal = 0;
                    for (int i = 0; i < rightEyeAbductions.Count; i++)
                    {
                        rightEyeAdbTotal += rightEyeAbductions[i].getAverageVelocity();
                    }
                    rightEyeAdbTotal /= rightEyeAbductions.Count;
                    //calculate peak velocity based on graph data 
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeRightEyeAbd);
                    averageList = CalculateAverageSet(ref horizontalVelocitiesOverTimeRightEyeAbd);
                    double peakValue = FindPeakValue(averageList);
                    //Output variables
                    doc.Add(new Paragraph("Average Velocity: " + Math.Round(rightEyeAdbTotal, 2) + " degrees/second\n"));
                    doc.Add(new Paragraph("Average Amplitude: " + Math.Round(GetAverageAmplitude(rightEyeAbductions), 2) + " degrees\n"));
                    doc.Add(new Paragraph("Peak Velocity of Average Curve: " + Math.Round(peakValue, 2) + " degrees/second\n"));
                    //Calculate Standard Divation
                    double standardDeviation = FindPeakVelocityStandardDivation(horizontalVelocitiesOverTimeRightEyeAbd);
                    doc.Add(new Paragraph("Peak Velocity Standard Deviation: " + Math.Round(standardDeviation, 2)));
                    
                    //get sets graph
                    comboBox1.Text = "Right Eye Abductions";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    Image gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 8;
                    //get average graph
                    comboBox1.Text = "Right Eye Average Abduction";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 8;

                    doc.NewPage();
                    //Print Acceleration Graph
                    KeyValuePair<int, double> peakAccelerationDataPoint = new KeyValuePair<int, double>();
                    peakAccelerationDataPoint = GetPeakAccelerationPoint(averageList);
                    doc.Add(new Paragraph("Peak Acceleration: " + peakAccelerationDataPoint.Value + " degrees/second^2\n"));
                    doc.Add(new Paragraph("Time At Peak Acceleration: " + peakAccelerationDataPoint.Key + " milliseconds\n"));

                    //get accelration graph
                    comboBox1.Text = "Right Eye Average Acceleration Abduction";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 8;

                    doc.NewPage();

                    //RIGHT EYE ADDUCTION
                    doc.Add(new Paragraph("Right Eye Adduction:"));

                    double averageTotal = 0;
                    for (int i = 0; i < rightEyeAdductions.Count; i++)
                    {
                        averageTotal += rightEyeAdductions[i].getAverageVelocity();
                    }
                    averageTotal /= rightEyeAdductions.Count;

                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeRightEyeAdd);
                    averageList = CalculateAverageSet(ref horizontalVelocitiesOverTimeRightEyeAdd);

                    peakValue = FindPeakValue(averageList);
                    doc.Add(new Paragraph("Average Velocity: " + (-1 * Math.Round(averageTotal, 2)) + " degrees/second\n"));
                    doc.Add(new Paragraph("Average Amplitude: " + Math.Round(GetAverageAmplitude(rightEyeAdductions), 2) + " degrees\n"));
                    doc.Add(new Paragraph("Peak Velocity of Average Curve: " + (-1 * Math.Round(peakValue, 2)) + " degrees/second\n"));
                    //Calculate Standard Divation
                    standardDeviation = FindPeakVelocityStandardDivation(horizontalVelocitiesOverTimeRightEyeAdd);
                    doc.Add(new Paragraph("Peak Velocity Standard Deviation: " + Math.Round(standardDeviation, 2)));

                    //get sets graph
                    comboBox1.Text = "Right Eye Adductions";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 8;
                    //get average graph
                    comboBox1.Text = "Right Eye Average Adduction";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 8;

                    doc.NewPage();
                    //Print Acceleration Graph
                    peakAccelerationDataPoint = GetPeakAccelerationPoint(averageList);
                    doc.Add(new Paragraph("Peak Acceleration: " + (-1 * peakAccelerationDataPoint.Value) + " degrees/second^2\n"));
                    doc.Add(new Paragraph("Time At Peak Acceleration: " + peakAccelerationDataPoint.Key + " milliseconds\n"));

                    //get accelration graph
                    comboBox1.Text = "Right Eye Average Acceleration Adduction";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 8;

                    doc.NewPage();
                    //LEFT EYE ABDUCTION
                    doc.Add(new Paragraph("Left Eye Abduction:"));

                    averageTotal = 0;
                    for (int i = 0; i < leftEyeAbductions.Count; i++)
                    {
                        averageTotal += leftEyeAbductions[i].getAverageVelocity();
                    }
                    averageTotal /= leftEyeAbductions.Count;

                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeLeftEyeAbd);
                    averageList = CalculateAverageSet(ref horizontalVelocitiesOverTimeLeftEyeAbd);

                    peakValue = FindPeakValue(averageList);
                    doc.Add(new Paragraph("Average Velocity: " + (-1 * Math.Round(averageTotal, 2)) + " degrees/second\n"));
                    doc.Add(new Paragraph("Average Amplitude: " + Math.Round(GetAverageAmplitude(leftEyeAbductions), 2) + " degrees\n"));
                    doc.Add(new Paragraph("Peak Velocity of Average Curve: " + (-1 * Math.Round(peakValue, 2)) + " degrees/second\n"));
                    //Calculate Standard Divation
                    standardDeviation = FindPeakVelocityStandardDivation(horizontalVelocitiesOverTimeLeftEyeAbd);
                    doc.Add(new Paragraph("Peak Velocity Standard Deviation: " + Math.Round(standardDeviation, 2)));
                    //get sets graph
                    comboBox1.Text = "Left Eye Abductions";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 8;
                    //get average graph
                    comboBox1.Text = "Left Eye Average Abduction";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 8;

                    doc.NewPage();
                    //Print Acceleration Graph
                    peakAccelerationDataPoint = GetPeakAccelerationPoint(averageList);
                    doc.Add(new Paragraph("Peak Acceleration: " + (-1 * peakAccelerationDataPoint.Value) + " degrees/second^2\n"));
                    doc.Add(new Paragraph("Time At Peak Acceleration: " + peakAccelerationDataPoint.Key + " milliseconds\n"));

                    //get accelration graph
                    comboBox1.Text = "Left Eye Average Acceleration Abduction";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 7;
                    
                    doc.NewPage();
                    //LEFT EYE ADDUCTION
                    doc.Add(new Paragraph("Left Eye Adduction:"));

                    averageTotal = 0;
                    for (int i = 0; i < leftEyeAdductions.Count; i++)
                    {
                        averageTotal += leftEyeAdductions[i].getAverageVelocity();
                    }
                    averageTotal /= leftEyeAdductions.Count;

                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeLeftEyeAdd);
                    averageList = CalculateAverageSet(ref horizontalVelocitiesOverTimeLeftEyeAdd);

                    peakValue = FindPeakValue(averageList);
                    doc.Add(new Paragraph("Average Velocity: " + Math.Round(averageTotal, 2) + " degrees/second\n"));
                    doc.Add(new Paragraph("Average Amplitude: " + Math.Round(GetAverageAmplitude(leftEyeAdductions), 2) + " degrees\n"));
                    doc.Add(new Paragraph("Peak Velocity of Average Curve: " + Math.Round(peakValue, 2) + " degrees/second\n"));
                    //Calculate Standard Divation
                    standardDeviation = FindPeakVelocityStandardDivation(horizontalVelocitiesOverTimeLeftEyeAdd);
                    doc.Add(new Paragraph("Peak Velocity Standard Deviation: " + Math.Round(standardDeviation, 2)));
                   
                    //get sets graph
                    comboBox1.Text = "Left Eye Adductions";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 8;
                    //get average graph
                    comboBox1.Text = "Left Eye Average Adduction";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 7;
                    
                    doc.NewPage();
                    //Print Acceleration Graph
                    peakAccelerationDataPoint = GetPeakAccelerationPoint(averageList);
                    doc.Add(new Paragraph("Peak Acceleration: " + peakAccelerationDataPoint.Value + " degrees/second^2\n"));
                    doc.Add(new Paragraph("Time At Peak Acceleration: " + peakAccelerationDataPoint.Key + " milliseconds\n"));

                    //get accelration graph
                    comboBox1.Text = "Left Eye Average Acceleration Adduction";
                    chart1.SaveImage(imagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif);
                    gif = Image.GetInstance(imagePath);
                    doc.Add(gif);
                    progressBar1.Value += 7;

                    //delete image file used to crate pdf
                    File.Delete(imagePath);
                    progressBar1.Value = 0;
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("The report .pdf file is already open. \n Close it and try again\nForm1.cs:595");
                    progressBar1.Value = 0;
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Non IOException Error\n" + ex.Message + "\nMake sure the ASC file includes velocity data\nForm1.cs:601");
                    progressBar1.Value = 0;
                    return;
                }
                finally
                {
                    doc.Close();
                }
                doc.Close();
                DragAndDropTB.Text = string.Empty;
            }
            //Exception Handelining 
            catch (FileNotFoundException exseption)
            {
                MessageBox.Show("Error: File Not Found\nForm1.cs:615");
                DragAndDropTB.Text = string.Empty;
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error: Please Enter A Valid File\nForm1.cs:621");
                return;
            }
            catch (UnauthorizedAccessException exseption)
            {
                MessageBox.Show("Error: No Permission To Access The File\nForm1.cs:626");
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
                    MessageBox.Show("Please Enter Correct File Type\n(.edf converted to .asc)\nForm1.cs:643");
                    DragAndDropTB.Text = string.Empty;
                    return;
                }

                string sameFolder = fileDirectory.Substring(0, fileDirectory.LastIndexOf('.'));
                FileStream outputFile = new FileStream(sameFolder + "RawData.txt", FileMode.Create, FileAccess.Write);

                List<Saccade> rightEyeAbductions = new List<Saccade>();
                List<Saccade> rightEyeAdductions = new List<Saccade>();
                List<Saccade> leftEyeAbductions = new List<Saccade>();
                List<Saccade> leftEyeAdductions = new List<Saccade>();

                GetEndSaccadeData(ref rightEyeAbductions, ref rightEyeAdductions, ref leftEyeAbductions, ref leftEyeAdductions);
                
                List<List<double>> horizontalVelocitiesOverTimeRightEyeAbd = new List<List<double>>();
                List<List<double>> horizontalVelocitiesOverTimeRightEyeAdd = new List<List<double>>();
                List<List<double>> horizontalVelocitiesOverTimeLeftEyeAbd = new List<List<double>>(); 
                List<List<double>> horizontalVelocitiesOverTimeLeftEyeAdd = new List<List<double>>();

                GetSampleData(ref rightEyeAbductions, ref rightEyeAdductions, ref leftEyeAbductions, ref leftEyeAdductions, ref horizontalVelocitiesOverTimeRightEyeAbd, ref horizontalVelocitiesOverTimeRightEyeAdd, ref horizontalVelocitiesOverTimeLeftEyeAbd, ref horizontalVelocitiesOverTimeLeftEyeAdd);
                if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeRightEyeAbd);
                if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeRightEyeAdd);
                if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeLeftEyeAbd);
                if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeLeftEyeAdd);
                //Output formating
                string outputString = "RIGHT EYE ABDUCTIONS HORIZONTAL VELOCITY OVER TIME: "; //Cration of outputString
                byte[] bytes = Encoding.ASCII.GetBytes(outputString);// Cration of bytes
                byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);// Creation of newline
                
                
                outputFile.Write(bytes, 0, outputString.Length);
                outputFile.Write(newline, 0, newline.Length);
                outputFile.Write(newline, 0, newline.Length);
                outputFile.Flush();

                for (int i = 0; i < horizontalVelocitiesOverTimeRightEyeAbd.Count; i++)
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
                    for (int j = 0; j < horizontalVelocitiesOverTimeRightEyeAbd[i].Count; j++)
                    {

                        outputString = (j * 2) + " , " + Convert.ToString(horizontalVelocitiesOverTimeRightEyeAbd[i][j]);
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                    }
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Flush();
                }

                outputString = "RIGHT EYE ADDUCTIONS HORIZONTAL VELOCITY OVER TIME: "; //change
                bytes = Encoding.ASCII.GetBytes(outputString);
                outputFile.Write(bytes, 0, outputString.Length);
                outputFile.Write(newline, 0, newline.Length);
                outputFile.Write(newline, 0, newline.Length);
                outputFile.Flush();

                for (int i = 0; i < horizontalVelocitiesOverTimeRightEyeAdd.Count; i++)//chagne
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
                    for (int j = 0; j < horizontalVelocitiesOverTimeRightEyeAdd[i].Count; j++)//change
                    {
                        outputString = (j * 2) + " , " + Convert.ToString(horizontalVelocitiesOverTimeRightEyeAdd[i][j]);//change
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                    }
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Flush();
                }

                outputString = "LEFT EYE ABDUCTIONS HORIZONTAL VELOCITY OVER TIME: "; //change
                bytes = Encoding.ASCII.GetBytes(outputString);
                outputFile.Write(bytes, 0, outputString.Length);
                outputFile.Write(newline, 0, newline.Length);
                outputFile.Write(newline, 0, newline.Length);
                outputFile.Flush();

                for (int i = 0; i < horizontalVelocitiesOverTimeLeftEyeAbd.Count; i++)//chagne
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
                    for (int j = 0; j < horizontalVelocitiesOverTimeLeftEyeAbd[i].Count; j++)//change
                    {

                        outputString = (j * 2) + " , " + Convert.ToString(horizontalVelocitiesOverTimeLeftEyeAbd[i][j]);//change
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                    }
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Flush();
                }

                outputString = "LEFT EYE ADDUCTIONS HORIZONTAL VELOCITY OVER TIME: "; //change
                bytes = Encoding.ASCII.GetBytes(outputString);
                outputFile.Write(bytes, 0, outputString.Length);
                outputFile.Write(newline, 0, newline.Length);
                outputFile.Write(newline, 0, newline.Length);
                outputFile.Flush();

                for (int i = 0; i < horizontalVelocitiesOverTimeLeftEyeAdd.Count; i++)//chagne
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
                    for (int j = 0; j < horizontalVelocitiesOverTimeLeftEyeAdd[i].Count; j++)//change
                    {

                        outputString = (j * 2) + " , " + Convert.ToString(horizontalVelocitiesOverTimeLeftEyeAdd[i][j]);//change
                        bytes = Encoding.ASCII.GetBytes(outputString);
                        outputFile.Write(bytes, 0, outputString.Length);
                        outputFile.Write(newline, 0, newline.Length);
                        outputFile.Flush();
                    }
                    outputFile.Write(newline, 0, newline.Length);
                    outputFile.Flush();
                }
                //Close files and clear message
                outputFile.Close();
                DragAndDropTB.Text = string.Empty;
            }
            catch (FileNotFoundException exseption)
            {
                MessageBox.Show("Error: File Not Found\nForm1.cs:800");
                DragAndDropTB.Text = string.Empty;
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error: Please Enter A Valid File\nForm1.cs:806");
                return;
            }
            catch (UnauthorizedAccessException exseption)
            {
                MessageBox.Show("Error: No Permission To Access The File\nForm1.cs:811");
                //DragAndDropTB.Text = string.Empty;
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

            for (int j = 0; j < FindMinSetTime(listOfSets); j++)
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

                List<Saccade> rightEyeAbductions = new List<Saccade>();
                List<Saccade> rightEyeAdductions = new List<Saccade>();
                List<Saccade> leftEyeAbductions = new List<Saccade>();
                List<Saccade> leftEyeAdductions = new List<Saccade>();

                GetEndSaccadeData(ref rightEyeAbductions, ref rightEyeAdductions, ref leftEyeAbductions, ref leftEyeAdductions);

                List<List<double>> horizontalVelocitiesOverTimeRightEyeAbd = new List<List<double>>();
                List<List<double>> horizontalVelocitiesOverTimeRightEyeAdd = new List<List<double>>();
                List<List<double>> horizontalVelocitiesOverTimeLeftEyeAbd = new List<List<double>>();
                List<List<double>> horizontalVelocitiesOverTimeLeftEyeAdd = new List<List<double>>();

                GetSampleData(ref rightEyeAbductions, ref rightEyeAdductions, ref leftEyeAbductions, ref leftEyeAdductions, ref horizontalVelocitiesOverTimeRightEyeAbd, ref horizontalVelocitiesOverTimeRightEyeAdd, ref horizontalVelocitiesOverTimeLeftEyeAbd, ref horizontalVelocitiesOverTimeLeftEyeAdd);
                //Set Title 
                chart1.Titles[0].Text = comboBox1.Text;

                List<double> averageList = new List<double>();
                if (comboBox1.Text.Contains("Right Eye") && comboBox1.Text.Contains("Abduction"))
                {
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeRightEyeAbd);
                    averageList = CalculateAverageSet(ref horizontalVelocitiesOverTimeRightEyeAbd);
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
                        ChartAllSets(horizontalVelocitiesOverTimeRightEyeAbd);
                    }
                }
                if (comboBox1.Text.Contains("Right Eye") && comboBox1.Text.Contains("Adduction"))
                {

                if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeRightEyeAdd);
                    averageList = CalculateAverageSet(ref horizontalVelocitiesOverTimeRightEyeAdd);
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
                        ChartAllSets(horizontalVelocitiesOverTimeRightEyeAdd);
                    }
                }
                if (comboBox1.Text.Contains("Left Eye") && comboBox1.Text.Contains("Abduction"))
                {
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeLeftEyeAbd);
                    averageList = CalculateAverageSet(ref horizontalVelocitiesOverTimeLeftEyeAbd);
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
                        ChartAllSets(horizontalVelocitiesOverTimeLeftEyeAbd);
                    }
                }
                if (comboBox1.Text.Contains("Left Eye") && comboBox1.Text.Contains("Adduction"))
                {
                    if (removeOutliersBT.Checked == true) RemoveOutliers(ref horizontalVelocitiesOverTimeLeftEyeAdd);
                    averageList = CalculateAverageSet(ref horizontalVelocitiesOverTimeLeftEyeAdd);
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
                        ChartAllSets(horizontalVelocitiesOverTimeLeftEyeAdd);
                    }
                }  
            }
            catch (FileNotFoundException exseption)
            {
                MessageBox.Show("Error: File Not Found\nForm1.cs:1044");
                DragAndDropTB.Text = string.Empty;
                progressBar1.Value = 0;
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error: Please Enter A Valid File\nForm1.cs:1051");
                progressBar1.Value = 0;
                return;
            }
            catch (UnauthorizedAccessException exseption)
            {
                MessageBox.Show("Error: No Permission To Access The File\nForm1.cs:1057");
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
                    MessageBox.Show("IOException\nForm1.cs:1098");
                }
            }
        }

        //Creates 2nd form 
        private void BtnFormTwo_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2();
            secondForm.Show();
        }
    }
}