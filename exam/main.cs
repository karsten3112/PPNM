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
			if(inp[0] == "-Test3"){
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
				double dx = (xend - xstart)/(N -1);
				double ystart = -2.0; double yend = 0.3;
				double dy = (yend - ystart)/(M-1);
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
				int size1 = 8; int N = 50;
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
		double dx = (xend-xstart)/(size-1);
		double dy = (yend-ystart)/(size-1);
		vector xs = new vector(size);
		vector ys = new vector(size);
		matrix F = new matrix(size, size);
		Func<double, double, double> f = delegate(double x, double y){
			return x*x + y*y;
		};
		for(int i = 0; i < size; i++){
			xs[i] = xstart + dx*i;
			ys[i] = ystart + dy*i;
			for(int j = 0; j < size; j++){
				double evalx = xstart + dx*i;
				double evaly = ystart + dy*j;
				F[i,j] = f(evalx, evaly);
				WriteLine($"{evalx}	{evaly}	{F[i,j]}");
			}
			WriteLine("");
		}
		WriteLine("\n");
		int N = 25;
		double[] xs1 = {-2.0,-1.5555,1};
		dy = (yend-ystart)/(N-1);
		dx = (xend-xstart)/(N-1);
		bilin interp = new bilin(xs, ys, F);
		for(int i = 0; i < N; i++){
			double evalx1 = xstart + i*dx;
			for(int j = 0; j < N; j++){
				double evaly1 = ystart + j*dy;
				double val = interp.eval(evalx1, evaly1);
				WriteLine($"{evalx1}	{evaly1}	{val}");
			}
			WriteLine("");
		}
	}
	public static vector sphere(double R, double theta, double phi, vector offset){
		double x = offset[0] + R*Sin(theta)*Cos(phi);
		double y = offset[1] + R*Sin(theta)*Sin(phi);
		double z = offset[2] + R*Cos(theta);
		return new vector(x,y,z);
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
