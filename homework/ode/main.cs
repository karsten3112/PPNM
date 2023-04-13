using System;
using static System.Console;
using static System.Math;

class main{
	static double b=0.25, c=5.0;
	static double m1=1.0, m2=1.0, m3=1.0;
	static double G = 1.0;
	static void Main(string[] args){
		foreach(string arg in args){
			if(arg == "-Tbody"){
				vector yinit = new vector(12);
				double tinit = 0.0; double tend = 6.32591398; //initial-conditions given from the paper in the assignment.
				yinit[0] = 0.97000436; yinit[1] = -0.24308753;
				yinit[2] = -yinit[0]; yinit[3] = -yinit[1];
				yinit[4] = 0.0; yinit[5] = 0.0;

				yinit[10] = -0.93240737; yinit[11] = -0.86473146;
				yinit[6] = -yinit[10]/2.0; yinit[7] = -yinit[11]/2.0;
				yinit[8] = -yinit[10]/2.0; yinit[9] = -yinit[11]/2.0;

				Func<double, vector, vector> F = Tbody;
				odeint solve = new odeint(F,tinit,yinit,tend, true);
				genlist<double> ts = solve.xs;
				genlist<vector> ys = solve.ys;
				for(int i = 0; i < ts.size; i++){
					WriteLine($"{ys[i][0]}	{ys[i][1]}	{ys[i][2]}	{ys[i][3]}	{ys[i][4]}	{ys[i][5]}");
				}
			}
			if(arg == "-harmosc"){
				Func<double,vector,vector> F = delegate(double t,vector y){
					double theta=y[0]; double omega=y[1];
			    	return new vector(omega,-theta);
				};
				vector yinit = new vector(1,0);
				double xinit = 0.0;
				double xend = 10.0;
				odeint solve = new odeint(F,xinit,yinit,xend, true);
				genlist<double> ts = solve.xs;
				genlist<vector> ys = solve.ys;
				for(int i = 0; i < ts.size; i++){
					WriteLine($"{ts[i]} {ys[i][0]}");
				}

			}
			if(arg == "-damposc"){

			}
			if(arg == "-lotka"){
				double a = 1.5; double b = 1.0; double c = 3.0; double d = 1.0;
				Func<double,vector,vector> F = delegate(double t, vector y){
					double y0 = y[0];
					double y1 = y[1];
					double y0d=a*y0-b*y1*y0;
					double y1d=-c*y1+d*y1*y0;
					return new vector(y0d,y1d);
				};
				vector yinit = new vector(10, 5);
				double xinit = 0.0;
				double xend = 15.0;
				odeint solve = new odeint(F,xinit,yinit,xend, true, 0.005);
				genlist<double> ts = solve.xs;
				genlist<vector> ys = solve.ys;
				for(int i = 0; i < ts.size; i++){
					WriteLine($"{ts[i]} {ys[i][0]}");
				}
				WriteLine("");
				WriteLine("");
				for(int i = 0; i < ts.size; i++){
					WriteLine($"{ts[i]}	{ys[i][1]}");
				}
			}
		}
	}
	public static vector Tbody(double t, vector ys){
		vector result = new vector(ys.size);
		vector r1 = new vector(ys[0], ys[1]);
		vector r2 = new vector(ys[2], ys[3]);
		vector r3 = new vector(ys[4], ys[5]);
		vector v1 = new vector(ys[6], ys[7]);
		vector v2 = new vector(ys[8], ys[9]);
		vector v3 = new vector(ys[10], ys[11]);

		vector drdt1 = v1;
		vector drdt2 = v2;
		vector drdt3 = v3;

		vector rel1 = r2 - r1;
		vector rel2 = r2 - r3;
		vector rel3 = r3 - r1;

		double norm1 = rel1.norm();
		double norm2 = rel2.norm();
		double norm3 = rel3.norm();

		vector dvdt1 = G*(m2*(rel1)/(norm1*norm1*norm1) + m3*(rel3)/(norm3*norm3*norm3));
		vector dvdt2 = G*(-m1*(rel1)/(norm1*norm1*norm1) - m3*(rel2)/(norm2*norm2*norm2));
		vector dvdt3 = G*(m1*(rel2)/(norm2*norm2*norm2) - m1*(rel3)/(norm3*norm3*norm3));
		result[0] = drdt1[0]; result[1] = drdt1[1];
		result[2] = drdt2[0]; result[3] = drdt2[1];
		result[4] = drdt3[0]; result[5] = drdt3[1];

		result[6] = dvdt1[0]; result[7] = dvdt1[1];
		result[8] = dvdt2[0]; result[9] = dvdt2[1];
		result[10] = dvdt3[0]; result[11] = dvdt3[1];
		return result;
	}
}
