using Cards.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Cards.API.Utils
{
    public static class PathBuilder
    {
        public static void CreatePath(string cardsDataFileName)
        {
           DataInitializer.InitialPath(GetPath(cardsDataFileName));
        }

        private static string GetPath(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }
    }
}
