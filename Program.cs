using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

                var task = new Task(logic.RunAlgorithm);
                task.Start();
                while (true)
                {
                    var input = Console.ReadLine();

                    if (string.Compare(input, "exit", StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        break;
                    }
                    else
                    {
                        logic.printCurrentBestPopulation();
                    }

                }
                Console.WriteLine("Exiting... press any body to close");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }

    }
}
