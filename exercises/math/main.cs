using static System.Console;
using static System.Math;

class calculate{
	static double sqrt2 = Sqrt(2.0);
	static double exp = Exp(PI);
	static double pi = Pow(PI, Exp(1));
	static void Main(){
		Write($"sqrt(2) = {sqrt2}\n");
		Write($"e^pi = {exp}\n");
		Write($"pi^e = {pi}\n");
	}
}
