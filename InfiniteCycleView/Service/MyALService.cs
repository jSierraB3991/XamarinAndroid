namespace InfiniteCycleView.Service
{
    using Android.Content;
    using InfiniteCycleView.Model;
    using Refit;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MyALService
    {
        public async Task<Response<List<Model>>> GetFavoritesByUser(Context ctx, string user)
        {
            if (!Util.IsConnectedToInternet(ctx)) 
                return new Response<List<Model>> { IsSuccess = false, Message = ctx.GetString(Resource.String.msg_no_internet_conection) };
            try
            {
                IRefitService service = RestService.For<IRefitService>(ctx.GetString(Resource.String.data_url_api));
                var response = await service.GetUser(user);
                var model = response.Favorites.Anime.Select(fa => new Model { Id = fa.MalId, ImageUrl = fa.ImageUrl, Name = fa.Name }).ToList();
                if (model == null) model = new List<Model>();
                model.AddRange(response.Favorites.Manga.Select(fa => new Model { Id = fa.MalId, ImageUrl = fa.ImageUrl, Name = fa.Name }).ToList());

                return new Response<List<Model>> { IsSuccess = true, Result = model.ToList() };
            }
            catch (System.Exception ex) {
                return new Response<List<Model>> { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}