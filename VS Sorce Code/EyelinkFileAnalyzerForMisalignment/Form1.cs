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
        private void PrintPositionData(EyePositions eyePositionClass)
        {

            resultsTB.AppendText("Right Eye Pixel Fixation point: (" + eyePositionClass.getRightEyeXPos() + " , " + eyePositionClass.getRightEyeYPos() + ")" + Environment.NewLine);
            resultsTB.AppendText("Left Eye Pixel Fixation point: (" + eyePositionClass.getLeftEyeXPos() + " , " + eyePositionClass.getLeftEyeYPos() + ")" + Environment.NewLine);

            // determine if eyes are crossed 
            bool eyesCrossed = false;
            bool wallEyes = false;
            bool rightUp = false;
            bool leftUp = false;
            

            if ((eyePositionClass.getRightEyeXPos() < eyePositionClass.getLeftEyeXPos())) eyesCrossed = true;
            if ((eyePositionClass.getRightEyeXPos() > eyePositionClass.getLeftEyeXPos())) wallEyes = true;
            if ((eyePositionClass.getRightEyeYPos() < eyePositionClass.getLeftEyeYPos())) leftUp = true;
            if ((eyePositionClass.getRightEyeYPos() > eyePositionClass.getLeftEyeYPos())) rightUp = true;

            //find distance between eyes
            double horizontalDistance = Math.Sqrt(Math.Pow((eyePositionClass.getLeftEyeXPos() - eyePositionClass.getRightEyeXPos()), 2));
            double verticalDistance = Math.Sqrt(Math.Pow((eyePositionClass.getLeftEyeYPos() - eyePositionClass.getRightEyeYPos()), 2));
            double eyeToScreenDistance = 0;

            double num = 0;
            string candidate = eyeToScreenTB.Text;
            if (double.TryParse(candidate, out num))//check if text is a number
            {
                eyeToScreenDistance = num;
            }
            else
            {
                eyeToScreenDistance = 790;
            }


            //convert Pixles to Inches
            // 96 is the pixles per inch on the screen we are using
            horizontalDistance = (horizontalDistance / 96);
            verticalDistance = (verticalDistance / 96);

            //convert Inches to Millimeters
            // 25.4 is the milimeters per inch converstion rate
            horizontalDistance = (horizontalDistance * 25.4);
            verticalDistance = (verticalDistance * 25.4);

            //resultsTB.AppendText("Horizontal Distance: " + Math.Round(horizontalDistance, 2) + " Millimeters" + Environment.NewLine);
            //resultsTB.AppendText("Vertical Distance: " + Math.Round(verticalDistance, 2) + " Millimeters" + Environment.NewLine);

            //get radians from triangle

            double horizontalNumberDivided = horizontalDistance / eyeToScreenDistance;
            double horizontalMisalignmentRadians = Math.Atan(horizontalNumberDivided);

            double verticalNumberDivided = verticalDistance / eyeToScreenDistance;
            double verticalMisalignmentRadians = Math.Atan(verticalNumberDivided);

            // radians to degrees
            double horizontalMisalignmentDegrees = horizontalMisalignmentRadians * (180 / Math.PI);
            double verticalMisalignmentDegrees = verticalMisalignmentRadians * (180 / Math.PI);

            resultsTB.AppendText("Horizontal Eye Misalignment: " + Math.Round(horizontalMisalignmentDegrees, 4) + " Degrees" + Environment.NewLine);
            resultsTB.AppendText("Vertical Eye Misalignment: " + Math.Round(verticalMisalignmentDegrees, 4) + " Degrees" + Environment.NewLine);

            resultsTB.AppendText("Classifications: " + Environment.NewLine);
            //if the resluts is less then a degree its insignificent 
            if (horizontalMisalignmentDegrees < 1)
            {
                resultsTB.AppendText("  Less than 1 degree of horizontal misalignment" + Environment.NewLine);
            }
            else if (eyesCrossed)
            {
                resultsTB.AppendText("  Esotropia: " + eyesCrossed + Environment.NewLine);
            }
            else if (wallEyes)
            {
                resultsTB.AppendText("  Exotropia: " + wallEyes + Environment.NewLine);
            }
            if (verticalMisalignmentDegrees < 1)
            {
                resultsTB.AppendText("  Less than 1 degree of vertical misalignment" + Environment.NewLine);
            }
            else if (rightUp)
            {
                resultsTB.AppendText("  Right Hypertropia: " + rightUp + Environment.NewLine);
            }
            else if (leftUp)
            {
                resultsTB.AppendText("  Left Hypertropia: " + leftUp + Environment.NewLine);
            }
            return;
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            resultsTB.Text = "";
            try
            {
                string fileDirectory = DragAndDropTB.Text;
                string fileType = fileDirectory.Substring(fileDirectory.LastIndexOf('.'));

                //Checks for the correct file type
                if ((fileType != ".asc") && (fileType != ".txt"))
                {
                    MessageBox.Show("Please Enter Correct File Type\n(.edf converted to .asc)");
                    DragAndDropTB.Text = string.Empty;
                    return;
                }
                string sameFolder = fileDirectory.Substring(0, fileDirectory.LastIndexOf('.'));


                //find input file directory and create a file stream
                FileStream inputFile = new FileStream(fileDirectory, FileMode.Open, FileAccess.Read);

                double rightEyePosX = 0;
                double rightEyePosY = 0;
                double leftEyePosX = 0;
                double leftEyePosY = 0;
                bool foundRight = false;
                bool foundLeft = false;
                EyePositions Center = new EyePositions();
                EyePositions Top = new EyePositions();
                EyePositions Right = new EyePositions();
                EyePositions Bottom = new EyePositions();
                EyePositions Left = new EyePositions();
                //Read Line by Line
                using (var streamReader = new StreamReader(inputFile))// helps with line by line
                {
                    string position = "";
                    string line;//helps with line by line
                    while ((line = streamReader.ReadLine()) != null)//helps with line by line
                    {
                        //find where target is
                        if (line.Contains("SHOW_CENTER")) position = "Center";
                        if (line.Contains("SHOW_TOP")) position = "Top";
                        if (line.Contains("SHOW_RIGHT")) position = "Right";
                        if (line.Contains("SHOW_BOTTOM")) position = "Bottom";
                        if (line.Contains("SHOW_LEFT")) position = "Left";
                        if (position == "") continue;
                        
                        //Find End Saccade data line
                        // Data format: ESACC <Eye Tracked> <STime> <ETime> <Dur> <Sxpos> <Sypos> <Expos> <Eypos> <Ampl> <PeakVel> 
                        if ((line.Contains("EFIX R")) && (foundRight == false) && (position != ""))
                        {
                            string[] splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                            if (Convert.ToDouble(splitData[4]) < 500) continue; //stop small fixations
                            rightEyePosX = Convert.ToDouble(splitData[5]);
                            rightEyePosY = Convert.ToDouble(splitData[6]);
                            foundRight = true;
                        }
                        if ((line.Contains("EFIX L")) && (foundLeft == false) && (position != ""))
                        {
                            string[] splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                            if (Convert.ToDouble(splitData[4]) < 500) continue; //stop small fixations
                            leftEyePosX = Convert.ToDouble(splitData[5]);
                            leftEyePosY = Convert.ToDouble(splitData[6]);
                            foundLeft = true;
                        }
                        if ((foundLeft == true) && (foundRight == true) && (position != ""))
                        {
                            if (position == "Center")
                            {
                                Center.Initilize(rightEyePosX, rightEyePosY, leftEyePosX, leftEyePosY);
                                foundLeft = false;
                                foundRight = false;
                                position = "";
                                continue;
                            }
                            if (position == "Top")
                            {
                                Top.Initilize(rightEyePosX, rightEyePosY, leftEyePosX, leftEyePosY);
                                foundLeft = false;
                                foundRight = false;
                                position = "";
                                continue;
                            }
                            if (position == "Right")
                            {
                                Right.Initilize(rightEyePosX, rightEyePosY, leftEyePosX, leftEyePosY);
                                foundLeft = false;
                                foundRight = false;
                                position = "";
                                continue;
                            }
                            if (position == "Bottom")
                            {
                                Bottom.Initilize(rightEyePosX, rightEyePosY, leftEyePosX, leftEyePosY);
                                foundLeft = false;
                                foundRight = false;
                                position = "";
                                continue;
                            }
                            if (position == "Left")
                            {
                                Left.Initilize(rightEyePosX, rightEyePosY, leftEyePosX, leftEyePosY);
                                foundLeft = false;
                                foundRight = false;
                                position = "";
                                continue;
                            }
                        }
                    }
                }
                inputFile.Close();

                if (Center.getInitalized())
                {
                    resultsTB.AppendText(Environment.NewLine + "CENTER:" + Environment.NewLine);
                    PrintPositionData(Center);
                }
                if (Top.getInitalized())
                {
                    resultsTB.AppendText(Environment.NewLine + "TOP:" + Environment.NewLine);
                    PrintPositionData(Top);
                }
                if (Right.getInitalized())
                {
                    resultsTB.AppendText(Environment.NewLine + "RIGHT:" + Environment.NewLine);
                    PrintPositionData(Right);
                }
                if (Bottom.getInitalized())
                {
                    resultsTB.AppendText(Environment.NewLine + "BOTTOM:" + Environment.NewLine);
                    PrintPositionData(Bottom);
                }
                if (Left.getInitalized())
                {
                    resultsTB.AppendText(Environment.NewLine + "LEFT:" + Environment.NewLine);
                    PrintPositionData(Left);
                }
                if (resultsTB.Text == "")
                {
                    resultsTB.Text = "There Is A Problem With The Input Data";
                }

            }
            //Exception Handelining 
            catch (FileNotFoundException exseption)
            {
                MessageBox.Show(exseption.Message);
                DragAndDropTB.Text = string.Empty;
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Please Enter A File");
                return;
            }
            catch (UnauthorizedAccessException exseption)
            {
                MessageBox.Show(exseption.Message);
                DragAndDropTB.Text = string.Empty;
                return;
            }
        }
       
        private void DragAndDropTB_TextChanged(object sender, EventArgs e)
        {
            resultsTB.Text = "";
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

        //same thing but with PDF report
        private void Button1_Click(object sender, EventArgs e)
        {
            resultsTB.Text = "";
            try
            {
                string fileDirectory = DragAndDropTB.Text;
                string fileType = fileDirectory.Substring(fileDirectory.LastIndexOf('.'));

                //Checks for the correct file type
                if ((fileType != ".asc") && (fileType != ".txt"))
                {
                    MessageBox.Show("Please Enter Correct File Type\n(.edf converted to .asc)");
                    DragAndDropTB.Text = string.Empty;
                    return;
                }
                string sameFolder = fileDirectory.Substring(0, fileDirectory.LastIndexOf('.'));

                //find input file directory and create a file stream
                FileStream inputFile = new FileStream(fileDirectory, FileMode.Open, FileAccess.Read);

                double rightEyePosX = 0;
                double rightEyePosY = 0;
                double leftEyePosX = 0;
                double leftEyePosY = 0;
                bool foundRight = false;
                bool foundLeft = false;
                EyePositions Center = new EyePositions();
                EyePositions Top = new EyePositions();
                EyePositions Right = new EyePositions();
                EyePositions Bottom = new EyePositions();
                EyePositions Left = new EyePositions();
                //Read Line by Line
                using (var streamReader = new StreamReader(inputFile))// helps with line by line
                {
                    string position = "";
                    string line;//helps with line by line
                    while ((line = streamReader.ReadLine()) != null)//helps with line by line
                    {
                        //find where target is
                        if (line.Contains("SHOW_CENTER")) position = "Center";
                        if (line.Contains("SHOW_TOP")) position = "Top";
                        if (line.Contains("SHOW_RIGHT")) position = "Right";
                        if (line.Contains("SHOW_BOTTOM")) position = "Bottom";
                        if (line.Contains("SHOW_LEFT")) position = "Left";
                        if (position == "") continue;

                        //Find End Saccade data line
                        // Data format: ESACC <Eye Tracked> <STime> <ETime> <Dur> <Sxpos> <Sypos> <Expos> <Eypos> <Ampl> <PeakVel> 
                        if ((line.Contains("EFIX R")) && (foundRight == false) && (position != ""))
                        {
                            string[] splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                            if (Convert.ToDouble(splitData[4]) < 500) continue; //stop small fixations
                            rightEyePosX = Convert.ToDouble(splitData[5]);
                            rightEyePosY = Convert.ToDouble(splitData[6]);
                            foundRight = true;
                        }
                        if ((line.Contains("EFIX L")) && (foundLeft == false) && (position != ""))
                        {
                            string[] splitData = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                            if (Convert.ToDouble(splitData[4]) < 500) continue; //stop small fixations
                            leftEyePosX = Convert.ToDouble(splitData[5]);
                            leftEyePosY = Convert.ToDouble(splitData[6]);
                            foundLeft = true;
                        }
                        if ((foundLeft == true) && (foundRight == true) && (position != ""))
                        {
                            if (position == "Center")
                            {
                                Center.Initilize(rightEyePosX, rightEyePosY, leftEyePosX, leftEyePosY);
                                foundLeft = false;
                                foundRight = false;
                                position = "";
                                continue;
                            }
                            if (position == "Top")
                            {
                                Top.Initilize(rightEyePosX, rightEyePosY, leftEyePosX, leftEyePosY);
                                foundLeft = false;
                                foundRight = false;
                                position = "";
                                continue;
                            }
                            if (position == "Right")
                            {
                                Right.Initilize(rightEyePosX, rightEyePosY, leftEyePosX, leftEyePosY);
                                foundLeft = false;
                                foundRight = false;
                                position = "";
                                continue;
                            }
                            if (position == "Bottom")
                            {
                                Bottom.Initilize(rightEyePosX, rightEyePosY, leftEyePosX, leftEyePosY);
                                foundLeft = false;
                                foundRight = false;
                                position = "";
                                continue;
                            }
                            if (position == "Left")
                            {
                                Left.Initilize(rightEyePosX, rightEyePosY, leftEyePosX, leftEyePosY);
                                foundLeft = false;
                                foundRight = false;
                                position = "";
                                continue;
                            }
                        }
                    }
                }
                inputFile.Close();

                if (Center.getInitalized())
                {
                    resultsTB.AppendText(Environment.NewLine + "CENTER:" + Environment.NewLine);
                    PrintPositionData(Center);
                }
                if (Top.getInitalized())
                {
                    resultsTB.AppendText(Environment.NewLine + "TOP:" + Environment.NewLine);
                    PrintPositionData(Top);
                }
                if (Right.getInitalized())
                {
                    resultsTB.AppendText(Environment.NewLine + "RIGHT:" + Environment.NewLine);
                    PrintPositionData(Right);
                }
                if (Bottom.getInitalized())
                {
                    resultsTB.AppendText(Environment.NewLine + "BOTTOM:" + Environment.NewLine);
                    PrintPositionData(Bottom);
                }
                if (Left.getInitalized())
                {
                    resultsTB.AppendText(Environment.NewLine + "LEFT:" + Environment.NewLine);
                    PrintPositionData(Left);
                }
                if (resultsTB.Text == "")
                {
                    resultsTB.Text = "There Is A Problem With The Input Data";
                }
                else
                {
                    string pdfPath = sameFolder + "Report.pdf";
                    Document doc = new Document();
                    PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
                    doc.Open();
                    doc.Add(new Paragraph(resultsTB.Text));
                    doc.Close();
                }

            }
            //Exception Handelining 
            catch (FileNotFoundException exseption)
            {
                MessageBox.Show(exseption.Message);
                DragAndDropTB.Text = string.Empty;
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Please Enter A File");
                return;
            }
            catch (UnauthorizedAccessException exseption)
            {
                MessageBox.Show(exseption.Message);
                DragAndDropTB.Text = string.Empty;
                return;
            }
        }
    }
}