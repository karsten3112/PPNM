using static System.Math;
using static System.Console;
using System;

public static class jacobi{

	public static (matrix, matrix) cyclic_EVD(matrix A, double eps=1e-12){
		matrix w = A.copy();
		matrix V = matrix.id(A.size1);
		bool stat = true;
		while(stat){
			(stat, w, V) = convergence(w, V, eps);
		}
		return (w, V);
	}

	public static (bool, matrix, matrix) convergence(matrix A, matrix B, double eps=1e-12){
		bool result = false;
		matrix w = A.copy();
		matrix V = B.copy();
		for(int p=0;p<A.size1-1;p++){
			for(int q=p+1;q<A.size1;q++){
				double App =A[p,p]; double Aqq = A[q,q]; double Apq = A[p,q];
				double theta = 0.5*Atan2(2.0*Apq, Aqq - App);
				double c = Cos(theta); double s = Sin(theta);
				double new_App=c*c*App-2*s*c*Apq+s*s*Aqq;
				double new_Aqq=s*s*App+2*s*c*Apq+c*c*Aqq;
				if(Abs(new_App - App) > eps || Abs(new_Aqq - Aqq) > eps){
					w = timesJ(w, p, q, theta);
					w = Jtimes(w, p, q, -theta);
					V = timesJ(V, p, q, theta);
					result = true;
				}
			}
		}
		return (result, w, V);
	}

	public static matrix timesJ(matrix A, int p, int q, double theta){
		matrix result = A.copy();
		double s = Sin(theta); double c = Cos(theta);
		for(int i = 0; i < A.size1; i++){
			double Aip = result[i,p]; double Aiq = result[i,q];
			result[i,p] = c*Aip - s*Aiq;
			result[i,q] = s*Aip + c*Aiq;
		}
		return result;
	}
	public static matrix Jtimes(matrix A, int p, int q, double theta){
		matrix result = A.copy();
		double s = Sin(theta); double c = Cos(theta);
		for(int j = 0; j < A.size1; j++){
			double Apj = result[p,j]; double Aqj = result[q,j];
			result[p,j] = c*Apj + s*Aqj;
			result[q,j] = -s*Apj + c*Aqj;

		}
		return result;
	}

}
