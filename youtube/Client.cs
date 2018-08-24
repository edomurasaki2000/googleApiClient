using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
namespace google.youtube
{
   public class Client 
    {
       public string access_token { get; set;}
       public static string scope = "https://www.googleapis.com/auth/youtube https://www.googleapis.com/auth/youtubepartner";
         
         string GET(string url)
         {
             var req = WebRequest.Create(url);
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
             var res = req.GetResponse();
             var s = new StreamReader(res.GetResponseStream()).ReadToEnd();
             res.Dispose();
             return s;
         }
         public SearchListResponse search(string q, string pageToken=null)
         {
             string url =string.Format(
                 "https://www.googleapis.com/youtube/v3/search?part=snippet&type=video&maxResults=50&q={0}",Uri.EscapeDataString(q));
        
             if(pageToken !=null)
                 url = string.Format(
                                  "https://www.googleapis.com/youtube/v3/search?part=snippet&type=video&maxResults=50&q={0}&pageToken={1}", Uri.EscapeDataString(q),pageToken);

        
             var req = WebRequest.Create(url);
               req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
             var res = req.GetResponse();
             var r = new StreamReader(res.GetResponseStream()).ReadToEnd();
              return Newtonsoft.Json.JsonConvert.DeserializeObject<SearchListResponse>(r);
         }
         public Playlist add_playlist(string title,string description,string privacyStatus)
         {
             var req = WebRequest.Create("https://www.googleapis.com/youtube/v3/playlists?part=status,snippet");
             req.Method = "POST";
             req.ContentType = "application/json";
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
             var st = req.GetRequestStream();
             var w = new StreamWriter(st);
             var j = new JObject(new JProperty("snippet",new JObject(
                 new JProperty("title",new JValue(title)),
                 new JProperty("description",new JValue(description))
                 )),
                                 new JProperty("status",new JObject(
                                     new JProperty("privacyStatus",new JValue(privacyStatus))
                                     ))
                 );
             w.Write(j.ToString());
             w.Close();
             var res = req.GetResponse();
             var r = new StreamReader(res.GetResponseStream()).ReadToEnd();
             res.Dispose();
             return Newtonsoft.Json.JsonConvert.DeserializeObject<Playlist>(r);
         }

