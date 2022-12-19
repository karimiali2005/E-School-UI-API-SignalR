using Es.DataLayerCore.Context;
using Es.DataLayerCore.DTOs.Statist;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class PreRegistrationService:IPreRegistrationService
    {
        private readonly ESchoolContext _context;
        public PreRegistrationService(ESchoolContext context)
        {
            _context = context;
        }

        public async Task<List<PreRegistration>> PreRegistrationShow()
        {
            try
            {
                var preRegistrations = await _context.PreRegistration.Where(p => p.PreRegistrationArchive == false).ToListAsync();
                return preRegistrations;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<PreRegistration> PreRegistrationByID(int preRegistrationID)
        {
            try
            {
                var preRegistration = await _context.PreRegistration.FindAsync(preRegistrationID);
                return preRegistration;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> PreRegistrationArchive(int preRegistrationID)
        {
            try
            {
                var preRegistration = await _context.PreRegistration.FindAsync(preRegistrationID);
                preRegistration.PreRegistrationArchive = true;
                
                await _context.SaveChangesAsync();

                return true;
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<StatisticsShowResult> StatisticsShow()
        {
            try
            {
                var statistics = await _context.StatisticsShowAsync();
                return statistics.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
