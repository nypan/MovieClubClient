using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;

namespace MovieClubClient
{
    internal static class WriteTables
    {
        public static void WriteMovieTable(ICollection<Movie> movies)
        {
            ConsoleTableBuilder.From(MovieTableData(movies))
                .WithTitle($"Search result {movies.Count} movies")
                .ExportAndWriteLine();
        }

        internal static void WriteMemberTable(ICollection<MemberDto> members, string name)
        {
            ConsoleTableBuilder.From(MemberTableData(members))
                .WithTitle($"Team {name} {members.Count} members")
                .ExportAndWriteLine();
        }

        static DataTable MemberTableData(ICollection<MemberDto> members)
        {
            DataTable table = new DataTable();
            table.Columns.Add(nameof(MemberDto.Id), typeof(Guid));
            table.Columns.Add(nameof(MemberDto.FullName), typeof(string));
            foreach (var member in members)
            {
                table.Rows.Add(member.Id,
                               member.FullName);
            }
            return table;

        }


        static DataTable MovieTableData(ICollection<Movie> movies)
        {
            DataTable table = new DataTable();
            table.Columns.Add(nameof(Movie.Id), typeof(string));
            table.Columns.Add(nameof(Movie.Title), typeof(string));
            table.Columns.Add("Genres", typeof(string));
            table.Columns.Add(nameof(Movie.Year), typeof(int));
            table.Columns.Add(nameof(Movie.RuntimeMinutes), typeof(int));
            foreach (var movie in movies)
            {
                var genres = movie.Genres.Aggregate( (i, j) => i + "," + j);
                table.Rows.Add(movie.Id, 
                               movie.Title, 
                               genres, 
                               movie.Year, 
                               movie.RuntimeMinutes);
            }
            
          
            return table;
        }
    }
}
