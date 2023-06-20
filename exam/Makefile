DIR = ../matlib/matrix

Test.svg: test.data test.gpi
	gnuplot test.gpi

test.data: main.exe
	mono main.exe -Test > test.data

main.exe: main.cs matrix.dll bilin.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:exe -out:main.exe

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

bilin.dll: bilin.cs matrix.dll
	mcs bilin.cs -target:library -out:bilin.dll -reference:matrix.dll

