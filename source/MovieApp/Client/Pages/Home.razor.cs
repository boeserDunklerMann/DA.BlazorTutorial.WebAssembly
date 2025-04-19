using Microsoft.AspNetCore.Components;
using MovieApp.Client.GraphQLAPIClient;
using MovieApp.Server.Models;

namespace MovieApp.Client.Pages
{
	/// <ChangeLog>
	/// <Create Datum="19.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public class HomeBase : ComponentBase
	{
		[Parameter]
		public string GenreName { get; set; } = default!;
		[Inject]
		private MovieClient MovieClient { get; set; } = default!;
		protected List<Movie> lstMovie = new();
		protected List<Movie> filteredMovie = new();

		protected override async Task OnInitializedAsync()
		{
			MovieSortInput initialSort = new() { Title = SortEnumType.Asc };
			await GetMovieListAsync(initialSort);
		}
		protected override void OnParametersSet()
		{
			FilterMovie();
		}
		protected async Task SortMovieData(ChangeEventArgs eventArgs)
		{
			switch(eventArgs.Value?.ToString())
			{
				case "title":
					await GetMovieListAsync(new MovieSortInput { Title = SortEnumType.Asc });
					break;
				case "rating":
					await GetMovieListAsync(new MovieSortInput { Rating = SortEnumType.Desc });
					break;
				case "duration":
					await GetMovieListAsync(new MovieSortInput { Duration = SortEnumType.Desc });
					break;
				default:
					throw new ApplicationException($"got unexpected ssorting: {eventArgs.Value}");
			}
		}
		private async Task GetMovieListAsync(MovieSortInput sortInput)
		{
			var results = await MovieClient.SortMovieList.ExecuteAsync(sortInput);
			if (results.Data is not null)
			{
				lstMovie = results.Data.MovieList.Select(m => new Movie
				{
					MovieId = m.MovieId,
					Title = m.Title,
					Duration = m.Duration,
					Genre = m.Genre,
					Language = m.Language,
					PosterPath = m.PosterPath,
					Rating = m.Rating
				}).ToList();
			}
			filteredMovie = lstMovie;
		}

		private void FilterMovie()
		{
			if (!string.IsNullOrEmpty(GenreName))
				lstMovie = filteredMovie.Where(m => m.Genre == GenreName).ToList();
			else
				lstMovie = filteredMovie;
		}
	}
}