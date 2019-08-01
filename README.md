# C#-dll-Format-Parser

A project used as a parser to C# dll file.

I execute the "Hello World!" program in C# first. The program is named as CsharpHelloworld. After comparison, I location special data segment in the example file.

Meanwhile, I use the dumpbin tool along with the Vistual Studio 2019. This can be used as a parser to analyze the dll file format, which indicates what my mentor wants me to do is make a tool just like the dumpbin and have a deeper understanding of it:)

The file format is as follows:

1. MZ file header

2. Default part: This program cannot be run in DOS mode.

3. PE file header

4. Sections' headers

5. Sections' content in order

PS: The all-zero reserved size = size of raw data - size of section

# Up to seven sections: .data .idata .rdata .reloc .rsrc .text .textbss

The .text section is where all general-purpose code emitted by the compiler or assembler ends up. 

The .data section is where your initialized data goes. This data consists of global and static variables that are initialized at compile time. It also includes string literals. The linker combines all the .data sections from the OBJ and LIB files into one .data section in the EXE.

The .bss section is where any uninitialized static and global variables are stored.

The .idata section contains information about functions (and data) that the module imports from other DLLs. The .edata section is a list of the functions and data that the PE file exports for other modules. The .rdata section represents read-only data, such as literal strings, constants, and debug directory information.

The .rsrc section contains all the resources for the module. The .reloc section holds a table of base relocations.

The .CRT is another initialized data section utilized by the Microsoft C/C++ run-time libraries (hence the name). Don't know why it can't be into .data section.


# Reference:

https://en.wikipedia.org/wiki/DOS_MZ_executable

http://www.delorie.com/djgpp/doc/exe/

https://marcin-chwedczuk.github.io/a-closer-look-at-portable-executable-msdos-stub MZ file parse example

https://en.wikipedia.org/wiki/List_of_file_signatures  List of File signature

https://en.wikipedia.org/wiki/Portable_Executable  PE file format

https://docs.microsoft.com/en-us/windows/win32/debug/pe-format#file-headers  PE

https://stackoverflow.com/questions/27214966/exe-header-information-reloc-rsrc-meaning

https://docs.microsoft.com/en-us/previous-versions/ms809762(v=msdn.10) section information
