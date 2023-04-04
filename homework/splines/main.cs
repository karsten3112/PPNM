using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
			foreach(string arg in args){
			if(arg == "-linspline"){
				(vector xs, vector ys) = gendat(50, -PI, PI);
				for(int i = 0; i < xs.size; i++){
					WriteLine($"{xs[i]}	{ys[i]}");
				}
				WriteLine("");
				WriteLine("");
				(vector zinpt, vector d) = gendat(50, -PI, PI);
				linspline spline = new linspline(xs, ys, zinpt); 
				for(int i = 0; i < xs.size; i++){
					WriteLine($"{xs[i]} {spline.z[i]}");
        		}
        		WriteLine("");
        		WriteLine("");
        		for(int i = 0; i < zinpt.size; i++){
        			WriteLine($"{zinpt[i]} {spline.linInteg(xs, ys, zinpt[i])}");
        		}


        	}
			if(arg == "-qspline"){
				(vector xs, vector ys, vector y1s) = qdata(6);
				for(int i = 0; i < xs.size; i++){
					WriteLine($"{xs[i]}	{y1s[i]}");
				}
				WriteLine("");
				WriteLine("");

				qspline spline = new qspline(xs, y1s);
				(vector xp, vector l) = gendat(500, -1, 1);
				for(int i = 0; i < xp.size; i++){
					double z = spline.evaluate(xp[i]);
					WriteLine($"{xp[i]}	{z}");
				}
			}
			if(arg == "-cspline"){

			}
        }
	}

	public static (vector, vector) gendat(int size, double start=0.0, double end=10.0){
		double dx = (end - start)/size;
		vector xdat = new vector(size);
		vector ydat = new vector(size);
		for(int i = 0; i < size; i++){
			double xi = start + dx*i;
			xdat[i] = xi;
			ydat[i] = Cos(2*xi);
		}
	return (xdat, ydat);
	}

	public static (vector, vector, vector) qdata(int size=5){
		vector x = new vector(2*size);
		vector y = new vector(2*size);
		vector y1 = new vector(2*size);
		for(int i = 0; i <size; i++){
			x[i] = -1 + 0.2*i;
			y1[i] = Exp(1.5*x[i]);
		}
		for(int i = 0; i< size; i++){
			x[i+size] = 0.2 + 0.2*i;
			y1[i+size] = Exp(-1.5*x[i+size]);
		} 
	return (x, y, y1);
	}
}
