using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.LeaderSorter
{
    public class LeaderSorterConfiguration
    {

        [Serializable]
        public enum Colour
        {
            Red,
            Green,
            Blue
        }

        public int NumberOfTeams { get; set; }
        public int NumberOfLeadersPerTeam { get; set; }

        public LeaderSorterConfiguration(int numberOfTeams = 3, int numberOfLeadersPerTeam = 5)
        {
            NumberOfTeams = numberOfTeams;
            NumberOfLeadersPerTeam = numberOfLeadersPerTeam;
        }
    }
}
