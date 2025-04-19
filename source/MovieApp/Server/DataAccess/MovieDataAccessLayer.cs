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
		private readonly MovieDBContext _dbContext;
		public MovieDataAccessLayer(IDbContextFactory<MovieDBContext> dbContextFactory)
		{
			_dbContext = dbContextFactory.CreateDbContext();
		}

		public async Task AddMovie(Movie movie)
		{
			try
			{
				await _dbContext.Movies.AddAsync(movie);
				await _dbContext.SaveChangesAsync();
			}
			catch
			{
				throw;
			}
		}

		public async Task<string> DeleteMovie(int movieID)
		{
			try
			{
				Movie? movie = await _dbContext.Movies.FindAsync(movieID);
				if (movie != null)
				{
					_dbContext.Movies.Remove(movie);
					await _dbContext.SaveChangesAsync();
					return movie.PosterPath!;
				}
				return "";
			}
			catch
			{
				throw;
			}
		}

		public async Task<List<Genre>> GetGenre()
		{
			return await _dbContext.Genres.AsNoTracking().ToListAsync();
		}

		public async Task<List<Movie>> GetAllMovies()
		{
			return await _dbContext.Movies.AsNoTracking().ToListAsync();
		}

		public async Task UpdateMovie(Movie movie)
		{
			try
			{
				var movieFromDb = await _dbContext.Movies.FirstOrDefaultAsync(e=>e.MovieId==movie.MovieId);
				if (movieFromDb is not null)
				{
					movieFromDb.Title = movie.Title;
					movieFromDb.Genre=movie.Genre;
					movieFromDb.Duration = movie.Duration;
					movieFromDb.PosterPath = movie.PosterPath;
					movieFromDb.Rating = movie.Rating;
					movieFromDb.Overview = movie.Overview;
					movieFromDb.Language = movie.Language;
					await _dbContext.SaveChangesAsync();
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
