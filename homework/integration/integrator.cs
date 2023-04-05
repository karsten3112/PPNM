using static System.Math;
using static System.Console;
using System;

public static class integrator{
	public static double[] ws = {2.0/6.0, 1.0/6.0, 1.0/6.0, 2.0/6.0};
	public static double[] vs = {1.0/4.0, 1.0/4.0, 1.0/4.0, 1.0/4.0};
	public static double[] xs = {1.0/6.0, 2.0/6.0, 4.0/6.0, 5.0/6.0};

	public static double integrate(Func<double, double> f, double a, double b, double acc=0.001, double eps=0.001, Double f2=Double.NaN, Double f3=Double.NaN){
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
			return integrate(f, a, (b+a)*0.5, acc/Sqrt(2), eps, f1, f2) + integrate(f,(b+a)*0.5, b, acc/Sqrt(2), eps, f3, f4);
		}
	}
}
