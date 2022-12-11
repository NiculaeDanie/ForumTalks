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
    public class GetAllCommentQuery: IRequest<ICollection<Comment>>
    {
        public string PostId { get; set; }
    }
    public class GetAllCommentQueryHandler: IRequestHandler<GetAllCommentQuery, ICollection<Comment>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllCommentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Comment>> Handle(GetAllCommentQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CommentRepository.getAll(request.PostId);
        }
    }
}
