grammar rfasm;

// Parser rules

program 
    : statement+ EOF
    ;

statement
    : instruction
    | directive
    | label
    ;

label
    : COLON argument
    ;

directive
    : WIDTH argument
    | VALUE argument argument
    ; 

instruction
    : HLT
    | LDR argument argument
    | STM argument argument
    | ADD
    | SUB
    | LSL
    | LSR 
    | CMP argument argument 
    | B   argument
    | BEQ argument
    | BLT argument
    | BGT argument
    | BOF argument
    | BSR argument
    | RTN 
    | STB argument argument
    ;

argument
    : String
    ;

Comment: '//' ~[\r\n]* -> skip;

WhiteSpace : (' '|'\t') -> skip;

WIDTH: '.width';
VALUE: '.val';

COLON: ':';

HLT: 'HLT';
LDR: 'LDR';
STM: 'STM';
ADD: 'ADD';
SUB: 'SUB';
LSL: 'LSL';
LSR: 'LSR';
CMP: 'CMP';
B  : 'B';
BEQ: 'BEQ';
BLT: 'BLT';
BGT: 'BGT';
BOF: 'BOF';
BSR: 'BSR';
RTN: 'RTN';
STB: 'STB';

Char: [a-zA-Z#*_0-9];
String: Char+;