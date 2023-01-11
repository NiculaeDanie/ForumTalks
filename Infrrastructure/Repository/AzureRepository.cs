﻿using Application.Abstracts;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrrastructure.Repository
{
    public class AzureRepository : IAzureRepozitory
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        public AzureRepository(IConfiguration configuration)
        {
            _storageConnectionString = configuration["BlobConnectionString"];
            _storageContainerName = configuration["BlobContainerName"];
        }

        public async Task<Uri> DownloadAsync(string blobFilename)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            try
            {
                // Get a reference to the blob uploaded earlier from the API in the container from configuration settings
                BlobClient file = client.GetBlobClient(blobFilename);
                // Check if the file exists in the container
                if (await file.ExistsAsync())
                {
                    var data = await file.OpenReadAsync();

                    Stream blobContent = data;
                    // Download the file details async
                    var content = await file.DownloadContentAsync();
                    // Add data to variables in order to return a BlobDto
                    string name = blobFilename;
                    string contentType = content.Value.Details.ContentType;
                    // Create new BlobDto with blob data from variables
                    return file.Uri;

                }
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                return null;
            }
            // File does not exist, return null and handle that in requesting method
            return null;
        }

        public async Task<Blob> UploadAsync(IFormFile file, string filename)
        {
            Blob response = new();
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            try
            {
                BlobClient client = container.GetBlobClient(filename);
                await using (Stream? data = file.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }
                response.Uri = client.Uri.AbsoluteUri;
                response.Name = client.Name;
            }
            catch (RequestFailedException ex)
            {
                return null;
            }
            return response;
        }
    }
}
