using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

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
            {
                return (bool)GetValue(IsSearchingProperty);
            }

            set
            {
                SetValue(IsSearchingProperty, value);
            }
        }

        public MoviesPage()
        {
            BindingContext = this;

            InitializeComponent();
        }

        async void OnTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if
                (e.NewTextValue == null || e.NewTextValue.Length < MovieService.MinSearchLength)
                return;

            await FindMovies(actor: e.NewTextValue);
        }

        async Task FindMovies (string actor)
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
                await DisplayAlert("Error", "Could not retrive the list of movies.", "OK");
            }

            finally
            {
                IsSearching = false;
            }

        }
    }
}
