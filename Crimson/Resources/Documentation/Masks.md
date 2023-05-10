# Masks
Masks are Crimson's answer to C's `struct`s and allow for more complex
data structures without type-checking.

## Mono-masks vs. Multi-masks
Mono-masks conceal a single 

## Declaration
```
// This is a mono-mask
mask Name<size>;

// These are multi-masks
mask Name(param1<size>);
mask Name(param1<size>, param2<size>);
mask Name(param1<size>, param2<size>, ...);


mask Int<4>;
mask Fraction(numerator<Int$>, denominator<Int$>)
```

## Usage
The size of a mask is referenced by `$MaskName` (roughly `sizeof(MaskName)`).

A multi-mask's value can be referenced by `variable$MaskName.parameterName`.

To retrieve data from a mono-mask, or to retrieve all data from a multi-mask, use:
`scope variable<$MaskName> = masked$MaskName;`

Example usage:
```
// Creates the scope variable 'fraction' of the size of the 'Fraction' mask
scope fraction<$Fraction>;

// Set the values of the numerator and denominator to 4
fraction$Fraction.numerator *= 4;
fraction$Fraction.denominator *= 4;

// This does not work because you cannot reassign 
// where the parameters of the mask point to
fraction$Fraction.numerator = 4;

// Pass the pointer of the denominator to my_function
my_function( *fraction$Fraction.denominator );
```

In C#, this is *roughly* equivalent to:

```cs
Fraction fraction = new Fraction();
fraction.numerator = 4;
fraction.denominator = 4;
my_function(fraction.denominator);
```

*Note: Of course C# and Crimson are completely different languages so these
examples are functionally entirely different (for example in memory
management). This only serves to provide a more familiar analogue for
the usage of Crimson's masks.*