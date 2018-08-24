using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace google.gmail
{
    public class Profile
    {

        public string emailAddress { get; set; }
        public int messagesTotal { get; set; }
        public int threadsTotal { get; set; }
        public string historyId { get; set; }
    }
    public class Message
    {
        public byte[] getContent()
        {
            var base64 = raw;
            base64 = base64.Replace('-', '+').Replace('_', '/');
            return Convert.FromBase64String(base64);

        }
        public override string ToString()
        {
            return Subject;
        }
        [JsonIgnore]
        public string Subject { get { return payload.headers.First(p => { return p.name == "Subject"; }).value; } }
        [JsonIgnore]
        public string Date { get { return payload.headers.First(p => { return p.name == "Date"; }).value; } }
        [JsonIgnore]
        public string From { get { return payload.headers.First(p => { return p.name == "From"; }).value; } }

        public string id { get; set; }
        public string threadId { get; set; }
        public string[] labelIds { get; set; }
        public string snippet { get; set; }
        public string historyId { get; set; }
        public string internalDate { get; set; }
        public Payload payload { get; set; }
        public class Payload
        {
            public string mimeType { get; set; }
            public Header[] headers { get; set; }
            public class Header
            {
                public string name { get; set; }
                public string value { get; set; }
            }

        }
        public int sizeEstimate { get; set; }
        public string raw { get; set; }

    }
    public class Messages
    {
        [JsonProperty]
        public Message[] messages { get; set; }
        public class Message
        {
            [JsonProperty]
            public string id { get; set; }
            [JsonProperty]
            public string threadId { get; set; }
        }
         [JsonProperty]
        public string nextPageToken { get; set; }
        [JsonProperty]
        public int resultSizeEstimate { get; set; }
    }
}