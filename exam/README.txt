Student ID: 202107603

Assignment nr. 3 % 26 = 3

-----------------------------------------------------------------------------------
Assigment:

Bi-linear interpolation on a rectilinear grid in two dimensions

Introduction:
A rectilinear grid (note that rectilinear is not necessarily cartesian nor regular) in two dimensions is a set of nx×ny points where each point can be adressed by a double index (i,j) 
where 1 ≤ i ≤ nx, 1 ≤ j ≤ ny and the coordinates of the point (i,j) are given as (xi,yj), where x and y are vectors with sizes nx and ny correspondingly. 
The values of the tabulated function F at the grid points can then be arranged as a matrix {Fi,j=F(xi,yj)}.

Problem:
Build an interpolating routine which takes as the input the vectors {xi} and {yj}, and the matrix {Fi,j} and returns the bi-linear interpolated value of the function at a given 2D-point p=(px,py).
Hints
See the chapter "Bi-linear interpolation" in the book.
The signature of the interpolating subroutine can be

static double bilinear(double[] x, double[] y, matrix F, double px, double py)

-----------------------------------------------------------------------------------
Solution:

The solution is given by the file: bilin.cs - the approach was very similar to earlier interpolation homeworks, done with object oriented programming-style.

This means that the bilin class is initiated as an object, and when initiated it calculates all the coefficients for the bilinear functions for the given list of x- and y values 
with the corresponding matrix of function values, which are required to create an instance. 

This made the process easier, as it allowed me to store the all coefficients in matrices as an attribute the object has. And it allowed me to only calculate the grid once
and then evaluate the needed amount of times afterwards.

After this I made an evaluate method which takes (double px, double py) and returns the interpolated value at the point.
This is done by two quick binsearches over the x- and y-lists given in the beginning, finding the corresponding places (i,j) and then allowing for the simple calculation
of the function in the given area.

The function used in the bi-linear interpolation is the one given in the "Bi-linear interpolation" chapter from the book. The coefficients that we solve for were obtained by
solving analytically and then applying.

Test1.gif - shows the first test of the bi-linear interpolation routine.
Irreg.png - shows bi-linear interpolation over f(x,y) over an "irregular" square grid.
Cylside.png and Cyltop.png - shows bi-linear interpolation done in cylindrical coordinates and then trans. into cartesian. 

-----------------------------------------------------------------------------------
Self-assesment:

I would asses this project to 10/10, as the 2D-interpolation on a rectilinear grid works as intended, and returns satisfactory results.


