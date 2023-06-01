using static System.Math;
using static System.Console;
using System;

public static class integrator{
	public static double[] ws = {2.0/6.0, 1.0/6.0, 1.0/6.0, 2.0/6.0};
	public static double[] vs = {1.0/4.0, 1.0/4.0, 1.0/4.0, 1.0/4.0};
	public static double[] xs = {1.0/6.0, 2.0/6.0, 4.0/6.0, 5.0/6.0};


	public static (double, double) q4calc(Func<double, double> f, double a, double b, double acc=1e-4, double eps=1e-4, Double f2=Double.NaN, Double f3=Double.NaN){
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
			return (Q, Abs(Q-q));
		} else {
			(double a1, double a2) = q4calc(f, a, (b+a)*0.5, acc/Sqrt(2.0), eps, f1, f2);
			(double b1, double b2) = q4calc(f,(b+a)*0.5, b, acc/Sqrt(2.0), eps, f3, f4);
			return (a1 + b1, (b2 + a2)/2.0);
		}
	}

	public static (double, double, int) integrate(Func<double, double> f, double a, double b, double acc=1e-4, double eps=1e-4){
		int n = 0;
		Func<double, double> ft = delegate(double z){
			n++;
			return f(z);
		};
		if(Double.IsNegativeInfinity(a) && !Double.IsPositiveInfinity(b)){
			Func<double, double> fn = delegate(double t){
				return ft(b-(1-t)/t)/t/t;
			};
			n=0;
			(double res, double err) = q4calc(fn, 0,1,acc,eps);
			return (res, err, n);
		}
		if(!Double.IsNegativeInfinity(a) && Double.IsPositiveInfinity(b)){
			Func<double, double> fn = delegate(double t){
				return ft(a+(1-t)/t)/t/t;
			};
			n=0;
			(double res, double err) = q4calc(fn, 0,1,acc,eps);
			return (res, err, n);
		}
		if(Double.IsNegativeInfinity(a) && Double.IsPositiveInfinity(b)){
			Func<double, double> fn = delegate(double t){
				return (ft((1-t)/t) + ft(-(1-t)/t))/t/t;
			};
			n=0;
			(double res, double err) = q4calc(fn, 0,1,acc,eps);
			return (res, err, n);
		} else {
			(double res, double err) = q4calc(ft, a, b, acc, eps);
			return (res, err, n);
		}
	}

	public static (double, double, int) CCtrans(Func<double, double> f, double a, double b, double acc=1e-4, double eps=1e-4){
		Func<double, double> Ft = delegate(double z){
			double result = f((a+b)/2.0+(b-a)/2.0*Cos(z))*Sin(z)*(b-a)/2.0;
			return result;
		};
		return integrate(Ft, 0, PI, acc, eps);
	}

}
