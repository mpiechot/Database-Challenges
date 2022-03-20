using FileHelpers;

namespace TSVParser
{
    [DelimitedRecord("\t")]
    public class Basic
    {
        public string tconst;

        public string titleType;

        public string primaryTitle;

        public string originalTitle;

        public string isAdult;

        public string startYear;

        public string endYear;

        public string runtimeMinutes;

        public string genres;
    }
}
