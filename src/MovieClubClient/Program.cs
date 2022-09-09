using MovieClubClient;
using System.CommandLine;
using System.Xml.Linq;

/// <summary>
///  Delta version
/// </summary>
class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static string baseUrl = "https://app-dev-movie-api.azurewebsites.net";
    private static MovieClubApiClient movieClient;
    private static string Version = "Delta version";
    static int Main(string[] args)
    {
        Console.WriteLine($"Api {Version} {baseUrl}");
        client.BaseAddress = new Uri(baseUrl);
        movieClient = new MovieClubApiClient(client);
        var rootCommand = new RootCommand("Example of Console client to Movie API");

        SearchCommand(rootCommand);
        TeamCommand(rootCommand);
        GetMemberCommand(rootCommand);
        AddMemberCommand(rootCommand);
        DeleteMemberCommand(rootCommand);
        return rootCommand.InvokeAsync(args).Result;

    }




    #region  Arguments command

    private static void DeleteMemberCommand(RootCommand rootCommand)
    {
        var memberIdOption = new Option<Guid>(
        name: "--id",
        description: "Member id (Guid)");

        var deleteMemberCommand = new Command(name: "deletemember", description: "Remove team member")
        {
            memberIdOption
        };

        rootCommand.AddCommand(deleteMemberCommand);

        deleteMemberCommand.SetHandler(async (id) =>
        {
            await DeleteMemberToTeam(id);
        },
        memberIdOption);

    }

    

    private static void AddMemberCommand(RootCommand rootCommand)
    {
        var fullNameOption = new Option<string>(
         name: "--fullname",
         description: "Fullname of member");

        var teamOption = new Option<string>(
         name: "--team",
         description: "Team name");

        var addMemberCommand = new Command(name: "addmember", description: "Add team member")
        {
            fullNameOption,
            teamOption,
        };

        rootCommand.AddCommand(addMemberCommand);

        addMemberCommand.SetHandler(async (fullname,team) =>
        {
            await AddMemberToTeam(fullname,team);
        },
        fullNameOption,teamOption);

    }

    
    private static void GetMemberCommand(RootCommand rootCommand)
    {
        var memberIdOption = new Option<Guid>(
        name: "--id",
        description: "Member Id (Guid)");
        var getMemberCommand = new Command(name: "getmember", description: "Get team member")
        {
            memberIdOption
        };
        rootCommand.AddCommand(getMemberCommand);
        getMemberCommand.SetHandler(async (id) =>
        {
            await GetMemberById(id);
        },
        memberIdOption);

    }



    private static void TeamCommand(RootCommand rootCommand)
    {
        var teamNameOption = new Option<string>(
        name: "--name",
        description: "Get team members");
        var teamCommand = new Command(name: "team", description: "Get team members")
        {
            teamNameOption
        };
        rootCommand.AddCommand(teamCommand);
        teamCommand.SetHandler(async (name) =>
        {
            await GetMembersInTeam(name);
        },
        teamNameOption);


    }


    private static void SearchCommand(RootCommand rootCommand)
    {
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


        var searchCommand = new Command(name: "search", description: "Search movies")
        {
            searchTitleOption,
            searchGenreOption,
            searchYearFromOption,
            searchYearToOption,
            searchRuntimeMinutesFromOption,
            searchRuntimeMinutesToOption,
        };

        rootCommand.AddCommand(searchCommand);

        searchCommand.SetHandler(async (title, genre, yearFrom, yearTo, runtimeMinutesFrom, runtimeMinutesTo) =>
        {
            await SearchMovieByTitle(title, genre, yearFrom, yearTo, runtimeMinutesFrom, runtimeMinutesTo);
        },
        searchTitleOption,
        searchGenreOption,
        searchYearFromOption,
        searchYearToOption,
        searchRuntimeMinutesFromOption,
        searchRuntimeMinutesToOption);
    }

    #endregion

    #region API call
    private static async Task DeleteMemberToTeam(Guid id)
    {
        var result = await movieClient.RemoveAsync(id);
        Console.WriteLine($"Removed Member Id {id} result {result}");
    }
    private static async Task AddMemberToTeam(string fullname, string team)
    {
        var addmember = new NewMemberDto();
        addmember.FullName = fullname;
        addmember.Team = team;
        addmember.FavoriteMovies = new string[] { }; 
        var memberId = await movieClient.AddAsync(addmember);
        Console.WriteLine($"Added Member Id {memberId}");
    }

    private static async Task GetMemberById(Guid id)
    {
        var member = await movieClient.MembersAsync(id);
        WriteTables.WriteMemberWithMovies(member);
    }
    private static async Task GetMembersInTeam(string name)
    {
        var members = await movieClient.TeamAsync(name);
        WriteTables.WriteMemberTable(members,name);

    }

    private static async Task SearchMovieByTitle(
        string title,
        string genre,
        int? yearFrom,
        int? yearTo,
        int? runtimeMinutesFrom,
        int? runtimeMinutesTo)
    {
        var movieSearch = new MovieSearchDto
        { 
            Title = title,
            Genre = genre,
            YearFrom = yearFrom,
            YearTo = yearTo,
            RuntimeMinutesFrom = runtimeMinutesFrom,
            RuntimeMinutesTo = runtimeMinutesTo,
        };
        var movies = await movieClient.SearchAsync(movieSearch);
        WriteTables.WriteMovieTable(movies);
    }
    #endregion
}