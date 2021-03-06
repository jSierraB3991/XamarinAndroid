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
                
                model.ForEach(m => {
                    m.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Mauris rutrum tortor non lorem posuere, quis aliquet libero viverra. " +
                    "Etiam ac efficitur turpis, eget bibendum metus. Nullam est felis, consectetur sed dapibus eu, fermentum ac nulla. " +
                    "Nulla ornare, nibh eu consequat faucibus, lectus orci imperdiet ex, eget laoreet ex dolor et ex. " +
                    "Pellentesque mattis molestie aliquam. Aliquam pretium augue ut dolor semper, ut venenatis metus aliquet. " +
                    "Nunc congue, mi at pretium imperdiet, nibh ex dignissim leo, commodo placerat metus sapien at orci. " +
                    "Phasellus in faucibus enim. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. " +
                    "Etiam dictum odio risus, placerat feugiat sapien placerat at. In id velit sed tortor sagittis commodo.";
                });

                return new Response<List<Model>> { IsSuccess = true, Result = model };
            }
            catch (System.Exception ex) {
                return new Response<List<Model>> { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}