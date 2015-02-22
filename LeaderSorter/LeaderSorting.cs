using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extensions;

namespace GeneticAlgorithm.LeaderSorter
{
    [Serializable]
    public class LeaderSorting : BaseGeneticAlgorithm
    {
        Random _rand = new Random((int)DateTime.Now.Ticks);
        private List<LeaderGroup> _colourGroups = new List<LeaderGroup>();
        private List<Leader> _leaderpool = new List<Leader>();
        private LeaderSorterConfiguration Configuration { get; set; }

        #region Fields

        public List<Leader> Leaderpool
        {
            get { return _leaderpool; }
            set { _leaderpool = value; }
        }

        public List<LeaderGroup> ColourGroups
        {
            get { return _colourGroups; }
            set { _colourGroups = value; }
        }

        public override int GoodFitBonus { get; set; }
        public override int BadFitReduction { get; set; }

        public override double Fitness
        {
            get { return ColourGroups.Sum(leaderGroup => leaderGroup.FitnessFunction(GoodFitBonus, BadFitReduction)); }
            protected internal set { }
        }

        internal double MaxValue
        {
            get { return _leaderpool.Sum(leader => leader.WhiteList.Count); }
        }

        #endregion

        public LeaderSorting(LeaderSorterConfiguration configuration)
        {
            Configuration = configuration;
        }

        public override void MutatePopulation()
        {
            _rand = new Random((int)DateTime.Now.Ticks);
            var upper = _rand.Next(10, 20);
            for (var i = 0; i < upper; i++)
            {
                //2 Random Numbers for choosing which lists
                var randIndex = _rand.PickUniqueRandomNumbers(2, _colourGroups.Count);
                if (randIndex[0] == randIndex[1]) continue;
                var list1 = _colourGroups[randIndex[0]].LeaderList;
                var list2 = _colourGroups[randIndex[1]].LeaderList;

                //2 random numbers corresponding to the number of leaders per list
                var list1Index = _rand.Next(list1.Count);
                var list2Index = _rand.Next(list2.Count);

                var itemList1 = list1.ElementAt(list1Index);
                var itemList2 = list2.ElementAt(list2Index);

                //Swap them
                list1.Add(itemList2);
                list2.Add(itemList1);

                list1.Remove(itemList1);
                list2.Remove(itemList2);

                _colourGroups[randIndex[0]].LeaderList = list1;
                _colourGroups[randIndex[1]].LeaderList = list2;

            }

        }

        public override string PrettyPrint()
        {
            var sb = new StringBuilder();
            _colourGroups.ForEach(x => sb.Append(x.ToString()));
            return sb.ToString();
        }

        public void PrepWorkspace()
        {
            //TODO: Find more efficient way of doing this
            for (var i = 0; i < Configuration.NumberOfTeams; i++)
            {
                ColourGroups.Add(new LeaderGroup(new List<Leader>()));
            }
            for (var i = 0; i < _leaderpool.Count; i++)
            {
                ColourGroups[i % Configuration.NumberOfTeams].LeaderList.Add(_leaderpool[i]);
            }

            foreach (var leader in Leaderpool)
            {
                var blacklist = leader.RawBlackList.Select(antiRequestName => Leaderpool.First(x => x.PublicName == antiRequestName).LeaderId);
                var whitelist =
                    leader.RawWhiteList.Select(
                        antiRequestName => Leaderpool.First(x => x.PublicName == antiRequestName).LeaderId);
                leader.BlackList.AddRange(blacklist);
                leader.WhiteList.AddRange(whitelist);

            }
        }
    }
}
