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

				qnewton minsRs = new qnewton(Rs, xinitRs, 1e-3,10000);
				qnewton minsHs = new qnewton(Hs, xinitRs, 1e-3,10000);
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

		}

	}
}
