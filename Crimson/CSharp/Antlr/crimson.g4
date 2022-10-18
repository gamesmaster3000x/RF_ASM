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
    : OpenBracket CloseBracket
    | OpenBracket packageDependency (Comma packageDependency)* CloseBracket
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
    : Global internalVariableDeclaration // Need to add =value or =func()
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
    | functionCall SemiColon
    ;
internalVariableDeclaration 
    : parameterType Identifier (Equals resolvableValue)? SemiColon // Need to add =value or =func()
    ;
assignVariable
    : Identifier Equals resolvableValue SemiColon
    ;
 
// Function
functionCall
    : Identifier inputParameters
    ;
inputParameters
    : OpenBracket Identifier? (Comma Identifier)* CloseBracket
    ;
functionReturn
    : Return resolvableValue SemiColon
    ;
resolvableValue
    : Value
    | functionCall
    ;

// Parameters
parameterList
    : OpenBracket CloseBracket
    | OpenBracket parameter (Comma parameter)* CloseBracket 
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
    : (Alphabetic) (Alphabetic | Digit | Punctuation)* (Alphabetic | Digit)
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