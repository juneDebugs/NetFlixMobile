using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NetFlixMobile.Views
{
    public partial class MovieDetailsPage : ContentPage
    {
        private MovieService _movieService = new MovieService();
        private Movie _movie;

        public MovieDetailsPage()
        {
            if
                (movie == null)
                throw new ArgumentNullException(nameof(movie));

            _movie = movie;

            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            BindingContext = await _movieService.GetMovie(_movie.Title);

            base.OnAppearing();
        }
    }
}
