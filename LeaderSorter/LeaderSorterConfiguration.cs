using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.LeaderSorter
{
    public class LeaderSorterConfiguration
    {
        public int NumberOfTeams { get; set; }

        public LeaderSorterConfiguration(int numberOfTeams = 16)
        {
            NumberOfTeams = numberOfTeams;
        }
    }
}
