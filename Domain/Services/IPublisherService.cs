using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_template.Domain.Services
{
    public interface IPublisherService
    {
        Task Publish<T>(T message);
    }
}
