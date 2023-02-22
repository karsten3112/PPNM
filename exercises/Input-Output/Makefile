out.txt: main.exe
	mono main.exe

input.dll: input.cs
	mcs input.cs -out:input.dll -target:library

main.exe: main.cs input.dll
	mcs main.cs -out:main.exe -target:exe -reference:input.dll



