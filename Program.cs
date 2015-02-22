﻿using tests.LeaderSorter;

namespace tests
{
    static public class Program
    {
        static void Main()
        {
            var xmlReader = new DataInjectXls(@"D:\User Files\patrick\Code\LeaderSorting\Artifacts\LeaderList.xlsx");
            var iga = new LeaderSorting(new LeaderSorterConfiguration());
            xmlReader.FillMeWithData(ref iga);
        }

    }
}
