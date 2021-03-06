﻿using HTF2020.Contracts.Enums;
using HTF2020.Contracts.Models;
using HTF2020.Contracts.Requests;
using HTF2020.GameController.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFellowshipOfCode.DotNet.YourAdventure
{
    class PathFinding
    {

        private readonly Random _random = new Random();
        private Queue<Node> steps = new Queue<Node>();

        public PathFinding(PlayTurnRequest request)
        {
            List<Location> chests = request.Map.GetChestsLocations().ToList();

            List<Location> goals = new List<Location>();
            // Chests
            goals.AddRange(chests);
            // Finish
            goals.Add(request.Map.GetEndPosition());

            for (int i = 0; i < goals.Count; i++)
            {
                Location start = i > 0 ? goals[i - 1] : request.PartyLocation;
                PathFinder currentPathFinder = new PathFinder(request.Map, start, goals[i]);
                Node[] nodes = currentPathFinder.SearchPath();
                Array.Reverse(nodes);
                foreach (Node n in nodes)
                    steps.Enqueue(n);
            }
        }

        public Task<Turn> NextMove(PlayTurnRequest request)
        {
            // Get next action in the queue
            if (steps.Peek() != null)
            {
                Node node = steps.Dequeue();
                TurnAction action = GetTurnAction(request.PartyLocation, node.Location);
                return Task.FromResult(request.PossibleActions.Contains(action) ? new Turn(action) : new Turn(request.PossibleActions[_random.Next(request.PossibleActions.Length)]));
            }

            // Fallback method: do something random
            return Task.FromResult(new Turn(request.PossibleActions[_random.Next(request.PossibleActions.Length)]));
        }


        private TurnAction GetTurnAction(Location locNow, Location locGoal)
        {
            if (locGoal.X > locNow.X)
                return TurnAction.WalkEast;
            else if (locGoal.X < locNow.X)
                return TurnAction.WalkWest;
            else if (locGoal.Y > locNow.Y)
                return TurnAction.WalkSouth;
            else if (locGoal.Y < locNow.Y)
                return TurnAction.WalkNorth;

            return TurnAction.Pass;
        }

    }
}
