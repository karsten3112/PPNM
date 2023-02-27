using System;
using static System.Console;
using static System.Threading.Thread;
using static System.Math;

public class data{
	public int a,b;
	public double sum;
	public void harmonic(object obj){
		var local = (data)obj;
		local.sum = 0;
		for(int i=local.a; i < local.b;i++){
			local.sum +=1.0/i; 

		}
	}
}
