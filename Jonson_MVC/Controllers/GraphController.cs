using System;
using System.Linq;
using System.Web.Mvc;
using Jonson_MVC.Models;
using Johnson;
using System.Collections.Generic;

namespace Jonson_MVC.Controllers
{
    public class GraphController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            const int n = 3;

            GraphMatrixModel model = new GraphMatrixModel();

            if (Session["currentGraph"] == null)
            {
                model.Data = new int?[n][];
                for (var i = 0; i < n; i++)
                {
                    model.Data[i] = new int?[n];
                }
            }
            else
            {
                model.Data = (int?[][])Session["currentGraph"];
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(GraphMatrixModel graphModel)
        {
            var graphJsonModel = new GraphJsonModel();

            Session["currentGraph"] = graphModel.Data;

            try
            {
                Session["paths"] = CalculatePath(graphModel.Data);
            }
            catch (NegativeCyclesException)
            {
                ModelState.AddModelError("negativeCyclesException", "Граф имеет циклы с отрицательным весом");
                return View(graphModel);
            }

            for (var i = 0; i < graphModel.Data.Length; i++)
            {
                graphJsonModel.Nodes.Add(new Node(i, i.ToString()));
                for (var j = 0; j < graphModel.Data.Length; j++)
                {
                    if (graphModel.Data[i][j].HasValue)
                    {
                        if (!graphJsonModel.Edges.Any(x => x.from == j && x.to == i && x.ves == graphModel.Data[i][j].Value))
                        {
                            var edge = new Edge
                            {
                                from = i,
                                to = j,
                                ves = graphModel.Data[i][j].Value,
                                arrows = (graphModel.Data[i][j] == graphModel.Data[j][i]) ? "to, from" : "to"
                            };

                            graphJsonModel.Edges.Add(edge);
                        }
                    }
                }
            }

            return View("Index", graphJsonModel);
        }

        private Dictionary<int, Dictionary<int, KeyValuePair<int, int>>> CalculatePath(int?[][] data)
        {
            var graph = Graph<int>.FormMatrix(data);
            var jsonson = new Johnson<int>(graph);
            var result = jsonson.Perform(data.Length + 1);
            return result;
        }

        [HttpGet]
        public JsonResult CalculatePath(int from, int to)
        {
            var paths = (Dictionary<int, Dictionary<int, KeyValuePair<int, int>>>)Session["paths"];
            var stack = new Stack<KeyValuePair<int, int>>();

            try
            {
                stack.Push(new KeyValuePair<int, int>(to, paths[from][to].Value));
                var from1 = to;

                while (from1 != from)
                {
                    if (from1 == paths[from][from1].Key)
                    {
                        throw new PathNotExistException();
                    }
                    from1 = paths[from][from1].Key;
                    stack.Push(new KeyValuePair<int, int>(from1, paths[from][from1].Value));
                }
            }
            catch (PathNotExistException)
            {
                return Json(new { isError = true, errorMessage = "Пути не существует" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { isError = true, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(stack.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}