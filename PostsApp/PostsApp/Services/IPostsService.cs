using System.Collections.Generic;
using System.Threading.Tasks;
using PostsApp.Models;
using Refit;

namespace PostsApp.Services
{
    public interface IPostsService
    {
        [Get("/posts")]
        Task<IList<Post>> ListPostsAsync();

        [Get("/posts/{postId}/comments")]
        Task<IList<Comment>> GetCommentsFromPost(int postId);

        [Get("/photos")]
        Task<IList<Photo>> ListPhotosAsync();
    }
}
