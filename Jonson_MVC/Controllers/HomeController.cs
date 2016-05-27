using System.Linq;
using System.Web.Mvc;
using Jonson_MVC.Models;

namespace Jonson_MVC.Controllers
{
    public class GraphController : Controller
    {
        public ActionResult Index()
        {
            int?[,] graph = {
                { null, 10, 18, 7, null, null },
                { 10, null, 16, 9, 21, null },
                { null, 16, null, null, null, 15 },
                { 7, 9, null, null, null, 12 },
                { null, null, null, null, null, 23 },
                { null, null, 15, null, 23, null }
            };

            var graphModel = new GraphJsonModel();

            for (var i = 0; i < 6; i++)
            {
                graphModel.Nodes.Add(new Node(i, i.ToString()));
                for (var j = 0; j < 6; j++)
                {
                    if (graph[i, j].HasValue)
                    {
                        if (!graphModel.Edges.Any(x => x.from == j && x.to == i && x.ves == graph[i, j].Value))
                        {
                            var edge = new Edge
                            {
                                from = i,
                                to = j,
                                ves = graph[i, j].Value,
                                arrows = (graph[i, j] == graph[j, i]) ? "to, from" : "to"
                            };

                            graphModel.Edges.Add(edge);
                        }
                    }
                }
            }

            return View(graphModel);
        }
    }
}