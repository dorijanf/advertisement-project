using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Exceptions
{
    public class ElasticSearchError : Exception
    {
        public ElasticSearchError() : base("Something went wrong with elastic search.") { }

        public ElasticSearchError(string message) : base(message) { }
    }
}
