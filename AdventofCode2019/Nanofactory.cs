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
        private Substance[] substances;
        public Nanofactory(string url)
        {
            try
            {
                StreamReader sr = new StreamReader(url);
                string allreactions = sr.ReadToEnd();
                string[] reactionData = allreactions.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                reaction = new string[reactionData.Length][];
                substances = new Substance[reactionData.Length];
                for(int i = 0; i < reactionData.Length; i++)
                {
                    reactionData[i] = reactionData[i].Replace("=>", "").Replace(",","").Replace("\r","");
                    reaction[i] = reactionData[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] components = new string[reaction[i].Length - 2];
                    for (int j = 0; j < components.Length; j ++)
                    {
                        components[j] = reaction[i][j];
                    }
                    substances[i] = new Substance(int.Parse(reaction[i][reaction[i].Length - 2]), reaction[i][reaction[i].Length - 1],components, this);
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
            for(int i = 0; i < reaction.Length; i++)
            {
                if (reaction[i][reaction[i].Length - 1] == substance)
                {
                    for(int j = 0; i < reaction[i].Length - 2; j += 2)
                    {

                    }
                }
            }
            return oreAmount;
        }
        private void CalcAmountofSubstance(string substance)
        {
            for(int i = 0; i < substances.Length; i++)
            {
                if (substances[i].Name == substance)
                {


                }
            }
        }
    }
}
