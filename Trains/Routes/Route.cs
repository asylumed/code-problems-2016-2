using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains.Routes
{
    [DebuggerDisplay("{ToString()} ({Distance})")]
    public class Route
    {
        public Route(RouteNode endNode, Route startRoute, int distance)
        {
            this.StartRoute = startRoute;
            this.EndNode = endNode;
            this.Distance = distance;
        }

        public Route StartRoute { get; }
        public RouteNode EndNode { get; }
        public int Distance { get; }

        public int GetEdgeCount()
        {
            if (StartRoute == null) return 0;
            return 1 + StartRoute.GetEdgeCount();
        }

        public bool Contains(RouteNode node)
        {
            if (EndNode == node) return true;
            return StartRoute != null && StartRoute.Contains(node);
        }

        internal bool GetIsCyclic()
        {
            if (StartRoute == null) return false;
            return StartRoute.GetIsCyclic() || StartRoute.Contains(EndNode);
        }

        public override string ToString()
        {
            if (StartRoute == null) return EndNode.ToString();
            return StartRoute.ToString() + EndNode.ToString();
        }

    }
}
