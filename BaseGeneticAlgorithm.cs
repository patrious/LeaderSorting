using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    [Serializable]
    public abstract class BaseGeneticAlgorithm : IGeneticAlgorithm
    {
        protected BaseGeneticAlgorithm()
        {
            Fitness = 0;
            PopulationSize = 0;
            GoodFitBonus = 3;
            BadFitReduction = -5;
        }

        public uint PopulationSize { get; set; }
        public abstract int GoodFitBonus { get; set; }
        public abstract int BadFitReduction { get; set; }
        public abstract double Fitness { get; protected internal set; }
        public abstract void MutatePopulation();
        public abstract string PrettyPrint();
        

        public IGeneticAlgorithm SpawnChild()
        {
            return Extensions.Extensions.DeepClone(this);
        }

        public List<IGeneticAlgorithm> SpawnChildren( int numberOfChildren)
        {
            //TODO: Parallel this
            var children = new List<IGeneticAlgorithm>();
            for (var i = 0; i < numberOfChildren; i++)
            {
                var child = Extensions.Extensions.DeepClone(this);
                child.MutatePopulation();
                children.Add(child);
            }
            return children.OrderBy(x => -x.Fitness).ToList();
        }

    }

    public interface IGeneticAlgorithm
    {
        uint PopulationSize { get; set; }
        int GoodFitBonus { get; set; }
        int BadFitReduction { get; set; }
        double Fitness { get; }
        void MutatePopulation();
        string PrettyPrint();

        IGeneticAlgorithm SpawnChild();
        List<IGeneticAlgorithm> SpawnChildren(int numberOfChildren);


    }

}
