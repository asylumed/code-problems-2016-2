using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains.Routes
{
    public class RouteGraph
    {
        public static RouteGraph Parse(string routeGraphDefinition)
        {
            if (routeGraphDefinition == null) throw new ArgumentNullException("routeGraphDefinition");

            var routeEdgeDefinitions = routeGraphDefinition.Split(new[] { ", " }, StringSplitOptions.None);

            var routeGraph = new RouteGraph(routeEdgeDefinitions.Select(RouteEdge.Parse));

            return routeGraph;
        }

        private ILookup<RouteNode, RouteEdge> _edges;

        public RouteGraph(IEnumerable<RouteEdge> edges)
        {
            this._edges = edges.ToLookup(edge => edge.Start);
        }

        internal int GetDistance(params RouteNode[] nodes)
        {
            int totalDistance = 0;

            if (nodes == null) throw new ArgumentNullException("nodes");
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                var edge = GetEdge(nodes[i], nodes[i + 1]);
                totalDistance += edge.Distance;
            }

            return totalDistance;
        }

        private RouteEdge GetEdge(RouteNode startNode, RouteNode endNode)
        {
            foreach (var edge in _edges[startNode])
            {
                if (edge.End == endNode)
                {
                    return edge;
                }
            }

            throw new InvalidOperationException($"Route from {startNode} to {endNode} is not available.");
        }

        public IEnumerable<Route> Walk(RouteNode startNode, Func<Route, bool> routeVisitor)
        {
            return Walk(Route.CreateStart(startNode), routeVisitor);
        }

        private IEnumerable<Route> Walk(Route route, Func<Route, bool> routeVisitor)
        {
            if (routeVisitor(route))
            {
                yield return route;

                var edges = _edges[route.End];
                foreach (var edge in edges)
                {
                    foreach (var childRoute in Walk(route.Extend(edge), routeVisitor))
                    {
                        yield return childRoute;
                    }
                }
            }
        }
    }
}
