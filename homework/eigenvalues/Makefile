DIR1 = ../../matlib/complex
DIR2 = ../../homework/eigenvalues/matrix-classes

Out.txt: main.exe
	mono $^ > Out.txt

main.exe: cmath.dll matrix.dll EVD.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

matrix.dll: $(DIR2)/vector.cs $(DIR2)/matrix.cs
	mcs -target:library -out:matrix.dll $^

EVD.dll: EVD.cs matrix.dll
	mcs EVD.cs -reference:matrix.dll -target:library -out:EVD.dll
