using static System.Math;
using System;

public class linspline{
	public vector xs;
	public vector ys;
	public vector ps;
	public vector cs; //Making vector of constants after integration demanding continuity

	public linspline(vector x, vector y, double cinit=0.0){
		this.xs = x;
		this.ys = y;
		this.ps = calcps(this.xs, this.ys);
		this.cs = calccs(this.xs,this.ys, this.ps, cinit);
	}

	public static int binsearch(vector x, double z){
		if(!(x[0] <=z && z <=x[x.size -1])){
			throw new Exception("Binsearch; bad z");
		}
		int i=0; int j = x.size-1;
		while(j-i>1){
			int mid = (i+j)/2;
			if(z > x[mid]){
				i = mid;
			} else {
				j = mid;
			}
		}
		return i;
	}
	public static vector calcps(vector x, vector y){
		vector p = new vector(x.size - 1);
		for(int i = 0; i < x.size-1; i++){
			double dy = y[i+1] - y[i];
			double dx = x[i+1] - x[i];
			p[i] = dy/dx;
		}
		return p;
	}

	public double evaluate(double z){
		int k = binsearch(this.xs, z);
		return this.ys[k] + this.ps[k]*(z - this.xs[k]);
	}

	public static vector calccs(vector xs, vector ys, vector ps, double cinit){ //Calculating cs given a certain starting condition and demanding continuity
		vector result = new vector(xs.size-1);
		for(int i = 0; i < xs.size-1; i++){
			if(i == 0){
				result[i] = cinit;
			} else {
				result[i] = ys[i-1]*(xs[i] - xs[i-1]) + ps[i-1]*(xs[i] - xs[i-1])*(xs[i] - xs[i-1])*0.5 + result[i-1];
			}
		}
		return result;
	}

	public double linInteg(double z){
		int k = binsearch(this.xs, z);
		return this.ys[k]*(z - this.xs[k]) + this.ps[k]*(z - this.xs[k])*(z - this.xs[k])*0.5 + this.cs[k];
	}
}
