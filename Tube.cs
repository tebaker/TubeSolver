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
        private Stack<char> colorStack; // Stack holding order within the tube, FILO
        private Dictionary<char, int> volumeByColor; // Holds the color and the volume of that color within the tube

        public Tube(int tubeId, int numOfSetments, string colorOrderOfTube)
        {
            tubeID = tubeId;
            segments = numOfSetments;
            volumeByColor = new Dictionary<char, int> ();
            colorStack = new Stack<char>();


            // Calculating freeSpace, setting volumeByColor and tubeOrder at the same time
            freeSpace = numOfSetments;

            // Setting volume by color order based on tube stack
            for(int i = 0; i < colorOrderOfTube.Length; i += 2)
            {
                freeSpace -= colorOrderOfTube[i+1] - '0';
                volumeByColor.Add(colorOrderOfTube[i], colorOrderOfTube[i+1] - '0');
                colorStack.Push(colorOrderOfTube[i]);
            }
        }

        // Return floart score of individual tube based on heuristic function
        //  - Tube heuristic: Greatest number of like colors over the total tube segments
        public decimal CalcScore()
        {
            decimal greatestVolume = 0;

            foreach (char colorVolume in colorStack)
            {
                greatestVolume = Math.Max(colorVolume - '0', greatestVolume); // - '0' to convert char to int
            }

            return greatestVolume / segments;

            // Alternater overall heuristic idea:
            // Count of "rack code" string
        }
         // Returns a unique string of the tube colors with tube identifier
        public string Serialize()
        {
            string returnStr = "_" + tubeID;

            foreach(char colorVolume in colorStack)
            {
                returnStr += Char.ToString(colorVolume);
            }

            return returnStr;
        }

        // Returns the color and volume of the next liquid on the tube stack
        public char PeekColor()
        {
            return colorStack.Peek();
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

            foreach (char color in colorStack)
            {
                int loopAmount = volumeByColor[color];

                for(int i = 0; i < loopAmount; i++)
                {
                    contentsOfTube += Char.ToString(color) + "|";
                }
            }
            contentsOfTube += ">";

            return contentsOfTube;
        }
    }
}
