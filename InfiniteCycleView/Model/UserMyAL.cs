namespace InfiniteCycleView.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class AnimeStats
    {
        [JsonProperty("days_watched")]
        public double DaysWatched { get; set; }

        [JsonProperty("mean_score")]
        public double MeanScore { get; set; }

        public int Watching { get; set; }
        
        public int Completed { get; set; }

        [JsonProperty("on_hold")]
        public int OnHold { get; set; }

        public int Dropped { get; set; }

        [JsonProperty("plan_to_watch")]
        public int PlanWatch { get; set; }

        [JsonProperty("total_entries")]
        public int TotalEntries { get; set; }
        public int Rewatched { get; set; }

        [JsonProperty("episodes_watched")]
        public int EpisodesWatched { get; set; }
    }

    public class MangaStats
    {
        [JsonProperty("days_read")]
        public double DaysRead { get; set; }

        [JsonProperty("mean_score")]
        public int MeanScore { get; set; }

        public int Reading { get; set; }
        
        public int Completed { get; set; }

        [JsonProperty("on_hold")]
        public int OnHold { get; set; }

        public int Dropped { get; set; }

        [JsonProperty("plan_to_read")]
        public int PlanRead { get; set; }

        [JsonProperty("total_entries")]
        public int TotalEntries { get; set; }

        public int Reread { get; set; }

        [JsonProperty("chapters_read")]
        public int ChaptersRead { get; set; }

        [JsonProperty("volumes_read")]
        public int VolumesRead { get; set; }
    }

    public class Anime
    {
        [JsonProperty("mal_id")]
        public int MalId { get; set; }
        
        public string Url { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        public string Name { get; set; }
    }

    public class Manga
    {
        [JsonProperty("mal_id")]
        public int MalId { get; set; }
        
        public string Url { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        public string Name { get; set; }
    }

    public class Favorites
    {
        public List<Anime> Anime { get; set; }

        public List<Manga> Manga { get; set; }

        public List<object> Characters { get; set; }
        
        public List<object> People { get; set; }
    }

    public class UserMyAL
    {
        [JsonProperty("request_hash")]
        public string RequestHash { get; set; }


        [JsonProperty("request_cached")]
        public bool RequestCached { get; set; }


        [JsonProperty("request_cache_expiry")]
        public int RequestCacheExpiry { get; set; }


        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        public string Url { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("last_online")]
        public DateTime LastOnline { get; set; }

        public object Gender { get; set; }

        public object Birthday { get; set; }

        public object Location { get; set; }

        public DateTime Joined { get; set; }

        [JsonProperty("anime_stats")]
        public AnimeStats AnimeStats { get; set; }

        [JsonProperty("manga_stats")]
        public MangaStats MangaStats { get; set; }

        public Favorites Favorites { get; set; }

        public object About { get; set; }
    }
}