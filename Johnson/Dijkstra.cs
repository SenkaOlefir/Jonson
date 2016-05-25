using System;
using System.Collections.Generic;
using System.Linq;

namespace Johnson
{
    /// <summary> 
    /// Implements a generalized Dijkstra's algorithm to calculate 
    /// both minimum distance and minimum path. 
    /// </summary> 
    /// <remarks> 
    /// For this algorithm, all nodes should be provided, and handled 
    /// in the delegate methods, including the start and finish nodes. 
    /// </remarks> 
    public sealed class Dijkstra<T>
    {
        public Dijkstra(Graph<T> graph)
        {
            Graph = graph;
            if (TotalNodeCount < 3) throw new ArgumentOutOfRangeException(nameof(TotalNodeCount), TotalNodeCount, "Expected a minimum of 3.");
        }

        private int TotalNodeCount => Graph.Count;
        private Graph<T> Graph { get; }

	    /// <summary> 
	    /// Performs the Dijkstra algorithm on the data provided when the  
	    /// <see>
	    ///     <cref>Dijkstra</cref>
	    /// </see>
	    ///     object was instantiated. 
	    /// </summary> 
	    /// <param name="start"> 
	    /// The node to use as a starting location. 
	    /// </param>
	    /// <param name="tracertDist"></param>
	    /// <returns> 
	    /// A struct containing both the minimum distance and minimum path 
	    /// to every node from the given <paramref name="start"/> node. 
	    /// </returns> 
	    public Dictionary<T, KeyValuePair<T, int>> Perform(T start, Func<T, int, int> tracertDist = null)
        {
            // Initialize the distance to every node from the starting node. 
            Dictionary<T, KeyValuePair<T, int>> d = GetStartingTraversalCost(start);
            // Initialize best path to every node as from the starting node. 
            ICollection<T> c = Graph.Keys.ToList();

            // begin greedy loop 
            while (c.Count > 1)
            {
                // Find element v in c, that minimizes d[v] 
                var v = FindMinimizingDinC(d, c);
                c.Remove(v); // remove v from the list of future solutions 
                                              // Consider all unselected nodes and consider their cost from v. 
                foreach (var w in c)
                {
                    var dist = tracertDist != null && Graph[v].ContainsKey(w) ? tracertDist(w, Graph[v][w]) : 0;
                    if (Graph[v].ContainsKey(w)
                        && d[w].Value > d[v].Value + Graph[v][w] + dist) // don't let wrap-around negatives slip by 
                    {
                        // We have found a better way to get at relative 
                        d[w] = new KeyValuePair<T, int>(v, d[v].Value + Graph[v][w]);
                    }
                }
            }

            return d;
        }

        private T FindMinimizingDinC(Dictionary<T, KeyValuePair<T, int>> d, ICollection<T> c)
        {
            var currentMin = int.MaxValue;
            var currentIndex = default(T);
            foreach (var i in d.Where(d1 => c.Contains(d1.Key)))
            {
                if (i.Value.Value < currentMin)
                {
                    currentMin = i.Value.Value;
                    currentIndex = i.Key;
                }
            }

            return currentIndex;
        }

        private Dictionary<T, KeyValuePair<T, int>> GetStartingTraversalCost(T start)
        {
            var path = new Dictionary<T, KeyValuePair<T, int>>(TotalNodeCount);

            foreach (var key in Graph.Keys)
            {
                path[key] = new KeyValuePair<T, int>(default(T), int.MaxValue);
            }

            path[start] = new KeyValuePair<T, int>(default(T), 0);
            return path;
        }
    }
}