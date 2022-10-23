using System;
using System.Collections.Generic;
using System.Text;

namespace TubesSolver
{
    // Tube holds the individual colors and their volumes within the tube stack
    class Tube
    {
        public int tubeID { get; private set; } // Unique tubeID for each tube

        public decimal freeSpace { get; private set; } // Available space not taken up by color liquid
        private decimal segments { get; set; } // Total number of segments in the tube
        private Stack<string> tubeStack; // Stack holding order within the tube, FILO
        private Dictionary<char, decimal> volumeByColor; // Holds the color and the volume of that color within the tube

        public Tube(int tubeId, int numOfSetments)
        {
            tubeID = tubeId;
            segments = numOfSetments;
            freeSpace = segments;
            tubeStack = new Stack<string>();
            volumeByColor = new Dictionary<char, decimal>();
        }

        public Tube(int tubeId, int numOfSetments, List<string> colorOrderOfTube)
        {
            tubeID = tubeId;
            segments = numOfSetments;
            volumeByColor = new Dictionary<char, decimal> ();
            tubeStack = new Stack<string>();


            // Calculating freeSpace, setting volumeByColor and tubeOrder at the same time
            freeSpace = numOfSetments;

            // Setting volume by color order based on tube stack
            foreach(string colorData in colorOrderOfTube)
            {
                freeSpace -= colorData[1] - '0';
                volumeByColor.Add(colorData[0], colorData[1]);
                tubeStack.Push(colorData);
            }
        }

        // Return floart score of individual tube based on heuristic function
        //  - Tube heuristic: Greatest number of like colors over the total tube segments
        public decimal CalcScore()
        {
            decimal greatestVolume = 0;

            foreach (string colorVolume in tubeStack)
            {
                greatestVolume = Math.Max(colorVolume[1] - '0', greatestVolume); // - '0' to convert char to int
            }

            return greatestVolume / segments;

            // Alternater overall heuristic idea:
            // Count of "rack code" string
        }
         // Returns a unique string of the tube colors with tube identifier
        public string GetTubeCode()
        {
            string returnStr = "_" + tubeID + "_";

            foreach(string colorVolume in tubeStack)
            {
                returnStr += colorVolume;
            }

            return returnStr;
        }

        // Returns the color and volume of the next liquid on the tube stack
        public string PeekColor()
        {
            return tubeStack.Peek();
        }

        // Returns a string of the current contents of the tube
        public string Print()
        {
            string contentsOfTube = "|";

            if(freeSpace > 0)
            {
                for(int i = 0; i < freeSpace; i++)
                {
                    contentsOfTube += " " + "|";
                }
            }

            foreach (string colorVolume in tubeStack)
            {
                for(int i = 0; i < colorVolume[1] - '0'; i++)
                {
                    contentsOfTube += colorVolume[0] + "|";
                }
            }
            contentsOfTube += ">";

            return contentsOfTube;
        }
    }
}
