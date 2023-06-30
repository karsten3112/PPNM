using System;
using static System.Math;

public static class LTSQ{

	public static (vector, matrix) lsfit(Func<double, double>[] fs, vector x, vector y, vector dy){
		matrix A = new matrix(x.size, fs.Length);
		matrix cov = new matrix(x.size, x.size);
		vector b = new vector(x.size);
		for(int i = 0; i < x.size; i++){
			b[i] = y[i]/dy[i];
			for(int k = 0; k < fs.Length; k++){
				A[i,k] = fs[k](x[i])/dy[i];
			}
		}
		(matrix Q, matrix R) = QRGS.decomp(A);
		matrix Ainv = QRGS.inverse(Q, 	R);
		cov = Ainv*Ainv.transpose();
		vector c = QRGS.solve(Q, R, b);
		return (c, cov);
	}

}
