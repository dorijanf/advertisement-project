using System.Threading.Tasks;
using SharedModels.Utils;

namespace Domain.Interfaces
{
    /// <summary>
    /// Mock email service which stores email in the EmailQueue database
    /// table. No SMTP server is set up for further email sending.
    /// </summary>
    public interface IEmailSenderService
    {
        /// <summary>
        /// Adds email to Email queue in the database.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task SendEmail(EmailObject email);
    }
}
