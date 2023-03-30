using static System.Console;
using static System.Math;

public class vec{
	public double x;
	public double y;
	public double z;

	public vec(){
		x = 0.0;
		y = 0.0;
		z = 0.0;
	}

	public vec(double a, double b, double c){
		x = a;
		y = b;
		z = c;
	}

	public void print(string s=""){
		Write(s);WriteLine($"{this.x} {this.y} {this.z}");
	}

	public override string ToString(){
		return $"x = {this.x}, y = {this.y}, z = {this.z}";
	}


	public static vec operator*(vec v, double c){
		return new vec(c*v.x, c*v.y, c*v.z);
	}

	public static vec operator*(double c, vec v){
		return new vec(c*v.x, c*v.y, c*v.z);;
	}

	public static vec operator+(vec u, vec v){
		return new vec(v.x + u.x, v.y + u.y, v.z + u.z);
	}

	public static vec operator-(vec u, vec v){
		return new vec(u.x - v.x, u.y - v.y, u.z - v.z);
	}

	public static vec operator-(vec v){
		return new vec(-v.x, -v.y, -v.z);
	}

	public double dot(vec a){
		return this.x*a.x + this.y*a.y + this.z*a.z;
	}

	public static double dot(vec a, vec b){
		return a.dot(b);
	}
	public static vec vec_prod(vec c, vec u){
		double x_d = c.y*u.z - c.z*u.y;
		double y_d = c.z*u.x - c.x*u.z;
		double z_d = c.x*u.y - c.y*u.x;
		return new vec(x_d, y_d, z_d);
	}

	public vec vec_prod(vec a){
		return vec_prod(this, a);
	}

	public static double norm(vec c){
		return Sqrt(c.x*c.x + c.y*c.y + c.z*c.z);
	}

	public double norm(){
		return norm(this);
	}

	public static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9){
		if(Abs(a-b) < tau || Abs(a-b)/(Abs(a) + Abs(b)) < epsilon){
			return true;
 		 } else {
 			 return false;
 			 }
	}

	public bool approx(vec a){
		if(approx(this.x, a.x) && approx(this.y, a.y) && approx(this.z, a.z)){
			return true;
		} else {
			return false;
		}
	}

	public static bool approx(vec a, vec b) {
		if(a.approx(b)){
			return true;
		} else {
			return false;
		}
	}
}
