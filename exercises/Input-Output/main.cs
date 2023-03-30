using static System.Console;
using static System.Math;
using System;


class main{
	public static string file_input;
	public static string file_output;
	public static double[] numbers;
	public static char[] split_delims = {char.Parse(" "), char.Parse("\t"), char.Parse(","), char.Parse("\n")};
	public static double[] numbers_file;
	public static double[] numbers_dir;

	static void Main(string[] args){
		var split_options = StringSplitOptions.RemoveEmptyEntries;

		foreach(string arg in args){
			string[] input = arg.Split(":");
			if(input[0] == "-input"){
				file_input = input[1];
				string file_inp = IOhandle.readString(file_input);
				string[] nums = file_inp.Split(split_delims, split_options);
				numbers_file = new double[nums.Length];
				for(int i = 0; i < nums.Length; i++){
					double num = double.Parse(nums[i]);
					numbers_file[i] = num;
				}
			}
			if(input[0] == "-output"){
				file_output = input[1];
			}
			if(input[0] == "-numbers"){
				string[] seperate = input[1].Split(split_delims, split_options);
				numbers_dir = new double[seperate.Length];
				for(int i = 0; i < seperate.Length; i++){
					double num = double.Parse(seperate[i]);
					numbers_dir[i] = num;
				}
			}
		}
	numbers = concatnums(numbers_file, numbers_dir);
	if(file_output == null){
		foreach(double num in numbers){
			WriteLine($"{num} | cos({num}) = {Cos(num)} | sin({num}) = {Sin(num)}");
		}
	} else {
		string[] lines = new string[numbers.Length];
		for(int i = 0; i < numbers.Length; i++){
			string line = $"{numbers[i]} | cos({numbers[i]}) = {Cos(numbers[i])} | sin({numbers[i]}) = {Sin(numbers[i])}";
			lines[i] = line;
		}
		IOhandle.write(file_output, lines);
	}
	}

	public static double[] concatnums(double[] a, double[] b){
		int s1 = 0;
		int s2 = 0;
		if(a == null){
			s1 = 0;
		} else { s1 = a.Length;}
		if(b == null){
			s2 = 0;
		} else { s2 = b.Length;}
		double[] result = new double[s1 + s2];
		for(int i = 0; i < s1; i++){
			result[i] = a[i];
		}
		for(int i = 0; i < s2; i++){
			result[s1 + i] = b[i];
		}
		return result;
	}
}
