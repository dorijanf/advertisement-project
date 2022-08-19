using System.Threading.Tasks;
using Database;
using Database.Entities;
using SharedModels.Utils;

namespace Domain.Services
{
    /// <summary>
    /// Mock email service which stores email in the EmailQueue database
    /// table. No SMTP server is set up for further email sending.
    /// </summary>
    public class EmailSenderService : IEmailSenderService

    {
    private readonly AdvertisementContext dbContext;

    public EmailSenderService(AdvertisementContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <summary>
    /// Adds email to Email queue in the database.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task SendEmail(EmailObject email)
    {
        dbContext.EmailQueues.Add(new EmailQueue()
        {
            SendTo = email.UserEmail,
            Content = email.Content
        });

        await dbContext.SaveChangesAsync();
    }
    }
}
