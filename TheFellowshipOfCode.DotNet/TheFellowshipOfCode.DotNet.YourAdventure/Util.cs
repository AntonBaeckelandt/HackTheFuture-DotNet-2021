using HTF2020.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheFellowshipOfCode.DotNet.YourAdventure
{
    public class Util
    {

        public static int ManhattanDistance(Location loc1, Location loc2)
        {
            return Math.Abs(loc1.X - loc2.X) + Math.Abs(loc1.Y - loc2.Y);
        }

    }
}
