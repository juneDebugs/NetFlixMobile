using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using NetFlixMobile.Services;
using System.Linq;
using NetFlixMobile.Models;

namespace NetFlixMobile.Views
{
	public partial class MoviesPage : ContentPage
	{
		private MovieService _service = new MovieService();
         
		private BindableProperty IsSearchingProperty =
			BindableProperty.Create("IsSearching", typeof(bool), typeof(MoviesPage), false);
		
        public bool IsSearching
		{
			get 
                { return (bool)GetValue(IsSearchingProperty); }
			set 
                { SetValue(IsSearchingProperty, value); }
		}

		public MoviesPage()
		{
			BindingContext = this;

			InitializeComponent();
		}

		async void OnTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			// Here we are checking for "null" because if the user presses Cancel, 
			// e.NewTextValue will be null. 
			if (e.NewTextValue == null || e.NewTextValue.Length < MovieService.MinSearchLength)
				return;
                 
			await FindMovies(actor: e.NewTextValue);
		}

		async Task FindMovies(string actor)
		{
			try
			{
				IsSearching = true;

				var movies = await _service.FindMoviesByActor(actor);
				moviesListView.ItemsSource = movies;
				moviesListView.IsVisible = movies.Any();
				notFound.IsVisible = !moviesListView.IsVisible;
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Could not retrieve the list of movies.", "OK");
			}
			finally
			{
				IsSearching = false;
			}
		}

		async void OnMovieSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if 
                (e.SelectedItem == null)
				return;

			var movie = e.SelectedItem as Movie;

			moviesListView.SelectedItem = null;

			await Navigation.PushAsync(new MovieDetailsPage(movie));
		}
	}
}
