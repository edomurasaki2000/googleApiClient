using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace google.gmail
{
    public class Client 
    {
        public string access_token { get; set; }
        public static string scope = "https://mail.google.com/";
         
        string GET(string url)
        {
            var req = WebRequest.Create(url);
            req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
            var res = req.GetResponse();
            var s = new StreamReader(res.GetResponseStream()).ReadToEnd();
            res.Dispose();
            return s;
        }
        public T GET<T>(Uri url)
        {
            var req = WebRequest.Create(url);
            req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
            var res = req.GetResponse();
            var s = new StreamReader(res.GetResponseStream()).ReadToEnd();
            res.Dispose();
            return JsonConvert.DeserializeObject<T>(s);
        }
        public T GET<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(GET(url));
        }
        public Messages messages(int maxResults=100,string pageToken=null)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("maxResults={0}",maxResults)
                .AppendFormat("&labelIds=INBOX");
            if (pageToken != null)
                sb.AppendFormat("&pageToken={0}",WebUtility.UrlEncode(pageToken));
            var builder = new UriBuilder("https://www.googleapis.com/gmail/v1/users/me/messages");
            builder.Query = sb.ToString();

            return GET<Messages>( builder.Uri);//"https://www.googleapis.com/gmail/v1/users/me/messages?maxResults=10&labelIds=INBOX");
        }
        public Profile Profile()
        {
          return  GET<Profile>(new Uri("https://www.googleapis.com/gmail/v1/users/me/profile"));
        }
        public Message Message(string msgId,bool raw=false)
        {
            var url = string.Format(
                "https://www.googleapis.com/gmail/v1/users/me/messages/{0}?format=metadata&metadataHeaders=Subject&metadataHeaders=From&metadataHeaders=Date", msgId);
            if(raw)
                url = string.Format(
                                "https://www.googleapis.com/gmail/v1/users/me/messages/{0}?format=raw", msgId);
       
            return GET<Message>(new Uri(url));
        }
        public void Modify(string id,params string[] labels)
        {
            var url = string.Format("https://www.googleapis.com/gmail/v1/users/userId/messages/{0}/modify",id);
            var req = WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            var w = new StreamWriter(req.GetRequestStream());

            var j = new JObject(
                new JProperty("removeLabelIds",
                    new JArray(labels.Select(v => { return new JValue(v); }).ToArray())
                    )
                    );
            w.Write(j.ToString());
            w.Close();
            var res = req.GetResponse(); 
            var s = new StreamReader(res.GetResponseStream()).ReadToEnd();
            res.Dispose();
        }
        public void Delete(string msgId)
        {
            var req = WebRequest.Create(
                "https://www.googleapis.com/gmail/v1/users/me/messages/" + msgId);
            req.Method = "DELETE";
            req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
            var res = req.GetResponse();
            res.Dispose();
        }
        public void Trash(string msgId)
        {
            var url = string.Format(
                "https://www.googleapis.com/gmail/v1/users/me/messages/{0}/trash", msgId);
            var req = WebRequest.Create(url);
            req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
          
            req.Method = "POST";
            req.ContentLength = 0;
            var res = req.GetResponse();
            var w = new StreamReader(res.GetResponseStream()).ReadToEnd();
            res.Dispose();
        }
    }
}
