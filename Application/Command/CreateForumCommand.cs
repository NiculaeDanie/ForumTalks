using Application.Abstracts;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public class CreateForumCommand: IRequest<Forum>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string OwnerId { get; set; }
    }

    public class CreateForumCommandHandler: IRequestHandler<CreateForumCommand, Forum>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateForumCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Forum> Handle(CreateForumCommand request, CancellationToken cancellationToken)
        {
            Forum forum = new Forum()
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                OwnerId = request.OwnerId
            };
            await _unitOfWork.ForumRepository.add(forum);
            await _unitOfWork.Save();
            return forum;
        }
    }
}
