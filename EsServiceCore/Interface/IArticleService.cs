using Es.DataLayerCore.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface IArticleService
    {
        Task<List<Article>> ArticleShowAll();
        Task<bool> ArticleInsert(Article article);
        Task<Article> ArticleGetById(int articleId);
        Task<bool> ArticleUpdate(Article article);
        Task<bool> ArticleDelete(int id);
    }
}
