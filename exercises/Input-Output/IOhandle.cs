using static System.Console;
using System;
using System.IO;
using System.Collections.Generic;

public static class IOhandle{
	static char[] split_delimiters = {char.Parse(" "), char.Parse("\t"), char.Parse(","), char.Parse("\n")};

	public static List<string> Read(string filename){
		List<string> result = new List<string>();
		var instream = new StreamReader(filename);
		for(string line = instream.ReadLine(); line != null; line = instream.ReadLine()){
			result.Add(line);
		}
		instream.Close();
		return result;
	}

	public static void Write(string filename, string[] data){
		var outstream = new StreamWriter(filename);
		foreach(string line in data){
			outstream.WriteLine(line);
		}
		outstream.Close();
	}

	public static List<double> getnums(List<string> data){
		List<double> result = new List<double>();
		var split_options = StringSplitOptions.RemoveEmptyEntries;
		foreach(string line in data){
			string[] lines = line.Split(split_delimiters, split_options);
			foreach(string l in lines){
				double num = double.Parse(l);
				result.Add(num);
			}
		}
		return result;
	}

	public static List<double> getnums(string data){
		List<string> dataL = new List<string>();
		dataL.Add(data);
		return getnums(dataL);
	}

}
