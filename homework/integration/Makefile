DIR = ../../matlib/matrix

all:Test.txt Erf.svg

Test.txt: main.exe
	mono main.exe -Test > Test.txt

Erf.svg: erf.gpi erf.data
	gnuplot erf.gpi

erf.data: main.exe
	mono main.exe -erf > erf.data

main.exe: main.cs integrator.dll matrix.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))	

integrator.dll: integrator.cs matrix.dll
	mcs integrator.cs -reference:matrix.dll -out:integrator.dll -target:library


matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

clean: 
	rm -f *t.txt *.exe *.dll *.data *.svg

