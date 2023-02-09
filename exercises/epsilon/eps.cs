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
		WriteLine($"THE MAX INT FROM LOOP = {i}");
		WriteLine("COMPARING INT FROM LOOP WITH INT.MAX");
		compareint(i, max);
		i = 1;
		while(i-1 < i){
			i--;
		}
		WriteLine($"THE MIN INT FROM LOOP = {i}");
		WriteLine("COMPARING INT FROM LOOP WITH INT.MIN");
		compareint(i, min);

		WriteLine("#################");
		WriteLine("COMPUTING MACHINE EPSILON");
		machine_eps();

		WriteLine("#################");
		sumfunc();
		WriteLine($"sumA = {sumA}");
		WriteLine($"sumB = {sumB}");

		WriteLine("#################");
		WriteLine("For a = 1.07, b = 1.08");
		WriteLine($"{approx(1.07, 1.08)}");
		WriteLine("For a = 1.00, b = 1.00 + 1e-12");
		WriteLine($"{approx(1.00, 1e-12 + 1.00)}");
	}

	public static void compareint(int val1, int val2){
		if(val1 == val2){
			WriteLine("SUCCESS... THE TWO INTEGERS ARE THE SAME");
		} else {
			WriteLine("FALSE... THE TWO INTEGERS ARE NOT THE SAME");
		}
	}

	public static void machine_eps(){
		while(1+x != 1){
			x/=2;
		}
		x = x*2;
		WriteLine($"THE PRECISION OF TYPE DOUBLE: {x}");
		WriteLine($"SHOULD BE THE SAME AS: 2^(-52) = {eps_double}");
		while((float)(1f + y) != 1){
			y/=2f;
		}
		y = y*2;
		WriteLine($"THE PRECISION OF TYPE FLOAT: {y}");
		WriteLine($"SHOULD BE THE SAME AS: 2^(-23) = {eps_float}");
		
	}

	public static void sumfunc(){
		int n =(int)1E6;
		i = 0;
		while(i != n + 1){
			if(i == 0){
				sumA+=1;
				sumB+=tiny;
				i++;
			} if(i == n) {
				sumB+=1;
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
			WriteLine(a);
			WriteLine(b);
			if(Abs(a-b) < tau || Abs(a-b)/(Abs(a) + Abs(b)) < epsilon){
				return true;
			} else {
				return false;
			}
	}
	
}