         public void delete_playlist(string id)
         {
             var req = WebRequest.Create("https://www.googleapis.com/youtube/v3/playlists?id=" + id);
             req.Method = "DELETE";
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
              
             var res = req.GetResponse();
             //  );
         }
       public Playlist edit_playlist(string id,string title, string description, string privacyStatus)
         {
             var req = WebRequest.Create("https://www.googleapis.com/youtube/v3/playlists?part=status,snippet");
             req.Method = "PUT";
             req.ContentType = "application/json";
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
             var st = req.GetRequestStream();
             var w = new StreamWriter(st);
             var j = new JObject(new JProperty("id",new JValue(id)),
                 new JProperty("snippet", new JObject(
                 new JProperty("title", new JValue(title)),
                 new JProperty("description", new JValue(description))
                 )),
                                 new JProperty("status", new JObject(
                                     new JProperty("privacyStatus", new JValue(privacyStatus))
                                     ))
                 );
             w.Write(j.ToString());
             w.Close();
             var res = req.GetResponse();
             var r = new StreamReader(res.GetResponseStream()).ReadToEnd();
             res.Dispose();
             return Newtonsoft.Json.JsonConvert.DeserializeObject<Playlist>(r);
         }
       public string add_subscription(string channelId)
       {
           var req = WebRequest.Create("https://www.googleapis.com/youtube/v3/subscriptions?part=snippet");
           req.Method = "POST";
           req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
           req.ContentType = "application/json";
           var j = new JObject(new JProperty("snippet",
               new JObject(new JProperty("resourceId",
                 new JObject(new JProperty("kind", new JValue("youtube#channel")),
                     new JProperty("channelId",new JValue(channelId)))  
                   ))
               ));
           var w = new StreamWriter(req.GetRequestStream());
           w.Write(j.ToString());
           var res = req.GetResponse();
           var r = new StreamReader(res.GetResponseStream()).ReadToEnd();
           res.Dispose();
           return r;
       }
       public void delete_subscription(string subscription_id)
       {
           var req = WebRequest.Create("https://www.googleapis.com/youtube/v3/subscriptions?id="+subscription_id);
           req.Method = "DELETE";
           req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
           var res = req.GetResponse();
           res.Dispose();
       }
       public SubscriptionListResponse list_subscription()
       {
             return Newtonsoft.Json.JsonConvert.DeserializeObject<SubscriptionListResponse>(
                 GET("https://www.googleapis.com/youtube/v3/subscriptions?maxResults=50&part=snippet&mine=true"));

       }
       public PlaylistItemListResponse.PlaylistItem[] list_channel_videos(string id)
       {
           return list_playlistItems(
               channels(id).items[0].contentDetails.relatedPlaylists.uploads);
       }
       public PlaylistItemListResponse.PlaylistItem[] list_my_videos()
       {
           return list_playlistItems(
               channels().items[0].contentDetails.relatedPlaylists.uploads);
       }
       public PlaylistListResponse list_playlist(string channelId=null)
         {
             string url = "https://www.googleapis.com/youtube/v3/playlists?part=snippet&mine=true&maxResults=50";
           if(channelId !=null)
         url = "https://www.googleapis.com/youtube/v3/playlists?part=snippet&channelId="+channelId+"&maxResults=50";
       
           return Newtonsoft.Json.JsonConvert.DeserializeObject<PlaylistListResponse>(
                 GET(url));
               
         }
         public ChannelListResponse channels(string id=null)
         {
             string url=  "https://www.googleapis.com/youtube/v3/channels?part=contentDetails&mine=true";
             if (id != null)
                 url = "https://www.googleapis.com/youtube/v3/channels?part=contentDetails&id=" + id;
     
                      return Newtonsoft.Json.JsonConvert.DeserializeObject<ChannelListResponse>( GET(
                          url));
         }
         public PlaylistItem  add_playlistItems(string playlist_id,string video_id)
         {
             var req = WebRequest.Create("https://www.googleapis.com/youtube/v3/playlistItems?part=snippet");
             req.Method = "POST";
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
             req.ContentType = "application/json";
             var j = new JObject(new JProperty("snippet", new JObject(
                new JProperty("playlistId", new JValue(playlist_id)),
                new JProperty("resourceId", new JObject(
                    new JProperty("kind", new JValue("youtube#video")),
                    new JProperty("videoId", new JValue(video_id))
                    ))
                 )));
             var w = new StreamWriter(req.GetRequestStream());
             w.Write(j.ToString());
             w.Close();
             var res = req.GetResponse();
             var r = new StreamReader(res.GetResponseStream())
             .ReadToEnd();
             return Newtonsoft.Json.JsonConvert.DeserializeObject<PlaylistItem>(r);
         }
         public void  delete_playlistItems(string id)
         {
             var req = WebRequest.Create("https://www.googleapis.com/youtube/v3/playlistItems?id=" + id);
             req.Method = "DELETE";
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
             var st = req.GetRequestStream();
             var res = req.GetResponse();
               //  );
         }
         public PlaylistItemListResponse.PlaylistItem[] list_playlistItems(string id)
         {
             var list = new List<PlaylistItemListResponse.PlaylistItem>();
             string nextPageToken = null;
             while (true)
             { 
                 string url= string.Format(
                     "https://www.googleapis.com/youtube/v3/playlistItems?maxResults=50&part=snippet&playlistId=" + id);

                 if(nextPageToken !=null)
                 url =string.Format("https://www.googleapis.com/youtube/v3/playlistItems?maxResults=50&part=snippet&playlistId={0}&pageToken={1}", id,nextPageToken);

                 var s = GET(url);

                 var r = Newtonsoft.Json.JsonConvert.DeserializeObject<PlaylistItemListResponse>(s);
                 list.AddRange(r.items);
                 if (list.Count == r.pageInfo.totalResults)
                     break;
                 nextPageToken = r.nextPageToken;
             }
              return list.ToArray();
         }
         public string  captions(string video_id)
         {
             var req = WebRequest.Create("https://www.googleapis.com/youtube/v3/captions?part=id&videoId="+video_id);
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
            
            var  res = req.GetResponse();
             var ss = new StreamReader(res.GetResponseStream()).ReadToEnd();
             res.Dispose();
             return ss;
         }
         public string get_caption(string caption_id)
         {
             var req = WebRequest.Create("https://www.googleapis.com/youtube/v3/captions/" + caption_id );
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);

