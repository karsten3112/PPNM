using static System.Console;
using static System.Math;
using System;

class main{
	static complex var1 = new complex(1,0);
	static complex var2 = new complex(0,1);

	public static void Main(string[] args){
		complex sqrti = cmath.sqrt(var2);
		complex sqrtn = cmath.sqrt(-var1);
		complex exi = cmath.exp(var2);
		complex exip = cmath.exp(var2*PI);
		complex iPowi = cmath.pow(var2, var2);
		complex logi = cmath.log(var2);
		complex sinip = cmath.sin(var2*PI);
		WriteLine("Calculating sqrt(-1) ....");
		WriteLine(sqrtn);
		WriteLine("sqrt(-1) = +-i (should be) - comparing....");
		WriteLine($"{sqrtn.approx(+-var2)}");

		WriteLine("---------------------------------------------------");

		WriteLine("Calculating sqrt(i) ....");
		WriteLine(sqrti);
		WriteLine("sqrt(i) = 1/sqrt(2) + i/sqrt(2) (should be) - comparing....");
		WriteLine($"{sqrti.approx(var1/cmath.sqrt(2)+var2/cmath.sqrt(2))}");

		WriteLine("---------------------------------------------------");

		WriteLine("Calculating exp(i) ....");
		WriteLine(exi);
		WriteLine("exp(i) = cos(1) + i*sin(1) (should be) - comparing....");
		WriteLine($"{exi.approx(cmath.cos(var1) + var2*cmath.sin(var1))}");

		WriteLine("---------------------------------------------------");

		WriteLine("Calculating exp(pi*i) ....");
		WriteLine(exip);
		WriteLine("exp(pi*i) = -1 (should be) - comparing....");
		WriteLine($"{exip.approx(-var1)}");

		WriteLine("---------------------------------------------------");

		WriteLine("Calculating i^i ....");
		WriteLine(iPowi);
		WriteLine("i^i = e^(-pi/2) (should be) - comparing....");
		WriteLine($"{iPowi.approx(cmath.exp(-var1*PI/2.0))}");

		WriteLine("---------------------------------------------------");

		WriteLine("Calculating ln(i) ....");
		WriteLine(logi);
		WriteLine("ln(i) = i*pi/2 (should be) - comparing....");
		WriteLine($"{logi.approx(var2*PI/2.0)}");

		WriteLine("---------------------------------------------------");

		WriteLine("Calculating sin(i*pi) ....");
		WriteLine(sinip);
		WriteLine("sin(i*pi) = i*sinh(pi) (should be) - comparing....");
		WriteLine($"{sinip.approx(var2*(cmath.exp(PI)-cmath.exp(-PI))/2.0)}");
	}
}
