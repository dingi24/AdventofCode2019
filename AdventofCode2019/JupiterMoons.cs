using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode2019
{
    class JupiterMoons
    {
        private XYZ positionIo, positionEuropa, positionGanymede, positionCallisto;

        public JupiterMoons(string url)
        {
            try
            {
                StreamReader sr = new StreamReader(url);
                string moonsData = sr.ReadToEnd();
                moonsData = moonsData.Replace(" ", "").Replace("x=", "").Replace("y=", "").Replace("z=", "").Replace("<", "").Replace(">", "");
                string[] mda = moonsData.Split(new char[] { '\n', ' ', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                positionIo = new XYZ(int.Parse(mda[0].Split(',')[0]), int.Parse(mda[0].Split(',')[1]), int.Parse(mda[0].Split(',')[2]) );
                positionEuropa = new XYZ(int.Parse(mda[1].Split(',')[0]), int.Parse(mda[1].Split(',')[1]), int.Parse(mda[1].Split(',')[2]));
                positionGanymede = new XYZ(int.Parse(mda[2].Split(',')[0]), int.Parse(mda[2].Split(',')[1]), int.Parse(mda[2].Split(',')[2]));
                positionCallisto = new XYZ(int.Parse(mda[3].Split(',')[0]), int.Parse(mda[3].Split(',')[1]), int.Parse(mda[3].Split(',')[2]));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public int GetTotalEnergyAfterXSteps(int steps)
        {
            XYZ pIo=new XYZ(positionIo),pEuropa= new XYZ(positionEuropa),pGanymede = new XYZ(positionGanymede),pCallisto = new XYZ(positionCallisto),velIo=new XYZ(0), velEuropa = new XYZ(0), velGanymede = new XYZ(0), velCallisto = new XYZ(0), gIo, gEuropa, gGanymede, gCallisto;
            for (int i = 0; i < steps; i++)
            {
                gIo = CalculateGravity(new XYZ[] { pIo, pCallisto, pEuropa, pGanymede });
                gEuropa = CalculateGravity(new XYZ[] { pEuropa, pIo, pCallisto, pGanymede });
                gGanymede = CalculateGravity(new XYZ[] { pGanymede, pIo, pCallisto, pEuropa });
                gCallisto = CalculateGravity(new XYZ[] { pCallisto, pIo, pEuropa, pGanymede });

                velIo.addXYZ(gIo);
                velEuropa.addXYZ(gEuropa);
                velGanymede.addXYZ(gGanymede);
                velCallisto.addXYZ(gCallisto);

                pIo.addXYZ(velIo);
                pEuropa.addXYZ(velEuropa);
                pGanymede.addXYZ(velGanymede);
                pCallisto.addXYZ(velCallisto);
            }
            return (pIo.GetXYZAbs() * velIo.GetXYZAbs()) + (pEuropa.GetXYZAbs() * velEuropa.GetXYZAbs()) + (pGanymede.GetXYZAbs() * velGanymede.GetXYZAbs()) + (pCallisto.GetXYZAbs() * velCallisto.GetXYZAbs());
        }
        public long GetMinStepsUntilRepeat()
        {
            long i = 0,xCycle=-1,yCycle=-1,zCycle=-1;
            bool checkX = false, checkY = false, checkZ = false ;
            XYZ pIo = new XYZ(positionIo), pEuropa = new XYZ(positionEuropa), pGanymede = new XYZ(positionGanymede), pCallisto = new XYZ(positionCallisto), velIo = new XYZ(0), velEuropa = new XYZ(0), velGanymede = new XYZ(0), velCallisto = new XYZ(0), gIo, gEuropa, gGanymede, gCallisto;
            do
            {

                i++;
                gIo = CalculateGravity(new XYZ[] { pIo, pCallisto, pEuropa, pGanymede });
                gEuropa = CalculateGravity(new XYZ[] { pEuropa, pIo, pCallisto, pGanymede });
                gGanymede = CalculateGravity(new XYZ[] { pGanymede, pIo, pCallisto, pEuropa });
                gCallisto = CalculateGravity(new XYZ[] { pCallisto, pIo, pEuropa, pGanymede });

                velIo.addXYZ(gIo);
                velEuropa.addXYZ(gEuropa);
                velGanymede.addXYZ(gGanymede);
                velCallisto.addXYZ(gCallisto);

                pIo.addXYZ(velIo);
                pEuropa.addXYZ(velEuropa);
                pGanymede.addXYZ(velGanymede);
                pCallisto.addXYZ(velCallisto);

                if (xCycle == -1 && velIo.X == 0 && velEuropa.X == 0 && velGanymede.X == 0 && velCallisto.X == 0)
                {
                    checkX = true;
                    xCycle = i;
                }
                if (yCycle == -1 && velIo.Y == 0 && velEuropa.Y == 0 && velGanymede.Y == 0 && velCallisto.Y == 0)
                {
                    checkY = true;
                    yCycle = i;
                }
                if (zCycle == -1 && velIo.Z == 0 && velEuropa.Z == 0 && velGanymede.Z == 0 && velCallisto.Z == 0)
                {
                    checkZ = true;
                    zCycle = i;
                }

            } while (!checkX || !checkY || !checkZ );
            return CalcLeastCommonMultiple3Values(xCycle, yCycle, zCycle ) * 2;
        }

        private static long CalcLeastCommonMultiple3Values(long a,long b,long c)
        {
            long lcmAB = a * b / CalcGreatestCommonDivisor(a, b);
            return c * lcmAB / CalcGreatestCommonDivisor(c, lcmAB);
        }
        private static long CalcGreatestCommonDivisor(long a, long b)
        {

            long gcd;
            if (a == 0)
            {
                return Math.Abs(b);
            }
            if (b == 0)
            {
                return Math.Abs(a);
            }
            do
            {
                gcd = a % b;
                a = b;
                b = gcd;
            } while (b != 0);

            return Math.Abs(a);
        }

        private XYZ CalculateGravity(XYZ[] xyz)
        {
            int gx=0, gy=0, gz=0;
            for(int i = 1; i< xyz.Length; i++)
            {
                if (xyz[0].X > xyz[i].X)
                {
                    gx--;
                }
                else if(xyz[0].X < xyz[i].X)
                {
                    gx++;
                }

                if (xyz[0].Y > xyz[i].Y)
                {
                    gy--;
                }
                else if (xyz[0].Y < xyz[i].Y)
                {
                    gy++;
                }

                if (xyz[0].Z > xyz[i].Z)
                {
                    gz--;
                }
                else if (xyz[0].Z < xyz[i].Z)
                {
                    gz++;
                }
            }
            return new XYZ(gx, gy, gz);
        }
    }
}
