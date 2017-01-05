using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace Words
{
    class Program
    {
        static void Main(string[] args)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();

            /// List for storing word of the dictionary
            List<string> TwoLetterWord = new List<string>();
            List<string> ThreeLetterWord = new List<string>();
            List<string> FourLetterWord = new List<string>();
            List<string> SixLetterWord = new List<string>();

            /// List for storing all six lettered word which are made of 2+4letters, 3+3letters and 4+2letters concatenation, this is the final output list
            List<string> Output = new List<string>();

            /// Creating the clean haystack where needle will be searched
            var dictionary = File.ReadAllText("dictionary.txt");
            //  Regex.Replace(dictionary.Trim(), @"[^\S\r\n]+", " ");
            var words = dictionary.Split().Select(x => x.ToLower()).Distinct();
            var haystack = String.Join("\n", words);
            haystack = "\n" + haystack;

            /// Waiting message
            Console.WriteLine("Searching for combinations, final output will be written to bin\\Debug\\SixLetterWords.txt after completion.");

            /// Filtering out 6letter word from the dictionary      
            foreach (var item in words)
            {
                if (item.Length == 2)
                    TwoLetterWord.Add(item);
                if (item.Length == 3)
                    ThreeLetterWord.Add(item);
                if (item.Length == 4)
                    FourLetterWord.Add(item);
                if (item.Length == 6)
                    SixLetterWord.Add(item);
            }

            //For 2+4 Letter Word
            foreach (string word2 in TwoLetterWord)
            {
                foreach (string word4 in FourLetterWord)
                {
                    foreach (string word6 in SixLetterWord)
                    {
                        if ((word2.Length + word4.Length == 6) && (word2 + word4 == word6)) // (word2.Length==2 && (word2== "Ab"))
                        {
                            using (StreamWriter writer = new StreamWriter("SixLetterWords.txt", true))
                            {
                                writer.Write(word2 + " + " + word4 + " => " + word6 + Environment.NewLine);
                            }
                        }
                    }
                }
            }
            //For 3+3 Letter Word
            foreach (string word3 in ThreeLetterWord)
            {
                foreach (string word33 in ThreeLetterWord)
                {
                    foreach (string word6 in SixLetterWord)
                    {
                        if ((word3.Length + word33.Length == 6) && (word3 + word33 == word6)) // (word2.Length==2 && (word2== "Ab"))
                        {
                            using (StreamWriter writer = new StreamWriter("SixLetterWords.txt", true))
                            {
                                writer.Write(word3 + " + " + word33 + " => " + word6 + Environment.NewLine);
                            }
                        }
                    }
                }
            }
            //For 4+2 Letter Word
            foreach (string word44 in FourLetterWord)
            {
                foreach (string word22 in TwoLetterWord)
                {
                    foreach (string word6 in SixLetterWord)
                    {
                        if ((word44.Length + word22.Length == 6) && (word44 + word22 == word6)) // (word2.Length==2 && (word2== "Ab"))
                        {
                            using (StreamWriter writer = new StreamWriter("SixLetterWords.txt", true))
                            {
                                writer.Write(word44 + " + " + word22 + " => " + word6 + Environment.NewLine);
                            }
                        }
                    }
                }
            }
            //var elapsedMs = watch.ElapsedMilliseconds;
            //Console.Write(elapsedMs);
            //Console.ReadKey();
        }
    }
}
