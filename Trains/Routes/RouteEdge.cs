using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Trains.Routes
{
    public class RouteEdge
    {
        public RouteNode Start { get; }
        public RouteNode End { get; }

        public int Distance { get; }

        public RouteEdge(RouteNode start, RouteNode end, int distance)
        {
            this.Start = start;
            this.End = end;
            this.Distance = distance;
        }

        public static RouteEdge Parse(string routeEdgeText)
        {
            if (routeEdgeText == null) throw new ArgumentNullException("routeEdgeText");

            var match = Regex.Match(routeEdgeText, "([a-zA-Z])([a-zA-Z])([0-9]+)");
            if (!match.Success)
            {
                throw new ArgumentException($"Route edge definition '{routeEdgeText}' did not match the expected format.");
            }

            var startNodeName = match.Groups[1].Value[0];
            var endNodeName = match.Groups[2].Value[0];
            var edgeDistance = int.Parse(match.Groups[3].Value);

            return new RouteEdge(startNodeName, endNodeName, edgeDistance);
        }
    }
}
