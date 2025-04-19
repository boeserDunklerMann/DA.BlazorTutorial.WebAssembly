﻿using Microsoft.AspNetCore.Components;
using MovieApp.Client.GraphQLAPIClient;
using MovieApp.Server.Models;

namespace MovieApp.Client.Pages
{
	public class MovieDetailsBase : ComponentBase
	{
		[Inject]
		MovieClient MovieClient { get; set; } = default!;
		[Parameter]
		public int MovieID { get; set; }
		public Movie movie = new();
		protected string imagePreview = "";
		protected string movieDuration = "";

		protected override async Task OnParametersSetAsync()
		{
			MovieFilterInput movieFilterInput = new()
			{
				MovieId = new()
				{
					Eq = MovieID
				}
			};
			var response = await MovieClient.FilterMovieByID.ExecuteAsync(movieFilterInput);
			if (response.Data is not null)
			{
				var movieData = response.Data.MovieList[0];
				movie.MovieId = movieData.MovieId;
				movie.Title = movieData.Title;
				movie.Genre = movieData.Genre;
				movie.Duration = movieData.Duration;
				movie.PosterPath = movieData.PosterPath;
				movie.Rating = movieData.Rating;
				//movie.Overview = movieData.Overview;
				movie.Language = movieData.Language;
				imagePreview = "/Poster/" + movie.PosterPath;
				ConvertMinToHour();
			}
		}

		private void ConvertMinToHour()
		{
			TimeSpan movieLength = TimeSpan.FromMinutes(movie.Duration);
			movieDuration = string.Format("{0:0}h {1:0}min", (int)movieLength.TotalHours, movieLength.Minutes);
		}
	}
}
