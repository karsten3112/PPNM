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
				jacobi_test(int.Parse(input[1]));
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
		A[1,0] = 2.0;
		A[0,1] = 2.0;
		A[1,1] = 1.0;
		WriteLine("THE MATRIX A");
		A.print();
		EVD eigen = new EVD(A);
		eigen.W.print();
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

}
