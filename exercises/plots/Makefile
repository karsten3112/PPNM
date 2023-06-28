DIR = ../../matlib/complex


all: Erf.svg Gamma.svg Cgamma.svg Lngamma.svg

Lngamma.svg: ln.data lgamma.gpi
	gnuplot lgamma.gpi

ln.data: main.exe
	mono main.exe -lngamma > ln.data

Erf.svg: erf.data erf.gpi
	gnuplot erf.gpi

erf.data: main.exe
	mono main.exe -erf > erf.data

Gamma.svg: rg.data gamma.gpi
	gnuplot gamma.gpi

rg.data: main.exe
	mono main.exe -rgamma > rg.data

Cgamma.svg: cg.data cgamma.gpi
	gnuplot cgamma.gpi

cg.data: main.exe
	mono main.exe -cgamma > cg.data


main.exe: main.cs lib.dll
	mcs main.cs -target:exe -reference:$(filter %.dll, $^) -out:main.exe

lib.dll: gamma.cs erf.cs $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs -target:library -out:$@ $^

clean:
	rm -f *.svg *.data *.dll *.png *.exe
