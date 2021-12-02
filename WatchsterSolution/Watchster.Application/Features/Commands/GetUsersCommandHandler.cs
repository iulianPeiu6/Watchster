using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, IEnumerable<User>>
    {
        private readonly IUserRepository repository;

        public GetUsersCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public Task<IEnumerable<User>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
        {
            return repository.GetAllAsync();
        }
    }
}
