using static System.Math;
using static System.Console;
using System;

public static class QRGS{

	public static (matrix, matrix) decomp(matrix a){
		matrix Q = a.copy();
		matrix r = new matrix(a.size2, a.size2);
		for(int i = 0; i < a.size2; i++){
			double v_norm = Sqrt(a[i].dot(a[i]));
			Q[i] = a[i]/v_norm;
			r[i,i] = v_norm;
			for(int j = i + 1; j < a.size2; j++){
				r[i, j] = Q[i].dot(a[j]);
				a[j] = a[j] - Q[i].dot(a[j])*Q[i];
			}
		}
		return (Q,r);
	}
	public static vector solve(matrix Q, matrix R, vector b){
			vector y = new vector(b.size);
			vector c = Q.transpose()*b;
			for(int i = c.size - 1; i >= 0; i--){
				double sum = 0;
				for(int k = i+1; k < c.size; k++){
					sum+= R[i,k]*y[k];
				}
				y[i] = (c[i] - sum)/R[i,i];
			}
			return y;
	}

	public static double det(matrix R){
		double deter = 1;
		for(int i = 0; i < R.size1; i++){
			deter *= R[i,i];
		}
		return deter;
	}

	public static matrix inverse(matrix Q, matrix R){
		matrix result = new matrix(Q.size1, Q.size1);
		matrix c = new matrix(Q.size1, Q.size1);
		c.setid();
		for(int i = 0; i < c.size1; i++){
			vector sol = solve(Q, R, c[i]);
			result[i] = sol;
		}
		return result;
	}
}

