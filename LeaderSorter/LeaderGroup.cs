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

        //TODO: Do this in a generic count way

        public int DirectorshipsHeld
        {

            get { return LeaderList.AsParallel().Count(leader => leader.Traits.Contains(Traits.MajorDirectorship)); }
        }

        public int CoopsInFall
        {

            get { return LeaderList.AsParallel().Count(leader => leader.Traits.Contains(Traits.CoopInFall)); }
        }

        public int Returnings
        {

            get { return LeaderList.AsParallel().Count(leader => leader.Traits.Contains(Traits.Returning)); }
        }

        public int Hems
        {

            get { return LeaderList.AsParallel().Count(leader => leader.Traits.Contains(Traits.Hem)); }
        }

        public int Bigs
        {

            get { return LeaderList.AsParallel().Count(leader => leader.LeaderType.Equals(LeaderType.Big)); }
        }

        public int Huges
        {

            get { return LeaderList.AsParallel().Count(leader => leader.LeaderType.Equals(LeaderType.Huge)); }
        }

        public double FitnessFunction(double goodFitBonus, double badFitBonus)
        {
            //Leader stuff
            //In the group stuff
            var fitness = LeaderList.AsParallel().Sum(leader =>
                 {
                     //The Hard Part
                     var fit = 0.0;
                     fit += leader.WhiteList.Count(friend => LeaderList.Any(x => x.LeaderId == friend)) * goodFitBonus;
                     fit -= leader.BlackList.Count(enemy => LeaderList.Any(x => x.LeaderId == enemy)) * badFitBonus * 10;
                     return fit;
                 });
            return fitness;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Huges:{0} Bigs:{1} Hems:{2} Return:{3} Coop:{4} Directorship:{5}", Huges, Bigs,
                Hems, Returnings, CoopsInFall, DirectorshipsHeld));
            LeaderList.ForEach(x => sb.AppendLine(x.PrettyPrint()));
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
