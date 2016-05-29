using System;
using System.Collections.Generic;

namespace Johnson
{
    public sealed class BellmanFord<T>
    {
        public BellmanFord(Graph<T> graph)
        {
            Graph = graph;
            if (TotalNodeCount < 3) throw new ArgumentOutOfRangeException(nameof(TotalNodeCount), TotalNodeCount, "Expected a minimum of 3.");
        }

        private int TotalNodeCount => Graph.Count;
        private Graph<T> Graph { get; }

        // The main function that finds shortest distances from src
        // to all other vertices using Bellman-Ford algorithm.  The
        // function also detects negative weight cycle
        public Dictionary<T, int> Perform(T src)
        {
            var dist = GetStartingTraversalCost(src);

            // Step 2: Relax all edges |V| - 1 times. A simple
            // shortest path from src to any other vertex can
            // have at-most |V| - 1 edges
            for (int i = 1; i < TotalNodeCount; ++i)
            {
                foreach(var edge in Graph.Edges)
                {
                    T from = edge.src;
                    T to = edge.dest;
                    int weight = edge.weight;
                    if (dist[from] != int.MaxValue &&
                        dist[from] + weight < dist[to])
                        dist[to] = dist[from] + weight;
                }
            }

            // Step 3: check for negative-weight cycles.  The above
            // step guarantees shortest distances if graph doesn't
            // contain negative weight cycle. If we get a shorter
            //  path, then there is a cycle.
            foreach (var edge in Graph.Edges)
            {
                T from = edge.src;
                T to = edge.dest;
                int weight = edge.weight;
                if (dist[from] != int.MaxValue &&
                    dist[from] + weight < dist[to])
                    throw new NegativeCyclesException("Has negative cycles");
            }

            return dist;
        }

        private Dictionary<T, int> GetStartingTraversalCost(T start)
        {
            var path = new Dictionary<T, int>(TotalNodeCount);

            foreach (var key in Graph.Keys)
            {
                path[key] = int.MaxValue;
            }

            path[start] = 0;

            return path;
        }
    }
}
