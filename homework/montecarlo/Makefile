DIR = ../../matlib/matrix

all:QuasiMC.svg PlainMC.svg Compare.svg Test.txt

Compare.svg: compare.gpi quasi.data plain.data
	gnuplot compare.gpi

Test.txt: main.exe
	mono main.exe -test > Test.txt

QuasiMC.svg: quasi.gpi quasi.data
	gnuplot quasi.gpi
	
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

clean: 
	rm -f *t.txt *.exe *.dll *.data *.svg

