DIR = ../../matlib/matrix

all:QuasiMC.svg Out.txt PlainMC.svg

QuasiMC.svg: quasi.gpi quasi.data
	gnuplot quasi.gpi

Out.txt: main.exe
	mono main.exe -debug > Out.txt

quasi.data: main.exe
	mono main.exe -tQuasiMC > quasi.data

PlainMC.svg: plain.gpi plain.data
	gnuplot plain.gpi

plain.data: main.exe
	mono main.exe -tPlainMC > plain.data


main.exe: main.cs matrix.dll mc.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:exe -out:main.exe

matrix.dll: $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs $^ -target:library -out:matrix.dll

mc.dll: mc.cs matrix.dll
	mcs mc.cs -reference:matrix.dll -target:library -out:mc.dll

