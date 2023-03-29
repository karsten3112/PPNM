using static System.Math;
using static System.Console;

public static class sfuns{
	public static double gamma(double x){
		if(x < 0){
			return PI/(Sin(PI*x)*gamma(1.0 - x));
		}
		if(x < 9){
			return gamma(1.0 + x)/x;
		}
		double lngamma = x*Log(x + 1/(12*x - 1/(10*x))) - x + 1.0/2.0*(Log(2*PI) - Log(x));
		return Exp(lngamma);
	}

	public static double lngamma(double x){
		if(x <= 0){
			return double.NaN;
		}
		if(x < 9){
			return lngamma(1.0 + x) - Log(x);
		}
		double lngam = x*Log(x + 1/(12*x - 1/(10*x))) - x + 1.0/2.0*(Log(2*PI) - Log(x));
		return lngam;
	}
}
