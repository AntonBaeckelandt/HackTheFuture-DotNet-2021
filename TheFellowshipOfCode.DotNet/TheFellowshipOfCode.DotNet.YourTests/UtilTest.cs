using FluentAssertions;
using HTF2020.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheFellowshipOfCode.DotNet.YourAdventure;
using Xunit;

namespace TheFellowshipOfCode.DotNet.YourTests
{
    public class UtilTest
    {

        [Fact]
        public async Task Util_ManhattanDistance()
        {
            Location loc1 = new Location(1, 2);
            Location loc2 = new Location(3, 4);
            // 1 - 3 = -2 => 2
            // 2 - 4 = 2 => 2
            // 4
            Util.ManhattanDistance(loc1, loc2).Should().Be(4);
        }

    }
}
