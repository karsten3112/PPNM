DIR = ../../matlib/matrix

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs integrate.dll matrix.dll
	

integrate.dll: integrate.cs matrix.dll
	mcs integrate.cs -reference:matrix.dll -out:integrate.dll -target:library


matrix.dll: $(DIR)/matrix.cs $(DIR)/matrix.cs
	mcs -target:library -out:./matrix.dll $^

