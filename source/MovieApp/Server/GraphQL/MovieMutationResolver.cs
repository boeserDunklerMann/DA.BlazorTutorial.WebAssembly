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

		[GraphQLDescription("Edit an existing movie-data.")]
		public async Task<AddMoviePayload> EditMovie(Movie movie)
		{
			if (CheckBase64String(movie.PosterPath))
			{
				string fileName = Guid.NewGuid() + ".jpg";
				string fullPath = System.IO.Path.Combine(posterFolderPath, fileName);
				byte[] imageBytes = Convert.FromBase64String(movie.PosterPath!);    // we can assume that this prop is not null and valid base64
				File.WriteAllBytes(fullPath, imageBytes);
			}
			await _movieService.UpdateMovie(movie);
			return new AddMoviePayload(movie);
		}

		[GraphQLDescription("Delete a movie")]
		public async Task<int> DeleteMovie(int movieID)
		{
			string posterFilename = await _movieService.DeleteMovie(movieID);
			if (string.IsNullOrEmpty(posterFilename) && posterFilename != _config["DefaulPoster"])
			{
				string fullPath = System.IO.Path.Combine(posterFolderPath, posterFilename);
				if (File.Exists(fullPath))
				{
					File.Delete(fullPath);
				}
			}
			return movieID;
		}

		private static bool CheckBase64String(string? inputBase64)
		{
			if (inputBase64 != null)
			{
				Span<byte> buffer = new(new byte[inputBase64.Length]);
				return Convert.TryFromBase64String(inputBase64, buffer, out int bytesParsed);
			}
			return false;
		}
	}
}
