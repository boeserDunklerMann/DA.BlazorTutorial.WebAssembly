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
		const int MaxFileSize = 10 * 1024 * 1024;   // 10 MB
		const string DefaultStatus = "Maximum file size allowed for the image is 10 MB";
		protected string status = DefaultStatus;

		protected override async void OnInitialized()
		{
			await GetavailableGenre();
		}

		private async Task GetavailableGenre()
		{
			var results = await MovieClient.FetchGenreList.ExecuteAsync();
			if (results.Data is not null)
			{
				lstGenre = results.Data.GenreList.Select(x => new Genre
				{
					GenreId = x.GenreId,
					GenreName = x.GenreName
				}).ToList();
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
				Rating = movie.Rating!.Value,
				Genre = movie.Genre,
				Language = movie.Language,
				PosterPath = movie.PosterPath
			};
			await MovieClient.AddMovieData.ExecuteAsync(movieData);
			NavigateToAdminPanel();
		}

		public void NavigateToAdminPanel()
		{
			NavigationManager?.NavigateTo("/");
		}

		protected async Task ViewImageAsync(InputFileChangeEventArgs e)
		{
			if (e.File.Size>MaxFileSize)
			{
				status = $"The file size is {e.File.Size} bytes, which exceeds the max. allowed file size of {MaxFileSize} bytes.";
				return;
			}
			else
				if (!e.File.ContentType.Contains("image"))
			{
				status = "Please upload a valid image file";
				return;
			}
			else
			{
				// everything fine (size and contenttype)
				using StreamReader reader = new(e.File.OpenReadStream(MaxFileSize));

				string format = "image/jpeg";
				var imageFile = await e.File.RequestImageFileAsync(format, 640, 480);
				using Stream fileStream = imageFile.OpenReadStream(MaxFileSize);
				using MemoryStream memoryStream = new();
				await fileStream.CopyToAsync(memoryStream);
				imagePreview = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
				movie.PosterPath = Convert.ToBase64String(memoryStream.ToArray());
				status = DefaultStatus;
			}
		}
	}
}