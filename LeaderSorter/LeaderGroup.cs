﻿using System;
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

        public double FitnessFunction(double goodFitBonus, double badFitBonus)
        {
            var fitness = 0.0;
            //Leader stuff
            //In the group stuff
            LeaderList.ForEach(leader =>
            {
                //The Hard Part
                fitness += leader.WhiteList.Count(friend => LeaderList.Any(x => x.LeaderGuid == friend)) * goodFitBonus;
                fitness -= leader.BlackList.Count(enemy => LeaderList.Any(x => x.LeaderGuid == enemy)) * badFitBonus;
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
