using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.LeaderSorter
{
    public class LeaderParser
    {
        readonly Dictionary<string, int> _dictionary = new Dictionary<string, int>();
        public Leader ParseLeader(object[] itemArray)
        {
            Func<string, string> parseString = key => itemArray[_dictionary[key]].ToString();
            Func<string, bool> checkTrait = key => parseString(key) == "Y";
            Func<string, List<string>> parseRequests = key => parseString(key).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            //TODO: make this configurable

            var lastname = parseString("Last Name");
            var firstname = parseString("First Name");


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

            //Requsits and Anti... sigh
            var rawRequests = parseRequests("Requests");
            var rawAntiRequests = parseRequests("Anti-requests");
            return new Leader(lastname, firstname, perferredname, program, traits, rawRequests, rawAntiRequests);
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
