using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSVParser
{
    public class TSVConverter
    {
        public static void ConvertActors()
        {
            Console.WriteLine("Converting Actors...");
            Convert<Actor>(@"D:\Desktop\Uni\Andere Uni\Datenbanken\postgres\database-files\actors.tsv", action =>
            {
                if (!action.primaryProfession.Contains("primaryProfession"))
                {
                    action.primaryProfession = "{" + action.primaryProfession + "}";
                    action.knownForTitles = "{" + action.knownForTitles + "}";
                }
            });
            Console.WriteLine("Done with Actors!");
        }

        public static void ConvertBasics(string filePath)
        {
            Console.WriteLine("Converting Basics at: " + filePath);
            string[] files = Directory.GetFiles(filePath);

            foreach (var file in files)
            {
                Console.WriteLine("=> Working on: " + file);
                Convert<Basic>(file, action =>
                {
                    if (!action.genres.Contains("genres"))
                    {
                        action.genres = "{" + action.genres + "}";
                        action.runtimeMinutes = action.runtimeMinutes.Equals(@"\N") ? "NULL" : action.runtimeMinutes;
                    }
                });
            }
            Console.WriteLine("Done with Basics!");
        }

        public static void ConvertCrews()
        {
            Console.WriteLine("Converting Crews...");
            Convert<Crew>(@"D:\Desktop\Uni\Andere Uni\Datenbanken\postgres\database-files\crew.tsv", action =>
            {
                if (!action.writers.Contains("writers"))
                {
                    action.writers = action.writers != @"\N" ? "{" + action.writers + "}" : action.writers;
                    action.directors = action.directors != @"\N" ? "{" + action.directors + "}" : action.directors;
                }
            });
            Console.WriteLine("Done with Crews!");
        }

        public static void ConvertPrincipals(string filePath)
        {
            Console.WriteLine("Converting Principals at: " + filePath);
            string[] files = Directory.GetFiles(filePath);

            foreach (var file in files)
            {
                Console.WriteLine("=> Working on: " + file);
                Convert<Principal>(file, action =>
                {
                    if (!action.characters.Contains("characters"))
                    {
                        action.characters = action.characters != @"\N" ? "{" + action.characters + "}" : action.characters;
                    }
                });
            }

            Console.WriteLine("Done with Principals!");
        }

        private static void Convert<T>(string path, Action<T> action) where T : class
        {
            var engine = new FileHelperEngine<T>();

            var result = engine.ReadFile(path);

            foreach (var item in result)
            {
                action(item);
            }

            engine.WriteFile(path, result);
        }
    }
}
