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
        public string currentState;

        public Rack(int segments, string serializedRackCode)
        {
            currentState = serializedRackCode;
            List<Tube> listOfTubes = Deserialize(segments, serializedRackCode);

            tubesInRack = listOfTubes.Count;
            rack = new Dictionary<int, Tube>();

            foreach (Tube tube in listOfTubes)
            {
                rack.Add(tube.tubeID, tube);
            }
        }

        // Calculates the store of the game state based on the score of each individual tube over the total number of tubes.
        public decimal CalcScore()
        {
            decimal returnScore = 0.0M;

            foreach (KeyValuePair<int, Tube> tube in rack)
            {
                returnScore += tube.Value.CalcScore();
            }

            return returnScore / tubesInRack;
        }

        // Returns the string of current tubes so they can be hashed or deseerialized
        public string Serialize()
        {
            string returnCode = "";
            foreach (KeyValuePair<int, Tube> tube in rack)
            {
                returnCode += tube.Value.Serialize();
            }
            return returnCode.Substring(1); // Returning substring from index 1 to end to eliminate leading "_";
        }

        // Will pour the top fluid contents from one tube into another
        public bool IsValidPour(int tubeId1, int tubeId2, ref string outSwapCode)
        {
            // Checking if tube1 is empty
            if (rack[tubeId1].IsEmpty()) return false;

            bool valid = false;

            string topOfTube1 = rack[tubeId1].PeekTop();

            // Checking if the liquid from one tube could be contained by the other and vice versa
            if(rack[tubeId2].CouldContain(topOfTube1))
            {
                outSwapCode = GenerateTestSwapCode(tubeId1, tubeId2, topOfTube1);
                valid = true;
            }
            // Make sure we haven't seen the state previously (in main code, outside Rack)

            // Making sure we aren't pouring a full bottle into an empty bottle
            if (rack[tubeId1].isFullSameColor && rack[tubeId2].isEmpty) valid = false;

            return valid;
        }

        // Generates new serialized swapCode for check
        private string GenerateTestSwapCode(int tubeId1, int tubeId2, string topOfTube1)
        {
            // Pouring from tube 1 into tube 2
            for (int i = 0; i < topOfTube1.Length; i++)
            {
                rack[tubeId2].Push(topOfTube1[i]); // Pop returns the popped character
                rack[tubeId1].Pop(); // Popping from tube 1
            }

            // Grabbing serial from pour
            string testSwapCode = Serialize();

            // Pouring back from tube 2 to tube 1 to get things back to normal
            for (int i = 0; i < topOfTube1.Length; i++)
            {
                rack[tubeId1].Push(topOfTube1[i]); // Pop returns the popped character
                rack[tubeId2].Pop(); // Popping from tube 1
            }
            return testSwapCode;
        }

        // Pour will pour the top volume of liquid from one tube to another
        public string Pour(int tubeId1, int tubeId2)
        {
            string topOfTube1 = rack[tubeId1].PeekTop();

            for(int i = 0; i < topOfTube1.Length; i++)
            {
                rack[tubeId2].Push(topOfTube1[i]); // Pop returns the popped character
                rack[tubeId1].Pop(); // Popping from tube 1
            }

            return Serialize();
        }

        // Example rack code: 0B2R2_1B1_2B1G3_3_4G1R2
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
