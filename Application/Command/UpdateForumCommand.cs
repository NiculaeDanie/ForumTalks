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
        public class UpdateForumCommand : IRequest<Forum>
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public string OwnerId { get; set; }
        }

        public class UpdateForumCommandHandler : IRequestHandler<UpdateForumCommand,Forum>
        {
            private readonly IUnitOfWork _unitOfWork;
            public UpdateForumCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Forum> Handle(UpdateForumCommand request, CancellationToken cancellationToken)
            {
                var forum = await _unitOfWork.ForumRepository.getById(request.Id);
                if (forum.OwnerId != request.OwnerId)
                    return null;
                forum.Description = request.Description;
                forum.Price = request.Price;
                forum.Name = request.Name;
                await _unitOfWork.ForumRepository.update(forum);
                await _unitOfWork.Save();
                return forum;
            }
        }

}
