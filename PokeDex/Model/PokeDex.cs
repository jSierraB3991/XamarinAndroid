namespace PokeDex.Model
{
    using System;
    public class PokeDex
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public int Id => Int32.Parse(this.Url.Split('/')[^2]);

        public string UrlImagePrincipal => $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{this.Id}.png";
    }
}