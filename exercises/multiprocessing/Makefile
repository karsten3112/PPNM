N=1e8

TIME = time --portability --append --output $@
Out.txt: main.exe Makefile
	>$@
	echo "Calculation of harmonic sum using locally seperated threads" >>$@
	$(TIME) mono $< -terms:$N -threads:1 -sep >>$@
	$(TIME) mono $< -terms:$N -threads:2 -sep >>$@
	$(TIME) mono $< -terms:$N -threads:3 -sep >>$@
	$(TIME) mono $< -terms:$N -threads:4 -sep >>$@
	echo "-------------------------------------------------" >>$@
	echo "Calculation of harmonic sum using Parallel for-loop" >>$@
	$(TIME) mono $< -terms:$N -threads:1 -for >>$@
	$(TIME) mono $< -terms:$N -threads:2 -for >>$@
	$(TIME) mono $< -terms:$N -threads:3 -for >>$@
	echo "The sum differs due to the variable is not local, which means all threads are writing to same variable giving rise to race conditions" >>$@
	echo "This also explains the slower speed of Parallel.For" >>$@


main.exe: main.cs subroute.dll
	mcs main.cs -reference:subroute.dll -out:main.exe -target:exe

subroute.dll: subroute.cs
	mcs subroute.cs -target:library -out:subroute.dll

clean:
	rm -f *.dll *.txt *.exe
