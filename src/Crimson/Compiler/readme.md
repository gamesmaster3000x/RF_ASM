# Crimson Compiler

Produces a Berry (`.bry` file) from a collection of Crimson (`.crm`) source files.

## Berries

The structure of a Berry (`.bry` file).

```
// Manifest hash
// - SHA-512 hash of the manifest
// - Started and terminated with '{' and '}'
// - Arranged in 64-byte rows
// - Cryptographically signed by the vendor
0000111100001111000011110000111100001111000011110000111100001111
1111000011110000111100001111000011110000111100001111000011110000
======END-MANIFEST-HASH======

// Manifest
// - JSON data
// Manifest
{
    // The functions contained within the Berry and their addresses
    "functions": {
        // name: address (decimal format)
        "my_func1": 0,
        "my_func2":
    }
}
======END-MANIFEST======

// Signed binary data hash
// - SHA-512 hash of binary data
// - Arranged in 64-byte rows
// - Cryptographically signed by the vendor
0000111100001111000011110000111100001111000011110000111100001111
1111000011110000111100001111000011110000111100001111000011110000
======END-BIN-HASH======

// Binary data
// - Raw binary data
// - Arranged in 64-byte rows
// - Started by first line of correct length
// - Terminated by '======END-BIN======'
0000111100001111000011110000111100001111000011110000111100001111
1111000011110000111100001111000011110000111100001111000011110000
...
... more data
...
0000111100001111000011110000111100001111000011110000111100001111
1111000011110000111100001111000011110000111100001111000011110000
======END-BIN======
```