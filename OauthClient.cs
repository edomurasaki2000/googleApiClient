using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
namespace google
{
    public class TokenResult
    {
        
        [JsonProperty]
        public string access_token{get; set;}
        [JsonProperty]
        public int expires_in{get; set;}
        [JsonProperty]
public string refresh_token{get; set;}
        [JsonProperty]
        public string scope{get; set;}
        [JsonProperty]
 public string token_type{get; set;}
     
    }
  public  class OauthClient
    {

      System.Threading.ManualResetEvent evt= new System.Threading.ManualResetEvent(false);
      string code=null;
      void callback(IAsyncResult ar)
      {
          var http = ar.AsyncState as HttpListener;
          var ctx =http.EndGetContext(ar);
         code= ctx.Request.QueryString["code"];
          ctx.Response.StatusCode=200;
          var w = new System.IO.StreamWriter(ctx.Response.OutputStream);
          w.WriteLine(code);
          w.Flush();
          w.Close();
          http.Stop();
          http.Close();
          evt.Set();
      }
      public TokenResult refresh_token(string refresh_token, string client_id, string client_secret)
      {
           var req = WebRequest.Create(
                "https://www.googleapis.com/oauth2/v4/token");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            var st = req.GetRequestStream();
            var w = new System.IO.StreamWriter(st);
                var sb = new StringBuilder();
            sb.AppendFormat("refresh_token={0}",WebUtility.UrlEncode(refresh_token))
            .AppendFormat("&client_id={0}", WebUtility.UrlEncode(client_id))
            .AppendFormat("&client_secret={0}", WebUtility.UrlEncode(client_secret))
               .AppendFormat("&grant_type={0}", "refresh_token");
        
           w.Write(sb.ToString());
            w.Close();
            var res = req.GetResponse();
           var s = new System.IO.StreamReader(res.GetResponseStream()).ReadToEnd();
           res.Dispose();
           return JsonConvert.DeserializeObject<TokenResult>(s);
             }
       public TokenResult Do_auth(string client_id,string client_secret, string redirect_uri,string scope)
       {
          var http= new HttpListener();
           http.Prefixes.Add(redirect_uri);
           http.Start();
           http.BeginGetContext(callback,http);
           System.Diagnostics.Process.Start(authrization_url(client_id,redirect_uri,scope).ToString());
           evt.WaitOne();
           return get_token(code,client_id,client_secret,redirect_uri);
       }
        Uri authrization_url(string client_id, string redirect_uri,string scope)
        {
            var builder = new UriBuilder("https://accounts.google.com/o/oauth2/v2/auth");
            var sb = new StringBuilder();
            sb.AppendFormat("client_id={0}", WebUtility.UrlEncode(client_id))
                .AppendFormat("&redirect_uri={0}", WebUtility.UrlEncode(redirect_uri))
                .AppendFormat("&response_type={0}", "code")
                .AppendFormat("&scope={0}",WebUtility.UrlEncode(scope));
            builder.Query = sb.ToString();
            return builder.Uri;
        }
        TokenResult get_token(string code,string client_id,string client_secret,string redirect_uri)
        {
            var req = WebRequest.Create(
                "https://www.googleapis.com/oauth2/v4/token");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            var st = req.GetRequestStream();
            var w = new System.IO.StreamWriter(st);
                var sb = new StringBuilder();
            sb.AppendFormat("code={0}",WebUtility.UrlEncode(code))
            .AppendFormat("&client_id={0}", WebUtility.UrlEncode(client_id))
            .AppendFormat("&client_secret={0}", WebUtility.UrlEncode(client_secret))
           .AppendFormat("&redirect_uri={0}",WebUtility.UrlEncode(redirect_uri))
            .AppendFormat("&grant_type={0}", "authorization_code");
            w.Write(sb.ToString());
            w.Close();
            var res = req.GetResponse();
           var s = new System.IO.StreamReader(res.GetResponseStream()).ReadToEnd();
           res.Dispose();
           return JsonConvert.DeserializeObject<TokenResult>( s);
          

        }
    }
}
