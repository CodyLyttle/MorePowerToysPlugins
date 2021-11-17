using System;
using ConsoleApp1;
using Microsoft.PowerToys.Run.Plugin.Google;

while (true)
{
    switch (GetNextAction())
    {
        case SearchAction.PublicSearch:
            Search(false);
            break;
        case SearchAction.PrivateSearch:
            Search(true);
            break;
        case SearchAction.Quit:
            return;
        default:
            return;
    }
}

static SearchAction GetNextAction()
{
    Console.WriteLine("1 - Public Search");
    Console.WriteLine("2 - Private Search");
    Console.WriteLine("or quit");

    return Console.ReadLine() switch
    {
        "1" => SearchAction.PublicSearch,
        "2" => SearchAction.PrivateSearch,
        _ => SearchAction.Quit
    };
}

static void Search(bool usePrivateBrowsing)
{
    Console.WriteLine("Type your search query");
    string searchTerm = Console.ReadLine() ?? string.Empty;
    GoogleSearch.OpenInEdge(searchTerm, usePrivateBrowsing);
}