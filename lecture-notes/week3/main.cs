using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(string[] args){
		foreach(string arg in args){
			WriteLine(arg);
			double[] numbers = input.get_numbers_from_args(args);
		}
	}
}
