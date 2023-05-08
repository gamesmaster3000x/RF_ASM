# Crimson Compiler
Produces a Berry (`.bry` file) from a collection of Crimson (`.crm`) source files.

## Stages
Compilation occurs in several stages:

| Stage        | Purpose |
| -            | -       |
| Parsing      | Create an in-code representation of the source code (an abstract syntax tree). |
| Mapping      | Convert textual references within the syntax tree to their associated objects within the tree. |
| Generalising | Convert the abstract syntax tree into fragments of non-language-dependent assembly. |
| Specialising | Assemble the fragments into target-specific machine code (e.g. RFASM or x86) |
| Packing      | Pack the machine code fragments into a Berry which can later be given to a linker. |

## Structure of a Berry
A Berry (`.bry` file) contains several things:
 - Machine code, already specialised for an architecture.
 - The metadata needed to link it the machine code.
 - The source code for that machine code.

```jsonc
/* 
 * Manifest hash
 *     In psuedocode: dilithium( sha512 ( base64encode ( {manifest} ) ) )
 *     Arranged in 64-character rows
 */
>>>>>>START-MANIFEST-HASH<<<<<<
0000111100001111000011110000111100001111000011110000111100001111
1111000011110000111100001111000011110000111100001111000011110000
>>>>>>END-MANIFEST-HASH<<<<<<
/*
 * Manifest
 *     JSON data
 */
{
    // The width of an instruction or address in bytes (i.e. the size of an integer)
    "data_width": 4,

    // The machine code this Berry is targetting (e.g. RFASM/RFVM, x86, AMD-64)
    "target": "rfvm",

    // An array of the functions this Berry would like to link with (indices used in 'items' below)
    "links": [
        "func_myfunc1", // Function in this Berry (declared in 'items' below)
        "func_stevenfunc", // Functions in another Berry
        "gvar_mygvar", // Global variable in this Berry (declared in 'items' below)
        "gvar_stevengvar" // Global variable in another Berry (who is this 'Steven' guy?)
    ],

    "items": {
        "functions" {
            "func_myfunc1": {
                // The machine code for the function (b64 encoded, not linked)
                "bin": "VGhpcyBpcyB0aGUgbWFjaGluZSBjb2RlIGZvciB0aGUgZnVuY3Rpb24h",
                // The source code for the function (b64 encoded)
                "src": "QW5kIHRoaXMgaXMgdGhlIHNvdXJjZSBjb2RlIGZvciB0aGUgZnVuY3Rpb24h",
                // Bytes 14, 16 and 25 in the DECODED (non-b64) machine code should be linked
                // to link 0 (func_myfunc1); bytes 25 and 42 to link 3 (gvar_stevengvar)
                "links": "0:14,16,25;3:25,42;" 
            }
        },
        "global_variables": {
            // Reserve 4 bytes for mygvar (it's 4 bytes wide)
            "gvar_mygvar": 4
        }
    }
}
```