using System.Threading.Tasks;
using Core.Entities.Products;

namespace Core.Interfaces.Services
{
    public interface IEmailSenderService
    {
        Task SendConfirmationEmailAsync(string to, string token, string username);
        Task SendPasswordResetEmailAsync(string to, string token, string username);
        Task SendProductQuantityEmailAsync(string to, string username, Product product);

    }
}