using Watchster.Domain.Entities;

namespace Watchster.Application.Models
{
    public class ResetPasswordCodeResult
    {
        public ResetPasswordCode ResetPasswordCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
