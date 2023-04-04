using static System.Console;
using System;
using System.IO;

public static class IOhandle{
	static char[] split_delimiters = {char.Parse(" "), char.Parse("\t"), char.Parse(","), char.Parse("\n")};

	public static genlist<string> Read(string filename){
		genlist<string> result = new genlist<string>();
		var instream = new StreamReader(filename);
		for(string line = instream.ReadLine(); line != null; line = instream.ReadLine()){
			result.add(line);
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

	public static genlist<double> getnums(genlist<string> data){
       	genlist<double> result = new genlist<double>();
        var split_options = StringSplitOptions.RemoveEmptyEntries;
        for(int i = 0; i < data.size; i++){
            string[] lines = data[i].Split(split_delimiters, split_options);
            foreach(string l in lines){
                double num = double.Parse(l);
                result.add(num);
            }
        }
        return result;
     }


}
