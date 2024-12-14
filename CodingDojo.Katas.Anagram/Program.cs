using System.Diagnostics;

namespace CodingDojo.Katas.Anagram
{
    public class Program
    {
        private static readonly HashSet<string> validWords;
        private static readonly HashSet<string> anagrams = [];

        static Program()
        {
            //validWords = File.ReadAllLines("word_list.txt")
            //                 .SelectMany(w => w.Trim().Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries))
            //                 .ToHashSet();
            validWords = File.ReadAllLines("words_alpha.txt")
                             .Select(w => w.Trim())
                             .ToHashSet();
        }

        static void Main()
        {
            Console.WriteLine($"Input source word:");
            var input = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(input))
            {
                input = "documenting";
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            int count = 0;
            foreach (var anagram in GenerateTwoWordAnagrams(input))
            {
                count++;
                Console.WriteLine(anagram);
            }

            stopwatch.Stop();
            Console.WriteLine($"{count} two word anagrams of {input} found in: {stopwatch.Elapsed}");
        }

        public static IEnumerable<string> GenerateTwoWordAnagrams(string input)
        {
            anagrams.Clear();

            foreach (var permutation in EnumeratePermutations(input))
                foreach (var (firstWord, secondWord) in EnumerateSubstrings(permutation))
                    if (WordsAreValid(firstWord, secondWord) && IsValidAnagram(firstWord, secondWord))
                        yield return $"{firstWord} {secondWord}";

            anagrams.Clear();

            static IEnumerable<string> EnumeratePermutations(string input) => Permute(input.ToCharArray(), 0, input.Length - 1);
            static IEnumerable<(string firstWord, string secondWord)> EnumerateSubstrings(string input)
            {
                for (int i = 2; i < input.Length - 2; i++)
                {
                    yield return (input[..i], input[i..]);
                }
            }
            static bool WordsAreValid(params string[] words) => words.All(validWords.Contains);
            static bool IsValidAnagram(string firstWord, string secondWord) => !anagrams.Contains($"{secondWord} {firstWord}") && anagrams.Add($"{firstWord} {secondWord}");
        }

        private static IEnumerable<string> Permute(char[] chars, int left, int right)
        {
            if (left == right)
            {
                yield return new string(chars);
            }
            else
            {
                for (int i = left; i <= right; i++)
                {
                    SwapCharacters(ref chars[left], ref chars[i]);

                    foreach (var permutation in Permute(chars, left + 1, right))
                        yield return permutation;

                    SwapCharacters(ref chars[left], ref chars[i]);
                }
            }
            static void SwapCharacters(ref char a, ref char b) => (b, a) = (a, b);
        }
    }
}
