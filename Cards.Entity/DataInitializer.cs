using System.IO;

namespace Cards.Entity
{
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
