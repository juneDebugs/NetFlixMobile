using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace NetFlixMobile.Services
{
    public class MovieService
    {
        public static readonly int MinSearchLength = 5;

        private const string Url = "http://netflixroulette.net/api/api.php";
        private HttpClient _client = new HttpClient();

        public async Task<IEnumerable<Movie>> FindMoviesByActor(string actor)
        {
            if (actor.Length < MinSearchLength)
                return Enumerable.Empty<Movie>();

            var response = await _client.GetAsync($"{Url}?actor={actor}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return Enumerable.Empty<Movie>();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Movie>>(content);
        }


    }
}
