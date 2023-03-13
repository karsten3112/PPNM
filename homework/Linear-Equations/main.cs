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
		WriteLine("********************");
		WriteLine("TESTING INVERSE METHOD FOR MATRIX A");
		test_inverse();
	}

	public static void test_decomp(){
		for(int k = 0; k < 2; k++){
			int n = 0;
			int m = 0;
			while(n <= m){
				n = rnd.Next(2,6);
				m = rnd.Next(1,5);
			}
			matrix A = new matrix(n,m);
            for(int i = 0; i < A.size1; i++){
            	for(int j = 0; j < A.size2; j++){
                	A[i,j] = rnd.NextDouble()*10;
                }
            }
            WriteLine("THE RANDOM MATRIX A");
            A.print();
            (matrix Q, matrix R) = QRGS.decomp(A);
            WriteLine("THE ORTHONORMAL Q MATRIX");
            Q.print();

            WriteLine("THE U-TRIANGULAR R MATRIX");
            R.print();

            WriteLine("PRODUCT OF Q*R = A");
            matrix e = Q*R;
            e.print();

            WriteLine("PRODUCT OF Q^TQ = I");
            matrix d = Q.transpose()*Q;
            d.print();
            WriteLine($"TEST NR. {k + 1} DONE");
		}
    }

	public static void test_solve(){
		int N = rnd.Next(1,4);
		vector b = new vector(N);
		matrix A = new matrix(N, N);
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
		(matrix Q, matrix R) = QRGS.decomp(A);
		matrix s = Q*R;
		s.print();
		vector solution = QRGS.solve(Q, R, b);
		WriteLine("SOLUTION x TO Ry=Q^T*b");
		solution.print();
		WriteLine("PRODUCT OF Q*R*x=b");
		vector f = Q*R*solution;
		f.print();
	}

	public static void test_inverse(){
		int N = rnd.Next(1,4);
        matrix A = new matrix(N, N);
		matrix R = new matrix(N, N);
		}

}
