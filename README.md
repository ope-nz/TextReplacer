# TextReplacer
Simple C# text replacement command line utility for use in batch files.

# Usage

Example 1 - this will read input.txt, find all occurences of the string "FIND", replace with "REPLACEMENT" and write out to output.txt

TextReplacer.exe -input .\input.txt -output .\output.txt -find FIND -replace REPLACEMENT

TextReplacer can also use a file for the input eg

Example 2 - this will read input.txt, find all occurences of the string "FIND", replace with the contents of C:\Temp\Replacement.txt and write out to output.txt

TextReplacer.exe -input .\input.txt -output .\output.txt -find FIND -replace C:\Temp\Replacement.txt
