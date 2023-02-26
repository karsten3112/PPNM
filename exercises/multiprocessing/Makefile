Out.txt: main.exe
	mono main.exe

main.exe: main.cs subroute.dll
	mcs main.cs -reference:subroute.dll -out:main.exe -target:exe

subroute.dll: subroute.cs
	mcs subroute.cs -target:library -out:subroute.dll
