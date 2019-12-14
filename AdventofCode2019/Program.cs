using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace AdventofCode2019
{
    class Program
    {        
        static void Main(string[] args)
        {
            Console.Write("Which Day?: ");
            bool wrongInput;
            do
            {
                wrongInput = false;
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Fuel f = new Fuel("../../../input/mass.txt");
                        Console.WriteLine("day 1 solution: " + f.GetFuel());
                        break;
                    case 2:
                        IntcodeComputer ic1 = new IntcodeComputer("../../../input/gravity_assist_programm.txt");
                        ic1.Alarm1202();
                        long[] intcode = ic1.Run(true,false);
                        int nounverb = -1;
                        for (int noun = 0; noun <= 99; noun++)
                        {
                            for (int verb = 0; verb <= 99; verb++)
                            {
                                ic1 = new IntcodeComputer("../../../input/gravity_assist_programm.txt");
                                ic1.SetNounVerb(noun, verb);
                                long[] check = ic1.Run(true,false);
                                if (check[0] == 19690720)
                                {
                                    nounverb = 100 * noun + verb;
                                }
                            }
                        }
                        Console.WriteLine("day 2 solution: {0} {1}", intcode[0], nounverb);
                        break;
                    case 3:
                        Wires w = new Wires("../../../input/wires.txt");
                        Console.WriteLine("day 3 solution: {0} {1}", w.GetManhattendistance(), w.GetFewestStepsToIntersection());
                        break;
                    case 4:
                        Password6D p = new Password6D(265275, 781584);
                        Console.WriteLine("day 4 solution: {0} {1}", p.GetPasswordsAmountCrit1(), p.GetPasswordsAmountCrit2());
                        break;
                    case 5:
                        Console.WriteLine("day 5 solution: ");
                        IntcodeComputer ic2 = new IntcodeComputer("../../../input/TEST.txt");
                        ic2.Run(true,false);
                        Console.Write("\n");
                        break;
                    case 6:
                        OrbitMap om = new OrbitMap("../../../input/orbit_map.txt");
                        Console.WriteLine("day 6 solution: {0} {1}", om.GetOrbitSum(), om.GetMinimumTransfers("SAN", "YOU"));
                        break;
                    case 7:
                        long highestoutput1 = 0;
                        IntcodeComputer ic3 = new IntcodeComputer("../../../input/amplifier_controller_software.txt");
                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                for (int k = 0; k < 5; k++)
                                {
                                    for (int l = 0; l < 5; l++)
                                    {
                                        for (int m = 0; m < 5; m++)
                                        {
                                            int[] phase = new int[] { i, j, k, l, m };
                                            if (!new int[] { j, k, l, m }.Contains(i) && !new int[] { i, k, l, m }.Contains(j) && !new int[] { i, j, l, m }.Contains(k) && !new int[] { i, j, k, m }.Contains(l) && !new int[] { i, j, k, l }.Contains(m))
                                            {
                                                long inputSignal = 0;
                                                for (int n = 0; n < 5; n++)
                                                {
                                                    ic3.SetInput(new long[] {phase[n], inputSignal });
                                                    ic3.Run(false,false);
                                                    inputSignal = ic3.GetLastOutput();
                                                }
                                                if (inputSignal > highestoutput1)
                                                {
                                                    highestoutput1 = inputSignal;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        long highestoutput2 = 0;
                        for (int i = 5; i < 10; i++)
                        {
                            for (int j = 5; j < 10; j++)
                            {
                                for (int k = 5; k < 10; k++)
                                {
                                    for (int l = 5; l < 10; l++)
                                    {
                                        for (int m = 5; m < 10; m++)
                                        {
                                            if (!new int[] { j, k, l, m }.Contains(i) && !new int[] { i, k, l, m }.Contains(j) && !new int[] { i, j, l, m }.Contains(k) && !new int[] { i, j, k, m }.Contains(l) && !new int[] { i, j, k, l }.Contains(m))
                                            {
                                                IntcodeComputer ampA = new IntcodeComputer("../../../input/amplifier_controller_software.txt");
                                                IntcodeComputer ampB = new IntcodeComputer("../../../input/amplifier_controller_software.txt");
                                                IntcodeComputer ampC = new IntcodeComputer("../../../input/amplifier_controller_software.txt");
                                                IntcodeComputer ampD = new IntcodeComputer("../../../input/amplifier_controller_software.txt");
                                                IntcodeComputer ampE = new IntcodeComputer("../../../input/amplifier_controller_software.txt");

                                                ampA.SetICOut(ampB);
                                                ampB.SetICOut(ampC);
                                                ampC.SetICOut(ampD);
                                                ampD.SetICOut(ampE);
                                                ampE.SetICOut(ampA);

                                                ampA.SetInput(new long[] {i,0});
                                                ampB.SetInput(new long[] { j });
                                                ampC.SetInput(new long[] { k });
                                                ampD.SetInput(new long[] { l });
                                                ampE.SetInput(new long[] { m });

                                                ampA.SetName("ampA");
                                                ampB.SetName("ampB");
                                                ampC.SetName("ampC");
                                                ampD.SetName("ampD");
                                                ampE.SetName("ampE");

                                                Thread aThread = new Thread(AmplifierLoopThread),bThread = new Thread(new ParameterizedThreadStart(AmplifierLoopThread)),cThread = new Thread(new ParameterizedThreadStart(AmplifierLoopThread)),dThread = new Thread(new ParameterizedThreadStart(AmplifierLoopThread)),eThread = new Thread(new ParameterizedThreadStart(AmplifierLoopThread));
                                                aThread.Start(ampA);
                                                bThread.Start(ampB);
                                                cThread.Start(ampC);
                                                dThread.Start(ampD);
                                                eThread.Start(ampE);
                                                eThread.Join();
                                                if ( ampA.GetLastInput()> highestoutput2)
                                                {
                                                    highestoutput2 = ampA.GetLastInput();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Console.WriteLine("day 7 solution: {0} {1}",highestoutput1,highestoutput2);
                        break;
                    case 8:
                        SpaceImage si = new SpaceImage(25,6,"../../../input/space_image.txt");
                        Console.WriteLine("day 8 solution: {0}",si.Get1_2MultipliedOfLayerWithFewest0Digits());
                        si.PrintImage();
                        break;
                    case 9:
                        IntcodeComputer icBOOST = new IntcodeComputer("../../../input/BOOST.txt");
                        icBOOST.Run(true, false);
                        break;
                    case 10:
                        break;
                    case 11:
                        break;
                    case 12:
                        JupiterMoons jm = new JupiterMoons("../../../input/jupiter_moons.txt");
                        Console.WriteLine("day 12 solution: {0} {1}", jm.GetTotalEnergyAfterXSteps(1000), jm.GetMinStepsUntilRepeat());
                        break;
                    case 13:
                        IntcodeComputer ArcadeGame = new IntcodeComputer("../../../input/arcade_game.txt");
                        ArcadeGame.Run(false,false);
                        long[] gameFieldData = ArcadeGame.GetOutput();
                        int blockTileCount = 0;
                        for (int i = 0; i < gameFieldData.Length; i += 3)
                        {
                            if(gameFieldData[i + 2] == 2)
                            {
                                blockTileCount++;
                            }
                        }
                        Console.WriteLine("\nday 13 solution: {0}", blockTileCount);
                        //for (int i = 0; i < gameFieldData.Length; i+=3)
                        //{
                        //    Console.SetCursorPosition((int)gameFieldData[i], (int)gameFieldData[i + 1]);
                        //    switch (gameFieldData[i + 2])
                        //    {
                        //        case 0:
                        //            Console.Write(" ");
                        //            break;
                        //        case 1:
                        //            Console.Write("#");
                        //            break;
                        //        case 2:
                        //            Console.Write("=");
                        //            blockTileCount++;
                        //            break;
                        //        case 3:
                        //            Console.Write("-");
                        //            break;
                        //        case 4:
                        //            Console.Write("o");
                        //            break;
                        //        default:
                        //            Console.Write("-1");
                        //            break;
                        //    }
                        //}
                        break;
                    case 14:
                        break;
                    case 15:
                        break;
                    case 16:
                        break;
                    case 17:
                        break;
                    case 18:
                        break;
                    case 19:
                        break;
                    case 20:
                        break;
                    case 21:
                        break;
                    case 22:
                        break;
                    case 23:
                        break;
                    case 24:
                        break;
                    case 25:
                        break;
                    default:
                        Console.Write("Wrong Input! Try Again: ");
                        wrongInput = true;
                        break;
                }
                if (!wrongInput)
                {
                    Console.Write("Another Day?(Y/N): ");
                    string input = Console.ReadLine();
                    if ( input=="Y" || input== "y")
                    {
                        wrongInput = true;
                        Console.Write("Which Day?: ");
                    }
                }
            } while (wrongInput);
            Console.WriteLine("\nMerry Christmas!");
            Console.ReadKey();
        }
        public static void AmplifierLoopThread(object obj)
        {
            IntcodeComputer ic = (IntcodeComputer)obj;
            ic.Run(false,true);
        } 
    }
}
