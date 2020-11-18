using System.Collections.Generic;

namespace PokeDex.Model
{
    public class PokeResult
    {
        public int Count { get; set; }

        public string Next { get; set; }

        public string Previous { get; set; }

        public List<PokeDex> Results { get; set; }
    }
}