using static System.Console;
using static System.Math;
using System;


public partial class simplex{
	public vector min; public int count = 0;

	public simplex(Func<vector, double> f, vector xinit, double st=1e-1, double acc=1e-5, int maxiter=10000){
		vector[] ps = new vector[xinit.size + 1];
		ps[xinit.size] = xinit.copy();
		for(int i = 0; i < xinit.size; i++){
			vector xstep = xinit.copy();
			xstep[i] = xstep[i] + st;
			ps[i] = xstep.copy();
		}
		this.min = findmin(f, ps, acc, maxiter);
	}

	public vector findmin(Func<vector, double> f, vector[] ps, double acc, int maxiter){
		double s = 0;
		vector pce = new vector(ps[0].size);
		do{
			this.count++;
			(int i, int j) = findmaxmin(f, ps);
			vector phi = ps[i];
			vector plo = ps[j];
			pce = findce(i, ps);
			vector refl = reflection(pce, phi);
			if(f(refl) < f(plo)){
				vector exp = expansion(pce, phi);
				if(f(exp) < f(refl)){
					phi = exp.copy();
					ps[i] = phi.copy();
				} else {
					phi = refl.copy();
					ps[i] = phi.copy();
				}
			} else {
				if(f(refl) < f(phi)){
					phi = refl.copy();
					ps[i] = phi.copy();
				} else {
					vector con = contraction(pce, phi);
					if(f(con) < f(phi)){
						phi = con.copy();
						ps[i] = phi.copy();
					} else {
						ps = reduction(j, ps);
					}
				}
			}
			s = sizecal(ps);
		}while(this.count < maxiter && s > acc);
	return pce;
	}

	public static (int, int) findmaxmin(Func<vector, double> f, vector[] ps){
			int n = 0; int m = 0;
			double hi = 0; double hl = 0;
			for(int i = 0; i < ps.Length; i++){
			if(i == 0){
				hi = f(ps[i]);
				hl = hi;
			} else {
				if(f(ps[i]) > hi){
					hi = f(ps[i]);
					n = i;
				}
				if(f(ps[i]) < hl){
					hl = f(ps[i]);
					m = i;
				}
			}
		}
	return (n, m);
	}

	public static vector findce(int n, vector[] ps){
		vector result = new vector(ps[0].size);
		for(int i = 0; i < ps.Length; i++){
			if(i != n){
				result += ps[i];
			}
		}
		result /= result.size;
		return result;
	}

	public static vector reflection(vector pce, vector phi){
		vector result = pce + (pce - phi);
		return result;
	}

	public static vector expansion(vector pce, vector phi){
		vector result = pce + 2.0*(pce - phi);
		return result;
	}

	public static vector contraction(vector pce, vector phi){
		vector result = pce + 0.5*(phi - pce);
		return result;
	}

	public static vector[] reduction(int n, vector[] ps){
		vector[] result = new vector[ps.Length];
		for(int i = 0; i < ps.Length; i++){
			if(i != n){
				result[i] = 0.5*(ps[i] + ps[n]);
			} else {
				result[i] = ps[i];
			}
		}
	return result;
	}

	public static double sizecal(vector[] ps){
		double result = 0;
		for(int i = 1; i < ps.Length; i++){
			vector d = (ps[0] - ps[i]);
			double c = d.norm();
			if(i == 1){
				result = c;
			} else {
				if(c > result){
					result = c;
				}
			}
		}
	return result;
	}
}
