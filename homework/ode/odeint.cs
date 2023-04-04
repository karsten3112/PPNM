using static System.Math;
using static System.Console;
using System;

public class odeint{
	public genlist<double> xs; public genlist<vector> ys;
	public double initx; public vector inity;

	public odeint(Func<double, vector, vector> F, double x, vector y, double xend, double h=0.01, double acc=0.01, double eps=0.01, genlist<double> xlist=null, genlist<vector> ylist=null){
		this.initx = x; this.inity = y.copy();
		(genlist<double> xf, genlist<vector> yf) = driver(F, this.initx, this.inity, xend, h, acc, eps);
		this.xs = xf;
		this.ys = yf;
	}

	public (vector, vector) rkstep12(Func<double, vector, vector> F, double x, vector y, double h){
		vector k0 = F(x,y);
		vector k1 = F(x+0.5*h, y+k0*0.5*h);
		vector yh = y+k1*h;
		vector err = (k1-k0)*h;
		return (yh, err);
	}

	public (vector, vector) rkstep45(Func<double, vector, vector> F, double x, vector y, double h){
		vector k1 = F(x,y);
		vector k2 = F(x+(0.25)*h, y + 0.25*k1)*h; //Numbers coming from the Fehlberg butcher table in the ODE chapter
		vector k3 = F(x+(3.0/8.0)*h, y + (3.0/32.0)*k1 + (9.0/32.0)*k2)*h;
		vector k4 = F(x+(12.0/13.0)*h, y + (1932.0/2197.0)*k1 - (7200.0/2197.0)*k2 + (7296.0/2197.0)*k3)*h;
		vector k5 = F(x+h, y + (439.0/216.0)*k1 - (8)*k2 + (3680.0/513.0)*k3 - (845.0/4104.0)*k4)*h;
		vector k6 = F(x+(0.5)*h, y - (8.0/27.0)*k1 + (2)*k2 - (3544.0/2565.0)*k3 + (1859.0/4104.0)*k4 - (11.0/40.0)*k5)*h;

		vector yh = y + (k1*(25.0/216.0) + k3*(1408.0/2565.0) + k4*(2197.0/4104.0) - k5*(1.0/5.0))*h;
		vector err = h*((25.0/216.0 - 16.0/216.0)*k1 + (2197.0/4104.0 - 6656.0/12825.0)*k3 + (2197.0/4104.0 - 28561.0/56430.0)*k4 - (1.0/5.0 + 9.0/50.0)*k5 - (2.0/55.0)*k6);
		return (yh, err);
	}

	public (genlist<double>, genlist<vector>) driver(Func<double, vector, vector> F, double xinit, vector yinit, double xend, double h, double acc, double eps){
		if(xinit > xend){
			throw new ArgumentException("startpoint larger than end point");
		}
		double x = xinit; vector y = yinit;
		genlist<double> xres = new genlist<double>(); xres.add(xinit);
		genlist<vector> yres = new genlist<vector>(); yres.add(yinit);
		do{
			if(x>=xend){
				return (xres, yres);
			}
			if(x+h > xend){
				h = xend -x;
			}
			(vector yh, vector err) = rkstep12(F, x, y, h);
			double tol = (acc + eps*yh.norm())*Sqrt(h/(xend-xinit));
			double erv = err.norm();
			if(erv <= tol){
				x+=h; y=yh;
				xres.add(x);
				yres.add(y);
			}
		h*=Min(Pow(tol/erv,0.25)*0.95, 2);
		}while(true);
	}
}
