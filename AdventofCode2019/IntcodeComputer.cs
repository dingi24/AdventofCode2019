using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventofCode2019
{
    class IntcodeComputer
    {
        private int[] intcode;

        public IntcodeComputer(string url)
        {
            try
            {
                StreamReader sr = new StreamReader(url);
                string s = sr.ReadToEnd();
                s.Trim(' ', '\t', '\r', '\n');
                string[] data = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                intcode = new int[data.Length];


                for (int i = 0; i < data.Length; i++)
                {
                    intcode[i] = int.Parse(data[i]);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Alarm1202()
        {
            intcode[1] = 12;
            intcode[2] = 2;
        }
        public void SetNounVerb(int noun, int verb)
        {
            intcode[1] = noun;
            intcode[2] = verb;
        }
        public int[] Run()
        {
            int[] intcodeR = intcode;
            bool _continue = true;
            for(int i = 0; i < intcode.Length&&_continue; i += 4)
            {
                switch (intcodeR[i])
                {
                    case 1:
                        intcodeR[intcodeR[i + 3]] = intcodeR[intcodeR[i + 1]] + intcodeR[intcodeR[i + 2]];
                        break;
                    case 2:
                        intcodeR[intcodeR[i + 3]] = intcodeR[intcodeR[i + 1]] * intcodeR[intcodeR[i + 2]];
                        break;
                    case 99: _continue = false;
                        break;                       
                }
            }
            return intcodeR;
        }
    }
}
