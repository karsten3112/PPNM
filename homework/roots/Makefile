DIR1 = ../../matlib/matrix
DIR2 = ../Linear-Equations
DIR3 = ../ode
DIR4 = ../../exercises/generic-list

all:Test.txt Rmax.svg Rmin.svg Psi.svg Conv2.svg

Conv2.svg: conv2.gpi es2.data es1.data
	gnuplot conv2.gpi

es2.data: main.exe
	mono main.exe -Shooting2 > es2.data

Psi.svg: psi.gpi es1.data
	gnuplot psi.gpi

Rmin.svg: rmin.gpi es1.data
	gnuplot rmin.gpi

Rmax.svg: rmax.gpi es1.data
	gnuplot rmax.gpi

es1.data: main.exe
	mono main.exe -Shooting1 > es1.data

Test.txt: main.exe
	mono main.exe -Test > Test.txt

En.txt: main.exe
	mono main.exe -Shooting1 > En.txt

Out.txt: main.exe
	mono main.exe -Test: > Out.txt


main.exe: main.cs rootfinder.dll lib.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:exe -out:main.exe

rootfinder.dll: rootfinder.cs lib.dll
	mcs rootfinder.cs $(addprefix -reference:,$(filter %.dll,$^)) -out:rootfinder.dll -target:library

lib.dll: $(DIR1)/matrix.cs $(DIR1)/vector.cs $(DIR2)/QRGS.cs $(DIR3)/odeint.cs $(DIR4)/genlist.cs
	mcs -target:library -out:lib.dll $^

