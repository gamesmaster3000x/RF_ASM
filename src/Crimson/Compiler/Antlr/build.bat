@echo off

:setup
set "java_path=java"
set "input_file=Input/Crimson.g4"
set "output_dir=Output"
set "package=Compiler.AntlrBuild"

REM ######## Below this line are the statements to complete the build. Configure the build above. ########
:prepare
set "help_command=%java_path% -jar .\antlr-4.11.1-complete.jar"
set "build_command=%java_path% -jar .\antlr-4.11.1-complete.jar %input_file% -o %output_dir% -package %package% -visitor -listener -long-messages -Dlanguage=CSharp"
:build
echo -
echo Building Crimson ANTLR Grammar
echo ANTLR options:
@echo on
%help_command%
@echo off
echo -
echo Building!
@echo on
%build_command%
@echo off
echo Done!

pause