using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode2019
{
    class XYZ
    {
        private int x, y, z;
        public XYZ(int xyz)
        {
            x = xyz;
            y = xyz;
            z = xyz;
        }
        public XYZ(int x,int y,int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public XYZ(XYZ xyz)
        {
            x = xyz.X;
            y = xyz.Y;
            z = xyz.Z;
        }
        public void addXYZ(XYZ add)
        {
            x += add.X;
            y += add.Y;
            z += add.Z;
        }
        public int GetXYZAbs()
        {
            return Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
        }
        public int X
        {
            get => x;
            set => x = value;
        }
        public int Y
        {
            get => y;
            set => y = value;
        }
        public int Z
        {
            get => z;
            set => z = value;
        }
        public void Printposition()
        {
            Console.WriteLine("x:{0} y:{1} z:{2}",x,y,z);
        }
        public static bool EqualXYZ(XYZ xyz1,XYZ xyz2)
        {
            if(xyz1.X==xyz2.X&& xyz1.Y == xyz2.Y&& xyz1.Z == xyz2.Z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
