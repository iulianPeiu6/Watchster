using System;
using Watchster.Domain.Common;

namespace Watchster.Domain.Entities
{
    public class ResetPasswordCode : BaseEntity
    {
        public String Code { get; set; }
        public DateTime expirationDate { get; set; }
        public String Email { get; set; }
    }
}
