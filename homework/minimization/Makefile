DIR = ../../matlib/matrix
DIR1 = ../../exercises/generic-list

all: Test.txt Higgs.svg

Test.txt: main.exe
	mono main.exe -test: > Test.txt

Higgs.svg: higgs.gpi fit.data
	gnuplot higgs.gpi

fit.data: main.exe
	mono main.exe -higgs:higgs.data.txt > fit.data

main.exe: main.cs matrix.dll min.dll IOhandle.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

min.dll: qnewtonMin.cs simplex.cs matrix.dll
	mcs $(filter %.cs,$^) -target:library -reference:matrix.dll -out:min.dll

IOhandle.dll:IOhandle.cs $(DIR1)/genlist.cs
	mcs $(filter %.cs,$^) -target:library -out:IOhandle.dll
