DIR = ../../matlib/matrix

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs matrix.dll rootfinder.dll
	mcs main.cs -reference:matrix.dll -target:exe -out:main.exe

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs QRGS.cs
	mcs -target:library -out:./matrix.dll $^

rootfinder.dll: rootfinder.cs matrix.dll
	mcs rootfinder.cs -reference:matrix.dll -out:rootfinder.dll -target:library
