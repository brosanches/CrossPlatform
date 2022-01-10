using System;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Numerics;
using System.Linq;

namespace lab2
{
    public class Program
    {
        private static int ConvertToInt32(string value)
        {
            bool parsed = Int32.TryParse(value, out int result);

            if (!parsed)
                throw new ArgumentException($"The value {value} was not parsed to int");

            return result;
        }

        [Option(ShortName = "i")]
        public string InputFile { get; }

        [Option(ShortName = "o")]
        public string OutputFile { get; }

        static void Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            string[] lines = File.ReadAllLines(InputFile);

            int h, w, n;

            h = ConvertToInt32(lines[0].Split()[0]);
            w = ConvertToInt32(lines[0].Split()[1]);
            n = ConvertToInt32(lines[0].Split()[2]);

            if (!(1 <= h && h <= 100))
                throw new ArgumentException("Input values do not match criteria 1 <= H <= 100");

            if (!(1 <= w && w <= 100))
                throw new ArgumentException("Input values do not match criteria 1 <= W <= 100");

            if (!(1 <= n && n <= 1_000_000))
                throw new ArgumentException("Input values do not match criteria 1 <= N <= 1_000_000");

            int[,] f = new int[h + 1, w + 1];

            for (int i = 0; i < h + 1; i++)
            {
                for (int j = 0; j < w + 1; j++)
                {
                    f[i, j] = 0;
                }
            }


            for (int z = 1; z <= h; z++)
                for (int x = 1; x <= w; x++)
                {
                    int t = ConvertToInt32(lines[z].Split()[x - 1]);

                    if (!(0 <= t && t <= 10_000))
                        throw new ArgumentException("Input values do not match criteria 0 <= node <= 10_000");

                    f[z, x] = t;
                    f[z, x] += f[z, x - 1] + f[z - 1, x] - f[z - 1, x - 1];
                }

            string res = "";
            int x1, y1, x2, y2;
            for (int z = 0; z < n; z++)
            {
                x1 = ConvertToInt32(lines[h + 1].Split()[0]);
                y1 = ConvertToInt32(lines[h + 1].Split()[1]);
                x2 = ConvertToInt32(lines[h + 1].Split()[2]);
                y2 = ConvertToInt32(lines[h + 1].Split()[3]);

                if (!(1 <= x1 && x1 <= x2 && x2 <= h))
                    throw new ArgumentException("Input values do not match criteria 1 <= x1 <= x2 <= H");

                if (!(1 <= y1 && y1 <= y2 && y2 <= w))
                    throw new ArgumentException("Input values do not match criteria 1 <= y1 <= y2 <= W");

                res += f[x2, y2] - f[x2, y1 - 1] - f[x1 - 1, y2] + f[x1 - 1, y1 - 1] + "\n";
            }

            Console.WriteLine(res);

            File.WriteAllText(OutputFile, res);
        }
    }
}
