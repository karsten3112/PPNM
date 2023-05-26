using static System.Console;
using System.IO;
using System;

public static class IOhandle{
	static char[] split_delimiters = {char.Parse(" "), char.Parse("\t"), char.Parse(","), char.Parse("\n")};

	public static genlist<double[]> readFile(string filename){
 		genlist<double[]> result = new genlist<double[]>();
		var instream = new StreamReader(filename);
		for(string line = instream.ReadLine(); line != null; line = instream.ReadLine()){
			string[] input = line.Split(split_delimiters, StringSplitOptions.RemoveEmptyEntries);
			double[] vec = new double[input.Length];
			for(int i = 0; i < input.Length; i++){
				double num = double.Parse(input[i]);
				vec[i] = num;
			}
			result.add(vec);
		}
		instream.Close();
		return result;
	}

	public static void write(string filename, string[] data){
		var outstream = new StreamWriter(filename);
		foreach(string line in data){
			outstream.WriteLine(line);
		}
		outstream.Close();
	}

}
