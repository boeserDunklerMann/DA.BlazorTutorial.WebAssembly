using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MovieApp.Client.GraphQLAPIClient;
using MovieApp.Server.Models;

namespace MovieApp.Client.Pages
{
	/// <ChangeLog>
	/// <Create Datum="17.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public class AddEditMovieBase : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;

		[Inject]
		MovieClient MovieClient { get; set; } = default!;

		[Parameter]
		public int MovieID { get; set; }

		protected string Title = "Add";
		public Movie movie = new();
		protected List<Genre>? lstGenre = new();
		protected string? imagePreview;
		const int MaxFileSize = 10 * 1024 * 1024; // 10 MB
		const string DefaultStatus = "Maximum size allowed for the image is 10 MB";
		protected string status = DefaultStatus;

		protected override async void OnInitialized()
		{
			await GetAvailableGenre();
		}

		private async Task GetAvailableGenre()
		{
			var results = await MovieClient.FetchGenreList.ExecuteAsync();
			if (results.Data is not null)
			{
				lstGenre = results.Data.GenreList.Select(g => new Genre
				{ GenreId = g.GenreId, GenreName = g.GenreName }).ToList();
			}
		}

		protected async Task SaveMovieAsync()
		{
			MovieInput movieData = new()
			{
				MovieId = movie.MovieId,
				Title = movie.Title,
				Overview = movie.Overview,
				Duration = movie.Duration,
				Rating = movie.Rating ?? 0,
				Genre = movie.Genre,
				Language = movie.Language,
				PosterPath = movie.PosterPath
			};
			if (movieData.MovieId != 0)
				await MovieClient.EditMovieData.ExecuteAsync(movieData);
			else
				await MovieClient.AddMovieData.ExecuteAsync(movieData);
			NavigateToAdminPanel();
		}

		protected void NavigateToAdminPanel()
		{
			NavigationManager?.NavigateTo("/admin/movies");
		}
		protected async Task ViewImage(InputFileChangeEventArgs e)
		{
			if (e.File.Size > MaxFileSize)
			{
				status = $"The file size is {e.File.Size} bytes, this is more than the allowed limit of {MaxFileSize} bytes.";
				return;
			}
			else if (!e.File.ContentType.Contains("image"))
			{
				status = "Please upload a valid image file";
				return;
			}
			else
			{
				using var reader = new StreamReader(e.File.OpenReadStream(MaxFileSize));

				var format = "image/jpeg";
				var imageFile = await e.File.RequestImageFileAsync(format, 640, 480);

				using var fileStream = imageFile.OpenReadStream(MaxFileSize);
				using var memoryStream = new MemoryStream();
				await fileStream.CopyToAsync(memoryStream);

				imagePreview = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
				movie.PosterPath = Convert.ToBase64String(memoryStream.ToArray());

				status = DefaultStatus;
			}
		}
		protected override async Task OnParametersSetAsync()
		{
			if (MovieID != 0)
			{
				Title = "Edit";

				MovieFilterInput movieFilterInput = new()
				{
					MovieId = new()
					{
						Eq = MovieID
					}
				};
				var response = await MovieClient.FilterMovieByID.ExecuteAsync(movieFilterInput);
				var moviedata = response?.Data?.MovieList[0];
				if (moviedata is not null)
				{
					movie.MovieId = moviedata.MovieId;
					movie.Title = moviedata.Title;
					movie.Genre = moviedata.Genre;
					movie.Duration = moviedata.Duration;
					movie.PosterPath = moviedata.PosterPath;
					movie.Rating = moviedata.Rating;
					movie.Language = moviedata.Language;
					imagePreview = $"/Poster/{movie.PosterPath}";
				}
			}
		}
	}
}