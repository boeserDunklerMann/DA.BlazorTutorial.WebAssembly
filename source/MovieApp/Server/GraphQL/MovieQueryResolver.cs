using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;

namespace MovieApp.Server.GraphQL
{
	/// <ChangeLog>
	/// <Create Datum="17.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public class MovieQueryResolver
	{
		private readonly IMovie _movieService;

		public MovieQueryResolver(IMovie movieService)
		{
			_movieService = movieService;
		}

		[GraphQLDescription("gets the list of genres")]
		public async Task<List<Genre>> GetGenreList()
		{
			return await _movieService.GetGenre();
		}
	}
}