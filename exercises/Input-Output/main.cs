using static System.Console;
using static System.Math;
using System;

class main{
	static void Main(string[] args){
		foreach(string arg in args){
			if(arg = "file_input"){

			}
		}
		for(string line = ReadLine(); line != "file_done"; line = ReadLine()){	//Kan ikke få det til at virke som Dmitri gør med "line != null".
			double[] nums = handleinput.nums(line);
			foreach(double num in nums) {
				WriteLine($"number = {num} ; Sin({num}) = {Sin(num)} ; Cos({num}) = {Cos(num)}");
			}
		}
	}
}
