using MovieClubClient;
class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        var baseUrl  = "https://app-dev-movie-api.azurewebsites.net";
        client.BaseAddress = new Uri(baseUrl);
        var movieClient = new MovieClubApiClient(client);
        var movieSearch = new MovieSearchDto { Title = "Shining" };
        var movies = await movieClient.SearchAsync(movieSearch);
        foreach (var movie in movies)
        {
            Console.WriteLine(movie.Title);

        }
    }
}