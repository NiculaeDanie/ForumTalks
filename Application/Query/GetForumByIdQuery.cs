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
    public class GetForumByIdQuery: IRequest<Forum>
    {
        public string Id { get; set; }
    }

    public class GetForumByIdQueryHandler : IRequestHandler<GetForumByIdQuery, Forum>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetForumByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Forum> Handle(GetForumByIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.ForumRepository.getById(request.Id);
        }
    }
}
