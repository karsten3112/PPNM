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
				yvec[i] = ys[i];
				dyvec[i] = dys[i];
			}
			vector cs = LTSQ.lsfit(fs, xvec, yvec, dyvec);
		}
		
	}
}
