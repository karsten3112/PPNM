DIR1 = ../../matlib/matrix
DIR2 = ../Linear-Equations
DIR3 = ../ode
DIR4 = ../../exercises/generic-list

all:Test.txt Rmax.svg Rmin.svg Psi.svg Eps.svg Acc.svg Conv2.svg

Conv2.svg: conv2.gpi conv2.data
	gnuplot conv2.gpi

conv2.data: main.exe
	mono main.exe -conv2 > conv2.data

Eps.svg: eps.gpi epsacc.data
	gnuplot eps.gpi

Acc.svg: acc.gpi epsacc.data
	gnuplot acc.gpi

epsacc.data: main.exe
	mono main.exe -epsacc > epsacc.data

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


main.exe: main.cs rootfinder.dll lib.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:exe -out:main.exe

rootfinder.dll: rootfinder.cs lib.dll
	mcs rootfinder.cs $(addprefix -reference:,$(filter %.dll,$^)) -out:rootfinder.dll -target:library

lib.dll: $(DIR1)/matrix.cs $(DIR1)/vector.cs $(DIR2)/QRGS.cs $(DIR3)/odeint.cs $(DIR4)/genlist.cs
	mcs -target:library -out:lib.dll $^

