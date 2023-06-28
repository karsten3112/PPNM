all: Output_file.txt Out.txt 

Out.txt: main.exe
	mono main.exe -input:input.txt -numbers:1,2,3,4,5 > Out.txt #File made via redirection std. output to Out.txt

Output_file.txt: main.exe
	mono main.exe -input:input.txt -output:Output_file.txt -numbers:20,30,60,80 #File made via output stream to Output_file.txt

IOhandle.dll: IOhandle.cs
	mcs IOhandle.cs -out:IOhandle.dll -target:library

main.exe: main.cs IOhandle.dll
	mcs main.cs -out:main.exe -target:exe -reference:IOhandle.dll

clean: 
	rm -f Out.txt Output_file.txt *.exe *.dll *.data *.svg


