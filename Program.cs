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

            // Adding initial state as previous state to solver controller
            // This will let us exclude the initial state from future calculations and stop looping
            solverController.AddPreviousState(rack.currentState);

            // Holding results of score and serialized code
            string outCode = "";
            decimal outScore = 0.0M;

            solverController.PushUexploredState(rack.currentState, "NULL", "NULL"); // Initial state has no from or to pour

            while(solverController.IsUnexploredEmpty() == false)
            {
                // Popping from unexplored list and setting result to new rack for testing
                string unexploredStateCode = solverController.PopUnexploredState();
                rack = new Rack(TUBE_SEGMENTS, unexploredStateCode);

                // Generating all the valid next states from the initial state
                for(int i = 0; i < TUBES_IN_RACK; i++)
                {
                    for(int j = 0; j < TUBES_IN_RACK; j++)
                    {
                        if (i != j && rack.IsValidPour(i, j, ref outCode, ref outScore) && !solverController.IsAlreadySeenState(outCode))
                        {
                            solverController.PushUexploredState(outCode, i.ToString(), j.ToString());
                            Console.WriteLine(outCode + "\t->" + outScore);
                            if (rack.IsWinState()) break;
                        }
                    }
                }
            }

            Console.WriteLine("DIT ITTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
            Console.WriteLine(outCode);


        }
    }
}
