using System;
using static System.Console;
using static System.Math;

class main{
	static double b=0.25, c=5.0;
	static void Main(){
		Func<double,vector,vector> F = delegate(double t,vector y){
			double theta=y[0]; double omega=y[1];
			return new vector(omega,-theta);
		};

		vector yinit = new vector(PI-0.1,0.1);
		double xinit = 0.0;
		double xend = 10.0;
		odeint solve = new odeint(F,xinit,yinit,xend, true);
		genlist<double> ts = solve.xs;
		genlist<vector> ys = solve.ys;
		for(int i = 0; i < ts.size; i++){
			WriteLine($"{ts[i]}	{ys[i][0]}");
		}
	}

	public static vector diff_eq(double z, vector fs){
		vector result = new vector(fs.size);
		result[0] = fs[1];
		result[1] = -fs[0];
		return result;
	}
}
