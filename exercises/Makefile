DIR = ../../matlib/complex

cmath.dll: $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs -target:library -out:./cmath.dll $^

Out.txt: main.exe
	mono main.exe

