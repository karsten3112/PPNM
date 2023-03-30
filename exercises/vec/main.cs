using static System.Console;
using static System.Math;
using System;

class main{
	static Random rnd = new System.Random(1);

	static void Main(){
		WriteLine("TESTING ALL METHODS AND OPERATORS FOR 18 PSEUDO-RANDOM VECTORS");
		WriteLine("------------------------------------------------------------------------");
		WriteLine("");
		for(int i = 0; i < 9; i++){
			vec a = new vec(rnd.NextDouble()*5, rnd.NextDouble()*5, rnd.NextDouble()*5);
			vec b = new vec(rnd.NextDouble()*5, rnd.NextDouble()*5, rnd.NextDouble()*5);
			WriteLine("The pseudo random vector a");
			WriteLine($"a with entries; {a}");
			WriteLine("The pseudo random vector b");
			WriteLine($"b with entries; {b}");

			WriteLine("Testing vector scalar multiplication operator");
			if(test_scalar(a, rnd.NextDouble()*3)){
				WriteLine("Passed...");
			} else {
				WriteLine("Failed...");
			}

			WriteLine("Testing vector addition and subtraction operators");
			if(test_operators(a, b)){
				WriteLine("Passed...");
			} else {
				WriteLine("Failed...");
			}

			WriteLine("Testing dot product");
			if(test_dot(a, b)){
				WriteLine("Passed...");
			} else {
				WriteLine("Failed...");
			}

			WriteLine("Testing cross-product");
			if(test_cross(a, b)){
				WriteLine("Passed...");
			} else {
				WriteLine("Failed...");
			}

			WriteLine("Testing norm of vectors");
			if(test_norm(a)){
				WriteLine("Passed...");
			} else {
				WriteLine("Failed...");
			}

			WriteLine($"Test nr. {i + 1} Done");
			WriteLine("########################################################");
		}
	}

	public static bool test_cross(vec a, vec b){
		vec c = a.vec_prod(b);
		double d = c.dot(a);
		double e = c.dot(b);
		if(approxn(d, 0) && approxn(e, 0)){
			return true;
		} else {
			return false;
		}
	}

	public static bool test_scalar(vec a, double c){
		vec d = c*a;
		vec f = a*c;
		vec n = new vec(c*a.x,c*a.y,c*a.z);
		if(d.approx(n) && f.approx(n)){
			return true;
		} else {
			return false;
		}
	}

	public static bool test_operators(vec a, vec b){
		vec c = a - b;
		vec d = a + b;
		vec g = -a;
		vec mop = new vec(-a.x,-a.y,-a.z);
		vec resm = new vec(a.x - b.x, a.y - b.y, a.z - b.z);
		vec resp = new vec(a.x + b.x, a.y + b.y, a.z + b.z);
		if(c.approx(resm) && d.approx(resp) && g.approx(mop)){
			return true;
		} else {
			return true;
		}
	}

	public static bool test_dot(vec a, vec b){
		double num = a.dot(b);
		double result = a.x*b.x + a.y*b.y + a.z*b.z;
		if(approxn(num, result)){
			return true;
		} else {
			return false;
		}
	}

	public static bool test_norm(vec a){
		double test = a.norm();
		double res = Sqrt(Pow(a.x,2) + Pow(a.y,2) + Pow(a.z, 2));
		if(approxn(test, res)){
			return true;
		} else {
			return false;
		}
	}
	
	public static bool approxn(double a, double b, double epsilon=1e-9, double tau=1e-9){
		if(Abs(a-b) < tau || Abs(a-b)/(Abs(a) + Abs(b)) < epsilon){
			return true;
		} else {
			return false;
		}
	}
}
