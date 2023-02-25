grammar Crimson;

// Parser rules
translationUnit 
    : (imports+=importUnit)* (opHandlers+=operationHandler)* (statements+=globalStatement)* eof=EOF
    ;

// Compilation-Unit statements
importUnit
    : Hashtag Using path=String As identifier=fullName SemiColon
    ;
operationHandler
    : Hashtag OpHandler OpenBracket t1=type op=Operator t2=type CloseBracket RightArrow OpenBrace identifier=fullName CloseBrace SemiColon
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
	: name=fullName parameters=parameterList
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
    | basicCall                     #FunctionBasicCallStatement
    | assemblyCall                  #FunctionAssemblyCallStatement
    ;
internalVariableDeclaration 
    : type fullName DirectEquals (complex=complexValue | simple=simpleValue) SemiColon
    ;
assignVariable
    : name=fullName DirectEquals (complex=complexValue | simple=simpleValue) SemiColon     #AssignVariableDirect
    | name=fullName PointerEquals (complex=complexValue | simple=simpleValue) SemiColon    #AssignVariableAtPointer
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
basicCall
    : BasicCall basicText=~('\r' | '\n')*
    ;
assemblyCall
    : AssemblyCall assemblyText=~('\r' | '\n')*
    ;
 
// Function
functionCall
    : name=fullName args=arguments
    ;
arguments
    : OpenBracket (simpleValue)? (Comma (simpleValue))* CloseBracket
    ;
functionReturn
    : Return simpleValue SemiColon
    | Return SemiColon
    ;
simpleValue
	: id=fullName pointer=Asterisk?
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
    : t=type name=fullName
    ;

// Structures
structureDeclaration
    : Structure name=fullName body=structureBody
    ;
structureBody
    : OpenBrace internalVariableDeclaration* CloseBrace
    ;

// Types 
type
    : Integer
    | Boolean
    | fullName
    | array
    | Null
	;
array
    : OpenSquare type CloseSquare
    ;

// Misc
fullName
	: (libraryName=ShortName Dot)? memberName=ShortName
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
BasicCall: 'B~';
AssemblyCall: 'A~';
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
    : '/*' .*? '*/'
    ;
Number
    : Digit+
    ;
String
    : Quote ~('"')* Quote
    ;
ShortName
    : (Alphabetic) (Alphabetic | Number | Underscore)*
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