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
    : Hashtag OpHandler op=Operator RightArrow identifier=fullName SemiColon
    ;

// Function-only statements
statement
    : functionReturn                #ReturnStatement
    | assignVariable                #AssignVariableStatement
    | functionCall SemiColon        #FunctionCallStatement
    | ifBlock                       #IfStatement
    | whileBlock                    #WhileStatement
    | basicCall                     #BasicCallStatement
    | assemblyCall                  #AssemblyCallStatement
    | globalVariableDeclaration     #GlobalVariableStatement
    | scopeVariableDeclaration      #ScopeVariableStatement
    | functionDeclaration           #FunctionDeclarationStatement
    | structureDeclaration          #StructureDeclarationStatement
    ;
assignVariable
    : name=ShortName DirectEquals (complex=complexValue | simple=simpleValue) size=datasize SemiColon     #AssignVariableDirect
    | name=ShortName PointerEquals (complex=complexValue | simple=simpleValue) size=datasize SemiColon    #AssignVariableAtPointer
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
	: Global assignment=assignVariable
    ;
scopeVariableDeclaration
    : Scope name=ShortName size=datasize SemiColon
    ;
functionDeclaration
    : Function header=functionHeader body=scope
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
    : Number
	| String
	;
operation
	: leftValue=simpleValue operator=Operator rightValue=simpleValue
	;

// Parameters
parameter
    : name=ShortName size=datasize
    ;
parameterList 
    : OpenBracket CloseBracket
    | OpenBracket parameter (Comma parameter)* CloseBracket 
    ;

// Structures
structureDeclaration
    : Structure name=fullName body=structureBody
    ;
structureBody
    : OpenBrace scopeVariableDeclaration* CloseBrace
    ;
array
    : OpenSquare blockSize=datasize CloseSquare
    ;
datasize
	: OpenTriangle sizeVal=simpleValue CloseTriangle
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
Scope: 'scope';
Return: 'return';
Structure: 'structure';
Using: 'using';
OpHandler: 'ophandler';
As: 'as';
If: 'if';
While: 'while';
Else: 'else';
Elif: 'elif';

Operator: Comparator | MathsOperator;

fragment Plus: '+'; 
fragment Minus: '-'; 
Asterisk: '*'; 
fragment Slash: '/';
MathsOperator: Plus | Minus | Asterisk | Slash;

fragment Less: '<?';
fragment LessEqual: '<=';
fragment Greater: '>?';
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
Colon: ':';
OpenBrace: '{'; 
CloseBrace: '}';
OpenTriangle: '<'; 
CloseTriangle: '>'; 
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