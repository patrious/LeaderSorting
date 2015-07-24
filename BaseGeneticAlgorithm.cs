using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    [Serializable]
    public abstract class BaseGeneticAlgorithm : IGeneticAlgorithm
    {
        protected BaseGeneticAlgorithm()
        {
            PopulationSize = 0;
            GoodFitBonus = 3;
            BadFitReduction = -5;
        }

        public uint PopulationSize { get; set; }
        public int GoodFitBonus { get; set; }
        public int BadFitReduction { get; set; }
        public abstract double Fitness { get; }
        public abstract void MutatePopulation();
        public abstract string PrettyPrint();

        public List<IGeneticAlgorithm> SpawnChildren(int numberOfChildren)
        {
            var children = new List<IGeneticAlgorithm>(numberOfChildren);
            
            while (children.Count < numberOfChildren)
            {
                children.Add(SpawnChild());
            }


            return children.OrderBy(x => -x.Fitness).ToList();
        }

        private IGeneticAlgorithm SpawnChild()
        {
            var child = Extensions.Extensions.DeepClone(this);
            child.MutatePopulation();
            return child;
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

        List<IGeneticAlgorithm> SpawnChildren(int numberOfChildren);


    }

}
