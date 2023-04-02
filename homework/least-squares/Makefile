DIR = ../../matlib/matrix


Out.txt: main.exe
	mono main.exe -input:data.txt > Out.txt

main.exe: main.cs LTSQ.dll matrix.dll QRGS.dll IOhandle.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^)) 

LTSQ.dll: LTSQ.cs matrix.dll QRGS.dll
	mcs LTSQ.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:library

matrix.dll: $(DIR)/vector.cs $(DIR)/matrix.cs
	mcs -target:library -out:matrix.dll $^

QRGS.dll: QRGS.cs matrix.dll
		mcs QRGS.cs -reference:matrix.dll -target:library -out:QRGS.dll

IOhandle.dll: IOhandle.cs
	mcs IOhandle.cs -target:library -out:IOhandle.dll
