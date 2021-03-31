Wake Up Neo
==========

Universal matrix calculator written on C#

## Current status

It is not production quality and is not ready for any professional and/or unsupervised use.
However, it should be generally stable.

## What works

- Basic matrix operations such as:
  - Addition
  - Subtraction
  - Multiplication 
  - Exponentiation (negative degrees too)
  - Determinant
  - Transpose
  - Rank
  - Inversing
- Creating variables through the command line
- Matrix expressions
- History of commands entered

## Platforms

Windows only :(

## Building

Requires **.NET Core 3.0** Something like this:

```
 dotnet build -c Release
```

## Getting binaries

If you don't want to build it yourself, you can find some pre-built Windows binaries in [Releases](https://github.com/n1ghtraven/WakeUpNeo/releases)

## How to use

Interaction with this calculator occurs using the command line. It serves to store a matrix into a variable and to enter a matrix expression. 

The expression for assigning a variable to a matrix looks like this: 

```
A = 1 2 3 : 4 5 6 : 7 8 9
```

Where ```A``` is the name of the variable and ```1 2 3``` are strings. Sign ```:``` serves as a column separator. There can be any number of spaces between numbers, separator and equals. To change the value of a matrix element, go to the variable tab change the element value and press ``` Enter```

The matrix expression can look like this: 

```
(A*B)-(C^T)
```

The following concepts are used for the calculation: 

- Subtraction is a sign ```-```
- Addition is a sign ```+```
- Multiplication is a sign ```*```
- Exponentiation is a sign ```^```
- You can calculate the inverse matrix either by raising the matrix to the power of "-1" for example like this ```  A^-1```, or use the function ```inv()``` and pass the matrix variable as an argument
- To calculate the determinant, use the ```det() ``` function and pass it the matrix variable as an argument
- For transposition, it is enough to raise the matrix to the power of T (the capital letter is important) or use the function ```trans()```  for example like this ```  A^T``` or ```  trans(A)```
- To calculate the rank, it is enough to use the function ```rank()```

To delete a variable from memory, go to the tab with it and click on the button. The ```T``` variable is reserved and this name cannot be used. 

## Contributor

Special thanks to **Alina Kapustina** for her help with testing 

