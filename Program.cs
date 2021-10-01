using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static System.Console;
namespace SHANON_FANO
{
 
    class Program
    {
        static public IDictionary<char,float> returnWordProb(string word){
            Dictionary<char,float> obj = new() ;
            List<char> arr = word.ToCharArray().ToList();
            WriteLine(arr[0]);
            foreach (var i in arr){
                char letter = i;
                int count = 0;
                arr.ForEach(element=>{
                    if(element == letter) count++;
                });
                obj[i]=count;
            }
            Dictionary<char,float> sorted =new();
            List<char> arrOfObj = obj.Keys.ToList().OrderByDescending(a=>obj[a]).ToList();
            arrOfObj.ForEach(i=>{
                sorted[i]=(obj[i]/word.Length);
            });
            return sorted;
        } 
        static void Main(string[] args)
        {
            var se = JsonSerializer.Serialize(returnWordProb("ABRACADABRA"));
            Console.WriteLine(se);
            
        }
    }
}
