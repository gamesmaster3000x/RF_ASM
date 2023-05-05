# The Stack in Crimson
Crimson's stack counts up in memory, ie. the memory addresses of successive items increase.

## Stack Frame
The contents of the stack are stored in stack frames.
A new stack frame is started when the program enters a new scope, such as when it temporarily jumps to another subroutine/function
and will need to restore its previous state upon returning.

 | Size | Purpose |
 |--- | --- |
 | word | The size of the previous frame |
 | ? | Scope inputs |
 | ? | Scope outputs |
 | ? | Stack variables |