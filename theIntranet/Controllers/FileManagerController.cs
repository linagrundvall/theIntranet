using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using theIntranet.Models;

namespace theIntranet.Controllers
{
    public class FileManagerController : Controller
    {
        private BlobServiceClient _serviceClient;
        private BlobContainerClient _containerClient;
        private BlobClient _blobClient;
        //private List<string> _fileTypesAllowed;

        public FileManagerController(IConfiguration configuration)
        {
            _serviceClient = new BlobServiceClient(configuration.GetConnectionString("StorageAccount"));
            _containerClient = GetBlobContainerClient("files");
            //_fileTypesAllowed = new List<string>() { ".pdf", ".docx", ".pptx", ".xlsx", ".png", ".jpg" };
        }

        private BlobContainerClient GetBlobContainerClient(string name)
        {
            try 
            {
                return _serviceClient.CreateBlobContainer(name);
            }
            catch
            {
                return _serviceClient.GetBlobContainerClient(name);
            }
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();

            await foreach(BlobItem blobItem in _containerClient.GetBlobsAsync())
                viewModel.Items.Add(blobItem);

            return View(viewModel);
        }

        public async Task<ActionResult> Upload(IndexViewModel model)
        {
            using (var ms = new MemoryStream())
            {
                
                
                    string extension = Path.GetExtension(model.FileItem.File.FileName).ToLower();

                    if (extension == ".pdf" || extension == ".docx" || extension == ".pptx" || extension == ".xlsx" || extension == ".png" || extension == ".jpg")
                    {
                        model.FileItem.File.CopyTo(ms);
                        _blobClient = _containerClient.GetBlobClient(model.FileItem.File.FileName);
                        ms.Position = 0;
                        await _blobClient.UploadAsync(ms, true);
                    }
                    else
                    {
                        throw new ArgumentException("Filetype is wrong.");
                        //Funkar inte. Ta bort?
                    }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Download(string name)
        {
            string localPath = Path.Combine($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\files\download", name);
            _blobClient = _containerClient.GetBlobClient(name);
            await _blobClient.DownloadToAsync(localPath);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string name)
        {
            await _containerClient.DeleteBlobIfExistsAsync(name);

            return RedirectToAction(nameof(Index));
        }
    }
}
