Out.txt: main.exe
	mono main.exe

IOhandle.dll: IOhandle.cs
	mcs IOhandle.cs -out:IOhandle.dll -target:library

main.exe: main.cs IOhandle.dll
	mcs main.cs -out:main.exe -target:exe -reference:IOhandle.dll



