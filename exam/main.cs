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
}
