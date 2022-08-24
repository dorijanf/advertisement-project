using System;

namespace SharedModels.Exceptions
{
    /// <summary>
    /// In case something happens when performing elastic search
    /// indexing, the program should throw an exception.
    /// </summary>
    public class ElasticSearchError : Exception
    {
        public ElasticSearchError() : base("Something went wrong with elastic search.") { }

        public ElasticSearchError(string message) : base(message) { }
    }
}
