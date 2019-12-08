using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventofCode2019
{
    class SpaceImage
    {
        private int[,] layer;
        private int[,] imagePixel;
        int indexLayerWithFewest0Digits,height,width;
        public SpaceImage(int width,int height,string url)
        {
            this.height = height;
            this.width = width;
            try
            {
                StreamReader sr = new StreamReader(url);
                string ImageData = sr.ReadToEnd();
                ImageData = ImageData.Replace(" ", "").Replace("\n", "").Replace("\r", "");
                layer = new int[ImageData.Length / (height * width), height * width];

                for (int i = 0; i < layer.GetLength(0); i++)
                {
                    for (int j = 0; j < layer.GetLength(1); j++)
                    {
                        layer[i, j] = int.Parse(ImageData[i * layer.GetLength(1) + j].ToString());
                    }
                }
                CalcIndexLayerWithFewest0Digits();
                CalcImagePixel();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public int Get1_2MultipliedOfLayerWithFewest0Digits()
        {
            int amount1D = 0, amount2D = 0;
            for(int i = 0; i < layer.GetLength(1); i++)
            {
                if (layer[indexLayerWithFewest0Digits, i] == 1)
                {
                    amount1D++;
                }
                else if(layer[indexLayerWithFewest0Digits, i] == 2)
                {
                    amount2D++;
                }
            }
            return amount1D * amount2D;
        }
        public void PrintImage()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if(imagePixel[i, j] == 1)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    
                }
                Console.Write("\n");
            }
        }
        private void CalcIndexLayerWithFewest0Digits()
        {
            indexLayerWithFewest0Digits = 0;
            int lowest0d = int.MaxValue;
            for (int i = 0; i < layer.GetLength(0); i++)
            {
                int amount0d = 0;
                for (int j = 0; j < layer.GetLength(1); j++)
                {
                    if (layer[i, j] == 0)
                    {
                        amount0d++;
                    }
                }
                if (amount0d < lowest0d)
                {
                    lowest0d = amount0d;
                    indexLayerWithFewest0Digits = i;
                }
            }
        }
        private void CalcImagePixel()
        {
            imagePixel = new int[height, width];
            for(int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    imagePixel[i, j] = 2;
                }
            }
            for(int i = 0; i < layer.GetLength(0); i++)
            {
                for(int h = 0; h < height; h++)
                {
                    for(int w = 0; w < width; w++)
                    {
                        if (imagePixel[h, w] == 2)
                        {
                            imagePixel[h, w] = layer[i, h * width + w];
                        }
                    }
                }
            }
        }

    }
}
