using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){
		for(double x = 1.0/64; x <= 10; x+=1.0/32){
			WriteLine($"{x} {sfuns.gamma(x)}");
		}
	}
}
