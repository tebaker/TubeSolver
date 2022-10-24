using System;
using System.Collections.Generic;
using System.Text;

namespace TubesSolver
{
    // Solver Controller will hold a hashset of a unique rack state.
    // Solver Controller will help determine most approproate next step by
    // determining best possible next (valid) move and storing that on a sequence stack
    // to be used to backtrack in case solver gets stuck in a dead end.

    // Loops or repeated patterns can be avoided by making use of the previousStates hash set
    // and if the solver follows a path toward a dead end, it can backtrack until it finds a
    // state where there is a valid next move.

    // The sequence stack will act as step-by-step for solving the problem

    class SolverController
    {
        HashSet<string> previousStates;    // holding all previous state strings
        Stack<List<string>> sequenceStack; // Holding sequence of previous state strings.
                                           //     String in list as index 0 is raw state string,
                                           //     Index 1 is tube-to-tube pour that resulted in state.

        Stack<List<string>> unexploredStates; // Holding valid string state for the unexplored states that are possible
                                              //    based on our current state
                                              // Index 0 holds the unexplored state code
                                              // Also holding at index 1 and 2 the from tube and to tube to make the state possible

        public SolverController()
        {
            previousStates = new HashSet<string>(); // Set of states already explored
            sequenceStack = new Stack<List<string>>(); // Pair of moves required to get to current state, rack code
            unexploredStates = new Stack<List<string>>();
        }

        // Adding previous state to already explored hash set
        public void AddPreviousState(string previousState)
        {
            previousStates.Add(previousState);
        }

        public bool IsAlreadySeenState(string stateCode)
        {
            return previousStates.Contains(stateCode);
        }

        public void PushUexploredState(string unexploredStateCode, string fromTube, string toTube)
        {
            unexploredStates.Push(new List<string>() { unexploredStateCode, fromTube, toTube });
        }

        public string PopUnexploredState()
        {
            string unexploredStatusCode = unexploredStates.Peek()[0];

            previousStates.Add(unexploredStatusCode);
         
            unexploredStates.Pop();

            return unexploredStatusCode;
        }

        public bool IsUnexploredEmpty()
        {
            if(unexploredStates.Count == 0) {
                return true;
            }
            return false;
        }
    }
}
