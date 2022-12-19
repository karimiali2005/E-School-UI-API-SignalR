using Es.DataLayerCore.Context;
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
    public class IntroductionService: IIntroductionService
    {
        private readonly ESchoolContext _context;
        public IntroductionService(ESchoolContext context)
        {
            _context = context;
        }
        public async Task<List<IntroductionShowAllResult>> IntroductionShowAll()
        {
            try
            {
                var introductions = await _context.IntroductionShowAllAsync();
                return introductions;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<IntroductionType>> IntroductionTypeShowAll()
        {
            try
            {
                var introductionType = await _context.IntroductionType.ToListAsync();
                return introductionType;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> IntroductionInsert(Introduction introduction)
        {
            try
            {

                _context.Introduction.Add(introduction);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Introduction> IntroductionGetById(int introductionId)
        {
            try
            {
                var introduction = await _context.Introduction.FindAsync(introductionId);
                return introduction;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> IntroductionUpdate(Introduction introduction)
        {
            try
            {

                _context.Introduction.Update(introduction);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<bool> IntroductionDelete(int id)
        {
            try
            {


                var article = await _context.Introduction.FindAsync(id);
                _context.Introduction.Remove(article);
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
