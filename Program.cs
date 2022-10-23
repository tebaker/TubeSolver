using System;
using System.Collections.Generic;
using System.Text;
namespace TubesSolver
{
    class Program
    {
        // TODO: Error check for "volume over max TUBE_SEGMENTS" in tube constructor

        /*
         Algorithm:
            1) Look at current tubes:
                a. Determine best move by evaluating all possible (valid) moves based off current game state.
                    NOTE: A move is valid if:
                        * The volume of the top layer of 'water' fits into the empty space of another tube
                        * The state caused by the move hasn't already been explored
                b. Put each move generated this way onto a pending move stack and select move with best score
            2) 
         
         
         */


        const int TUBE_SEGMENTS = 4;
        const int TUBES_IN_RACK = 4;

        static void Main(string[] args)
        {
            // Setting list of tubes for rack
            List<Tube> tubeList = new List<Tube>()
            {
                new Tube(0, TUBE_SEGMENTS, "R2B2"),
                new Tube(1, TUBE_SEGMENTS, "B1"),
                new Tube(2, TUBE_SEGMENTS, "G3B1"),
                new Tube(3, TUBE_SEGMENTS, ""),
                new Tube(4, TUBE_SEGMENTS, "R2G1")
            };

            Rack rack = new Rack(tubeList);

            Console.WriteLine(rack.Print());

            Console.WriteLine(rack.CalcScore());

            rack.DeserializeRackCode(TUBE_SEGMENTS, rack.GetRackCode());

            // We're going to loop until rack score is 1; meaning we've solved the program.
        }
    }
}
