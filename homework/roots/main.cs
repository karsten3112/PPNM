using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
		double rmin; double rmax;
		foreach(string arg in args){
		string[] inp = arg.Split(":");
		if(inp[0] == "-Test"){
			Func<vector, vector> f = delegate(vector xs){
				double x = xs[0]; double y = xs[1];
				vector res = new vector(2);
				res[0] = 2.0*(1-x)-400*(y-x*x)*x;
				res[1] = 200*(y-x*x);
				return res;
			};
			Func<vector, vector> db = delegate(vector xs){
				vector y = new vector(2);
				y[0] = 2*(10-xs[0]);
				y[1] = 2*xs[1];
				return y;
			};
			double x1 = 0.83;
			vector xinit = new vector(x1, x1);
			(vector xf, int count) = rootf.newton(db, xinit);
			WriteLine(count);
			xf.print();
		}
		if(inp[0] == "-Shooting"){
			vector Einit = new vector(-0.9);
			rmin = 1e-1;
			rmax = 2;
			(vector e, int count) = shooting(rmin, rmax, Einit);
			WriteLine($"{e[0]}	{rmin}	{rmax}");
		}
		}
	}

	public static (vector, int) shooting(double rmin, double rmax, vector einit){
		vector yinit = new vector(rmin - rmin*rmin, 1.0-2.0*rmin);
		Func<vector, vector> M = delegate(vector z){
			double E = z[0];
			Func<double, vector, vector> diffeq = delegate(double r, vector ys){
				vector res = new vector(ys.size); double x1 = ys[0]; double x2 = ys[1];
				res[0] = x2;
				res[1] = -2.0*(E*x1 + x1/r);
				return res;
			};
		odeint solution = new odeint(diffeq, rmin,yinit,rmax);
		double F = solution.ys[0][0];
		vector result = new vector(F);
		return result;
		};
		(vector xf, int count) = rootf.newton(M, einit, 1e-3);
		return (xf, count);
	}
}

