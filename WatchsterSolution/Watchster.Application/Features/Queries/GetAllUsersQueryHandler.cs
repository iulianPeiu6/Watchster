using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository repository;

        public GetAllUsersQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return repository.GetAllAsync();
        }
    }
}
