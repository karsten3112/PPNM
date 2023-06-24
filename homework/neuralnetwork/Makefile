DIR = ../../matlib/matrix
DIR1 = ../../classes
DIR2 = ../../exercises/generic-list


all: Test1.svg Test2.svg Test3.svg Diffeq.svg

Test1.svg: test1.gpi test1.data
	gnuplot test1.gpi

#Training network for tabulation using Qnewton...
test1.data: main.exe
	mono main.exe -test:qnewton > test1.data

Test2.svg: test2.gpi test2.data
	gnuplot test2.gpi

#Training network for tabulation using simplex...
test2.data: main.exe
	mono main.exe -test:simplex > test2.data

Test3.svg: test3.gpi test3.data
	gnuplot test3.gpi

#Training network for tabulation using PSO...
test3.data: main.exe
	mono main.exe -test:pso > test3.data

Diffeq.svg: diff.data diff.gpi
	gnuplot diff.gpi

#Training network for diffeq. solve...
#May take a little time since training is done with; PSO, Qnewton and Simplex...
#PSO takes a little time...

diff.data: main.exe
	mono main.exe -diffeq > diff.data


main.exe: main.cs lib.dll ann.dll integrator.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

lib.dll: $(DIR)/matrix.cs $(DIR)/vector.cs $(DIR2)/genlist.cs
	mcs -target:library -out:./lib.dll $^

ann.dll: ann.cs lib.dll min.dll integrator.dll pso.dll
	mcs ann.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:ann.dll

min.dll: $(DIR1)/qnewtonMin.cs $(DIR1)/simplex.cs lib.dll
	mcs $(filter %.cs,$^) -target:library -reference:lib.dll -out:min.dll

integrator.dll: $(DIR1)/integrator.cs lib.dll
	mcs $(filter %.cs,$^) -target:library -reference:lib.dll -out:integrator.dll

pso.dll: pso.cs lib.dll
	mcs pso.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:pso.dll
