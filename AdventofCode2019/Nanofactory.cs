using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventofCode2019
{
    class Nanofactory
    {
        private string[][] reaction;

        public Nanofactory(string url)
        {
            try
            {
                StreamReader sr = new StreamReader(url);
                string allreactions = sr.ReadToEnd();
                string[] reactionData = allreactions.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                reaction = new string[reactionData.Length][];
                for(int i = 0; i < reactionData.Length; i++)
                {
                    reactionData[i] = reactionData[i].Replace("=>", "").Replace(",","").Replace("\r","");
                    reaction[i] = reactionData[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public int GetOreAmountOf(string substance)
        {
            int oreAmount = 0;
            for (int i = 0; i < reaction.Length; i++)
            {
                if (reaction[i][reaction[i].Length - 1] == substance)
                {
                    if (reaction[i][1] == "ORE")
                    {
                        return int.Parse(reaction[i][0]);
                    }
                    else
                    {
                        for(int j = 0; j < reaction[i].Length - 2; j += 2)
                        {
                            oreAmount += int.Parse(reaction[i][j]) * GetOreAmountOf(reaction[i][j + 1]);
                        }
                        return oreAmount;
                    }
                }
            }
            return oreAmount;
        }
    }
}
