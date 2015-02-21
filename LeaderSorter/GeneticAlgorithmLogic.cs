using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.LeaderSorter
{
    class GeneticAlgorithmLogic
    {

        public IGeneticAlgorithm GeneticAlgorithm { get; set; }
        public IGeneticAlgorithm CurrentPopulation { get; set; }
        public IGeneticAlgorithm BestPopulation { get; set; }

        public double CurrentFitness { get { return CurrentPopulation.Fitness; } }
        public int NoImprovements { get; set; }
        public GeneticAlgorithmConfig Configuration { get; set; }

        public event EventHandler NewBestFistnessFoundEvent;
        public event EventHandler FoundSolutionEvent;

        protected virtual void OnFoundSolutionEvent()
        {
            var handler = FoundSolutionEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnNewBestFistnessFoundEvent()
        {
            var handler = NewBestFistnessFoundEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public GeneticAlgorithmLogic(IGeneticAlgorithm algorithm, GeneticAlgorithmConfig config)
        {
            
            GeneticAlgorithm = algorithm;
            Configuration = config;
            NewBestFistnessFoundEvent += OnNewBestFistnessFoundEvent;
            FoundSolutionEvent += OnFoundSolutionEvent; 
        }

        private void OnFoundSolutionEvent(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("Best Population Found: {0}", BestPopulation.Fitness);
            Console.WriteLine("Done");
        }

        private void OnNewBestFistnessFoundEvent(object sender, EventArgs eventArgs)
        {
            BestPopulation = CurrentPopulation;
            Console.WriteLine(((LeaderSorting)BestPopulation).PrettyPrint());
        }

        public void RunAlgorithm()
        {
            CurrentPopulation = GeneticAlgorithm;
            BestPopulation = CurrentPopulation;
            //Setup
            var testNumber = 1;
            NoImprovements = 0;
            while (true)
            {
                //Generate sorted list of children
                var spawn = CurrentPopulation.SpawnChildren(Configuration.NumChildrenToSpawn);
                //Display Progress
                DisplayProgress(testNumber++);
                if (testNumber > Configuration.NumberOfIterations) break;

                if (!(spawn[0].Fitness > CurrentFitness))
                {
                    if (NoImprovements++ < Configuration.NoImproveThreshold) continue;
                    //If there have been no improvements for long time, swap out 'main' with secondary generation
                    NoImprovements = 0;
                    CurrentPopulation = spawn[1];
                }
                else
                {
                    NoImprovements = 0;
                    CurrentPopulation = spawn[0];

                    if (CurrentFitness > BestPopulation.Fitness)
                    {
                        OnNewBestFistnessFoundEvent();
                    }
                }
                //We have found our goal, we can break
                if (CurrentFitness >= Configuration.Goal)
                {
                    OnFoundSolutionEvent(); 
                    break;
                }
            }
            
        }

        private void DisplayProgress(int testNumber)
        {
            if (testNumber++ % 100 == 0) Console.WriteLine("{0:00000} - Current Fitness: {1} \tBest Fitness {2}", testNumber, CurrentFitness, BestPopulation.Fitness);
        }
    }

    public class GeneticAlgorithmConfig
    {
        public double Goal;
        public int NumberOfIterations;
        public int NoImproveThreshold;
        public int NumChildrenToSpawn;

        public GeneticAlgorithmConfig(double goal = 50, int noImprovementThreshold = 5000, int numChildrenToSpawn = 5, int numberOfIterations = 100000)
        {
            Goal = goal;
            NoImproveThreshold = noImprovementThreshold;
            NumChildrenToSpawn = numChildrenToSpawn;
            NumberOfIterations = numberOfIterations;
        }
    }
}
