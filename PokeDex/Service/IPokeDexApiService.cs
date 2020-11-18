namespace PokeDex.Service
{
    using PokeDex.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPokeDexApiService
    {
        Task<Response<PokeResult>> GetAll(string endPoint);
    }
}