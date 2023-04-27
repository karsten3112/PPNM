using static System.Console;
using static System.Math;
using System;


public static class montecarlo{

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

	public static void qausiMC(){

	}

}
