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
        private Route(RouteNode endNode, Route startRoute, int distance)
        {
            StartRoute = startRoute;
            End = endNode;
            Distance = distance;
        }

        public Route StartRoute { get; }
        public RouteNode End { get; }
        public int Distance { get; }

        public int GetEdgeCount()
        {
            if (StartRoute == null) return 0;
            return 1 + StartRoute.GetEdgeCount();
        }

        public bool Contains(RouteNode node)
        {
            if (End == node) return true;
            return StartRoute != null && StartRoute.Contains(node);
        }

        internal bool GetIsCyclic()
        {
            if (StartRoute == null) return false;
            return StartRoute.GetIsCyclic() || StartRoute.Contains(End);
        }

        public override string ToString()
        {
            if (StartRoute == null) return End.ToString();
            return StartRoute.ToString() + End.ToString();
        }

        public Route Extend(RouteEdge edge)
        {
            if (edge == null) throw new ArgumentNullException("edge");
            if (edge.Start != this.End) throw new InvalidOperationException($"Edge starting point {edge.Start} does not match route end poin {End}");

            return new Route(edge.End, this, this.Distance + edge.Distance);
        }

        internal static Route CreateStart(RouteNode startNode)
        {
            return new Route(startNode, null, 0);
        }
    }
}
