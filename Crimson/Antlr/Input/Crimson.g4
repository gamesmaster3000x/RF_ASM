grammar Crimson;

// Parser rules
scope
    : OpenBrace (imports+=importUnit)* (opHandlers+=operationHandler)* (statements+=statement)* CloseBrace 
    ; 

// Scope header things
importUnit
    : Hashtag Using path=String As identifier=fullName SemiColon
    ;
operationHandler
    : Hashtag OpHandler OpenBracket t1=type op=Operator t2=type CloseBracket RightArrow OpenBrace identifier=fullName CloseBrace SemiColon
    ;

// Function-only statements
statement
    : internalVariableDeclaration   #VariableDeclarationStatement
    | functionReturn                #ReturnStatement
    | assignVariable                #AssignVariableStatement
    | functionCall SemiColon        #FunctionCallStatement
    | ifBlock                       #IfStatement
    | whileBlock                    #WhileStatement
    | basicCall                     #BasicCallStatement
    | assemblyCall                  #AssemblyCallStatement
    | globalVariableDeclaration     #GlobalVariableStatement
    | functionDeclaration           #FunctionDeclarationStatement
    | structureDeclaration          #StructureDeclarationStatement
    ;
internalVariableDeclaration 
    : type fullName DirectEquals (complex=complexValue | simple=simpleValue) SemiColon
    ;
assignVariable
    : name=fullName DirectEquals (complex=complexValue | simple=simpleValue) SemiColon     #AssignVariableDirect
    | name=fullName PointerEquals (complex=complexValue | simple=simpleValue) SemiColon    #AssignVariableAtPointer
    ;
ifBlock
    : If condition scope (elseBlock | elseIfBlock)?
    ;
whileBlock
    : While condition scope
    ;
condition
    : OpenBracket op=operation CloseBracket
    ;
elseIfBlock
    : Else ifBlock
    ;
elseBlock
    : Else scope
    ;
basicCall
    : BasicCall basicText=String SemiColon
    ;
assemblyCall
    : AssemblyCall assemblyText=String SemiColon
    ;
globalVariableDeclaration
    : Global declaration=internalVariableDeclaration
    ;
scopedVariableDeclaration
    : Scoped declaration=internalVariableDeclaration
    ;
functionDeclaration
    : Function returnType=type header=functionHeader body=scope
    ;
functionHeader
	: name=fullName parameters=parameterList
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
Scoped: 'scoped';
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