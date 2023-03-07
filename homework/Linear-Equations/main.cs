
using System;
using static System.Console;
using static System.Math;

class main{
	static Random rnd = new System.Random(1);

	static void Main(){
		WriteLine("TESTING QR-DECOMP FOR 2 PSEUDO-RANDOM nxm MATRICES");
		test_decomp();
		WriteLine("********************");
		WriteLine("TESTING SOLVE METHOD FOR Ax=b SYSTEM");
		test_solve();
	}

	public static void test_decomp(){
		for(int k = 0; k < 2; k++){
			matrix A = new matrix(rnd.Next(1,5),rnd.Next(1,5));
            matrix R = new matrix(A.size2, A.size2);
            for(int i = 0; i < A.size1; i++){
            	for(int j = 0; j < A.size2; j++){
                	A[i,j] = rnd.NextDouble()*10;
                }
            }
            WriteLine("THE RANDOM MATRIX A");
            A.print();
            double[] norms = QRGS.decomp(A, R);
            WriteLine("THE ORTHONORMAL Q MATRIX");
            A.print();

            WriteLine("THE U-TRIANGULAR R MATRIX");
            R.print();

            WriteLine("PRODUCT OF Q*R = A");
            matrix e = A*R;
            e.print();

            WriteLine("PRODUCT OF Q^TQ = I");
            matrix d = A.transpose()*A;
            d.print();

            WriteLine($"TEST NR. {k + 1} DONE");
		}
    }

	public static void test_solve(){
		int N = rnd.Next(1,4);
		vector b = new vector(N);
		matrix A = new matrix(N, N);
		matrix R = new matrix(N, N);
		for(int i = 0; i < N; i++){
			b[i] = rnd.Next(0,15);
			for(int j = 0; j < N; j++){
				A[i,j] = rnd.Next(0,15);
			}
		}
		WriteLine("THE RANDOM A MATRIX");
		A.print();
		WriteLine("THE RANDOM b VECTOR");
		b.print();
		double[] norms = QRGS.decomp(A, R);
		for(int i = 0; i < R.size2; i++){
			R[i] *= norms[i];
		}
		vector solution = QRGS.solve(A, R, b);
		WriteLine("SOLUTION x TO Ry=Q^T*b");
		solution.print();
		WriteLine("PRODUCT OF Q*R*y=b");
		vector f = A*R*solution;
		f.print();
	}
}
