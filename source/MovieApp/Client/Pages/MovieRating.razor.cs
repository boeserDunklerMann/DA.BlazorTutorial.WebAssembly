using Microsoft.AspNetCore.Components;

namespace MovieApp.Client.Pages
{
	public class MovieRatingBase : ComponentBase
	{
		[Parameter]
		public decimal? Rating { get; set; }
	}
}
