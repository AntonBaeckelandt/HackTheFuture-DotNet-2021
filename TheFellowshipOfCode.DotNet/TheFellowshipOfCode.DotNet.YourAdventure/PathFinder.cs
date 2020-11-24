using HTF2020.Contracts.Enums;
using HTF2020.Contracts.Models;
using HTF2020.GameController.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TheFellowshipOfCode.DotNet.YourAdventure
{
    public class PathFinder
    {

        public Node Goal { get; }
        public Node Start { get; }

        private Map map;

        public PathFinder(Map map, Location start, Location goal)
        {
            this.map = map;
            Start = new Node(start, start, goal);
            Goal = new Node(goal, goal, goal);
        }

        public Node[] SearchPath()
        {
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            openList.Add(Start);
            int g = 0;
            while (openList.Count > 0)
            {
                Node lowestCostNode = openList.OrderByDescending(i => i.Cost).First();
                Location[] neighbours = map.GetNeighbours(lowestCostNode.Location);

                closedList.Add(lowestCostNode);
                openList.Remove(lowestCostNode);

                Node[] neighbourNodes = neighbours.Select(n => new Node(n, Start.Location, Goal.Location)).ToArray();
                if (closedList.FirstOrDefault(l => l.Location.X == Goal.Location.X && l.Location.Y == Goal.Location.Y) != null)
                {
                    Debug.WriteLine("Found the goal");
                    break;
                }

                foreach (Node neighbour in neighbourNodes)
                {

                    // In closed list?
                    if (closedList.FirstOrDefault(l => l.Location.X == neighbour.Location.X
                            && l.Location.Y == neighbour.Location.Y) != null)
                        continue;
                    // Not walkable?
                    if (map.GetTileAtLocation(neighbour.Location).TerrainType != TerrainType.Grass)
                        continue;

                    // Update values!
                    // Not in open list?
                    if (openList.FirstOrDefault(l => l.Location.X == neighbour.Location.X && l.Location.Y == neighbour.Location.Y) == null)
                    {
                        neighbour.Parent = lowestCostNode;
                        neighbour.DistanceFromStart = g + 1;
                        openList.Insert(0, neighbour);
                    }
                    else
                    {
                        if (g + neighbour.DistanceToGoal < neighbour.Cost)
                        {
                            neighbour.DistanceFromStart = g + 1;
                            neighbour.Parent = lowestCostNode;
                        }
                    }
                }
            }

            Debug.WriteLine($"=== FOUND PATH FOR {Goal.Location.X}, {Goal.Location.Y} ===");
            List<Node> output = new List<Node>();
            Node current = closedList.FirstOrDefault(l => l.Location.X == Goal.Location.X && l.Location.Y == Goal.Location.Y);
            output.Add(current);
            while (current.Parent != null)
            {
                Debug.WriteLine("Next is: " + current.Parent.Location.X + ", " + current.Parent.Location.Y);
                current = current.Parent;
                output.Add(current);
            }
            return output.ToArray();
        }




    }



}
