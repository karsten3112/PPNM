using static System.Console;
using static System.Math;
using System;

public class bilin{
	public matrix a; public matrix b;
	public matrix c; public matrix d;
	public vector xs; public vector ys;
	
	public bilin(vector x, vector y, matrix F){
		if(x.size < 1 || y.size < 1){
			throw new Exception("Cannot create a grid from 1D series of points");
		}
		this.xs = x.copy();
		this.ys = y.copy();
		(matrix a1, matrix b1, matrix c1, matrix d1) = coeffs(this.xs, this.ys, F);
		this.a = a1.copy();
		this.b = b1.copy();
		this.c = c1.copy();
		this.d = d1.copy();
	}

	public (matrix, matrix, matrix, matrix) coeffs(vector xs, vector ys, matrix F){
		int Ni = xs.size - 1;
		int Nj = ys.size - 1;
		matrix am = new matrix(Ni, Nj);
		matrix bm = new matrix(Ni, Nj);
		matrix cm = new matrix(Ni, Nj);
		matrix dm = new matrix(Ni, Nj);
		for(int i = 0; i < Ni; i++){
			for(int j = 0; j < Nj; j++){
				double a  = F[i,j];
				double b = (F[i+1,j] - a)/(xs[i+1] - xs[i]);
				double c = (F[i,j+1] - a)/(ys[j+1]-ys[j]);
				double d = (F[i+1,j+1] - a - b*(xs[i+1] - xs[i]) - c*(ys[j+1]-ys[j]))/(ys[j+1]-ys[j])/(xs[i+1] - xs[i]);
				am[i,j] = a;
				bm[i,j] = b;
				cm[i,j] = c;
				dm[i,j] = d;
			}
		}

		return (am, bm, cm, dm);
	}

	public double eval(double px, double py){
		int i = binsearch(this.xs, px);
		int j = binsearch(this.ys, py);
		double result = this.a[i,j] + this.b[i,j]*(px - this.xs[i]) + this.c[i,j]*(py - this.ys[j]) + this.d[i,j]*(py - this.ys[j])*(px - this.xs[i]);
		return result;
	}

	public double integrate(double px, double py){ //maybe implement this
		double result = 0; double uy = 0; double ux = 0;
		double ut = 0;
		int i = binsearch(this.xs, px);
		int j = binsearch(this.ys, py);
		for(int k = 0; k <= i; k++){
			for(int s = 0; s <= j; s++){
				if(k == i && s != j){
					ux = (px - this.xs[i]);
					ut = px;
					uy = (this.ys[j+1] - this.ys[j]);
				}
				if(k != i && s == j){
					ux = (this.xs[i+1] - this.xs[i]);
					ut = this.xs[i+1];
					uy = (py - this.ys[j]);
				}
				if(k == i && s == j) {
					ux = (px - this.xs[i]);
					ut = px;
					uy = (py - this.ys[j]);
				} else {
					uy = (this.ys[j+1] - this.ys[j]);
					ux = (this.xs[i+1] - this.xs[i]);
					ut = this.xs[i+1];
				}
				result+= uy*((this.b[i,j] + 0.5*this.d[i,j])*(ux*Cos(ut)-Sin(ut))-Cos(ut)*(this.a[i,j] +0.5*this.b[i,j]));
			}
		}
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
