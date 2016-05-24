using System;
using System.Linq;
using System.Collections.Generic;

namespace Johnson
{
    class Program
    {
        static void Main(string[] args)
        {
            string enteredValue = string.Empty;

            int n = 0;
            int ves = 0;

            Console.Write("Enter count of vertexes -> ");
            try
            {
                n = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Entered value has incorrect format");
                Console.WriteLine("Program will be terminated");
            }

            Console.WriteLine("Enter all edges one-by-one");
            Console.WriteLine("Enter any symbol if edge don't exist");

            int?[,] graph = new int?[n, n];

            for(var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    Console.Write($"Enter ves between ({i},{j})-> ");
                    enteredValue = Console.ReadLine();
                    graph[i, j] = int.TryParse(enteredValue, out ves) ? ves : (int?)null;
                }
            }

            var dictGraph = new Graph<int>();

            for (var i = 0; i < 6; i++)
            {
                dictGraph.Add(i, new Dictionary<int, int>());
                for (var j = 0; j < 6; j++)
                {
                    if (graph[i, j].HasValue)
                    {
                        dictGraph[i].Add(j, graph[i, j].Value);
                    }
                }
            }

            var dijkstra = new Johnson<int>(dictGraph);
            var result = dijkstra.Perform(6);

            foreach (var key in result.Keys)
            {
                var stack = new Stack<int>();
                foreach (var key1 in result[key].Keys)
                {
                    stack.Push(key1);
                    var from = key1;
                    while(from != key)
                    {
                        from = result[key][from].Key;
                        stack.Push(from);
                    }
                    stack.Reverse();
                    Console.WriteLine(string.Join("->", stack.Select(v => v.ToString())));
                    stack.Clear();
                }
            }

            Console.ReadKey();
        }
    }
}
