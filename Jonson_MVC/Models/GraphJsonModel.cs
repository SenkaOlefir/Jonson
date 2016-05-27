using System.Collections.Generic;

namespace Jonson_MVC.Models
{
    public class GraphJsonModel
    {
        public GraphJsonModel()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public ICollection<Node> Nodes { get; set; }
        public ICollection<Edge> Edges { get; set; }
    }

    public struct Node
    {
        public Node(int id, string label)
        {
            this.id = id;
            this.label = label;
        }
        public int id { get; }
        public string label { get; }
    }

    public struct Edge
    {
        private static readonly object _font;

        static Edge()
        {
            _font = new {align = "middle"};
        }

        public int from { get; set; }
        public int to { get; set; }
        public int ves { get; set; }
        public string arrows { get; set; }
        public string label => ves.ToString();
        public object font => _font;
    }
}