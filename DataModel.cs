using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace google
{
    public class ErrorResponse
    {
        [JsonProperty]
        public Error error { get; set; }
        public class Error
        {
            [JsonProperty]
            public Item[] errors { get; set; }
            public class Item
            {
                [JsonProperty]
                public string domain { get; set; }
                [JsonProperty]
                public string reason { get; set; }
                [JsonProperty]
                public string message { get; set; }
                [JsonProperty]
                public string locationType { get; set; }
                [JsonProperty]
                public string location { get; set; }

            }
            [JsonProperty]
            public int code { get; set; }

            [JsonProperty]
            public string message { get; set; }
        }
    }
}
