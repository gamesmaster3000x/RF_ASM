grammar Crimson;

// Parser rules
compilationUnit 
    : (imports+=importUnit)* (statements+=globalStatement)* eof=EOF
    ;

// Compilation-Unit statements
importUnit
    : Hashtag Using path=String As identifier=Identifier
    ;
globalStatement
    : globalVariableDeclaration #GlobalVariableUnitStatement
    | functionDeclaration       #FunctionUnitStatement
    | structureDeclaration      #StructureUnitStatement
    ;
globalVariableDeclaration
    : Global declaration=internalVariableDeclaration // Need to add =value or =func()
    ;
functionDeclaration
    : Function name=Identifier returnType=type parameters=parameterList body=functionBody
    ; 
functionBody
    : OpenBrace (statements+=internalStatement)* CloseBrace 
    ; 

// Function-only statements
internalStatement
    : internalVariableDeclaration 	#FunctionVariableDeclarationStatement
    | functionReturn 				#FunctionReturnStatement
    | assignVariable 				#FunctionAssignVariableStatement
    | allocateMemory 				#FunctionAllocateMemoryStatement
    | functionCall SemiColon 		#FunctionFunctionCallStatement
    | ifBlock 						#FunctionIfStatement
    | assemblyCall 					#FunctionAssemblyCallStatement
    ;
internalVariableDeclaration 
    : type Identifier (Equals resolvableValue)? SemiColon // Need to add =value or =func()
    ;
assignVariable
    : Identifier Equals resolvableValue SemiColon
    ;
ifBlock
    : If condition functionBody (elseBlock | elseIfBlock)?
    ;
condition
    : OpenBracket BooleanValue CloseBracket
    | OpenBracket resolvableValue Comparator resolvableValue CloseBracket
    ;
elseIfBlock
    : Else ifBlock
    ;
elseBlock
    : Else functionBody
    ;
assemblyCall
    : Tilda assemblyText=~('\r' | '\n')*
    ;
 
// Function
functionCall 
    : Identifier arguments
    ;
arguments
    : OpenBracket (resolvableValue)? (Comma (resolvableValue))* CloseBracket
    ;
allocateMemory
    : Allocate Identifier Number SemiColon
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

Function: 'function';
Global: 'global';
Return: 'return';
Allocate: 'allocate';
Structure: 'structure';
Using: 'using';
As: 'as';
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

Tilda: '~';
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
Hashtag: '#'; 
Quote: '"'; 

SkipTokens
    : (WhiteSpace | Newline | LineComment) -> skip
    ;
LineComment 
    : '//' ~('\r' | '\n')*
    ;
Number
    : Digit+
    ;
String
    : Quote ~('"')* Quote
    ;
Identifier
    : (Alphabetic) 
    | (Alphabetic) (Alphabetic | Number | Underscore | Dot)* (Alphabetic | Number)
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
fragment WhiteSpace
    : [ \t]+
    ;
fragment Newline
    : [\r\n]+
    | EOF
    ;