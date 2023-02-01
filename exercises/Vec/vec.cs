using static System.Math;
using static System.Console;

public class Vec{
	public double x,y,z;
	public Vec(){
		x = 0;
		y = 0;
		z = 0;
	}
		
	public Vec(double a, double b, double c){
		x = a;
		y = b;
		z = c;
	}

	static void Main(double a, double b, double c){
		Vec v = new Vec(a, b, c);
	}
	
	public static Vec operator*(Vec v, double c){
		return new Vec(c*v.x, c*v.y, c*v.z);
	}
	
	public static Vec operator+(Vec v, Vec u){
		return new Vec(v.x + u.x, v.y + u.y, v.z + u.z);
	}

	public static Vec operator-(Vec v, Vec u){
		return new Vec(v.x - u.x, v.y - u.y, v.z - u.z);
	}
	
	public void print(string s){
		Write(s);
		WriteLine($"{x} {y} {z}");
	}
	
	public void print(){
		this.print("");
	}
}
