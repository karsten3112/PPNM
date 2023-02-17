using static System.Console;
using System;

public static class handleinput{
	static char[] split_delimiters = {char.Parse(" "), char.Parse("\t"), char.Parse(","),char.Parse("\n")};

	public static double[] nums(string arg){
		var split_options = StringSplitOptions.RemoveEmptyEntries;
		double[] result = null;
		string[] numbers = arg.Split(split_delimiters, split_options);
		result = new double[numbers.Length];
		for(int i = 0; i < numbers.Length; i++){
			result[i] = double.Parse(numbers[i]);
		}
	return result;
	}
}
