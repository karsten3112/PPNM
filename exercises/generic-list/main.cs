using static System.Console;
using static System.Math;
using System;

class main{
	public static void Main(string[] args){
		foreach(string arg in args){
			string[] inp = arg.Split(":");
			if(inp[0] == "-input"){
				genlist<double> dat = IOhandle.getnums(IOhandle.Read(inp[1]));
				WriteLine("Printing the numbers read from input-file");
				for(int i = 0; i < dat.size; i++){
					Write($"| {dat[i]} |");
				}
				Write("\n");
				WriteLine("Testing add function by adding these numbers to already existing list");
				double[] nums = {2,4,6,7};
				foreach(double num in nums){
					dat.add(num);
					Write($"| {num} |");
				}
				Write("\n");
				WriteLine("The List after having added the numbers");
				for(int i = 0; i < dat.size; i++){
					Write($"| {dat[i]} |");
				}
				Write("\n");
			}
			
		}
	}
}
