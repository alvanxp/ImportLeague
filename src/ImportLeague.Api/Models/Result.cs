using Newtonsoft.Json;

namespace ImportLeague.Api.Models
{
    /// <summary>
    /// Import League Result
    /// </summary>
    public class Result
    {
        public Result(string message)
        {
            Message = message;
        }
        /// <summary>
        /// Result message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
