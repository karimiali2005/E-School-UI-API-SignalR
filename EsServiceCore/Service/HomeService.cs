using Es.DataLayerCore.Context;
using Es.DataLayerCore.DTOs.Article;
using Es.DataLayerCore.DTOs.Gallery;
using Es.DataLayerCore.DTOs.Introduction;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class HomeService:IHomeService
    {
        private readonly ESchoolContext _context;
        public HomeService(ESchoolContext context)
        {
            _context = context;
        }

        public async Task<bool> PreRegistrationExists(string IRNational)
        {
            var result = await _context.PreRegistration.Where(p=>p.Irnational==IRNational).AnyAsync();
            return result;
        }
        public async Task<bool> PreRegistrationInsert(PreRegistration preRegistration)
        {
            try
            {

                await _context.PreRegistration.AddAsync(preRegistration);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<List<GalleryShowResult>> GalleryShowAll()
        {
            try
            {
                var gallery = await _context.GalleryShowAsync();
                return gallery;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<GalleryDetailShowResult>> GalleryDetailShow(int galleryId)
        {
            try
            {
                var gallery = await _context.GalleryDetailShowAsync(galleryId);
                return gallery;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ArticleShowTopResult>> ArticleShowTop()
        {
            try
            {
                var articles = await _context.ArticleShowTopAsync();
                return articles;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Article> ArticleGetById(int articleId)
        {
            try
            {
                
                var article = await _context.Article.FindAsync(articleId);
                return article;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> ArticleCountorIncrease(int articleId)
        {
            try
            {

                var article = await _context.Article.FindAsync(articleId);
                article.ArticleCountor++;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<IntroductionGroupShowResult>> IntroductionGroupShow(int introductionTypeId)
        {
            try
            {
                var introduction = await _context.IntroductionGroupShowAsync(introductionTypeId);
                return introduction;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
