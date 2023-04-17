using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
			foreach(string arg in args){
			if(arg == "-linspline"){
				(vector xs, vector ys) = gendat(8, -PI, PI);
				for(int i = 0; i < xs.size; i++){
					WriteLine($"{xs[i]}	{ys[i]}");
				}
				vector xp = xdat(500, -PI, PI);
				linspline spline = new linspline(xs, ys);
				WriteLine("");
				WriteLine("");
				for(int i = 0; i < xp.size; i++){
					WriteLine($"{xp[i]}	{spline.evaluate(xp[i])}	{spline.integrate(xp[i])}");
				}
        	}
			if(arg == "-qspline"){
				(vector xs, vector ys) = gendat(8, -PI, PI);
				for(int i = 0; i < xs.size; i++){
					WriteLine($"{xs[i]}	{ys[i]}");
				}
				WriteLine("");
				WriteLine("");

				qspline spline = new qspline(xs, ys);
				vector xp = xdat(500, -PI, PI);

				for(int i = 0; i < xp.size; i++){
					double z1 = spline.evaluate(xp[i]);
					double z2 = spline.derivative(xp[i]);
					double z3 = spline.integrate(xp[i]);
					WriteLine($"{xp[i]}	{z1}	{z2}	{z3}");
				}
			}

			if(arg == "-cspline"){
				(vector xs, vector ys) = gendat(8, -PI, PI);
				for(int i = 0; i < xs.size; i++){
					WriteLine($"{xs[i]}	{ys[i]}");
				}
				cspline spline = new cspline(xs, ys);
				WriteLine("");
				WriteLine("");
				vector xp  = xdat(500, -PI, PI);
				for(int i = 0; i < xp.size; i++){
					double z1 = spline.evaluate(xp[i]);
					double z2 = spline.derivative(xp[i]);
					double z3 = spline.integrate(xp[i]);
					WriteLine($"{xp[i]}	{z1}	{z2}	{z3}");
				}
			}
        }
	}

	public static (vector, vector) gendat(int size, double start=0.0, double end=10.0){
		double dx = (end - start)/size;
		vector xdat = new vector(size + 1);
		vector ydat = new vector(size + 1);
		for(int i = 0; i < size + 1; i++){
			double xi = start + dx*i;
			xdat[i] = xi;
			ydat[i] = Cos(2*xi);
		}
	return (xdat, ydat);
	}

	public static vector xdat(int size, double start=0.0, double end=10.0){
		double dx = (end - start)/size;
		vector result = new vector(size + 1);
		for(int i = 0; i < size+1;i++){
			double xi = start + dx*i;
			result[i] = xi;
		}
	return result;
	}

}
