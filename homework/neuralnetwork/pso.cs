using static System.Math;
using static System.Console;
using System;



public partial class pso{
	static Random rnd = new System.Random();
	public vector min; public int count;
	public vector g;
	public pso(Func<vector, double> f, vector lbound, vector ubound, int N=100, int sat=10000, double conv=1e-2, double delt=1.0){
		vector scaling = ubound - lbound;
		part[] particles = new part[N];
		for(int i = 0; i < N; i++){
			vector xinit = new vector(scaling.size);
			vector vinit = new vector(scaling.size);
			for(int j = 0; j < xinit.size; j++){
				double x = lbound[j] + rnd.NextDouble()*(ubound[j]-lbound[j]);
				double v = ((lbound[j] - ubound[j])*0.5 + rnd.NextDouble()*(ubound[j]-lbound[j]))/delt;
				xinit[j] = x;
				vinit[j] = v;
			}
			particles[i] = new part(xinit, vinit);
		}
		findG(particles, f);
		this.min = run(particles, f, conv, sat);
	}

	public vector run(part[] partlist, Func<vector, double> f, double acc, int sat){
		bool c = false;
		do{
			this.count++;
			for(int i = 0; i < partlist.Length; i++){
				double u = rnd.NextDouble();
				partlist[i].setV(this.g, u);
				partlist[i].move();
				partlist[i].setP(f);
			}
			findG(partlist, f);
			c = conv(partlist, acc);
		}while(this.count < sat && c != true);
		return this.g;
	}

	public void findG(part[] partlist, Func<vector, double> f){
		for(int i = 0; i < partlist.Length; i++){
			if(this.g == null){
				this.g = partlist[i].p.copy();
			} else {
				if(f(partlist[i].p) < f(this.g)){
					this.g = partlist[i].p.copy();
				}
			}
		}
	}

	public bool conv(part[] partlist, double acc){
		bool result = true;
		for(int i = 1; i < partlist.Length;i++){
			double size = (partlist[0].x - partlist[i].x).norm();
			if(size > acc){
				result = false;
				return result;
			}
		}
		return result;
	}

}

public partial class part{
	public vector p; public vector x; public vector v;

	public part(vector xs, vector vs){
		this.p = xs.copy();
		this.x = xs.copy();
		this.v = vs.copy();
	}
	
	public void setV(vector g, double u, double delt=1.0, double damp=0.72){
		this.v = this.v*damp + u*(this.p - this.x)/delt + u*(g-this.x)/delt;
	}
	
	public void move(double delt=1.0){
		this.x+=this.v*delt;
	}
	
	public void setP(Func<vector, double> f){
		if(f(this.x) < f(this.p)){
			this.p = this.x.copy();
		}
	}
}
