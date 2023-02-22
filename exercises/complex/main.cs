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
		WriteLine(sqrti);

	}
}
