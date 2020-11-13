namespace LoadMore.Service
{
    using LoadMore.Model;
    using Refit;
    using System.Threading.Tasks;

    internal interface ISocialMediaService
    {
        [Get("/Publish?PageNumber={pageNumber}")]
        Task<RequestPublish> GetPublish(int pageNumber);
    }
}