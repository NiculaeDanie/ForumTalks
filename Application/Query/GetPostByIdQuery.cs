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
    public class GetPostByIdQuery: IRequest<Post>
    {
        public string Id { get; set; }
    }
    public class GetPostByIdQueryHandler: IRequestHandler<GetPostByIdQuery, Post>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPostByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Post> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.PostRepository.getById(request.Id);
        }
    }
}
