using static System.Console;
using static System.Math;
using System;


public class EVD{
	public vector W; public matrix V;
	public matrix Y;

	public EVD(matrix M){
		matrix C = M.copy();
		(vector W1, matrix V1, matrix Y1) = cyclic(C);
		this.W = W1;
		this.V = V1;
		this.Y = Y1;
	}

	public static (vector, matrix, matrix) cyclic(matrix M){
		bool change = true; bool check = false; double theta = 0;
		vector eigvec = new vector(M.size1);
		matrix V = new matrix(M.size1, M.size1);
		V.setid();
		while(change == true){
			change = false;
			for(int p = 0; p < M.size1-1; p++){
				for(int q = p+1; q < M.size1; q++){
					(check, theta) = convergence(M, p, q);
					if(check == true){
						timesJ(M, p, q, theta);
						Jtimes(M, p, q, theta);
						timesJ(V, p, q, theta);
						change = true;
					}
				}
			}
		}
		for(int i = 0; i < M.size1;i++){
			eigvec[i] = M[i,i];
		}
		matrix G = M.copy();
		return (eigvec, V, G);
	}

	public static void timesJ(matrix A, int p, int q, double theta){
		double c = Cos(theta); double s = Sin(theta);
		for(int i = 0; i < A.size1; i++){
			double Aip = A[i,p]; double Aiq = A[i,q]; //We have to assign these variables or else we change the values before complete rotation
			A[i,p] = c*Aip - s*Aiq;
			A[i,q] = s*Aip + c*Aiq;
		}
	}

	public static void Jtimes(matrix A, int p, int q, double theta){
		double c = Cos(theta); double s = Sin(theta);
		for(int i = 0; i < A.size2; i++){
			double Api = A[p,i]; double Aqi = A[q,i]; //We have to assign these variables or else we change the values before complete rotation
			A[p,i] = c*Api - s*Aqi; //Har andet fortegn end Dmitri
			A[q,i] = s*Api + c*Aqi;
		}
	}

	public static (bool, double) convergence(matrix A, int p, int q){
		double Aqq = A[q,q]; double Apq = A[p,q]; double App = A[p,p];
		double theta = 0.5*Atan2(2.0*Apq, Aqq - App);
		double s = Sin(theta); double c = Cos(theta);
		double newAqq = s*s*App + c*c*Aqq + 2.0*s*c*Apq;
		double newApp = c*c*App + s*s*Aqq - 2.0*s*c*Apq;
		if(newApp != App || newAqq != Aqq){
			return (true, theta);
		}
		return (false, 0.0);
		}
}
