using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts
{
    public interface IAzureRepozitory
    {
        Task<Blob> UploadAsync(IFormFile file, string filename);
        Task<Uri> DownloadAsync(string blobFilename);
    }
}
