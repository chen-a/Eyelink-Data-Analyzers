using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyelinkFileAnalizer
{
    class EyePositions
    {
        double rightEyeXPos;
        double rightEyeYPos;
        double leftEyeXPos;
        double leftEyeYPos;
        bool initalized = false;
        public EyePositions()
        { 
            rightEyeXPos = 0;
            rightEyeYPos = 0;
            leftEyeXPos = 0;
            leftEyeYPos = 0;
            initalized = false;
            return;
        }

        public void Initilize(double rightEyeXPosToAdd, double rightEyeYPosToAdd, double leftEyeXPosToAdd, double leftEyeYPosToAdd)
        {
            rightEyeXPos = rightEyeXPosToAdd;
            rightEyeYPos = rightEyeYPosToAdd;
            leftEyeXPos = leftEyeXPosToAdd;
            leftEyeYPos = leftEyeYPosToAdd;
            initalized = true;
            return;
        }

        public double getRightEyeXPos() { return rightEyeXPos; }
        public double getRightEyeYPos() { return rightEyeYPos; }
        public double getLeftEyeXPos() { return leftEyeXPos; }
        public double getLeftEyeYPos() { return leftEyeYPos; }
        public bool getInitalized() { return initalized; }
    }
}
