using static System.Math;
using static System.Console;
using System;

public static class integrator{
	public static double[] ws = {2.0/6.0, 1.0/6.0, 1.0/6.0, 2.0/6.0};
	public static double[] vs = {1.0/4.0, 1.0/4.0, 1.0/4.0, 1.0/4.0};
	public static double[] xs = {1.0/6.0, 2.0/6.0, 4.0/6.0, 5.0/6.0};
	public static int count = 0;
	
	public static double q4calc(Func<double, double> f, double a, double b, double acc=1e-4, double eps=1e-4, Double f2=Double.NaN, Double f3=Double.NaN){
		double h = b - a;
		//if(h<1e-13){
			//throw new ArgumentException("h is too small");
		//}
		if(Double.IsNaN(f2) || Double.IsNaN(f3)){
			f2 = f(a + h*xs[1]);
			f3 = f(a + h*xs[2]);
		}
		double f1 = f(a+h*xs[0]);
		double f4 = f(a+h*xs[3]);
		double Q = h*(ws[0]*f1 + ws[1]*f2 + ws[2]*f3 + ws[3]*f4);
		double q = h*(vs[0]*f1 + vs[1]*f2 + vs[2]*f3 + vs[3]*f4);
		if(Abs(Q-q) <= acc + eps*Abs(Q)){
			return Q;
		} else {
			return q4calc(f, a, (b+a)*0.5, acc/Sqrt(2.0), eps, f1, f2) + q4calc(f,(b+a)*0.5, b, acc/Sqrt(2.0), eps, f3, f4);
		}
	}

	public static (double, int) integrate(Func<double, double> f, double a, double b, double acc=1e-4, double eps=1e-4){
		int n = 0;
		Func<double, double> ft = delegate(double z){
			n++;
			return f(z);
		};
		return (q4calc(ft, a, b, acc, eps), n);
	}

	public static (double, int) CCtrans(Func<double, double> f, double a, double b, double acc=1e-4, double eps=1e-4){
		Func<double, double> Ft = delegate(double z){
			double result = f((a+b)/2.0+(b-a)/2.0*Cos(z))*Sin(z)*(b-a)/2.0;
			return result;
		};
		return integrate(Ft, 0, PI, acc, eps);
	}

}
