N=1e8

TIME = time --portability --append --output $@
Out.txt: main.exe Makefile
	>$@
	$(TIME) mono $< -terms:$N -threads:1 >>$@
	$(TIME) mono $< -terms:$N -threads:2 >>$@
	$(TIME) mono $< -terms:$N -threads:3 >>$@
	$(TIME) mono $< -terms:$N -threads:4 >>$@


main.exe: main.cs subroute.dll
	mcs main.cs -reference:subroute.dll -out:main.exe -target:exe

subroute.dll: subroute.cs
	mcs subroute.cs -target:library -out:subroute.dll
