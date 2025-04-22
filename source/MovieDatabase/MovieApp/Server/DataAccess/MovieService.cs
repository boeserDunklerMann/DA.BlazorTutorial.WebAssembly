using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;

namespace MovieApp.Server.DataAccess
{
	/// <ChangeLog>
	/// <Create Datum="22.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public sealed class MovieService(IDbContextFactory<MovieDbContext> dbContextFactory) : IMovie
	{
		private readonly MovieDbContext dbContext = dbContextFactory.CreateDbContext();

		public async Task<List<Genre>> GetGenresAsync()
		{
			return await dbContext.Genres.AsNoTracking().ToListAsync();
		}
	}
}
