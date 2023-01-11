using Application.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query
{
    public class GetPictureQuery: IRequest<Uri>
    {
        public string userId { get; set; }
    }
    public class GetPictureQueryHandler: IRequestHandler<GetPictureQuery, Uri>
    {
        private readonly IAzureRepozitory _azureRepository;
        public GetPictureQueryHandler(IAzureRepozitory azureRepository)
        {
            _azureRepository = azureRepository;
        }

        public async Task<Uri> Handle(GetPictureQuery request, CancellationToken cancellationToken)
        {
            return await _azureRepository.DownloadAsync(request.userId + ".jpg");
        }
    }
}
