using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
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
			Func<vector, vector> f2 = delegate(vector xs){
				vector y = new vector(2);
				y[0] = 2*(xs[0]-10);
				y[1] = 2*xs[1];
				return y;
			};
			Func<vector, vector> f3 = delegate(vector xs){
				vector y = new vector(1);
				double x = xs[0];
				y[0] = 0.2*Pow(x,5)+2.0*Pow(x,2)+1.5*x;
				return y;
			};
			double x1 = 0.83;
			vector xinit1 = new vector(x1, x1);
			(vector xf1, int count1) = rootf.newton(f, xinit1);
			(vector xf2, int count2) = rootf.newton(f2, xinit1);
			vector xinit2 = new vector(-0.2);
			vector xinit3 = new vector(-0.5);
			vector xinit4 = new vector(-1.6);
			(vector xf3, int count3) = rootf.newton(f3, xinit2);
			(vector xf4, int count4) = rootf.newton(f3, xinit3);
			(vector xf5, int count5) = rootf.newton(f3, xinit4);
			genlist<vector> inits = new genlist<vector>();
			inits.add(xinit1); inits.add(xinit1); inits.add(xinit2); inits.add(xinit3); inits.add(xinit4);
			genlist<vector> roots = new genlist<vector>();
			roots.add(xf1); roots.add(xf2); roots.add(xf3); roots.add(xf4); roots.add(xf5);
			int[] counts = {count1, count2, count3, count4, count5};
			string[] funcs = {"∇[(1-x)^2+100(y-x^2)^2]","∇[(x-10)^2 + y^2]","f(x) = 0.2x^5 + 2.0x^2 + 1.5x", "f(x) = 0.2x^5 + 2.0x^2 + 1.5x", "f(x) = 0.2x^5 + 2.0x^2 + 1.5x"};
			WriteLine("Testing rootfinding routine for a few functions \n");
			for(int i = 0; i < roots.size; i++){
				WriteLine($"Finding root of function {funcs[i]} |");
				inits[i].print("with initial guess (x, y) = ");
				roots[i].print("Result from rootfinding method x0 = ");
				WriteLine($"With number of counts: {counts[i]}");
				WriteLine("-----------------------------------------------------------------");
			}
		}
		if(inp[0] == "-Shooting1"){
			double[] rmins = {0.1, 0.2, 0.3, 0.4, 0.5, 0.7, 0.8, 1.0};
			double[] rmaxs = {2, 3, 4, 5, 6, 7, 8, 10};
			double[] rmaxE = new double[8];
			double[] rminE = new double[8];
			double eps = 1e-3; double acc = 1e-3;
			for(int i = 0; i < rmins.Length; i++){
				vector Einit = new vector(-0.9);
				(vector e, int count) = shooting1(rmins[0], rmaxs[i], Einit, acc, eps);
				rmaxE[i] = e[0];
				(e, count) = shooting1(rmins[i], rmaxs[7], Einit, acc, eps);
				rminE[i] = e[0];
			}
			for(int i = 0; i < rmaxs.Length; i++){
				WriteLine($"{rmaxs[i]}	{rmaxE[i]}	{eps}");
			}
			WriteLine("\n");
			for(int i = 0; i < rmins.Length; i++){
				WriteLine($"{rmins[i]}	{rminE[i]}	{eps}");
			}
			WriteLine("\n");
			double[] psiE = {rmaxE[0], rmaxE[2], rmaxE[5], rmaxE[7]};
			double[] psir = {rmaxs[0], rmaxs[2], rmaxs[5], rmaxs[7]};
			for(int i = 0; i < psiE.Length; i++){
				psi(psiE[i], 0.1, psir[i], acc, eps);
				WriteLine("\n");
			}
		}
		if(inp[0] == "-epsacc"){
			double[] vals = {0.001,0.01,0.1,0.2,0.3,0.4,0.5,0.6,0.7,0.8,0.9,1.0};
			double rmin = 0.1;
			double rmax = 10.0;
			vector Einit = new vector(-0.9);
			foreach(double v in vals){
				double acc = 0.01;
				(vector e, int count) = shooting1(rmin, rmax, Einit, acc, v);
				(vector e1, int count1) = shooting1(rmin, rmax, Einit, v, acc);
				WriteLine($"{v}	{e[0]}	{e1[0]}");
			}
		}
		if(inp[0] == "-conv2"){
			double[] rmaxs = {3,3.5,4,4.5,5,6,7,8,9};
			double rmin = 0.01;
			for(int i = 0; i < rmaxs.Length; i++){
				vector Einit = new vector(-0.9);
				(vector e, int count) = shooting2(rmin, rmaxs[i], Einit, 0.001, 0.001);
				(vector e1, int count1) = shooting1(rmin, rmaxs[i], Einit, 0.001, 0.001);
				WriteLine($"{rmaxs[i]}	{e[0]}	{e1[0]}");
 			}
		}
	}
	}

	public static (vector, int) shooting1(double rmin, double rmax, vector einit, double acc, double eps){ //
		vector yinit = new vector(rmin - rmin*rmin, 1.0-2.0*rmin);
		Func<vector, vector> M = delegate(vector z){
			double E = z[0];
			Func<double, vector, vector> diffeq = delegate(double r, vector ys){
				vector res = new vector(ys.size); double x1 = ys[0]; double x2 = ys[1];
				res[0] = x2;
				res[1] = -2.0*(E*x1 + x1/r);
				return res;
			};
		odeint solution = new odeint(diffeq, rmin,yinit,rmax, false, 2.0, 10.0, acc, eps);
		double F = solution.ys[0][0];
		vector result = new vector(F);
		return result;
		};
		(vector xf, int count) = rootf.newton(M, einit, 1e-3);
		return (xf, count);
	}


	public static (vector, int) shooting2(double rmin, double rmax, vector einit, double acc, double eps){ //Has different auxillery function for faster convergence
		vector yinit = new vector(rmin - rmin*rmin, 1.0-2.0*rmin);
		Func<vector, vector> M = delegate(vector z){
			double E = z[0];
			Func<double, vector, vector> diffeq = delegate(double r, vector ys){
				vector res = new vector(ys.size); double x1 = ys[0]; double x2 = ys[1];
					res[0] = x2;
					res[1] = -2.0*(E*x1 + x1/r);
					return res;
			};
		odeint solution = new odeint(diffeq, rmin,yinit,rmax, false, 2.0, 10.0, acc, eps);
        double F = solution.ys[0][0];
		double rm = solution.xs[0];
		double k = Sqrt(-2*E);
		vector n = new vector(rm*Exp(-k*rm)); //This part is changed from shooting1
        vector result = new vector(F);
        return (result-n);
    };
	(vector xf, int count) = rootf.newton(M, einit, 1e-3);
    return (xf, count);
	}

	public static void psi(double E, double rmin, double rmax, double acc, double eps){
		vector yinit = new vector(rmin - rmin*rmin, 1.0-2.0*rmin);
		Func<double, vector, vector> diffeq = delegate(double r, vector ys){
			vector res = new vector(ys.size); double x1 = ys[0]; double x2 = ys[1];
				res[0] = x2;
				res[1] = -2.0*(E*x1 + x1/r);
				return res;
		};
		odeint solution = new odeint(diffeq, rmin,yinit,rmax, true,0.01, 0.01, acc, eps);
		genlist<vector> F = solution.ys;
		genlist<double> rs = solution.xs;
		for(int i = 0; i < F.size;i++){
			WriteLine($"{rs[i]}	{F[i][0]}");
		}
	}

}


