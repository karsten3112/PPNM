DIR = ../../matlib/matrix
DIR1 = ../../exercises/generic-list

Out.txt: main.exe
	mono main.exe -test: > Out.txt

main.exe: main.cs matrix.dll $(DIR1)/genlist.dll min.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

min.dll: qnewton.cs simplex.cs matrix.dll
	mcs $(filter %.cs,$^) -target:library -reference:matrix.dll -out:min.dll

