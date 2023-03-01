# The Stack in Crimson
Crimson's stack counts up in memory, ie. the memory addresses of successive items increase.

## Stack Frame
The contents of the stack are stored in stack frames.
A new stack frame is started when the program temporarily jumps to another subroutine/function
and will need to restore its previous state upon returning.

 | Size | Purpose |
 |--- | --- |
 | word | The size of the previous frame |
 | ? | Reserved for the return value of this subroutine/function |
 | ? | Value section