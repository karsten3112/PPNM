using System;
using static System.Console;
using static System.Math;

public static partial class sfuns{
	public static double rgamma(double x){
		if(x < 0){
			return PI/Sin(PI*x)/rgamma(1-x);
		}
		if(x < 9){
			return rgamma(x+1)/x;
		}
		double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
	 return Exp(lngamma);
	}

	public static double lngamma(double x){
		if(x < 0){
			throw new Exception("x has to be > 0");
		}
		if(x < 9){
			return lngamma(x+1)-Log(x);
		}
		double lngamma1=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
	return lngamma1;
	}

	public static complex cgamma(complex z, bool ln=false){
		if(z.Re < 0){
			return PI/cmath.sin(PI*z)/cgamma(1 - z);
		}
		if(z.Re < 9){
			return cgamma(z + 1)/z;
		}
		complex lngamma = z*cmath.log(z+1/(12*z-1/z/10))-z + cmath.log(2*PI/z)/2;
	return cmath.exp(lngamma);
	}
}
