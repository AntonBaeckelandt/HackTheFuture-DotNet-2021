using HTF2020.Contracts.Enums;
using HTF2020.Contracts.Models;
using HTF2020.GameController.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheFellowshipOfCode.DotNet.YourAdventure
{
    public static class MapExtensions
    {

        public static IEnumerable<Location> GetChestsLocations(this Map map)
        {
			for (int i = 0; i < map.Tiles.GetLength(0); i++)
			{
				for (int j = 0; j < map.Tiles.GetLength(1); j++)
				{
					if (map.Tiles[i, j].TileType == TileType.TreasureChest)
					{
						yield return new Location(i, j);
					}
				}
			}
		}

		public static Location[] GetNeighbours(this Map map, Location location)
		{
			if (map.IsOutOfBounds(location))
			{
				return null;
			}
			Location[] source = new Location[4]
			{
			new Location(location.X + 1, location.Y),
			new Location(location.X, location.Y + 1),
			new Location(location.X - 1, location.Y),
			new Location(location.X, location.Y - 1)
			};
			return (from l in ((IEnumerable<Location>)source).Where((Func<Location, bool>)map.IsNotOutOfBounds) select new Location(l.X, l.Y)).ToArray();
		}

		public static Location GetEndPosition(this Map map)
		{
			for (int i = 0; i < map.Tiles.GetLength(0); i++)
			{
				for (int j = 0; j < map.Tiles.GetLength(1); j++)
				{
					if (map.Tiles[i, j].TileType == TileType.Finish)
					{
						return new Location(i, j);
					}
				}
			}
			return null;
		}

	}
}
