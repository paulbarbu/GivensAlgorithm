using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Givens
{
    class GivensMethod
    {
        Matrix A, Q, b;
        List<Matrix> G = new List<Matrix>();
        
        public GivensMethod(Matrix A, Matrix b)
        {
            //TODO: check if A is square and if b is a vector and if their dimensions match (A.n==b.n)
            //TODO: check not to have leaked implementation details of Matrix here (such as m==0)
            this.A = A;
            this.b = b;
        }

        public Matrix solve(){
            for (int i = 1; i < A.n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (0 != A[i, j])
                    {
                        Debug.WriteLine("G for {0} {1}:", i, j);
                        G.Add(generateG(i, j));
                        Debug.WriteLine(G[G.Count-1]);
                        
                        A = G[G.Count-1].product(A);

                        Debug.WriteLine("A =");
                        Debug.WriteLine(A);
                    }
                }
            }

            computeQ();
            
            return solveUpper();
        }

        private Matrix solveUpper()
        {
            //Q^-1 is equal to Q transposed since it's orthogonal
            Q.transpose();
            Debug.WriteLine("Qt =");
            Debug.WriteLine(Q);

            Matrix Qb = Q.product(b);
            double[] x = new double[Qb.n];
            
            Debug.WriteLine("Qb =");
            Debug.WriteLine(Qb);

            for (int i = Qb.n - 1; i >= 0; i--)
            {
                double S = 0;

                for(int j = i+1; j<Qb.n; j++){
                    S += x[j] * A[i, j];
                }

                x[i] = (Qb[i] - S) / A[i, i];
            }

            return new Matrix(x);
        }

        private void computeQ()
        {
            G[0].transpose();
            Q = G[0];
            
            for (int i = 1; i < G.Count; i++)
            {
                G[i].transpose();

                Q = Q.product(G[i]);
            }

            Debug.WriteLine("Q =");
            Debug.WriteLine(Q);
        }

        private Matrix generateG(int i, int j){
            double a = A[i-1,j];
            double b = A[i,j];
            double r = Math.Sqrt(a * a + b * b);
            double c = a / r;
            double s = -b / r;

            double[,] g = new double[A.n, A.m]; //TODO: zero by default?

            for (int k = 0; k < A.n; k++)
            {
                g[k, k] = 1;
            }

            g[i, i] = g[j, j] = c;
            g[i, j] = s;
            g[j, i] = -s;

            return new Matrix(g);
        }
    }
}
