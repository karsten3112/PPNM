using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
		foreach(string arg in args){
			if(arg == "-Test"){
				string[] ints = {"sqrt(x)","1/sqrt(x)","4*sqrt(1-x^2)","ln(x)/sqrt(x)"};
				Func<double, double> f1 = delegate(double z){
					return Sqrt(z);
				};
				Func<double, double> f2 = delegate(double z){
					return 1.0/Sqrt(z);
				};
				Func<double, double> f3 = delegate(double z){
					return 4*Sqrt(1-z*z);
				};
				Func<double, double> f4 = delegate(double z){
					return Log(z)/Sqrt(z);
				};
				(double res1, double err1, int n1) = integrator.integrate(f1, 0,1.0);
				(double res2, double err2, int n2) = integrator.integrate(f2, 0,1.0);
				(double res3, double err3, int n3) = integrator.integrate(f3, 0,1.0);
				(double res4, double err4, int n4) = integrator.integrate(f4, 0,1.0);
				double[] results = {res1, res2, res3, res4};
				double[] errs = {err1, err2, err3, err4};
				int[] ns = {n1,n2,n3,n4};
				(res1, err1, n1) = integrator.CCtrans(f1, 0,1.0);
                (res2, err2, n2) = integrator.CCtrans(f2, 0,1.0);
                (res3, err3, n3) = integrator.CCtrans(f3, 0,1.0);
                (res4, err4, n4) = integrator.CCtrans(f4, 0,1.0);
                double[] results1 = {res1,res2,res3,res4};
				double[] errs1 = {err1, err2, err3, err4};
                int[] ns1 = {n1,n2,n3,n4};
				double[] actual = {2.0/3.0,  2.0, PI, -4.0};
				WriteLine("Calculating the integrals with abs. precission; 1e-4 and rel. prec.; 1e-4");
				for(int i = 0; i < results.Length;i++){
					WriteLine("-------------------------------------------------------------------");
					WriteLine($"Integral nr. {i+1} of f(x) = {ints[i]} over x -> [0;1] | should be I = {actual[i]} - Calculating via our integration methods");
					WriteLine($"From regular q4; I = {results[i]} ± {errs[i]} nr. of splits = {ns[i]}");
					WriteLine($"From CC-trans; I = {results1[i]} ± {errs1[i]} nr. of splits = {ns1[i]}");
					WriteLine("Checking with approx method");
					WriteLine($"{approx(results[i], actual[i], 1e-3, 1e-3)}");
				}
				Func<double, double> f5 = delegate(double z){
					return z*Exp(-z*z);
				};
				Func<double, double> f6 = delegate(double z){
					return Exp(-z*z);
				};
				(res1, err1, n1) = integrator.integrate(f5, 0,Double.PositiveInfinity);
				(res2, err2, n2) = integrator.integrate(f6, Double.NegativeInfinity, Double.PositiveInfinity);
				double[] results2 = {res1, res2};
				double[] errs2 = {err1, err2};
				double[] actual1 = {0.5, Sqrt(PI)};
				double[] ns2 = {n1, n2};
				string[] ints1 = {"x*e^(-x^2)", "e^(-x^2)"};
				string[] bounds1 = {"0;+inf", "-inf;+inf" };
				WriteLine("==================================INFINITE INTEGRALS===========================================");
				for(int i = 0; i < results2.Length;i++){
					WriteLine($"Integral nr. {i+1} of f(x) = {ints1[i]} over x -> [{bounds1[i]}] | should be I = {actual1[i]} - Calculating via our integration method");
					WriteLine($"From regular q4; I = {results2[i]} ± {errs2[i]} nr. of splits = {ns2[i]}");
					WriteLine("Checking with approx method");
					WriteLine($"{approx(results2[i], actual1[i], 1e-3, 1e-3)}");
					WriteLine("-------------------------------------------------------------------");

				}
			}

			if(arg == "-erf"){
				Func<double, double> erf = delegate(double z){
					return erf1(z);
				};
				double start = -3.0, end = 3.0; int npoints = 50; double dx = (end - start)/npoints;
				for(int i = 0; i < npoints; i++){
					double xdat = start + i*dx;
					double ydat = erf(xdat);
					WriteLine($"{xdat}	{ydat}");
				}
				WriteLine("");
				WriteLine("");
				for(int i = 0; i < npoints; i++){
					double xdat = start + i*dx;
					double ydat = erf2(xdat);
					WriteLine($"{xdat}  {ydat}");
				}
			}
		}
	}

	public static double erf1(double z){
		Func<double, double> f1 = delegate(double x){
			return Exp(-x*x);
		};
		if(z < 0.0){
			return -erf1(-z);
		}
		if(z <= 1.0 && z >= 0.0){
			(double res, double err) = integrator.q4calc(f1, 0, z,1e-9,1e-9);
			double result = 2.0*res/Sqrt(PI);
			return result;
		} else {
			Func<double, double> f2 = delegate(double t){
				return Exp(-Pow((z+(1.0-t)/t),2))/(t*t);
			};
			(double res, double err) = integrator.q4calc(f2, 0, 1.0,1e-9,1e-9);
			double result = 1.0 - 2.0*res/Sqrt(PI);
			return result;
		}
	}

	static double erf2(double x){
		if(x<0) return -erf2(-x);
		double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
		double t=1/(1+0.3275911*x);
		double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));
		return 1-sum*Exp(-x*x);
	}

	public static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9){
		WriteLine("Comparing......");
        if(Abs(a-b) < tau || Abs(a-b)/(Abs(a) + Abs(b)) < epsilon){
        	return true;
        } else {
            return false;
        }
    }


}
