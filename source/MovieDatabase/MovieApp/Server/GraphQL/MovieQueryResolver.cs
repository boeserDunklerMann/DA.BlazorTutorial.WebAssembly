using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;

namespace MovieApp.Server.GraphQL
{
	/// <ChangeLog>
	/// <Create Datum="22.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public sealed class MovieQueryResolver(IMovie movie)
	{
		private readonly IMovie movieService = movie;

		[GraphQLDescription("Gets the list of genres.")]
		public async Task<List<Genre>> GetGenreListAsync()
		{
			return await movieService.GetGenresAsync();
		}
	}
}