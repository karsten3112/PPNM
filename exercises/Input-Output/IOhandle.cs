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

}
