gamma svg: gamma.gpi gamma.data factorials.data
	gnuplot $<
	
gamma.data: main.exe
	mono $< > $@

main.exe: main.cs sfuns.dll
	mcs main.cs -target:exe -reference:sfuns.dll -out:main.exe

sfuns.dll: gamma.cs erf.cs
	mcs -target:library -out:$@ $^

factorials.data: Makefile
	>$@
	echo 0 1 >>$@
	echo 1 1 >>$@
	echo 2 2 >>$@
	echo 3 6 >>$@
	echo 4 24 >>$@
	echo 5 120 >>$@
