using static System.Console;
using System;
using static System.Math;

public class ann{
	public int n; public int count;
	public vector bestPs;
	static Random rnd = new System.Random(3);

	Func<double, double> f = delegate(double z){
		return z*Exp(-z*z);
	};

	Func<double, double> fd = delegate(double z){
		return Exp(-z*z)*(1.0-2.0*z*z);
	};

	Func<double, double> fa = delegate(double z){
		return -0.5*Exp(-z*z);
	};

	Func<double, double> fdd = delegate(double z){
		return Exp(-z*z)*(4.0*z*z*z-6.0*z);
	};
	
	public ann(int num){
		this.n = num;
		this.count = 0;
	}

	public ann(int num, vector ps){
		this.n = num;
		this.count = 1;
		this.bestPs = ps.copy();
	}

	public double dresponse(double x, vector ps){
		double sum = 0.0;
		for(int i = 0; i < ps.size; i+=3){
			sum+= this.fd((x-ps[i])/ps[i+1])*ps[i+2]/ps[i+1];
		}
		return sum;
	}

	public double aresponse(double x, vector ps){
		double sum = 0.0;
		for(int i = 0; i < ps.size; i+=3){
			sum+= this.fa((x-ps[i])/ps[i+1])*ps[i+2]*ps[i+1];
		}
		return sum;
	}

	public double ddresponse(double x, vector ps){
		double sum = 0.0;
		for(int i = 0; i < ps.size; i+=3){
			sum+= this.fdd((x-ps[i])/ps[i+1])*ps[i+2]/ps[i+1]/ps[i+1];
		}
		return sum;
	}

	public double response(double x, vector ps){
		double sum = 0.0;
		for(int i = 0; i < ps.size; i+=3){
			sum+=this.f((x-ps[i])/ps[i+1])*ps[i+2];
		}
		return sum;
	}



	public void trainC(Func<vector, double> C, string min="qnewton"){
		double Cres = 0;
		vector pres = new vector(this.n*3);
		vector ps = new vector(3*this.n);
		for(int k = 0; k < 3*this.n; k++){
			ps[k] = -1.0 + rnd.NextDouble()*2.0;
		}
		if(min == "qnewton"){
			qnewtonMin minp = new qnewtonMin(C, ps, 1e-3, 5000);
			pres = minp.xs.copy();
		}
		if(min == "simplex"){
			simplex minp = new simplex(C, ps);
			pres = minp.min;
		}
		if(min == "pso"){
			vector lbound = new vector(this.n*3);
			vector ubound = new vector(this.n*3);
			for(int i = 0; i < lbound.size; i++){
				lbound[i] = -1.0;
				ubound[i] = 1.0;
			}
			pso swarm = new pso(C, lbound, ubound, 100, 5000, 1e-2, 0.5);
			pres = swarm.min;
		}
		if(min != "qnewton" && min != "simplex" && min != "pso"){
			throw new Exception("Minimization routine does not exist");
		}
		Cres = C(pres);
		if(this.count == 0 || Cres < C(this.bestPs)){
			this.bestPs = pres.copy();
		}
		this.count++;
	}

	public void trainint(vector x, vector y, string min="qnewton"){
		Func<vector, double > C = delegate(vector ps){
			double sum = 0;
			for(int i = 0; i < x.size; i++){
				sum+=(response(x[i], ps) - y[i])*(response(x[i], ps) - y[i]);
			}
			return sum;
		};
		trainC(C, min);
	}

	public void traindiff(Func<double, double, double, double, double> phi, vector y, vector dy, double xstart, double xend, string min="qnewton", double a=1.0, double b=1.0){
		Func<vector, double> C = delegate(vector ps){
			double p1 = a*(response(y[0], ps) - y[1])*(response(y[0], ps) - y[1]);
			double p2 = b*(dresponse(dy[0], ps) - dy[1])*(dresponse(dy[0], ps) - dy[1]);
			Func<double, double> f = delegate(double x){
				return phi(ddresponse(x, ps), dresponse(x, ps), response(x, ps), x)*phi(ddresponse(x, ps), dresponse(x, ps), response(x, ps), x);
			};
			(double result, double err) = integrator.integrate(f, xstart, xend);
			return result + p1 + p2;
		};
		trainC(C, min);
	}

	public double response(double x){
		if(this.count == 0){
			throw new Exception("Network has to be trained first");
		}
		return response(x, this.bestPs);
	}

	public double dresponse(double x){
		if(this.count == 0){
			throw new Exception("Network has to be trained first");
		}
		return dresponse(x, this.bestPs);
	}

	public double aresponse(double x){
		if(this.count == 0){
			throw new Exception("Network has to be trained first");
		}
		return aresponse(x, this.bestPs);
	}

	public double ddresponse(double x){
		if(this.count == 0){
			throw new Exception("Network has to be trained first");
		}
		return ddresponse(x, this.bestPs);
	}
}

