using MovieClubClient;
using System.CommandLine;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static string baseUrl = "https://app-dev-movie-api.azurewebsites.net";
    private static MovieClubApiClient movieClient;

    static int Main(string[] args)
    {
        client.BaseAddress = new Uri(baseUrl);
        movieClient = new MovieClubApiClient(client);

        var searchTitleOption = new Option<string>(
        name: "--title",
        description: "Search movies by title");

        var searchGenreOption = new Option<string>(
        name: "--genre",
        description: "Search movies by genre");

        var searchYearFromOption = new Option<int?>(
        name: "--yearFrom",
        description: "Search movies by release year from");

        var searchYearToOption = new Option<int?>(
        name: "--yearTo",
        description: "Search movies by release year to");

        var searchRuntimeMinutesFromOption = new Option<int?>(
        name: "--runtimeMinutesFrom",
        description: "Search movies by runtime minutes from");

        var searchRuntimeMinutesToOption = new Option<int?>(
        name: "--runtimeMinutesTo",
        description: "Search movies by runtime minutes to");


        var rootCommand = new RootCommand("Example of Console client to Movie API");
        var searchCommand = new Command(name : "search", description: "Search movies")
        {
            searchTitleOption,
            searchGenreOption,
            searchYearFromOption,
            searchYearToOption,
            searchRuntimeMinutesFromOption,
            searchRuntimeMinutesToOption,
        };

        rootCommand.AddCommand(searchCommand);

        searchCommand.SetHandler(async (title,genre,yearFrom,yearTo,runtimeMinutesFrom,runtimeMinutesTo) =>
        {
            await SearchMovieByTitle(title,genre,yearFrom,yearTo,runtimeMinutesFrom,runtimeMinutesTo);
        },
        searchTitleOption,
        searchGenreOption,
        searchYearFromOption,
        searchYearToOption,
        searchRuntimeMinutesFromOption,
        searchRuntimeMinutesToOption);
  
        return rootCommand.InvokeAsync(args).Result;
    }

    private static async Task SearchMovieByTitle(
        string title,
        string genre,
        int? yearFrom,
        int? yearTo,
        int? runtimeMinutesFrom,
        int? runtimeMinutesTo)
    {
        var movieSearch = new MovieSearchDto { Title = title };
        var movies = await movieClient.SearchAsync(movieSearch);
        foreach (var movie in movies)
        {
            Console.WriteLine($"Id : {movie.Id}  Title : {movie.Title}");
        }
    }
}