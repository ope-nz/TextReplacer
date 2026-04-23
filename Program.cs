using System;
using System.IO;
using System.Text;

namespace TextReplacer
{
    class Program
    {
        static void Main(string[] args)
        {
            string TFind = "";
            string TReplace = null; // null = not supplied, "" = explicitly empty (delete matches)
            string TOutput = "";
            string TInput = "";
            bool TTrim = false;

            for (int n = 0; n < args.Length; n++)
            {
                string thisKey = args[n].ToLower();
                switch (thisKey)
                {
                    case "-trim":
                        TTrim = true;
                        break;
                    case "-find":
                    case "-replace":
                    case "-output":
                    case "-input":
                        if (n + 1 >= args.Length) break;
                        string thisVal = args[++n].Trim();
                        switch (thisKey)
                        {
                            case "-find":    TFind    = thisVal; break;
                            case "-replace": TReplace = thisVal; break;
                            case "-output":  TOutput  = thisVal; break;
                            case "-input":   TInput   = thisVal; break;
                        }
                        break;
                }
            }

            if (TOutput == "O") TOutput = TInput;

            if (TFind == "")        { Console.WriteLine("Error: -find is required.");                   return; }
            if (TReplace == null)   { Console.WriteLine("Error: -replace is required.");                return; }
            if (TInput == "")       { Console.WriteLine("Error: -input is required.");                  return; }
            if (TOutput == "")      { Console.WriteLine("Error: -output is required.");                 return; }
            if (!File.Exists(TInput)) { Console.WriteLine("Error: input file not found: " + TInput);   return; }

            string temp_text = File.ReadAllText(TInput, Encoding.UTF8);

            // If -replace is a file path, use its contents as-is (no placeholder substitution)
            if (File.Exists(TReplace))
            {
                TReplace = File.ReadAllText(TReplace, Encoding.UTF8);
            }
            else
            {
                // Apply placeholders embedded anywhere in the value, e.g. "hello[LF]world"
                TReplace = TReplace
                    .Replace("[DateTime]",      DateTime.Now.ToString("dddd, MMM dd, yyyy h:mm:ss tt"))
                    .Replace("[DateTimeShort]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    .Replace("[CRLF]", "\r\n")
                    .Replace("[CR]",   "\r")
                    .Replace("[LF]",   "\n")
                    .Replace("[P]",   "%")
                    .Replace("[Q]",   "\"")
                    .Replace("[S]",   " ")
                    .Replace("[]",   "");
            }

            TFind = TFind
                .Replace("[CRLF]", "\r\n")
                .Replace("[CR]",   "\r")
                .Replace("[LF]",   "\n")
                .Replace("[P]",   "%")                                
                .Replace("[Q]",   "\"")
                .Replace("[S]",   " ")
                .Replace("[]",   "");

            //Console.WriteLine("Input:  " + TInput);
            //Console.WriteLine("Output: " + TOutput);
            //Console.WriteLine("Find:   " + TFind);
            //Console.WriteLine("Replace:" + TReplace);

            temp_text = temp_text.Replace(TFind, TReplace);

            if (TTrim)
            {
                string[] lines = temp_text.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                    lines[i] = lines[i].Trim();
                temp_text = string.Join("\n", lines);
            }

            File.WriteAllText(TOutput, temp_text, new UTF8Encoding(false));

            string findPreview    = TFind.Length    > 10 ? TFind.Substring(0, 10)    + "..." : TFind;
            string replacePreview = TReplace.Length > 10 ? TReplace.Substring(0, 10) + "..." : TReplace;
            Console.WriteLine("Replaced " + findPreview + " with " + replacePreview);
        }
    }
}
