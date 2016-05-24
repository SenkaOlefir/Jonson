using System;
using System.Linq;
using System.Collections.Generic;

namespace Johnson
{
    class Program
    {
        static void Main(string[] args)
        {
            //var graph = new Graph<int>();
            //graph.Add(1, new Dictionary<int, int>()
            //{
            //    { 2, 10 },
            //    { 3, 18 },
            //    { 4, 8 }
            //});

            //graph.Add(2, new Dictionary<int, int>()
            //{
            //    { 1, 10 },
            //    { 3, 16 },
            //    { 4, 9 },
            //    { 5, 21 }
            //});

            //graph.Add(3, new Dictionary<int, int>()
            //{
            //    { 2, 16 },
            //    { 6, 15 }
            //});

            //graph.Add(4, new Dictionary<int, int>()
            //{
            //    { 1, 7 },
            //    { 2, 9 },
            //    { 6, 12 }
            //});

            //graph.Add(5, new Dictionary<int, int>()
            //{
            //    { 6, 23 }
            //});

            //graph.Add(6, new Dictionary<int, int>()
            //{
            //    { 3, 15 },
            //    { 5, 23 }
            //});

            int?[,] graph = new int?[,]
            {
                { null, 10, 18, 8, null, null },
                { 10, null, 16, 9, 21, null },
                { null, 16, null, null, null, -15 },
                { 7, 9, null, null, null, 12 },
                { null, null, null, null, null, 23 },
                { null, null, 15, null, 23, null }
            };

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
