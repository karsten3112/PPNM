using static System.Console;
using static System.Math;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;


class main{
	static string file_input;
	static string file_output;
	static List<double> extra_numinp;
	static List<double> numbers;

	static void Main(string[] args) {
		if(args.Length == 0){
			WriteLine("We did not parse any arguments");
		} else {
			foreach(string arg in args) {
				string[] opt = arg.Split(":");
				if(opt[0] == "-input"){
					file_input = opt[1];
        		}
         		if(opt[0] == "-output"){
             		file_output = opt[1];
        		}
         		if(opt[0] == "-numbers"){
             		extra_numinp = IOhandle.getnums(opt[1]);
				}
			}
			if(file_input != null && file_output != null){
				WriteLine($"VI SKRIVER FRA; {file_input} TIL {file_output}");
				numbers = IOhandle.getnums(IOhandle.Read(file_input));
				string[] numbs = action(numbers, extra_numinp);
				IOhandle.Write(file_output, numbs);
			}
	}

	}
	public static string[] action(List<double> numbs, List<double> extra_nums){
		List<double> nums = numbs.Concat(extra_nums).ToList();
		string[] result = new string[nums.Count];
		for(int i = 0; i < nums.Count; i++){
			double sin = Sin(nums[i]);
			double cos = Cos(nums[i]);
			string line = $"Inserted value;{nums[i]}; cos({nums[i]}) = {cos}; sin({nums[i]}) = {sin}";
			result[i] = line;
		}
		return result;
	}
}
