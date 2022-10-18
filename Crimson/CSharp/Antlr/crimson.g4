grammar crimson;

// Parser rules
program 
    : packageDefinitionList EOF
    ;

// Package
packageDefinitionList
    : packageDefinition*
    ;
packageDefinition
    : Package Identifier packageDependencyList packageBody
    ;
packageDependencyList
    : OpenBracket packageDependency? (Comma packageDependency)* CloseBracket
    ;
packageDependency
    : Identifier OpenBracket Identifier CloseBracket Identifier
    ;
packageBody
    : OpenBrace topLevelStatement* CloseBrace 
    ;

// Top level statements
topLevelStatement
    : globalVariableDeclaration
    | functionDeclaration
    ;
globalVariableDeclaration
    : Global parameterType Identifier SemiColon // Need to add =value or =func()
    ;
functionDeclaration
    : Function Identifier functionReturnType parameterList functionBody
    ;
functionReturnType
    : OpenSquare parameterType CloseSquare
    ;
functionBody
    : OpenBrace functionOnlyStatement* CloseBrace 
    ;

// Function-only statements
functionOnlyStatement
    : internalVariableDeclaration
    | functionReturn
    | functionCall
    ;
internalVariableDeclaration
    : parameterType Identifier Equals (functionCall | Value) SemiColon // Need to add =value or =func()
    ;
 
// Function
functionCall
    : Identifier inputParameters
    ;
inputParameters
    : OpenBracket Identifier? (Comma Identifier)* CloseBracket
    ;
functionReturn
    : Return Value SemiColon
    ;

// Parameters
parameterList
    : OpenBracket parameter? (Comma parameter)* CloseBracket 
    ;
parameter
    : parameterType Identifier
    ;
parameterType
    : Integer | Boolean
    ;

// Lexicon 
Package: 'package';
Function: 'function';
Global: 'global';
Return: 'return';

Integer: 'int';
Boolean: 'bool';

Equals: '=';
OpenBracket: '(';
CloseBracket: ')';
OpenSquare: '[';
CloseSquare: ']';
OpenBrace: '{'; 
CloseBrace: '}';
Comma: ','; 
Dot: '.'; 
SemiColon: ';'; 

Identifier
    : (Alphabetic | Punctuation) (Alphabetic | Digit | Punctuation)*
    ;
Value
    : (Alphabetic | Digit)+
    ;
fragment Alphabetic
    : [a-zA-Z]
    ;
fragment Digit
    : [0-9]
    ;
fragment Punctuation
    : [_.]
    ;