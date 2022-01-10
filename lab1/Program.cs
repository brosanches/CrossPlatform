using System;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Numerics;
using System.Linq;

namespace lab1
{
    public class Program
    {
        [Option(ShortName = "i")]
        public string InputFile { get; }

        [Option(ShortName = "o")]
        public string OutputFile { get; }

        static void Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            string[] lines = File.ReadAllLines(InputFile);

            int N, K;

            Library.ParseStrings(lines, out N, out K);

            string min = Library.GetMin(N, K);

            string max = Library.GetMax(N, K);

            string res = "";

            if (String.Equals(min, "NO SOLUTION") && String.Equals(max, "NO SOLUTION"))
            {
                res = "NO SOLUTION";
            }
            else
            {
                if (!String.Equals(min, "NO SOLUTION"))
                {
                    res += min;
                }
                if (!String.Equals(max, "NO SOLUTION"))
                {
                    res += "\n" + max;
                }
            }

            Console.WriteLine(res);

            File.WriteAllText(OutputFile, res);
        }
    }
}
