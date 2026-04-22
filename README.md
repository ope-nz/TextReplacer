# TextReplacer

A simple command-line tool for find-and-replace operations on text files.

## Building

Run `_compile.bat` to compile with the .NET Framework 4.0 C# compiler:

```
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /target:winexe /out:TextReplacer.exe Program.cs
```

## Usage

```
TextReplacer.exe -input <file> -output <file|O> -find <text> -replace <text>
```

### Arguments

| Argument   | Description |
|------------|-------------|
| `-input`   | Path to the source file to read |
| `-output`  | Path to write the result. Use `O` to overwrite the input file in place |
| `-find`    | Text to search for |
| `-replace` | Replacement text, a placeholder (see below), or a path to a file whose contents are used as the replacement |

### Placeholders

These can be used in both `-find` and `-replace` values. Placeholders can be embedded anywhere in the value, e.g. `line1[LF]line2`.

| Placeholder       | Replaced with |
|-------------------|---------------|
| `[CR]`            | Carriage return (`\r`, 0x0D) |
| `[LF]`            | Line feed (`\n`, 0x0A) |
| `[CRLF]`          | Carriage return + line feed (`\r\n`) |
| `[P]`             | Percent sign (`%`) |
| `[Q]`             | Double quote (`"`) |
| `[S]`             | Space (` `) |
| `[]`              | Empty string (deleted) |
| `[DateTime]`      | Current date/time, e.g. `Friday, Apr 04, 2026 3:45:00 PM` |
| `[DateTimeShort]` | Current date/time in ISO format, e.g. `2026-04-04 15:45:00` |

`[P]`, `[Q]`, and `[S]` are useful when passing values via batch scripts or shells where `%`, `"`, and spaces are hard to escape directly. `[DateTime]` and `[DateTimeShort]` apply to `-replace` only.

## Examples

Convert Unix line endings to Windows:
```
TextReplacer.exe -input file.txt -output O -find [LF] -replace [CRLF]
```

Strip carriage returns (Windows to Unix):
```
TextReplacer.exe -input file.txt -output O -find [CRLF] -replace [LF]
```

Stamp the current date into a file:
```
TextReplacer.exe -input template.txt -output output.txt -find $DATE$ -replace [DateTime]
```

Replace a token with the contents of another file:
```
TextReplacer.exe -input template.txt -output output.txt -find $BODY$ -replace body.txt
```
