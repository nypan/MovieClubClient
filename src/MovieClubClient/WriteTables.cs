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
