using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace tests.LeaderSorter
{
    /*
    public class LeaderSorterTestDataGenerator : ILeaderDataSource
    {
        public LeaderSorterConfiguration Configuration { get; set; }
        public LeaderSorterTestDataGenerator(LeaderSorterConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void FillMeWithData(LeaderSorting leaderSorting)
        {
            foreach (LeaderSorterConfiguration.Colour source in Enum.GetValues(typeof(LeaderSorterConfiguration.Colour)))
            {
                var leaderList = GenerateTestLeaders();
                leaderSorting.ColourGroups.Add(new LeaderGroup(leaderList, source));
            }
            //Store all leaders in a pool.
            leaderSorting.ColourGroups.ForEach(x => leaderSorting.Leaderpool.AddRange(x.LeaderList));

            //Generate Whitelists/Blacklists
            GenerateWhiteBlackLists(leaderSorting.Leaderpool);
        }

        private void GenerateWhiteBlackLists(List<Leader> leaderPool)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            leaderPool.ForEach(x =>
            {
                x.WhiteList = leaderPool.PickRandom(rand.Next(3)).ToList();
                var tempBlackList = leaderPool.PickRandom(rand.Next(2));
                x.BlackList = tempBlackList.Except(x.WhiteList).ToList();
            });

        }
        private List<Leader> GenerateTestLeaders()
        {
            return Enumerable.Range(0, Configuration.NumberOfLeadersPerTeam).Select(i => Leader.GenerateLeader()).ToList();
        }


    }

    public class ImportLeaderData : ILeaderDataSource
    {
        LeaderSorting Problem { get; set; }
        LeaderSorterConfiguration Configuration { get; set; }

        public ImportLeaderData(LeaderSorterConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void FillMeWithData(LeaderSorting iga)
        {
            Problem = iga;

            //Extract all the leader names
            FillAllLeaders();
            FillAllRequests();
            FillLeaderTraits();
            GenerateInitalTeams();
            //Match up requests with leader names
        }

        private void GenerateInitalTeams()
        {
            
        }

        private void FillLeaderTraits()
        {
            throw new NotImplementedException();
        }

        private void FillAllRequests()
        {
            throw new NotImplementedException();
        }

        private void FillAllLeaders()
        {
            throw new NotImplementedException();
        }
    }
    */
}
