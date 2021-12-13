using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchster.Aplication.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.Interfaces
{
    public interface IRatingRepository : IRepository<Rating>
    {
    }
}
