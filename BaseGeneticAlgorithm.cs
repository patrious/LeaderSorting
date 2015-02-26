using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        
        public async Task<List<IGeneticAlgorithm>> SpawnChildren( int numberOfChildren)
        {
            //  Task<byte[]> getContentsTask = client.GetByteArrayAsync(url);
            //  byte[] urlContents = await getContentsTask;
            //TODO: Parallel this

            var children = new List<IGeneticAlgorithm>();
            for (var i = 0; i < numberOfChildren; i++)
            {
                var child = await SpawnChild();
                children.Add(child);
            }

            return children.OrderBy(x => -x.Fitness).ToList();
        }

        private async Task<IGeneticAlgorithm> SpawnChild()
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

        Task<List<IGeneticAlgorithm>> SpawnChildren(int numberOfChildren);


    }

}
