using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSVParser
{
    [DelimitedRecord("\t")]
    public class Crew
    {
        public string tconst;

        public string directors;

        public string writers;
    }
}
