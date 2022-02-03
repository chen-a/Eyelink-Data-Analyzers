using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyelinkFileAnalizer
{
    class Saccade
    {
        string eyeTracked;
        double startTime;
        double endTime;
        double duration;
        double startXPosition;
        double startYPosition;
        double endXPosition;
        double endYPosition;
        double amplitude;
        double peakVelocity;
        double averageVelocity;
        string direction;

        public Saccade(string eyeTrackedToAdd, double startTimeToAdd, double endTimeToAdd, double durationToAdd, double startXPositionToAdd,
                double startYPositionToAdd, double endXpostionToAdd, double endYPositionToAdd, double amplitudeToAdd, double peakVelocityToAdd,
                 double averageVelocityToAdd, string directionToAdd)
        {
            eyeTracked = eyeTrackedToAdd;
            startTime = startTimeToAdd;
            endTime = endTimeToAdd;
            duration = durationToAdd;
            startXPosition = startXPositionToAdd;
            startYPosition = startYPositionToAdd;
            endXPosition = endXpostionToAdd;
            endYPosition = endYPositionToAdd;
            amplitude = amplitudeToAdd;
            peakVelocity = peakVelocityToAdd;
            averageVelocity = averageVelocityToAdd;
            direction = directionToAdd;
        }

        public double getAverageVelocity() { return averageVelocity; }
        public double getStartTime() { return startTime; }
        public double getEndTime() { return endTime; }
        public double getPeakVelocity() { return peakVelocity; }
        public double getAmplitude() { return amplitude; }
    }
}
