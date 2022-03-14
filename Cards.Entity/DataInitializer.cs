using System.IO;

namespace Cards.Entity
{
    public static class DataInitializer
    {
        private static string CardsDataPath { get; set; }

        public static void InitialPath(string cardsDataPath)
        {
            CardsDataPath = cardsDataPath;
        }

        public static string GetCardsContent()
        {
            return File.ReadAllText(CardsDataPath);
        }
    }
}
