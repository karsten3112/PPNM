using static System.Math;
using System;

public class cspline{

	public vector x; public vector y;

	public cspline(vector xs, vector ys){
		this.x = xs.copy(); this.y = ys.copy();
	}

	public matrix Gauss_elim(matrix A){
		return A;
	}

	public vector backsub(matrix A, vector b){ //Matrix A is already upper triangular.
		vector y = new vector(b.size);
		for(int i = b.size - 1; i >= 0; i--){
			double sum = 0;
			for(int k = i+1; k < b.size; k++){
				sum+= A[i,k]*y[k];
			}
			y[i] = (b[i] - sum)/A[i,i];
		}
    return y;
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
