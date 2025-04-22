using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;

namespace MovieApp.Server.GraphQL
{
	/// <ChangeLog>
	/// <Create Datum="22.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public sealed class MovieMutationResolver(IConfiguration cfg, IMovie movieService, IWebHostEnvironment webHostEnvironment)
	{
		public record AddMoviePayload(Movie Movie);
		private readonly string posterFolderPath = System.IO.Path.Combine(webHostEnvironment.ContentRootPath, "Poster");

		[GraphQLDescription("Add new movie data.")]
		public AddMoviePayload AddMovie(Movie movie)
		{
			if (!string.IsNullOrEmpty(movie.PosterPath))
			{
				string filename = Guid.NewGuid() + ".jpg";
				string fullPath = System.IO.Path.Combine(posterFolderPath, filename);
				byte[] imageBytes = Convert.FromBase64String(movie.PosterPath);
				File.WriteAllBytes(fullPath, imageBytes);
				movie.PosterPath = filename;
			}
			else
				movie.PosterPath = cfg["DefaultPoster"];
			movieService.AddMovieAsync(movie);
			return new AddMoviePayload(movie);
		}
	}
}