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
        HashSet<string> previousStates; // holding all previous state strings
        Stack<List<string>> sequenceStack; // Holding sequence of previous state strings.
                                           // String in list as index 0 is raw state string,
                                           // Index 1 is tube-to-tube pour that resulted in state.

        public SolverController()
        {
            previousStates = new HashSet<string>(); // Set of states already explored
            sequenceStack = new Stack<List<string>>(); // Pair of moves required to get to current state, rack code
        }

        public void AddPreviousState(string previousState)
        {
            previousStates.Add(previousState);
        }
    }
}
