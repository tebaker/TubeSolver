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
            // Example Rack Code: 0BBRR_1B_2BGGG_3_4GRR
            Rack rack = new Rack(TUBE_SEGMENTS, "0BBRR_1B_2BGGG_3_4GRR");

            SolverController solverController = new SolverController();

            Console.WriteLine(rack.Print());

            Console.WriteLine(rack.CalcScore()); // TODO need to check about this method. Not working as it should.


            // We're going to loop until rack score is 1; meaning we've solved the program.
        }
    }
}
