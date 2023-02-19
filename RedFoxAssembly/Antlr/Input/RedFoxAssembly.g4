grammar RedFoxAssembly;

program
	: (configurations+=configuration)+ (commands+=command)+ (end)
	;
	
// Metadata (not currently used)
metadata
	: literal EOL #LiteralMetadata
	;

literal
	: Literal contents=~(EOL | Literal)* Literal
	;

// Configuration

configuration
	: width EOL  #WidthConfiguration
	| value EOL  #ValueConfiguration
	;

width
	: Width Blank val=Digit
	;

value
	: WordValue Blank id=Identifier Blank wordValue=word
	| ByteValue Blank id=Identifier Blank byteValue=byte
	;

// Instructions

command
	: label EOL         #LabelCommand
	| instruction EOL   #InstructionCommand
	| repeat EOL        #RepeatCommand
	;

label
	: LabelStart id=Identifier
	;

repeat
	: Repeat Blank (times+=Digit)+ (Blank bytes+=byte)+
	;

instruction
	: hlt #HLTInstruction
	| nop #NOPInstruction
	| add #ADDInstruction
	| sub #SUBInstruction
	| lsl #LSLInstruction
	| lsr #LSRInstruction
	| neg #NEGInstruction
	| not #NOTInstruction
	| cmp #CMPInstruction
	| jmp #JMPInstruction
	| bfg #BFGInstruction
	//| lsl #LSLInstruction
	//| lsl #LSLInstruction
	//| lsl #LSLInstruction
	| bsr #BSRInstruction
	| rtn #RTNInstruction
	| rrb #RRBInstruction
	| rrw #RRWInstruction
	| rmb #RMBInstruction
	| rmw #RMWInstruction
	| wrb #WRBInstruction
	| wrw #WRWInstruction
	| wmb #WMBInstruction
	| wmw #WMWInstruction
	| rvb #RVBInstruction
	| rvw #RVWInstruction
	| sin #SINInstruction
	| int #INTInstruction
	| sfg #SFGInstruction
	| and #ANDInstruction
	| lor #LORInstruction
	| xor #XORInstruction
	;

hlt: op=HLT;
nop: op=NOP;
add: op=ADD;
sub: op=SUB;
lsl: op=LSL;
lsr: op=LSR;
neg: op=NEG;
not: op=NOT;
cmp: op=CMP;
jmp: op=JMP Blank arg1w=word;
bfg: op=BFG Blank arg1w=word arg2b=byte;
//
//
//
bsr: op=BSR Blank arg1w=word;
rtn: op=RTN;
rrb: op=RRB Blank arg1b=byte Blank arg2b=byte;
rrw: op=RRW Blank arg1b=byte;
rmb: op=RMB Blank arg1w=word;
rmw: op=RMW Blank arg1w=word;
wrb: op=WRB Blank arg1b=byte Blank arg2b=byte;
wrw: op=WRW Blank arg1b=byte;
wmb: op=WMB Blank arg1w=word;
wmw: op=WMW Blank arg1w=word;
rvb: op=RVB Blank arg1b=byte;
rvw: op=RVW Blank arg1w=word;
sin: op=SIN Blank arg1w=word Blank arg2b=byte;
int: op=INT Blank arg1b=byte;
sfg: op=SFG Blank arg1b=byte Blank arg2b=byte;
and: op=AND;
lor: op=LOR;
xor: op=XOR;

end: End EOF;

// Data

word
	: isHex=HexPrefix (hexData+=bytedata)+
	| registerTarget=RegisterPrefix registerData=bytedata
	| val=Identifier
	;

byte
	: isHex=HexPrefix hexData=bytedata
	| registerTarget=RegisterPrefix registerData=bytedata
	| val=Identifier
	;

bytedata
	: (ByteLetter | Digit) (ByteLetter | Digit)
	;


// ===== LEXER =====

LabelStart: '::';
Width: '.width';
Repeat: '.repeat';
Literal: '$';
WordValue: '.word';
ByteValue: '.byte';
End: '.end';

HLT: 'HLT';
NOP: 'NOP';
ADD: 'ADD';
SUB: 'SUB';
LSL: 'LSL';
LSR: 'LSR';
NEG: 'NEG';
NOT: 'NOT';
CMP: 'CMP';
JMP: 'JMP';
BFG: 'BFG';
// HLT: 'HLT';
// HLT: 'HLT';
// HLT: 'HLT';
BSR: 'BSR';
RTN: 'RTN';
RRB: 'RRB';
RRW: 'RRW';
RMB: 'RMB';
RMW: 'RMW';
WRB: 'WRB';
WRW: 'WRW';
WMB: 'WMB';
WMW: 'WMW';
RVB: 'RVB';
RVW: 'RVW';
SIN: 'SIN';
INT: 'INT';
SFG: 'SFG';
AND: 'AND';
LOR: 'LOR';
XOR: 'XOR';

RegisterPrefix
	: Register 
	| GeneralRegister 
	| ComponentRegister 
	| SpecialisedRegister
	;
fragment Register: 'Rx';
fragment GeneralRegister: 'Gx';
fragment ComponentRegister: 'Cx';
fragment SpecialisedRegister: 'Sx';
HexPrefix: '0x';

Underscore: '_'; 
Quote: '\'';
Blank: Space | Tab;
Space: ' ';
Tab: '\t';

Digit: [0-9];
ByteLetter: [a-fA-F];

Identifier
    : Underscore (Alphabetic | Digit | Underscore)+ Underscore
    ;
SkipTokens
    : (LineComment) -> skip
    ;
LineComment 
    : '//' ~('\r' | '\n')*
    ;
EOL
    : [\r\n]+
    | EOF
    ;
fragment Alphabetic
    : [a-zA-Z]
    ;
fragment WhiteSpace
    : [ \t]+
    ;
