using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace AdventofCode2019
{
    class Fuel
    {
        private int[] mass;
        private int fuel;

        public Fuel(string url)
        {
            try
            {
                StreamReader sr = new StreamReader(url);
                string s = sr.ReadToEnd();
                string[] data = s.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                mass = new int[data.Length];
                fuel = 0;

                for (int i = 0; i < data.Length; i++)
                {
                    mass[i] = int.Parse(data[i]);
                    fuel += CalculateFuel(mass[i]);
                }
                
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public int GetFuel()
        {
            return fuel;
        }
        private int CalculateFuel(int mass)
        {
            int fuel = 0;
            while((Convert.ToInt32(Math.Floor(mass / 3.0)) - 2) > 0)
            {
                fuel += mass = Convert.ToInt32(Math.Floor(mass / 3.0)) - 2;               
            }
            return fuel;
        }
    }
}
