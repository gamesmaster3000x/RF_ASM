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
    : OpenBrace statement* CloseBrace 
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

// Statements
statement
    : Identifier SemiColon
    ;

// Lexicon
Package: 'package';

Integer: 'int';
Boolean: 'bool';

OpenBracket: '(';
CloseBracket: ')';
OpenBrace: '{';
CloseBrace: '}';
Comma: ','; 
SemiColon: ';'; 
    
Identifier
    : NonDigit+
    ;
fragment NonDigit 
    : [a-zA-Z_.]
    ;