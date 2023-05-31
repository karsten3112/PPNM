using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
		double dr = 0; double rmax = 0;
		foreach(string arg in args){
			string[] input = arg.Split(":");
			if(input[0] == "-dr"){
				dr = double.Parse(input[1])*0.1;
			}
			if(input[0] == "-rmax"){
				rmax = double.Parse(input[1]);
			}
			if(input[0] == "-test"){
				WriteLine("=============================NEW TEST====================================");
				jacobi_test(1);
				WriteLine("=============================NEW TEST====================================");
				jacobi_test(2);
			}
			if(input[0] == "-Psi"){
				matrix H = Hamilton(10.0, 0.2);
				EVD eigen = new EVD(H);
				matrix Psis = eigen.V;
				for(int i = 0; i < Psis.size1; i++){
					WriteLine($"{(i+1)*0.2}	{Psis[i,0]/Sqrt(0.2)}");
				}
			}
		}
		if(dr != 0 && rmax != 0){
			matrix H = Hamilton(rmax, dr);
			EVD eigen = new EVD(H);
			vector Es = eigen.W;
			WriteLine($"{dr}	{rmax}	{Es[0]}");
		}
	}

	static void jacobi_test(int num){
		matrix A = new matrix(3,3);
		A.setid();
		if(num==1){
			A[1,0] = 2.0;
			A[0,1] = 2.0;
			A[1,1] = 1.0;
		}
		if(num==2){
			A[0] = new vector(3.0, -2.0, 4.0);
			A[1] = new vector(-2.0, 6.0, 2.0);
            A[2] = new vector(4.0, 2.0, 3.0);
        }
		A.print("The Matrix A");
		EVD eigen = new EVD(A);
		matrix V = eigen.V;
		matrix Y = eigen.Y;
		V.print("The orthogonal matrix (V) containing eigenvectors for matrix A");
		vector E = eigen.W;
		WriteLine("Vector containing the eigenvalues for matrix A");
		E.print();
		WriteLine("--------------------------------------------------------");
		WriteLine("Testing for V*V^T = 1 (unity) and V^T*V = 1 (unity)");
		matrix D = V.transpose();
		matrix F = V*D;
		F.print("Product of V*V^T");
		matrix G = D*V;
		G.print("Product of V^T*V");
		WriteLine("Testing with approx method....");
		pass(F,G);
		WriteLine("--------------------------------------------------------");
		WriteLine("Testing for V^T*A*V = D where D is the diagonal matrix containing Eigenvalues");
		matrix H = D*A*V;
		H.print("Product of V^T*A*V");
		WriteLine("Testing with approx method....");
		pass(Y, H);
		WriteLine("--------------------------------------------------------");
		WriteLine("Testing for V*D*V^T = A");
		matrix I = V*Y*D;
		I.print("Product of V*D*V^T");
		WriteLine("Testing with approx method....");
		pass(I, A);
	}

	static matrix Hamilton(double rmax, double dr){
		int size = (int)(rmax/dr) - 1;
		matrix K = matrix.id(size);
		K*=-2.0;
		matrix V = matrix.id(size);
		for(int i = 0; i < size - 1; i++){
			K[i,i+1] = 1.0;
			K[i+1,i] = 1.0;
		}
		K*=-1.0/(2.0*dr*dr);
		for(int i = 0; i < size; i++){
			double factor = (1.0+i)*dr;
			V[i,i] = -1.0/factor;
		}
		matrix H = K + V;
		return H;
	}
	static void pass(matrix A, matrix B){
		bool p = A.approx(B);
		if(p == true){
			WriteLine("Passed");
		} else {
			WriteLine("Failed the test");
		}
	}
}
