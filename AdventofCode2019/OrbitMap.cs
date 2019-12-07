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
        private string[,] OrbitersData;

        public OrbitMap(string [] orbitMap)
        {
            CalcOrbitAttributes(orbitMap);
        }
        public OrbitMap(string url)
        {
            StreamReader sr = new StreamReader(url);
            string s = sr.ReadToEnd();
            CalcOrbitAttributes(s.Split(new char[] { '\n', ' ', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries));
            
        }
        private void CalcOrbitAttributes(string[] orbitMap)
        {
            string[] mapData = orbitMap;
            OrbitersData = new string[mapData.Length, 2];
            for (int i = 0; i < OrbitersData.GetLength(0); i++)
            {
                OrbitersData[i, 0] = mapData[i].Split(')')[0];
                OrbitersData[i, 1] = mapData[i].Split(')')[1];
            }
            OrbitSum = 0;
            for (int i = 0; i < OrbitersData.GetLength(0); i++)
            {
                int currentOrbitsum = 1;
                string nextOrbiter = OrbitersData[i, 0];
                for (int j = 0; j < mapData.Length; j++)
                {
                    if (OrbitersData[j, 1].Equals(nextOrbiter))
                    {
                        currentOrbitsum++;
                        nextOrbiter = OrbitersData[j, 0];
                        j = -1;
                    }
                }
                OrbitSum += currentOrbitsum;
            }
        }
        public int GetOrbitSum()
        {
            return OrbitSum;
        }
        public int GetMinimumTransfers(string Orbiter1,string Orbiter2)
        {
            string intersectionOrbiter="COM";
            bool _continue = true;
            List<string> OrbitersOfO1 = new List<string>();
            for (int i=0;i<OrbitersData.GetLength(0); i++)
            {
                if (OrbitersData[i, 1] == Orbiter1)
                {
                    string nextOrbiter = OrbitersData[i, 0];
                    OrbitersOfO1.Add(nextOrbiter);
                    for(int j = 0; j < OrbitersData.GetLength(0); j++)
                    {
                        if (OrbitersData[j, 1] == nextOrbiter)
                        {
                            nextOrbiter = OrbitersData[j, 0];
                            OrbitersOfO1.Add(nextOrbiter);
                            j = -1;
                        }
                    }
                }
            }
            int transfersToIntersection = 0;
            for (int i = 0; i < OrbitersData.GetLength(0); i++)
            {
                if (OrbitersData[i, 1] == Orbiter2)
                {
                    string nextOrbiter = OrbitersData[i, 0];
                    do
                    {
                        foreach (var orbitofO1 in OrbitersOfO1)
                        {
                            if (orbitofO1 == nextOrbiter)
                            {
                                _continue = false;
                                intersectionOrbiter = nextOrbiter;
                            }
                        }
                        if (_continue)
                        {
                            bool _continue2 = true;
                            transfersToIntersection++;
                            for (int j = 0; j < OrbitersData.GetLength(0)&&_continue2; j++)
                            {
                                if (OrbitersData[j, 1] == nextOrbiter)
                                {
                                    nextOrbiter = OrbitersData[j, 0];
                                    _continue2 = false;
                                }
                            }
                        }
                        
                    } while (_continue);
                    
                }
            }

            return transfersToIntersection+OrbitersOfO1.IndexOf(intersectionOrbiter);
        }
    }
}
