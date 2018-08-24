using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace google.gdrive
{
    public class File
    {

        public override string ToString()
        {
            return name;
        }
             [JsonProperty]
        public string kind{get; set;}
        [JsonProperty]
        public string id{get; set;}
        [JsonProperty]
         public string name{get; set;}
        [JsonProperty]
         public string mimeType { get; set; }
         [JsonProperty]
         public string description { get; set; }
        [JsonProperty]
         public bool trashed { get; set; } 

        [JsonProperty]
         public string[] parents { get; set; } 
 
        [JsonProperty]
        public string webContentLink{get; set;}
         [JsonProperty]
  public DateTime createdTime { get; set; } 
         [JsonProperty]
 public DateTime modifiedTime { get; set; } 
         [JsonProperty]
 public bool shared { get; set; } 
         [JsonProperty]
 public bool ownedByMe { get; set; } 

         [JsonProperty]
       public Capabilities capabilities{ get; set; }  

   public class Capabilities {
        [JsonProperty]
  public bool canAddChildren{ get; set; }  
        [JsonProperty]
  public bool canChangeCopyRequiresWriterPermission{ get; set; } 
        [JsonProperty]
  public bool canChangeViewersCanCopyContent{ get; set; }  
        [JsonProperty]
  public bool canComment{ get; set; }  
        [JsonProperty]
  public bool canDelete{ get; set; }  
        [JsonProperty]
  public bool canDownload{ get; set; }  
        [JsonProperty]
  public bool canEdit{ get; set; }  
        [JsonProperty]
  public bool canListChildren{ get; set; }  
        [JsonProperty]
  public bool canMoveItemIntoTeamDrive{ get; set; }  
        [JsonProperty]
  public bool canReadRevisions{ get; set; } 
        [JsonProperty]
  public bool canRemoveChildren{ get; set; }  
        [JsonProperty]
  public bool canRename{ get; set; }  
        [JsonProperty]
  public bool canShare{ get; set; }  
        [JsonProperty]
  public bool canTrash{ get; set; }  
        [JsonProperty]
  public bool canUntrash{ get; set; }  
 }
        [JsonProperty]
        public long size{ get; set; }

        [JsonProperty]
        public string md5Checksum { get; set; } 

         [JsonProperty]
 public int quotaBytesUsed{ get; set; }  
}

    
    public class ListFilesResponse
    {
        [JsonProperty]
        public string  kind{get; set;}
        [JsonProperty]
        public string nextPageToken{get; set;}
        [JsonProperty]
    public bool incompleteSearch{get; set;}
        [JsonProperty]
        public File[] files { get; set; }
    }
    public class AboutResponse
    {
        [JsonProperty]
        public User user { get; set; }
        public class User
        {
            [JsonProperty]
            public string kind { get; set; }
             [JsonProperty]
            public string displayName { get; set; }
             [JsonProperty]
            public bool me { get; set; }
             [JsonProperty]
            public string permissionId { get; set; }
             [JsonProperty]
            public string emailAddress { get; set; }
        }
         [JsonProperty]
        public StorageQuota storageQuota { get; set; }
        public class StorageQuota
        {
             [JsonProperty]
            public long limit { get; set; }
             [JsonProperty]
            public long usage { get; set; }
             [JsonProperty]
            public long usageInDrive { get; set; }
             [JsonProperty]
            public int usageInDriveTrash { get; set; }
        }
    }
}