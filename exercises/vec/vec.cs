using static System.Console;
using static System.Math;

public class Vec{
	double x;
	double y;
	double z;
	
	public Vec(){
		x = 0.0;
		y = 0.0;
		z = 0.0;
	}

	public Vec(double a, double b, double c){
		x = a;
		y = b;
		z = c;
	}

	public static Vec operator*(Vec v, double c){
		return new Vec(c*v.x, c*v.y, c*v.z);
	}

	public static Vec operator*(double c, Vec v){
		return c*v;
	}

	public static Vec operator+(Vec u, Vec v){
		return new Vec(v.x + u.x, v.y + u.y, v.z + u.z);
	}
	
	public static Vec operator-(Vec u, Vec v){
		return new Vec(u.x - v.x, u.y - v.y, u.z - v.z); 
	}
	
	public static Vec operator-(Vec v){
		return new Vec(-v.x, -v.y, -v.z);
	}
}
