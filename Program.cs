using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using NeedSomeSlice;
namespace SHANON_FANO
{
    class Program
    {
        static public IDictionary<char, int> returnWordProb(string word)
        {
            Dictionary<char, int> obj = new();
            List<char> arr = word.ToCharArray().ToList();
            foreach (var i in arr)
            {
                char letter = i;
                int count = 0;
                arr.ForEach(element =>
                {
                    if (element == letter) count++;
                });
                obj[i] = count;
            }
            Dictionary<char, int> sorted = new();
            List<char> arrOfObj = obj.Keys.ToList().OrderByDescending(a => obj[a]).ToList();
            arrOfObj.ForEach(i =>
            {
                sorted[i] = obj[i];
            });
            return sorted;
        }

        static public IDictionary<char, string> CreateTree(string word)
        {
            IDictionary<char, int> probClass = returnWordProb(word);
            Dictionary<char, string> answer = new();
            probClass.Keys.ToList().ForEach(i =>
            {
                answer[i] = "";
            });
            if (probClass.Keys.Count > 1)
            {
                RecursiveMethod(group: probClass, answer: answer);
            }
            else answer[probClass.Keys.ToList()[0]] = "0";
            return answer;
        }

        static public void RecursiveMethod(
            IDictionary<char, string> answer = null,
            IDictionary<char, int> group = null,
            string accumulation = ""
           )
        {
            foreach (var q in group.Keys)
            {
                answer[q] = accumulation;
            }
            List<char> keys = group.Keys.ToList();
            List<int> val = group.Values.ToList();
            List<int> diff = new();
            if (val.Count > 1)
            {
                for (var i = 0; i < val.Count() - 1; i++)
                {
                    var leftList =  val.ToArray()[0..(i+1)].Sum();
                    var rightList = val.ToArray()[(i+1)..].Sum();
                    diff.Add(Math.Abs(leftList-rightList));
                }

                var minValue = diff.Min();
                var indexOfMin = diff.IndexOf(minValue);
                Dictionary<char,int> leftParameter = new();
                Dictionary<char,int> rightParameter = new();
               for (int i=0; i<indexOfMin+1; i++){
                   leftParameter[keys[val[i]]] = val[i];
                   
                   
               }
               for(int i=indexOfMin+1; i<val.Count(); i++){
                   rightParameter[keys[val[i]]] = val[i];
                   Console.WriteLine(i);
                   Console.WriteLine(val[i]);
               }
                if (leftParameter.Values.Count >= 0)
                {
                    var newLeftAccumulator = accumulation + "0";
                    RecursiveMethod(answer: answer, group: leftParameter, accumulation: newLeftAccumulator);
                }
                if (rightParameter.Values.Count >= 0)
                {
                    var newRightAccumulator = accumulation + "1";
                    RecursiveMethod(answer: answer, group: rightParameter, accumulation: newRightAccumulator);
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
