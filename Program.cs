using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Coding;
namespace SHANON_FANO
{
    class Program
    {
        static public IDictionary<char, float> returnWordProb(string word)
        {
            Dictionary<char, float> obj = new();
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
            Dictionary<char, float> sorted = new();
            List<char> arrOfObj = obj.Keys.ToList().OrderByDescending(a => obj[a]).ToList();
            arrOfObj.ForEach(i =>
            {
                sorted[i] = (obj[i] / word.Length);
            });
            return sorted;
        }

        static public IDictionary<char, string> CreateTree(string word)
        {
            IDictionary<char, float> probClass = returnWordProb(word);
            //probClass[]
            Dictionary<char, string> answer = new();
            probClass.Keys.ToList().ForEach(i =>
            {
                answer[i] = "";
            });
            if(probClass.Keys.Count>1){
                 RecursiveMethod(probClass, answer: answer);
            }
            else answer[probClass.Keys.ToList()[0]]="0";
           
            return answer;
        }

        static public void RecursiveMethod(
            IDictionary<char, float> input = null,
            IDictionary<char, string> answer = null,
            IDictionary<char, float> leftParam = null,
            IDictionary<char, float> rightParam = null,
            string accumulation = ""
           )
        {
            if (input is not null)
            {
                List<char> keys = input.Keys.ToList();
                List<float> val = input.Values.ToList();
                List<float> diff = new();
                for (var i = 0; i < val.Count() - 1; i++)
                {
                    var leftList = val.Slice(0, i + 1);
                    var rightList = val.Slice(i + 1, val.Count);

                    var leftSum = leftList.Aggregate((a, b) => a + b);
                    var rightSum = rightList.Aggregate((a, b) => a + b);
                    var AbsoluteDifference = Math.Abs(leftSum - rightSum);
                    diff.Add(AbsoluteDifference);
                }
                var minValue = diff.Min();

                var indexOfMin = diff.IndexOf(minValue);

                Dictionary<char, float> leftParameter = new();
                Dictionary<char, float> rightParameter = new();
                var leftKeys = new List<char>();
                leftKeys.AddRange(keys.Slice(0, indexOfMin + 1));
                var leftValues = new List<float>();
                leftValues.AddRange(val.Slice(0, indexOfMin + 1));
                for (var i = 0; i < leftKeys.Count; i++)
                {
                    leftParameter.Add(leftKeys[i], val[i]);
                }
                var rightKeys = new List<char>();
                rightKeys.AddRange(keys.Slice(indexOfMin + 1, keys.Count));
                var rightValues = new List<float>();
                rightValues.AddRange(val.Slice(indexOfMin + 1, keys.Count));
                for (var i = 0; i < rightKeys.Count; i++)
                {
                    rightParameter.Add(rightKeys[i], val[i]);
                }

                var newLeftAccumulator = accumulation + "0";
                RecursiveMethod(answer: answer, leftParam: leftParameter, accumulation: newLeftAccumulator);

                //right
                var newRightAccumulator = accumulation + "1";
                RecursiveMethod(answer: answer, rightParam: rightParameter, accumulation: newRightAccumulator);
            }
            else
            {
                if (rightParam is not null)
                {
                    var myKeys = rightParam.Keys;
                    foreach (var q in myKeys)
                    {
                        answer[q] = accumulation;
                    }
                    List<char> keys = rightParam.Keys.ToList();
                    List<float> val = rightParam.Values.ToList();
                    List<float> diff = new();
                    if (val.Count > 1)
                    {
                        for (var i = 0; i < val.Count() - 1; i++)
                        {
                            var leftList = val.Slice(0, i + 1);
                            var rightList = val.Slice(i + 1, val.Count);

                            var leftSum = leftList.Aggregate((a, b) => a + b);
                            var rightSum = rightList.Aggregate((a, b) => a + b);
                            var AbsoluteDifference = Math.Abs(leftSum - rightSum);
                            diff.Add(AbsoluteDifference);
                        }
                        var minValue = diff.Min();
                        var indexOfMin = diff.IndexOf(minValue);
                        Dictionary<char, float> leftParameter = new();
                        Dictionary<char, float> rightParameter = new();
                        var leftKeys = new List<char>();
                        leftKeys.AddRange(keys.Slice(0, indexOfMin + 1));
                        var leftValues = new List<float>();
                        leftValues.AddRange(val.Slice(0, indexOfMin + 1));
                        for (var i = 0; i < leftKeys.Count; i++)
                        {
                            leftParameter.Add(leftKeys[i], leftValues[i]);
                        }
                        var rightKeys = new List<char>();
                        rightKeys.AddRange(keys.Slice(indexOfMin + 1, keys.Count));
                        var rightValues = new List<float>();
                        rightValues.AddRange(val.Slice(indexOfMin + 1, keys.Count));
                        for (var i = 0; i < rightKeys.Count; i++)
                        {
                            rightParameter.Add(rightKeys[i], rightValues[i]);
                        }
                        if (leftKeys.Count > 0)
                        {
                            var newLeftAccumulator = accumulation + "0";
                            RecursiveMethod(answer: answer, leftParam: leftParameter, accumulation: newLeftAccumulator);
                        }

                        if (rightKeys.Count > 0)
                        {
                            //right
                            var newRightAccumulator = accumulation + "1";
                            RecursiveMethod(answer: answer, rightParam: rightParameter, accumulation: newRightAccumulator);
                        }
                    }


                }

                if (leftParam is not null)
                {
                    var myKeys = leftParam.Keys;
                    foreach (var q in myKeys)
                    {
                        answer[q] = accumulation;
                    }
                    List<char> keys = leftParam.Keys.ToList();
                    List<float> val = leftParam.Values.ToList();
                    List<float> diff = new();
                    if (val.Count > 1)
                    {
                        for (var i = 0; i < val.Count() - 1; i++)
                        {
                            var leftList = val.Slice(0, i + 1);
                            var rightList = val.Slice(i + 1, val.Count);
                            var leftSum = leftList.Aggregate((a, b) => a + b);
                            var rightSum = rightList.Aggregate((a, b) => a + b);
                            var AbsoluteDifference = Math.Abs(leftSum - rightSum);
                            diff.Add(AbsoluteDifference);
                        }
                        var minValue = diff.Min();
                        var indexOfMin = diff.IndexOf(minValue);
                        Dictionary<char, float> leftParameter = new();
                        Dictionary<char, float> rightParameter = new();
                        var leftKeys = new List<char>();
                        leftKeys.AddRange(keys.Slice(0, indexOfMin + 1));
                        var leftValues = new List<float>();
                        leftValues.AddRange(val.Slice(0, indexOfMin + 1));
                        for (var i = 0; i < leftKeys.Count; i++)
                        {
                            leftParameter.Add(leftKeys[i], leftValues[i]);
                        }
                        var rightKeys = new List<char>();
                        rightKeys.AddRange(keys.Slice(indexOfMin + 1, keys.Count));
                        var rightValues = new List<float>();
                        rightValues.AddRange(val.Slice(indexOfMin + 1, keys.Count));
                        for (var i = 0; i < rightKeys.Count; i++)
                        {
                            rightParameter.Add(rightKeys[i], rightValues[i]);
                        }
                        if (leftKeys.Count > 0)
                        {
                            var newLeftAccumulator = accumulation + "0";
                            RecursiveMethod(answer: answer, leftParam: leftParameter, accumulation: newLeftAccumulator);
                        }
                        if (rightKeys.Count > 0)
                        {
                            var newRightAccumulator = accumulation + "1";
                            RecursiveMethod(answer: answer, rightParam: rightParameter, accumulation: newRightAccumulator);
                        }
                    }
                }

            }


        }

        static void Main(string[] args)
        {
            var answer = JsonSerializer.Serialize(CreateTree("samson"));
            Console.WriteLine(answer);

        }

    }
}
