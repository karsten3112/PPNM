using System;
using static System.Console;
using static System.Math;

class main{
	static string input_file;
	static string output_file;
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
			if(input[0] == "-output"){
				output_file = input[1];
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
			hl(-cs[1], -uncer2);
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
	public static void hl(double c, double uncer){
		string[] vals = new string[8];
		vals[0] = "calculated values of half-life time T = ln(2)/lambda";
		vals[1] = "without uncertainties on lambda;";
		vals[2] = $"T = {Log(2)/c} days";
		vals[3] = "-----------------------------------------------";
		vals[4] = "with uncertainties;";
		vals[5] = $"T± = {Log(2)/(c-uncer)} < {Log(2)/c} < {Log(2)/(c+uncer)} days";
		vals[6] = "According to Google the current determined half-life time is T=3.631±0.0002 days";
		vals[7] = "Which does not lie in our interval of uncertainty meaning the fits and data has gotten a lot better since";
		IOhandle.write(output_file, vals);
	}
}
