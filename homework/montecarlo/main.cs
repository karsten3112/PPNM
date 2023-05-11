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
				int N = 100; double res = 0.0; double err = 2.0;
				vector a = new vector(0.0,0.0);
				vector b = new vector(1.0,1.0);
				Func<vector, double> f = delegate(vector z){
					double x = z[0]; double y = z[1];
					return x*x/(1.0+y*y);
				};
				while(convergence != true){
					if(inp[0] == "-tPlainMC"){
						(res, err) = montecarlo.plainMC(f, a, b, N);
						uncer = 1e-3;
					} else {
						(res, err) = montecarlo.quasiMC(f, a, b, N);
						uncer = 1e-6;
					}
					WriteLine($"{N}	{err} {res}");
					if(err <= uncer){
						convergence = true;
					}
					N+=100;
				}
			}

			if(inp[0] == "-hard"){
				vector a = new vector(0.0,0.0,0.0);
				vector b = new vector(PI,PI,PI);
				Func<vector, double> f = delegate(vector z){
					return 1.0/(1.0 - Cos(z[0])*Cos(z[1])*Cos(z[2]))/(PI*PI*PI);
				};
				(double res, double err) = montecarlo.plainMC(f, a, b, 10000000);
				WriteLine(res);
			}
			if(inp[0] == "-debug"){
				for(int i = 0; i < 100;i++){
					WriteLine($"{corput(i, 2)}");
				}
			}
		}
	}
	public static double corput(int n, int b){
         double q = 0.0; double bk = (double)1.0/b;
         while(n > 0){
             q+= (n % b)*bk;
             n/=b;
             bk/=b;
         }
         return q;
     }

}
