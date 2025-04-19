using Microsoft.AspNetCore.Components;
using MovieApp.Client.GraphQLAPIClient;
using MovieApp.Server.Models;

namespace MovieApp.Client.Pages
{
	/// <ChangeLog>
	/// <Create Datum="19.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public class MovieGenreBase : ComponentBase
	{
		[Inject]
		NavigationManager NavigationManager { get; set; } = default!;
		[Inject]
		MovieClient MovieClient { get; set; } = default!;
		[Parameter]
		public string SelectedGenre { get; set; } = "";
		protected List<Genre> lstGenre = new();

		protected override async Task OnInitializedAsync()
		{
			var genres = await MovieClient.FetchGenreList.ExecuteAsync();
			if (genres.Data is not null)
			{
				lstGenre = genres.Data.GenreList.Select(g => new Genre
				{
					GenreId = g.GenreId,
					GenreName = g.GenreName
				}).ToList();
			}
		}

		protected void SelectGenre(string genreName)
		{
			if (string.IsNullOrEmpty(genreName))
			{
				NavigationManager.NavigateTo("/");
			}
			else
			{
				NavigationManager.NavigateTo($"/category/{genreName}");
			}
		}
	}
}