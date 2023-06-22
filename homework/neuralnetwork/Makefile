DIR = ../../matlib/matrix
DIR1 = ../../classes
DIR2 = ../../exercises/generic-list


all: Tab.svg

Tab.svg: tab.gpi tab.data
	gnuplot tab.gpi

tab.data: main.exe
	mono main.exe -diffeq > tab.data

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs lib.dll ann.dll integrator.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

lib.dll: $(DIR)/matrix.cs $(DIR)/vector.cs $(DIR2)/genlist.cs
	mcs -target:library -out:./lib.dll $^

ann.dll: ann.cs lib.dll min.dll integrator.dll
	mcs ann.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:ann.dll

min.dll: $(DIR1)/qnewtonMin.cs $(DIR1)/simplex.cs lib.dll
	mcs $(filter %.cs,$^) -target:library -reference:lib.dll -out:min.dll

integrator.dll: $(DIR1)/integrator.cs lib.dll
	mcs $(filter %.cs,$^) -target:library -reference:lib.dll -out:integrator.dll
