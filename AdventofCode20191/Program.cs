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
            Fuel f = new Fuel("../../../mass.txt");
            Console.WriteLine(f.GetFuel());    

            Console.ReadKey();
        }
    }
}
