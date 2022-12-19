using Es.DataLayerCore.Context;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class GalleryService: IGalleryService
    {
        private readonly ESchoolContext _context;
        public GalleryService(ESchoolContext context)
        {
            _context = context;
        }
        public async Task<List<Gallery>> GalleryShowAll()
        {
            try
            {
                var gallery = await _context.Gallery.ToListAsync();
                return gallery;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> GalleryInsert(Gallery gallery)
        {
            try
            {

                await _context.Gallery.AddAsync(gallery);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Gallery> GalleryGetById(int galleryId)
        {
            try
            {
                var gallery = await _context.Gallery.FindAsync(galleryId);
                return gallery;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> GalleryUpdate(Gallery gallery)
        {
            try
            {

                _context.Gallery.Update(gallery);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> GalleryDelete(int id)
        {
            try
            {
                var galleryDetail= await _context.GalleryDetail.Where(g=>g.GalleryId==id).ToListAsync();
                _context.GalleryDetail.RemoveRange(galleryDetail);

                var gallery = await _context.Gallery.FindAsync(id);
                _context.Gallery.Remove(gallery);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<GalleryDetail>> GalleryDetailPicShowAll(int galleryId)
        {
            try
            {
                var gallerydetails = await _context.GalleryDetail.Where(g => g.GalleryDetailType == 1&&g.GalleryId== galleryId).ToListAsync();
                return gallerydetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<bool> GalleryDetailInsert(GalleryDetail galleryDetail)
        {
            try
            {

                _context.GalleryDetail.Add(galleryDetail);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<GalleryDetail> GalleryDetailGetById(int galleryDetailId)
        {
            try
            {
                var galleryDetail = await _context.GalleryDetail.FindAsync(galleryDetailId);
                return galleryDetail;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> GalleryDetailDelete(int id)
        {
            try
            {
                var galleryDetail = await _context.GalleryDetail.FindAsync(id);
                _context.GalleryDetail.Remove(galleryDetail);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<List<GalleryDetail>> GalleryDetailVideoShowAll(int galleryId)
        {
            try
            {
                var gallerydetails = await _context.GalleryDetail.Where(g => g.GalleryDetailType == 2 && g.GalleryId == galleryId).ToListAsync();
                return gallerydetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
