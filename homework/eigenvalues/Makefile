DIR1 = $(HOME)/repos/PPNM/matlib/complex
DIR2 = $(HOME)/repos/PPNM/homework/eigenvalues/matrix-classes

Out.txt: main.exe
	mono main.exe

main.exe: cmath.dll matrix.dll 
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

cmath.dll: $(DIR1)/cmath.cs $(DIR1)/complex.cs
	mcs -target:library -out:./cmath.dll $^


matrix.dll: $(DIR2)/vector.cs $(DIR2)/matrix.cs
	mcs -target:library -out:matrix.dll $^
