DIR = ../../matlib/matrix

all:Linplot.svg Qplot.svg

Linplot.svg: linplot.gpi linplot.data
	gnuplot linplot.gpi

linplot.data: main.exe
	mono main.exe -linspline > linplot.data


Qplot.svg: qplot.gpi qplot.data
	gnuplot qplot.gpi

qplot.data: main.exe
	mono main.exe -qspline > qplot.data

main.exe: main.cs matrix.dll splines.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:matrix.dll $^

splines.dll: linspline.cs qspline.cs cspline.cs matrix.dll
	mcs $(filter %.cs,$^) -target:library -out:splines.dll $(addprefix -reference:,$(filter %.dll,$^))
