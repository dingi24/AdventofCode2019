using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace AdventofCode2019
{
    class IntcodeComputer
    {
        private int[] intcode,input;
        private int output;
        private IntcodeComputer icOut;
        private string name;

        public IntcodeComputer(int[] intcode)
        {
            this.intcode = intcode;
            input = new int[] { 0 };
            output = 0;
        }
        public IntcodeComputer(string url)
        {
            input = new int[] { 0 };
            output = 0;
            try
            {
                StreamReader sr = new StreamReader(url);
                string s = sr.ReadToEnd();
                string[] data = s.Split(new char[] { ',','\n','\t',' ','\r' }, StringSplitOptions.RemoveEmptyEntries);

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
        public IntcodeComputer(string url,IntcodeComputer icOut) : this(url)
        {
            this.icOut = icOut;
        }
        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetICOut(IntcodeComputer icOut)
        {
            this.icOut = icOut;
        }
        public void SetInput(int[] input)
        {
            this.input = input;
        }
        public void InputAdd(int add)
        {
            Console.WriteLine(name + ": Received Input " + add);
            int[] newInput = new int[input.Length+1];
            int i;
            for (i = 0; i < input.Length; i++)
            {
                newInput[i] = input[i];
            }
            newInput[i] = add;
            input = newInput;
        }
        public int GetLastInput()
        {
            Console.WriteLine(name+": Last Input is "+input[input.Length - 1]);
            return input[input.Length - 1];
        }
        public int GetOutput()
        {
            return output;
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
        public int[] Run(bool enableUI,bool hasICOut)
        {
            int[] intcodeR = intcode;
            bool _continue = true;
            int opcode, modeP1, modeP2, modeP3, instruction, index1, index2, index3, instructionLength = 0, inputIndex = 0;
            for(int i = 0; i < intcode.Length&&_continue; i += instructionLength)
            {
                instruction = intcodeR[i];

                modeP3 = instruction / 10000;
                instruction %= 10000;
                modeP2 = instruction / 1000;
                instruction %= 1000;
                modeP1 = instruction / 100;
                instruction %= 100;
                opcode = instruction;

                int[] index = new int[] { 0, 0, 0 };
                if (i+3 < intcode.Length)
                {
                    index = GetIndex(i, modeP1, modeP2, modeP3, intcodeR[i + 1], intcodeR[i + 2], intcodeR[i + 3]);
                }    
                switch (opcode)
                {
                    case 1:
                        instructionLength = 4;
                        intcodeR[index[2]] = intcodeR[index[0]] + intcodeR[index[1]];
                        break;
                    case 2:
                        instructionLength = 4;
                        intcodeR[index[2]] = intcodeR[index[0]] * intcodeR[index[1]];
                        break;
                    case 3:
                        instructionLength = 2;
                        if (hasICOut)
                        {
                            while (inputIndex >= input.Length)
                            {
                                Thread.Sleep(0);
                            }
                            Console.WriteLine(name + ": Next Input is: " + input[inputIndex]);    
                            intcodeR[index[0]] = input[inputIndex];
                            inputIndex++;
                        }
                        else if (enableUI)
                        {
                            Console.Write("input: ");
                            intcodeR[index[0]] = Convert.ToInt32(Console.ReadLine());
                        }
                        else
                        {
                            intcodeR[index[0]] = input[inputIndex];
                            if (inputIndex < input.Length - 1)
                            {
                                inputIndex++;
                            }
                        }
                        break;
                    case 4:
                        instructionLength = 2;
                        if (enableUI)
                        {
                            Console.Write(intcodeR[index[0]] + " ");
                        }
                        else
                        {
                            if (hasICOut)
                            {
                                Console.WriteLine(name + ": Send Output " + intcodeR[index[0]]);
                                icOut.InputAdd(intcodeR[index[0]]);
                            } 
                            output = intcodeR[index[0]];
                        }
                        break;
                    case 5:
                        if (intcodeR[index[0]] != 0)
                        {
                            instructionLength = 0;
                            i = intcodeR[index[1]];
                        }
                        else
                        {
                            instructionLength = 3;
                        }
                        break;
                    case 6:
                        if (intcodeR[index[0]] == 0)
                        {
                            instructionLength = 0;
                            i = intcodeR[index[1]];
                        }
                        else
                        {
                            instructionLength = 3;
                        }
                        break;
                    case 7:
                        instructionLength = 4;
                        if (intcodeR[index[0]]< intcodeR[index[1]])
                        {
                            intcodeR[index[2]] = 1;
                        }
                        else
                        {
                            intcodeR[index[2]] = 0;
                        }
                        break;
                    case 8:
                        instructionLength = 4;
                        if (intcodeR[index[0]] == intcodeR[index[1]])
                        {
                            intcodeR[index[2]] = 1;
                        }
                        else
                        {
                            intcodeR[index[2]] = 0;
                        }
                        break;
                    case 99:
                        Console.WriteLine(name + ": Operation complete");
                        _continue = false;
                        break;                       
                }
            }
            return intcodeR;
        }
        private int[] GetIndex(int i,int m1,int m2,int m3,int v1,int v2, int v3)
        {
            int i1=0, i2=0, i3=0;
            switch (m1)
            {
                case 0:
                    i1 = v1;
                    break;
                case 1:
                    i1 = i + 1;
                    break;
            }
            switch (m2)
            {
                case 0:
                    i2 = v2;
                    break;
                case 1:
                    i2 = i + 2;
                    break;
            }
            switch (m3)
            {
                case 0:
                    i3 = v3;
                    break;
                case 1:
                    i3 = i + 3;
                    break;
            }
            return new int[] { i1, i2, i3 };
        }
    }
}
