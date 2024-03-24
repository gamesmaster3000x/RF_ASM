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
A Berry (`.bry` file) contains Crimson source code, broken into segments (e.g. functions) which is ready to be linked.

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
    "items": {

        
        "example_scope.myfunc1": {
            "access": "local",
            "in": {
                "var_name": "type_name",
                "other_var": 4
            },
            "out": "example_scope.my_struct",

            // B64 of minified code
            // Looks something like "my_func(param);cool_stuff(5);"
            "src": "QW5kIHRoaXMgaXMgdGhlIHNvdXJjZSBjb2RlIGZvciB0aGUgZnVuY3Rpb24h",
        },

        // This global variable is in a nested scope!
        "example_scope.inner_scope.my_gvar": {
            "access": "global"
            "type": "example_scope.my_struct"
        },

        // This struct is hidden
        "example_scope.my_struct": {
            "access": "hidden"
        }
    }
}
```