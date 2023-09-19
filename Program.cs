using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Lab2;

public class Program
{
    static void Main(string[] args)
    {
        string csvFilePath = @"C:\Users\elias\Server Side Web Prog\Lab1\videogames.csv";

        Dictionary<string, List<VideoGame>> platformGames = ReadVideoGamesFromCsv(csvFilePath);

        if (platformGames.Count > 0)
        {
            PublisherData(platformGames);
            GenreData(platformGames);

            foreach (var platform in platformGames.Keys)
            { 
                List<VideoGame> topGames = GetTopGamesByGlobalSales(platformGames, platform, 5);

                Console.WriteLine($"\nTop 5 games for platform {platform} by Global Sales:");
                foreach(var game in topGames) 
                {
                    Console.WriteLine(game);
                }
            }
        }
        else
        {
            Console.WriteLine("No video game data found.");
        }
    }
    // Read video game data from a CSV file and return a List of VideoGame objects
    static Dictionary<string, List<VideoGame>> ReadVideoGamesFromCsv(string filePath)
    {
        Dictionary<string, List<VideoGame>> platformGames = new Dictionary<string, List<VideoGame>>();

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                // Skip the header row
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var videoGame = new VideoGame
                    {
                        Name = values[0],
                        Platform = values[1],
                        Year = int.Parse(values[2]),
                        Genre = values[3],
                        Publisher = values[4],
                        NA_Sales = double.Parse(values[5], CultureInfo.InvariantCulture),
                        EU_Sales = double.Parse(values[6], CultureInfo.InvariantCulture),
                        JP_Sales = double.Parse(values[7], CultureInfo.InvariantCulture),
                        Other_Sales = double.Parse(values[8], CultureInfo.InvariantCulture),
                        Global_Sales = double.Parse(values[9], CultureInfo.InvariantCulture)
                    };

                    if (platformGames.ContainsKey(videoGame.Platform))
                    {
                        platformGames[videoGame.Platform].Add(videoGame);
                    }
                    else
                    {
                        // Create a new entry for the platform
                        platformGames[videoGame.Platform] = new List<VideoGame> { videoGame };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return platformGames;
    }

    static List<VideoGame> GetTopGamesByGlobalSales(Dictionary<string, List<VideoGame>> platformGames, string platform, int count)
    {
        if (platformGames.ContainsKey(platform))
        {
            return platformGames[platform]
                .OrderByDescending(game => game.Global_Sales)
                .Take(count)
                .ToList();
        }
        else
        {
            Console.WriteLine($"No data found for platform: {platform}");
            return new List<VideoGame>();
        }

    }
  


    static void PublisherData(Dictionary<string, List<VideoGame>> platformGames)
    {
        Console.Write("Enter the name of the publisher: ");
        string selectedPublisher = Console.ReadLine();

        List<VideoGame> allGames = platformGames.Values.SelectMany(games => games).ToList();

        var publisherGames = allGames.Where(game => game.Publisher == selectedPublisher).ToList();

        if (publisherGames.Any())
        {
            publisherGames = publisherGames.OrderBy(game => game.Name).ToList(); // Sort using LINQ
            double percentage = (double)publisherGames.Count / allGames.Count * 100;

            Console.WriteLine($"Games published by {selectedPublisher}:");
            foreach (var game in publisherGames)
            {
                Console.WriteLine(game);
            }

            Console.WriteLine($"\nOut of {allGames.Count} games, {publisherGames.Count} are published by {selectedPublisher}, which is {percentage:F2}%.");
        }
        else
        {
            Console.WriteLine($"No games found for the publisher: {selectedPublisher}");
        }
    }


    static void GenreData(Dictionary<string, List<VideoGame>> platformGames)
    {
        Console.Write("Enter the name of the genre: ");
        string selectedGenre = Console.ReadLine();

        List<VideoGame> allGames = platformGames.Values.SelectMany(games => games).ToList();


        var genreGames = allGames.Where(game => game.Genre == selectedGenre).ToList();

        if (genreGames.Any())
        {
            genreGames = genreGames.OrderBy(game => game.Name).ToList(); // Sort using LINQ
            double percentage = (double)genreGames.Count / allGames.Count * 100;

            Console.WriteLine($"\nGames of the genre '{selectedGenre}':");
            foreach (var game in genreGames)
            {
                Console.WriteLine(game);
            }

            Console.WriteLine($"\nOut of {allGames.Count} games, {genreGames.Count} are of the genre '{selectedGenre}', which is {percentage:F2}%.");
        }
        else
        {
            Console.WriteLine($"No games found for the genre: {selectedGenre}");
        }

    }
}