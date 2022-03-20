using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSVParser
{
    [DelimitedRecord("\t")]
    public class Actor
    {
        public string nconst;

        public string primaryName;

        public string birthYear;

        public string deathYear;

        public string primaryProfession;

        public string knownForTitles;
    }
}
