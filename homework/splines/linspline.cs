using static System.Math;
using System;

public class linspline{
	public vector x;
	public vector y;
	public vector z;

	public linspline(vector xs, vector ys, vector zs){
		x = xs;
		y = ys;
		z = new vector(zs.size);
		for(int i = 0; i < zs.size; i++){
			z[i] = lininterp(xs, ys, zs[i]);
		}
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
	public static double lininterp(vector x, vector y, double z){
		int i = binsearch(x, z);
		double dy = y[i+1] - y[i];
		double dx = x[i+1] - x[i];
		if(dx > 0.0){
			return y[i] + dy/dx*(z - x[i]);
		} else {
			throw new Exception("dx is strictly positive");
		}
	}

	public double linInteg(vector x, vector y, double z){
		int k = binsearch(x, z);
		double sum = 0; double dy; double dx;
		for(int j = 0; j+1 < k; j++){
			dy = y[j+1] - y[j];
			dx = x[j+1] - x[j];
			sum+= y[j]*(x[j+1] - x[j]) + dy/dx*(x[j+1] - x[j])*(x[j+1] - x[j])/2.0;
		}
		dy = y[k+1] - y[k];
		dx = x[k+1] - x[k];
		sum+= y[k]*(z - x[k]) + dy/dx*(z - x[k])*(z - x[k])/2.0;
		return sum;
	}
}
