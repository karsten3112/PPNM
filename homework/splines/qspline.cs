using static System.Math;
using System;


public class qspline{
	public vector x; public vector y;
	public vector b; public vector c;
	public qspline(vector xs, vector ys){
		x = xs.copy(); y = ys.copy();
		for(int i = 0; i < x.size; i++){
			
		}
	}

	public double evaluate(double z){
		return 2.0;
	}

	public double derivative(double z){
		return 2.0;
	}

	public double integral(double z){
		return 2.0;
	}

	public static int binsearch(vector x, double z){
		if(!(x[0] <=z && z <=x[x.size -1])){
             throw new Exception("Binsearch; bad z");
        }
        int i=0; int j = x.size-1;
        while(j-i>1){
        	int mid = (i+j)/2;
            if(z > x[mid]){
            	i = mid;
            } else {
            	j = mid;
           	}
       	}
    return i;
	}

}
