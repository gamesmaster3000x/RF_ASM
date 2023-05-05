# RedFoxAssembly Compiler

## Running the Compiler
If compiling using Visual Studio, the output `.exe` will be in `/RedFoxAssembly/bin/[Debug/Release]/{.NET version}`. For me, this path is `/RedFoxAssembly/bin/Debug/net6.0/`.

Run the compiler `.exe` from the command line.
```
// Example command to run the compiler executable

RedFoxAssembly.exe -a value --
```

### Arguments
| Short Flag | Long Flag | Arguments | Purpose | Usage |
| --- | --- | --- | --- |
| `-w` | `--dataWidth` | int | Specify the width of a word in the compilation. This may be overwritten by the program itself. | `-w 4` |
| `-s` | `--source` | string | Specify the relative path to the source code to compile. | `-s ../Path/To/Program.rfp` |
| `-m` | `--metapath` | string | Specify the path to the metadata JSON file. | `-m ../Path/To/Metdata.json` |
| - | `--help` | - | Display a help message with commands. | `--help` |
| - | `--version` | - | Display version information | `--version` |

## Metadata
A JSON file containing plaintext metadata about the current compilation.

### Fields
| Name | Value | Purpose |
| --- | --- | --- |
| Authors | Array of strings | List the names of the authors of this program. |
| AddAuthors | Boolean | Add the names of the authors to the binary file. |
| AddDateTime | Boolean | Add the date and time of compilation to the binary file. |
| AddConstants | Boolean | Add the names and values of constants in the assembly program to the binary file. (Useful for debugging/inspecting the binary) |
| AddLabels | Boolean | Add the names and addresses of labels in the assembly program to the binary file. (Useful for debugging/inspecting the binary) |

Example JSON:
```json
{
	"Authors": ["Me", "You"],
	"AddAuthors": true,
	"AddDateTime": true,
	"AddConstants": true,
	"AddLabels": true
}
```

## Structure of a Program

A program consists of, in order:
1) A list of configuration statements
2) A list of command statements 
2) A `.end` file terminator

## Statements

### Configuration Statements
| Statement | Argument #1 | Argument #2 | Purpose | Example |
| --- | --- | --- | --- | --- |
| `.width` | (int) width | - | To set the width of a word in this program. | `.width 5` |
| `.word` | (id) name | (word) value | To set a value which will be preprocessed by the compiler and can be inserted into future statements. | `.word _FOO_ 0x01AB` |
| `.byte` | (id) name | (byte) value | To set a value which will be preprocessed by the compiler and can be inserted into future statements. | `.byte _BAR_ 0xCD` |

Declaring multiple constants (`.word` or `.byte`) with the same `{id}` will cause a compiler error.

### Command Statements
| Statement | Purpose | Syntax | Argument #1 | Argument #2 | Example |
| --- | --- | --- | --- | --- |
| Label  | Declare a point which may be jumped to from elsewhere in the program.| `::_{id}_`  | `(id) id`: An identifier to name this label. | - | `::_POINT_`
| Repeat | Repeat a selection of bytes to fill space in the output binary. | `.repeat {times} {pattern}` | `(int) times`: The number of times the pattern should repeat | `(byte[]) pattern` | `.repeat 15 0x05 0x06 _NAMEOFCONSTANT_` |
| Instruction | Instruct the CPU. | `{instruction} {arg1} {arg2}` | See instruction reference | <- | <-
| End | The final statement in the program. Used by ANTLR to detect the end of the file. Adding statements after this may cause undocumented behaviors. | `{instruction} {arg1} {arg2}` | See instruction reference | <- | <-

### Other Statements
| Statement | Argument #1 | Argument #2 | Purpose | Example |
| --- | --- | --- | --- | --- |
| `.end` | - | - | The final statement in the program. Used by ANTLR to detect the end of the file. Adding statements after this may cause undocumented behaviors. | `.end` |

### Addressing Modes and Register Prefixes
| Mode | Prefix | Addressing Mode | Offset |
| --- | --- | --- | --- |
| Hexadecimal | `0x` | `None` | - |
| Register | `Rx` | `Register` | 0  | 
| SpecialisedRegister | `Sx` | `Register` | 0 | 
| ComponentRegister | `Cx` | `Register` | +64 | 
| GeneralRegister | `Gx` | `Register` | +128 | 

### Data Types
| Type | Description | Example | Notes |
| --- | --- | --- | --- |
| Byte | A byte, written in hexadecimal... Idk what else to say look it up if you don't know. | `0x69` | `0x` may be replaced with an addressing mode. |
| Word | A number of bytes equal to the current data width. | For data width 3: `0x12CD4F` | `0x` may be replaced with an addressing mode. |
| Int | A decimal number. | `420` | - |
| ID | A name to identify a thing. Started and terminated with an underscore (`_`). May contain alphanumberic characters (`0-9`, `a-z`, `A-Z`) and underscores. Format: `_{0-9a-zA-Z}_` | `_C00L_NAM3_` | - |

## Instructions
The full list of instructions can be found in the RFVM (Red Fox Virtual Machine) documentation, but you're particularly nosy you can look in `/CSharp/Antlr/Input/RedFoxAssembly.g4` to see the exact version which this compiler is using.

## Example Program
```
// Example program (might do something I haven't checked)
.width 2
.word _OpA_ 0x2358
.word _OpB_ 0x41f0

.repeat 15 0x05 0x06 0x07

JMP _start_

::_label_

SUB
RRW 0x00
WRW 0x80
RTN

::_start_

RVW _OpA_
WRW 0x01

RVW _OpB_
WRW 0x02

BSR _label_

RVB 0x80
WRB 0x85 0x00

RRW Rx85
HLT
.end
```