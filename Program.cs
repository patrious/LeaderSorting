using System;
using System.Runtime.InteropServices.WindowsRuntime;
using GeneticAlgorithm.LeaderSorter;
using GeneticAlgorithm.LeaderSorter.DataInjest;

namespace GeneticAlgorithm
{
    static public class Program
    {
        static void Main()
        {
            try
            {

            
            var xmlReader = new DataInjectXls(@"D:\User Files\patrick\Code\LeaderSorting\Artifacts\LeaderList.xlsx");
            var iga = new LeaderSorting(new LeaderSorterConfiguration());
            xmlReader.FillMeWithData(ref iga);


            var algorthmConfig = new GeneticAlgorithmConfig();
            var logic = new GeneticAlgorithmLogic(iga, algorthmConfig);

            logic.RunAlgorithm();
            }
            catch (Exception)
            {
                Console.ReadLine();
            }
        }

    }
}
