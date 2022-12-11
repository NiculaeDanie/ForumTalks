using Application.Abstracts;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query
{
    public class GetAllPostQuery: IRequest<ICollection<Post>>
    {
        public string ForumId { get; set; }
    }
    public class GetAllPostQueryHandler: IRequestHandler<GetAllPostQuery,ICollection<Post>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllPostQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Post>> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.PostRepository.getAll(request.ForumId);
        }
    }
}
