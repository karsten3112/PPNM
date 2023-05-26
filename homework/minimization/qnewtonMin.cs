using static System.Console;
using static System.Math;
using System;


public partial class qnewtonMin{
	public vector delx; public vector xs;
	public int count;
	public qnewtonMin(Func<vector, double> f, vector xinit, double acc=1e-3, int maxiter=5000){
		(vector xf, int counts) = findmin(f, xinit, acc, maxiter);
		this.xs = xf.copy();
		this.count = counts;
	}

	public (vector, int) findmin(Func<vector, double> f, vector xinit, double acc, int maxiter){
		matrix B = new matrix(xinit.size, xinit.size); int i = 0;
		vector gradf = new vector(xinit.size);
		vector xs = xinit.copy();
		vector delx = new vector(xinit.size);
		B.setid();
		do{
			i++;
			gradf = gradient(f, xs);
			delx = -B*gradf;
			double lambda = 1.0;
			while(true){
				if(f(xs + lambda*delx) < f(xs)){
					B = B + SR1upd(f, B, xs, lambda*delx);
					xs = xs + lambda*delx;
					break;
				}
				lambda/=2.0;
				if(lambda < 1.0/1024.0){
					xs = xs + lambda*delx;
					B.setid();
					break;
				}
			}
		}while(gradf.norm() > acc && i < maxiter);

	return (xs, i);
	}

	public matrix Broyden(Func<vector, double> f, matrix B, vector xs, vector delx, double eps=1e-6){ //Regular Broyden update
		vector s = delx;
		matrix delB = new matrix(xs.size, xs.size);
		vector y = gradient(f, xs + s) - gradient(f, xs);
		vector u = s - B*y;
		double prod = s.dot(y);
		if(Abs(prod) < Pow(2.0,-26.0)){
			prod = Pow(2.0,-26.0);
		}
		matrix cd = new matrix(u.size, 1);
		cd[0] = u/prod;
		matrix sd = new matrix(s.size, 1);
		sd[0] = s;
		matrix l = sd.transpose();
		delB = cd*l;
		return delB;
	}

	public matrix SR1upd(Func<vector, double> f, matrix B, vector xs, vector delx, double eps=1e-6){ //Symmetric rank-1 update
		vector s = delx;
		matrix delB = new matrix(xs.size, xs.size);
		vector y = gradient(f, xs + s) - gradient(f, xs);
		vector u = s - B*y;
		double prod = u.dot(y);
		if(Abs(prod) < Pow(2.0,-26.0)){
			prod = Pow(2.0,-26.0);
		}
		matrix un = new matrix(u.size, 1); //Smart way to transpose vectors?
		un[0] = u;
		matrix ut = un.transpose();
		delB = un*ut/prod;
	return delB;
	}


	public vector gradient(Func<vector, double> f, vector xs){
		vector grad = new vector(xs.size);
		vector xstep = xs.copy();
		double deltax = xs.norm()*Pow(2.0,-26.0);
		for(int i = 0; i < grad.size; i++){
			xstep[i]+= deltax;
			grad[i] = (f(xstep) - f(xs))/deltax;
			xstep = xs.copy();
		}
	return grad;
	}

}

