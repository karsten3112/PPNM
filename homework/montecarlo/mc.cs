using static System.Console;
using static System.Math;
using System;


public static class montecarlo{
	public static int[] bs1 = {2,3,5,7,11,13};
	public static int[] bs2 = {17,19,23,29,31};

	public static (double, double) plainMC(Func<vector, double> f, vector a, vector b, int N){
		double V = 1; double err = 0.0; Random rnd = new Random();
		double sum1 = 0.0; double sum2 = 0.0;
		double result = 0.0;
		vector num = new vector(a.size);
		for(int i = 0; i < a.size; i++){
			V*=(b[i]-a[i]);
		}
		for(int i = 0; i < N; i++){
			for(int j = 0; j < a.size; j++){
				num[j] = a[j] + rnd.NextDouble()*(b[j]-a[j]);
			}
			sum1+=f(num); sum2+=f(num)*f(num);
		}
		sum1 = sum1/N;
		sum2 = sum2/N;
		result = V*sum1;
		err = V*Sqrt((sum2 - sum1*sum1)/N);
		return (result,err);
	}

	public static (double, double) quasiMC(Func<vector, double> f, vector a, vector b, int N){
		vector vec1 = new vector(a.size); vector vec2 = new vector(a.size);
		double sum1 = 0.0; double sum2 = 0.0; double V = 1.0; double err;
		for(int i = 0; i < a.size; i++){
			V*=(b[i]-a[i]);
		}
		for(int i = 0; i < N; i++){
			vec1 = halton(i, bs1, a.size);
			vec2 = halton(i, bs2, a.size);
			for(int j = 0; j < a.size; j++){
				vec1[j] = vec1[j]*(b[j]-a[j]) + a[j];
				vec2[j] = vec2[j]*(b[j]-a[j]) + a[j];
			}
			sum1+=f(vec1); sum2+=f(vec2);
		}
		sum1/=N; sum2/=N;
		sum1*=V; sum2*=V;
		err = Abs(sum1 - sum2);
		return (sum1,err);
	}

	public static double corput(int n, int b){
		double q = 0.0; double bk = (double)1.0/b;
		while(n > 0){
			q+= (n % b)*bk;
			n/=b;
			bk/=b;
		}
		return q;
	}

	public static vector halton(int n, int[] bs, int dim){
		vector result = new vector(dim);
		for(int i = 0; i < dim; i++){
			result[i] = corput(n, bs[i]);
		}
		return result;
	}
}
