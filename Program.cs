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

            int c = args.GetUpperBound(0);

            for (int n = 0; n < c; n++)
            {
                string thisKey = args[n].ToLower();
                string thisVal = args[n + 1].TrimEnd().TrimStart();

                switch (thisKey)
                {
                    case "-find":
                        TFind = thisVal;
                        break;
                    case "-replace":
                        TReplace = thisVal;
                        break;
                    case "-output":
                        TOutput = thisVal;
                        break;
                    case "-input":
                        TInput = thisVal;
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

            File.WriteAllText(TOutput, temp_text, new UTF8Encoding(false));

            Console.WriteLine("Done!");
        }
    }
}
