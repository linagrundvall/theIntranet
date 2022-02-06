using Azure.Storage.Blobs.Models;

namespace theIntranet.Models
{
    public class IndexViewModel
    {
        public FileModel FileItem { get; set; }
        public List<BlobItem> Items { get; set; } = new List<BlobItem>();
    }
}
