using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode2019
{
    class OrbitMap
    {
        private int OrbitSum;

        public OrbitMap(string [] orbitMap)
        {

            string[] mapData = orbitMap;
            string[,] OrbitData = new string[mapData.Length, 2];
            for (int i = 0; i < mapData.Length; i++)
            {
                OrbitData[i, 0] = mapData[i].Split(')')[0];
                OrbitData[i, 1] = mapData[i].Split(')')[1];
            }
            OrbitSum = 0;
            for(int i = 0; i < mapData.Length; i++)
            {
                Console.WriteLine(OrbitData[i, 0] + " " + OrbitData[i, 1]);
            }
            for (int i = 0; i < mapData.Length; i++)
            {
                int currentOrbitsum = 1;
                string nextOrbiter = OrbitData[i, 0];
                for (int j = 0; j < mapData.Length; j++)
                {
                    Console.WriteLine("{0} == {1}", OrbitData[j,1],nextOrbiter);

                    if (OrbitData[j, 1].Equals(nextOrbiter))
                    {
                        Console.WriteLine("yes");
                        currentOrbitsum++;
                        nextOrbiter = OrbitData[j, 0];
                        j = -1;
                    }
                }
                Console.WriteLine("Orbits of {0}: {1}",OrbitData[i,1],currentOrbitsum);
                OrbitSum += currentOrbitsum;
            }

        }
        public OrbitMap(string url)
        {
            StreamReader sr = new StreamReader(url);
            string s = sr.ReadToEnd();
            string[] mapData = s.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[,] OrbitData = new string[mapData.Length, 2];
            for (int i = 0; i < mapData.Length; i++)
            {
                OrbitData[i, 0] = mapData[i].Split(')')[0];
                OrbitData[i, 1] = mapData[i].Split(')')[1];
            }
            OrbitSum = 0;
            for (int i = 0; i < mapData.Length; i++)
            {
                int currentOrbitsum = 1;
                string nextOrbiter = OrbitData[i, 0];
                for (int j = 0; j < mapData.Length; j++)
                {
                    if (OrbitData[j, 1].Equals(nextOrbiter))
                    {
                        currentOrbitsum++;
                        nextOrbiter = OrbitData[j, 0];
                        j = 0;
                    }
                }
                
                OrbitSum += currentOrbitsum;
            }

        }
        public int GetOrbitSum()
        {
            return OrbitSum;
        }
    }
}
