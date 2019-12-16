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
        private long[] intcode, input, output;
        private IntcodeComputer icOut;


        public IntcodeComputer(long[] intcode)
        {
            this.intcode = intcode;
            input = new long[] {  };
            output = new long[] { };
        }
        public IntcodeComputer(string url)
        {
            input = new long[] {  };
            output = new long[] { };
            try
            {
                StreamReader sr = new StreamReader(url);
                string s = sr.ReadToEnd();
                string[] data = s.Split(new char[] { ',','\n','\t',' ','\r' }, StringSplitOptions.RemoveEmptyEntries);

                intcode = new long[data.Length+2*data.Length];


                for (int i = 0; i < data.Length; i++)
                {
                    intcode[i] = long.Parse(data[i]);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void SetValueAtX(int x,int value)
        {
            intcode[x] = value;
        }
        public IntcodeComputer(string url,IntcodeComputer icOut) : this(url)
        {
            this.icOut = icOut;
        }
        public void SetICOut(IntcodeComputer icOut)
        {
            this.icOut = icOut;
        }
        public void SetInput(long[] input)
        {
            this.input = input;
        }
        public void InputAdd(long add)
        {
            long[] newInput = new long[input.Length+1];
            int i;
            for (i = 0; i < input.Length; i++)
            {
                newInput[i] = input[i];
            }
            newInput[i] = add;
            input = newInput;
        }
        public void ResetOutput()
        {
            output = new long[] { };
        }
        public long GetLastInput()
        {
            return input[input.Length - 1];
        }
        public long[] GetOutput()
        {
            return output;
        }
        public long GetLastOutput() 
        {
            return output[output.Length - 1];
        }
        public void OutputAdd(long add)
        {
            long[] newOutput = new long[output.Length + 1];
            int i;
            for (i = 0; i < output.Length; i++)
            {
                newOutput[i] = output[i];
            }
            newOutput[i] = add;
            output = newOutput;
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
        public long[] Run(int IOSetting)
        {
            long[] intcodeR = new long[intcode.Length];
            for(int i = 0; i < intcode.Length; i++)
            {
                intcodeR[i] = intcode[i];
            }
            bool _continue = true;
            int  instructionLength = 0, inputIndex = 0, modeP1, modeP2, modeP3,opcode, instruction,outputAGcounter=0;
            long relativeBase=0;
            for(long i = 0; i < intcode.Length&&_continue; i += instructionLength)
            {
                instruction = (int)intcodeR[i];

                modeP3 = instruction / 10000;
                instruction %= 10000;
                modeP2 = instruction / 1000;
                instruction %= 1000;
                modeP1 = instruction / 100;
                instruction %= 100;
                opcode = instruction;

                long[] index = new long[] { 0, 0, 0 };
                if (i+3 < intcode.Length)
                {
                    index = GetIndex(i,relativeBase, modeP1, modeP2, modeP3, intcodeR[i + 1], intcodeR[i + 2], intcodeR[i + 3]);
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
                        switch (IOSetting)
                        {
                            case 1:
                                intcodeR[index[0]] = Convert.ToInt32(Console.ReadLine());
                                break;
                            case 2:
                                while (inputIndex >= input.Length)
                                {
                                    Thread.Sleep(0);
                                }
                                intcodeR[index[0]] = input[inputIndex];
                                inputIndex++;
                                break;
                            case 3:
                                Console.SetCursorPosition(0, 23);
                                Console.WriteLine("                          ");
                                Console.SetCursorPosition(0, 23);
                                Console.Write("Next Move?: ");
                                string e = Console.ReadLine();
                                if (int.TryParse(e,out int n))
                                {
                                    intcodeR[index[0]] = Convert.ToInt32(e);
                                }
                                else
                                {
                                    intcodeR[index[0]] = 0;
                                }
                                break;
                            case 0:
                            default:
                                intcodeR[index[0]] = input[inputIndex];
                                if (inputIndex < input.Length - 1)
                                {
                                    inputIndex++;
                                }
                                break;
                        }
                        break;
                    case 4:
                        instructionLength = 2;
                        switch (IOSetting)
                        {
                            case 1:
                                Console.Write(intcodeR[index[0]] + " ");
                                break;
                            case 2:
                                icOut.InputAdd(intcodeR[index[0]]);
                                OutputAdd(intcodeR[index[0]]);
                                break;
                            case 3:
                                OutputAdd(intcodeR[index[0]]);
                                outputAGcounter++;
                                if (outputAGcounter == 3)
                                {
                                    DrawTile();
                                    outputAGcounter = 0;
                                }
                                break;
                            case 0:
                            default:
                                OutputAdd(intcodeR[index[0]]);
                                break;
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
                    case 9:
                        instructionLength = 2;
                        relativeBase += intcodeR[index[0]];
                        break;
                    case 99:
                        _continue = false;
                        break;                       
                }
            }
            return intcodeR;
        }
        private long[] GetIndex(long i,long rb,long m1,long m2,long m3,long v1,long v2, long v3)
        {
            long i1=0, i2=0, i3=0;
            switch (m1)
            {
                case 0:
                    i1 = v1;
                    break;
                case 1:
                    i1 = i + 1;
                    break;
                case 2:
                    i1 = rb + v1;
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
                case 2:
                    i2 = rb + v2;
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
                case 2:
                    i3 = rb + v3;
                    break;
            }
            return new long[] { i1, i2, i3 };
        }
        private void DrawTile()
        {
            long[] gameField = new long[] { output[output.Length-3], output[output.Length - 2], output[output.Length - 1] };
            if (gameField[0] == -1 && gameField[1] == 0)
            {
                Console.SetCursorPosition(0, 25);
                Console.WriteLine("                          ");
                Console.SetCursorPosition(0, 25);
                Console.WriteLine("Score: " + gameField[ 2]);
            }
            else
            {
                Console.SetCursorPosition((int)gameField[0], (int)gameField[1]);
                switch (gameField[2])
                {
                    case 0:
                        Console.Write(" ");
                        break;
                    case 1:
                        Console.Write("#");
                        break;
                    case 2:
                        Console.Write("=");
                        break;
                    case 3:
                        Console.Write("-");
                        break;
                    case 4:
                        Console.Write("o");
                        break;
                    default:
                        Console.Write("-1");
                        break;
                }
            }
            
        }
    }
}
