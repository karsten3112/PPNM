using System;
using static System.Console;
using static System.Math;

class main{
	static double b=0.25, c=5.0;

	static void Main(string[] args){
		foreach(string arg in args){
			if(arg == "-harmosc"){
				Func<double,vector,vector> F = delegate(double t,vector y){
					double theta=y[0]; double omega=y[1];
			    	return new vector(omega,-theta);
				};
				vector yinit = new vector(1,0);
				double xinit = 0.0;
				double xend = 10.0;
				odeint solve = new odeint(F,xinit,yinit,xend, true);
				genlist<double> ts = solve.xs;
				genlist<vector> ys = solve.ys;
				for(int i = 0; i < ts.size; i++){
					WriteLine($"{ts[i]} {ys[i][0]}");
				}

			}
			if(arg == "-damposc"){

			}
			if(arg == "-lotka"){
				double a = 1.5; double b = 1.0; double c = 3.0; double d = 1.0;
				Func<double,vector,vector> F = delegate(double t, vector y){
					double y0 = y[0];
					double y1 = y[1];
					double y0d=a*y0-b*y1*y0;
					double y1d=-c*y1+d*y1*y0;
					return new vector(y0d,y1d);
				};
				vector yinit = new vector(10, 5);
				double xinit = 0.0;
				double xend = 15.0;
				odeint solve = new odeint(F,xinit,yinit,xend, true, 0.005);
				genlist<double> ts = solve.xs;
				genlist<vector> ys = solve.ys;
				for(int i = 0; i < ts.size; i++){
					WriteLine($"{ts[i]} {ys[i][0]}");
				}
				WriteLine("");
				WriteLine("");
				for(int i = 0; i < ts.size; i++){
					WriteLine($"{ts[i]}	{ys[i][1]}");
				}
			}
		}
	}
}
