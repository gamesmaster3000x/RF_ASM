grammar Crimson;

// Parser rules
compilationUnit 
    : packageDefinitions=packageDefinitionList eof=EOF 
    ;

// Package
packageDefinitionList
    : (definitions+=packageDefinition)*
    ;
packageDefinition
    : Package name=Identifier dependencies=packageDependencyList body=packageBody
    ;
packageDependencyList
    : OpenBracket CloseBracket
    | OpenBracket dependencies+=packageDependency (Comma dependencies+=packageDependency)* CloseBracket
    ;
packageDependency
    : packageName=Identifier OpenBracket path=Identifier CloseBracket customName=Identifier
    ;
packageBody
    : OpenBrace (topLevelStatements+=topLevelStatement)* CloseBrace 
    ;

// Top level statements
topLevelStatement
    : globalVariableDeclaration
    | functionDeclaration 
    | structureDeclaration
    ;
globalVariableDeclaration
    : Global declaration=internalVariableDeclaration // Need to add =value or =func()
    ;
functionDeclaration
    : Function name=Identifier returnType=type parameters=parameterList body=functionBody
    ;
functionBody
    : OpenBrace (statements+=functionOnlyStatement)* CloseBrace 
    ;

// Function-only statements
functionOnlyStatement
    : internalVariableDeclaration
    | functionReturn
    | functionCall SemiColon
    | ifBlock
    ;
internalVariableDeclaration 
    : type Identifier (Equals resolvableValue)? SemiColon // Need to add =value or =func()
    ;
assignVariable
    : Identifier Equals resolvableValue SemiColon
    ;
ifBlock
    : If condition functionBody (elseBlock | elifBlock)?
    ;
condition
    : OpenBracket BooleanValue CloseBracket
    | OpenBracket resolvableValue Comparator resolvableValue CloseBracket
    ;
elifBlock
    : Elif condition functionBody (elseBlock | elifBlock)?
    ;
elseBlock
    : Else functionBody
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
    | Return SemiColon
    ;
resolvableValue
    : Identifier
    | Number
    | functionCall
    | Null
    | BooleanValue
    ;

// Parameters 
parameterList 
    : OpenBracket CloseBracket
    | OpenBracket parameter (Comma parameter)* CloseBracket 
    ;
parameter
    : type Identifier
    ;

// Structures
structureDeclaration
    : Structure Identifier structureBody
    ;
structureBody
    : OpenBrace internalVariableDeclaration* CloseBrace
    ;

// Types 
type
    : Integer
    | Boolean
    | Identifier
    | array
    | Null
    ;
array
    : OpenSquare type CloseSquare
    ;

/*
 * =
 * LEXER 
 * =
 */

Package: 'package';
Function: 'function';
Global: 'global';
Return: 'return';
Structure: 'structure';
If: 'if';
Else: 'else';
Elif: 'elif';

Integer: 'int';
Boolean: 'bool';
Null: 'null';

fragment True: 'true';
fragment False: 'false';
BooleanValue: True | False;

fragment Less: '<';
fragment Greater: '>';
fragment EqualTo: '==';
Comparator: Less | Greater | EqualTo;

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
Underscore: '_'; 

Number
    : Digit+
    ;
Identifier
    : (Alphabetic) (Alphabetic | Number | Underscore | Dot)* (Alphabetic | Number)
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