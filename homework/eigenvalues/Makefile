DIR = ../../homework/eigenvalues/matrix-classes

all:Test.txt plots.txt

Test.txt: main.exe
	mono main.exe -test:1 > Test.txt

plots.txt: main.exe
	mono main.exe -rmax:4 -dr:0.5 > plots.txt

main.exe: main.cs matrix.dll jacobi.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

matrix.dll: $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:matrix.dll $^

jacobi.dll: jacobi.cs matrix.dll
	mcs jacobi.cs -reference:matrix.dll -target:library -out:jacobi.dll
