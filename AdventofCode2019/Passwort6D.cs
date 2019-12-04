using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode2019
{
    class Password6D
    {
        private int passwordAmount;

        public Password6D(int prangeStart,int prangeEnd)
        {
            int[] digits = new int[6];

            passwordAmount = 0;
            for (int i = prangeStart; i <= prangeEnd; i++)
            {
                int j = i;
                digits[0] = j / 100000;
                j %= 100000;
                digits[1] = j / 10000;
                j %= 10000;
                digits[2] = j / 1000;
                j %= 1000;
                digits[3] = j / 100;
                j %= 100;
                digits[4] = j / 10;
                j %= 10;
                digits[5] = j;

                if((digits[0]==digits[1]||digits[1]==digits[2] || digits[2] == digits[3] || digits[3] == digits[4] || digits[4] == digits[5]) &&(digits[0] >= digits[1] && digits[1]>= digits[2] && digits[2] >= digits[3] && digits[3] >= digits[4] && digits[4] >= digits[5]))
                {
                    passwordAmount++;
                }
            }

        }
        public int GetPasswordsAmount()
        {
            return passwordAmount;
        }
    }
    
}
