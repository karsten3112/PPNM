using static System.Console;
using static System.Math;

class main{
	static double sqrt2 = Sqrt(2.0);
	static double exp = Exp(PI);
	static double pi = Pow(PI, Exp(1));
	static double Pw2 = Pow(2, (double)1/5);
	static int[] xvals = {1,2,3,10};
	
	static void Main(){
		WriteLine("COMPUTED VALUES FOR PART 1:");
		Write($"sqrt(2) = {sqrt2}\n");
		Write($"e^pi = {exp}\n");
		Write($"pi^e = {pi}\n");
		Write($"2^(1/5) = {Pw2}\n");
		WriteLine("TESTING THE COMPUTED VALUES");
		
		WriteLine("PART 2: GAMMAFUNCTION IMPLEMENTATION");
		foreach(int i in xvals){ 		
			Write($"Gamma({i}) = {sfuns.gamma(i)}\n");
		}

		WriteLine("PART 3: LNGAMMAFUNCTION IMPLEMENTATION");
		foreach(int i in xvals){
             Write($"Gamma({i}) = {sfuns.gamma(i, true)}\n");
         }

	}
}
