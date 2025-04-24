using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MovieApp.Client.GraphQLAPIClient;
using MovieApp.Server.Models;

namespace MovieApp.Client.Pages
{
	/// <ChangeLog>
	/// <Create Datum="22.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public class AddEditMovieBase : ComponentBase
	{
		[Inject]
		private NavigationManager NavigationManager { get; set; } = default!;
		[Inject]
		private MovieClient MovieClient { get; set; } = default!;
		[Parameter]
		public int MovieID { get; set; }
		public string Title = "Add";
		public Movie movie = new();
		public List<Genre>? genres = [];
		public string? imagePreview;
		const int MaxFileSize = 10 * 1024 * 1024;   // 10 MB
		const string defaultStatus = "Maximum size allowed for the image is 10 Megs.";
		public string status = defaultStatus;
		protected override async void OnInitialized()
		{
			await GetGenresAsync();
		}
		private async Task GetGenresAsync()
		{
			var results = await MovieClient.FetchGenreList.ExecuteAsync();
			if (results.Data is not null)
			{
				genres = results.Data.GenreList.Select(g => new Genre
				{
					GenreId = g.GenreId,
					GenreName = g.GenreName
				}).ToList();
			}
		}
		public async Task SaveMovieAsync()
		{
			MovieInput movieData = new()
			{
				MovieId = movie.MovieId,
				Title = movie.Title,
				Overview = movie.Overview,
				Duration = movie.Duration,
				Rating = movie.Rating,
				Genre = movie.Genre,
				Language = movie.Language,
				PosterPath = movie.PosterPath
			};
			await MovieClient.AddMovieData.ExecuteAsync(movieData);
			NavigateToAdminPanel();
		}
		public void NavigateToAdminPanel() => NavigationManager?.NavigateTo("/");
		public async Task ViewImage(InputFileChangeEventArgs args)
		{
			if (args.File.Size > MaxFileSize)
			{
				status = $"The file size is {args.File.Size} bytes, which is AddMovieDataResult than the allowed size of {MaxFileSize} bytes.";
				return;
			}
			else if (!args.File.ContentType.Contains("image", StringComparison.OrdinalIgnoreCase))
			{
				status = "Only images are allowed";
				return;
			}
			else
			{
				using StreamReader reader = new(args.File.OpenReadStream(MaxFileSize));
				string format = "image/jpeg";
				var imageFile = await args.File.RequestImageFileAsync(format, 640, 480);
				using Stream fileStream = imageFile.OpenReadStream(MaxFileSize);
				using MemoryStream memStream = new();
				await fileStream.CopyToAsync(memStream);
				imagePreview = $"data:{format};base64,{Convert.ToBase64String(memStream.ToArray())}";
				movie.PosterPath = Convert.ToBase64String(memStream.ToArray());
				status = defaultStatus;
			}
		}
	}
}