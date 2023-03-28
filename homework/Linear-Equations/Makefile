DIR = ../../homework/Linear-Equations/matrix-classes

all: Fit.svg Test.txt

Fit.svg: fit.gpi out.times.data
	gnuplot fit.gpi

Test.txt: main.exe
	mono $^ -QRGS-test > Test.txt

out.times.data : main.exe
	>$@
	for N in $$(seq 100 20 700); do \
		time --format "$$N %e" --output $@ --append \
		mono $< -TimeIt:$$N ;\
	done 

main.exe: main.cs QRGS.dll matrix.dll
	mcs main.cs $(addprefix -reference:,$(filter %.dll,$^))
	
QRGS.dll: QRGS.cs matrix.dll
	mcs QRGS.cs $(addprefix -reference:,$(filter %.dll,$^)) -target:library -out:QRGS.dll

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:matrix.dll $^
