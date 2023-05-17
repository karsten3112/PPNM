using static System.Console;
using static System.Math;
using System;

public static class rootf{

	public static (vector, int) newton(Func<vector, vector> f, vector xs, double eps=1e-2, int maxsteps=5000){
		bool convergence = false; vector x = xs.copy(); int i = 0;
		vector delx = new vector(xs.size); double lambda = 1.0;
		matrix J = new matrix(xs.size, xs.size);
		do{ i++;
			J = calcJ(f, x);
			(matrix Q, matrix R) = QRGS.decomp(J);
			delx = QRGS.solve(Q, R, -f(x));
			lambda = 1.0;
			while(f(x + lambda*delx).norm() > (1.0 - lambda/2.0)*f(x).norm() && lambda >= 1.0/1024){
				lambda/=2.0;
			}
			x+= lambda*delx;
			if(f(x).norm() < eps){
				convergence = true;
			}
		} while(convergence != true && i < maxsteps);
		return (x, i);
	}

	public static (vector, int) qnewton(Func<vector, vector> f, vector xs, double eps=1e-2, int maxsteps=5000){
		bool convergence = false; vector x = new vector(xs.size); int i = 0;
		vector delx = new vector(xs.size); double lambda = 1.0;
		matrix J = calcJ(f, xs);
		J.print();
		(matrix Q, matrix R) = QRGS.decomp(J);
		matrix B = QRGS.inverse(Q, R);
		while(convergence != true && i < maxsteps){
			delx = -B*f(x);
			while(f(x + lambda*delx).norm() > (1.0 - lambda/2.0)*f(x).norm() && lambda >= 1.0/1024){
				lambda/=2.0; //we keep dividing the step into halfes for certainty
			}

			x+= lambda*delx; //We accept the new step no matter what
			 
			if(lambda >= 1.0/1024){			//if step is of acceptable size we update Broyden and continue
				B+= updateB(f, x, delx, B);
			} else {						//if step is not accepted we recalculate J as consequence and continue
				J = calcJ(f, x);
				(Q, R) = QRGS.decomp(J);
				B = QRGS.inverse(Q, R);
			}

			if(f(xs).norm() <= eps){
				convergence = true;
			}
			i++;
		}
		return (x, i);
	}

	public static matrix calcJ(Func<vector, vector> f, vector xs){
		double deltax = xs.norm()*Pow(2.0,-26.0); vector x_step = xs.copy();
		if(deltax == 0) {deltax = Pow(2.0,-26.0);}
		matrix J = new matrix(xs.size, xs.size);
		for(int i = 0; i < J.size1; i++){
		for(int k = 0; k < J.size2; k++){
			x_step[k]+=deltax;
			double delf = f(x_step)[i] - f(xs)[i];
			J[i,k] = delf/deltax;
			x_step = xs.copy();
			}
		}
		return J;
	}

	public static matrix updateB(Func<vector, vector> f, vector x, vector delx, matrix B){
		matrix delxs = new matrix(x.size, 1); matrix delf = new matrix(x.size, 1);
		delxs[0] = delx; delf[0] = f(x + delx) - f(x);
		matrix delB = new matrix(B.size1, B.size1);
		double v = delxs[0].dot(delf[0]);
		delB = (delxs - B*delf)/(v)*delxs.T;
		return delB;
	}

}
