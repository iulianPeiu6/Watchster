using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.DataAccess.Context;
using Watchster.Domain.Entities;

namespace Watchster.DataAccess.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(WatchsterContext context) : base(context)
        {
        }
    }
}
