using static System.Console;
using static System.Math;
using System;

public static class rootf{

	public static vector newton(Func<vector, vector> f, vector xs, double eps=1e-2){
		bool convergence = false; vector x = new vector(xs.size);
		matrix J = calcJ(f, xs);
		(matrix Q, matrix R) = QRGS.decomp(J);
		matrix B = QRGS.inverse(Q, R);
		while(convergence != false){
			if(f(x)[0] <= eps){ //Change to all entries
				convergence = true;
			}
		}
		return x;
	}

	public static matrix calcJ(Func<vector, vector> f, vector xs){
		double delta = Pow(2.0,-26); vector x_step = xs;
		matrix J = new matrix(f(xs).size, xs.size);
		for(int i = 0; i < J.size1; i++){
			for(int k = 0; k < J.size2; k++){
				x_step[k] += xs[k]*delta; //We step in the k direction
				J[i,k] = (f(xs+x_step)[i] - f(xs)[i])/(xs[k]*delta); //calculating derivatives for different coordinates with same func.
				x_step = xs;
			}
		}
		return J;
	}

}
