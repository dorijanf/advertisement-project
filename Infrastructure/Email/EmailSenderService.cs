using Database.Entities;
using Domain.Interfaces;
using SharedModels.Utils;

namespace Infrastructure.Email
{
    /// <summary>
    /// Mock email service which stores email in the EmailQueue database
    /// table. No SMTP server is set up for further email sending.
    /// </summary>
    public class EmailSenderService : IEmailSenderService

    {
        private readonly IRepository<EmailQueue> emailQueueRepository;

        public EmailSenderService(IRepository<EmailQueue> emailQueueRepository)
        {
            this.emailQueueRepository = emailQueueRepository;
        }

        /// <summary>
        /// Adds email to Email queue in the database.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task SendEmail(EmailObject email)
        {
            await emailQueueRepository.Insert(new EmailQueue()
            {
                SendTo = email.UserEmail,
                Content = email.Content
            });
        }
    }
}
