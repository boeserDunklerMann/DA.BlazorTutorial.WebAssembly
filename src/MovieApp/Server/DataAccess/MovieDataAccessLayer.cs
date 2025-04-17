using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;

namespace MovieApp.Server.DataAccess
{
	/// <ChangeLog>
	/// <Create Datum="17.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public class MovieDataAccessLayer : IMovie
	{
		private readonly MovieDbContext _dbContext;
		public MovieDataAccessLayer(IDbContextFactory<MovieDbContext> dbContext)
		{
			_dbContext = dbContext.CreateDbContext();
		}

		public async Task<List<Genre>> GetGenre()
		{
			return await _dbContext.Genres.AsNoTracking().ToListAsync();
		}
	}
}
