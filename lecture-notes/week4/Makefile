DIR = $(HOME)/repos/PPNM/matlib/complex


gamma.svg: cgamma.data rgamma.data erf.data
	gnuplot cgamma.gpi gamma.gpi erf.gpi

rgamma.data: main.exe
	mono main.exe -rgamma:rgamma.data

erf.data: main.exe
	mono main.exe -erf:erf.data


cgamma.data: main.exe
	mono main.exe -cgamma:cgamma.data


main.exe: main.cs lib.dll
	mcs main.cs -target:exe -reference:$(filter %.dll, $^) -out:main.exe

lib.dll: gamma.cs erf.cs IOhandle.cs $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs -target:library -out:$@ $^
