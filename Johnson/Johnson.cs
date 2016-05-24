using System;
using System.Collections.Generic;

namespace Johnson
{
    public sealed class Johnson<T>
    {
        private readonly Dijkstra<T> _dijkstra;
        private readonly BellmanFord<T> _bellmanFord;

        public Johnson(Graph<T> graph)
        {
            _graph = graph;
            if (TotalNodeCount < 3) throw new ArgumentOutOfRangeException(nameof(TotalNodeCount), TotalNodeCount, "Expected a minimum of 3.");
            _dijkstra = new Dijkstra<T>(graph);
            _bellmanFord = new BellmanFord<T>(graph);
        }

        private int TotalNodeCount => _graph.Count;
        private Graph<T> _graph { get; }

        public Dictionary<T, Dictionary<T, KeyValuePair<T, int>>>  Perform(T tempVertex)
        {
            _graph.InsertVertex(tempVertex, 0);
            var belmanFord = _bellmanFord.Perform(tempVertex);
            _graph.RemovetVertex(tempVertex);

            var path = new Dictionary<T, Dictionary<T, KeyValuePair<T, int>>>();

            foreach (var key in _graph.Keys)
            {
                path.Add(key, _dijkstra.Perform(key, (to, dict) =>
                {
                    return dict + belmanFord[to] - belmanFord[key];
                }));
            }

            return path;
        }
    }
}
