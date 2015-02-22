using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithm.LeaderSorter;

namespace tests.LeaderSorter
{
    public class LeaderParser
    {
        readonly Dictionary<string, int> _dictionary = new Dictionary<string, int>();

        public Leader ParseLeader(object[] itemArray, string leaderTypeString)
        {
            Func<string, string> parseString = key =>
            {
                try
                { return itemArray[_dictionary[key]].ToString(); }
                catch
                { return null; }
            };

            Func<string, bool> checkTrait = key => parseString(key) == "Y";
            Func<string, List<string>> parseRequests = key => parseString(key).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            //TODO: make this configurable

            var lastname = parseString("Last Name");
            var firstname = parseString("First Name");
            
            var leaderType = ParseLeaderType(leaderTypeString);


            if (String.IsNullOrEmpty(firstname) && String.IsNullOrEmpty(lastname))
                return null;

            var perferredname = parseString("Preferred Name");
            var program = parseString("Program");

            var traits = new List<Traits>();
            if (checkTrait("Major Directorship"))
                traits.Add(Traits.MajorDirectorship);

            if (checkTrait("Coop in Fall"))
                traits.Add(Traits.CoopInFall);

            if (checkTrait("Returning?"))
                traits.Add(Traits.Returning);

            if (checkTrait("HEM Interview?"))
                traits.Add(Traits.Hem);

            //Requsits and Anti... sigh
            var rawRequests = parseRequests("Requests");
            var rawAntiRequests = parseRequests("Anti-requests");
            return new Leader(lastname, firstname, perferredname, program, leaderType, traits, rawRequests, rawAntiRequests);
        }

        private LeaderType ParseLeaderType(string leaderTypeString)
        {
            if (string.Equals(leaderTypeString, "Huge", StringComparison.InvariantCultureIgnoreCase))
                return LeaderType.Huge;
            if (string.Equals(leaderTypeString, "Big", StringComparison.InvariantCultureIgnoreCase))
                return LeaderType.Big;

            return LeaderType.Undef;
        }


        public void InjectColumnNames(DataRow dataRow)
        {
            var itemAccess = dataRow.ItemArray;
            for (var i = 0; i < itemAccess.Length; i++)
            {
                if (!_dictionary.ContainsKey(itemAccess[i].ToString()))
                    _dictionary.Add(itemAccess[i].ToString(), i);
            }
        }
    }
}
