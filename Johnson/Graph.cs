using System;
using System.Collections.Generic;

namespace Johnson
{
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
                            weight = this[key][key1]
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
    }
}
