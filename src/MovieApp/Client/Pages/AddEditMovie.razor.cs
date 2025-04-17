using Microsoft.AspNetCore.Components;
using MovieApp.Client.GraphQLAPIClient;
using MovieApp.Server.Models;

namespace MovieApp.Client.Pages
{
	/// <ChangeLog>
	/// <Create Datum="17.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public partial class AddEditMovie : ComponentBase
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
		protected string? imagePeview;
		const int MaxFileSize = 10 * 1024 * 1024;   // 10 MB
		const string DefaultStatus = "Maximum file size allowed for the image is 10 MB";
		protected string status = DefaultStatus;

	}
}