using System;
using System.Collections.Generic;

namespace Johnson
{
    public class NegativeCyclesException : Exception
    {
        public NegativeCyclesException() : base() { }
        public NegativeCyclesException(string message) : base(message) { }
    }

    public class PathNotExistException : Exception
    {
        public PathNotExistException() : base() { }
        public PathNotExistException(string message) : base(message) { }
    }

    public class Graph<TKey> : Dictionary<TKey, Dictionary<TKey, int>>
    {
        public struct Edge
        {
            public TKey src { get; set; }
            public TKey dest { get; set; }
            public int weight { get; set; }
        }

        public IEnumerable<Edge> Edges
        {
            get
            {
                foreach(var key in Keys)
                {
                    foreach(var key1 in this[key].Keys)
                    {
                        yield return new Edge
                        {
                            src = key,
                            dest = key1,
                            weight = this[key][key1] + 2
                        };
                    }
                }
            }
        }

        public void InsertVertex(TKey key, int defaultVes)
        {
            var edges = new Dictionary<TKey, int>();

            foreach (var vertex in Keys)
            {
                edges.Add(vertex, defaultVes);
            }

            Add(key, edges);
        }

        public void RemovetVertex(TKey key)
        {
            try
            {
                Remove(key);
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"Vertex {key} does not exist on graph");
            }
        }

        public static Graph<int> FormMatrix(int?[,] graph)
        {
            var dictGraph = new Graph<int>();

            for (var i = 0; i < graph.GetLength(0); i++)
            {
                dictGraph.Add(i, new Dictionary<int, int>());
                for (var j = 0; j < graph.GetLength(0); j++)
                {
                    if (graph[i, j].HasValue)
                    {
                        dictGraph[i].Add(j, graph[i, j].Value);
                    }
                }
            }

            return dictGraph;
        }

        public static Graph<int> FormMatrix(int?[][] graph)
        {
            var dictGraph = new Graph<int>();

            for (var i = 0; i < graph.Length; i++)
            {
                dictGraph.Add(i, new Dictionary<int, int>());
                for (var j = 0; j < graph.Length; j++)
                {
                    if (graph[i][j].HasValue)
                    {
                        dictGraph[i].Add(j, graph[i][j].Value);
                    }
                }
            }

            return dictGraph;
        }
    }
}
