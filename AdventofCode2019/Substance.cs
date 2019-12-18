using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode2019
{
    class Substance
    {
        private int requiredAmount, excessAmount, amountFromReaction,preUsedRRA;
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
            preUsedRRA=0;
        }
        public string Name
        {
            get => name;
        }
        public int RequiredAmount
        {
            set => requiredAmount = value;
        }
        public void Reset()
        {
            requiredAmount = 0;
            preUsedRRA = 0;
        }
        public int GetRequiredReactionsAmount()
        {
            int i;
            for (i = 0; (i * amountFromReaction) < (requiredAmount+excessAmount-preUsedRRA); i++) ;
            preUsedRRA = requiredAmount + excessAmount;
            return i;
        }
        public void AddRequiredAmount(int amount)
        {
            int j = excessAmount;
            bool amountNotZero = true;
            for(int i = 1; i <= j&&amountNotZero; i++)
            {
                if (amount > 0)
                {
                    amount--;
                    excessAmount--;
                    requiredAmount++;
                }
                else
                {
                    amountNotZero = false;
                }
            }
            if (amount % amountFromReaction != 0)
            {
                int excessCalcHelp = amount;
                excessCalcHelp -= (excessCalcHelp / amountFromReaction) * amountFromReaction;
                excessAmount += amountFromReaction - excessCalcHelp;
            }
            requiredAmount += amount;
        }
        public int GetRequiredComponentsAmount()
        {
            int requiredComponentsAmount = 0;
            for(int i = 0; i < components.Length; i += 2)
            {
                    requiredComponentsAmount += int.Parse(components[i])*GetRequiredReactionsAmount();
            }
            return requiredComponentsAmount;
        }
    }
}
