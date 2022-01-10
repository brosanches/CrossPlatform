using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public static class Library
    {
        private static StringBuilder a;
        private static StringBuilder b;

        private static int[] black = { 6, 2, 5, 5, 4, 5, 6, 3, 7, 6 };

        private static bool CheckN(int x) => !(1 <= x && x <= 100);
        private static bool CheckK(int x) => !(1 <= x && x <= 700);
        private static int ConvertToInt32(string value)
        {
            bool parsed = Int32.TryParse(value, out int result);

            if (!parsed)
                throw new ArgumentException($"The value {value} was not parsed to int");

            return result;
        }
        public static void ParseStrings(string[] lines, out int N, out int K)
        {
            N = ConvertToInt32(lines[0].Split()[0]);
            K = ConvertToInt32(lines[0].Split()[1]);

            if (CheckN(N))
                throw new ArgumentException("Input values do not match criteria 1 <= N <= 100");

            if (CheckK(K))
                throw new ArgumentException($"Input values do not match criteria 1 <= K <= 700");
        }

        public static string GetMin (int N, int K, string prefix = "")
        {
            if (prefix.Length == N)
                a = new StringBuilder(prefix);

            else if (prefix.Length == 0) 
            {
                if (K < 2 * N || K > 7 * N)
                    a = new StringBuilder("NO SOLUTION");
                else 
                {
                    int j = 1;
                    bool Stop = false;
                    while (!Stop && j < 10)
                    {
                        if (2 * (N - (prefix.Length + 1)) <= K - black[j] && K - black[j] <= 7 * (N - (prefix.Length + 1)))
                        {
                            GetMin(N, K - black[j], prefix + j);
                            Stop = true; 
                        }
                        j += 1;
                    }
                }
            }
            else
            {
                int j = 0;
                bool Stop = false;

                while (!Stop && j < 10)
                {
                    if (2 * (N - (prefix.Length + 1)) <= K - black[j] && K - black[j] <= 7 * (N - (prefix.Length + 1)))
                    {
                        GetMin(N, K - black[j], prefix + j);
                        Stop = true;
                    }
                    j += 1;
                }
            }

            return a.ToString();
        }

        public static string GetMax(int N, int K, string prefix = "")
        {
            if (prefix.Length == N)
                return prefix;

            else if (prefix.Length == 0)
            {
                if (K < 2 * N || K > 7 * N)
                    return "NO SOLUTION";
                else
                {
                    int j = 9;
                    while (j > 0)
                    {
                        if (2 * (N - (prefix.Length + 1)) <= K - black[j] && K - black[j] <= 7 * (N - (prefix.Length + 1)))
                        {
                            return GetMax(N, K - black[j], prefix + j);
                        }
                        j -= 1;
                    }
                }
            }
            else
            {
                int j = 9;

                while (j > -1)
                {
                    if (2 * (N - (prefix.Length + 1)) <= K - black[j] && K - black[j] <= 7 * (N - (prefix.Length + 1)))
                    {
                        return GetMax(N, K - black[j], prefix + j);
                    }
                    j -= 1;
                }
            }

            return prefix;
        }
    }
}
