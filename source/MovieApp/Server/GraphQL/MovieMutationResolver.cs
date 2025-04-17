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

		readonly IWebHostEnvironment _hostingEnvironment;
		readonly IMovie _movieService;
		readonly IConfiguration _config;
		readonly string posterFolderPath = string.Empty;

		public MovieMutationResolver(IConfiguration config, IMovie movieService, IWebHostEnvironment hostingEnvironment)
		{
			_config = config;
			_movieService = movieService;
			_hostingEnvironment = hostingEnvironment;
			posterFolderPath = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Poster");
		}

		[GraphQLDescription("Add new movie data.")]
		public AddMoviePayload AddMovie(Movie movie)
		{
			if (!string.IsNullOrEmpty(movie.PosterPath))
			{
				string fileName = Guid.NewGuid() + ".jpg";
				string fullPath = System.IO.Path.Combine(posterFolderPath, fileName);

				byte[] imageBytes = Convert.FromBase64String(movie.PosterPath);
				File.WriteAllBytes(fullPath, imageBytes);

				movie.PosterPath = fileName;
			}
			else
			{
				movie.PosterPath = _config["DefaultPoster"];
			}

			_movieService.AddMovie(movie);

			return new AddMoviePayload(movie);
		}
	}
}
