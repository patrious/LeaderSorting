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
        public int ProtectedGroupThreshold { get; set; }
        public int ProtectedGroupSpreadFactor { get; set; }

        public LeaderSorterConfiguration(int numberOfTeams = 6)
        {
            //TODO: Do this correctly.
            NumberOfTeams = numberOfTeams;
            ProtectedGroupSpreadFactor = 4;
            ProtectedGroupThreshold = 4;
        }
    }
}
