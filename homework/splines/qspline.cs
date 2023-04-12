using static System.Math;
using System;


public class qspline{
	public vector xs; public vector ys;
	public vector bs; public vector cs; public vector ps;
	public vector dxs;

	public qspline(vector x, vector y){
		this.xs = x.copy();
		this.ys = y.copy();
		this.ps = calcps(this.xs, this.ys);
		this.dxs = calcdxs(this.xs);
		vector cf = forward_recC(this.dxs, this.ps); //only runs on a forward recursion for the moment
		vector cb = backward_recC(this.dxs, this.ps);
		this.cs = 0.5*(cb+cf);
		this.bs = calcbs(this.cs, this.ps, this.xs);
	}

	public vector forward_recC(vector dx, vector p){
		int n = dx.size-1;
		vector cs = new vector(n);
		for(int i = 0; i < n; i++){
			if(i == 0){
				cs[i] = 0;
			} else {
				cs[i] = (p[i] - p[i-1] - cs[i-1]*dx[i-1])/dx[i];
			}
		}
		cs[n-1] = (p[n-1] - p[n-2] - cs[n-2]*dx[n-2])/dx[n-1];
	return cs;
	}

	public vector backward_recC(vector dx, vector p){
		int n = dx.size-1;
        vector cs = new vector(n);
        for(int i = n-1; i > 0; i--){
			if(i == n-1){
				cs[i] = 0.0;
			} else {
				cs[i] = (p[i+1] - p[i] - cs[i+1]*dx[i+1])/dx[i];
			}
		}
		cs[0] = (p[1] - p[0] - cs[1]*dx[1])/dx[0];
    return cs;
    }

	public static vector calcdxs(vector x){
		vector dxs = new vector(x.size-1);
		for(int i = 0; i < x.size-1; i++){
			dxs[i] = x[i+1] - x[i];
		}
		return dxs;
	}

	public static vector calcps(vector x, vector y){
		vector ps = new vector(x.size - 1);
		for(int i = 0; i < x.size-1; i++){
			double dy = y[i+1] - y[i];
			double dx = x[i+1] - x[i];
			ps[i] = dy/dx;
		}
	return ps;
	}

	public vector calcbs(vector c, vector p, vector x){
		vector bs = new vector(c.size);
		double dx = 0;
		for(int i = 0; i < c.size; i++){
			dx = x[i+1] - x[i];
			bs[i] = p[i] - c[i]*dx;
		}
	return bs;
	}

	public double evaluate(double z){
		int k = binsearch(this.xs, z);
		double result = this.ys[k] + this.bs[k]*(z - this.xs[k]) + this.cs[k]*(z - this.xs[k])*(z - this.xs[k]);
	return result;
	}

	public double derivative(double z){
		int k = binsearch(this.xs, z);
		double result = this.bs[k] + 2*this.cs[k]*(z - this.xs[k]);
	return result;
	}

	public double integral(double z){
		int k = binsearch(this.xs, z);
		double result = this.ys[k]*(z - this.xs[k]) + this.bs[k]*(z - this.xs[k])*(z - this.xs[k])/2 + this.cs[k]*(z - this.xs[k])*(z - this.xs[k])*(z - this.xs[k])/3;
		return result;
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

}
