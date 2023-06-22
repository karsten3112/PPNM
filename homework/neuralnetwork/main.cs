using static System.Console;
using static System.Math;
using System;

class main{
	static void Main(string[] args){
		foreach(string arg in args){
			string[] inp = arg.Split(":");
			if(inp[0] == "-test"){
				test();
			}
			if(inp[0] == "-diffeq"){
				ann network = new ann(6);
				vector yinit = new vector(0.0,1.0);
				vector dyinit = new vector(0.0,0.0);
				Func<double, double, double, double, double> phi = delegate(double ddy, double dy, double y, double x){
					return y + ddy;
				};
				double xstart = 0; double xend = PI;
				network.traindiff(phi, yinit, dyinit, xstart, xend, "simplex");
				Func<double, string, double> res = delegate(double z, string h){
					return network.response(z);
				};
				(vector xres, vector yres) = genpoints(res, "none", 300, xstart, xend);
				for(int i = 0; i < xres.size; i++){
					WriteLine($"{xres[i]} {yres[i]}");
				}


			}
		}
	}


	public static void test(){
		Func<double, string, double> g = delegate(double z, string h){
			return Cos(5*z-1)*Exp(-z*z);
		};
		(vector xs, vector ys) = genpoints(g, "none", 30, -1.0, 1.0);
		for(int i = 0; i < xs.size; i++){
			WriteLine($"{xs[i]}	{ys[i]}");
		}
		ann network = new ann(6);
		network.trainint(xs, ys, "simplex");
		Func<double, string, double> res = delegate(double z, string type){
			if(type == "derivative"){
				return network.dresponse(z);
			}
			if(type == "antiderivative"){
				return network.aresponse(z);
			}
			if(type == "none"){
				return network.response(z);
			}
			else {
				return 0;
			}
		};
		WriteLine("");
		WriteLine("");
		(vector xres, vector yres) = genpoints(res, "none", 300, -1.0, 1.0);
		(vector xres1, vector yres1) = genpoints(res, "derivative", 300, -1.0, 1.0);
		(vector xres2, vector yres2) = genpoints(res, "antiderivative", 300, -1.0, 1.0);
		for(int i = 0; i < xres.size; i++){
			WriteLine($"{xres[i]} {yres[i]}	{yres1[i]}	{yres2[i]}");
		}

	}

	public static (vector, vector) genpoints(Func<double, string, double> f, string type, int N, double xstart, double xend){
		vector xs = new vector(N); vector ys = new vector(N);
		double deltax = Abs(xend - xstart)/N;
		for(int i = 0; i < N; i++){
			xs[i] = xstart + i*deltax;
			ys[i] = f(xstart + i*deltax, type);
		}
		return (xs, ys);
		}

}
