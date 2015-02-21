using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.LeaderSorter
{
    [Serializable]
    public abstract class BaseGeneticAlgorithm : IGeneticAlgorithm
    {
        protected BaseGeneticAlgorithm()
        {
            Fitness = 0;
            PopulationSize = 0;
            GoodFitBonus = 3;
            BadFitReduction = -1;
        }

        public uint PopulationSize { get; set; }
        public abstract int GoodFitBonus { get; set; }
        public abstract int BadFitReduction { get; set; }
        public abstract double Fitness { get; protected internal set; }
        public abstract void MutatePopulation();

        public abstract IGeneticAlgorithm SpawnChild();
        public abstract List<IGeneticAlgorithm> SpawnChildren(int numberOfChildren);

    }

    public interface IGeneticAlgorithm
    {
        uint PopulationSize { get; set; }
        int GoodFitBonus { get; set; }
        int BadFitReduction { get; set; }
        double Fitness { get; }
        void MutatePopulation();

        IGeneticAlgorithm SpawnChild();
        List<IGeneticAlgorithm> SpawnChildren(int numberOfChildren);


    }

}
