using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.LeaderSorter
{
    
    [Serializable]
    public class LeaderGroup
    {
        public LeaderSorterConfiguration.Colour GroupColour;
        public List<Leader> LeaderList;

        public LeaderGroup(List<Leader> leaderList, LeaderSorterConfiguration.Colour groupColour)
        {
            LeaderList = leaderList;
            GroupColour = groupColour;
        }

        public double FitnessFunction(double goodFitBonus, double badFitBonus)
        {
            var fitness = 0.0;
            LeaderList.ForEach(leader =>
            {
                //fitness += leader.WhiteList.Count(friend => LeaderList.Contains(friend))* goodFitBonus;
               // fitness += leader.BlackList.Count(enemy => LeaderList.Contains(enemy)) * badFitBonus;
            }
            );
            return fitness;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(GroupColour.ToString());
            LeaderList.ForEach(x=>sb.AppendLine(x.PrettyPrint()));
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
