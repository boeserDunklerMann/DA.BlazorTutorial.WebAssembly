using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;

namespace MovieApp.Server.GraphQL
{
	/// <ChangeLog>
	/// <Create Datum="17.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public class MovieMutationResolver
	{
		public record AddMoviePayload(Movie movie);

		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly IMovie _movieService;
		private readonly IConfiguration _config;
		private readonly string posterFolderPath = string.Empty;

		public MovieMutationResolver(IConfiguration config, IMovie movieService, IWebHostEnvironment environment)
		{
			_hostingEnvironment = environment;
			_config = config;
			_movieService = movieService;
			posterFolderPath = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Poster");
		}

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
				movie.PosterPath = _config["DefaultPoster"];
			_movieService.AddMovie(movie);
			return new AddMoviePayload(movie);
		}
	}
}