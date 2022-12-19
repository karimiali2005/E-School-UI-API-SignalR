using System;

namespace Es.DataLayerCore.DTOs.Gallery
{
    public class GalleryDetailShowResult
    {
        public int GalleryID { get; set; }
        public string GalleryTitle { get; set; }
        public string GalleryDescription { get; set; }
        public DateTime GalleryDateCreate { get; set; }
        public int GalleryDetailID { get; set; }
        public string GalleryDetailName { get; set; }
        public bool GalleryIsDefault { get; set; }
        public int GalleryDetailType { get; set; }
    }
}
