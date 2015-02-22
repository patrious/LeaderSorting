using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.LeaderSorter
{
    [Serializable]
    public class LeaderSorterConfiguration
    {
        public int NumberOfTeams { get; set; }

        public LeaderSorterConfiguration(int numberOfTeams = 16)
        {
            NumberOfTeams = numberOfTeams;
        }
    }
}
