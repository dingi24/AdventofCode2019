using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventofCode2019
{
    class Wires
    {
        private string[] wirePath1Dir, wirePath2Dir;
        private int[] wirePath1Dis, wirePath2Dis;
        private HashSet<string> wire1Coords,wire2Coords;
        private List<string> intersection;

        public Wires(string url)
        {
            try
            {
                StreamReader sr = new StreamReader(url);
                string s = sr.ReadToEnd();
                s = s.Trim(' ', '\r', '\t');
                string[] data = s.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                wirePath1Dir = data[0].Split(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' ,}, StringSplitOptions.RemoveEmptyEntries);    
                wirePath2Dir = data[1].Split(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' ,}, StringSplitOptions.RemoveEmptyEntries);               

             
                string[] help = data[0].Split(new char[] { 'R', 'U', 'D', 'L', ',' }, StringSplitOptions.RemoveEmptyEntries);
                wirePath1Dis = new int[help.Length];
                for (int i = 0; i < help.Length; i++)
                {
                    wirePath1Dis[i] = int.Parse(help[i]);
                }               
                help = data[1].Split(new char[] { 'R', 'U', 'D', 'L',',' }, StringSplitOptions.RemoveEmptyEntries);
                wirePath2Dis = new int[help.Length];
                for (int i = 0; i < help.Length; i++)
                {
                    wirePath2Dis[i] = int.Parse(help[i]);
                }


                wire1Coords = CreateCoordsSet(wirePath1Dir, wirePath1Dis);
                wire2Coords = CreateCoordsSet(wirePath2Dir, wirePath2Dis);

                intersection = new List<string>();
                foreach (var c1 in wire1Coords)
                {
                    if (wire2Coords.Contains(c1))
                    {
                        intersection.Add(c1);
                    }
                }

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public int GetManhattendistance()
        {
            int closest = Int32.MaxValue;
            foreach (var c in intersection)
            {
                string[] s = c.Split(',');
                if((Math.Abs(int.Parse(s[0]))+ Math.Abs(int.Parse(s[1]))) < closest)
                {
                    closest = Math.Abs(int.Parse(s[0])) + Math.Abs(int.Parse(s[1]));
                }
            }
            return closest;         
        }
        public int GetFewestStepsToIntersection()
        {
            int fewestSteps = Int32.MaxValue;
            foreach(var i in intersection)
            {
                int stepsToI = StepsToIntersection(wirePath1Dir, wirePath1Dis, i) + StepsToIntersection(wirePath2Dir, wirePath2Dis, i);
                if (stepsToI < fewestSteps)
                {
                    fewestSteps = stepsToI;
                }
            }
            return fewestSteps;
        }
        private int StepsToIntersection(string[] dir,int[] dis,string intersection)
        {
            bool _continue = true;
            int x = 0,y=0,steps=0;
            for (int i = 0; i < dis.Length&&_continue; i++)
            {
                switch (dir[i])
                {
                    case "R":
                        for (int j = 1; j <= dis[i]; j++)
                        {
                            if (((x + j) + "," + y) == intersection)
                            {
                                _continue = false;
                                steps += j;
                            }
                        }
                        x += dis[i];
                        break;
                    case "L":
                        for (int j = 1; j <= dis[i]; j++)
                        {
                            if (((x - j) + "," + y) == intersection)
                            {
                                _continue = false;
                                steps += j;
                            }
                        }
                        x -= dis[i];
                        break;
                    case "U":
                        for (int j = 1; j <= dis[i]; j++)
                        {
                            if ((x + "," + (y+j)) == intersection)
                            {
                                _continue = false;
                                steps += j;
                            }
                        }
                        y += dis[i];
                        break;
                    case "D":
                        for (int j = 1; j <= dis[i]; j++)
                        {
                            if ((x + "," + (y- j)) == intersection)
                            {
                                _continue = false;
                                steps += j;
                            }
                        }
                        y -= dis[i];
                        break;
                }
                if (_continue)
                {
                    steps += dis[i];
                }
            }
            return steps;
        }
        public void PrintArrays()
        {
            //for(int i = 0; i < wirePath1Dis.Length; i++)
            //{
            //    Console.WriteLine("{0} {1} {2} {3}\n",wirePath1Dir[i], wirePath1Dis[i], wirePath2Dir[i], wirePath2Dis[i]);
            //}
            for (int i = 0; i < wirePath1Dir.Length; i++)
            {
                Console.WriteLine("{0} {1} \n",i, wirePath1Dir[i]);
            }
            Console.WriteLine("{0} {1} {2} {3}\n", wirePath1Dir.Length, wirePath1Dis.Length, wirePath2Dir.Length, wirePath2Dis.Length);
        }
        private HashSet<string> CreateCoordsSet(string[] dir, int[] dis)
        {
            int preX = 0, preY = 0;
            HashSet<string> hs = new HashSet<string>();
            for (int i = 0; i < dis.Length; i++)
            {
                switch (dir[i])
                {
                    case "R":
                        for (int j = 1; j <= dis[i]; j++)
                        {
                            hs.Add((preX + j) + "," + preY);
                        }
                        preX += dis[i];
                        break;
                    case "L":
                        for (int j = 1; j <= dis[i]; j++)
                        {
                            hs.Add((preX - j) + "," + preY);
                        }
                        preX -= dis[i];
                        break;
                    case "U":
                        for (int j = 1; j <= dis[i]; j++)
                        {
                            hs.Add(preX + "," + (preY + j));
                        }
                        preY += dis[i];
                        break;
                    case "D":
                        for (int j = 1; j <= dis[i]; j++)
                        {
                            hs.Add(preX + "," + (preY - j));
                        }
                        preY -= dis[i];
                        break;
                }
            }
            return hs;
        }
    }
}


