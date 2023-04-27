DIR = ../../matlib/matrix

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs matrix.dll
	mcs main.cs -reference:matrix.dll -target:exe -out:main.exe

matrix.dll: $(DIR)/matrix.cs $(DIR)/matrix.cs
	mcs -target:library -out:./matrix.dll $^

