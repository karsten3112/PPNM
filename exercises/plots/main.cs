using static System.Math;
using System;
using static System.Console;

static class main{
	static void Main(string[] args){
		foreach(string arg in args){
			string[] inp =  arg.Split(":");
			if(inp[0] == "-erf"){
				int N = 600;
				double[] xs = gendata(-3.0, 3.0, N);
				for(int i = 0; i < N; i++){
					WriteLine($"{xs[i]}	{sfuns.erf(xs[i])}");
				}
			}
			if(inp[0] == "-rgamma"){
				int N = 1200;
				double[] xs = gendata(-5.0, 5.0, N);
				for(int i = 0; i < N; i++){
					WriteLine($"{xs[i]}	{sfuns.rgamma(xs[i])}");
				}
			}
			if(inp[0] == "-cgamma"){
				int N = 400;
				int M = 200;
				double[] xs = gendata(-3.5, 5.0, N);
				for(int i = 0; i < N; i++){
					complex xstart = new complex(xs[i], -3.0);
					complex xend = new complex(xs[i], 3.0);
					complex[] ys = gendata(xstart, xend, M);
					for(int j = 0; j < M; j++){
						WriteLine($"{xs[i]}	{ys[j].Im}	{cmath.abs(sfuns.cgamma(ys[j]))}");
					}
					WriteLine("");
				}
			}
			if(inp[0] == "-lngamma"){
				int N = 600;
				double[] xs = gendata(0, 5.0, N);
				for(int i = 0; i < N; i++){
					WriteLine($"{xs[i]}	{sfuns.lngamma(xs[i])}");
				}
			}
		}
	}


	public static double[] gendata(double xstart, double xend, int N){
		double dx = (xend - xstart)/(N - 1);
		double[] result = new double[N];
		for(int i = 0; i < N; i++){
			result[i] = xstart + dx*i;
		}
	return result;
	}

	public static complex[] gendata(complex xstart, complex xend, int N){
		complex dx = new complex(0.0, (complex.imag(xend) - complex.imag(xstart))/(N - 1));
		complex[] result = new complex[N];
		for(int i = 0; i < N; i++){
			result[i] = xstart + dx*i;
		}
	return result;
	}

}
