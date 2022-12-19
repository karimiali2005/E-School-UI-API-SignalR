using Es.DataLayerCore.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface IGalleryService
    {
        Task<List<Gallery>> GalleryShowAll();
        Task<bool> GalleryInsert(Gallery gallery);
        Task<Gallery> GalleryGetById(int galleryId);
        Task<bool> GalleryUpdate(Gallery gallery);
        Task<bool> GalleryDelete(int id);
        Task<List<GalleryDetail>> GalleryDetailPicShowAll(int galleryId);
        Task<bool> GalleryDetailInsert(GalleryDetail galleryDetail);
        Task<GalleryDetail> GalleryDetailGetById(int galleryDetailId);
        Task<bool> GalleryDetailDelete(int id);
        Task<List<GalleryDetail>> GalleryDetailVideoShowAll(int galleryId);
    }
}
