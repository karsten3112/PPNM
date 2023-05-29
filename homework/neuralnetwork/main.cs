using static System.Console;
using static System.Math;
using System;

class main{
	static void Main(){
		Func<double, double> g = delegate(double z){
			return Cos(5*z-1)*Exp(-z*z);
		};
		(vector xs, vector ys) = genpoints(g, 30, -1.0, 1.0);
		for(int i = 0; i < xs.size; i++){
			WriteLine($"{xs[i]}	{ys[i]}");
		}
		ann network = new ann(3);
		network.train(xs, ys);
		vector ps = network.ps;
		Func<double, double> res = delegate(double z){
			return network.response(z, ps);
		};
		WriteLine("");
		WriteLine("");
		(vector xres, vector yres) = genpoints(res, 300, -1.0, 1.0);
		for(int i = 0; i < xres.size; i++){
			WriteLine($"{xres[i]} {yres[i]}");
		}

	}


	public static (vector, vector) genpoints(Func<double, double> f, int N, double xstart, double xend){
		vector xs = new vector(N); vector ys = new vector(N);
		double deltax = Abs(xend - xstart)/N;
		for(int i = 0; i < N; i++){
			xs[i] = xstart + i*deltax;
			ys[i] = f(xstart + i*deltax);
		}
		return (xs, ys);
		}



}
