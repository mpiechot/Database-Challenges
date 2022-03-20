namespace TSVParser
{
    /// <summary>
    /// TSVSplitter splits up large tsv files to smaller pieces that can be edited via Texteditor.
    /// Use <see cref="TSVMerger"/> to merge them back togehter.
    /// </summary>
    public static class TSVSplitter
    {
        private static int linesPerFile = 900000;

        private static string? firstLine = string.Empty;
        private static List<string> lines = new List<string>();
        private static bool directoryCreated = false;

        public static void Merge(string directoryPath, string fileName)
        {
            try
            {
                var files = Directory.GetFiles(directoryPath);
                bool firstLineAdded = false;

                foreach (var file in files)
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        firstLine = sr.ReadLine();

                        if (firstLine == null)
                        {
                            throw new ArgumentNullException("First line is null!");
                        }

                        if (!firstLineAdded)
                        {
                            lines.Add(firstLine);
                            firstLineAdded = true;
                        }

                        string? line = string.Empty;
                        while ((line = sr.ReadLine()) != null)
                        {
                            line = line.Replace("\\\"", string.Empty);
                            line = line.Replace("\"", string.Empty);
                            line = line.Replace("NULL", "\\N");
                            lines.Add(line);
                        }
                    }

                    using (StreamWriter sw = new StreamWriter(Path.Combine(directoryPath, fileName), true))
                    {
                        foreach (var line in lines)
                        {
                            sw.WriteLine(line);
                        }

                        sw.Close();
                    }

                    lines.Clear();
                }
                Reset();
            }
            catch (Exception _)
            {

                throw;
            }
        }

        public static void Split(string filePath)
        {
            try
            {
                int num = 0;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    firstLine = sr.ReadLine();

                    if (firstLine == null)
                    {
                        throw new ArgumentNullException("First line is null!");
                    }

                    lines.Add(firstLine);
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                        if (lines.Count == linesPerFile)
                        {
                            SaveFile(filePath, num);
                            num++;
                        }
                    }
                }

                SaveFile(filePath, num);
                Reset();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Reset()
        {
            lines.Clear();
            firstLine = string.Empty;
            directoryCreated = false;
        }

        private static void SaveFile(string path, int num)
        {
            string? directoryPath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));

            if (!directoryCreated)
            {
                directoryCreated = true;
                Directory.CreateDirectory(directoryPath);
            }

            StreamWriter sw = new StreamWriter(Path.Combine(directoryPath, Path.GetFileNameWithoutExtension(path) + $"-{num}.tsv"));

            foreach (string line in lines)
            {
                sw.WriteLine(line);
            }

            sw.Close();
            lines.Clear();
            lines.Add(firstLine);
        }
    }
}
