using System;
using static System.Console;
using static System.Math;

class main{
	public static int sample = 1000;
	public static int csample = 22500;
	static void Main(string[] args){
		foreach(string arg in args){
			string[] inp = arg.Split(":");
			if(inp[0] == "-erf"){
				string[] data = new string[sample];
				string erf_filename = inp[1];
				double xbound = 3.0;
				double dx = 2*xbound/sample;
				for(int i = 0; i < sample; i++){
					data[i] = $"{-xbound + i*dx} {sfuns.erf(-xbound + i*dx)}";
				}
				IOhandle.Write(erf_filename, data);
			}
			if(inp[0] == "-rgamma"){
				string[] data = new string[sample]; 
				string gam_filename = inp[1];
				double xbound = 5.0;
				double dx = 2*xbound/sample;
                for(int i = 0; i < sample; i++){
                	data[i] = $"{-xbound + i*dx} {sfuns.rgamma(-xbound + i*dx)}";
               	}
                IOhandle.Write(gam_filename, data);
			}
			if(inp[0] == "-cgamma"){
				string[] data = new string[csample +(int)Sqrt(csample)];
				string gam_filename = inp[1];
				double bounds = 5.0;
				double dx = 2*bounds/Sqrt(csample);
				for(int i = 0; i < (int)Sqrt(csample); i++){
					for(int j = 1; j < (int)Sqrt(csample); j++){
						if(j == (int)Sqrt(csample)){
							data[i*(int)Sqrt(csample) + j + 1] = "";
						} else {
							complex num = new complex(-bounds + i*dx, -bounds + j*dx);
							data[i*(int)Sqrt(csample) + j] = $"{num.Re}	{num.Im}	{cmath.abs(sfuns.cgamma(num))}";
						}
					}
				}
				IOhandle.Write(gam_filename, data);
			}
		}
	}
}
