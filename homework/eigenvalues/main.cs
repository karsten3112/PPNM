using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
		double dr = 0; double rmax = 0;
		foreach(string arg in args){
			string[] input = arg.Split(":");
			if(input[0] == "-dr"){
				dr = double.Parse(input[1]);
			}
			if(input[0] == "-rmax"){
				rmax = double.Parse(input[1]);
			}
			if(input[0] == "-test"){
				jacobi_test(int.Parse(input[1]));
			}
		}
		if(dr != 0 && rmax != 0){
			matrix H = Hamilton(rmax, dr);
			(matrix w, matrix Psis) = jacobi.cyclic_EVD(H);
			matrix PsisT = Psis.transpose();
			matrix Es = PsisT*H*Psis;
			Es.print();
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
		(matrix w, matrix v) = jacobi.cyclic_EVD(A);
		WriteLine("matrix v containing eigenvektors");
		v.print();
		matrix vT = v.transpose();
		matrix I = v*vT;
		WriteLine("matrix D containing eigenvalues");
		matrix D = vT*A*v;
		D.print();
		matrix s = v*D*vT;

	}

	static matrix Hamilton(double rmax, double dr){
		int size = (int)(rmax/dr) - 1;
		matrix K = matrix.id(size);
		K*=-2.0;
		matrix V = matrix.id(size);
		for(int i = 0; i < size - 1; i++){
			K[i,i+1] = 1.0;
		}
		for(int i = 1; i < size; i++){
			K[i,i-1] = 1.0;
		}
		K*=-1/(2*dr*dr);
		for(int i = 0; i < size; i++){
			double factor = (1+i)*dr;
			V[i,i] = -1/factor;
		}
		matrix H = K + V;
		return H;
	}

}
