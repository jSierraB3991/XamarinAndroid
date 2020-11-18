namespace PokeDex.Service
{
    using Newtonsoft.Json;
    using PokeDex.Model;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PokeDexApiService : IPokeDexApiService
    {
        public async Task<Response<PokeResult>> GetAll(string endPoint)
        {
            try
            {
                string url = "https://www.pokeapi.co/api/v2/";
                endPoint = endPoint.Replace(url, string.Empty);
                HttpClient cliente = new HttpClient { BaseAddress = new Uri(url) };
                HttpResponseMessage response = await cliente.GetAsync($"{endPoint}");
                string answer = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode) return new Response<PokeResult> { IsSuccess = false, Message = answer };

                return new Response<PokeResult> { IsSuccess = true, Result = JsonConvert.DeserializeObject<PokeResult>(answer) };
            }
            catch (Exception ex)
            {
                return new Response<PokeResult>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }
    }
}