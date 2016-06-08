using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Routes;

namespace Trains
{
    class Program
    {
        static void Main(string[] args)
        {
            var routeGraph = RouteGraph.Parse("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");

            var answer1 = routeGraph.GetDistance('A', 'B', 'C');
            var answer2 = routeGraph.GetDistance('A', 'D');
            var answer3 = routeGraph.GetDistance('A', 'D', 'C');
            var answer4 = routeGraph.GetDistance('A', 'E', 'B', 'C', 'D');
            //var answer5 = routeGraph.GetDistance('A', 'E', 'D'); // Crashes (on purpose)

            var answer6 = routeGraph
                .Walk('C', (route) => route.GetEdgeCount() <= 3)
                .Where(route => route.EndNode == 'C' && route.GetEdgeCount() > 1)
                .Count();

            var answer7 = routeGraph
                .Walk('A', (route) => route.GetEdgeCount() <= 4)
                .Where(route => route.EndNode == 'C' && route.GetEdgeCount() == 4)
                .Count();

            var answer9 = routeGraph
                .Walk('B', (route) => !route.GetIsCyclic() || route.EndNode == 'B')
                .Where(route => route.EndNode == 'B' && route.GetEdgeCount() > 1)
                .OrderBy(route => route.Distance)
                .First()
                .Distance;

            var answer10 = routeGraph
                .Walk('C', (route) => route.Distance < 80)
                .Where(route => route.EndNode == 'C' && route.GetEdgeCount() > 1)
                .ToArray();
        }
    }
}
