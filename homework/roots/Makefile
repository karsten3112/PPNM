DIR1 = ../../matlib/matrix
DIR2 = ../Linear-Equations
DIR3 = ../ode


Out.txt: main.exe
	mono main.exe -Test: > Out.txt

main.exe: main.cs matrix.dll rootfinder.dll $(DIR3)/odeint.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:exe -out:main.exe

matrix.dll: $(DIR1)/matrix.cs $(DIR1)/vector.cs $(DIR2)/QRGS.cs
	mcs -target:library -out:./matrix.dll $^

rootfinder.dll: rootfinder.cs matrix.dll
	mcs rootfinder.cs -reference:matrix.dll -out:rootfinder.dll -target:library
