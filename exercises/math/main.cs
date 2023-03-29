using static System.Console;
using static System.Math;
using System;

class main{

	public static double sqrt2 = Sqrt(2.0);
	public static double pow2 = Pow(2.0, 1.0/5.0);
	public static double exppi = Exp(PI);
	public static double powpi = Pow(PI, Exp(1));
	public static double[] nums = {1, 2, 3, 10};

	public static void Main(){
		WriteLine($"sqrt(2) = {sqrt2}");
		WriteLine($"sqrt(2)² = {sqrt2*sqrt2} , should be equal to 2");
		WriteLine("--------------------------");
		WriteLine($"2^(1/5) = {pow2}");
		WriteLine($"(2^(1/5))^5 = {Pow(pow2, 5)} , should be equal to 2");
		WriteLine("--------------------------");
		WriteLine($"e^(Pi) = {exppi}");
		WriteLine($"ln(e^(Pi)) = {Log(exppi)} , should be equal to Pi=3.14159....");
		WriteLine("--------------------------");
		WriteLine($"(Pi)^(e) = {powpi}");
		WriteLine($"Log((Pi)^(e)) = {Log(powpi, PI)} , should be equalt to exp=2.7182....");
		WriteLine();
		WriteLine("===============================");
		WriteLine();
		WriteLine("Calculating gamma-function values");

		foreach(double num in nums){
			WriteLine($"gamma({num}) = {sfuns.gamma(num)}");
			WriteLine($"Precision within 6 decimals");
			WriteLine($"gamma({num}) = {Round(sfuns.gamma(num), 6)}");
			WriteLine("--------------------------");
		}

		WriteLine();
		WriteLine("===============================");
		WriteLine();
		WriteLine("Calculating lngamma-function values");


		foreach(double num in nums){
			WriteLine($"lngamma({num}) = {sfuns.lngamma(num)}");
			WriteLine("Precision within 6 decimals");
			WriteLine($"lngamma({num}) = {Round(sfuns.lngamma(num), 6)}");
			WriteLine("--------------------------");
			
		}
	}
}
