using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventofCode2019
{
    class Wires
    {
        private string[] wirePath1Dir, wirePath2Dir;
        private int[] wirePath1Dis, wirePath2Dis;

        public Wires(string url)
        {
            try
            {
                StreamReader sr = new StreamReader(url);
                string s = sr.ReadToEnd();
                string[] data = s.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                wirePath1Dir = data[0].Split(',');    
                wirePath2Dir = data[1].Split(',');
                for(int i = 0; i < wirePath1Dir.Length;i++)
                {
                    wirePath1Dir[i]=wirePath1Dir[i].Trim('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                }    
                for (int i = 0; i < wirePath2Dir.Length;i++)
                {
                    wirePath2Dir[i]=wirePath2Dir[i].Trim('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                }

                string help = data[0];                
                string[] helpA = help.Split(new char[] { 'R', 'U', 'D', 'L', ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < helpA.Length; i++)
                    wirePath1Dis = new int[helpA.Length];
                for (int i = 0; i < helpA.Length; i++)
                {
                    wirePath1Dis[i] = int.Parse(helpA[i]);
                }

                help = data[1];
                helpA = help.Split(new char[] { 'R', 'U', 'D', 'L',',' }, StringSplitOptions.RemoveEmptyEntries);
                wirePath2Dis = new int[helpA.Length];
                for (int i = 0; i < helpA.Length; i++)
                {
                    wirePath2Dis[i] = int.Parse(helpA[i]);
                }


            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void PrintArrays()
        {
            for(int i = 0; i < wirePath1Dir.Length; i++)
            {
                Console.WriteLine("{0} {1} {2} {3}\n",wirePath1Dir[i], wirePath1Dis[i], wirePath2Dir[i], wirePath2Dis[i]);
            }
            Console.WriteLine(wirePath1Dir[wirePath1Dir.Length - 1]);
        }
    }
}


