Out.txt: main.exe
	mono main.exe -input:input.txt > Out.txt

main.exe: main.cs lib.dll
	mcs main.cs -reference:lib.dll -target:exe -out:main.exe

lib.dll: genlist.cs IOhandle.cs
	mcs genlist.cs IOhandle.cs -out:lib.dll -target:library

clean: 
	rm -f Out.txt *.exe *.dll *.data *.svg

