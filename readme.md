***NOTE: Sub-projects are documented in their own folders.***

---
# Why am I archiving this project?
I've really enjoyed working on Crimson, RFASM, and Berry over the last 9 months-or-so, so I'd like to give it a proper send-off before I archive it.

---
## The short answer
1) Crimson has no features which another language does not do better. 
2) Its only benefit is that it's the only language which can target RedFoxAssembly, but that itself can only be run on the RedFoxVirtualMachine, which is no longer supported and serves no useful purpose any more.

---
## The long answer
I was originally tasked with creating an assembler to target RedFoxAssembly (RFASM) for the RedFoxVirtualMachine (RFVM). I was given the instruction set and details for how the RFVM would read machine code, but mostly free reign over the language's syntax and complete control over the assembly process. I achieved this goal fairly quickly, bodging a solution together with regular expressions and string manipulation. It wasn't an ideal solution, but it worked well enough.

For several years, I've wanted to create a programming language, but never had a reason to do so. This project gave me that reason. It isn't noted anywhere here, but one of the purposes of the RFVM is to be a mockup and testing ground for a physical DIY breadboard computer. Because of this, Crimson would be running in an extremely memory-limited environment, so the utmost control over memory would be required. I initially considered writing a port of a C-compiler for RFASM, but decided it would be more fun to do it myself (I suspect I was right!).

I set about learning to use ANTLR to parse Crimson source code for me (because I wasn't about to write a custom parser for that!), and ended up retrofitting/rewriting the RFASM assembler using ANTLR as well. I explored the idea of an intermediary language, CrimsonBasic, which was a stripped back version of Crimson without 'if' statements, which could be nearly directly compiled into machine code. This concept was scrapped, but it aided my thought process a lot.

I eventually got tired of working on the compiler, so decided to make a package manager for Crimson. Enter Berry.

I've never really explored web-development except some limited experimentation with HTML, so this was an exciting new challenge. I started using ASP.NET (although I was recommended to use Node.js) and adopted an MVC architecture after some squabbles with Razor pages. I introduced a SQLite database, introduced quantum-safe author-verification with CRYSTALS-Dilithium and reached the point of needing to decide what would be in a 'Berry' (a package of Crimson code).

I wanted...
1) ... Berry to be peer-to-peer so I wouldn't have to run servers myself
2) ... Berries to be platform independent (i.e. x86 vs x64 agnostic) so authors wouldn't need to upload several versions
3) ... Berries to come partially compiled / ready to link in order to reduce compilation times for large projects

But this is where it all fell down...

Crimson can't solely target RFASM because by this point the RFVM was already out of support, but if I make Crimson platform independent, I have to compete with the likes of C, C++, Go and Rust... Crimson's main features are its memory micro-management, but firmware has survived perfectly well on C for decades and Crimson's `masks` are just a confusing and unnecessary complication of C's `structs`. Rust is one of (if not *the*) most loved programming language at the time of writing, which is partially due to its excellent memory management system - a system which produces highly efficient code whilst maintaining useability and preventing casting/null/memory errors before even running the program. Go was designed by the folks at Google (*I'm competing with GOOGLE now???*) to compile to many platforms, and it has a well-established ecosystem of packages ready to use, unlike Crimson.

So you see, everything Crimson does, they can do better.

On its own, that isn't a reason to not try though. After all, every idea has to grow and change over time, and we can't make something better if we don't try to. But I wasn't trying to compete initially. I wanted to make a programming language to help a friend achieve their own goals. The RFASM assembler achieved that (I think...), but Crimson ran out of time to find its own place in the world.

 -- 12/05/2023