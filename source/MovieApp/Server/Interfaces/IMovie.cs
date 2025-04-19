using MovieApp.Server.Models;

namespace MovieApp.Server.Interfaces
{
	/// <ChangeLog>
	/// <Create Datum="17.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public interface IMovie
	{
		Task<List<Genre>> GetGenre();
		Task AddMovie(Movie movie);
		Task<List<Movie>> GetAllMovies();
		Task UpdateMovie(Movie movie);
		Task<string> DeleteMovie(int movieID);
	}
}