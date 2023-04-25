DIR = ../../homework/eigenvalues/matrix-classes

all:Test.txt Rmax.svg DelR.svg Psi.svg

Test.txt: main.exe
	mono main.exe -test:1

Rmax.svg: rmax.gpi rmax.data
	gnuplot rmax.gpi

rmax.data: main.exe
	>$@
	for N in $$(seq 2 1 6); do \
		mono main.exe -rmax:"$$N" -dr:3 >> rmax.data;\
	done

DelR.svg: delr.gpi delr.data
	gnuplot delr.gpi

delr.data: main.exe
	>$@
	for N in $$(seq 2 1 10); do \
		mono main.exe -rmax:5 -dr:"$$N" >> delr.data;\
	done

Psi.svg: psi.gpi psi.data
	gnuplot psi.gpi

psi.data: main.exe
	mono main.exe -Psi > psi.data

main.exe: main.cs matrix.dll EVD.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

matrix.dll: $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:matrix.dll $^

EVD.dll: EVD.cs matrix.dll
	mcs EVD.cs -reference:matrix.dll -target:library -out:EVD.dll
