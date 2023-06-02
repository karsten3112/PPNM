using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
		foreach(string arg in args){
			string[] inp = arg.Split(":");
			if(inp[0] == "-tPlainMC" || inp[0] == "-tQuasiMC"){
				bool convergence = false;
				double uncer = 0.0;
				int N = 1000; double res = 0.0; double err = 0.0; double acr = 0.0;
				vector a = new vector(0.0,0.0);
				vector b = new vector(1.0,1.0);
				Func<vector, double> f = delegate(vector z){
					double x = z[0]; double y = z[1];
					double r = Sqrt(x*x + y*y);
					return x*x/(1+y*y);
				};
				while(convergence != true){
					if(inp[0] == "-tPlainMC"){
						(res, err) = montecarlo.plainMC(f, a, b, N);
						acr = Abs(PI/12.0 - res);
						uncer = 1e-3;
					} else {
						(res, err) = montecarlo.quasiMC(f, a, b, N);
						uncer = 1e-6;
						acr = Abs(PI/12.0 - res);
					}
					WriteLine($"{N}	{err} {res}	{acr}");
					if(err <= uncer || N == 10000){
						convergence = true;
					}
					N+=200;
				}
			}
			if(inp[0] == "-test"){
				Func<vector, double> f = delegate(vector z){
					return 1.0/(1.0 - Cos(z[0])*Cos(z[1])*Cos(z[2]))/(PI*PI*PI);
				};
				Func<vector, double> f1 = delegate(vector z){
					double x = z[0]; double y = z[1];
					double r = Sqrt(x*x + y*y);
					if(r <= 1.0){
						return 1.0;
					} else {
						return 0.0;
					}
				};
				Func<vector, double> f2 = delegate(vector u){
					double x = u[0]; double y = u[1]; double z = u[2];
					double r = Sqrt(x*x+y*y+z*z);
					if(r <= 1.0 && z > 0.0){
						return 1.0;
					} else {
						return 0.0;
					}
				};
				vector a = new vector(-1.0, -1.0);
				vector b = new vector(1.0, 1.0);
				double[] test1 = test(f1, a, b, 100000);
				a = new vector(-1.0, -1.0, -1.0);
				b = new vector(1.0, 1.0, 1.0);
				double[] test2 = test(f2, a, b, 100000);
				a = new vector(0,0,0);
				b = new vector(PI, PI, PI);
				double[] test3 = test(f, a, b, 100000);
				double[][] finale = {test1, test2, test3};
				string[] integrals = {"1. integral of unit circle", "2. integral of half-sphere ", "3. integral of function given in assignment"};
				string[] analytic = {"PI", "2/3*PI", "~1.393203929"};
				double[] analytic1 = {PI, 2.0/3.0*PI, 1.393203929};
				int i = 0;
				WriteLine("Calculating different integrals with N=100000 for different functions");
				WriteLine();
				foreach(double[] rest in finale){
					WriteLine($"{integrals[i]} | Should return I = {analytic[i]}");
					WriteLine($"Result from plainMC-routine with error estimate");
					WriteLine($"I = {Round(rest[0],3)} ± {rest[1]}");
					WriteLine($"Result from qausiMC-routine with error estimate");
					WriteLine($"I = {rest[2]} ± {rest[3]}");
					WriteLine($"Analytic value being:");
					WriteLine($"I ~ {analytic1[i]}");
					i++;
					WriteLine("---------------------------------------------------");
				}
			}
		}
	}
	public static double[] test(Func<vector, double> f, vector a, vector b, int num){
		double[] result = new double[4];
		(double res1, double err1) = montecarlo.plainMC(f, a, b, num);
		(double res2, double err2) = montecarlo.quasiMC(f, a, b, num);
		result[0] = res1; result[1] = err1; result[2] = res2; result[3] = err2;
		return result;
	}

  	public static bool approxn(double a, double b, double epsilon=1e-9, double tau=1e-9){
  		if(Abs(a-b) < tau || Abs(a-b)/(Abs(a) + Abs(b)) < epsilon){
  			return true;
  		} else {
  			return false;
  		}
  	}
}
