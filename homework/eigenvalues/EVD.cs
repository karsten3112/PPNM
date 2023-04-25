using static System.Console;
using static System.Math;
using System;


public class EVD{
	public vector W; public matrix V;

	public EVD(matrix A){
		matrix M = A.copy();
		(vector W1, matrix V1) = cyclic(M);
		this.W = W1;
		this.V = V1;
	}

	public static (vector, matrix) cyclic(matrix M){
		int a = 0; int b = 0; double theta = 0;
		vector eigvec = new vector(M.size1);
		matrix V = new matrix(M.size1, M.size1);
		V.setid();
		bool change = false;
		int j = 0;
		(change, theta, a, b) = convergence(M, 0, 1);
		while(change == false){
			if(change == false){
				timesJ(M, a, b, theta);
				Jtimes(M, a, b, theta);
				timesJ(V, a, b, theta);
			}
			(change, theta, a, b) = convergence(M, a, b);
			j++;
			if(j >= 5000){
				throw new Exception("MAX TRIES EXCEEDED");
			}
		}
		WriteLine(j);
		for(int i = 0; i < M.size1;i++){
			eigvec[i] = M[i,i];
		}
		return (eigvec, V);
	}

	public static void timesJ(matrix A, int p, int q, double theta){
		double c = Cos(theta); double s = Sin(theta);
		for(int i = 0; i < A.size1; i++){
			A[i,p] = c*A[i,p] - s*A[i,q];
			A[i,q] = s*A[i,p] + c*A[i,q];
		}
	}

	public static void Jtimes(matrix A, int p, int q, double theta){
		double c = Cos(theta); double s = Sin(theta);
		for(int i = 0; i < A.size2; i++){
			A[p,i] = c*A[p,i] - s*A[q,i]; //Har andet fortegn end Dmitri
			A[q,i] = s*A[p,i] + c*A[q,i];
		}
	}

	public static (bool, double, int, int) convergence(matrix A, int a, int b){
		for(int p = a; p < A.size1-1; p++){
			for(int q = b; q < A.size1; q++){
				double Aqq = A[q,q]; double Apq = A[p,q]; double App = A[p,p];
				double theta = 0.5*Atan2(2.0*Apq, Aqq - App);
				double s = Sin(theta); double c = Cos(theta);
				double newAqq = s*s*App + c*c*Aqq + 2.0*s*c*Apq;
				double newApp = c*c*App + s*s*Aqq - 2.0*s*c*Apq;
				if(App != newApp || Aqq != newAqq){
					return (false, theta, p, q);
				}
			}
		}
	return (true, 0.0, 0, 0);
	}

}
