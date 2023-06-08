using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;

namespace TextReplacer
{
    class Program
    {
        static void Main(string[] args)
        {
            string home = Directory.GetCurrentDirectory();			
			
            string ExeFriendlyName = System.AppDomain.CurrentDomain.FriendlyName;
            string[] ExeNameBits = ExeFriendlyName.Split('.');
            string ExeName = ExeNameBits[0];
            
            string TFind = "";
            string TReplace = "";
			string TOutput = "";
			string TInput = "";

            // join into one string
            string argString = string.Join(" ", args);

			Console.WriteLine(argString);

            int c = args.GetUpperBound(0);

            // Loop through arguments
            for (int n = 0; n < c; n++)
            {
                string thisKey = args[n].ToLower();
                string thisVal = args[n + 1].TrimEnd().TrimStart();

                // eval the key or slash-switch option ("/key")
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

            if (TFind == "") return;
            if (TReplace == "") return;
			if (TInput == "") return;
			if (TOutput == "") return;
			
			if (!File.Exists(TInput)) return;			

            string temp_text = File.ReadAllText(TInput);
			
			if (File.Exists(TReplace)) TReplace = File.ReadAllText(TReplace);
			
            temp_text = temp_text.Replace(TFind,TReplace);

			File.WriteAllText(TOutput,temp_text);

            Console.WriteLine("Done!");
        }

        
    }
}