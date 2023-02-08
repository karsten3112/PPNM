using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){
		static_hello.print();
		static_world.print();
		static_hello.greeting = "new hello from main";
		static_world.greeting = "new world from main";
		static_hello.print();
		static_world.print();
	}
}
