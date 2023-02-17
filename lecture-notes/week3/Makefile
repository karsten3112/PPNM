main.exe: main.cs input.dll
	mcs -out:$@ $(filter %.cs,$^) $(addprefix -reference:,$(filter %.dll,$^))

input.dll: input.cs
	mcs -target:library -out:$@ $(filter %.cs,$^)
