using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
		foreach(string arg in args){
		string[] inp = arg.Split(":");
		if(inp[0] == "-Test"){
			Func<vector, vector> f = delegate(vector xs){
				double x = xs[0]; double y = xs[1];
				vector res = new vector(2);
				res[0] = 2.0*(1-x)-400*(y-x*x)*x;
				res[1] = 200*(y-x*x);
				return res;
			};
			Func<vector, vector> db = delegate(vector xs){
				vector y = new vector(2);
				y[0] = 2*(10-xs[0]);
				y[1] = 2*xs[1];
				return y;
			};
			double x1 = 0.83;
			vector xinit = new vector(x1, x1);
			(vector xf, int count) = rootf.newton(f, xinit);
			WriteLine(count);
			xf.print();
		}

		}
	}
}
