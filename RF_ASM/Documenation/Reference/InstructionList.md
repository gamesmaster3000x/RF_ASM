Instructions:

Code     Hex   Info
HLT      00    Immediately stop the execution of instructions. Next instruction will not be read.
LDR α,β  01    Load the value from address β to the α register.
STM α,β  02    Store the value from the α register to the memory address β.
ADD      03    Add the values in registers 1 and 2, and store the result in 0.
SUB      04    Subtract the value of register 2 from 1 and store the result in 0.
LSL      05    Shift the value of register 1 to the left (x2) and store the result in 0.
LSR      06    Shift the value of register 1 to the right (/2) and store the result in 0.
CMP α,β  07    Compare value at α to β.
B α      08    Jump to the memory address at α.
BEQ α    09    If CMP returns equal, jump to the memory address at α. 
BLT α    0A    If CMP returns less than, jump to the memory address at α.
BGT α    0B    If CMP returns greater than, jump to the memory address at α.
BOF α    0C    If the overflow flag is active, jump to the memory address at α.
BSR α    0D    Push the next memory address to Stack, and jump to the memory address at α.
RTN      0E    Jump to the last memory address in stack, and remove it.
STB α,β  0F    Store the lowest byte of α to the memory address β.

Addressing modes:

Code Binary Info
#    0     Raw Value
*    1     Register Address
