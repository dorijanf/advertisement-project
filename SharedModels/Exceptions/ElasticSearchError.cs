using System;

namespace SharedModels.Exceptions
{
    public class ElasticSearchError : Exception
    {
        public ElasticSearchError() : base("Something went wrong with elastic search.") { }

        public ElasticSearchError(string message) : base(message) { }
    }
}
