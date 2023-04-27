DIR = ../../matlib/matrix

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs matrix.dll mc.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:exe -out:main.exe

matrix.dll: $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs $^ -target:library -out:matrix.dll

mc.dll: mc.cs matrix.dll
	mcs mc.cs -reference:matrix.dll -target:library -out:mc.dll

