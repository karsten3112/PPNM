DIR = ../../homework/eigenvalues/matrix-classes

all:Test.txt Rmax.svg

Test.txt: main.exe
	mono main.exe -test:1 > Test.txt

Rmax.svg: rmax.gpi rmax.data
	gnuplot rmax.gpi

rmax.data: main.exe
	for N in $$(seq 2 1 7); do \
		mono main.exe -rmax:"$$N" -dr:0.3 >> rmax.data;\
	done

main.exe: main.cs matrix.dll jacobi.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

matrix.dll: $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:matrix.dll $^

jacobi.dll: jacobi.cs matrix.dll
	mcs jacobi.cs -reference:matrix.dll -target:library -out:jacobi.dll
