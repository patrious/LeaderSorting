using System.Runtime.InteropServices.WindowsRuntime;
using tests.LeaderSorter;

namespace tests
{
    static public class Program
    {
        static void Main()
        {
            var xmlReader = new DataInjectXls(@"D:\User Files\patrick\Code\LeaderSorting\Artifacts\LeaderList.xlsx");
            var iga = new LeaderSorting(new LeaderSorterConfiguration());
            xmlReader.FillMeWithData(ref iga);


            var algorthmConfig = new GeneticAlgorithmConfig();
            var logic = new GeneticAlgorithmLogic(iga, algorthmConfig);

            logic.RunAlgorithm();

            


        }

    }
}
