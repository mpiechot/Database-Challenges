// See https://aka.ms/new-console-template for more information
using FileHelpers;
using TSVParser;

public class Program
{
    private static TSVConverter tsvConverter = new TSVConverter();
    private const string DirectoryPath = @"D:\Desktop\Uni\Andere Uni\Datenbanken\postgres\database-files\";
    private static string[] FileNames = new string[] { "basics", "principals", "titles"};

    public static void Main(string[] args)
    {
        Console.WriteLine("Starting to work on Conversions:");

        TSVConverter.ConvertActors();
        TSVConverter.ConvertCrews();

        foreach (string file in FileNames)
        {
            Console.WriteLine("Splitting: " + file);
            TSVSplitter.Split(DirectoryPath + file + ".tsv");

            Console.WriteLine("Converting: " + file);
            switch (file)
            {
                case "basics": TSVConverter.ConvertBasics(Path.Combine(DirectoryPath, file)); break;
                case "principals": TSVConverter.ConvertPrincipals(Path.Combine(DirectoryPath, file)); break;
            }

            Console.WriteLine("Merging: " + file);
            TSVSplitter.Merge(Path.Combine(DirectoryPath, file), file + ".tsv");
        }

        Console.WriteLine("Finished");
    }
}