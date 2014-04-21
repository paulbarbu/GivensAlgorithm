using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Givens
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            double[,] a = { {6, 5, 0},
                            {5, 1, 4},
                            {0, 4, 3} };

            double[] b = {1,2,3};
            */

            string[] lines = System.IO.File.ReadAllLines(@"in.txt");

            int pos = Array.IndexOf(lines, "@");
            double[,] a = new double[pos, pos];
            double[] b = new double[pos];

            Console.WriteLine("Found @: {0}", pos);

            for (int i = 0; i < pos; i++)
            {
                string[] coef = lines[i].Split(' ');
                for (int j = 0; j < coef.Length; j++)
                {
                    a[i, j] = Convert.ToDouble(coef[j]);
                }
            }

            string[] freeTerms = lines[pos + 1].Split(' ');
            for (int j = 0; j < freeTerms.Length; j++)
            {
                b[j] = Convert.ToDouble(freeTerms[j]);
            }
            
            Matrix A = new Matrix(a);
            Matrix B = new Matrix(b);


            Console.WriteLine("A =");
            Console.WriteLine(A);
            Console.WriteLine("B =");
            Console.WriteLine(B);

            GivensMethod gm = new GivensMethod(A, B);
            Matrix X = gm.solve();

            Console.WriteLine("X =");
            Console.WriteLine(X);

            Console.ReadKey(true);
        }
    }
}

