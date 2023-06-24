using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
		foreach(string arg in args){
			string[] inp = arg.Split(":");
			if(inp[0] == "-Test"){
				test(-2.0, 2.0, -2.0, 2.0, 10);
			}
			if(inp[0] == "-Sim"){
				vector d = new vector(2.0, 2.0, 1.0);
				vector phi = genpoints(0, 2*PI, 10);
				vector theta = genpoints(0, PI, 10);
				double R = 1.0;
				matrix F = new matrix(phi.size, theta.size);
				for(int i = 0; i < phi.size; i++){
					for(int j = 0; j < theta.size; j++){
						F[i,j] = R;
					}
				}
				bilin interp = new bilin(phi, theta, F);
				vector thetav = genpoints(0.0, PI, 8);
				for(int i = 0; i < thetav.size; i++){
					vector phie = genpoints(0.0, 2*PI, 100);
					for(int j = 0; j < phie.size; j++){
						double Re = interp.eval(phie[j], thetav[i]);
						vector ev = sphere(Re, thetav[i], phie[j], d);
						WriteLine($"{ev[0]}	{ev[1]}	{ev[2]}");
					}
					WriteLine("");
				}
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
			return 2.0*Exp(-x*x-y*y);
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
		double[] xs1 = {-0.5,0,1};
		dy = (yend-ystart)/300;
		bilin interp = new bilin(xs, ys, F);
		foreach(double x in xs1){
			for(int i = 0; i < 250; i++){
				double evaly1 = ystart + (i+1)*dy;
				double val = interp.eval(x, evaly1);
				WriteLine($"{x}	{evaly1}	{val}");
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
