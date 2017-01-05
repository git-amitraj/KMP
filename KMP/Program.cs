using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KMP
{
    class Program
    {
        static void Main(string[] args)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();

            /// ArrayList for storing the indexes where the pattern matches
            ArrayList list2 = new ArrayList();
            ArrayList list24 = new ArrayList();
            ArrayList list3 = new ArrayList();
            ArrayList list33 = new ArrayList();
            ArrayList list4 = new ArrayList();
            ArrayList list42 = new ArrayList();

            /// List for storing all six lettered word of the dictionary
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
                if (item.Length == 6)
                    SixLetterWord.Add(item);
            }

            /// From the 6letter word spitting on 2+4, 3+3, 4+2 parts and matching them with dictionary words(haystack)
            foreach (var item in SixLetterWord)
            {
                var val2 = item.Substring(0, 2);
                val2 = "\n" + val2 + "\n";
                list2 = KMPUtil.GetAllOccurences(val2, haystack);
                var val24 = item.Substring(2);
                val24 = "\n" + val24 + "\n";
                list24 = KMPUtil.GetAllOccurences(val24, haystack);
                if ((list2.Count == 1 && list24.Count == 1))
                {
                    Output.Add(item);///Storing 2+4 parts 6letter word
                }

                var val3 = item.Substring(0, 3);
                val3 = "\n" + val3 + "\n";
                list3 = KMPUtil.GetAllOccurences(val3, haystack);
                var val33 = item.Substring(3);
                val33 = "\n" + val33 + "\n";
                list33 = KMPUtil.GetAllOccurences(val33, haystack);
                if ((list3.Count == 1 && list33.Count == 1))
                {
                    Output.Add(item);///Storing 3+3 parts 6letter word
                }

                var val4 = item.Substring(0, 4);
                val4 = "\n" + val4 + "\n";
                list4 = KMPUtil.GetAllOccurences(val4, haystack);
                var val42 = item.Substring(4);
                val42 = "\n" + val42 + "\n";
                list42 = KMPUtil.GetAllOccurences(val42, haystack);
                if ((list4.Count == 1 && list42.Count == 1))
                {
                    Output.Add(item);///Storing 4+2 parts 6letter word
                }
            }
            /// Storing output in a text file - bin/debug folder
            using (StreamWriter writer = new StreamWriter("SixLetterWords.txt", true))
            {
                foreach (String s in Output) writer.WriteLine(s);
            }
            //var elapsedMs = watch.ElapsedMilliseconds;
            //Console.Write(elapsedMs);
            //Console.ReadKey();
        }
    }
}
