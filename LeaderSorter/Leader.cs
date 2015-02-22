using System;
using System.Collections.Generic;
using System.Text;
using Extensions;

namespace GeneticAlgorithm.LeaderSorter
{
    [Serializable]
    public enum Traits
    {
        MajorDirectorship,
        CoopInFall,
        Returning,
    }

    [Serializable]
    public enum LeaderType
    {
        Big,
        Huge,
        Undef
    }

    [Serializable]
    public class Leader
    {
        private readonly string _firstname;
        private readonly string _lastname;
        private readonly string _perferredname;
        private string _program;
        private List<Traits> _traits;

        public Guid LeaderId;
        public LeaderType LeaderType;
        public List<Guid> BlackList = new List<Guid>();
        public List<Guid> WhiteList = new List<Guid>();

        public string PublicName
        {
            get
            {
                return !String.IsNullOrEmpty(_perferredname)
                    ? String.Format("{0} {1}", _perferredname, _lastname)
                    : String.Format("{0} {1}", _firstname, _lastname);
            }
        }

        public List<string> RawBlackList;
        public List<string> RawWhiteList;


        public Leader(string lastname, string firstname, string perferredname, string program, LeaderType leaderType, List<Traits> traits, List<string> rawWhiteList, List<string> rawBlackList)
        {
            LeaderId = Guid.NewGuid();
            this.LeaderType = leaderType;
            _lastname = lastname;
            _firstname = firstname;
            _perferredname = perferredname;
            _program = program;
            _traits = traits;
            RawBlackList = rawBlackList;
            RawWhiteList = rawWhiteList;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1},", _firstname, _lastname);
        }

        public string PrettyPrint()
        {

            var blacklistnames = new StringBuilder();
            BlackList.ForEach(x => blacklistnames.AppendFormat("{0} ", x.ToString()));
            var whitelistnames = new StringBuilder();
            WhiteList.ForEach(x => whitelistnames.AppendFormat("{0} ", x.ToString()));
            return string.Format("{0} \t: F - {1} : E - {2}", this, whitelistnames, blacklistnames);
        }
        
    }

    public static class LeaderExtensions
    {
        public static Leader GenerateLeader(this Leader leader)
        {
            throw new NotImplementedException("Need to fix code");
            //var traits = GenerateTraits();
            //return new Leader(NameGen.GetLastName(), NameGen.GetFirstName(), traits);
        }

        private static List<Traits> GenerateTraits(this Leader leader)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            var traits = Enum.GetValues(typeof(Traits));
            var retVal = new List<Traits>
            {
                (Traits) traits.GetValue(rand.Next(traits.Length)),
                (Traits) traits.GetValue(rand.Next(traits.Length))
            };
            return retVal;
        }
    }
}
