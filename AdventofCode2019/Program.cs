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
            Fuel f = new Fuel("../../../mass.txt");
            Console.WriteLine("day 1 solution: "+f.GetFuel());

            //day2
            IntcodeComputer a = new IntcodeComputer("../../../gravity_assist_programm.txt");
            a.Alarm1202();
            int[] intcode = a.Run();
            int nounverb = -1;
            for(int noun = 0; noun <= 99; noun++)
            {
                for(int verb =0;verb<=99; verb++)
                {
                    a = new IntcodeComputer("../../../gravity_assist_programm.txt");
                    a.SetNounVerb(noun, verb);
                    int[] check = a.Run();
                    if(check[0]== 19690720)
                    {
                        nounverb = 100 * noun + verb;
                    }
                }
            }
            Console.WriteLine("day 2 solution: {0} {1}" ,intcode[0],nounverb);

            //day3
            Wires w = new Wires("../../../wires.txt");
            Console.WriteLine("day 2 solution: {0} {1}",w.GetManhattendistance(),w.GetFewestStepsToIntersection());

            Console.ReadKey();
        }
    }
}
