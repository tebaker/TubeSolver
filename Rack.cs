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
        public 

        public Rack(int segments, string serializedRackCode)
        {
            List<Tube> listOfTubes = Deserialize(segments, serializedRackCode);

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

        public string Serialize()
        {
            string returnCode = "";
            foreach (KeyValuePair<int, Tube> tube in rack)
            {
                returnCode += tube.Value.Serialize();
            }
            return returnCode;
        }

        // Will pour the top fluid contents from one tube into another
        public bool Pour(Tube tube1, Tube tube2 )
        {
            return true;
        }
        // Example rack code: _0_B2R2_1_B1_2_B1G3_3__4_G1R2
        //      After split: [[0], [B2R2], [1], [
        // Deserializing the rack code from string to Tube / Rack classes
        public List<Tube> Deserialize(int segments, string rackCode)
        {
            string[] rackCodeSplit = rackCode.Split('_');

            //foreach (string str in rackCodeSplit) Console.WriteLine(str);

            //Console.WriteLine(rackCode);

            List<Tube> holdTubes = new List<Tube>();

            foreach(string tubeCode in rackCodeSplit)
            {
                int tubeId = tubeCode[0] - '0';
                string colorVolumes = tubeCode.Substring(1);

                Tube tube = new Tube(tubeId, segments, colorVolumes);
                holdTubes.Add(tube);
                //Console.WriteLine(listStrElements[i]);
            }
            return holdTubes;
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
