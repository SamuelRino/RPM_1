using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_10
{
    public class Computing
    {
        public static double[] Sqrt0Mas(int[] mas)
        {
            double[] newMas = new double[mas.Length];
            for (int i = 0; i < mas.Length; i++)
            {
                newMas[i] = mas[i];
                if (mas[i] > 0) newMas[i] = Math.Round(Math.Sqrt(mas[i]), 2);
            }
            return newMas;
        }
    }
}
