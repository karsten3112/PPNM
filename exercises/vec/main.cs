using static System.Console;
using static System.Math;

class main{

	static void Main(){
	vec p = new vec();
	vec v = new vec(1.0, 2.0, 3.0);
	vec u = new vec(3.0, 0, 5.0);
	WriteLine(p);
	WriteLine(v);
	WriteLine(u);
	vec s = (double)(2)*v;
	double d = v.dot(u);
	WriteLine(d);
	WriteLine(s);
	}
}
