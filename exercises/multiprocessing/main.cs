using static System.Threading.Thread;
using static System.Console;
using static System.Math;
using System;
using System.Threading;
using System.Threading.Tasks;

class main{
	public static double suma = 0;
	public static double sumb = 0;
	public static int numterm = (int)1e8;
	public static int numthr = 1;

	public static void Main(string[] args){
		foreach(string arg in args){
			string[] inp = arg.Split(":");
			if(inp[0] == "-threads"){
				numthr = int.Parse(inp[1]);
			}
			if(inp[0] == "-terms"){
				numterm = (int)float.Parse(inp[1]);
			}
		}
		Parallel.For(1, numterm+1, delegate(int i){sumb+=1.0/i;});
		data[] dat = createdata(numthr, numterm);
		var threads = new Thread[numthr];
		for(int i = 0; i < numthr; i++){
			threads[i] = new Thread(dat[i].harmonic);
			threads[i].Start(dat[i]);
		}
		for(int i = 0; i < numthr; i++){
			threads[i].Join();
		}
		for(int i = 0; i < numthr; i++){
			suma+=dat[i].sum;
		}
		WriteLine(suma);
		WriteLine(sumb);
	}

	public static data[] createdata(int nthreads, int nterms){
		data[] result = new data[nthreads];
		for(int i = 0; i < nthreads; i++){
			result[i] = new data();
			result[i].a = 1 + nterms/nthreads*i;
			result[i].b = 1 + nterms/nthreads*(1+i);
		}
			result[result.Length - 1].b = nterms+1;
		return result;
	}
}
