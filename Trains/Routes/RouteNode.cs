using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains.Routes
{
    // Struct so that we get equals and gethashcode free.
    public struct RouteNode
    {
        public RouteNode(char name)
        {
            Name = char.ToUpperInvariant(name);
        }

        public char Name { get; }

        public static implicit operator RouteNode(char nodeName)
        {
            return new RouteNode(nodeName);
        }

        public static bool operator ==(RouteNode a, RouteNode b)
        {
            return a.Name == b.Name;
        }
        public static bool operator !=(RouteNode a, RouteNode b)
        {
            return a.Name != b.Name;
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
