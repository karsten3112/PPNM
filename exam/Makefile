DIR = ../matlib/matrix

all: Test.svg Sim.svg

Test.svg: test.data test.gpi
	gnuplot test.gpi

test.data: main.exe
	mono main.exe -Test > test.data

Sim.svg: sim.data sim.gpi
	gnuplot sim.gpi

sim.data: main.exe
	mono main.exe -Sim > sim.data

main.exe: main.cs matrix.dll bilin.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:exe -out:main.exe

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

bilin.dll: bilin.cs matrix.dll
	mcs bilin.cs -target:library -out:bilin.dll -reference:matrix.dll

clean: 
	rm -f *t.txt *.exe *.dll *.data *.svg


