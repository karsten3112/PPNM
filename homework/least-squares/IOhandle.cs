using static System.Console;
using System.IO;
using System;


public static class IOhandle{
	static char[] split_delimiters = {char.Parse(" "), char.Parse("\t"), char.Parse(","), char.Parse("\n")};

	public static string readString(string filename){
		string result = null;
		var instream = new StreamReader(filename);
		for(string line = instream.ReadLine(); line != null; line = instream.ReadLine()){
			result+= " " + line;
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

	public static (double[], double[], double[]) readData(string filename, int datlength){
		double[] xs = new double[datlength];
		double[] ys = new double[datlength];
		double[] dys = new double[datlength];
		var split_options = StringSplitOptions.RemoveEmptyEntries;
		var instream = new StreamReader(filename);
		int i = 0;
		for(string data = instream.ReadLine(); data != null; data = instream.ReadLine()){
			string[] line = data.Split(split_delimiters, split_options);
			xs[i] = double.Parse(line[0]);
			ys[i] = double.Parse(line[1]);
			dys[i] = double.Parse(line[2]);
			i++;
		}
		instream.Close();
		return (xs, ys, dys);
	}

}
