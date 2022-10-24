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
        const int TUBES_IN_RACK = 5;

        static void Main(string[] args)
        {
            // Example Rack Code: 0BBRR_1B_2BGGG_3_4GRR
            Rack rack = new Rack(TUBE_SEGMENTS, "0BBRR_1B_2BGGG_3_4GRR");

            //Console.WriteLine(rack.CalcScore());

            //rack = new Rack(TUBE_SEGMENTS, "0BBB_1_2RRR");

            //Console.WriteLine(rack.CalcScore());

            //rack = new Rack(TUBE_SEGMENTS, "0_1BBB_2RRR");

            //Console.WriteLine(rack.CalcScore());

            //rack = new Rack(TUBE_SEGMENTS, "0RRR_1BBB_2");

            //Console.WriteLine(rack.CalcScore());


            SolverController solverController = new SolverController();

            // Adding initial state as previous state to solver controller
            // This will let us exclude the initial state from future calculations and stop looping
            solverController.AddPreviousState(rack.currentState);

            string initialState = rack.currentState;
            string highestCode = "";
            decimal highestScore = 0.0M;
            int solutionIter = 0;
            int iterTracking = 1;

            solverController.PushUexploredState(rack.currentState, "NULL", "NULL"); // Initial state has no from or to pour

            bool solutionFound = false;

            while (solverController.IsUnexploredEmpty() == false && solutionFound == false)
            {
                // Holding popped string for pushing into explored state
                List<string> poppedState = solverController.PopUnexploredState();

                solverController.AddPreviousState(poppedState[0]);

                // Popping from unexplored list and setting result to new rack for testing
                string unexploredStateCode = poppedState[0];
                rack = new Rack(TUBE_SEGMENTS, unexploredStateCode);

                // Adding state to sequence list as path for backtracking
                solverController.PushToSequenceStack(poppedState);

                // Deadend is true if path has no valid children sequences to follow
                bool deadEnd = true;

                // Generating all the valid next states from the initial state
                for(int i = 0; i < TUBES_IN_RACK; i++)
                {
                    // Holding results of score and serialized code
                    string outCode = "";
                    decimal outScore = 0.0M;

                    for (int j = 0; j < TUBES_IN_RACK; j++)
                    {
                        if (i != j && rack.IsValidPour(i, j, ref outCode) && !solverController.IsAlreadySeenState(outCode))
                        {
                            deadEnd = false;

                            outScore = rack.CalcScore();

                            solverController.PushUexploredState(outCode, i.ToString(), j.ToString());
                            Console.WriteLine(iterTracking + ": " + outCode + "\t->" + outScore);

                            // Setting highest score for viewing later
                            if(outScore > highestScore)
                            {
                                highestScore = outScore;
                                highestCode = outCode;
                                solutionIter = iterTracking;
                            }

                            if (outScore == 1.0M) solutionFound = true; break;
                        }

                        if (solutionFound) break;

                        iterTracking++;
                    }
                }
                if (deadEnd) solverController.PopFromSequenceStack();
            }

            Console.WriteLine();
            Console.WriteLine("Finished:");
            Console.WriteLine("Iteration: " + solutionIter);
            Console.WriteLine("Highest Score: " + highestScore);
            Console.WriteLine("Highest Code: " + highestCode);

            Console.WriteLine("Steps for solution:");
            Console.WriteLine("Initial State: " + initialState);
            solverController.PrintPathSteps();
        }
    }
}
