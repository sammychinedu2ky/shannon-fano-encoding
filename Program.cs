using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
namespace SHANON_FANO
{
    class Program
    {
        static  IDictionary<char, int> ReturnWordCount(string word)
        {
            Dictionary<char, int> obj = new();
            word.ToList().ForEach(i =>
           {
               var found = obj.TryGetValue(i, out int value) ? obj[i]++ : obj[i] = 1;
           });
            Dictionary<char, int> sorted = obj.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
            return sorted;
        }
        static Dictionary<char, string> answer = new();
        static  IDictionary<char, string> CreateTree(string word)
        {
            IDictionary<char, int> probClass = ReturnWordCount(word);
            probClass.Keys.ToList().ForEach(i => { answer[i] = ""; });
            if (probClass.Keys.Count > 1)
            {
                RecursiveMethod(group: probClass);
            }
            else answer[probClass.Keys.ToList()[0]] = "0";
            return answer;
        }

        static  void RecursiveMethod(
            IDictionary<char, int> group = null,
            string accumulation = ""
           )
        {
            group.Keys.ToList().ForEach(i => answer[i] = accumulation);
            List<char> keys = group.Keys.ToList();
            List<int> val = group.Values.ToList();
            List<int> diff = new();
            if (val.Count > 1)
            {
                diff = val.Select((_, index) => Math.Abs(val.ToArray()[0..(index + 1)].Sum() - val.ToArray()[(index + 1)..].Sum())).ToList().GetRange(0, val.Count - 1);
                var indexOfMin = diff.IndexOf(diff.Min());
                Dictionary<char, int> leftParameter = new();
                Dictionary<char, int> rightParameter = new();
                for (int i = 0; i < indexOfMin + 1; i++)
                {
                    leftParameter[keys[i]] = val[i];
                }
                for (int i = indexOfMin + 1; i < val.Count(); i++)
                {
                    rightParameter[keys[i]] = val[i];
                }
                if (leftParameter.Values.Count >= 0)
                {
                    RecursiveMethod(group: leftParameter, accumulation: accumulation + "0");
                }
                if (rightParameter.Values.Count >= 0)
                {
                    RecursiveMethod(group: rightParameter, accumulation: accumulation + "1");
                }
            }

        }

        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Empty string argument");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"example of a command: \nshannon hello");
                Console.ResetColor();
            }
            else
            {
                var answer = JsonSerializer.Serialize(CreateTree(args[0]));
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(answer);
                Console.ResetColor();
            }
        }
    }
}
