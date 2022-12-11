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
    public class GetAllForumQuery: IRequest<ICollection<Forum>>
    {
    }

    public class GetAllForumQueryHandler: IRequestHandler<GetAllForumQuery, ICollection<Forum>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllForumQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Forum>> Handle(GetAllForumQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ForumRepository.getAll();
        }
    }
}
