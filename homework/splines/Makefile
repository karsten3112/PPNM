DIR = ../../matlib/matrix

all:Linplot.svg Qplot.svg Cplot.svg Compare.svg

Compare.svg: comp.gpi comp.data
	gnuplot comp.gpi

comp.data: main.exe
	mono main.exe -compare > comp.data

Linplot.svg: linplot.gpi linplot.data
	gnuplot linplot.gpi

linplot.data: main.exe
	mono main.exe -linspline > linplot.data

Cplot.svg: cplot.gpi cplot.data
	gnuplot cplot.gpi

cplot.data: main.exe
	mono main.exe -cspline > cplot.data


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

clean: 
	rm -f *t.txt *.exe *.dll *.data *.svg

