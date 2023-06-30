using static System.Math;
using static System.Console;
using System;

public class odeint{
	public genlist<double> xs; public genlist<vector> ys;
	public double initx; public vector inity;

	public odeint(Func<double, vector, vector> F, double x, vector y, double xend, bool record=false, double h=0.01, double maxstep=0.01, double acc=1e-2, double eps=1e-2){
		this.initx = x; this.inity = y.copy();
		vector yf = driver(F, this.initx, this.inity, xend, h, acc, eps, record, maxstep);
		if(record == false){
			this.xs = new genlist<double>(); this.xs.add(xend);
			this.ys = new genlist<vector>(); this.ys.add(yf);
		}
	}

	public (vector, vector) rkstep12(Func<double, vector, vector> F, double x, vector y, double h){
		vector k0 = F(x,y);
		vector k1 = F(x+0.5*h, y+k0*0.5*h);
		vector yh = y+k1*h;
		vector err = (k1-k0)*h;
		return (yh, err);
	}

	public (vector, vector) rkfstep45(Func<double, vector, vector> F, double x, vector y, double h){
		vector k1 = h*F(x,y);
		vector k2 = h*F(x+(0.25)*h, y + 0.25*k1); //Numbers coming from the Fehlberg butcher table in the ODE chapter
		vector k3 = h*F(x+(3.0/8.0)*h, y + (3.0/32.0)*k1 + (9.0/32.0)*k2);
		vector k4 = h*F(x+(12.0/13.0)*h, y + (1932.0/2197.0)*k1 - (7200.0/2197.0)*k2 + (7296.0/2197.0)*k3);
		vector k5 = h*F(x+h, y + (439.0/216.0)*k1 - (8.0)*k2 + (3680.0/513.0)*k3 - (845.0/4104.0)*k4);
		vector k6 = h*F(x+(0.5)*h, y - (8.0/27.0)*k1 + (2.0)*k2 - (3544.0/2565.0)*k3 + (1859.0/4104.0)*k4 - (11.0/40.0)*k5);

		vector yh = y + (k1*(25.0/216.0) + k3*(1408.0/2565.0) + k4*(2197.0/4104.0) - k5*(1.0/5.0));
		vector err = ((16.0/135.0 - 25.0/216.0)*k1 + (6656.0/12825.0- 1408.0/2565.0)*k3 + (28561.0/56430.0 - 2197.0/4104.0)*k4 + (1.0/5.0 - 9.0/50.0)*k5 + (2.0/55.0)*k6);
		return (yh, err);
	}

	public vector driver(Func<double, vector, vector> F, double xinit, vector yinit, double xend, double h, double acc, double eps, bool record, double maxstep){
		if(xinit > xend){
			throw new ArgumentException("startpoint larger than end point");
		}
		double x = xinit; vector y = yinit; vector tol = new vector(yinit.size);
		if(record == true){
			this.xs = new genlist<double>(); this.xs.add(x);
			this.ys = new genlist<vector>(); this.ys.add(y);
		}
		do{
			bool append = true;
			if(x>=xend){
				return y;
			}
			if(x+h > xend){
				h = xend - x;
			}
			(vector yh, vector err) = rkfstep45(F, x, y, h);
			for(int i = 0; i < yh.size; i++){
				tol[i] = (acc + eps*Abs(yh[i]))*Sqrt(h/(xend-xinit));
			}
			for(int i = 0; i < err.size; i++){
				if(Abs(err[i]) > tol[i]){
					append = false;
				}
			}
			if(append == true){
				x+=h; y=yh.copy();
				if(record == true){
					this.xs.add(x);
					this.ys.add(y);
				}
			} else {
				double factor = tol[0]/Abs(err[0]);
				for(int i = 1; i < err.size; i++){
					factor = Min(factor, tol[i]/Abs(err[i]));
				}
			h*=Min(Pow(factor,0.25)*0.95, 2);
			}
		}while(true);
	}
}
