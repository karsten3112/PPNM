using static System.Console;
using System;

public static class handleinput{
	static char[] split_delimiters = {char.Parse(" "), char.Parse("\t"), char.Parse(","),char.Parse("\n")};
	static string file_input = null;
	static string file_output = null;

	public static double[] nums(string arg){
		string[] opt = arg.Split(":");
		if(opt[0] == "-input"){
			file_input = opt[1];
			WriteLine(opt);
			WriteLine("vi fik input, og anden side;", file_input);
		}
		if(opt[0] == "-output"){
			file_output = opt[1];
			WriteLine("vi fik output, og anden side;", opt[1]);

		}
		if(opt[0] == "-numbers"){
			string nups = opt[1];
			WriteLine("vi fik numbers, og anden side;", opt[1]);
		}
		var split_options = StringSplitOptions.RemoveEmptyEntries;
		double[] result = null;
		for(string line = ReadLine(); line != null ; line = ReadLine()){
			string[] numbers = line.Split(split_delimiters, split_options);
			result = new double[numbers.Length];

			for(int i = 0; i < numbers.Length; i++){
				result[i] = double.Parse(numbers[i]);
			}
		}
	return result;
	}
}
