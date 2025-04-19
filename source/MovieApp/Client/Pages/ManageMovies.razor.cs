using Microsoft.AspNetCore.Components;
using MovieApp.Client.GraphQLAPIClient;
using MovieApp.Server.Models;

namespace MovieApp.Client.Pages
{
	/// <ChangeLog>
		/// <Create Datum="19.04.2025" Entwickler="DA" />
		/// </ChangeLog>
	public class ManageMoviesBase: ComponentBase
	{
		[Inject]
		private MovieClient MovieClient { get; set; } = default!;
		[Inject]
		private NavigationManager NavigationManager { get; set; } = default!;
		
		protected List<Movie>? lstMovie = new();
		protected Movie? movie = new();

		protected override async Task OnInitializedAsync()
		{
			await GetMovieList();
		}

		protected async Task GetMovieList()
		{
			var results = await MovieClient.FetchMovieList.ExecuteAsync();

			lstMovie = results?.Data?.MovieList.Select(x => new Movie
			{
				MovieId = x.MovieId,
				Title = x.Title,
				Duration = x.Duration,
				Genre = x.Genre,
				Language = x.Language,
				PosterPath = x.PosterPath,
				Rating = x.Rating,
			}).ToList();
		}

		protected void DeleteConfirm(int movieID)
		{
			movie = lstMovie?.FirstOrDefault(m => m.MovieId == movieID);
		}

		protected async Task DeleteMovie(int movieID)
		{
			var response = await MovieClient.DeleteMovieData.ExecuteAsync(movieID);
			if (response.Data is not null)
			{
				await GetMovieList();
			}
		}
	}
}
