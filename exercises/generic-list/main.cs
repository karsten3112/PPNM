using static System.Console;
using static System.Math;
using System;

class main{
	public static void Main(string[] args){
		if(args.Length != 0){
			genlist<double> dat = IOhandle.getnums(IOhandle.Read(args[0]));
			for(int i = 0; i < dat.size; i++){
				WriteLine(dat[i]);
			}
		}
	}
}
