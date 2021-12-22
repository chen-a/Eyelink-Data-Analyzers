using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EyelinkFileAnalizer
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void ListBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < s.Length; i++)
            {
                listBox1.Items.Add(s[i]);
            }
            return;
        }

        private void ListBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void SelectFilesButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // Read the files
                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        listBox1.Items.Add(file);
                    }
                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message + "\nForm2.cs:53");
                    }
                }
            }
        }

        private void ListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = listBox1.SelectedItems.Count - 1; i >= 0; i--)
            {
                listBox1.Items.Remove(listBox1.SelectedItems[i]);
            }

        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            int originalSize = listBox1.Items.Count;
            for (int i = 0; i < originalSize; i++)
            {
                listBox1.Items.RemoveAt(0);
            }
            resultsTB.Text = "";
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
        private void MainButton_Click(object sender, EventArgs e)
        {
            resultsTB.Text = "";
            List<double> averageVelocitesRightEyeAbd = new List<double>();
            List<double> peakVelocitesRightEyeAbd = new List<double>();

            List<double> averageVelocitesRightEyeAdd = new List<double>();
            List<double> peakVelocitesRightEyeAdd = new List<double>();

            List<double> averageVelocitesLeftEyeAbd = new List<double>();
            List<double> peakVelocitesLeftEyeAbd = new List<double>();

            List<double> averageVelocitesLeftEyeAdd = new List<double>();
            List<double> peakVelocitesLeftEyeAdd = new List<double>();

            foreach (string file in listBox1.Items)
            {
                string fileDirectory = file;
                string fileType = fileDirectory.Substring(fileDirectory.LastIndexOf('.'));

                //Checks for the correct file type
                if ((fileType != ".asc") && (fileType != ".txt"))
                {
                    MessageBox.Show("Incorect File Type Found\nPlease Enter Only Correct File Types\n(.edf converted to .asc)\n\nPress \"OK\" To Remove Incorrect Files\nForm2.cs:130");
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        string item = Convert.ToString(listBox1.Items[i]);
                        fileType = item.Substring(item.LastIndexOf('.'));
                        if ((fileType != ".asc") && (fileType != ".txt"))
                        {
                            listBox1.Items.RemoveAt(i);
                            --i;
                        }
                    }
                    return;
                }
                string sameFolder = fileDirectory.Substring(0, fileDirectory.LastIndexOf('.'));

                List<Saccade> rightEyeAbductions = new List<Saccade>();
                List<Saccade> rightEyeAdductions = new List<Saccade>();
                List<Saccade> leftEyeAbductions = new List<Saccade>();
                List<Saccade> leftEyeAdductions = new List<Saccade>();

                FileStream inputFile = new FileStream(fileDirectory, FileMode.Open, FileAccess.Read);

                //Read Line by Line
                using (var streamReader = new StreamReader(inputFile))// helps with line by line
                {
                    string line;//helps with line by line
                    while ((line = streamReader.ReadLine()) != null)//helps with line by line
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

                        //Find End Saccade data line
                        // Data format: ESACC <Eye Tracked> <STime> <ETime> <Dur> <Sxpos> <Sypos> <Expos> <Eypos> <Ampl> <PeakVel> 
                        if (line.Contains("ESACC"))
                        {
                            string[] splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                            if (splitData.Length < 11)
                            {
                                MessageBox.Show("File does not have velocity data recorded\nForm2.cs:179");
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
                            if ((eyeTracked == "Right") && (direction == "Center To Left") && (amplitude > 5))
                            {
                                rightEyeAdductions.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
                                                    startYPosition, endXPosition, endYPosition, amplitude, peakVelocity,
                                                        averageVelocity, direction));
                            }
                            if ((eyeTracked == "Right") && (direction == "Center To Right") && (amplitude > 5))
                            {
                                rightEyeAbductions.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
                                                    startYPosition, endXPosition, endYPosition, amplitude, peakVelocity,
                                                        averageVelocity, direction));
                            }
                            if ((eyeTracked == "Left") && (direction == "Center To Left") && (amplitude > 5))
                            {
                                leftEyeAdductions.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
                                                    startYPosition, endXPosition, endYPosition, amplitude, peakVelocity,
                                                        averageVelocity, direction));
                            }
                            if ((eyeTracked == "Left") && (direction == "Center To Right") && (amplitude > 5))
                            {
                                leftEyeAbductions.Add(new Saccade(eyeTracked, startTime, endTime, duration, startXPosition,
                                                    startYPosition, endXPosition, endYPosition, amplitude, peakVelocity,
                                                        averageVelocity, direction));
                            }
                        }
                    }
                }
                inputFile.Close();
                //Right Eye Abductions
                double averageTotal = 0;
                for (int i = 0; i < rightEyeAbductions.Count; i++)
                {
                    averageTotal += rightEyeAbductions[i].getAverageVelocity();
                }
                averageTotal /= rightEyeAbductions.Count;

                double peakTotal = 0;
                for (int i = 0; i < rightEyeAbductions.Count; i++)
                {
                    peakTotal += rightEyeAbductions[i].getPeakVelocity();
                }
                peakTotal /= rightEyeAbductions.Count;

                averageVelocitesRightEyeAbd.Add(averageTotal);
                peakVelocitesRightEyeAbd.Add(peakTotal);

                //Right Eye Adductions
                averageTotal = 0;
                for (int i = 0; i < rightEyeAdductions.Count; i++)
                {
                    averageTotal += rightEyeAdductions[i].getAverageVelocity();
                }
                averageTotal /= rightEyeAdductions.Count;

                peakTotal = 0;
                for (int i = 0; i < rightEyeAdductions.Count; i++)
                {
                    peakTotal += rightEyeAdductions[i].getPeakVelocity();
                }
                peakTotal /= rightEyeAdductions.Count;

                averageVelocitesRightEyeAdd.Add(averageTotal);
                peakVelocitesRightEyeAdd.Add(peakTotal);

                //Left Eye Abductions
                averageTotal = 0;
                for (int i = 0; i < leftEyeAbductions.Count; i++)
                {
                    averageTotal += leftEyeAbductions[i].getAverageVelocity();
                }
                averageTotal /= leftEyeAbductions.Count;

                peakTotal = 0;
                for (int i = 0; i < leftEyeAbductions.Count; i++)
                {
                    peakTotal += leftEyeAbductions[i].getPeakVelocity();
                }
                peakTotal /= leftEyeAbductions.Count;

                averageVelocitesLeftEyeAbd.Add(averageTotal);
                peakVelocitesLeftEyeAbd.Add(peakTotal);

                //Left Eye Adductions
                averageTotal = 0;
                for (int i = 0; i < leftEyeAdductions.Count; i++)
                {
                    averageTotal += leftEyeAdductions[i].getAverageVelocity();
                }
                averageTotal /= leftEyeAdductions.Count;

                peakTotal = 0;
                for (int i = 0; i < leftEyeAdductions.Count; i++)
                {
                    peakTotal += leftEyeAdductions[i].getPeakVelocity();
                }
                peakTotal /= leftEyeAdductions.Count;

                averageVelocitesLeftEyeAdd.Add(averageTotal);
                peakVelocitesLeftEyeAdd.Add(peakTotal);
            }
            resultsTB.AppendText("Right Eye Abduction:");
            resultsTB.AppendText(Environment.NewLine);
            resultsTB.AppendText("   Average Velocity: " + CalculateStandardDiviation(averageVelocitesRightEyeAbd));
            resultsTB.AppendText(Environment.NewLine);
            resultsTB.AppendText("   Peak Velocity: " + CalculateStandardDiviation(peakVelocitesRightEyeAbd));
            resultsTB.AppendText(Environment.NewLine);
            resultsTB.AppendText("Right Eye Adduction:");
            resultsTB.AppendText(Environment.NewLine);
            resultsTB.AppendText("   Average Velocity: " + CalculateStandardDiviation(averageVelocitesRightEyeAdd));
            resultsTB.AppendText(Environment.NewLine);
            resultsTB.AppendText("   Peak Velocity: " + CalculateStandardDiviation(peakVelocitesRightEyeAdd));
            resultsTB.AppendText(Environment.NewLine);
            resultsTB.AppendText("Left Eye Abduction:");
            resultsTB.AppendText(Environment.NewLine);
            resultsTB.AppendText("   Average Velocity: " + CalculateStandardDiviation(averageVelocitesLeftEyeAbd));
            resultsTB.AppendText(Environment.NewLine);
            resultsTB.AppendText("   Peak Velocity: " + CalculateStandardDiviation(peakVelocitesLeftEyeAbd));
            resultsTB.AppendText(Environment.NewLine);
            resultsTB.AppendText("Left Eye Adduction:");
            resultsTB.AppendText(Environment.NewLine);
            resultsTB.AppendText("   Average Velocity: " + CalculateStandardDiviation(averageVelocitesLeftEyeAdd));
            resultsTB.AppendText(Environment.NewLine);
            resultsTB.AppendText("   Peak Velocity: " + CalculateStandardDiviation(peakVelocitesLeftEyeAdd));
        }
    }
}
