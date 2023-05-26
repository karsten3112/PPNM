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

				qnewtonMin minsRs = new qnewtonMin(Rs, xinitRs, 1e-3,10000);
				qnewtonMin minsHs = new qnewtonMin(Hs, xinitRs, 1e-3,10000);
				int tHs = minsHs.count;
				int tRs = minsRs.count;
				vector xsRs = minsRs.xs;
				vector xsHs = minsHs.xs;
				WriteLine("RosenBrock function");
				WriteLine(tRs);
				for(int i = 0; i < xsRs.size; i++){
					WriteLine($"{xsRs[i]}");
				}
				WriteLine("Himmelblau function");
				WriteLine(tHs);
				for(int i = 0; i < xsHs.size; i++){
					WriteLine($"{xsHs[i]}");
				}
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
				vector xinit = new vector(123, 3.0, 6.0);
				(vector xp, int c) = fit(Bw, energy, signal, err, xinit);
				(double[] xs, double[] ys) = genpoints(Bw, xp, 500, 100, 160);
				for(int i = 0; i < xs.Length; i++){
					WriteLine($"{xs[i]}	{ys[i]}");
				}
			}

		}


	}

	public static (vector, int) fit(Func<vector, double, double> f, genlist<double> datax, genlist<double> datay, genlist<double> err, vector init){
		Func<vector, double> chi2 = delegate(vector z){
			double chi = 0.0;
			for(int i = 0; i < datax.size; i++){
				chi+=(f(z, datax[i]) - datay[i])*(f(z, datax[i]) - datay[i])/err[i]/err[i];
			}
			return chi;
		};
		qnewtonMin fit = new qnewtonMin(chi2, init, 1e-3, 10000);
		return (fit.xs, fit.count);
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
