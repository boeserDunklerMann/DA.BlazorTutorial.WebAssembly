using MovieApp.Server.Models;

namespace MovieApp.Server.Interfaces
{
	/// <ChangeLog>
	/// <Create Datum="22.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public interface IMovie
	{
		Task<List<Genre>> GetGenresAsync();
		Task AddMovieAsync(Movie movie);
	}
}