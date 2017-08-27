using System;
using System.Collections.Generic;

using Xamarin.Forms;

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
            InitializeComponent();
        }

        void OnTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            throw new ();fgf
        }
    }
}
