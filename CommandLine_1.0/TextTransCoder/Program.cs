using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommandLine.Common;

namespace CommandLine.TextTransCoder
{
    public class ProgramArguments
    {
        [Argument(ArgumentType.AtMostOnce, DefaultValue = "utf-8", 
            ShortName="ie", LongName="input-encoding", HelpText = "Source encoding")]
        public string inputEncodingName;
        [Argument(ArgumentType.AtMostOnce, DefaultValue = "utf-8", 
            ShortName = "oe", LongName = "output-encoding", HelpText = "Target encoding")]
        public string outputEncodingName;
        [Argument(ArgumentType.AtMostOnce, DefaultValue = "con", 
            ShortName = "if", LongName = "input-file", HelpText = "Source text file")]
        public string inputFileName;
        [Argument(ArgumentType.AtMostOnce, DefaultValue = "con",
            ShortName = "of", LongName = "output-file", HelpText = "Target text file")]
        public string outputFileName;
        [Argument(ArgumentType.AtMostOnce, DefaultValue = 16384,
            ShortName = "bsz", LongName = "buffer-size", HelpText = "In-memory buffer size in bytes")]
        public int bufsize;
    }

    class Program
    {
        static void Main(string[] args)
        {
            ProgramArguments parsedArgs = new ProgramArguments();
            if (Parser.ParseArgumentsWithUsage(args, parsedArgs))
            {
#if DEBUG
                Console.WriteLine(parsedArgs.inputEncodingName);
                Console.WriteLine(parsedArgs.inputFileName);
                Console.WriteLine(parsedArgs.outputEncodingName);
                Console.WriteLine(parsedArgs.outputFileName);
                Console.WriteLine(parsedArgs.bufsize);
#endif
                int buflen = parsedArgs.bufsize;
                Encoding ie = Encoding.GetEncoding(parsedArgs.inputEncodingName);
                Encoding oe = Encoding.GetEncoding(parsedArgs.outputEncodingName);
                
                TextReader istream;
                if (parsedArgs.inputFileName != "con")
                    istream = new StreamReader(parsedArgs.inputFileName, ie);
                else
                {
                    if (ie != Encoding.Unicode)
                    {
                        Console.InputEncoding = ie;
                    }
                    istream = Console.In;
                }
                TextWriter ostream;
                if (parsedArgs.outputFileName != "con")
                    ostream = new StreamWriter(parsedArgs.outputFileName, false, oe, buflen);
                else
                {
                    if (oe != Encoding.Unicode)
                    {
                        Console.OutputEncoding = oe;
                    }
                    ostream = Console.Out;
                }

                int charlen = 0;
                char[] charBuffer = new char[buflen];
                charlen = istream.Read(charBuffer, 0, buflen);
                if (charlen > 0)
                {
                    do
                    {
                        ostream.Write(charBuffer, 0, charlen);
                        charlen = istream.Read(charBuffer, 0, buflen);
                    }
                    while (charlen > 0);
                }
                istream.Close();
                ostream.Close();
            }
#if DEBUG
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
#endif
        }
    }
}
