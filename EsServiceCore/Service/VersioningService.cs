using Es.DataLayerCore.Context;
using Es.DataLayerCore.DTOs.Versioning;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class VersioningService: IVersioningService
    {
        private readonly ESchoolContext _context;
        public VersioningService(ESchoolContext context)
        {
            _context = context;
        }
        public async Task<VersioningViewModel> GetLoadVersion()
        {
            try
            {
                var lastForce = _context.Versioning.Where(v => v.ForceInstalling).OrderByDescending(v => v.VersioningId).FirstOrDefault();
                var lastNoForce = _context.Versioning.OrderByDescending(v => v.VersioningId).FirstOrDefault();
                var versioning = new VersioningViewModel()
                {
                    VersionCodeForceInstalling = lastForce.VersionCode,
                    VersionNameForceInstalling = lastForce.VersionName,
                    VersionUrlForceInstalling=lastForce.VersioningUrl,
                    VersionCodeNoForceInstalling = lastNoForce.VersionCode,
                    VersionNameNoForceInstalling = lastNoForce.VersionName,
                    VersionUrlNoForceInstalling=lastNoForce.VersioningUrl
                };
                return versioning;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
