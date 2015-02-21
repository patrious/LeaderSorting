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
            Func<string, bool> parseTrait = key => itemArray[_dictionary[key]].ToString() == "Y";

            //TODO: make this configurable

            var lastname = parseString("Last Name");
            var firstname = parseString("First Name");
            var perferredname = parseString("Preferred Name");
            var program = parseString("Program");
            
            var traits = new List<Traits>();
            if(parseTrait("Major Directorship"))
                traits.Add(Traits.MajorDirectorship);

            if(parseTrait("Coop in Fall"))
                traits.Add(Traits.CoopInFall);

            if(parseTrait("Returning?"))
                traits.Add(Traits.Returning);

            //Requsits and Anti... sigh

            return new Leader(lastname, firstname, perferredname, program, traits);
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
