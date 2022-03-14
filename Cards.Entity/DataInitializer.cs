using System.IO;

namespace Cards.Entity
{
    /// <summary>
    /// Static class for initial path of data file and write and read contents from data file.
    /// </summary>
    public static class DataInitializer
    {
        public static string CardsDataPath { get; set; }

        public static void InitialPath(string cardsDataPath)
        {
            CardsDataPath = cardsDataPath;
        }

        public static string GetContents(string path)
        {
            return File.ReadAllText(path);
        }

        public static void WriteContents(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }
    }
}
