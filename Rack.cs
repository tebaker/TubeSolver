using System;
using System.Collections.Generic;
using System.Text;

namespace TubesSolver
{
    // Rack holds all the Tubes
    class Rack
    {
        public int tubesInRack { get; private set; }
        private Dictionary<int, Tube> rack;

        public Rack(List<Tube> listOfTubes)
        {
            tubesInRack = listOfTubes.Count;
            rack = new Dictionary<int, Tube>();

            foreach (Tube tube in listOfTubes)
            {
                rack.Add(tube.tubeID, tube);
            }
        }

        public decimal CalcScore()
        {
            decimal returnScore = 0;

            foreach (KeyValuePair<int, Tube> tube in rack)
            {
                returnScore += tube.Value.CalcScore();
            }

            return returnScore / tubesInRack;
        }

        public string GetRackCode()
        {
            string returnCode = "";
            foreach (KeyValuePair<int, Tube> tube in rack)
            {
                returnCode += tube.Value.GetTubeCode();
            }
            return returnCode;
        }

        // Will pour the top fluid contents from one tube into another
        public bool Pour(Tube tube1, Tube tube2 )
        {
            return true;
        }

        // Deserializing the rack code from string to Tube / Rack classes
        public Rack DeserializeRackCode(int segments, string rackCode)
        {
            int numOfTubes = 0;
            string[] listStrElements = rackCode.Split('_');

            // Example Rack Code: _0_B2R2_1_B1_2_B1G3_3__4_G1R2
            // * _{num}_ => tubeID
            // * {CHAR VOLUME} => character of color, volume of 'water'
            for (int i = 0; i < listStrElements.Length; i++)
            {
                Console.WriteLine(listStrElements[i]);
            }

            return new Rack(new List<Tube>());
        }

        // Returns string of contents of every tube
        public string Print()
        {
            string contentsOfRack = "";

            foreach(KeyValuePair<int, Tube> tube in rack)
            {
                contentsOfRack += tube.Key + ": " + tube.Value.Print() + "\n";
            }

            return contentsOfRack;
        }
    }
}
