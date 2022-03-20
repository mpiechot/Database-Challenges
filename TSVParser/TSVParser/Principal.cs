using FileHelpers;

namespace TSVParser
{
    [DelimitedRecord("\t")]
    public class Principal
    {
        public string tconst;

        public string ordering;

        public string nconst;

        public string category;

        public string job;

        public string characters;
    }
}
