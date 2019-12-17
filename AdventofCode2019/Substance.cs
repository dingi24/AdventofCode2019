using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode2019
{
    class Substance
    {
        private int requiredAmount, excessAmount,amountFromReaction;
        private string name;
        private string[] components;
        private Nanofactory nanofactory;

        public Substance(int amountFromReaction, string name,string[] components, Nanofactory nanofactory)
        {
            this.amountFromReaction = amountFromReaction;
            this.name = name;
            this.components = components;
            this.nanofactory = nanofactory;
            requiredAmount = 0;
            excessAmount = 0;
        }
        public string Name
        {
            get => name;
        }
        public void AddRequiredAmount(int amount)
        {
            int addRequiredAmount = 0;
            amount -= excessAmount;
            while (amount > addRequiredAmount)
            {
                addRequiredAmount += amountFromReaction;
            }
            excessAmount = addRequiredAmount % amount;
             
            requiredAmount += (addRequiredAmount / amount) * amount;
        }
        public int GetRequiredComponentsAmount()
        {
            int requiredComponentsAmount = 0;
            for(int i = 0; i < components.Length; i += 2)
            {
                for(int j = 0; j < requiredAmount; j += amountFromReaction)
                {
                    requiredComponentsAmount += int.Parse(components[i]);
                }
            }
            return requiredComponentsAmount;
        }
    }
}