             var res = req.GetResponse();
             var ss = new StreamReader(res.GetResponseStream()).ReadToEnd();
             res.Dispose();
             File.WriteAllText("pp.srt", ss);
             return ss;
         }
         public Video upload(FileInfo fi,string title,string desc,IProgress<double> prg=null)
         {
             var req = WebRequest.Create("https://www.googleapis.com/upload/youtube/v3/videos?uploadType=resumable&part=snippet,status")
                 as HttpWebRequest;
             req.AllowAutoRedirect = false;
             req.Method = "POST";
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
             req.Headers.Add("X-Upload-Content-Length",fi.Length.ToString());
             req.Headers.Add("x-upload-content-type", "application/octet-stream");
             req.ContentType = "application/json";
             var st = req.GetRequestStream();
             var w = new StreamWriter(st);
             var j = new JObject(new JProperty("snippet",
                 new JObject(new JProperty("title",new JValue(title)),
                     new JProperty("description",new JValue(desc))
                     )
                 ),
                                new JProperty("status", 
                                    new JObject(
                                        new JProperty("privacyStatus","private")
                                        )
                                    )
                 );
             w.Write(j.ToString());
             w.Close();
             var res = req.GetResponse();
             var url2 = res.Headers[HttpResponseHeader.Location];
            // var ss = new StreamReader(res.GetResponseStream()).ReadToEnd();
             res.Dispose();
             req = WebRequest.Create(url2) as HttpWebRequest;
             req.Method = "POST";
             req.ContentType = "application/octet-stream";
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
             st = req.GetRequestStream();
             var si = fi.OpenRead();
             if (prg == null)
                 si.CopyTo(st);
             else
             {
                 byte[] buf = new byte[1024 * 5];
                 while (true)
                 {
                     int len = si.Read(buf, 0, buf.Length);
                     if (len == 0)
                         break;
                     st.Write(buf, 0, len);
                     double d = si.Position;
                     d /=(si.Length/100);
                     prg.Report(d);
                 }
             }
             si.Close();
             st.Close();
             res = req.GetResponse();
            var ss = new StreamReader(res.GetResponseStream()).ReadToEnd();
            res.Dispose();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Video>(ss);
         }
         public void delete_video(string id)
         {
             var req = WebRequest.Create("https://www.googleapis.com/youtube/v3/videos?id="+id);
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
             req.Method = "DELETE";
             var res = req.GetResponse();
             res.Dispose();
             
         }
         public Video update_video(string id, string title, string desc, string privacyStatus)
         {
             var req = WebRequest.Create("https://www.googleapis.com/youtube/v3/videos?part=snippet,status");
             req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
             req.Method = "PUT";
             req.ContentType = "application/json";
             var st = req.GetRequestStream();
             var w = new StreamWriter(st);
             var j = new JObject(
                 new JProperty("id",new JValue(id)),
                 new JProperty("snippet",
                 new JObject(new JProperty("title", new JValue(title)),
                     new JProperty("description", new JValue(desc))
                     )
                 ),
                                new JProperty("status",
                                    new JObject(
                                        new JProperty("privacyStatus", privacyStatus)
                                        )
                                    )
                 );
             w.Write(j.ToString());
             w.Close();
             var res = req.GetResponse();
             var ss = new StreamReader(res.GetResponseStream()).ReadToEnd();
             res.Dispose();
             return Newtonsoft.Json.JsonConvert.DeserializeObject<Video>(ss);
         }

    }
}
