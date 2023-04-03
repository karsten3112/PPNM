using static System.Math;
using System;


public class qspline{
	public vector x; public vector y;
	public vector b; public vector c; public vector p;
	public qspline(vector xs, vector ys){
		x = xs.copy(); y = ys.copy();
		p = calcps(x, y);
		c = forward_recC(x, y, p); //only runs on a forward recursion for the moment
		b = calcbs(p, c, x);
	}

	public vector forward_recC(vector x, vector y, vector p){
		int n = x.size - 1;
		vector cs = new vector(n);
		double pi = 0; double pi1 = 0; double dx = 0; double dx1 = 0;
		for(int i = 0; i + 2 < x.size; i++){
			if(i == 0){
				cs[i] = 0;
				dx1 = x[i+2] - x[i+1];
				pi1 = p[i+1];
			} else {
				pi = pi1; //We use the values from last recursion
				dx = dx1;
				dx1 = x[i+2] - x[i+1];
				pi1 = p[i+1];
				cs[i] = (pi1 - pi - cs[i-1]*dx)/dx1;
			}
		}
		dx1 = x[n] - x[n-1];
		pi1 = p[n-1];
		dx = x[n-1] - x[n-2];
		pi = p[n-2];
		cs[n-1] = (pi1 - pi - cs[n-2]*dx)/dx1;
		return cs;
	}

	public vector calcps(vector x, vector y){
		int n = x.size - 1;
		vector ps = new vector(n);
		double dx = 0; double pi = 0; double dy = 0;
		for(int i = 0; i+1 < x.size; i++){
			dx = x[i+1] - x[i];
			dy = y[i+1] - y[i];
			pi = dy/dx;
			ps[i] = pi;
		}
		dx = x[n] - x[n-1];
		dy = y[n] - y[n-1];
		pi = dy/dx;
		ps[n-1] = pi;
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
		int k = binsearch(this.x, z);
		double result = this.y[k] + this.p[k]*(z -x[k]) + this.c[k]*(z-x[k])*(z-x[k+1]);//this.b[k]*(z - this.x[k]) + this.c[k]*(z - this.x[k])*(z - this.x[k]);
		return result;
	}

	public double derivative(double z){
		return 2.0;
	}

	public double integral(double z){
		return 2.0;
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
