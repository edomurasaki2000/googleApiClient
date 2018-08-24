using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
namespace google.gdrive
{
   public  class Client
    {
       static public string scope = "https://www.googleapis.com/auth/drive";
       public string access_token { get; set; }
       T GET<T>(Uri url)
       {
           var req = WebRequest.Create(url);
            req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
           var res = req.GetResponse();
           var r = new System.IO.StreamReader(res.GetResponseStream());
           var json = r.ReadToEnd();
           res.Dispose();
           return JsonConvert.DeserializeObject<T>(json);
       }

       public File get(string fileId)
       {
           return GET<File>(new Uri("https://www.googleapis.com/drive/v3/files/" + fileId + "?fields=*"));
       }
       public File move(string fileId, string addParents=null,string removeParents=null)
       {
           var builder=new UriBuilder( "https://www.googleapis.com/drive/v3/files/"+ fileId);
           var sb = new StringBuilder();
           sb.Append("fields=*");
           if( addParents!=null && removeParents!=null)
           {
               sb.AppendFormat("&addParents={0}",addParents);
               sb.AppendFormat("&removeParents={0}",removeParents);
           }
        builder.Query= sb.ToString();
        var req = WebRequest.Create(builder.Uri);
           req.Method = "PATCH";
         req.ContentType = "application/json";
         req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
         
         var w = new System.IO.StreamWriter(req.GetRequestStream());
         var j = new Newtonsoft.Json.Linq.JObject(new Newtonsoft.Json.Linq.JProperty("name", new Newtonsoft.Json.Linq.JValue("new01.zip")));
         w.Write(j.ToString());
           w.Close();
         var res = req.GetResponse();
         var json = new System.IO.StreamReader(res.GetResponseStream()).ReadToEnd();
         res.Dispose();
         return JsonConvert.DeserializeObject<File>(json);
       }
       public File create()
       {

           var req = WebRequest.Create("https://www.googleapis.com/drive/v3/files?uploadType=multipart&fields=*");
            req.Method = "POST";
            req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
            req.ContentType = " multipart/related; boundary=foo_bar_baz";
            var w = new System.IO.StreamWriter(req.GetRequestStream());
            w.WriteLine("--foo_bar_baz");
           w.WriteLine("Content-Type: application/json; charset=UTF-8");
           w.WriteLine();
           var j = new Newtonsoft.Json.Linq.JObject(
               new Newtonsoft.Json.Linq.JProperty("name",new Newtonsoft.Json.Linq.JValue("new folder01")),
               new Newtonsoft.Json.Linq.JProperty("mimeType", new Newtonsoft.Json.Linq.JValue("application/vnd.google-apps.folder"))
           );
           w.WriteLine(j.ToString());
           w.WriteLine("--foo_bar_baz--");
           w.Close();
           var res = req.GetResponse();
           var json = new System.IO.StreamReader(res.GetResponseStream()).ReadToEnd();
           res.Dispose();
           return JsonConvert.DeserializeObject<File>(json);
       }

       public File upload_a_file_with_a_resumable_session(string location,System.IO.FileInfo fi)
       {
          
           var req = WebRequest.Create(location);
           req.Method = "PUT";
           req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
          
           req.ContentType = "application/application/octet-stream";

           var st = req.GetRequestStream();
           var str = fi.OpenRead();
           str.CopyTo(st);
           st.Close();
           var res = req.GetResponse();
           var s = new System.IO.StreamReader(res.GetResponseStream()).ReadToEnd();
           res.Dispose();
           return JsonConvert.DeserializeObject<File>(s);
       }
       public String Initiating_a_resumable_upload_session(string parent, System.IO.FileInfo fi)
   {
       var req = WebRequest.Create("https://www.googleapis.com/upload/drive/v3/files?uploadType=resumable");
       req.Method = "POST";
       req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
          
       req.ContentType = "application/json; charset=UTF-8";
       var j = new Newtonsoft.Json.Linq.JObject(
           new Newtonsoft.Json.Linq.JProperty("name",new Newtonsoft.Json.Linq.JValue(fi.Name)),
         
           new Newtonsoft.Json.Linq.JProperty("parents",  new Newtonsoft.Json.Linq.JArray(new Newtonsoft.Json.Linq.JValue(parent)))
           );
       var w = new System.IO.StreamWriter(req.GetRequestStream());
       w.Write(j.ToString());
       w.Close();
       var res = req.GetResponse();
      var location=  res.Headers[HttpResponseHeader.Location];
      var s =new System.IO.StreamReader(res.GetResponseStream()).ReadToEnd();
      res.Dispose();
           return location;
   }
       public void delete(string fileId)
       {
           var builder = new UriBuilder("https://www.googleapis.com/drive/v3/files/" + fileId);
        
           var req = WebRequest.Create(builder.Uri);
           req.Method = "DELETE";
           req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
      
           var res = req.GetResponse();
           res.Dispose();
       }
       public bool check_file_md5(File f, System.IO.Stream st)
       {
           var bytes = System.Security.Cryptography.MD5.Create()
                    .ComputeHash(st);
           var m = BitConverter.ToString(bytes).Replace("-", String.Empty);
            
           return  f.md5Checksum.ToUpper().Equals(m.ToUpper());

       }
       public void download(string id,System.IO.Stream stw)
       {
           var req = WebRequest.Create(
               new Uri("https://www.googleapis.com/drive/v3/files/" + id + "?alt=media")
               );
           req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
           var res = req.GetResponse();
           var st = res.GetResponseStream();
           st.CopyTo(stw);
           res.Dispose();
          
       }
       public AboutResponse about()
       {
          return GET<AboutResponse>(new Uri(
              "https://www.googleapis.com/drive/v3/about?fields=user,storageQuota"));
          
       }
       public string upload(System.IO.Stream st)
       {
           var req = WebRequest.Create("https://www.googleapis.com/upload/drive/v3/files?uploadType=media");
           req.Method = "POST";
           req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
          
           req.ContentType = "application/x-zip-compressed";
           var stw = req.GetRequestStream();
           st.CopyTo(stw);
           stw.Close();
           var res = req.GetResponse();
           var s = new System.IO.StreamReader(res.GetResponseStream()).ReadToEnd();
           res.Dispose();
           return s;
       }
       public ListFilesResponse list_files(string folderId)
       {

        
           var builder = new UriBuilder("https://www.googleapis.com/drive/v3/files");
           var sb = new StringBuilder();
           sb.AppendFormat( "q='{0}' in parents",folderId);
              sb.Append("&fields=kind,nextPageToken,incompleteSearch,files(*)");
              builder.Query = sb.ToString();
           return GET<ListFilesResponse>(builder.Uri);

       }
       public ListFilesResponse list_files()
       {
           var builder = new UriBuilder("https://www.googleapis.com/drive/v3/files?fields=kind,nextPageToken,incompleteSearch,files(id,name,mimeType,description,trashed,parents,webContentLink, createdTime,modifiedTime,shared,ownedByMe,capabilities,md5Checksum,size,quotaBytesUsed)");
           return GET<ListFilesResponse>(builder.Uri);
         
       }
    }
}
