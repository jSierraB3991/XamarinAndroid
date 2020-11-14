namespace InfiniteCycleView.Service
{
    using InfiniteCycleView.Model;
    using Refit;
    using System.Threading.Tasks;

    public interface IRefitService
    {
        [Get("/user/{user}")]
        Task<UserMyAL> GetUser(string user);
    }
}