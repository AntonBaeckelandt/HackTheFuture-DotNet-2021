using HTF2020.Contracts.Models;
using HTF2020.Contracts.Requests;
using HTF2020.GameController.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheFellowshipOfCode.DotNet.YourAdventure
{
    public class Node
    {

        public Location Location { get; }
        public Location StartLocation { get; }
        public Location GoalLocation { get; }
        public int Cost { get { return DistanceFromStart + DistanceToGoal; } }
        public Node Parent { get; set; }

        public int DistanceToGoal { get; }
        public int DistanceFromStart { get; set; }

        public Node(Location location, Location startLocation, Location goalLocation)
        {
            Location = location;
            StartLocation = startLocation;
            GoalLocation = goalLocation;
            DistanceToGoal = Util.ManhattanDistance(Location, StartLocation);
            DistanceFromStart = Util.ManhattanDistance(Location, GoalLocation);
        }

    }
}
