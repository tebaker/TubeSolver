using System;
using System.Collections.Generic;
using System.Text;
namespace TubesSolver
{
    class Program
    {
        // TODO: Error check for "volume over max TUBE_SEGMENTS" in tube constructor

        const int TUBE_SEGMENTS = 4;
        const int TUBES_IN_RACK = 4;

        static void Main(string[] args)
        {
            // Setting list of tubes for rack
            List<Tube> tubeList = new List<Tube>()
            {
                new Tube(0, TUBE_SEGMENTS, new List<string>() { "R2", "B2" }),
                new Tube(1, TUBE_SEGMENTS, new List<string>() { "B1" }),
                new Tube(2, TUBE_SEGMENTS, new List<string>() { "G3", "B1" }),
                new Tube(3, TUBE_SEGMENTS, new List<string>() { }),
                new Tube(4, TUBE_SEGMENTS, new List<string>() { "R2", "G1" })
            };

            Rack rack = new Rack(tubeList);

            Console.WriteLine(rack.Print());

            Console.WriteLine(rack.CalcScore());

            Console.WriteLine(rack.GetRackCode());
        }
    }
}
