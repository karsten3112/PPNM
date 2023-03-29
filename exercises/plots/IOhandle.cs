using static System.Console;
using System;
using System.IO;
using System.Collections.Generic;

public static class IOhandle{
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
}
