# Crimson

## Contents
- [Language Syntax and Grammar](#Language-Syntax-and-Grammar)
  - [Packages](#Packages) 
    - [Declaring packages](#Declaring-Packages) 
    - [Importing packages](#Importing-Packages) 
  - [Statements](#Statements) 
    - [DeclarePackage](#DeclarePackage) 
    - [DeclareVariable](#DeclareVariable) 
    - [DeclareFunction](#DeclareFunction) 
    - [DeclareIf](#DeclareIf) 
    - [DeclareLoop](#DeclareLoop) 
    - [ImportPackage](#ImportPackage)
    - [CallFunction](#CallFunction)  
    - [SetVariable](#SetVariable) 
  - [Phrases](#Phrases) 
    - [ConditionPhrase](#ConditionPhrase) 
    - [ParameterPhrase](#ParameterPhrase) 
  - [Functions](#Functions) 
    - [Syntax](#Function-Syntax) 
  - [Memory Management](#Memory-Management) 
    - [Reserving memory](#Reserving-Memory) 
    - [Freeing memory](#Freeing-Memory) 
  - [Data Types](#Data-Types) 
    - [int](#int) 
    - [uint](#uint) 
    - [byte](#byte) 
    - [sbyte](#sbyte) 
    - [Notes on other data types](#Notes-on-data-types)
- [Compilation](#Compilation) 
  - [Compiler usage](#Compiler-usage) 

# Language Syntax and Grammar

## Packages
A package is a group of functions and variables which can be accessed in other compilations via imports. All functions and variables must sit within a package. Package names are unique and duplicate package names will cause compilation to fail.
### Declaring packages
```
packages program.utils {

    int function_one() {

    }

    void function_two() {
    
    }

    etc...
}
```
### Importing packages
Importing packages allows a programmer to use functions and variables in other compilations. 
```
import program.utils from otherprogram.crm;
```
During compilation, importing a package has the same effect as copying the package into the active code. All packages, whether or not imported, will be compiled simultaneously.

## Statements
### `DeclarePackage`
### `DeclareVariable`
### `DeclareFunction`
### `DeclareIf`
#### `DeclareElse`
### `DeclareLoop`
### `ImportPackage`
### `CallFunction`
### `SetVariable`

## Phrases
### `ConditionPhrase`
### `ParameterPhrase`

## Functions
### Function Syntax
A function is a repeatedly callable block of code which may or may not return a value.

The generic syntax of a function is as follows: 
```
return_type name (parameter_1_type parameter1, etc...) {
  // Body
  return a_value;
}
```

## Variables
### Variable Syntax
The generic syntax for declaring a variable is as follows: 
```
data_type name;
```
Memory is not assigned to a variable when it is declared (see notes on memory management).

To assign a value to a variable:
```
name = value;
```

## Memory Management
Crimson is intended to run on systems with limited memory, and requires a high level of programmer intervention to manage memory effectively.
### Reserving memory
Memory can be reserved with the keyword `allocate`.
```
int i;

// Reserve 4 bytes of memory and assign the pointer to 'i' to the first assigned address.
allocate i 4;

// The pointer to 'i' now points to the allocated memory.
```
### Freeing memory
Memory can be freed with the keyword `free`.
```
// Free memory at the pointer 'i'. Quantity determined automatically with metadata stored prior to i.
free i;

// The memory previously owned by 'i' can now be safely reused.
```
The quanity of memory to free may be determined automatically via metadata stored at the addresses prior to **'i'**. The size of **'i'** is stored at `*i - DATA_WIDTH` and is `DATA_WIDTH` wide.

## Data Types
### `int`
A signed integer whose width is equal to the data width of the assembly.
### `uint`
An unsigned integer whose width is equal to the data width of the assembly.
### `byte`
An unsigned byte of width 1 byte.
### `sbyte`
A signed byte of width 1 byte.
### Notes on data types
Unlike similar languages, such as C, Crimson does not feature the following data widths:
- `char` - A character.

# Compilation
## Compiler usage
## Example compilation
`main.crm`
```
import utils from utils.crm

package main {
    function main [int] () {
        int i;
        allocate i 4;
        i = 10;
        return utils.multiply(i, 5);
    }
}
```
`utils.crm`
```
package utils {
    function multiply [int] (int num1, int num2) {
        return num1 * num2;
    }
}
}
```