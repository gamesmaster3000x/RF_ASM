grammar Crimson;

// Parser rules
translationUnit 
    : (heapAllocator=heapMemoryAllocator) (imports+=importUnit)* (opHandlers+=operationHandler)* (statements+=globalStatement)* eof=EOF
    ;

// Compilation-Unit statements
heapMemoryAllocator
	: Hashtag Allocator header=functionHeader
	;
importUnit
    : Hashtag Using path=String As identifier=Identifier
    ;
operationHandler
    : Hashtag OpHandler OpenBracket t1=type op=Operator t2=type CloseBracket RightArrow header=functionHeader
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
    : Function returnType=type header=functionHeader body=functionBody
    ;
functionHeader
	: name=Identifier parameters=parameterList
	;
functionBody
    : OpenBrace (statements+=internalStatement)* CloseBrace 
    ; 

// Function-only statements
internalStatement
    : internalVariableDeclaration   #FunctionVariableDeclarationStatement
    | functionReturn                #FunctionReturnStatement
    | assignVariable                #FunctionAssignVariableStatement
    | functionCall SemiColon        #FunctionFunctionCallStatement
    | ifBlock                       #FunctionIfStatement
    | whileBlock                    #FunctionWhileStatement
    | assemblyCall                  #FunctionAssemblyCallStatement
    ;
internalVariableDeclaration 
    : type Identifier DirectEquals (complex=complexValue | simple=simpleValue) SemiColon
    ;
assignVariable
    : Identifier DirectEquals (complex=complexValue | simple=simpleValue) SemiColon     #AssignVariableDirect
    | Identifier PointerEquals (complex=complexValue | simple=simpleValue) SemiColon    #AssignVariableAtPointer
    ;
ifBlock
    : If condition functionBody (elseBlock | elseIfBlock)?
    ;
whileBlock
    : While condition functionBody
    ;
condition
    : OpenBracket op=operation CloseBracket
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
    : OpenBracket (simpleValue)? (Comma (simpleValue))* CloseBracket
    ;
functionReturn
    : Return simpleValue SemiColon
    | Return SemiColon
    ;
simpleValue
	: id=Identifier pointer=Asterisk?
	| raw=rawValue
	;
complexValue
	: op=operation
	| func=functionCall
	;
rawValue
    : Null
    | Number
    | BooleanValue
	;
operation
	: leftValue=simpleValue operator=Operator rightValue=simpleValue
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
    : name=rawType pointer=Asterisk?
	;
rawType
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

Allocator: 'allocator';
Function: 'function';
Global: 'global';
Return: 'return';
Structure: 'structure';
Using: 'using';
OpHandler: 'ophandler';
As: 'as';
If: 'if';
While: 'while';
Else: 'else';
Elif: 'elif';

Integer: 'int';
Boolean: 'bool';
Null: 'null';

fragment True: 'true';
fragment False: 'false';
BooleanValue: True | False;

Operator: Comparator | MathsOperator;

fragment Plus: '+'; 
fragment Minus: '-'; 
Asterisk: '*'; 
fragment Slash: '/';
MathsOperator: Plus | Minus | Asterisk | Slash;

fragment Less: '<';
fragment LessEqual: '<=';
fragment Greater: '>';
fragment GreaterEqual: '>=';
fragment EqualTo: '==';
Comparator: Less | LessEqual | Greater | GreaterEqual | EqualTo;

RightArrow: '->';
Tilda: '~';
DirectEquals: '=';
PointerEquals: '*=';
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
    : (WhiteSpace | Newline | LineComment | BlockComment) -> skip
    ;
LineComment 
    : '//' ~('\r' | '\n')*
    ;
BlockComment 
    : '/*' .* '*/'
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