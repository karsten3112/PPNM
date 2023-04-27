using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){
		vector a = new vector(0.0,0.0,0.0);
		vector b = new vector(PI,PI,PI);
		Func<vector, double> f = delegate(vector z){
			return 1.0/(1.0 - Cos(z[0])*Cos(z[1])*Cos(z[2]))/(PI*PI*PI);
		};
		(double res, double err) = montecarlo.plainMC(f, a, b, 10000000);
		WriteLine(res);
	}
}
