using System;
using DiffPlex;
using DiffPlex.DiffBuilder;
using System.IO;

namespace FileDiff
{
    class Program
    {
        const string ArgError = "Invalid number of arguments. Expecting 2 parameters representing files to diff.";
        
        static void Main(string[] args)
        {
            if (!ValidateArguments(args))
                return;

            new Program(new HtmlDataFormatter(), new HtmlUIWriter()).RunFileDiff(args[0], args[1]);
        }

        private readonly HtmlDataFormatter _formatter;
        private readonly IWriter _writer;

        public Program(HtmlDataFormatter htmlDataFormatter, IWriter writter)
        {
            _formatter = htmlDataFormatter;
            _writer = writter;
        }

        private static bool ValidateArguments(string[] args)
        {
            if (args.Length != 2)
            {
                Console.Out.WriteLine(ArgError);
                return false;
            }

            var fileA = args[0];
            var fileB = args[1];

            if (!(File.Exists(fileA) && File.Exists(fileB)))
            {
                Console.Out.WriteLine(ArgError);
                return false;
            }

            return true;
        }

        private void RunFileDiff(string fileA, string fileB)
        {
            var textA = LoadText(fileA);
            var textB = LoadText(fileB);

            var differ = new SideBySideDiffBuilder(new Differ());
            var diffResult = differ.BuildDiffModel(textA, textB);

            var formattedData = _formatter.Format(diffResult);
            _writer.Write(formattedData);
        }

        private string LoadText(string fileName)
        {
            using (var sr = new StreamReader(fileName))
                return sr.ReadToEnd();
        }
    }
}
