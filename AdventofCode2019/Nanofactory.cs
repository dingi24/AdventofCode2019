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
            for(int i = 0; i < substances.Length; i++)
            {
                if (substances[i].Name == substance)
                {
                    substances[i].RequiredAmount = 1;
                }
            }
            CalcAmountofSubstances(substance);
            for (int i = 0; i < reaction.Length; i++)
            {
                if (reaction[i][1] == "ORE")
                {
                    for(int j = 0; j < substances.Length; j++)
                    {
                        if(substances[j].Name== reaction[i][reaction[i].Length - 1])
                        {
                            oreAmount+=substances[j].GetRequiredComponentsAmount();
                        }
                    }
                }
            }
            for(int i = 0; i < substances.Length; i++)
            {
                substances[i].Reset();
            }
            return oreAmount;
        }
        public long GetSubstanceAmountForORE(string substance,long oreAmount)
        {
            long oreCounter = 0, maxAmount = 0;
            do
            {
                maxAmount++;
                oreCounter += GetOreAmountOf("FUEL");
            } while (oreCounter < oreAmount);
            return maxAmount;
        }
        private void CalcAmountofSubstances(string substance)
        {
            for(int i = 0; i < reaction.Length; i++)
            {
                if (reaction[i][reaction[i].Length - 1] == substance)
                {
                    if (reaction[i][1] != "ORE")
                    {
                        int requiredAmountofReactions = 0;
                        for(int j=0;j< substances.Length; j++)
                        {
                            if (substances[j].Name == substance)
                            {
                                requiredAmountofReactions = substances[j].GetRequiredReactionsAmount();
                            }
                        }
                        for (int k = 0; k < reaction[i].Length - 2; k += 2)
                        {
                            for (int l = 0; l < substances.Length; l++)
                            {
                                if (substances[l].Name == reaction[i][k + 1])
                                {
                                    substances[l].AddRequiredAmount(int.Parse(reaction[i][k]) * requiredAmountofReactions);
                                }
                            }
                        }
                        for (int j = 0; j < reaction[i].Length - 2; j += 2)
                        {
                            CalcAmountofSubstances(reaction[i][j + 1]);
                        }
                    }
                }
            }
        }
    }
}
