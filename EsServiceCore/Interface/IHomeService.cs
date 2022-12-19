using Es.DataLayerCore.DTOs.Article;
using Es.DataLayerCore.DTOs.Gallery;
using Es.DataLayerCore.DTOs.Introduction;
using Es.DataLayerCore.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface IHomeService
    {
        Task<bool> PreRegistrationExists(string IRNational);
        Task<bool> PreRegistrationInsert(PreRegistration preRegistration);
        Task<List<GalleryShowResult>> GalleryShowAll();
        Task<List<GalleryDetailShowResult>> GalleryDetailShow(int galleryId);
        Task<List<ArticleShowTopResult>> ArticleShowTop();
        Task<Article> ArticleGetById(int articleId);
        Task<bool> ArticleCountorIncrease(int articleId);
        Task<List<IntroductionGroupShowResult>> IntroductionGroupShow(int introductionTypeId);
    }
}
