using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
		foreach(string arg in args){
			string[] inp = arg.Split(":");
			if(inp[0] == "-test"){
				Func<vector, double> Rs = delegate(vector z){
					double x = z[0]; double y = z[1];
					return (1-x)*(1-x)+100*(y-x*x)*(y-x*x);
				};
				Func<vector, double> Hs = delegate(vector z){
					double x = z[0]; double y = z[1];
					return (x*x+y-11)*(x*x+y-11)+(x+y*y-7)*(x+y*y-7);
				};
				vector xinitRs = new vector(2.0, 3.5);
				vector xinitHs = new vector(-2.0, -4.0);
				qnewtonMin minsRs = new qnewtonMin(Rs, xinitRs, 1e-3,10000);
				qnewtonMin minsHs = new qnewtonMin(Hs, xinitHs, 1e-3,10000);
				simplex fit1 = new simplex(Rs, xinitRs);
				simplex fit2 = new simplex(Hs, xinitHs);
				int tHsq = minsHs.count;
				int tRsq = minsRs.count;
				vector xsRsq = minsRs.xs;
				vector xsHsq = minsHs.xs;
				WriteLine("Finding minima of Himmelblau- and RosenBrock funtion");
				WriteLine("-------------------RosenBrock function--------------------");
				xinitRs.print("Initial vector (x, y) being: ");
				WriteLine("Expected result: x = 1, y = 1");
				WriteLine("Result using QuasiNewton method ....");
				xsRsq.print("Result: ");
				WriteLine($"nr. of iterations: {tRsq}");
				WriteLine("Result using simplex method ....");
				fit1.min.print("Result: ");
				WriteLine($"nr. of iterations: {fit1.count}");
				WriteLine("-------------------Himmelblau function--------------------");
				xinitHs.print("Initial vector (x, y) being: ");
				WriteLine("Expected result: x = -3.779310, y = -3.283186");
				WriteLine("Result using QuasiNewton method ....");
				xsHsq.print("Result: ");
				WriteLine($"nr. of iterations: {tHsq}");
				WriteLine("Result using simplex method ....");
				fit2.min.print("Result: ");
				WriteLine($"nr. of iterations: {fit2.count}");
			}
			if(inp[0]=="-higgs"){
				string file = inp[1];
				genlist<double[]> data = IOhandle.readFile(file);
				genlist<double> energy = new genlist<double>();
				genlist<double> signal = new genlist<double>();
				genlist<double> err = new genlist<double>();

				for(int i = 0; i < data.size; i++){
					double[] dat = data[i];
					energy.add(dat[0]);
					signal.add(dat[1]);
					err.add(dat[2]);
				}
				Func<vector, double, double> Bw = delegate(vector z, double E){
					double m = z[0]; double gam = z[1]; double A = z[2];
					double result = A/((E-m)*(E-m)+gam*gam/4.0);
					return result;
				};
				vector xinit = new vector(130.0, 3.0, 6.0);
				WriteLine("---------------------Determined fitting parameters for Breitt-Wigner function---------------------");
				xinit.print("Initial vector (m, gamma, A) = ");
				WriteLine("Result using QuasiNewton method ....");
				(vector xp, int c) = fit("qnewton", Bw, energy, signal, err, xinit);
				xp.print("Result: ");
				WriteLine($"nr. of iterations: {c}");
				(vector xp1, int c1) = fit("simplex", Bw, energy, signal, err, xinit);
				WriteLine("Result using simplex method ....");
				xp1.print("Result: ");
				WriteLine($"nr. of iterations: {c1}");

				WriteLine("----------------------------------------------------------------------------------------------------\n");
				(double[] xs, double[] ys) = genpoints(Bw, xp, 500, 100, 160);
				(double[] xs1, double[] ys1) = genpoints(Bw, xp1, 500, 100, 160);
				for(int i = 0; i < xs.Length; i++){
					WriteLine($"{xs[i]}	{ys[i]}	{xs1[i]}	{ys1[i]}");
				}
			}

		}


	}

	public static (vector, int) fit(string routine, Func<vector, double, double> f, genlist<double> datax, genlist<double> datay, genlist<double> err, vector init){
		Func<vector, double> chi2 = delegate(vector z){
			double chi = 0.0;
			for(int i = 0; i < datax.size; i++){
				chi+=(f(z, datax[i]) - datay[i])*(f(z, datax[i]) - datay[i])/err[i]/err[i];
			}
			return chi;
		};
		if(routine == "simplex"){
			simplex fit = new simplex(chi2, init);
			return (fit.min, fit.count);
		}
		if(routine == "qnewton"){
			qnewtonMin fit = new qnewtonMin(chi2, init, 1e-3, 10000);
			return (fit.xs, fit.count);
		} else {
			throw new Exception("Method was not found");
		}
	}

	public static (double[], double[]) genpoints(Func<vector, double, double> f, vector z, int N, double xstart, double xend){
		double[] xs = new double[N]; double[] ys = new double[N];
		double deltax = Abs(xend - xstart)/N;
		for(int i = 0; i < N; i++){
			xs[i] = xstart + i*deltax;
			ys[i] = f(z, xstart + i*deltax);
		}
		return (xs, ys);
	}

}
