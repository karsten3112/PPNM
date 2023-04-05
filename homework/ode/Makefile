DIR1 = ../../matlib/matrix
DIR2 = ../../exercises/generic-list

all: Osc.svg Lotka.svg

Osc.svg: osc.gpi osc.data
	gnuplot osc.gpi

osc.data: main.exe
	mono main.exe -harmosc > osc.data

Lotka.svg: lotka.gpi lotka.data
	gnuplot lotka.gpi

lotka.data: main.exe
	mono main.exe -lotka > lotka.data

main.exe: main.cs genlist.dll matrix.dll odeint.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

odeint.dll: odeint.cs genlist.dll matrix.dll
	mcs odeint.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:library

matrix.dll: $(DIR1)/matrix.cs $(DIR1)/vector.cs
	mcs -target:library -out:matrix.dll $^

genlist.dll: $(DIR2)/genlist.cs
	mcs -target:library -out:genlist.dll $^
