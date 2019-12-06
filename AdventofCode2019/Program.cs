using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventofCode2019
{
    class Program
    {        
        static void Main(string[] args)
        {
            //day 1
            Fuel f = new Fuel("../../../input/mass.txt");
            Console.WriteLine("day 1 solution: " + f.GetFuel());

            //day2
            IntcodeComputer ic1 = new IntcodeComputer("../../../input/gravity_assist_programm.txt");
            ic1.Alarm1202();
            int[] intcode = ic1.Run();
            int nounverb = -1;
            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    ic1 = new IntcodeComputer("../../../input/gravity_assist_programm.txt");
                    ic1.SetNounVerb(noun, verb);
                    int[] check = ic1.Run();
                    if (check[0] == 19690720)
                    {
                        nounverb = 100 * noun + verb;
                    }
                }
            }
            Console.WriteLine("day 2 solution: {0} {1}", intcode[0], nounverb);

            //day3
            Wires w = new Wires("../../../input/wires.txt");
            Console.WriteLine("day 3 solution: {0} {1}", w.GetManhattendistance(), w.GetFewestStepsToIntersection());

            //day4
            Password6D p = new Password6D(265275, 781584);
            Console.WriteLine("day 4 solution: {0} {1}", p.GetPasswordsAmountCrit1(), p.GetPasswordsAmountCrit2());

            //day5
            Console.WriteLine("day 5 solution: ");
            IntcodeComputer ic2 = new IntcodeComputer("../../../input/TEST.txt");
            ic2.Run();
            Console.Write("\n");

            //day6
            //OrbitMap om = new OrbitMap("../../../input/orbit_map.txt");
            OrbitMap om = new OrbitMap(new string[] { "COM)B",
"B)C",
"C)D",
"D)E",
"E)F",
"B)G",
"G)H",
"E)J",
"J)K",
"K)L"});
            Console.WriteLine("day 6 solution: {0}",om.GetOrbitSum());


            Console.ReadKey();
        }
    }
}
