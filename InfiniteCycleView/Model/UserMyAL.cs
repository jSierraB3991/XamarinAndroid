namespace InfiniteCycleView.Model
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Favorites
    {
        [JsonProperty("mal_id")]
        public int MalId { get; set; }
        
        public string Url { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        public string Name { get; set; }
    }

    public class FavoritesUser
    {
        public List<Favorites> Anime { get; set; }

        public List<Favorites> Manga { get; set; }
    }

    public class UserMyAL
    {
        public FavoritesUser Favorites { get; set; }
    }
}