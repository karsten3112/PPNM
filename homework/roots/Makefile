DIR1 = ../../matlib/matrix
DIR2 = ../Linear-Equations
DIR3 = ../ode
DIR4 = ../../exercises/generic-list

all:Out.txt En.txt

En.txt: main.exe
	mono main.exe -Shooting: > En.txt

Out.txt: main.exe
	mono main.exe -Test: > Out.txt

main.exe: main.cs rootfinder.dll lib.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:exe -out:main.exe

rootfinder.dll: rootfinder.cs lib.dll
	mcs rootfinder.cs $(addprefix -reference:,$(filter %.dll,$^)) -out:rootfinder.dll -target:library

lib.dll: $(DIR1)/matrix.cs $(DIR1)/vector.cs $(DIR2)/QRGS.cs $(DIR3)/odeint.cs $(DIR4)/genlist.cs
	mcs -target:library -out:lib.dll $^

