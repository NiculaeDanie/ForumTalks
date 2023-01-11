using Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public class UploadPictureCommand: IRequest
    {
        public string id { get; set; }
        public IFormFile formFile { get; set; }
    }
    public class UploadPictureCommandHandler: IRequestHandler<UploadPictureCommand>
    {
        private readonly IAzureRepozitory _azureRepository;
        public UploadPictureCommandHandler(IAzureRepozitory azureRepository)
        {
            _azureRepository = azureRepository;
        }

        public async Task<Unit> Handle(UploadPictureCommand request, CancellationToken cancellationToken)
        {
            await _azureRepository.UploadAsync(request.formFile, request.id + ".jpg");
            return new Unit();
        }
    }
}
