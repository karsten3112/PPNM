DIR1 = $(HOME)/repos/PPNM/matlib/complex
DIR2 = $(HOME)/repos/PPNM/homework/Linear-Equations/matrix-classes

Out.txt: main.exe
	mono main.exe -out:Out.txt

main.exe: main.cs cmath.dll QRGS.dll matrix.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

cmath.dll: $(DIR1)/cmath.cs $(DIR1)/complex.cs
	mcs -target:library -out:./cmath.dll $^

QRGS.dll: QRGS.cs matrix.dll
	mcs QRGS.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:QRGS.dll

matrix.dll: $(DIR2)/matrix.cs $(DIR2)/vector.cs
	mcs -target:library -out:matrix.dll $^
