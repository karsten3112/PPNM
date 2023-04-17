using static System.Math;
using System;
using static System.Console;

public class cspline{

	public vector xs; public vector ys;
	public vector ps; public vector hs;
	public vector cs; public vector ds; public vector bs;
	public matrix A; public vector B;
	public double ein = 0.0; public vector es = null;

	public cspline(vector x, vector y){
		this.xs = x.copy(); this.ys = y.copy();
		this.ps = calcps(this.xs, this.ys);
		this.hs = calchs(this.xs);
		this.A = genmatrix(this.hs);
		this.B = genvector(this.ps, this.hs);
		(matrix Q, vector b) = Gauss_elim(this.A, this.B);
		this.bs = backsub(Q,b);
		this.cs = calccs(this.bs, this.ps, this.hs);
		this.ds = calcds(this.bs, this.ps, this.hs);
	}

	public (matrix, vector) Gauss_elim(matrix A, vector b){
		int n = A.size1;
		matrix Q = new matrix(n, n);
		Q.setid();
		vector D = new vector(n);
		for(int i = 0; i < A.size1-1; i++){
			if(i == 0){
				Q[i,i] = A[i,i];
				D[i] = b[i];
				Q[i,i+1] = A[i,i+1];
			} else {
				Q[i,i] = A[i,i] - Q[i-1,i]/Q[i-1,i-1];
				D[i] = b[i] - D[i-1]/Q[i-1,i-1];
				Q[i,i+1] = A[i,i+1];
			}
		Q[n-1,n-1] = A[n-1,n-1] - Q[n-2,n-1]/Q[n-2,n-2];
		D[n-1] = b[n-1] - D[n-2]/Q[n-2,n-2];
		}
		return (Q, D);
	}


	public vector backsub(matrix A, vector b){
		vector y = new vector(b.size);
		for(int i = b.size - 1; i >= 0; i--){
			double sum = 0;
			for(int k = i+1; k < b.size; k++){
				sum+= A[i,k]*y[k];
			}
			y[i] = (b[i] - sum)/A[i,i];
		}
    return y;
    }

	public static vector calccs(vector bs, vector ps, vector hs){
		vector cs = new vector(ps.size);
		for(int i = 0; i < ps.size; i++){
			cs[i] = (-2.0*bs[i] - bs[i+1] + 3.0*ps[i])/hs[i];
		}
	return cs;
	}

	public static vector calcds(vector bs, vector ps, vector hs){
		vector ds = new vector(ps.size);
		for(int i = 0; i < ps.size; i++){
			ds[i] = (bs[i] + bs[i+1] - 2.0*ps[i])/hs[i]/hs[i];
		}
	return ds;
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

	public static matrix genmatrix(vector hs){
		int n = hs.size + 1;
		matrix mat = new matrix(n,n);
		mat.setid();
		for(int i = 0; i < n-2; i++){
			if(i == 0){
				mat[i,i] = 2.0;
				mat[i,i+1] = 1.0;
			} else {
				mat[i,i] = 2.0*hs[i-1]/hs[i] + 2.0;
				mat[i,i+1] = hs[i-1]/hs[i];
				mat[i,i-1] = 1.0;
			}
		}
		mat[n-2,n-2] = 2.0*hs[n-4]/hs[n-3] + 2.0;
		mat[n-2,n-3] = 1.0;
		mat[n-2, n-1] = hs[n-3]/hs[n-2];
		mat[n-1,n-1] = 2.0;
		mat[n-1,n-2] = 1.0;
	return mat;
	}

	public static vector genvector(vector ps, vector hs){
		int n = ps.size+1;
		vector B = new vector(n);
		for(int i = 0; i<n-2; i++){
			if(i == 0){
				B[i] = 3.0*ps[i];
			} else{
				B[i] = 3.0*(ps[i-1] + ps[i]*hs[i-1]/hs[i]);
			}
		}
		B[n-2] = 3.0*(ps[n-3] + ps[n-2]*hs[n-3]/hs[n-2]);
		B[n-1] = 3.0*ps[n-2];
	return B;
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

	public static vector calchs(vector x){
		vector h = new vector(x.size-1);
		for(int i = 0; i < x.size-1; i++){
			h[i] = x[i+1] - x[i];
		}
	return h;
	}

	public double evaluate(double z){
		int k = binsearch(this.xs, z);
		double result = this.ys[k] + this.bs[k]*(z - this.xs[k]) + this.cs[k]*(z - this.xs[k])*(z - this.xs[k]) + this.ds[k]*(z - this.xs[k])*(z - this.xs[k])*(z - this.xs[k]);
	return result;
	}

	public double derivative(double z){
		int k = binsearch(this.xs, z);
		double result = this.bs[k] + 2.0*this.cs[k]*(z - this.xs[k]) + 3.0*this.ds[k]*(z - this.xs[k])*(z - this.xs[k]);
	return result;
	}

	public double integrate(double z, double einit=0.0){
		if(this.es == null || this.ein != einit){
			this.es = calces(this.xs, this.ys, this.bs, this.cs, this.ds, einit);
		}
		int k = binsearch(this.xs, z);
		double result = this.ys[k]*(z-this.xs[k]) + this.bs[k]*(z-this.xs[k])*(z-this.xs[k])/2.0 + this.cs[k]*(z-this.xs[k])*(z-this.xs[k])*(z-this.xs[k])/3.0 + this.ds[k]*(z-this.xs[k])*(z-this.xs[k])*(z-this.xs[k])*(z-this.xs[k])/4.0 + this.es[k];
	return result;
	}

	public vector calces(vector xs, vector ys, vector bs, vector cs, vector ds, double init){
		vector result = new vector(bs.size);
		for(int i = 0; i < xs.size -1; i++){
			if(i == 0){
				result[i] = init;
			} else {
				result[i] = result[i-1]+ys[i-1]*(xs[i]-xs[i-1])+bs[i-1]*(xs[i]-xs[i-1])*(xs[i]-xs[i-1])/2.0+cs[i-1]*(xs[i]-xs[i-1])*(xs[i]-xs[i-1])*(xs[i]-xs[i-1])/3.0+ds[i-1]*(xs[i]-xs[i-1])*(xs[i]-xs[i-1])*(xs[i]-xs[i-1])*(xs[i]-xs[i-1])/4.0;
			}
		}
	return result;
	}

}
