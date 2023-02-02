using static System.Math;
using static System.Console;

class epsilon{
	static int i = 0;
	static int max = int.MaxValue;
	static int min = int.MinValue;
	static double x = 1.0;
	static float y =1.0f; //We typecast 1.0 into float by the suffix "f, F"
		
	static void Main(){
		while(i + 1 > i) {
			i++;
		}
		WriteLine($"my max int from loop = {i}	|and|		max int from int.MaxValue = {max}");
		compareint(i, max);
		
		i = 0;
		
		while(i - 1 < i){
			i--;
		}
		WriteLine($"my min int from loop = {i}		|and|		min int from int.Minvalue = {min}");
		compareint(i, min);
		
		machine_eps(); //We run function for determining epsilon- "Machine precicision" and compare the precision of float and double types.
	}

	public static void compareint(int a, int b){
		if(i == a){
			WriteLine("SUCCES... THEY ARE THE SAME VALUE");
		} else {
			WriteLine("FALSE... THEY ARE NOT THE SAME VALUE");
		}
	}

	public static void machine_eps(){
		int p = 0;
		while(1 + x != 1){
			p++;				 //We keep dividing x by 2 until 1 + x = 1, since x will be approximated to zero by the machine
			x/=2;
		}
		WriteLine(x); 
		WriteLine(p);
		p = 0;
		while(1 + y != 1){
			p++;
			y/=2;
		}
		WriteLine(y);
		WriteLine(p);
	}
}
