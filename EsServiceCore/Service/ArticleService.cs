using Es.DataLayerCore.Context;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class ArticleService: IArticleService
    {
        private readonly ESchoolContext _context;
        public ArticleService(ESchoolContext context)
        {
            _context = context;
        }
        public async Task<List<Article>> ArticleShowAll()
        {
            try
            {
                var article = await _context.Article.ToListAsync();
                return article;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> ArticleInsert(Article article)
        {
            try
            {

                _context.Article.Add(article);
                await _context.SaveChangesAsync();

                return true;
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

        public async Task<bool> ArticleUpdate(Article article)
        {
            try
            {

                _context.Article.Update(article);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<bool> ArticleDelete(int id)
        {
            try
            {
               

                var article = await _context.Article.FindAsync(id);
                _context.Article.Remove(article);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
