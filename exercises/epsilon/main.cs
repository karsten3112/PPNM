using static System.Console;
using static System.Math;

class eps{
	static int max = int.MaxValue;
	static int min = int.MinValue;
	static int i = 1;
	static double eps_float = Pow(2, -23);
	static double eps_double = Pow(2, -52);
	static double x = 1.0;
	static float y = 1.0f;

	static double tiny = eps_double/2;
	static double sumA, sumB = 0;
	
	static void Main(){
		while(i+1 > i){
			i++;
		}
		WriteLine($"The max integer from loop = {i}");
		WriteLine("Comparing with int.MaxValue");
		compareint(i, max);

		WriteLine("#####################################");

		i = 1;
		while(i-1 < i){
			i--;
		}
		WriteLine($"The min integer from loop = {i}");
		WriteLine("Comparing with int.MinValue");
		compareint(i, min);

		WriteLine("#####################################");

		WriteLine("Computing machine epsilon");
		machine_eps();

		WriteLine("#####################################");

		WriteLine("Calculating the two different sums, sumA and SumB");
		sumfunc();
		WriteLine($"sumA = {sumA}");
		WriteLine($"sumB = {sumB}");

		WriteLine("Comparing the two sums");
		bool s = approx(sumA, sumB,1e-12, 1e-12);
		if(s){
			WriteLine("TRUE.... The two sums are the same");
		} else {
			WriteLine("FALSE.... The two sums are not the same");
		}

		WriteLine("Which is expected since double and float types always will be rounded to the nearest representable value, due to their binary representation");
		WriteLine("For example numbers like; pi, sqrt(2), exp(1) ... - cannot be represented via double or float types");
		WriteLine("Meaning since sumA=1 from the beginning it will always be rounded to 1, because since we add half of machine epsilon in each term, which is not representable.");

		WriteLine("#####################################");
		WriteLine("Comparing two doubles method with relative acc; 1e-9 and absolute acc 1e-9");
		WriteLine("For a = 1.07, b = 1.08");
		WriteLine($"{approx(1.07, 1.08)}");
		WriteLine("For a = 1.00, b = 1.00 + 1e-12");
		WriteLine($"{approx(1.00, 1e-12 + 1.00)}");
	}

	public static void compareint(int val1, int val2){
		WriteLine("Comparing....");
		if(val1 == val2){
			WriteLine("TRUE... the two integers have the same value");
		} else {
			WriteLine("FALSE... the two integers do not have the same value");
		}
	}

	public static void machine_eps(){
		while(1+x != 1){
			x/=2;
		}
		x = x*2;
		WriteLine($"The Precision of the type double; {x}");
		WriteLine($"Should be the same as; 2^(-52) = {eps_double}");

		if(approx(x, eps_double)){
			WriteLine("TRUE.... they have the same value");
		} else {
			WriteLine("FALSE.... they do not have the same value");
		}

		while((float)(1f + y) != 1){
			y/=2f;
		}
		y = y*2;
		WriteLine($"The Precision of the type float; {y}");
		WriteLine($"Should be the same as; 2^(-23) = {eps_float}");
		if(approx(y, eps_float)){
			WriteLine("TRUE.... they have the same value");
		} else {
			WriteLine("FALSE.... they do not have the same value");
		}
	}

	public static void sumfunc(){
		int n =(int)1E6;
		i = 0;
		while(i != n + 1){
			if(i == 0){
				sumA+=1.0;
				sumB+=tiny;
				i++;
			} if(i == n) {
				sumB+=1.0;
				sumA+=tiny;
				i++;
			} else {
				sumA+=tiny;
				sumB+=tiny;
				i++;
			}
		}
	}

	public static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9){
			WriteLine("Comparing......");
			if(Abs(a-b) < tau || Abs(a-b)/(Abs(a) + Abs(b)) < epsilon){
				return true;
			} else {
				return false;
			}
	}
	
}




