using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.LeaderSorter
{

    [Serializable]
    public class LeaderGroup
    {
        public List<Leader> LeaderList;

        public LeaderGroup(List<Leader> leaderList)
        {
            LeaderList = leaderList;
        }

        //TODO: Do this is a generic count way

        public int DirectorshipsHeld
        {
            //TODO: Parallel this
            get { return LeaderList.Count(leader => leader.Traits.Contains(Traits.MajorDirectorship)); }
        }

        public int CoopsInFall
        {
            //TODO: Parallel this
            get { return LeaderList.Count(leader => leader.Traits.Contains(Traits.CoopInFall)); }
        }
        
        public int Returnings
        {
            //TODO: Parallel this
            get { return LeaderList.Count(leader => leader.Traits.Contains(Traits.Returning)); }
        }

        public int Hems
        {
            //TODO: Parallel this
            get { return LeaderList.Count(leader => leader.Traits.Contains(Traits.Hem)); }
        }

        public int Bigs
        {
            //TODO: Parallel this
            get { return LeaderList.Count(leader => leader.LeaderType.Equals(LeaderType.Big)); }
        }

        public int Huges
        {
            //TODO: Parallel this
            get { return LeaderList.Count(leader => leader.LeaderType.Equals(LeaderType.Huge)); }
        }

        public double FitnessFunction(double goodFitBonus, double badFitBonus)
        {
            //TODO: Parallel this
            var fitness = 0.0;
            //Leader stuff
            //In the group stuff
            LeaderList.ForEach(leader =>
            {
                //The Hard Part
                fitness += leader.WhiteList.Count(friend => LeaderList.Any(x => x.LeaderId == friend)) * goodFitBonus;
                fitness -= leader.BlackList.Count(enemy => LeaderList.Any(x => x.LeaderId == enemy)) * badFitBonus * 10;
            }
            );
            return fitness;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            LeaderList.ForEach(x => sb.AppendLine(x.PrettyPrint()));
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
