using Application.Features.Models;
using System.Threading.Tasks;

namespace Application.Features.Infrastructure
{
    public interface IEmailServices
    {
        Task<bool> SendEmail(Email email);
    }
}
