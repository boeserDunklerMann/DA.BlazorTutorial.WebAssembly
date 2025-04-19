using Microsoft.AspNetCore.Components;
using MovieApp.Server.Models;

namespace MovieApp.Client.Pages
{
	/// <ChangeLog>
	/// <Create Datum="19.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public class MovieCardBase : ComponentBase
	{
		[Parameter]
		public Movie Movie { get; set; } = new();
		protected string imagePreview = "";
		protected override void OnParametersSet()
		{
			imagePreview = "/Poster/" + Movie.PosterPath;
		}
	}
}