using System;
using static System.Console;
using static System.Math;

class main{
	static Random rnd = new System.Random(3);

	static void Main(string[] args){
		foreach(string arg in args){
			string[] inp = arg.Split(":");
			if(inp[0] == "-QRGS-test"){
				WriteLine("TESTING QR-DECOMP FOR 2 PSEUDO-RANDOM nxm MATRICES");
				test_decomp();
         		WriteLine("********************");
         		WriteLine("TESTING SOLVE METHOD FOR Ax=b SYSTEM");
         		test_solve();
         		WriteLine("********************");
         		WriteLine("TESTING INVERSE METHOD FOR MATRIX A");
         		test_inverse();
			}
			if(inp[0] == "-TimeIt"){
				int size = int.Parse(inp[1]);
				matrix A = new matrix(size, size);
				QRGS.decomp(A);
			}
		}
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
            WriteLine("TESTING IF Q^T*Q IS APPROXIMATELY I");
			matrix A2 =  new matrix(d.size1, d.size1);
			A2.setid();
			bool s = d.approx(A2);
	        if(s == true){
				WriteLine("TRUE, THEY ARE THE SAME WITHIN 9 DECIMAL POINTS");
			}
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
		matrix A1 = A.copy();
		WriteLine("THE RANDOM b VECTOR");
		b.print();
		(matrix Q, matrix R) = QRGS.decomp(A1);
		vector solution = QRGS.solve(Q, R, b);
		WriteLine("SOLUTION x TO Ry=Q^T*b");
		solution.print();
		WriteLine("PRODUCT OF Q*R*x=b");
		vector f = Q*R*solution;
		f.print();
		WriteLine("PRODUCT OF A*x=b");
		vector g = A*solution;
		g.print();
	}

	public static void test_inverse(){
		int N = rnd.Next(1,4);
		double determ = 0;
		while(determ == 0){
        	matrix A = new matrix(N, N);
			for(int i = 0;i < N; i++){
				for(int j = 0; j < N; j++){
					A[i,j] = rnd.Next(0,15);
				}
			determ = QRGS.det(A);
		}
		matrix A1 = A.copy();
		WriteLine("THE RANDOM NxN MATRIX A;");
		A.print();
		WriteLine("THE INVERSE MATRIX B OF A");
		(matrix Q, matrix R) = QRGS.decomp(A1);
		matrix B = QRGS.inverse(Q, R);
		B.print();
		WriteLine("PRODUCT OF A*B=I");
		matrix C = A*B;
		C.print();
		WriteLine("TESTING IF A*B IS APPROXIMATELY I");
		matrix A2 =  new matrix(N,N);
		A2.setid();
		bool s = C.approx(A2);
		if(s == true){
			WriteLine("TRUE, THEY ARE THE SAME WITHIN 9 DECIMAL POINTS");
		}
		}
	}

}
