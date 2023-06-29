DIR = ../matlib/matrix

all: Test1.gif Test2.png Test3.png Vol.txt

Vol.txt: main.exe
	mono main.exe -Vol > Vol.txt

Test3.png: test3.data test3.gpi
	gnuplot test3.gpi

test3.data: main.exe
	mono main.exe -Test3 > test3.data

Test2.png: test2.data test2.gpi
	gnuplot test2.gpi

test2.data: main.exe
	mono main.exe -Test2 > test2.data

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
	rm -f *t.txt *.exe *.dll *.data *.svg *.png *.gif


