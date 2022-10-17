grammar crimson;

// Parser rules

program 
    : packageDefinitionList EOF
    ;

packageDefinitionList
    : packageDefinition*
    ;

packageDefinition
    : Package Identifier parameterList packageBody
    ;
    
parameterList
    : OpenBracket parameter? (Comma parameter)* CloseBracket
    ;

parameter
    : parameterType Identifier
    ;

packageBody
    : OpenBrace statement* CloseBrace 
    ;

parameterType
    : Integer | Boolean
    ;
    
statement
    : Identifier SemiColon
    ;

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
    : [a-zA-Z_]
    ;