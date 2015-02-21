using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Extensions;

namespace tests.LeaderSorter
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

        public override IGeneticAlgorithm SpawnChild()
        {
            return RandomExtension.DeepClone(this);
        }

        public override List<IGeneticAlgorithm> SpawnChildren(int numberOfChildren)
        {
            var children = new List<IGeneticAlgorithm>();
            for (var i = 0; i < numberOfChildren; i++)
            {
                var child = RandomExtension.DeepClone(this);
                child.MutatePopulation();
                children.Add(child);
            }
            return children.OrderBy(x => -x.Fitness).ToList();
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

        public string PrettyPrint()
        {
            var sb = new StringBuilder();
            _colourGroups.ForEach(x => sb.Append(x.ToString()));
            return sb.ToString();
        }

    }

    public static class RandomExtension
    {
        //Someday it will be unique
        public static List<int> PickUniqueRandomNumbers(this Random rand, int numberOfItems, int max = 1, int min = 0)
        {
            Debug.Assert(numberOfItems < max - min);

            var returnNumbers = new List<int>();
            while (returnNumbers.Count < numberOfItems)
            {
                returnNumbers.Add(rand.Next(min, max));
            }
            return returnNumbers;
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }


}
