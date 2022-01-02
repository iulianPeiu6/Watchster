using System;
using Watchster.Domain.Common;

namespace Watchster.Domain.Entities
{
    public class ResetPasswordCode : BaseEntity
    {
        public string Code { get; set; }
        public DateTime expirationDate { get; set; }
        public string Email { get; set; }
    }
}
