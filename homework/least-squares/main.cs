using System;
using static System.Console;
using static System.Math;

class main{
	static string input_file;
	static vector xvec;
	static vector yvec;
	static vector dyvec;

	static void Main(string[] args){
		var fs = new Func<double, double>[]{
			z => 1.0,
			z => z,
		};
		foreach(string arg in args){
			string[] input = arg.Split(":");
			if(input[0] == "-input"){
				input_file = input[1];
			}
		}
		if(input_file != null){
			(double[] xs, double[] ys, double[] dys) = IOhandle.readData(input_file, 9);
			xvec = new vector(xs.Length);
			yvec = new vector(xs.Length);
			dyvec = new vector(xs.Length);
			for(int i = 0; i < xs.Length; i++){
				xvec[i] = xs[i];
				yvec[i] = Log(ys[i]);
				dyvec[i] = dys[i]/ys[i];
			}
			(vector cs, matrix cov) = LTSQ.lsfit(fs, xvec, yvec, dyvec);
			double uncer1 = Sqrt(cov[0,0]); double uncer2 = Sqrt(cov[1,1]);
			gendata(1, 16, 400, cs);
			gendata(1, 16, 400, cs, uncer1, uncer2);
			gendata(1, 16, 400, cs, -uncer1, -uncer2);
		}

	}
	public static void gendata(double start, double end, int num, vector c, double uncer1=0, double uncer2=0){
		double dx = (end - start)/(num+1);
		for(int i = 1; i <= num+1; i++){
			double xval = dx*i;
			double yval = Exp(c[0]+uncer1)*Exp((c[1]+uncer2)*xval);
			WriteLine($"{xval}	{yval}");
		}
		WriteLine("");
		WriteLine("");
	}
}
