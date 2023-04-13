DIR = $(HOME)/repos/PPNM/matlib/complex


all: Erf.svg Gamma.svg Cgamma.svg


Gamma.svg: gamma.data gamma.gpi
	gnuplot gamma.gpi

gamma.data: main.exe
	mono main.exe -rgamma:gamma.data

Cgamma.svg: cgamma.data cgamma.gpi
	gnuplot cgamma.gpi

cgamma.data: main.exe
	mono main.exe -cgamma:cgamma.data

Erf.svg: erf.data erf.gpi
	gnuplot erf.gpi

erf.data: main.exe
	mono main.exe -erf:erf.data


main.exe: main.cs lib.dll
	mcs main.cs -target:exe -reference:$(filter %.dll, $^) -out:main.exe

lib.dll: gamma.cs erf.cs IOhandle.cs $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs -target:library -out:$@ $^
