using static System.Console;
using System;
using static System.Math;

public class ann{
	public int n; public vector ps;
	public vector binits;
	static Random rnd = new System.Random(1);
	Func<double, double> f = delegate(double z){
		return z*Exp(-z*z);
	};
	public ann(int num){
		this.n = num;
	}

	public double response(double x, vector p){
		double sum = 0.0;
		for(int i = 0; i < p.size; i+=3){
			sum+=this.f((x-p[i])/p[i+1])*p[i+2];
		}
		return sum;
	}

	public void train(vector x, vector y, double acc=1e-3, int sat = 5000){
		int dim = x.size; int count = 0; double Cres = 0;
		Func<vector, double> C = delegate(vector z){
			double sum = 0.0;
			for(int i = 0; i < dim; i++){
				sum+=(response(x[i], z) - y[i])*(response(x[i], z) - y[i]);
			}
			return sum/dim;
		};
		do{
			vector zinit = new vector(3*this.n);
			for(int k = 0; k < zinit.size; k++){
				zinit[k] = rnd.NextDouble();
			}
			if(count == 0){
				this.ps = zinit.copy();
			}
			qnewtonMin minp = new qnewtonMin(C, zinit, 1e-3, 5000);
			vector pres = minp.xs;
			Cres = C(pres);
			if(Cres < C(this.ps)){
				this.ps = pres.copy();
				this.binits = zinit.copy();
			}
			count++;
		}while(Cres < acc && count < sat);
	}

}
