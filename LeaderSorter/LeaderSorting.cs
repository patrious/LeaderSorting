﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Extensions;

namespace GeneticAlgorithm.LeaderSorter
{
    [Serializable]
    public class LeaderSorting : BaseGeneticAlgorithm
    {
        Random _rand = new Random((int)DateTime.Now.Ticks);
        private List<LeaderGroup> _leaderGroups = new List<LeaderGroup>();
        private List<Leader> _leaderpool = new List<Leader>();
        private LeaderSorterConfiguration Configuration { get; set; }

        #region Fields

        public Dictionary<string, int> ProtectedPrograms { get; set; }

        public List<Leader> Leaderpool
        {
            get { return _leaderpool; }
            set { _leaderpool = value; }
        }

        public List<LeaderGroup> LeaderGroups
        {
            get { return _leaderGroups; }
            set { _leaderGroups = value; }
        }

        public override int GoodFitBonus { get; set; }
        public override int BadFitReduction { get; set; }

        public override double Fitness
        {
            get
            {
                var fitnessColourGroups =
                    LeaderGroups.Sum(leaderGroup => leaderGroup.FitnessFunction(GoodFitBonus, BadFitReduction));
                var protectedProgramFitness =
                    (Math.Max(0, LeaderGroups.Count(x => x.LeaderList.Any(y => ProtectedPrograms.ContainsKey(y.Program))) - Configuration.ProtectedGroupSpreadFactor)) * BadFitReduction * 10;

                var traitFitness = CalculateTraitFitness();

                var leaderTypeFitness = CalculateLeaderTypeFitness();

                return fitnessColourGroups + protectedProgramFitness + traitFitness + leaderTypeFitness;
            }
            protected internal set { }
        }

        private double CalculateLeaderTypeFitness()
        {
            var dh = LeaderGroups.Max(x => x.Huges) - LeaderGroups.Min(x => x.Huges);
            var db = LeaderGroups.Max(x => x.Bigs) - LeaderGroups.Min(x => x.Bigs);
            return (dh + db) * BadFitReduction;
        }

        private double CalculateTraitFitness()
        {
            var dd = LeaderGroups.Max(x => x.DirectorshipsHeld) - LeaderGroups.Min(x => x.DirectorshipsHeld);
            var dc = LeaderGroups.Max(x => x.CoopsInFall) - LeaderGroups.Min(x => x.CoopsInFall);
            var dh = LeaderGroups.Max(x => x.Hems) - LeaderGroups.Min(x => x.Hems);
            var dr = LeaderGroups.Max(x => x.Returnings) - LeaderGroups.Min(x => x.Returnings);
            return (dd + dc + dh + dr) * BadFitReduction;
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
                var randIndex = _rand.PickUniqueRandomNumbers(2, _leaderGroups.Count);
                if (randIndex[0] == randIndex[1]) continue;
                var list1 = _leaderGroups[randIndex[0]].LeaderList;
                var list2 = _leaderGroups[randIndex[1]].LeaderList;

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

                _leaderGroups[randIndex[0]].LeaderList = list1;
                _leaderGroups[randIndex[1]].LeaderList = list2;

            }

        }

        public override string PrettyPrint()
        {
            var sb = new StringBuilder();
            _leaderGroups.ForEach(x => sb.Append(x.ToString()));
            return sb.ToString();
        }

        public void PrepWorkspace()
        {
            //Find Protected Programs
            ProtectedPrograms = Leaderpool.GroupBy(leader => leader.Program).Select(x => new
            {
                Program = x.Key,
                Count = x.Count()
            }).Where(x => x.Count < Configuration.ProtectedGroupThreshold).ToDictionary(x => x.Program, x => x.Count);

            //TODO: Find more efficient way of doing this
            for (var i = 0; i < Configuration.NumberOfTeams; i++)
            {
                LeaderGroups.Add(new LeaderGroup(new List<Leader>()));
            }
            for (var i = 0; i < _leaderpool.Count; i++)
            {
                LeaderGroups[i % Configuration.NumberOfTeams].LeaderList.Add(_leaderpool[i]);
            }

            foreach (var leader in Leaderpool)
            {
                leader.BlackList.AddRange(
                    leader.RawBlackList.Select(
                        antiRequestName => Leaderpool.Find(x => x.PublicName.Trim() == antiRequestName.Trim()))
                        .Where(tempLeader => tempLeader != null)
                        .Select(x => x.LeaderId));
                leader.WhiteList.AddRange(
                    leader.RawWhiteList.Select(
                        requestName => Leaderpool.Find(x => x.PublicName.Trim() == requestName.Trim()))
                        .Where(tempLeader => tempLeader != null)
                        .Select(x => x.LeaderId));
            }
        }
    }
}
