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
        public decimal segments { get; private set; } // Total number of segments in the tube
        private Stack<char> colorStack; // Stack holding order within the tube, FILO
        public decimal score; // Holding the score of the tube based on the calc score function

        public bool isFullSameColor;
        public bool isEmpty;

        public Tube(int tubeId, int numOfSetments, string colorOrderOfTube)
        {
            tubeID = tubeId;
            segments = numOfSetments;
            colorStack = new Stack<char>();
            score = -1.0M;
            isFullSameColor = false;
            isEmpty = false;

            // Calculating freeSpace, setting volumeByColor and tubeOrder at the same time
            freeSpace = numOfSetments - colorOrderOfTube.Length;

            // Setting volume by color order based on tube stack
            for(int i = 0; i < colorOrderOfTube.Length; i++)
            {
                colorStack.Push(colorOrderOfTube[i]);
            }
        }

        // Return floart score of individual tube based on heuristic function
        //  - Tube heuristic: Greatest number of like colors over the total tube segments
        public decimal CalcScore()
        {
            // Returning score of 1 for completely empty tube. This will help with checking win state
            if (freeSpace == segments)
            {
                isEmpty = true;
                return 1.0M;
            }
            Dictionary<char, int> volumeByColor = new Dictionary<char, int>();
            decimal score = 0.0M;

            foreach(char color in colorStack)
            {
                if(volumeByColor.ContainsKey(color) == true)
                {
                    volumeByColor[color]++;
                    score = Math.Max(score, volumeByColor[color]);
                }
                else
                {
                    volumeByColor.Add(color, 1);
                }
            }

            if (score / segments == 1.0M) isFullSameColor = true;

            return score / segments;
        }
         // Returns a unique string of the tube colors with tube identifier
        public string Serialize()
        {
            string returnStr = "_";

            foreach(char colorVolume in colorStack)
            {
                returnStr += Char.ToString(colorVolume);
            }

            return returnStr;
        }

        public bool IsEmpty()
        {
            if (freeSpace == segments) return true;
            return false;
        }

        // Returns the color on top of the tube stack
        // If multiple colors are stacked on top of one another, returns all like colors
        public string PeekTop()
        {
            string returnString = "";
            char previous = colorStack.Peek();
            foreach(char ch in colorStack)
            {
                if (previous == ch)
                {
                    returnString += ch;
                }
                else
                {
                    break;
                }
                previous = ch;
            }
            return returnString;
        }

        public void Push(char color)
        {
            colorStack.Push(color);
        }

        public char Pop()
        {
            char topOfColorStack = colorStack.Peek();
            colorStack.Pop();
            return topOfColorStack;
        }

        // Returns true if the stack has enough free space for the the passed in color
        public bool CouldContain(string colorVolume)
        {
            return colorVolume.Length <= freeSpace;
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
                contentsOfTube += color + "|";
            }
            contentsOfTube += ">";

            return contentsOfTube;
        }
    }
}
