using static System.Console;
using static System.Math;
using System;

class main{
	static void Main(string[] args) {
		if(args.Length == 0){
			WriteLine("We did not parse a file name");
		}
		foreach(string arg in args) {
			WriteLine(arg);
			double[] nums = handleinput.nums(arg);
			foreach(double num in nums) {
				WriteLine($"number = {num} ; Sin({num}) = {Sin(num)} ; Cos({num}) = {Cos(num)}");
			}
		}
	}
}
