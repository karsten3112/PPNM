using static System.Console;
using static System.Math;
using System;

public class genlist<T>{
	public T[] data;
	public int size => data.Length;
	public T this[int i] => data[i];

	public genlist(){
		data = new T[0];
	}

	public void add(T elem){
		T[] newdata = new T[size +1];
		Array.Copy(data, newdata, size);
		newdata[size] = elem;
		data = newdata;
	}

	public void remove(int i){
		T[] newdata = new T[size - 1];
		for(int j = 0; i < size - 1; j++){
			if(j != i){
				newdata[j] = data[j];
			}
		data = newdata;
		}
	}
}
