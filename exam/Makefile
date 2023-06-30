DIR = ../matlib/matrix

all: Test1.gif Cylside.png Cyltop.png Irreg.png

Irreg.png: irr.data irreg.gpi
	gnuplot irreg.gpi

irr.data: main.exe
	mono main.exe -Irreg > irr.data

Cylside.png: cyl.data cylside.gpi
	gnuplot cylside.gpi

Cyltop.png: cyl.data cyltop.gpi
	gnuplot cyltop.gpi

cyl.data: main.exe
	mono main.exe -Cyl > cyl.data

Test1.gif: test1.data test1.gpi
	gnuplot test1.gpi

test1.data: main.exe
	mono main.exe -Test1 > test1.data

main.exe: main.cs matrix.dll bilin.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:exe -out:main.exe

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

bilin.dll: bilin.cs matrix.dll
	mcs bilin.cs -target:library -out:bilin.dll -reference:matrix.dll

clean: 
	rm -f *.exe *.dll *.data *.svg *.png *.gif


