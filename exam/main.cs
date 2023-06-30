using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
		foreach(string arg in args){
			string[] inp = arg.Split(":");
			if(inp[0] == "-Test1"){
				test(-2.0, 2.0, -2.0, 2.0, 5);
			}
			if(inp[0] == "-Irreg"){
				double[] xs1 = {-2.0, -1.0,-0.5, -0.2,-0.1, 0, 0.1, 0.2, 1.0, 1.5, 2.0};
				double[] ys1 = {-2.0, -1.0,-0.5, -0.2,-0.1, 0, 0.1, 0.3};
				vector xs = new vector(xs1.Length);
				vector ys = new vector(ys1.Length);
				for(int i = 0; i < xs1.Length; i++){
					xs[i] = xs1[i];
				}
				for(int i = 0; i < ys1.Length; i++){
					ys[i] = ys1[i];
				}
				Func<double, double, double> f = delegate(double x, double y){
					return 3*Exp(-4*(x*x+y*y));
				};
				matrix F = new matrix(xs.size, ys.size);
				for(int i = 0; i < xs.size; i++){
					for(int j = 0; j < ys.size; j++){
						F[i,j] = f(xs[i], ys[j]);
						WriteLine($"{xs[i]}	{ys[j]}	{F[i,j]}");
					}
					Write("\n");
				}
				WriteLine("\n");
				bilin interp = new bilin(xs, ys, F);
				int N = 30; int M = 25;
				double xstart = -2.0; double xend = 2.0;
				double ystart = -2.0; double yend = 0.3;
				vector xval = genpoints(xstart, xend, N);
				vector yval = genpoints(ystart, yend, M);
				for(int i = 0; i < N; i++){
					for(int j = 0; j < M; j++){
						WriteLine($"{xval[i]}	{yval[j]}	{interp.eval(xval[i], yval[j])}");
					}
					Write("\n");
				}
			}

			if(inp[0] == "-Cyl"){
				Func<double, double, double> f = delegate(double x, double y){
					return x*x*x + y*y;
				};
				int size1 = 8; int N = 35;
				int size2 = 8;
				double phistart = 0.0; double rstart = 0.0;
				double phiend = 2.0*PI; double rend = 2.0;
				vector phi = genpoints(phistart, phiend, size2);
				vector Rs = genpoints(rstart, rend, size1);
				matrix F = new matrix(size1, size2);
				for(int i = 0; i < size1; i++){
					for(int j = 0; j < size2; j++){
						double xval = Rs[i]*Cos(phi[j]);
						double yval = Rs[i]*Sin(phi[j]);
						F[i,j] = f(xval, yval);
						WriteLine($"{Rs[i]}	{phi[j]}	{xval}	{yval}	{F[i,j]}");
					}
				}
				WriteLine("\n");
				bilin interp = new bilin(Rs, phi, F);
				double dr = (rend - rstart)/(N-1);
				double dphi = (phiend - phistart)/(N-1);
				for(int i = 0; i < N; i++){
					double rval = rstart + i*dr;
					for(int j = 0; j < N; j++){
						double phival = phistart + j*dphi;
						double z = interp.eval(rval, phival);
						WriteLine($"{rval}	{phival}	{rval*Cos(phival)}	{rval*Sin(phival)}	{z}");
					}
					WriteLine("");
				}
				WriteLine("\n");
			}
		}
	}



	public static void test(double xstart, double xend, double ystart, double yend, int size){
		vector xs = genpoints(xstart, xend, size);
		vector ys = genpoints(ystart, yend, size);
		matrix F = new matrix(size, size);
		Func<double, double, double> f = delegate(double x, double y){
			return x*x + y*y;
		};
		for(int i = 0; i < size; i++){
			for(int j = 0; j < size; j++){
				F[i,j] = f(xs[i], ys[j]);
				WriteLine($"{xs[i]}	{ys[j]}	{F[i,j]}");
			}
			WriteLine("");
		}
		WriteLine("\n");
		int N = 25;
		vector x1 = genpoints(xstart, xend, N);
		vector y1 = genpoints(ystart, yend, N);
		bilin interp = new bilin(xs, ys, F);
		for(int i = 0; i < N; i++){
			for(int j = 0; j < N; j++){
				double val = interp.eval(x1[i], y1[j]);
				WriteLine($"{x1[i]}	{y1[j]}	{val}");
			}
			WriteLine("");
		}
	}
	public static vector genpoints(double xstart, double xend, int N){
		double dx = (xend - xstart)/(N - 1);
		vector result = new vector(N);
		for(int i = 0; i < N; i++){
			result[i] = xstart + dx*i;
		}
		return result;
	}
}
